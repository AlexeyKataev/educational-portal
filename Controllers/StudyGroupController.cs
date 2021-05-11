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
	[Authorize(Roles="admin, systemAdmin, humanResources, student")]
    public class StudyGroupController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<StudyGroupController> _logger;

        public StudyGroupController(ILogger<StudyGroupController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

		[Authorize(Roles="admin, systemAdmin, humanResources")]
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

		[Authorize(Roles="admin, systemAdmin, humanResources")]
        public IActionResult AddStudyGroup() 
		{
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();
			ViewBag.formsEducation = _context.FormsEducation.ToList();
			return View();	
		} 

		[HttpGet]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public IActionResult EditStudyGroup(int groupId)
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public IActionResult EditStudySubgroup(int subGroupId)
		{
			return View();
		}		

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public async Task<IActionResult> ApplyChangesStudyGroup() // ПЕРЕДАТЬ МОДЕЛЬ
		{
			return null;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public async Task<IActionResult> ApplyChangesStudySubgroup() // ПЕРЕДАТЬ МОДЕЛЬ
		{
			return null;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
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
		[Authorize(Roles="admin, systemAdmin, humanResources")]
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

		[Authorize(Roles="student")]
		public IActionResult MyStudyGroup()
		{
			User me = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			Student aboutMe = _context.Students.AsNoTracking().FirstOrDefault(s => (s.UserId == me.Id));
			
			StudySubgroup mySubgroup = _context.StudySubgroups.AsNoTracking().FirstOrDefault(x => x.Id == aboutMe.StudySubgroupId);
			StudyGroup myGroup = _context.StudyGroups.AsNoTracking().FirstOrDefault(x => x.Id == mySubgroup.StudyGroupId);

			List<StudySubgroup> mySubgroups = _context.StudySubgroups.AsNoTracking().Where(x => x.StudyGroupId == myGroup.Id).ToList();

			List<Student> myClassmates_Students = new List<Student>();
			List<User> myClassmates_Users = new List<User>();

			foreach (var subgroup in mySubgroups)
			{
				List<Student> temp = _context.Students.Where(x => x.StudySubgroupId == subgroup.Id).ToList();
				myClassmates_Students.Concat(temp.ToList()).GetEnumerator();
			}

			foreach (var classmate in myClassmates_Students)
				myClassmates_Users.Add(_context.Users.FirstOrDefault(x => x.Id == classmate.UserId));

			ViewBag.mySubgroups = mySubgroups;
			ViewBag.myClassmates_Users = myClassmates_Users.OrderBy(x => x.SecondName);
			ViewBag.myClassmates_Students = myClassmates_Students.ToList();
		
			return View();
		}
	}
}
