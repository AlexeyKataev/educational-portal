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

namespace Dotnet.Controllers.API
{
	[Route("API/UserTicket")]
	[ApiController]
    public class UserTicketController : ControllerBase
    {
		private ApplicationContext _context;

        public UserTicketController(ApplicationContext context)
        {
            _context = context;
        }

		private static Random random = new Random();
		private async Task<string> RandomString(int length)
		{
			return await Task.Run(() => {
				const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
			});
		}

		/* 
			if (
				((EnumActions)Enum.Parse(typeof(EnumActions), $"{Request.Headers["Action"].ToString()}", true) == EnumActions.Remove) &&
				ModelState.IsValid
			)
			{
		*/

		[HttpPost]
		public async Task<ActionResult<Models.Account.UserTicket>> PostUserTicket(UserTicketViewModel viewModel) 
		{			
			// Сгенерировать новый токен
			if (((EnumActions)Enum.Parse(typeof(EnumActions), $"{Request.Headers["Action"].ToString()}", true) == EnumActions.Get) && ModelState.IsValid)
			{
				Dotnet.Models.User user = await _context.Users.FirstOrDefaultAsync(x => (x.Login == viewModel.Login || x.Email == viewModel.Login) && x.Password == viewModel.Password && x.RoleId == 9);

				if (user != null)
				{
					Models.Account.UserTicket userTicket = new Models.Account.UserTicket {
						OS			= viewModel.OS,
						Location	= viewModel.Location,
						Token		= await RandomString(32),
						IsValid		= true,
						UserId		= user.Id,						
					};

					await _context.AddAsync(userTicket);
					await _context.SaveChangesAsync();

					return userTicket;
				}
				
				return BadRequest();
			}
			// Деактивация приложения и токена
			else if (((EnumActions)Enum.Parse(typeof(EnumActions), $"{Request.Headers["Action"].ToString()}", true) == EnumActions.Remove) && ModelState.IsValid)
			{
				Dotnet.Models.Account.UserTicket userTicket = await _context.UserTickets.FirstOrDefaultAsync(x => 
					x.Token == Request.Headers["Token"].ToString() && 
					x.IsValid == true
				);

				if (userTicket != null)
				{
					userTicket.IsValid		= false;
					userTicket.LastActive	= DateTime.UtcNow;
					
					await _context.SaveChangesAsync();

					return userTicket;
				}

				return BadRequest();
			}

			return BadRequest();
		}
	}
}