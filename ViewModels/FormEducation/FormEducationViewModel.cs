using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels
{
	/* 
	Создание объекта "Форма обучения"	
	*/

	public class FormEducationViewModel
	{
		[Required(ErrorMessage = "Укажите название формы обучения")]
		public string Name { get; set; }

		public string Code { get; set; }
	}
}
