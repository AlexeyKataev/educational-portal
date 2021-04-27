using System.Collections.Generic;
using Dotnet.Models.Study;

namespace Dotnet.Models
{
	public class FileWork
	{
		public int Id { get; set; }

		public int FileId { get; set; }
		public File File { get; set; }

		public int WorkId { get; set; }
		public Work Work { get; set; }
	}
}
