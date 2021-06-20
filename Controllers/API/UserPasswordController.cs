using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models;
using Dotnet.ViewModels.API;
 
namespace Dotnet.Controllers.API
{
	[Route("API/UserPassword")]
	[ApiController]
    public class UserPasswordController : ControllerBase
    {
		private ApplicationContext _context;

        public UserPasswordController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<List<Dotnet.Models.Account.UserTicket>>> PostUserPassword(UserPasswordViewModel viewModel)
		{
			Dotnet.Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => x.Token == Request.Headers["Token"].ToString() && x.IsValid == true);

			if (userTicket != null)
			{
				List<Dotnet.Models.Account.UserTicket> userTickets = await _context.UserTickets.Where(x => x.UserId == userTicket.UserId && x.IsValid == true).ToListAsync();
				return userTickets;
			}
			
			return BadRequest();
		}
	}
}