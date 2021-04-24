using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Controllers
{
	[Authorize]
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class StudySubjectController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<StudySubjectController> _logger;

		public StudySubjectController(ILogger<StudySubjectController> logger, ApplicationContext context)
		{
			_context = context;
			_logger = logger;
		}

		public IActionResult StudySubjects()
		{
			List<Subject> subjects = _context.Subjects.ToList();

			ViewBag.allStudySubjects = subjects;

			return View();
		}

		public IActionResult AddStudySubject() => View();

		[HttpGet]
		public IActionResult EditStudySubject(int studySubjectId)
		{
			Subject subject = _context.Subjects.FirstOrDefault(s => (s.Id == studySubjectId));

			ViewBag.EditRow = subject;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesStudySubject(EditStudySubjectViewModel model)
		{
			if (ModelState.IsValid)
			{
				Subject subjectEdt = await _context.Subjects.FirstOrDefaultAsync(s => (s.Id == model.Id));

				Subject rowCheck = await _context.Subjects.FirstOrDefaultAsync(s => (s.Name == model.Name));

				if (rowCheck == null)
				{
					subjectEdt.Name	= model.Name;

					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Данный учебный предмет уже существует");
			}
			else 
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("StudySubjects", "StudySubject");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStudySubject(StudySubjectViewModel model)
		{
			if (ModelState.IsValid)
			{
				Subject subject = _context.Subjects.FirstOrDefault(s => (s.Name == model.Name));
				if (subject == null)
				{
					subject = new Subject {
						Name	= model.Name,
					};

					_context.Subjects.Add(subject);
					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Некорректные данные");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("AddStudySubject", "StudySubject");
		}
	}
}