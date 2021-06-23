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
using Dotnet.ViewModels.WebApp.StudyGroup;

namespace Dotnet.Controllers.WebApp
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

		private void StudyGroupsToView(bool isUnCreate = false)
		{
			if (isUnCreate == true)
			{
				ViewBag.studySubgroups = _context.StudySubgroups.ToList();
				ViewBag.studyGroups = _context.StudyGroups.ToList();
			}

			ViewBag.formsEducation = _context.FormsEducation.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();
		}

		[Authorize(Roles="admin, systemAdmin, humanResources")]
        public IActionResult StudyGroups()
        {
			StudyGroupsToView(true);
            return View();
        }

		[Authorize(Roles="admin, systemAdmin, humanResources")]
        public IActionResult AddStudyGroup() 
		{
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			StudyGroupsToView();
			return View();	
		} 

		private void EditableStudyGroupToView(ulong studyGroupId)
		{
			StudyGroupsToView(true);
			ViewBag.studyGroup = _context.StudyGroups.FirstOrDefault(s => s.Id == studyGroupId);
		}

		private void EditableStudySubgroupToView(ulong studySubgroupId)
		{
			StudyGroupsToView(true);
			StudySubgroup studySubgroup = _context.StudySubgroups.FirstOrDefault(s => s.Id == studySubgroupId);
			ViewBag.studySubgroup = studySubgroup;
			ViewBag.StudyGroup = _context.StudyGroups.FirstOrDefault(s => s.Id == studySubgroup.StudyGroupId);
		}

		[HttpGet]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public IActionResult EditStudyGroup(ulong studyGroupId)
		{
			EditableStudyGroupToView(studyGroupId);
			return View();
		}

		[HttpGet]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public IActionResult EditStudySubgroup(ulong studySubgroupId)
		{
			EditableStudySubgroupToView(studySubgroupId);
			return View();
		}		

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public async Task<IActionResult> ApplyChangesStudyGroup(EditStudyGroupViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				StudyGroup studyGroupEdit = await _context.StudyGroups.FirstOrDefaultAsync(s => s.Id == viewModel.Id);
				StudyGroup rowCheck = await _context.StudyGroups.FirstOrDefaultAsync(
					s =>
					(
						((s.Name == viewModel.Name) ||
						(s.Code == viewModel.Code)) & (s.Id != viewModel.Id)
					)
				);

				if (rowCheck == null || (rowCheck.Id == viewModel.Id))
				{
					studyGroupEdit.Name				= viewModel.Name;
					studyGroupEdit.Code				= viewModel.Code;
					studyGroupEdit.SpecialtyId		= viewModel.SpecialtyId;
					studyGroupEdit.FormEducationId 	= viewModel.FormEducationId;
					studyGroupEdit.DateStart		= viewModel.DateStart;
					studyGroupEdit.DateEnd			= viewModel.DateEnd;

					await _context.SaveChangesAsync();

					return RedirectToAction("StudyGroups", "StudyGroup");
				}
				else ModelState.AddModelError("", "В данной специальности уже есть учебная группа с такими данными");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			EditableStudyGroupToView(viewModel.Id);

			return View("EditStudyGroup", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public async Task<IActionResult> ApplyChangesStudySubgroup(EditStudySubgroupViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				StudySubgroup studySubgroupEdit = await _context.StudySubgroups.FirstOrDefaultAsync(s => s.Id == viewModel.Id);
				StudySubgroup rowCheck = await _context.StudySubgroups.FirstOrDefaultAsync(
					s =>
					(
						((s.Name == viewModel.Name) || 
						(s.Code == viewModel.Code)) & (s.Id != viewModel.Id)
					)
				);

				if (rowCheck == null || rowCheck.Id == viewModel.Id)
				{
					studySubgroupEdit.Name			= viewModel.Name;
					studySubgroupEdit.Code			= viewModel.Code;
					studySubgroupEdit.StudyGroupId	= viewModel.StudyGroupId;

					await _context.SaveChangesAsync();
					
					return RedirectToAction("StudyGroups", "StudyGroup");
				}
				else ModelState.AddModelError("", "В данной учебной группе уже есть учебная подгруппа с такими данными");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			EditableStudySubgroupToView(viewModel.Id);

			return View("EditStudySubgroup", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public async Task<IActionResult> CreateStudyGroup(StudyGroupAndSubgroupViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				StudyGroup studyGroup = await _context.StudyGroups.FirstOrDefaultAsync(g => 
					(g.Name == viewModel.StudyGroupViewModel.Name) ||
					(g.Code == viewModel.StudyGroupViewModel.Code) &&
					(g.SpecialtyId == viewModel.StudyGroupViewModel.SpecialtyId)
				);

				if (studyGroup == null || studyGroup.SpecialtyId != viewModel.StudyGroupViewModel.SpecialtyId)
				{
					studyGroup = new StudyGroup {
						Name			= viewModel.StudyGroupViewModel.Name,
						Code			= viewModel.StudyGroupViewModel.Code,
						DateStart		= Convert.ToDateTime(viewModel.StudyGroupViewModel.DateStart),
						DateEnd			= Convert.ToDateTime(viewModel.StudyGroupViewModel.DateEnd),
						FormEducationId = viewModel.StudyGroupViewModel.FormEducationId,
						SpecialtyId		= viewModel.StudyGroupViewModel.SpecialtyId,
					};

					await _context.StudyGroups.AddAsync(studyGroup);
					await _context.SaveChangesAsync();
					
					return RedirectToAction("StudyGroups", "StudyGroup");
				}
				else ModelState.AddModelError("", "Учебная группа с данными названием, кодом и (или) специальностью уже существует");
			}
			else ModelState.AddModelError("", "Некорретные данные");

			StudyGroupsToView(true);

			return View("AddStudyGroup", viewModel);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		public async Task<IActionResult> CreateStudySubgroup(StudyGroupAndSubgroupViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				StudySubgroup studySubgroup = await _context.StudySubgroups.FirstOrDefaultAsync(s => 
					(s.StudyGroupId == viewModel.StudySubgroupViewModel.StudyGroupId) && 
					(s.Name == viewModel.StudySubgroupViewModel.Name)
				);

				if (studySubgroup == null)
				{
					studySubgroup = new StudySubgroup {
						Name			= viewModel.StudySubgroupViewModel.Name,
						Code			= viewModel.StudySubgroupViewModel.Code,
						StudyGroupId	= viewModel.StudySubgroupViewModel.StudyGroupId,
					};

					await _context.StudySubgroups.AddAsync(studySubgroup);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddStudyGroup", "StudyGroup");
				}
				else ModelState.AddModelError("", "Некорретные данные");
			}
			else ModelState.AddModelError("", "Некорретные данные");

			StudyGroupsToView(true);

			return View("AddStudyGroup", viewModel);
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
				myClassmates_Students.AddRange(_context.Students.AsNoTracking().Where(x => x.StudySubgroupId == subgroup.Id).ToList());

			foreach (var classmate in myClassmates_Students)
				myClassmates_Users.Add(_context.Users.AsNoTracking().FirstOrDefault(x => x.Id == classmate.UserId));

			ViewBag.mySubgroups = mySubgroups;
			ViewBag.myClassmates_Users = myClassmates_Users.OrderBy(x => x.SecondName).ToList();
			ViewBag.myClassmates_Students = myClassmates_Students.ToList();
		
			return View();
		}
	}
}
