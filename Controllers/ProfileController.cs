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
    public class ProfileController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public ProfileController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }
		
		[HttpGet]
        public IActionResult MyProfile()
        {
			User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));

			ViewData["FirstName"] = user.FirstName;
			ViewData["SecondName"] = user.SecondName;
			ViewData["MiddleName"] = user.MiddleName;
			ViewData["Email"] = user.Email;
			if (user.DateOfBirth == new DateTime(0001, 1, 1, 1, 1, 1))
				ViewData["DateOfBirth"] = new DateTime(2001, 1, 1, 0, 0, 0).ToString("yyyy-MM-dd");
			else
				ViewData["DateOfBirth"] = user.DateOfBirth.ToString("yyyy-MM-dd");

			return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> MyProfile(ProfileModel model)
		{
			if (ModelState.IsValid)
			{
				User userEdt = await _context.Users.FirstOrDefaultAsync(u => (u.Login == User.Identity.Name));

				User userEmailCheck = await _context.Users.FirstOrDefaultAsync(u => (u.Email == model.Email));
				string email = model.Email;

				// Проверка на занятость запрашиваемого адреса электронной почты
				if (userEmailCheck != null && userEdt.Email != email)
					email = null;

				if (userEdt != null)
				{
					// Изменение записи об учётной записи в базе данных
					User userUpd = new User { 
						FirstName	= model.FirstName,
						SecondName	= model.SecondName,
						MiddleName	= model.MiddleName,
						DateOfBirth = Convert.ToDateTime(model.DateOfBirth),
						DateAdded	= DateTime.Now,
						Email 		= email, 
						Login		= userEdt.Login,
						Password	= userEdt.Password,
					};

					userUpd.Id = userEdt.Id;
					userUpd.RoleId = userEdt.RoleId;

					_context.Entry(userEdt).CurrentValues.SetValues(userUpd);
					_context.SaveChanges();

					return RedirectToAction("MyProfile", "Profile");
				}
				else
				{
					ViewData["Email"] = ModelState.Values;
					ModelState.AddModelError("", "Произошла ошибка");
				}
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			//return View(model);
			return RedirectToAction("MyProfile", "Profile");
		}
	}
}
