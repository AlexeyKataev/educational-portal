using System.Collections.Generic;

namespace Dotnet.Models
{
	public class TypeWorks
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public List<Work> Works { get; set; } = new List<Work>();
	}
}
