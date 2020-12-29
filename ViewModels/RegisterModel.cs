using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels
{
    public class RegisterModel
    {
		[Required(ErrorMessage ="Введите имя")]
        public string FirstName { get; set; }

		[Required(ErrorMessage ="Введите фамилию")]
        public string SecondName { get; set; }

        [Required(ErrorMessage ="Введите E-Mail")]
        public string Email { get; set; }

		[Required(ErrorMessage ="Введите имя пользователя")]
        public string Login { get; set; }
         
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}