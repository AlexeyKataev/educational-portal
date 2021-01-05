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
		public int FacultyId { get; set; }
		public Faculty Faculty { get; set; }

		// Много специальностей - много документов  
		public List<File> Files { get; set; } = new List<File>();
	}
}
