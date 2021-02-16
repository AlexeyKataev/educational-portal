using System.ComponentModel.DataAnnotations;

/* Изменение "Форма обучения" */

namespace Dotnet.ViewModels
{
	public class EditFormEducationViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите название формы обучения")]
		public string Name { get; set; }

		public string Code { get; set; }
	}
}
