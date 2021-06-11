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
    public class User : ControllerBase
    {
		private ApplicationContext _context;

        public User(ApplicationContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<ActionResult<UserViewModel>> GetUser()
		{
			return new UserViewModel { 
				FirstName = "Иван", 
				SecondName = "Шлыков", 
				MiddleName = "Игоревич", 
				DateAdded = new DateTime(0001, 01, 01, 01, 01, 01),
				DateOfBirth = new DateTime(2001, 01, 20, 01, 01, 01),
				Email = "mail@mail.ru",
				Id = 123,
				Login = "user1",
				Role = Enums.EnumRoles.Student,
			};
		}
	}
}