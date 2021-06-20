using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Dotnet.Models;
using Dotnet.ViewModels.WebApp.Account;
using Dotnet.ViewModels.WebApp.Profile;

namespace Dotnet.Controllers.WebApp
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

				User userEdit = await _context.Users.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);

				User userEmailCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == profileViewModel.Email);
				string email = profileViewModel.Email;

				if (userEmailCheck != null && userEdit.Email != email) email = null;

				if (userEdit != null)
				{					
					userEdit.FirstName		= profileViewModel.FirstName;
					userEdit.SecondName		= profileViewModel.SecondName;
					userEdit.MiddleName		= profileViewModel.MiddleName;
					userEdit.DateOfBirth 	= Convert.ToDateTime(profileViewModel.DateOfBirth);
					userEdit.Email 			= email;

					_context.SaveChanges();
				}
				else ModelState.AddModelError("", "Произошла ошибка");
			}
			else ModelState.AddModelError("", "Некорректные данные");

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
