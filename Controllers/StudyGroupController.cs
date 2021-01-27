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
    public class StudyGroupController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public StudyGroupController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult Groups()
        {
            return View();
        }

        public IActionResult AddStudyGroup()
        {
            return View();
        }

		[HttpGet]
		public IActionResult EditStudyGroup(int groupId)
		{
			return View();
		}

		[HttpGet]
		public IActionResult EditStudySubgroup(int subGroupId)
		{
			return View();
		}		

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesStudyGroup() // ПЕРЕДАТЬ МОДЕЛЬ
		{
			return null;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesStudySubgroup() // ПЕРЕДАТЬ МОДЕЛЬ
		{
			return null;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStudyGroup(StudyGroup model)
		{
			if (ModelState.IsValid)
			{
				StudyGroup studyGroup = await _context.StudyGroups.FirstAsync(g => (g.Name == model.Name));
				if (studyGroup == null)
				{
					studyGroup = new StudyGroup {
						Name		= model.Name,
						Code		= model.Code,
						DateStart	= Convert.ToDateTime(model.DateStart),
						DateEnd		= Convert.ToDateTime(model.DateEnd),
					};

					_context.StudyGroups.Add(studyGroup);
					await _context.SaveChangesAsync();

					RedirectToAction("AddStudyGroup", "StudyGroup");
				}
				else
					ModelState.AddModelError("", "Некорретные данные");
			}
			return View(model);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStudySubgroup(StudySubgroup model)
		{
			return null;
		}
	}
}
