using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.Models.Study;
using Dotnet.ViewModels.Work;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Controllers
{
	[Authorize(Roles="teacher")]
    public class WorkController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<WorkController> _logger;

        public WorkController(ILogger<WorkController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult AddTask()
        {
			User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.FirstOrDefault(t => (t.UserId == user.Id));

			if (teacher == null) return RedirectToAction("Index", "Home");

			List<SubjectTeacher> subjectTeacher = _context.SubjectTeacher.Where(x => x.TeacherId == teacher.Id).ToList();
			
			List<Subject> subjects = new List<Subject>();

			foreach (var row in subjectTeacher)
			{
				Subject subject = _context.Subjects.FirstOrDefault(x => x.Id == row.SubjectId);
				if (subject != null) subjects.Add(subject);
			}

			ViewBag.typesWorks = _context.TypesWorks.ToList();
			ViewBag.subjects = subjects;

			ViewBag.studySubgroups = _context.StudySubgroups.ToList();
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();

            return View();
        }

		public IActionResult Tasks()
		{
			User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.FirstOrDefault(t => (t.UserId == user.Id));

			if (teacher == null) return RedirectToAction("Index", "Home");

			ViewBag.works = _context.Works.Where(x => x.TeacherId == teacher.Id).OrderByDescending(x => x.Id).ToList();
			ViewBag.subjects = _context.Subjects.ToList();
			ViewBag.typesWorks = _context.TypesWorks.ToList();

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTask(WorkViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
				Teacher teacher = _context.Teachers.FirstOrDefault(t => (t.UserId == user.Id));

				if (teacher == null) 
				{
					ModelState.AddModelError("", "Некорректные данные.");
					return RedirectToAction("AddWork", "Work");
				}

				Work newWork = new Work {
					Id				= 0,
					Description		= viewModel.Description,
					IsObligation 	= viewModel.IsObligation,
					DateAdded 		= viewModel.DateAdded,
					DateDeparture 	= viewModel.DateDeparture,
					CountAttempts 	= viewModel.CountAttempts,
					SubjectId 		= viewModel.SubjectId,
					TypeWorksId 	= viewModel.TypeWorksId,
					TeacherId		= teacher.Id,
				};

				await _context.Works.AddAsync(newWork);
				await _context.SaveChangesAsync();

				foreach (var subgroup in viewModel.StudySubgroupsId)
				{
					StudySubgroupWork newRelatedRecord = new StudySubgroupWork {
						WorkId			= newWork.Id,
						StudySubgroupId	= subgroup,
					};
					await _context.StudySubgroupWork.AddAsync(newRelatedRecord);
				}

				await _context.SaveChangesAsync();
			}
			else ModelState.AddModelError("", "Некорретные данные.");

			return RedirectToAction("AddTask", "Work");
		}
	}
}
