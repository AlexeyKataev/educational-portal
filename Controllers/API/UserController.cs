using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models;
using Dotnet.ViewModels.API.App;
using Dotnet.Enums.WebApp;
 
namespace Dotnet.Controllers.API
{
	[Route("API/User")]
	[ApiController]
    public class UserController : ControllerBase
    {
		private ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

		// Добавить проверку действия
		[HttpGet]
		public async Task<ActionResult<UserViewModel>> GetUser()
		{
			Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == Request.Headers["Token"].ToString());
			Models.User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicket.UserId);
		
			return new UserViewModel {
				Id		= user.Id,
				Login	= user.Login,
				Email	= user.Email,
				Role	= user.RoleId == 1 ? EnumRoles.Admin : 
					user.RoleId == 6 ? EnumRoles.Teacher : 
					user.RoleId == 9 ? EnumRoles.Student : 
					user.RoleId == 10 ? EnumRoles.User : EnumRoles.Unsettled,
				FirstName	= user.FirstName,
				SecondName	= user.SecondName,
				MiddleName	= user.MiddleName,
				DateOfBirth	= user.DateOfBirth,
				DateAdded	= user.DateAdded,
			};
		}

		// Добавить проверку действия
		[HttpPost]
		public async Task<ActionResult<UserViewModel>> PostUser(UserViewModel user)
		{
			Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == Request.Headers["Token"].ToString());
			Models.User editUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicket.UserId);

			if (userTicket == null || editUser == null) return BadRequest();

			try 
			{
				editUser.FirstName		= user.FirstName;
				editUser.SecondName		= user.SecondName;
				editUser.MiddleName		= user.MiddleName;
				editUser.DateOfBirth	= user.DateOfBirth;
				editUser.Email			= user.Email;

				await _context.SaveChangesAsync();

				return user;
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}