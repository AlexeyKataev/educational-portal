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
		private readonly ILogger<HomeController> _logger;

        public FormEducationController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult FormsEducation()
        {
            return View();
        }

        public IActionResult AddFormEducation()
        {
            return View();
        }

		[HttpGet]
		public IActionResult EditFormEducation(int groupId)
		{
			return View();
		}	

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesFormEducation() // ПЕРЕДАТЬ МОДЕЛЬ
		{
			return null;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateFormEducation(FormEducation model)
		{
			if (ModelState.IsValid)
			{
				FormEducation formEducation = await _context.FormsEducation.FirstOrDefaultAsync(f => (f.Name == model.Name)); // ПЕРЕДЕЛАТЬ
				if (formEducation == null)
				{
					formEducation = new FormEducation {
						Name	= model.Name,
						Code	= model.Code,
					};

					_context.FormsEducation.Add(formEducation);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddFormEducation", "FormEducation");
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
