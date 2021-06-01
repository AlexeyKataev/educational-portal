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

		private void StudySubjectToView()
		{
			ViewBag.allStudySubjects = _context.Subjects.ToList();
		}

		public IActionResult StudySubjects()
		{
			StudySubjectToView();
			return View();
		}

		public IActionResult AddStudySubject() => View();

		private void EditableStudySubject(int studySubjectId)
		{
			ViewBag.EditRow = _context.Subjects.FirstOrDefault(s => s.Id == studySubjectId);
		}

		[HttpGet]
		public IActionResult EditStudySubject(int studySubjectId)
		{
			EditableStudySubject(studySubjectId);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesStudySubject(EditStudySubjectViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Subject subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == viewModel.Id);
				Subject rowCheck = await _context.Subjects.FirstOrDefaultAsync(s => s.Name == viewModel.Name);

				if (rowCheck == null)
				{
					subject.Name = viewModel.Name;

					await _context.SaveChangesAsync();

					return RedirectToAction("StudySubjects", "StudySubject");
				}
				else ModelState.AddModelError("", "Учебный предмет с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			EditableStudySubject(viewModel.Id);

			return View("EditStudySubject", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStudySubject(StudySubjectViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Subject subject = _context.Subjects.FirstOrDefault(s => (s.Name == viewModel.Name));
				
				if (subject == null)
				{
					subject = new Subject { Name = viewModel.Name };

					_context.Subjects.Add(subject);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddStudySubject", "StudySubject");
				}
				else ModelState.AddModelError("", "Учебный предмет с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			return View("AddStudySubject", viewModel);
		}
	}
}