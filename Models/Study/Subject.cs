using System.Collections.Generic;
using Dotnet.Models.Study;

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
		public List<SubjectTeacher> SubjectTeacher { get; set; } = new List<SubjectTeacher>();
	}
}
