using System.Text.Json.Serialization;
using System.Collections.Generic;
using Dotnet.Models.Account;
using Dotnet.Models.Messenger;

namespace Dotnet.Models
{
    public class User
    {
	   	public ulong Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
		public System.DateTime DateAdded { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    	public Enums.WebApp.EnumRoles UserRole { get; set; }
		[JsonIgnore]
		public List<File> Files { get; set; } = new List<File>();
		[JsonIgnore]
		public List<UserTicket> UserTickets { get; set; } = new List<UserTicket>();
		[JsonIgnore]
		public List<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
    }
}
