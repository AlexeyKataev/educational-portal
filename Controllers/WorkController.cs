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
using Microsoft.EntityFrameworkCore;

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

		[HttpGet]
		[Authorize(Roles="teacher")]
		public IActionResult EditTask(int taskId)
		{
			User user = _context.Users.AsNoTracking().FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.AsNoTracking().FirstOrDefault(t => (t.UserId == user.Id));

			Work work = _context.Works.AsNoTracking().FirstOrDefault(x => x.Id == taskId);
			
			FileWork fileWork = _context.FileWork.AsNoTracking().FirstOrDefault(x => x.WorkId == taskId);
			Dotnet.Models.File file = fileWork == null ? null : _context.Files.AsNoTracking().FirstOrDefault(f => f.Id == fileWork.FileId);

			if (teacher == null || work.TeacherId != teacher.Id) return RedirectToAction("Index", "Home");

			List<SubjectTeacher> subjectTeacher = _context.SubjectTeacher.Where(x => x.TeacherId == teacher.Id).ToList();
			List<StudySubgroupWork> studySubgroupWork = _context.StudySubgroupWork.AsNoTracking().Where(x => x.WorkId == taskId).ToList();

			List<int> studySubgroupChecked = new List<int>();
			List<Subject> subjects = new List<Subject>();

			foreach (var row in subjectTeacher)
			{
				Subject subject = _context.Subjects.FirstOrDefault(x => x.Id == row.SubjectId);
				if (subject != null) subjects.Add(subject);
			}

			foreach (var row in studySubgroupWork) studySubgroupChecked.Add(row.StudySubgroupId);

			ViewBag.typesWorks = _context.TypesWorks.AsNoTracking().ToList();
			ViewBag.subjects = subjects;

			ViewBag.studySubgroups = _context.StudySubgroups.AsNoTracking().ToList();
			ViewBag.studyGroups = _context.StudyGroups.AsNoTracking().ToList();
			ViewBag.specialties = _context.Specialties.AsNoTracking().ToList();
			ViewBag.faculties = _context.Faculties.AsNoTracking().ToList();
			ViewBag.institutions = _context.Institutions.AsNoTracking().ToList();

			ViewBag.studySubgroupWork = _context.StudySubgroupWork.AsNoTracking().ToList();

			ViewBag.studySubgroupChecked = studySubgroupChecked;
			ViewBag.work = work;
			ViewBag.pickFile = file == null ? null : file;

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

			ViewBag.subjects = _context.Subjects.AsNoTracking().ToList();
			ViewBag.typesWorks = _context.TypesWorks.AsNoTracking().ToList();
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
				User user = await _context.Users.FirstOrDefaultAsync(u => (u.Login == User.Identity.Name));
				Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(t => (t.UserId == user.Id));

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

		[HttpPost]
		[Authorize(Roles="teacher")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesTask(EditWorkViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				User user = await _context.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
				Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.UserId == user.Id);
				Work work = await _context.Works.FirstOrDefaultAsync(w => w.Id == viewModel.Id);
				List<StudySubgroupWork> studySubgroupWork = _context.StudySubgroupWork.Where(x => x.WorkId == viewModel.Id).ToList();

				if (work == null || teacher.Id != work.TeacherId) 
				{
					ModelState.AddModelError("", "Некорректные данные.");
					return RedirectToAction("Tasks", "Work");
				}

				work.Description		= viewModel.Description;
				work.IsObligation 		= viewModel.IsObligation;
				work.DateDeparture 		= viewModel.DateDeparture;
				work.CountAttempts 		= viewModel.CountAttempts;
				work.SubjectId 			= viewModel.SubjectId;
				work.TypeWorksId 		= viewModel.TypeWorksId;
				work.TeacherId			= teacher.Id;

				await _context.SaveChangesAsync();

				foreach (var x in studySubgroupWork)
					if (viewModel.StudySubgroupsId.Contains(x.StudySubgroupId) == false) _context.StudySubgroupWork.Remove(x);

				foreach (var x in viewModel.StudySubgroupsId)
				{
					if (await _context.StudySubgroupWork.FirstOrDefaultAsync(r => r.StudySubgroupId == x && r.WorkId == viewModel.Id) == null)
						await _context.StudySubgroupWork.AddAsync(new StudySubgroupWork { Id = 0, WorkId = viewModel.Id, StudySubgroupId = x});
				}

				await _context.SaveChangesAsync();
			}
			else ModelState.AddModelError("", "Некорретные данные.");

			return RedirectToAction("Tasks", "Work");
		}

		[HttpGet]
		[Authorize(Roles="teacher")]
		public IActionResult DeleteTask(int taskId)
		{	
			User user = _context.Users.AsNoTracking().FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.AsNoTracking().FirstOrDefault(t => (t.UserId == user.Id));

			List<StudySubgroupWork> studySubgroupWork = _context.StudySubgroupWork.Where(x => x.WorkId == taskId).ToList();
			Work work = _context.Works.FirstOrDefault(x => x.Id == taskId);

			if (studySubgroupWork.Count > 0)
				foreach (var row in studySubgroupWork)
					_context.Remove(row);

			if (work.TeacherId == teacher.Id)
				_context.Remove(work);

			_context.SaveChangesAsync();

			return RedirectToAction("Tasks", "Work");
		}
				
		[HttpGet]
		[Authorize(Roles="teacher, admin")]
		public IActionResult DownloadFile(string fileName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
			byte[] bytes = System.IO.File.ReadAllBytes(path);
	
			return File(bytes, "application/octet-stream", fileName);
		}

		[HttpGet]
		[Authorize(Roles="teacher")]
		public IActionResult DeleteFile(int fileId)
		{
			User user = _context.Users.AsNoTracking().FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.AsNoTracking().FirstOrDefault(t => (t.UserId == user.Id));

			FileWork fileWork = _context.FileWork.FirstOrDefault(x => x.FileId == fileId);
			Work work = _context.Works.AsNoTracking().FirstOrDefault(x => x.Id == fileWork.WorkId);
			
			if (work.TeacherId == teacher.Id)
			{
				Dotnet.Models.File file = _context.Files.FirstOrDefault(f => f.Id == fileWork.FileId);
				_context.Files.Remove(file);
				_context.FileWork.Remove(fileWork);

				_context.SaveChanges();
			}

			return Redirect(Request.Headers["Referer"].ToString());
		}
	}
}
