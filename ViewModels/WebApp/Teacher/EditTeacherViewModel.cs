using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Teacher
{
	public class EditTeacherViewModel
	{
       	[Required]
        public long Id { get; set; }

        [Required(ErrorMessage = "Введите идентификатор пользователя")]
        public long UserId { get; set; }

        public string Post { get; set; }

        public string Specialization { get; set; }
		
	}
}
