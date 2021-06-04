using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.FormEducation
{
	public class FormEducationViewModel
	{
		[Required(ErrorMessage = "Укажите название формы обучения")]
		public string Name { get; set; }

		public string Code { get; set; }
	}
}
