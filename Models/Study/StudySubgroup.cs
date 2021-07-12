using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class StudySubgroup
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public long StudyGroupId { get; set; }
		public StudyGroup StudyGroup { get; set; }
		public List<StudySubgroupWork> StudySubgroupWork { get; set; } = new List<StudySubgroupWork>();
		public List<Student> Students { get; set; } = new List<Student>();
	}
}
