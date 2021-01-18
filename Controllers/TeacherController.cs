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
	[Authorize]
    public class TeacherController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public TeacherController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

		[Authorize(Roles="admin, systemAdmin, humanResources")]
        public IActionResult AddTeacher()
        {
            return View();
        }
		
		[Authorize(Roles="admin, systemAdmin, humanResources")]
        public IActionResult Teachers()
        {
			List<List<User>> users = new List<List<User>>();

			List<Teacher> teachers = _context.Teachers.ToList();

			foreach (Teacher teacher in teachers.ToList())
			{
				List<User> tmpUsers = _context.Users.Where(u => u.Id == (teacher.UserId)).ToList();

				if (tmpUsers.Count != 0)
					users.Add(tmpUsers);
			}
			
			ViewBag.allUsers = users;
			ViewBag.allTeachers = teachers;

            return View();
        }

		[HttpPost]
		[Authorize(Roles="admin, systemAdmin, humanResources")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTeacher(Teacher model)
		{
            if (ModelState.IsValid)
            {
                Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(t => (t.UserId == model.UserId));
                if (teacher == null)
                {
                    // Добавление записи об учётной записи в базу данных
                    teacher = new Teacher { 
						UserId			= model.UserId,
						Post			= model.Post,
						Specialization	= model.Specialization,
					};
 
                    _context.Teachers.Add(teacher);
                    await _context.SaveChangesAsync();
 				
                    return RedirectToAction("AddTeacher", "Teacher");
                }
                else
                    ModelState.AddModelError("", "Некорректные данные");
            }
            return View(model);
		}
	}
}
