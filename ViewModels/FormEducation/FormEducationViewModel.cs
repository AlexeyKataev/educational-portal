using System.ComponentModel.DataAnnotations;

/* Объект "Форма обучения" */

namespace Dotnet.ViewModels
{
	public class FormEducationViewModel
	{
		[Required(ErrorMessage = "Укажите название формы обучения")]
		public string Name { get; set; }

		public string Code { get; set; }
	}
}
