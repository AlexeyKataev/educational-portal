using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Учебное заведение"	
	*/

	public class Institution
	{
		public int Id { get; set; }

		public int AddressId { get; set; }
		
		public string Name { get; set; }

		public List<User> Users { get; set; }

	}
}
