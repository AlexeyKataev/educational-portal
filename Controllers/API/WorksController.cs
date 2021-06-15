using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.Account;
using Dotnet.ViewModels.API.App;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Dotnet.Enums.API;
using Dotnet.ViewModels.API.Education;

namespace Dotnet.Controllers.API
{
	[Route("API/Works")]
	[ApiController]
    public class WorksController : ControllerBase
    {
		private ApplicationContext _context;

        public WorksController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<List<WorkViewModel>>> GetWork() 
		{			
			// Передать информацию о заданиях для определённой подгруппы
			if (((EnumActions)Enum.Parse(typeof(EnumActions), $"{Request.Headers["Action"].ToString()}", true) == EnumActions.Get) && ModelState.IsValid)
			{
				Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == Request.Headers["Token"].ToString());
				Models.User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicket.UserId);

				if (user == null | userTicket == null) return BadRequest();

				Models.Student student = await _context.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);
				Models.StudySubgroup studySubgroup = await _context.StudySubgroups.FirstOrDefaultAsync(x => x.Id == student.StudySubgroupId);
				List<Models.Study.StudySubgroupWork> studySubgroupWork = await _context.StudySubgroupWork.Where(x => x.StudySubgroupId == studySubgroup.Id).ToListAsync();

				List<WorkViewModel> works = new List<WorkViewModel>();

				foreach (var row in studySubgroupWork)
				{
					Work tempWork = await _context.Works.FirstOrDefaultAsync(x => x.Id == row.WorkId);
					Teacher tempTeacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == tempWork.TeacherId);
					User tempTeacherAsUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == tempTeacher.UserId);
					Subject tempSubject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == tempWork.SubjectId);
					TypeWorks tempTypeWorks = await _context.TypesWorks.FirstOrDefaultAsync(x => x.Id == tempWork.TypeWorksId);

					if (tempWork != null)
						works.Add(new WorkViewModel {
							Id = tempWork.Id,
							Description = tempWork.Description,
							IsObligation = tempWork.IsObligation,
							DateAdded = tempWork.DateAdded,
							DateDeparture = tempWork.DateDeparture,
							CountAttempts = tempWork.CountAttempts,
							Teacher = new UserViewModel { 
									Id = tempTeacher.Id, 
									Email = tempTeacherAsUser.Email, 
									FirstName = tempTeacherAsUser.FirstName,
									SecondName = tempTeacherAsUser.SecondName,
									MiddleName = tempTeacherAsUser.MiddleName,
							},
							Subject = tempSubject.Name,
							TypeWorks = tempTypeWorks.Name,
							IsSetAttachment = false,
						});
				}

				if (works != null) return works;
				else return BadRequest();
			}

			return BadRequest();
		}
	}
}