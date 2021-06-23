using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Student
{
	public class StudentViewModel
	{
		[Required(ErrorMessage = "Укажите пользователя.")]
		public ulong UserId { get; set;}	
		
		[Required(ErrorMessage = "Укажите статус.")]
		public bool IsLearns { get; set; }

		[Required(ErrorMessage = "Укажите учебную подгруппу.")]
		public ulong StudySubgroupId { get; set; }

		public bool IsOnline { get; set; } = false;
	}
}
