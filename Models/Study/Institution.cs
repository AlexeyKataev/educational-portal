using System.Collections.Generic;

namespace Dotnet.Models
{
	public class Institution
	{
		public ulong Id { get; set; }
		public string AddressId { get; set; }
		public string ContactInformationId { get; set; }
		public string Name { get; set; }
		public List<Faculty> Faculties { get; set; } = new List<Faculty>();
	}
}
