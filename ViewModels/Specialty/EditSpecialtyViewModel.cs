using System.ComponentModel.DataAnnotations;

/* Редактирование специальности */

namespace Dotnet.ViewModels
{
	public class EditSpecialtyViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите название специальности")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите код специальности")]
		public string Code { get; set; }
		


		[Required(ErrorMessage = "Укажите факультет")]
		public int FacultyId { get; set; }
	}
}
