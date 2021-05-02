using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Controllers
{
    public class HomeController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

		[Authorize]
        public IActionResult Index()
        {
			User me = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			ViewBag.me = me;

			if (me.RoleId == 6)	
			{
				Teacher aboutMe = _context.Teachers.FirstOrDefault(x => (x.UserId == me.Id));
				ViewBag.aboutMe = $"{aboutMe.Specialization} • {aboutMe.Post}";
			}
			else ViewBag.aboutMe = $"none";
			
            return View();
        }

		[Authorize]
		public IActionResult Services()
		{
			return View();
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
	}
}
