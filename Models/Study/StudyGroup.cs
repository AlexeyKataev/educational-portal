using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Учебная группа"	
	*/

	public class StudyGroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public System.DateTime DateStart { get; set; }

		public System.DateTime DateEnd { get; set; }



		// Одна группа - одна подгруппа
		public StudySubgroup StudySubgroup { get; set; }

		// Много групп - одна форма обучения
		public List<FormEducation> FormsEducation { get; set; } = new List<FormEducation>();
	}
}
