using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class Subject
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public List<Work> Works { get; set; } = new List<Work>();
		public List<SubjectTeacher> SubjectTeacher { get; set; } = new List<SubjectTeacher>();
	}
}
