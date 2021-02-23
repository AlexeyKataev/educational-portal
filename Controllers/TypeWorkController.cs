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
    public class TypeWorkController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<TypeWorkController> _logger;

        public TypeWorkController(ILogger<TypeWorkController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult TypesWorks()
        {
			List<TypeWorks> typeWorks = _context.TypesWorks.ToList();

			ViewData["allTypeWorks"] = typeWorks;

            return View();
        }

        public IActionResult AddTypeWorks()
        {
            return View();
        }

		[HttpGet]
		public IActionResult EditTypeWorks(int typeWorkId)
		{
			TypeWorks typeWorks = _context.TypesWorks.FirstOrDefault(t => (t.Id == typeWorkId));

			ViewBag.EditRow = typeWorks;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesTypeWorks(EditTypeWorkViewModel model)
		{
			if (ModelState.IsValid) 
			{
				TypeWorks typeWorksEdt = await _context.TypesWorks.FirstOrDefaultAsync(
					t => (t.Id == model.Id)
				);

				TypeWorks rowCheck = await _context.TypesWorks.FirstOrDefaultAsync(
					t => (t.Name == model.Name)
				);

				if (rowCheck == null)
				{
					typeWorksEdt.Name = model.Name;

					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Некорректные данные");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("TypesWorks", "TypeWork");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTypeWorks(TypeWorkViewModel model)
		{
			if (ModelState.IsValid)
			{
				TypeWorks typeWorks = await _context.TypesWorks.FirstOrDefaultAsync(t => (t.Name == model.Name));
				if (typeWorks == null)
				{
					typeWorks = new TypeWorks {
						Name = model.Name,
					};

					_context.TypesWorks.Add(typeWorks);
					await _context.SaveChangesAsync();
				}
				else
					ModelState.AddModelError("", "Некорретные данные");
			}
			else
				ModelState.AddModelError("", "Некорретные данные");

			return RedirectToAction("AddTypeWorks", "TypeWork");
		}
	}
}
