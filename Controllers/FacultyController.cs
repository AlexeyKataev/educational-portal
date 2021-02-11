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
    public class FacultyController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public FacultyController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult Faculties()
        {			
			List<Institution> institutions = _context.Institutions.ToList();
			List<Faculty> faculties = _context.Faculties.ToList();

			ViewBag.allInstitutions = institutions;
			ViewBag.allFaculties = faculties;

            return View();
        }

        public IActionResult AddFaculty()
        {
			List<Institution> institutions = _context.Institutions.ToList();

			ViewBag.allInstitutions = institutions;

            return View();
        }

		[HttpGet]
		public IActionResult EditFaculty(int facultyId)
		{
			Faculty faculty = _context.Faculties.FirstOrDefault(
				f => (f.Id == facultyId)
			);

			List<Institution> institutions = _context.Institutions.ToList();

			ViewData["Id"] 				= faculty.Id;
			ViewData["Name"] 			= faculty.Name;
			ViewData["Code"] 			= faculty.Code;
			ViewData["About"] 			= faculty.About;
			ViewData["InstitutionId"] 	= faculty.InstitutionId;

			ViewBag.allInstitutions = institutions;

			return View();
		}	

		[HttpPost]
		[Authorize(Roles="admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesFaculty(EditFacultyViewModel model)
		{
			if (ModelState.IsValid)
			{
				Faculty facultyEdt = await _context.Faculties.FirstOrDefaultAsync(
					f => (f.Id == model.Id)
				);

				// Проверка наличия факультета с данными названием и кодом в указанном учебном заведении
				Faculty nameAndCodeFacultyCheck = await _context.Faculties.FirstOrDefaultAsync(
						f => ((f.Name == model.Name || f.Code == model.Code) && 
						f.InstitutionId == model.InstitutionId && f.Id != model.Id
					)
				);

				if (nameAndCodeFacultyCheck == null)
				{
					facultyEdt.Name 			= model.Name;
					facultyEdt.Code 			= model.Code;
					facultyEdt.InstitutionId 	= model.InstitutionId;
					facultyEdt.About		 	= model.About;

					_context.SaveChanges();
				}
				else
					ModelState.AddModelError("", "В указанном учебном заведении уже есть факультет с таким названием и (или) кодом");
			}
			else 
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("Faculties", "Faculty");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateFaculty(Faculty model)
		{
			if (ModelState.IsValid)
			{
				Faculty faculty = await _context.Faculties.FirstOrDefaultAsync(
					f => ( 
						((f.Name == model.Name) || (f.Code == model.Code)) && 
						(f.InstitutionId == model.InstitutionId)
					)
				);
				if (faculty == null)
				{
					faculty = new Faculty {
						Name			= model.Name,
						Code			= model.Code,
						About			= model.About,
						InstitutionId	= model.InstitutionId,
					};

					_context.Faculties.Add(faculty);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddFaculty", "Faculty");
				}
				else
					ModelState.AddModelError("", "Некорректные данные");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("AddFormEducation", "FormEducation");
		}
	}
}
