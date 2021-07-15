using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Models;
using Dotnet.ViewModels.API.App;
using Microsoft.EntityFrameworkCore;
using Dotnet.Enums.API;
using Dotnet.ViewModels.API.Education;
using Dotnet.Classes;

namespace Dotnet.Controllers.API
{
	[Route("API/Students")]
	[ApiController]
    public class StudentsController : ControllerBase
    {
		private ApplicationContext _context;

        public StudentsController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<List<StudentViewModel>>> GetStudents() 
		{			
			if (((EnumActions)Enum.Parse(typeof(EnumActions), $"{Request.Headers["Action"].ToString()}", true) == EnumActions.Get))
			{
				string token = Request.Headers["Token"].ToString();

				Models.Account.UserTicket userTicketCheck = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == token);
				Models.User userCheck = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicketCheck.UserId);

				// Если нет токена или пользователь не существует
				if (userTicketCheck == null || userCheck == null) return BadRequest();

				// Если токет деактивирован
				if (!userTicketCheck.IsValid) 
				{
					return BadRequest();
				}
				else 
				{
					userTicketCheck.LastActive = DateTime.UtcNow;
					await _context.SaveChangesAsync();
				}

				// Если пользователь - это студент
				if (userCheck.RoleId == 9)
				{
					Models.Student studentTemp = await _context.Students.FirstOrDefaultAsync(x => x.UserId == userCheck.Id);
					Models.StudySubgroup studySubgroupTemp = await _context.StudySubgroups.FirstOrDefaultAsync(x => x.Id == studentTemp.StudySubgroupId);
					Models.StudyGroup studyGroupTemp = await _context.StudyGroups.FirstOrDefaultAsync(x => x.Id == studySubgroupTemp.StudyGroupId);
					
					List<Models.StudySubgroup> studySubgroupsTemp = await _context.StudySubgroups.Where(x => x.StudyGroupId == studyGroupTemp.Id).ToListAsync();

					List<Models.Student> studentsTemp = new List<Models.Student>();

					foreach (var item in studySubgroupsTemp)
					{
						studentsTemp.AddRange(await _context.Students.Where(x => x.StudySubgroupId == item.Id).ToListAsync());
					}

					List<StudentViewModel> students = new List<StudentViewModel>();

					foreach (var item in studentsTemp)
					{
						Models.User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == item.UserId);
						Models.StudySubgroup studySubgroup = await _context.StudySubgroups.FirstOrDefaultAsync(x => x.Id == item.StudySubgroupId);
						Models.StudyGroup studyGroup = await _context.StudyGroups.FirstOrDefaultAsync(x => x.Id == studySubgroup.StudyGroupId);
						Models.Specialty specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == studyGroup.SpecialtyId);

						students.Add(
							new StudentViewModel {
							Id 				= item.Id,
							User			= new UserViewModel {
								Id				= user.Id,
								FirstName 		= user.FirstName,
								SecondName		= user.SecondName,
								MiddleName		= user.MiddleName,
								DateOfBirth		= user.DateOfBirth,
							},
							IsLearns		= item.IsLearns,
							IsOnline		= item.IsOnline,
							StudySubgroup 	= new StudySubgroupViewModel {
								Id				= studySubgroup.Id,
								Name			= studySubgroup.Name,
								Code			= studySubgroup.Code,
								StudyGroup 		= new StudyGroupViewModel {
									Id 				= studyGroup.Id,
									Name	 		= studyGroup.Name,
									Code			= studyGroup.Code,
									DateStart		= studyGroup.DateStart,
									DateEnd			= studyGroup.DateEnd,
									Specialty		= new SpecialtyViewModel {
										Id 				= specialty.Id,
										Name			= specialty.Name,
										Code			= specialty.Code,
									},
								},
							},
						});
					}

					return students;
				}				
			}

			return BadRequest();
		}
	}
}