using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
 
namespace Dotnet.Controllers
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
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => (u.Email == model.Email || u.Login == model.Login));

                if (user == null)
                {
                    user = new User { 
						FirstName	= model.FirstName,
						SecondName	= model.SecondName,
						MiddleName	= null,
						DateOfBirth = new DateTime(0001, 1, 1, 1, 1, 1),
						DateAdded	= DateTime.Now,
						Email 		= model.Email, 
						Login		= model.Login,
						Password	= model.Password,
					};

                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

                    if (userRole != null)
                        user.Role = userRole;
 
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
 
                    await Authenticate(user); 
					
                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "Некорректные логин и (или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
			if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => (u.Email == model.Email || u.Login == model.Email) && u.Password == model.Password);

                if (user != null)
                {
                    await Authenticate(user); 
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и (или) пароль");
            }
            return View(model);
        }
		
        private async Task Authenticate(User user)
        {
			var claims = new List<Claim> {};

			if (user.Login != null)
			{
				claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
				};
			}
			else if (user.Email != null)
			{
				claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
				};
			}

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
				
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}