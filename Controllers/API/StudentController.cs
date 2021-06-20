using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.WebApp.Account;
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
	[Route("API/Student")]
	[ApiController]
    public class StudentController : ControllerBase
    {
		private ApplicationContext _context;

        public StudentController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<StudentViewModel>> GetStudent() 
		{			
			// Передать информацию о студенте
			if (((EnumActions)Enum.Parse(typeof(EnumActions), $"{Request.Headers["Action"].ToString()}", true) == EnumActions.Get))
			{
				Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == Request.Headers["Token"].ToString());
				Models.User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicket.UserId);

				if (userTicket == null || user == null) return BadRequest();

				Models.Student student = await _context.Students.FirstOrDefaultAsync(x => x.UserId == user.Id);
				Models.StudySubgroup studySubgroup = await _context.StudySubgroups.FirstOrDefaultAsync(x => x.Id == student.StudySubgroupId);
				Models.StudyGroup studyGroup = await _context.StudyGroups.FirstOrDefaultAsync(x => x.Id == studySubgroup.StudyGroupId);
				Models.Specialty specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == studyGroup.SpecialtyId);

				return new StudentViewModel {
					Id 			= student.Id,
					UserId		= student.UserId,
					IsLearns	= student.IsLearns,
					IsOnline	= student.IsOnline,
					StudySubgroup = new StudySubgroupViewModel {
						Id			= studySubgroup.Id,
						Name		= studySubgroup.Name,
						Code		= studySubgroup.Code,
						StudyGroup 	= new StudyGroupViewModel {
							Id 			= studyGroup.Id,
							Name	 	= studyGroup.Name,
							Code		= studyGroup.Code,
							DateStart	= studyGroup.DateStart,
							DateEnd		= studyGroup.DateEnd,
							Specialty	= new SpecialtyViewModel {
								Id 			= specialty.Id,
								Name		= specialty.Name,
								Code		= specialty.Code,
							},
						},
					},
				};
			}


			return BadRequest();
		}
	}
}