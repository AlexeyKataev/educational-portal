using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.Models.Study;
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
			User user = _context.Users.FirstOrDefault(u => (u.Login == User.Identity.Name));
			Teacher teacher = _context.Teachers.FirstOrDefault(t => (t.UserId == user.Id));

			List<SubjectTeacher> subjectTeacher = _context.SubjectTeacher.Where(x => x.TeacherId == teacher.Id).ToList();
			
			List<Subject> subjects = new List<Subject>();

			foreach (var row in subjectTeacher)
			{
				Subject subject = _context.Subjects.FirstOrDefault(x => x.Id == row.SubjectId);
				if (subject != null) subjects.Add(subject);
			}

			ViewBag.typesWorks = _context.TypesWorks.ToList();
			ViewBag.subjects = subjects;

			ViewBag.studySubgroups = _context.StudySubgroups.ToList();
			ViewBag.studyGroups = _context.StudyGroups.ToList();
			ViewBag.specialties = _context.Specialties.ToList();
			ViewBag.faculties = _context.Faculties.ToList();
			ViewBag.institutions = _context.Institutions.ToList();

            return View();
        }
	}
}
