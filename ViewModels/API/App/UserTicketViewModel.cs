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
        public string Login { get; set; } = "";
        
        public string Password { get; set; } = "";
        
        public string OS { get; set; } = "Microsoft Windows 10";

        private string token = "0";
        public string Token
        {
            get => token;
            set { token = value; }
        }
    }
}