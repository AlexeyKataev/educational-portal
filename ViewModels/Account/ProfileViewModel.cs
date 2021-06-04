using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.Account
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.DateTime)]
        public string DateOfBirth { get; set; }
		
        public string Email { get; set; }
    }
}