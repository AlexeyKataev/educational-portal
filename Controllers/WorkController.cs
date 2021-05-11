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
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace Dotnet.Controllers
{
	[Authorize(Roles="student, teacher")]
    public class WorkController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<WorkController> _logger;

        public WorkController(ILogger<WorkController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

		[Authorize(Roles="teacher")]
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

		[Authorize(Roles="teacher")]
		public IActionResult Tasks()
		{
			User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.FirstOrDefault(t => (t.UserId == user.Id));

			if (teacher == null) return RedirectToAction("Index", "Home");

			ViewBag.works = _context.Works.Where(x => x.TeacherId == teacher.Id).OrderByDescending(x => x.Id).ToList();
			ViewBag.subjects = _context.Subjects.ToList();
			ViewBag.typesWorks = _context.TypesWorks.ToList();

			ViewBag.fileWork = _context.FileWork.ToList();
			ViewBag.files = _context.Files.ToList();

			ViewBag.studySubgroupWork = _context.StudySubgroupWork.ToList();
			ViewBag.studySubgroups = _context.StudySubgroups.ToList();
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();

			return View();
		}

		[Authorize(Roles="student")]
		public IActionResult MyTasks()
		{
			User me = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			Student aboutMe = _context.Students.FirstOrDefault(s => (s.UserId == me.Id));
			StudySubgroup studySubgroup = _context.StudySubgroups.FirstOrDefault(s => (s.Id == aboutMe.StudySubgroupId));
			StudyGroup studyGroup = _context.StudyGroups.FirstOrDefault(s => (s.Id == studySubgroup.StudyGroupId));
			Specialty specialty = _context.Specialties.FirstOrDefault(s => (s.Id == studyGroup.SpecialtyId));

			List<StudySubgroupWork> studySubgroupWork = _context.StudySubgroupWork.Where(r => (r.StudySubgroupId == studySubgroup.Id)).ToList();
			List<Work> works = _context.Works.OrderByDescending(d => d.DateAdded).ToList();

			foreach (var work in works.ToList())
				if (studySubgroupWork.Find(x => x.WorkId == work.Id) == null) works.RemoveAt(works.IndexOf(work));

			if (aboutMe != null) ViewBag.aboutMe = $"{specialty.Code} {specialty.Name} • {studyGroup.Name}, подгруппа {studySubgroup.Name}";

			ViewBag.subjects = _context.Subjects.ToList();
			ViewBag.typesWorks = _context.TypesWorks.ToList();
			ViewBag.fileWork = _context.FileWork.ToList();
			ViewBag.files = _context.Files.ToList();

			ViewBag.myWorks = works;

			return View();
		}

		[HttpPost]
		[Authorize(Roles="teacher")]
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

				if (viewModel.File != null)
				{
					var filename = ContentDispositionHeaderValue.Parse(viewModel.File.ContentDisposition).FileName.Trim('"');
					var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", viewModel.File.FileName);
					using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
					{
						await viewModel.File.CopyToAsync(stream);
					}

					Models.File uploadedFile = new Models.File {
						Id				= 0,
						Name			= filename,
						Pseudonym		= filename,
						PathFile		= $"UploadedFiles/{viewModel.File.FileName}",
						Vanish			= false,
						NeedToDelete	= new DateTime(0001, 01, 01, 01, 01, 01),
						DateAdded		= DateTime.Now,
						UserId			= user.Id,
					};

					await _context.Files.AddAsync(uploadedFile);
					await _context.SaveChangesAsync();

					FileWork fileWork = new FileWork {
						Id 		= 0,
						FileId 	= uploadedFile.Id,
						WorkId 	= newWork.Id, 
					};

					await _context.FileWork.AddAsync(fileWork);
					await _context.SaveChangesAsync();
				}				
			}
			else ModelState.AddModelError("", "Некорретные данные.");

			return RedirectToAction("AddTask", "Work");
		}
				
		[HttpGet]
		[Authorize(Roles="teacher, admin")]
		public IActionResult DownloadFile(string fileName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
			byte[] bytes = System.IO.File.ReadAllBytes(path);
	
			return File(bytes, "application/octet-stream", fileName);
		}
	}
}
