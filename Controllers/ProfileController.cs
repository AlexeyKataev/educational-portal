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
    public class ProfileController : Controller
    {
		private UserContext db;
		private readonly ILogger<HomeController> _logger;

        public ProfileController(ILogger<HomeController> logger, UserContext context)
        {			
			db = context;
            _logger = logger;
        }

		[Authorize]
        public IActionResult MyProfile()
        {
            return View();
        }
	}
}
