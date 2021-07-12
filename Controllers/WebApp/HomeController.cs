using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models.Study;
using Dotnet.Models;
using Dotnet.Enums.WebApp;

namespace Dotnet.Controllers.WebApp
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
			User me = _context.Users.FirstOrDefault(u => u.Login == User.Identity.Name);
			ViewBag.me = me;

			if (me.RoleId == 1)
			{
				ViewBag.aboutMe = "Главный администратор";
			}
			else if (me.RoleId == 6)	
			{
				Teacher aboutMe = _context.Teachers.FirstOrDefault(x => x.UserId == me.Id);
				
				if (aboutMe != null) ViewBag.aboutMe = $"{aboutMe.Specialization} • {aboutMe.Post}";
				else ViewBag.aboutMe = "Вы не входите в штат сотрудников";
			}
			else if (me.RoleId == 9)
			{
				Student aboutMe = _context.Students.FirstOrDefault(s => s.UserId == me.Id);

				StudySubgroup studySubgroup = 
					aboutMe != null ? _context.StudySubgroups.FirstOrDefault(s => s.Id == aboutMe.StudySubgroupId) : null;
				StudyGroup studyGroup = 
					aboutMe != null ? _context.StudyGroups.FirstOrDefault(s => s.Id == studySubgroup.StudyGroupId) : null;
				Specialty specialty = 
					aboutMe != null ? _context.Specialties.FirstOrDefault(s => s.Id == studyGroup.SpecialtyId) : null;

				List<StudySubgroupWork> studySubgroupWork = 
					aboutMe != null ? _context.StudySubgroupWork.Where(r => (r.StudySubgroupId == studySubgroup.Id)).ToList() : null;
				List<Work> works = 
					aboutMe != null ? _context.Works.OrderByDescending(d => d.DateAdded).ToList() : null;

				foreach (var work in works != null ? works.ToList() : new List<Work>())
					if (studySubgroupWork.Find(x => x.WorkId == work.Id) == null) works.RemoveAt(works.IndexOf(work));

				if (aboutMe != null) ViewBag.aboutMe = $"{specialty.Code} {specialty.Name} • {studyGroup.Name}, подгруппа {studySubgroup.Name}";
				else ViewBag.aboutMe = "Вы не входите ни в один список студентов учебных групп";

				ViewBag.subjects = _context.Subjects.ToList();
				ViewBag.typesWorks = _context.TypesWorks.ToList();
				ViewBag.fileWork = _context.FileWork.ToList();
				ViewBag.files = _context.Files.ToList();

				if (works?.Count > 5) 
				{
					ViewBag.isMuchWorks = true;
					ViewBag.myWorks = works.GetRange(0, 5);
				}
				else 
				{
					ViewBag.isMuchWorks = false;
					ViewBag.myWorks = works;
				}
			}
			else ViewBag.aboutMe = $"none";
			
            return View();
        }

		[HttpGet]
		[Authorize(Roles="student")]
		public IActionResult DownloadFile(string fileName)
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", fileName);
			byte[] bytes = System.IO.File.ReadAllBytes(path);
	
			return File(bytes, "application/octet-stream", fileName);
		}

		[Authorize(Roles="admin")]
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
