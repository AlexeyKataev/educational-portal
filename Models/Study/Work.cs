using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class Work
	{
		public int Id { get; set; }
		[Column(TypeName = "ntext")]
		public string Description { get; set; }
		public bool IsObligation { get; set; }
		public System.DateTime DateAdded { get; set; }
		public System.DateTime DateDeparture { get; set; }
		public int CountAttempts { get; set; }
		public int TeacherId { get; set; }
		public Teacher Teacher { get; set; }
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }
		public int TypeWorksId { get; set; }
		public TypeWorks TypeWorks { get; set; }
		public List<FileWork> FileWork { get; set; } = new List<FileWork>();
		public List<StudySubgroupWork> StudySubgroupWork { get; set; } = new List<StudySubgroupWork>();
	}
}
