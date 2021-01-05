namespace Dotnet.Models
{
    public class User
    {
	   	public int Id { get; set; }

        public string FirstName { get; set; }
		
        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        public System.DateTime DateOfBirth { get; set; }

		public System.DateTime DateAdded { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }



		// Один пользователь - много ролей... ЭЭЭЭЭ, говно какое-то, должно быть не так
		public int? RoleId { get; set; }
    	public Role Role { get; set; }
    }
}
