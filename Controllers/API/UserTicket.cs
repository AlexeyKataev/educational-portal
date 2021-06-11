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
	[Route("API/UserTicket")]
	[ApiController]
    public class UserTicket : ControllerBase
    {
		private ApplicationContext _context;

        public UserTicket(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<UserTicketViewModel>> GetUserTicket()
		{
			return new UserTicketViewModel { Login = "login", Password = "password", Token = "1234-5678-9012-3456", OS = "Microsoft Windows 10" };
		}

		[HttpPost]
		//public async Task<ActionResult<UserTicketViewModel>> PostUserTicket(string Login, string Password, string OS, string Token) 
		public async Task<ActionResult<UserTicketViewModel>> PostUserTicket(UserTicketViewModel viewModel) 
		{
			if (ModelState.IsValid)
			{
				/* 

				Тут просто рофл, в дальнейшем нужно будет добавить таблицу с токенами, сроком их действия и т.д.

				https://localhost:5001/API/UniversalWindowsPlatform/GetUser?Login=%22user1%22&Password=%22123%22&Token=%220%22&OS=%22Microsoft%20Windows%2010%22

				https://localhost:5001/API/UniversalWindowsPlatform?Login=%22user1%22&Password=%22123%22

				https://localhost:5001/API/UniversalWindowsPlatform?Login=user1&Password=123&Token=0&OS=Windows
				*/

				// Добавить проверку на null
				//User user = await _context.Users.FirstOrDefaultAsync(x => x.Login == Login && x.Password == Password);
				
				Dotnet.Models.User user = await _context.Users.FirstOrDefaultAsync(x => x.Login == viewModel.Login && x.Password == viewModel.Password);

				return new UserTicketViewModel { Login = user.Login, Password = user.Password, Token = "1234-5678-9012-3456", OS = "Microsoft Windows 10" };
			}
			else return BadRequest();
		}
	}
}