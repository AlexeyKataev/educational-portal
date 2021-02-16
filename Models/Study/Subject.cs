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



		// Один предмет - много заданий	
		public List<Work> Works { get; set; } = new List<Work>();

		// Много предметов - много преподавателей
		public List<Teacher> Teachers { get; set; } = new List<Teacher>();
	}
}
