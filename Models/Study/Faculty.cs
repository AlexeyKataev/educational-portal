using System.Collections.Generic;

namespace Dotnet.Models
{
	/* 
	Создание объекта "Факультет"	
	*/

	public class Faculty
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }
		
		public string About { get; set; }



		// Много факультетов - одно учебное заведение 
		public Institution InstitutionId { get; set; }

		// Один факультет - много специальностей
		public List<Faculty> Faculties { get; set; } = new List<Faculty>();
	}
}
