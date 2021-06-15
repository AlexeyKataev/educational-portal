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

namespace Dotnet.Enums.API
{
	[Flags]
	public enum EnumActions : byte
	{
		Unsettled = 0,
		Add = 1,
		Update = 2,
		Get = 3,
		Remove = 4,
		Delete = 5,
	}
}