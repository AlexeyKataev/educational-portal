using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class StudySubgroup
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }



		// Много подгрупп - одна группа
		public int StudyGroupId { get; set; }
		public StudyGroup StudyGroup { get; set; }

		// Много подгрупп - много заданий
		public List<StudySubgroupWork> StudySubgroupWork { get; set; } = new List<StudySubgroupWork>();
	}
}
