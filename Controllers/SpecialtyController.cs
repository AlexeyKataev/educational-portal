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

		private void SpecialtyToView(bool isUnCreate = false)
		{
			if (isUnCreate == true) ViewBag.allSpecialties = _context.Specialties.ToList();
			ViewBag.allFaculties = _context.Faculties.ToList();
			ViewBag.allInstitutions = _context.Institutions.ToList();
		}

		public IActionResult AddSpecialty()
		{
			SpecialtyToView();
			return View();
		}

		public IActionResult Specialties()
		{
			SpecialtyToView(true);
			return View();
		}

		private void EditableSpecialtyToView(int specialtyId)
		{
			Specialty specialty = _context.Specialties.FirstOrDefault(s => s.Id == specialtyId);

			List<Faculty> faculties = _context.Faculties.ToList();
			List<Institution> institutions = _context.Institutions.ToList();

			ViewData["Id"]			= specialty.Id;
			ViewData["Name"]		= specialty.Name;
			ViewData["Code"]		= specialty.Code;
			ViewData["FacultyId"]	= specialty.FacultyId;

			ViewBag.allFaculties = faculties;
			ViewBag.allInstitutions = institutions;
		}

		[HttpGet]
		public IActionResult EditSpecialty(int specialtyId)
		{
			EditableSpecialtyToView(specialtyId);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesSpecialty(EditSpecialtyViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Specialty specialtyEdit = await _context.Specialties.FirstOrDefaultAsync(s => s.Id == viewModel.Id);
				Specialty rowCheck = await _context.Specialties.FirstOrDefaultAsync(
					s => 
					(
						(s.Name == viewModel.Name) || 
						(s.Code == viewModel.Code)
					));

				if (rowCheck == null || rowCheck.FacultyId != viewModel.FacultyId)
				{
					specialtyEdit.Name		= viewModel.Name;
					specialtyEdit.Code		= viewModel.Code;
					specialtyEdit.FacultyId	= viewModel.FacultyId;

					await _context.SaveChangesAsync();

					return RedirectToAction("Specialties", "Specialty");
				}
				else ModelState.AddModelError("", "В данном факультете уже есть специальнось с таким названием и (или) кодом");
			}
			else ModelState.AddModelError("", "Некорретные данные");

			EditableSpecialtyToView(viewModel.Id);

			return View("EditSpecialty", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateSpecialty(SpecialtyViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Specialty specialty = await _context.Specialties.FirstOrDefaultAsync(s => 
					(s.Name == viewModel.Name) || 
					(s.Code == viewModel.Name)
				);

				if (specialty == null || specialty.FacultyId != viewModel.FacultyId)
				{
					specialty = new Specialty {
						Name		= viewModel.Name,
						Code		= viewModel.Code,
						FacultyId 	= viewModel.FacultyId,
					};

					await _context.Specialties.AddAsync(specialty);
					await _context.SaveChangesAsync();
			
					return RedirectToAction("Specialties", "Specialty");
				}
			}
			else ModelState.AddModelError("", "Некорректные данные");

			SpecialtyToView();

			return View("AddSpecialty", viewModel);
		}
	}
}