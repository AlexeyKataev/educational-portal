using System.Collections.Generic;

namespace Dotnet.Models
{
	public class FormEducation
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public List<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
	}
}
