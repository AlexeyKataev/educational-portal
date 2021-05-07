using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.Student
{
	public class EditStudentViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите пользователя.")]
		public int UserId { get; set;}	
		
		[Required(ErrorMessage = "Укажите статус.")]
		public bool IsLearns { get; set; }

		[Required(ErrorMessage = "Укажите учебную подгруппу.")]
		public int StudySubgroupId { get; set; }

		public bool IsOnline { get; set; } = false;
	}
}
