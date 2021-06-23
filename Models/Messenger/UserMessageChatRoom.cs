namespace Dotnet.Models.Messenger
{
	public class UserMessageChatRoom
	{
		public ulong Id { get; set; }
		public ulong UserMessageId { get; set; } = 0;
		public UserMessage UserMessage { get; set; }
		public ulong ChatRoomId { get; set; } = 0;
		public ChatRoom ChatRoom { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}