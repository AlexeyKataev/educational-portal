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

		public string PathFile { get; set; }

		public bool Vanish { get; set; }

		public bool NeedToDelete { get; set; }

		public System.DateTime dateAdded { get; set; }



		// Один файл - одна специальность
		public Specialty Specialty { get; set; }

		// Много файлов - один пользователь
		public User UserId { get; set; }

		// Ещё связи с другими таблицами
	}
}
