using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Dotnet.Models;
using Dotnet.ViewModels.WebApp.Account;
using Dotnet.Enums.WebApp;
 
namespace Dotnet.Controllers.WebApp
{
    public class AccountController : Controller
    {
        private ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
			if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");			
			return View();
        }

		[HttpGet]
        public IActionResult Recovery() => View();

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Logout() 
		{ 
			await HttpContext.SignOutAsync(); 
			return RedirectToAction("Index", "Home"); 
		} 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => (u.Email == viewModel.Email || u.Login == viewModel.Login));

                if (user == null)
                {                    
					user.FirstName		= viewModel.FirstName;
					user.SecondName		= viewModel.SecondName;
					user.MiddleName		= null;
					user.DateOfBirth 	= new DateTime(0001, 1, 1, 1, 1, 1);
					user.DateAdded		= DateTime.Now;
					user.Email 			= viewModel.Email; 
					user.Login			= viewModel.Login;
					user.Password		= viewModel.Password;

                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.UserRole == EnumRoles.User);

                    if (userRole != null) user.UserRole = userRole.UserRole;
 
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
 
                    await Authenticate(user); 
					
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Некорректные логин и (или) пароль");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
			if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => 
					(u.Email == viewModel.Email || u.Login == viewModel.Email) && 
					u.Password == viewModel.Password
				);

                if (user != null)
                {
                    await Authenticate(user); 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и (или) пароль");
            }
            return View(viewModel);
        }
		
        private async Task Authenticate(User user)
        {
			var claims = new List<Claim>();

			if (user.Login != null)
			{
				claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserRole.ToString())
				};
			}
			else if (user.Email != null)
			{
				claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserRole.ToString())
				};
			}

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
				
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}