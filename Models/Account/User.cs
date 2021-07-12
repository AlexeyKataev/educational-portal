using System.Collections.Generic;
using System.Text.Json.Serialization;
using Dotnet.Models.Account;
using Dotnet.Models.Messenger;

namespace Dotnet.Models
{
    public class User
    {
	   	public long Id { get; set; } = 0;
        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public System.DateTime DateOfBirth { get; set; } = new System.DateTime(01, 01, 01, 01, 01, 01);
		public System.DateTime DateAdded { get; set; } = System.DateTime.UtcNow;
        public string Email { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    	public long? RoleId { get; set; }
    	public Role Role { get; set; }
		[JsonIgnore]
		public List<File> Files { get; set; } = new List<File>();
		[JsonIgnore]
		public List<UserTicket> UserTickets { get; set; } = new List<UserTicket>();
		[JsonIgnore]
		public List<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
