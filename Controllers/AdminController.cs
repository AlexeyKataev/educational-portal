using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Controllers
{
	[Authorize(Roles="admin, systemAdmin")]
    public class AdminController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger, ApplicationContext context)
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

				if (tmpUsers.Count != 0)
					users.Add(tmpUsers);
				else 
					roles.RemoveAt(roles.IndexOf(role));
			}

			ViewBag.allUsers = users;
			ViewBag.allRoles = roles;

            return View();
        }

		[HttpGet]
		[Authorize(Roles="admin")]
		public IActionResult EditUser(int userId)
		{
			User userEdt = _context.Users.FirstOrDefault(u => (u.Id == userId));
			
			List<Role> roles = _context.Roles.ToList();

			if (userEdt.DateOfBirth == new DateTime(0001, 1, 1, 1, 1, 1))
				userEdt.DateOfBirth = new DateTime(2001, 1, 1, 0, 0, 0).ToString("yyyy-MM-dd");
			else
				userEdt.DateOfBirth = userEdt.DateOfBirth.ToString("yyyy-MM-dd");

			ViewBag.user = userEdt;
			ViewBag.roles = roles;

			return View();
		}
		

		[Authorize(Roles="admin")]
		public async Task<IActionResult> ApplyChangesUser(string userId)
		{
			// Добавить ViewModel епта
			User userEdt = await _context.Users.FirstOrDefaultAsync(u => (u.Login == User.Identity.Name));

			return View();
		}
    }
}