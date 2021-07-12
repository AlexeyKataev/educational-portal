using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dotnet.Models.Messenger
{
	public class UserMessage
	{
		public long Id { get; set; } = 0;
		public long UserId { get; set; } = 0;
		[JsonIgnore]
		public User User { get; set; }
		public string Messsage { get; set; } = null;
		public System.DateTime SendingTime { get; set; } = System.DateTime.UtcNow;
		public System.DateTime EditingTime { get; set; } = new System.DateTime(01, 01, 01, 01, 01, 01);
		public System.DateTime IsArchival { get; set; } = new System.DateTime(01, 01, 01, 01, 01, 01);
		[JsonIgnore]
		public List<UserMessageChatRoom> UserMessageChatRoom { get; set; } = new List<UserMessageChatRoom>();
		[JsonIgnore]
		public List<MessageAttachedFile> MessageAttachedFile { get; set; } = new List<MessageAttachedFile>();
	}
}