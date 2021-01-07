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
			// Списки с информацией о пользователях
			ViewBag.AllUsers = null;
			List<List<User>> allUsers = new List<List<User>>();

			// Получение всех существующих в системе ролей в виде списка
			List<Role> roles = _context.Roles.ToList();

			// Сборка массивов с выборками пользователей, упорядоченных по ролям
			foreach (Role role in roles)
			{
				// Получение всех пользователей для определённой роли
				var tmpAllUsers = _context.Users.Where(u => u.RoleId == (role.Id)).ToList();

				if (tmpAllUsers.Count != 0)
				{
					allUsers.Add(tmpAllUsers);
				}
			}

            return View();
        }
    }
}