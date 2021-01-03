using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Специальность"	
	*/

	public class Specialty
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }



		// Много специальностей - один факультет
		public Faculty FacultyId { get; set; }

		// Одна специальность - один учебный план 
		public Faculty FileId { get; set; }
	}
}
