using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels
{
    public class TeacherModel
    {
        [Required(ErrorMessage = "Укажите ID пользователя")]
        public int UserId { get; set; }
         
        public string Post { get; set; }
        
		public string Specialization { get; set; }
    }
}