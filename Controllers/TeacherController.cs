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
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class TeacherController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<HomeController> _logger;

        public TeacherController(ILogger<HomeController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult AddTeacher()
        {
			List<User> users = _context.Users.Where(u => u.RoleId == 6).ToList();

			foreach (var user in users.ToList())
			{
				Teacher teacher = _context.Teachers.FirstOrDefault(u => (u.UserId == user.Id));

				if (teacher != null)
					users.RemoveAt(users.IndexOf(user));
			}

			ViewBag.allUsers = users;

            return View();
        }

        public IActionResult Teachers()
        {
			List<List<User>> users = new List<List<User>>();

			List<Teacher> teachers = _context.Teachers.ToList();

			foreach (Teacher teacher in teachers.ToList())
			{
				List<User> usersTmp = _context.Users.Where(u => u.Id == (teacher.UserId)).ToList();

				if (usersTmp.Count != 0)
					users.Add(usersTmp);
			}
			
			ViewBag.allUsers = users;
			ViewBag.allTeachers = teachers;

            return View();
        }

		[HttpGet]
		public IActionResult EditTeacher(int teacherId)
		{
			// Получить запрашиваемого на редактирование преподавателя
			Teacher teacher = _context.Teachers.FirstOrDefault(u => (u.UserId == teacherId));

			// Получить список всех зарегистрированных пользователей с ролью "Преподаватель"
			List<User> users = _context.Users.Where(u => u.RoleId == 6).ToList();

			// Проверить наличие пользователей в списке действующих преподавателей
			foreach (var user in users.ToList())
			{
				Teacher teacherTmp = _context.Teachers.FirstOrDefault(u => (u.UserId == user.Id));

				if (teacherTmp != null)
					users.RemoveAt(users.IndexOf(user));
			}

			ViewData["Id"]				= teacher.UserId;
			ViewData["Post"]			= teacher.Post;
			ViewData["Specialization"]	= teacher.Specialization;

			ViewBag.allUsers = users;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesUser(EditTeacherViewModel model)
		{
			if (ModelState.IsValid)
			{
				User teacherEdt = await _context.Users.FirstOrDefaultAsync(u => (u.Id == model.Id));

				// !!!!! ТУТ КАКОЕ-ТО ПОЛНОЕ ГОВНО, НЕ ПОНЯЛ ЧТО НАПИСАЛ. ПОЗЖЕ ПОСМОТРЕТЬ
				// ПОКА ПУСТЬ ТАК

				// Изменение записи об учётной записи в базе данных
				Teacher teacherUpd = new Teacher { 

				};

				_context.Entry(teacherEdt).CurrentValues.SetValues(teacherUpd);
				_context.SaveChanges();
				
				return RedirectToAction("Users", "Admin");				
			}
			else
				ModelState.AddModelError("", "Некорректные данные");
			
			return RedirectToAction("Users", "Admin");
		}

		[HttpPost]
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
