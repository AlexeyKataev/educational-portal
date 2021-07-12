using System.Collections.Generic;

namespace Dotnet.Models
{
	public class Role
	{
		public long Id { get; set; }
		
		public string Name { get; set; }

		public List<User> Users { get; set; }
		
		public Role()
		{
			Users = new List<User>();
		}
	}
}
