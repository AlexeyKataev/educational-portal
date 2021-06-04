using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        [Required(ErrorMessage = "Укажите ID пользователя")]
        public int UserId { get; set; }
         
        public string Post { get; set; }
        
		public string Specialization { get; set; }
    }
}