using System;
using System.Collections.Generic;
using Dotnet.ViewModels.API.App;

namespace Dotnet.ViewModels.API.Messenger.Chat 
{
	public class ChatRoomViewModel
	{
		public long Id { get; set; } = 0;
		public UserViewModel UserCreator { get; set; } = new UserViewModel();
		public List<UserViewModel> Members { get; set; } = new List<UserViewModel>();
		public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
		public string Name { get; set; } = "Новый чат";
		public bool CanInviteParticipants { get; set; } = true;
        public bool CanParticipantsWriteMessages { get; set; } = true;
        public bool CanThereBeVideoCalls { get; set; } = true;
		public bool PersonalChat { get; set; } = true;
	}
}