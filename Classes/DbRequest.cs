using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dotnet.Models;
using Dotnet.Models.Account;

namespace Dotnet.Classes
{
	public class DbRequest
	{
		private ApplicationContext _context;		

		public DbRequest(ApplicationContext context)
		{
			_context = context;
		}
	}
}