using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dotnet.Models;
using Dotnet.ViewModels.Account;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Dotnet.ViewModels.API.App
{
	public class UserTicketViewModel
    {
		public int Id { get; set; } = 0;

        public string Login { get; set; } = "";

        public string Email { get; set; } = "";
        
        public string Password { get; set; } = "";
        
        public string OS { get; set; } = "";

		public string Location { get; set; } = "";

        private string token = "0";
        public string Token
        {
            get => token;
            set { token = value; }
        }

		public DateTime ActivationDate { get; set; } = DateTime.UtcNow;

		public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public bool IsValid { get; set; } = true;

		public int UserId { get; set; } = 0;
    }
}