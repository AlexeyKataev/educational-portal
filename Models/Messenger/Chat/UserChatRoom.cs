using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger.Chat
{
	public class UserChatRoom
	{
		public long Id { get; set; } = 0;
		public long UserId { get; set; } = 0;
		[JsonIgnore]
		public User User { get; set; }
		public long ChatRoomId { get; set; } = 0;
		[JsonIgnore]
		public ChatRoom ChatRoom { get; set; }
		public long MessageLastReadId { get; set; } = 0;
		[JsonIgnore]
		public UserMessage UserMessage { get; set; }
		public System.DateTime UserExcluded { get; set; } = new System.DateTime(01, 01, 01, 01, 01, 01);
		public long ExcludedByWhomUserId { get; set; } = 0;
	}
}