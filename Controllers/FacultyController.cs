using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.Faculty;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Controllers
{
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class FacultyController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<FacultyController> _logger;

        public FacultyController(ILogger<FacultyController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult Faculties()
        {			
			ViewBag.allInstitutions = _context.Institutions.ToList();
			ViewBag.allFaculties = _context.Faculties.ToList();

            return View();
        }

		public IActionResult AddFaculty(FacultyViewModel viewModel = null)
        {
			ViewBag.allInstitutions = _context.Institutions.ToList();

            return View(viewModel);
        }

		private void EditableFacultyToView(int facultyId)
		{
			Faculty faculty = _context.Faculties.FirstOrDefault(f => f.Id == facultyId);

			ViewData["Id"] 				= faculty.Id;
			ViewData["Name"] 			= faculty.Name;
			ViewData["Code"] 			= faculty.Code;
			ViewData["About"] 			= faculty.About;
			ViewData["InstitutionId"] 	= faculty.InstitutionId;

			ViewBag.allInstitutions = _context.Institutions.ToList();
		}

		[HttpGet]
		public IActionResult EditFaculty(int facultyId)
		{
			EditableFacultyToView(facultyId);
			return View();
		}	

		[HttpPost]
		[Authorize(Roles="admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesFaculty(EditFacultyViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Faculty facultyEdit = await _context.Faculties.FirstOrDefaultAsync(f => (f.Id == viewModel.Id));
				Faculty rowCheck = await _context.Faculties.FirstOrDefaultAsync(
					f => 
					(
						(f.Name == viewModel.Name) || 
						(f.Code == viewModel.Code) && 
						(f.InstitutionId == viewModel.InstitutionId) && 
						(f.Id != viewModel.Id)
					));

				if (rowCheck == null)
				{
					facultyEdit.Name 			= viewModel.Name;
					facultyEdit.Code 			= viewModel.Code;
					facultyEdit.InstitutionId 	= viewModel.InstitutionId;
					facultyEdit.About		 	= viewModel.About;

					await _context.SaveChangesAsync();

					return RedirectToAction("Faculties", "Faculty");
				}
				else ModelState.AddModelError("", "В данном учебном заведении уже есть факультет с таким названием и (или) кодом");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			EditableFacultyToView(viewModel.Id);

			return View("EditFaculty", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateFaculty(FacultyViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Faculty faculty = await _context.Faculties.FirstOrDefaultAsync(
					f => ( 
						((f.Name == viewModel.Name) || (f.Code == viewModel.Code)) && 
						(f.InstitutionId == viewModel.InstitutionId)
					));

				if (faculty == null)
				{
					faculty = new Faculty {
						Name			= viewModel.Name,
						Code			= viewModel.Code,
						About			= viewModel.About,
						InstitutionId	= viewModel.InstitutionId,
					};

					await _context.Faculties.AddAsync(faculty);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddFaculty", "Faculty");
				}
				else ModelState.AddModelError("", "Факультет с данными названием и (или) кодом уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			ViewBag.allInstitutions = _context.Institutions.ToList();

			return View("AddFaculty", viewModel);
		}
	}
}
