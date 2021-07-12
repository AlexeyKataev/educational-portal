using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger
{
	public class ChatRoom
	{
		public long Id { get; set; } = 0;
		public long UserCreaterId { get; set; } = 0;
		public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
		public string Name { get; set; } = "Новый чат";
		public long FileAvatarId { get; set; } = 0;
		public bool CanInviteParticipants { get; set; } = true;
		public bool CanParticipantsWriteMessages { get; set; } = true;
		public bool CanThereBeVideoCalls { get; set; } = true;
		[JsonIgnore]
		public File File { get; set; }
		[JsonIgnore]
		public User User { get; set; }
	}
}