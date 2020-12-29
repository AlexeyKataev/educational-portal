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
    public class WorkController : Controller
    {
		private UserContext db;
		private readonly ILogger<HomeController> _logger;

        public WorkController(ILogger<HomeController> logger, UserContext context)
        {			
			db = context;
            _logger = logger;
        }

		[Authorize]
        public IActionResult MyWorks()
        {
            return View();
        }
		
		[Authorize]
        public IActionResult InProgress()
        {
            return View();
        }
				
		[Authorize]
        public IActionResult CompletedWork()
        {
            return View();
        }
				
		[Authorize]
        public IActionResult EditsRequired()
        {
            return View();
        }

		[Authorize]
        public IActionResult AddWork()
        {
            return View();
        }

		[Authorize]
        public IActionResult Notices()
        {
            return View();
        }
	}
}
