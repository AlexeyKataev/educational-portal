namespace Dotnet.Models.Messenger.Chat
{
	public class UserMessageChatRoom
	{
		public long Id { get; set; }
		public long UserMessageId { get; set; } = 0;
		public UserMessage UserMessage { get; set; }
		public long ChatRoomId { get; set; } = 0;
		public ChatRoom ChatRoom { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}