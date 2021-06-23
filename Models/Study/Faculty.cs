using System.Collections.Generic;

namespace Dotnet.Models
{
	public class Faculty
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string About { get; set; }
		public ulong InstitutionId { get; set; }
		public Institution Institution { get; set; }
		public List<Specialty> Specialty { get; set; } = new List<Specialty>();
	}
}
