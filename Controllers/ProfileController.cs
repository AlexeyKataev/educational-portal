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
    public class ProfileController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public ProfileController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }
		

		[Authorize]
        public IActionResult MyProfile()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		/* [Authorize(Roles="admin")] */
		public async Task<IActionResult> ChangeMe(ChangeMeModel model)
		{
			User user = await _context.Users.FirstOrDefaultAsync(u => (u.Login == (string)User.Identity.Name));

			return View(model);
		}
	}
}
