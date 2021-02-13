using System.Collections.Generic;

/* Объект "специальность" */

namespace Dotnet.Models
{
	public class Specialty
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }



		// Много специальностей - один факультет
		public int FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		// Одна специальность - много учебный групп
		public List<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();

	}
}
