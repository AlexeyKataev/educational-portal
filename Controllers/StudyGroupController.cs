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
using Dotnet.ViewModels.StudyGroup;

namespace Dotnet.Controllers
{
	[Authorize]
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class StudyGroupController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<StudyGroupController> _logger;

        public StudyGroupController(ILogger<StudyGroupController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult StudyGroups()
        {
			ViewBag.StudyGroups = _context.StudyGroups.ToList();
			ViewBag.StudySubgroups = _context.StudySubgroups.ToList();
			ViewBag.FormEducations = _context.FormsEducation.ToList();
			ViewBag.Specialties = _context.Specialties.ToList();
			ViewBag.Faculties = _context.Faculties.ToList();
			ViewBag.Institutions = _context.Institutions.ToList();

            return View();
        }

        public IActionResult AddStudyGroup() => View();

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
		public async Task<IActionResult> CreateStudyGroup(StudyGroupAndSubgroupViewModel model)
		{
			if (ModelState.IsValid)
			{
				StudyGroup studyGroup = await _context.StudyGroups.FirstOrDefaultAsync(g => 
					(g.Name == model.StudyGroupViewModel.Name) && (g.SpecialtyId == model.StudyGroupViewModel.SpecialtyId)
				);
				if (studyGroup == null)
				{
					studyGroup = new StudyGroup {
						Name			= model.StudyGroupViewModel.Name,
						Code			= model.StudyGroupViewModel.Code,
						DateStart		= Convert.ToDateTime(model.StudyGroupViewModel.DateStart),
						DateEnd			= Convert.ToDateTime(model.StudyGroupViewModel.DateEnd),
						FormEducationId = model.StudyGroupViewModel.FormEducationId,
						SpecialtyId		= model.StudyGroupViewModel.SpecialtyId,
					};

					_context.StudyGroups.Add(studyGroup);
					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Некорретные данные");
			}
			else
				ModelState.AddModelError("", "Некорретные данные");

			return RedirectToAction("AddStudyGroup", "StudyGroup");
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStudySubgroup(StudyGroupAndSubgroupViewModel model)
		{
			if (ModelState.IsValid)
			{
				StudySubgroup studySubgroup = await _context.StudySubgroups.FirstOrDefaultAsync(s => 
					(s.StudyGroupId == model.StudySubgroupViewModel.StudyGroupId) && (s.Name == model.StudySubgroupViewModel.Name)
				);
				if (studySubgroup == null)
				{
					studySubgroup = new StudySubgroup {
						Name			= model.StudySubgroupViewModel.Name,
						Code			= model.StudySubgroupViewModel.Code,
						StudyGroupId	= model.StudySubgroupViewModel.StudyGroupId,
					};

					await _context.StudySubgroups.AddAsync(studySubgroup);
					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Некорретные данные");
			}
			else
				ModelState.AddModelError("", "Некорретные данные");
			return RedirectToAction("AddStudyGroup", "StudyGroup");
		}
	}
}
