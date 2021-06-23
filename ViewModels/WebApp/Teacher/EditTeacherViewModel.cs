using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Teacher
{
	public class EditTeacherViewModel
	{
       	[Required]
        public ulong Id { get; set; }

        [Required(ErrorMessage = "Введите идентификатор пользователя")]
        public ulong UserId { get; set; }

        public string Post { get; set; }

        public string Specialization { get; set; }
		
	}
}
