using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.WebApp.FormEducation;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Controllers.WebApp
{
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class FormEducationController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<FormEducationController> _logger;

        public FormEducationController(ILogger<FormEducationController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

		private void FormEducationToView()
		{
			ViewBag.allFormsEducation = _context.FormsEducation;
		}

        public IActionResult FormsEducation()
        {
			FormEducationToView();
            return View();
        }

        public IActionResult AddFormEducation() => View();

		private void EditableFormEducation(long formEducationId)
		{
			FormEducation formEducation = _context.FormsEducation.FirstOrDefault(f => f.Id == formEducationId);

			ViewData["Id"]		= formEducation.Id;
			ViewData["Name"]	= formEducation.Name;
			ViewData["Code"]	= formEducation.Code;
		}

		[HttpGet]
		public IActionResult EditFormEducation(long formEducationId)
		{
			EditableFormEducation(formEducationId);
			return View();
		}	

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesFormEducation(EditFormEducationViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				FormEducation formEducationEdit = await _context.FormsEducation.FirstOrDefaultAsync(f => f.Id == viewModel.Id);
				FormEducation rowCheck = await _context.FormsEducation.FirstOrDefaultAsync(f => (f.Name == viewModel.Name) && (f.Id != viewModel.Id));

				if (rowCheck == null)
				{
					formEducationEdit.Name = viewModel.Name;
					formEducationEdit.Code = viewModel.Code;

					await _context.SaveChangesAsync();

					return RedirectToAction("FormsEducation", "FormEducation");
				}
				else ModelState.AddModelError("", "Форма обучения с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			EditableFormEducation(viewModel.Id);

			return View("EditFormEducation", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateFormEducation(FormEducationViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				FormEducation formEducation = await _context.FormsEducation.FirstOrDefaultAsync(
					f => (
						(f.Name == viewModel.Name) && 
						(f.Code == viewModel.Code)
					));

				if (formEducation == null)
				{					
					formEducation = new FormEducation {
						Name	= viewModel.Name,
						Code	= viewModel.Code,
					};			
					
					await _context.FormsEducation.AddAsync(formEducation);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddFormEducation", "FormEducation");
				}
				else ModelState.AddModelError("", "Вид формы обучения с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			FormEducationToView();

			return View("AddFormEducation", viewModel);
		}
	}
}
