using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Файл"	
	*/

	public class File
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Pseudonym { get; set; }

		public string PathFile { get; set; }

		public bool Vanish { get; set; }

		public System.DateTime NeedToDelete { get; set; }

		public System.DateTime dateAdded { get; set; }



		// Много файлов - много заданий
		public List<Work> Works { get; set; } = new List<Work>();

		// Много файлов - один пользователь
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
