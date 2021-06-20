using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.WebApp.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Controllers.WebApp
{
	[Authorize(Roles="admin, systemAdmin")]
    public class UserController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult Users()
        {
			List<List<User>> users = new List<List<User>>();

			List<Role> roles = _context.Roles.ToList();

			foreach (Role role in roles.ToList())
			{
				List<User> tmpUsers = _context.Users.Where(u => u.RoleId == (role.Id)).ToList();

				if (tmpUsers.Count != 0) users.Add(tmpUsers);
				else roles.RemoveAt(roles.IndexOf(role));
			}

			ViewBag.allUsers = users;
			ViewBag.allRoles = roles;

            return View();
        }

		[HttpGet]
		[Authorize(Roles="admin")]
		public IActionResult EditUser(int userId)
		{
			User user = _context.Users.FirstOrDefault(u => (u.Id == userId));
			
			List<Role> roles = _context.Roles.ToList();

			ViewData["id"]			= user.Id;
			ViewData["RoleId"]		= user.RoleId;
			ViewData["FirstName"] 	= user.FirstName;
			ViewData["SecondName"] 	= user.SecondName;
			ViewData["MiddleName"] 	= user.MiddleName;
			ViewData["Email"] 		= user.Email;

			if (user.DateOfBirth == new DateTime(0001, 1, 1, 1, 1, 1)) ViewData["DateOfBirth"] = new DateTime(2001, 1, 1, 0, 0, 0).ToString("yyyy-MM-dd");
			else ViewData["DateOfBirth"] = user.DateOfBirth.ToString("yyyy-MM-dd");

			ViewBag.roles = roles;

			return View();
		}
		
		[HttpPost]
		[Authorize(Roles="admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesUser(EditUserViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				User userEdit = await _context.Users.FirstOrDefaultAsync(u => (u.Id == viewModel.Id));

				User userEmailCheck = await _context.Users.FirstOrDefaultAsync(u => (u.Email == viewModel.Email));
				string email = viewModel.Email;

				if (userEmailCheck != null && userEdit.Email != email) email = null;

				User meCheck = await _context.Users.FirstOrDefaultAsync(u => (u.Login == User.Identity.Name));
				int someoneId = viewModel.Id;

				if (meCheck.Id != someoneId)
				{ 
					userEdit.FirstName		= viewModel.FirstName;
					userEdit.SecondName		= viewModel.SecondName;
					userEdit.MiddleName		= viewModel.MiddleName;
					userEdit.DateOfBirth	= Convert.ToDateTime(viewModel.DateOfBirth);
					userEdit.Email 			= email;
					userEdit.RoleId			= viewModel.RoleId;

					await _context.SaveChangesAsync();
				}
				else ModelState.AddModelError("", "Произошла ошибка");
			}
			else ModelState.AddModelError("", "Некорректные данные");
			
			return RedirectToAction("Users", "User");
		}
    }
}