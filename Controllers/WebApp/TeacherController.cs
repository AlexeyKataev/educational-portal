using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dotnet.ViewModels.WebApp.Teacher;
using Dotnet.Models.Study;
using Dotnet.Models;
using Dotnet.ViewModels;
using Dotnet.Enums.WebApp;

namespace Dotnet.Controllers.WebApp
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
			List<User> users = _context.Users.Where(u => u.UserRole == EnumRoles.Teacher).ToList();

			foreach (var user in users.ToList())
			{
				Teacher teacher = _context.Teachers.FirstOrDefault(u => u.UserId == user.Id);
				if (teacher != null) users.RemoveAt(users.IndexOf(user));
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
				List<User> usersTmp = _context.Users.Where(u => u.Id == teacher.UserId).ToList();
				if (usersTmp.Count != 0) users.Add(usersTmp);
			}
			
			ViewBag.allUsers = users;
			ViewBag.allTeachers = teachers;

            return View();
        }

		[HttpGet]
		public IActionResult EditTeacher(int userId, int teacherId)
		{
			Teacher teacher = _context.Teachers.FirstOrDefault(u => (u.Id == teacherId));

			List<User> users = _context.Users.Where(u => u.UserRole == EnumRoles.Teacher).ToList();

			foreach (var user in users.ToList())
			{
				Teacher teacherTmp = _context.Teachers.FirstOrDefault(u => (u.UserId == user.Id));
				if (teacherTmp != null && teacherTmp.Id != teacher.Id) users.RemoveAt(users.IndexOf(user));
			}

			List<Subject> subjects = _context.Subjects.ToList();
			List<SubjectTeacher> subjectTeacher = _context.SubjectTeacher.Where(t => t.TeacherId == teacherId).ToList();

			ViewData["Id"]				= teacher.Id;
			ViewData["UserId"]			= teacher.UserId;
			ViewData["Post"]			= teacher.Post;
			ViewData["Specialization"]	= teacher.Specialization;

			List<SubjectForTeacher> subjectsForTeacher = new List<SubjectForTeacher>();

			foreach (var subject in subjects)
			{
				if (subjectTeacher.Exists(id => id.SubjectId == (subject.Id))) subjectsForTeacher.Add(new SubjectForTeacher{ Id = subject.Id, Name = subject.Name, IsChecked = true });
				else subjectsForTeacher.Add(new SubjectForTeacher{ Id = subject.Id, Name = subject.Name, IsChecked = false });
			}

			ViewBag.allUsers = users;
			ViewBag.allSubjects = subjects;
			ViewBag.allSubjectsForTeacher = subjectsForTeacher;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesTeacher(EditTeacherViewModel model)
		{
			if (ModelState.IsValid)
			{
				Teacher teacherEdt = await _context.Teachers.FirstOrDefaultAsync(u => u.Id == model.Id);

				Teacher teacherCheck = await _context.Teachers.FirstOrDefaultAsync(u => u.Id == model.Id);

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
				else ModelState.AddModelError("", "Некорректные данные");
			}
			else ModelState.AddModelError("", "Некорректные данные");
			
			return RedirectToAction("Teachers", "Teacher");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ApplyChangesSubjectsForTeacher(SubjectsForTeacherViewModel model) 
		{
			if (ModelState.IsValid)
			{
				Teacher teacherCheck = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == model.Id);
				
				if (teacherCheck != null && model.SubjectsId != null)
				{
					foreach (var subjectId in model.SubjectsId)
					{
						var subjectTeacherCheck = _context.SubjectTeacher.Where(r => (r.TeacherId == model.Id) && (r.SubjectId == subjectId)).ToList();
						
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

					List<SubjectTeacher> allSubjectsForTeacher = _context.SubjectTeacher.Where(s => s.TeacherId == model.Id).ToList();

					foreach (var subject in allSubjectsForTeacher) 
					{
						if ((model.SubjectsId).Contains(subject.SubjectId) == false) 
						{
							SubjectTeacher deletedRow = await _context.SubjectTeacher.FirstOrDefaultAsync(r => r.TeacherId == model.Id && r.SubjectId == subject.SubjectId);
							_context.SubjectTeacher.Remove(deletedRow);
							await _context.SaveChangesAsync();
						}
					}					
				}
				else if (teacherCheck != null && model.SubjectsId == null)
				{
					List<SubjectTeacher> allSubjectsForTeacher = _context.SubjectTeacher.Where(s => s.TeacherId == model.Id).ToList();
					foreach (var row in allSubjectsForTeacher)
					{
						_context.SubjectTeacher.Remove(row);
						await _context.SaveChangesAsync();
					}
				}
				else ModelState.AddModelError("", "Некорректные данные");
			}
			return RedirectToAction("Teachers", "Teacher");	
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateTeacher(TeacherViewModel viewModel)
		{
            if (ModelState.IsValid)
            {
				User user = await _context.Users.FirstOrDefaultAsync(u => (u.Id == viewModel.UserId));
                Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(t => (t.UserId == viewModel.UserId));

                if (user.UserRole == EnumRoles.Teacher && teacher == null)
                {
                    teacher = new Teacher { 
						UserId			= viewModel.UserId,
						Post			= viewModel.Post,
						Specialization	= viewModel.Specialization,
					};
 
                    await _context.Teachers.AddAsync(teacher);
                    await _context.SaveChangesAsync();
 				
                    return RedirectToAction("AddTeacher", "Teacher");
                }
                else ModelState.AddModelError("", "Некорректные данные");
            }
			else ModelState.AddModelError("", "Некорректные данные");

			return RedirectToAction("AddTeacher", "Teacher");
		}
	}
}
