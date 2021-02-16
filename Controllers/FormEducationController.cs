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
    public class FormEducationController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<FormEducationController> _logger;

        public FormEducationController(ILogger<FormEducationController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult FormsEducation()
        {
			List<FormEducation> formsEducation = _context.FormsEducation.ToList();

			ViewBag.allFormsEducation = formsEducation;

            return View();
        }

        public IActionResult AddFormEducation()
        {
            return View();
        }

		[HttpGet]
		public IActionResult EditFormEducation(int formEducationId)
		{
			FormEducation formEducation = _context.FormsEducation.FirstOrDefault(
				f => (f.Id == formEducationId)
			);

			ViewData["Id"]		= formEducation.Id;
			ViewData["Name"]	= formEducation.Name;
			ViewData["Code"]	= formEducation.Code;

			return View();
		}	

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesFormEducation(EditFormEducationViewModel model)
		{
			if (ModelState.IsValid)
			{
				FormEducation formEducationEdt = await _context.FormsEducation.FirstOrDefaultAsync(
					f =>
					(
						f.Id == model.Id
					)
				);

				FormEducation rowCheck = await _context.FormsEducation.FirstOrDefaultAsync(
					f =>
					(
						f.Name == model.Name
					)
				);

				if (rowCheck == null)
				{
					formEducationEdt.Name	= model.Name;
					formEducationEdt.Code	= model.Code;

					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Данная форма обучения уже существует");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("FormsEducation", "FormEducation");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateFormEducation(FormEducationViewModel model)
		{
			if (ModelState.IsValid)
			{
				FormEducation formEducation = await _context.FormsEducation.FirstOrDefaultAsync(
					f => 
					(
						(f.Name == model.Name)
					)
				);
				if (formEducation == null)
				{					
					formEducation.Name	= model.Name;
					formEducation.Code	= model.Code;

					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Некорретные данные");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("AddFormEducation", "FormEducation");
		}
	}
}
