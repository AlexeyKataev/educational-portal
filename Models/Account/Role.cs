using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Роль" 
	1 Role = ∞ Users	
	*/

	public class Role
	{
		public int Id { get; set; }
		
		public string Name { get; set; 
		}
		public List<User> Users { get; set; }

		public Role()
		{
			Users = new List<User>();
		}
	}
}
