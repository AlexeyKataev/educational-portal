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

		[HttpGet]
		public async Task<ActionResult<UserViewModel>> GetUser()
		{
			Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == Request.Headers["Token"].ToString());
			Models.User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userTicket.UserId);
		
			return new UserViewModel {
				Id		= user.Id,
				Login	= user.Login,
				Email	= user.Email,
				Role	= user.RoleId == 1 ? Enums.EnumRoles.Admin : user.RoleId == 6 ? Enums.EnumRoles.Teacher : user.RoleId == 9 ? Enums.EnumRoles.Student : user.RoleId == 10 ? Enums.EnumRoles.User : Enums.EnumRoles.Unsettled,
				FirstName	= user.FirstName,
				SecondName	= user.SecondName,
				MiddleName	= user.MiddleName,
				DateOfBirth	= user.DateOfBirth,
				DateAdded	= user.DateAdded,
			};
		}
	}
}