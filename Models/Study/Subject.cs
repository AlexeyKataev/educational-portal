using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Учебный предмет"	
	*/

	public class Subject
	{
		public int Id { get; set; }

		public string Name { get; set; }



		// Много предметов - много файлов
		public List<File> Files { get; set; } = new List<File>();	

		// Один предмет - много заданий	
		public List<Work> Works { get; set; } = new List<Work>();

	}
}
