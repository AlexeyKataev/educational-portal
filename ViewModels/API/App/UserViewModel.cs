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
using Dotnet.Enums;

namespace Dotnet.ViewModels.API.App
{
	public class UserViewModel
    {
		public int Id { get; set; } = 0;

        public string Login { get; set; } = "";

        public string Email { get; set; } = "";

        public EnumRoles Role { get; set; } = EnumRoles.Unsettled;

        public string FirstName { get; set; } = "";

        public string SecondName { get; set; } = "";
        
        public string MiddleName { get; set; } = "";

        public string UI_Name { get => $"{FirstName} {MiddleName}"; }

        public string UI_FullName { get => $"{SecondName} {FirstName} {MiddleName}"; }
        
        public DateTime DateOfBirth { get; set; } = new DateTime(0001, 01, 01, 01, 01, 01);

        public string UI_DateOfBirth
        {
            get
            {
                if (DateOfBirth == new DateTime(0001, 01, 01, 01, 01, 01)) return "Не указано";
                else return $"{DateOfBirth:d MM yyyy}";
            }
        }

		public DateTime DateAdded { get; set; } = new DateTime(0001, 01, 01, 01, 01, 01);

        public string UI_DateAdded
        {
            get
            {
                if (DateAdded == new DateTime(0001, 01, 01, 01, 01, 01)) return "Не указано";
                else return $"{DateAdded:d MM yyyy}";
            }
        }
        
        public EnumSex Sex { get; set; } = EnumSex.Unsettled;
        
        public bool IsOnline { get; set; } = false;

        public DateTime LastSession { get; set; } = new DateTime(0001, 01, 01, 01, 01, 01);

        public string UI_LastSession
        {
            get
            {
                if (IsOnline) return "В сети";
                else return $"Был(а) в сети {LastSession:dd.MM.yyyy HH:mm}";
            }
        }
    }
}