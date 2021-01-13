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
	}
}
