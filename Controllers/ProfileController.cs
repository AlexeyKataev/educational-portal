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
using Dotnet.ViewModels.Profile;

namespace Dotnet.Controllers
{
	[Authorize]
    public class ProfileController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }
		
		[HttpGet]
        public IActionResult MyProfile()
        {
			User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));

			ViewData["FirstName"] 	= user.FirstName;
			ViewData["SecondName"] 	= user.SecondName;
			ViewData["MiddleName"] 	= user.MiddleName;
			ViewData["Email"] 		= user.Email;
			if (user.DateOfBirth == new DateTime(0001, 1, 1, 1, 1, 1))
				ViewData["DateOfBirth"] = new DateTime(2001, 1, 1, 0, 0, 0).ToString("yyyy-MM-dd");
			else
				ViewData["DateOfBirth"] = user.DateOfBirth.ToString("yyyy-MM-dd");

			return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> MyProfile(ProfilePageViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				ProfileViewModel profileViewModel = viewModel.ProfileViewModel;

				User userEdt = await _context.Users.FirstOrDefaultAsync(u => (u.Login == User.Identity.Name));

				User userEmailCheck = await _context.Users.FirstOrDefaultAsync(u => (u.Email == profileViewModel.Email));
				string email = profileViewModel.Email;

				// Проверка на занятость запрашиваемого адреса электронной почты
				if (userEmailCheck != null && userEdt.Email != email)
					email = null;

				if (userEdt != null)
				{
					// Изменение записи об учётной записи в базе данных
					User userUpd = new User { 						
						Id 			= userEdt.Id,
						FirstName	= profileViewModel.FirstName,
						SecondName	= profileViewModel.SecondName,
						MiddleName	= profileViewModel.MiddleName,
						DateOfBirth = Convert.ToDateTime(profileViewModel.DateOfBirth),
						DateAdded	= userEdt.DateAdded,
						Email 		= email, 
						Login		= userEdt.Login,
						Password	= userEdt.Password,
						RoleId		= userEdt.RoleId,
					};

					_context.Entry(userEdt).CurrentValues.SetValues(userUpd);
					_context.SaveChanges();
				}
				else
					ModelState.AddModelError("", "Произошла ошибка");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("MyProfile", "Profile");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangeMyPassword(ProfilePageViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				EditPasswordViewModel editPasswordViewModel = viewModel.EditPasswordViewModel;

				User userEdt = await _context.Users.FirstOrDefaultAsync(u => (u.Login == User.Identity.Name));

				if (editPasswordViewModel.Password == editPasswordViewModel.Password2)
				{
					userEdt.Password = editPasswordViewModel.Password;
					await _context.SaveChangesAsync();
				}
				else ModelState.AddModelError("", "Некорректные данные");

			}
			else ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("MyProfile", "Profile");
		}
	}
}
