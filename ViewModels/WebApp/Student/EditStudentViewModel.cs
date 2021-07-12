using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Student
{
	public class EditStudentViewModel
	{
		[Required]
		public long Id { get; set; }

		[Required(ErrorMessage = "Укажите пользователя.")]
		public long UserId { get; set; }	
		
		[Required(ErrorMessage = "Укажите статус.")]
		public bool IsLearns { get; set; }

		[Required(ErrorMessage = "Укажите учебную подгруппу.")]
		public long StudySubgroupId { get; set; }

		public bool IsOnline { get; set; } = false;
	}
}
