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
	[Authorize(Roles="admin, systemAdmin")]
	public class SpecialtyController : Controller
	{
		private ApplicationContext _context;
		private readonly ILogger<SpecialtyController> _logger;

		public SpecialtyController(ILogger<SpecialtyController> logger, ApplicationContext context)
		{
			_context = context;
			_logger = logger;
		}

		public IActionResult AddSpecialty()
		{
			List<Faculty> faculties = _context.Faculties.ToList();
			List<Institution> institutions = _context.Institutions.ToList();

			ViewBag.allFaculties = faculties;
			ViewBag.allInstitutions = institutions;

			return View();
		}

		public IActionResult Specialties()
		{
			List<Specialty> specialties = _context.Specialties.ToList();
			List<Faculty> faculties = _context.Faculties.ToList();
			List<Institution> institutions = _context.Institutions.ToList();

			ViewBag.allSpecialties = specialties;
			ViewBag.allFaculties = faculties;
			ViewBag.allInstitutions = institutions;

			return View();
		}

		[HttpGet]
		public IActionResult EditSpecialty(int specialtyId)
		{
			Specialty specialty = _context.Specialties.FirstOrDefault(
				s => (s.Id == specialtyId)
			);

			List<Faculty> faculties = _context.Faculties.ToList();
			List<Institution> institutions = _context.Institutions.ToList();

			ViewData["Id"]			= specialty.Id;
			ViewData["Name"]		= specialty.Name;
			ViewData["Code"]		= specialty.Code;
			ViewData["FacultyId"]	= specialty.FacultyId;

			ViewBag.allFaculties = faculties;
			ViewBag.allInstitutions = institutions;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesSpecialty(EditSpecialtyViewModel model)
		{
			if (ModelState.IsValid)
			{
				Specialty specialtyEdt = await _context.Specialties.FirstOrDefaultAsync(
					s => (s.Id == model.Id)
				);

				// Проверка наличия специальности с такими же данными
				Specialty rowsCheck = await _context.Specialties.FirstOrDefaultAsync(
					s => 
					(
						(s.Name == model.Name || s.Code == model.Code) &&
						(s.FacultyId == model.FacultyId) &&
						(s.Id != model.Id)
					)
				);


				if (rowsCheck == null)
				{
					specialtyEdt.Name		= model.Name;
					specialtyEdt.Code		= model.Code;
					specialtyEdt.FacultyId	= model.FacultyId;

					_context.SaveChanges();
				}
				else
					ModelState.AddModelError("", "В указанном факультете уже есть специальнось с таким названием и (или) кодом");
			}
			else
				ModelState.AddModelError("", "Некорретные данные");

			return RedirectToAction("Specialties", "Specialty");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateSpecialty(Specialty model)
		{
			if (ModelState.IsValid)
			{
				Specialty specialty = await _context.Specialties.FirstOrDefaultAsync(s => 
					(s.Name == model.Name || s.Code == model.Name) &&
					(s.FacultyId == model.FacultyId)
				);
				if (specialty == null)
				{
					specialty = new Specialty {
						Name		= model.Name,
						Code		= model.Code,
						FacultyId 	= model.FacultyId,
					};

					_context.Specialties.Add(specialty);
					await _context.SaveChangesAsync();
				}
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("Specialties", "Specialty");
		}
	}
}