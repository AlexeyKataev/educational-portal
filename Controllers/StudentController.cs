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
using Dotnet.ViewModels.Student;
using Dotnet.Models.Study;

namespace Dotnet.Controllers
{
	[Authorize(Roles="admin, systemAdmin, humanResources")]
    public class StudentController : Controller
    {
		private ApplicationContext _context;
		private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger, ApplicationContext context)
        {			
			_context = context;
            _logger = logger;
        }

        public IActionResult AddStudent()
        {
			List<User> users = _context.Users.Where(u => u.RoleId == 9).ToList();

			foreach (var user in users.ToList())
			{
				Student student = _context.Students.FirstOrDefault(u => (u.UserId == user.Id));

				if (student != null)
					users.RemoveAt(users.IndexOf(user));
			}

			ViewBag.allUsers = users;

			ViewBag.studySubgroups = _context.StudySubgroups.ToList();
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();

            return View();
        }

		public IActionResult Students()
		{
			List<User> users = new List<User>();
			List<Student> students = _context.Students.ToList();

			foreach (var student in students)
				users.Add(_context.Users.FirstOrDefault(u => u.Id == student.UserId));

			ViewBag.users = users;
			ViewBag.students = students;
			ViewBag.studySubgroups = _context.StudySubgroups.ToList();
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();

			return View();
		}

		[HttpGet]
		public IActionResult EditStudent(int studentId)
		{


			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateStudent(StudentViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == viewModel.UserId);
				Student student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == viewModel.UserId);
				
				if (user.RoleId == 9 && student == null)
				{
					student = new Student {
						UserId			= viewModel.UserId,
						IsLearns		= viewModel.IsLearns,
						StudySubgroupId	= viewModel.StudySubgroupId,
						IsOnline		= viewModel.IsOnline,
					};

					await _context.Students.AddAsync(student);
					await _context.SaveChangesAsync();
				}
				else ModelState.AddModelError("", "Некорректные данные.");
			}
			return RedirectToAction("AddStudent", "Student");
		}
	}
}
