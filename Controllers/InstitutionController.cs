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
    public class InstitutionController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public InstitutionController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult Institutions()
        {
            return View();
        }

        public IActionResult AddInstitution()
        {
            return View();
        }

		[HttpGet]
		public IActionResult EditInstitution(int groupId)
		{
			return View();
		}	

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesInstitution() // ПЕРЕДАТЬ МОДЕЛЬ
		{
			return null;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateInstitution(Institution model)
		{
			if (ModelState.IsValid)
			{
				Institution institution = await _context.Institutions.FirstOrDefaultAsync(i => (i.Name == model.Name));
				if (institution == null)
				{
					institution = new Institution {
						Name					= model.Name,
						AddressId				= model.AddressId,
						ContactInformationId 	= model.ContactInformationId,
					};

					_context.Institutions.Add(institution);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddInstitution", "Institution");
				}
				else
					ModelState.AddModelError("", "Некорретные данные");
			}
			return View(model);
		}
	}
}
