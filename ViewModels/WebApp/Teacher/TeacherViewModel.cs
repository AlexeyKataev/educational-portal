using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.Teacher
{
    public class TeacherViewModel
    {
        [Required(ErrorMessage = "Укажите ID пользователя")]
        public ulong UserId { get; set; }
         
        public string Post { get; set; }
        
		public string Specialization { get; set; }
    }
}