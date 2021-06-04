using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.Teacher
{
	public class EditTeacherViewModel
	{
       	[Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите идентификатор пользователя")]
        public int UserId { get; set; }

        public string Post { get; set; }

        public string Specialization { get; set; }
		
	}
}
