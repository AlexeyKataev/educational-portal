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
	[Authorize(Roles="teacher")]
    public class WorkController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<WorkController> _logger;

        public WorkController(ILogger<WorkController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult AddTask()
        {
            return View();
        }
	}
}
