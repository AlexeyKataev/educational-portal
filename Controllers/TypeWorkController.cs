using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.TypeWork;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace Dotnet.Controllers
{
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

		private void TypesWorksToView()
		{
			ViewData["allTypeWorks"] = _context.TypesWorks.ToList();
		}

        public IActionResult TypesWorks()
        {
			TypesWorksToView();
            return View();
        }

        public IActionResult AddTypeWorks() => View();

		private void EditableTypeWorks(int typeWorkId)
		{
			ViewBag.EditRow = _context.TypesWorks.FirstOrDefault(t => t.Id == typeWorkId);;
		}

		[HttpGet]
		public IActionResult EditTypeWorks(int typeWorkId)
		{
			EditableTypeWorks(typeWorkId);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesTypeWorks(EditTypeWorkViewModel viewModel)
		{
			if (ModelState.IsValid) 
			{
				TypeWorks typeWorksEdit = await _context.TypesWorks.FirstOrDefaultAsync(t => t.Id == viewModel.Id);
				TypeWorks rowCheck = await _context.TypesWorks.FirstOrDefaultAsync(t => t.Name == viewModel.Name);

				if (rowCheck == null || rowCheck.Id == viewModel.Id)
				{
					typeWorksEdit.Name = viewModel.Name;

					await _context.SaveChangesAsync();

					return RedirectToAction("TypesWorks", "TypeWork");
				}
				else ModelState.AddModelError("", "Вид работы с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорректные данные");

			EditableTypeWorks(viewModel.Id);

			return View("EditTypeWorks", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTypeWorks(TypeWorkViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				TypeWorks typeWorks = await _context.TypesWorks.FirstOrDefaultAsync(t => t.Name == viewModel.Name);

				if (typeWorks == null)
				{
					typeWorks = new TypeWorks { Name = viewModel.Name };

					await _context.TypesWorks.AddAsync(typeWorks);
					await _context.SaveChangesAsync();

					return RedirectToAction("AddTypeWorks", "TypeWork");
				}
				else ModelState.AddModelError("", "Вид работы с данным названием уже существует");
			}
			else ModelState.AddModelError("", "Некорретные данные");

			TypesWorksToView();

			return View("AddTypeWorks", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteTypeWorks(DeleteTypeWorkViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				TypeWorks typeWorks = await _context.TypesWorks.FirstOrDefaultAsync(t => t.Id == viewModel.Id);
				Work rowCheck = await _context.Works.FirstOrDefaultAsync(x => x.TypeWorksId == typeWorks.Id);

				if (typeWorks != null && rowCheck == null)
				{
					_context.Remove(typeWorks);
					await _context.SaveChangesAsync();

					return RedirectToAction("TypesWorks", "TypeWork");
				}
				else ModelState.AddModelError("", "Данная запись уже связана с другой записью");
			}
			else ModelState.AddModelError("", "Некорректный идентификатор");

			EditableTypeWorks(viewModel.Id);

			return View("EditTypeWorks");
		}
	}
}
