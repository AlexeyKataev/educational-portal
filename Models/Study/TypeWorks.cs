using System.Collections.Generic;

namespace Dotnet.Models
{
	public class TypeWorks
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public List<Work> Works { get; set; } = new List<Work>();
	}
}
