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

namespace Dotnet.ViewModels.API.Education
{
	public class WorkViewModel
	{
		public int Id { get; set; } = 0;

		public string Description { get; set; } = "Нет описания";

		public bool IsObligation { get; set; } = true;

		public DateTime DateAdded { get; set; } = new DateTime(01, 01, 01, 01, 01, 01);

		public DateTime DateDeparture { get; set; } = new DateTime(01, 01, 01, 01, 01, 01);

		public int CountAttempts { get; set; } = 1;

		public Dotnet.ViewModels.API.App.UserViewModel Teacher { get; set; } = new Dotnet.ViewModels.API.App.UserViewModel { };

		public string Subject { get; set; } = "";

		public string TypeWorks { get; set; } = "";

		public bool IsSetAttachment { get; set; } = false;

		public string AttachmentDownloadPath { get; set; } = "";
	}
}