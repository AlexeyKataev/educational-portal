using System.ComponentModel.DataAnnotations;
using Dotnet.Enums.WebApp;

namespace Dotnet.ViewModels.WebApp.Account
{
	public class EditUserViewModel
	{
       	[Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.DateTime)]
        public string DateOfBirth { get; set; }
		
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите роль пользователя")]
        public EnumRoles UserRole { get; set; }
	}
}
