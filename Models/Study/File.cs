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

		public System.DateTime NeedToDelete { get; set; }

		public System.DateTime dateAdded { get; set; }



		// Много документов - много специальностей
		public List<Specialty> Specialty { get; set; } = new List<Specialty>();

		// Много документов - много учебных предметов
		public List<Subject> Subjects { get; set; } = new List<Subject>();

		// Много файлов - один пользователь
		public User UserId { get; set; }
	}
}
