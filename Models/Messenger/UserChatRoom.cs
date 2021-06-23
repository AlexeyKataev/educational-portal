using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger
{
	public class UserChatRoom
	{
		public ulong Id { get; set; } = 0;
		public ulong UserId { get; set; } = 0;
		[JsonIgnore]
		public User User { get; set; }
		public ulong ChatRoomId { get; set; } = 0;
		[JsonIgnore]
		public ChatRoom ChatRoom { get; set; }
		public ulong MessageLastReadId { get; set; } = 0;
		[JsonIgnore]
		public UserMessage UserMessage { get; set; }
		public System.DateTime UserExcluded { get; set; } = new System.DateTime(01, 01, 01, 01, 01, 01);
		public ulong ExcludedByWhomUserId { get; set; } = 0;
	}
}