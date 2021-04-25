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



		// Одна группа - много подгрупп
		public List<StudySubgroup> StudySubgroups { get; set; } = new List<StudySubgroup>();

		// Много групп - одна группа обучения
		public int FormEducationId { get; set; }
		public FormEducation FormEducation { get; set; }	

		// Много групп - одна специальность
		public int SpecialtyId { get; set; }
		public Specialty Specialty { get; set; }	
	}
}
