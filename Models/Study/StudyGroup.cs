using System.Collections.Generic;

namespace Dotnet.Models
{
	public class StudyGroup
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public System.DateTime DateStart { get; set; }
		public System.DateTime DateEnd { get; set; }
		public List<StudySubgroup> StudySubgroups { get; set; } = new List<StudySubgroup>();
		public int FormEducationId { get; set; }
		public FormEducation FormEducation { get; set; }	
		public int SpecialtyId { get; set; }
		public Specialty Specialty { get; set; }	
	}
}
