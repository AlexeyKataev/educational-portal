using System.Collections.Generic;

namespace Dotnet.Models
{
	public class Specialty
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public ulong FacultyId { get; set; }
		public Faculty Faculty { get; set; }
		public List<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
	}
}
