using System.Collections.Generic;

namespace Dotnet.Models
{
	public class File
	{
		public ulong Id { get; set; }
		public string Name { get; set; }
		public string Pseudonym { get; set; }
		public string PathFile { get; set; }
		public bool Vanish { get; set; }
		public System.DateTime NeedToDelete { get; set; }
		public System.DateTime DateAdded { get; set; }
		public List<FileWork> FileWork { get; set; } = new List<FileWork>();
		public ulong UserId { get; set; }
		public User User { get; set; }
	}
}
