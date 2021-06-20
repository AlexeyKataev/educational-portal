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
 
namespace Dotnet.Controllers.API
{
	[Route("API/UserTickets")]
	[ApiController]
    public class UserTicketsController : ControllerBase
    {
		private ApplicationContext _context;

        public UserTicketsController(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<List<Dotnet.Models.Account.UserTicket>>> GetUserTickets()
		{
			// Получить список активных устройств для определенной учётной записи
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