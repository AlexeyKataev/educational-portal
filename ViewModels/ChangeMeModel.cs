using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels
{
    public class ChangeMeModel
    {
        [Required(ErrorMessage = "Введите E-Mail")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Введите дату рождения")]
        [DataType(DataType.DateTime)]
        public string DateOfBirth { get; set; }
    }
}