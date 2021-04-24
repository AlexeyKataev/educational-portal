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
using Dotnet.ViewModels.Teacher;
using Dotnet.Models.Study;

namespace Dotnet.Controllers
{
	[Authorize]
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class TeacherController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<TeacherController> _logger;

        public TeacherController(ILogger<TeacherController> logger, ApplicationContext context)
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

				if (teacherTmp != null && teacherTmp.Id != teacher.Id)
					users.RemoveAt(users.IndexOf(user));
			}

			// Получить список всех учебных предметов
			List<Subject> subjects = _context.Subjects.ToList();

			ViewData["Id"]				= teacher.Id;
			ViewData["UserId"]			= teacher.UserId;
			ViewData["Post"]			= teacher.Post;
			ViewData["Specialization"]	= teacher.Specialization;

			ViewBag.allUsers = users;
			ViewBag.allSubjects = subjects;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesTeacher(EditTeacherViewModel model)
		{
			if (ModelState.IsValid)
			{
				Teacher teacherEdt = await _context.Teachers.FirstOrDefaultAsync(u => (u.Id == model.Id));

				// Проверка, есть ли данный пользователь в таблице преподавателей
				Teacher teacherCheck = await _context.Teachers.FirstOrDefaultAsync(u => (u.UserId == model.UserId));

				if (teacherCheck == null || teacherCheck.UserId == teacherEdt.UserId)
				{
					Teacher teacherUpd = new Teacher { 
						Id 				= teacherEdt.Id,
						UserId 			= model.UserId,
						Post 			= model.Post,
						Specialization 	= model.Specialization,
					};

					_context.Entry(teacherEdt).CurrentValues.SetValues(teacherUpd);
					_context.SaveChanges();
				}
				else
				ModelState.AddModelError("", "Некорректные данные");
			}
			else
				ModelState.AddModelError("", "Некорректные данные");
			
			return RedirectToAction("Teachers", "Teacher");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesSubjectsForTeacher(SubjectsForTeacherViewModel model) 
		{
			if (ModelState.IsValid)
			{
				Teacher teacherCheck = await _context.Teachers.FirstOrDefaultAsync(t => (t.Id == model.Id));
				
				if (teacherCheck != null)
				{
					foreach (var subjectId in model.SubjectsId)
					{
						var subjectTeacherCheck = _context.SubjectTeacher.Where(r => (r.TeacherId == model.Id) && (r.SubjectId == subjectId)).ToList();
						
						// Если выбранный преподаватель не ведёт данный предмет - добавить
						if (subjectTeacherCheck.Count == 0)
						{
							SubjectTeacher subjectTeacher = new SubjectTeacher {
								TeacherId	= model.Id,
								SubjectId	= subjectId,
							};

							await _context.SubjectTeacher.AddAsync(subjectTeacher);
							await _context.SaveChangesAsync();
						}
					}
				}
				else ModelState.AddModelError("", "Некорректные данные");
			}
			return RedirectToAction("Teachers", "Teacher");	
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
			else
				ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("AddTeacher", "Teacher");
		}
	}
}
