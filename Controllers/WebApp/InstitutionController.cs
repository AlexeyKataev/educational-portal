using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.WebApp.Institution;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Controllers.WebApp
{
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class InstitutionController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<InstitutionController> _logger;

        public InstitutionController(ILogger<InstitutionController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult Institutions()
        {
			List<Institution> institutions = _context.Institutions.ToList();

			ViewBag.allInstitutions = institutions;

            return View();
        }

        public IActionResult AddInstitution() => View();

		[HttpGet]
		public IActionResult EditInstitution(long institutionId)
		{
			ViewBag.institution = _context.Institutions.FirstOrDefault(i => i.Id == institutionId);

			return View();
		}	

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesInstitution(EditInstitutionViewModel model)
		{
			if (ModelState.IsValid)
			{
				Institution institution = await _context.Institutions.FirstOrDefaultAsync(i => i.Id == model.Id);
				Institution rowCheck = await _context.Institutions.FirstOrDefaultAsync(i => i.Name == model.Name && i.Id != model.Id);

				if ((institution.Name == model.Name && institution.Id == model.Id) || rowCheck == null)
				{
					institution.Name 					= model.Name;
					institution.AddressId	 			= model.AddressId;
					institution.ContactInformationId 	= model.ContactInformationId;

					await _context.SaveChangesAsync();
			
					return RedirectToAction("Institutions", "Institution");
				}
				else ModelState.AddModelError("", "Данное учебное заведение уже существует");
			}
			else ModelState.AddModelError("", "Некорретные данные");

			return Redirect(Request.Headers["Referer"].ToString());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateInstitution(InstitutionViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				Institution institution = await _context.Institutions.FirstOrDefaultAsync(i => (i.Name == viewModel.Name));
				if (institution == null)
				{
					institution = new Institution {
						Name					= viewModel.Name,
						AddressId				= viewModel.AddressId,
						ContactInformationId 	= viewModel.ContactInformationId,
					};

					await _context.Institutions.AddAsync(institution);
					await _context.SaveChangesAsync();
					
					return RedirectToAction("AddInstitution", "Institution");
				}
				else ModelState.AddModelError("", "Учебное заведение с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			return View("AddInstitution", viewModel);
		}
	}
}
