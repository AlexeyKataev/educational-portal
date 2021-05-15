using System.Collections.Generic;

namespace Dotnet.Models
{
	public class Specialty
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public int FacultyId { get; set; }
		public Faculty Faculty { get; set; }
		public List<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
	}
}
