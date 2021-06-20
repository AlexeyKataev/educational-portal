using System.Collections.Generic;
using Dotnet.Enums.WebApp;

namespace Dotnet.Models
{
	public class Role
	{
		public int Id { get; set; }
		
		public string Name { get; set; }

		public EnumRoles UserRole { get; set; }

		public List<User> Users { get; set; }
		
		public Role()
		{
			Users = new List<User>();
		}
	}
}
