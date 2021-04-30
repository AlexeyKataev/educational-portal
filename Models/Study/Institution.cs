using System.Collections.Generic;

namespace Dotnet.Models
{
	public class Institution
	{
		public int Id { get; set; }

		public string AddressId { get; set; }	// Сменить тип
		
		public string ContactInformationId { get; set; }	// Сменить тип
		
		public string Name { get; set; }



		// Одно учебное заведение - много факультетов
		public List<Faculty> Faculties { get; set; } = new List<Faculty>();
	}
}
