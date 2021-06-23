using System;
using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger
{
	public class ChatRoom
	{
		public ulong Id { get; set; } = 0;
		public ulong UserId { get; set; } = 0;
		public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
		public string Name { get; set; } = "Новый чат";
		public ulong AvatarId { get; set; } = 0;
		[JsonIgnore]
		public File File { get; set; }
		public bool CanInviteParticipants { get; set; } = true;
		
	}
}