using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
/*using CustomIdentityApp.Models;
using CustomIdentityApp.ViewModels; */

namespace Dotnet.Controllers
{
	[Authorize(Roles="admin")]
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

			// Получение всех существующих в системе ролей в виде списка
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
    }
}