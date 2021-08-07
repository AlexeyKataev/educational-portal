using System.Collections.Generic;
using Dotnet.Models.Messenger.Chat;

namespace Dotnet.Models
{
	public class File
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Pseudonym { get; set; }
		public string PathFile { get; set; }
		public bool Vanish { get; set; }
		public System.DateTime NeedToDelete { get; set; }
		public System.DateTime DateAdded { get; set; }
		public List<FileWork> FileWork { get; set; } = new List<FileWork>();
		public long UserId { get; set; }
		public User User { get; set; }
		public List<ChatRoom> ChatRoom { get; set; } = new List<ChatRoom>();
	}
}
