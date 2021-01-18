using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Форма обучения"	
	*/

	public class FormEducation
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		// Много групп - одна форма обучения
		public List<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
	}
}
