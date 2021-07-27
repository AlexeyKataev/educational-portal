using System;
using Dotnet.ViewModels.API.App;

namespace Dotnet.ViewModels.API.Messenger.Chat
{
	public class UserMessageViewModel
    {
        public long Id { get; set; } = 0;
        public UserViewModel User { get; set; } = new UserViewModel();
        public ChatRoomViewModel ChatRoom { get; set; } = new ChatRoomViewModel();
        public string Message { get; set; } = "";
        public DateTime SendingTime { get; set; } = DateTime.UtcNow;
        public DateTime EditingTime { get; set; } = new DateTime(01, 01, 01, 01, 01, 01);
        public DateTime IsArchival { get; set; } = new DateTime(01, 01, 01, 01, 01, 01);
        public bool WhetherDelivered { get; set; } = true;
    }
}