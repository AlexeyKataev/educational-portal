using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите E-Mail")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}