using System.ComponentModel.DataAnnotations;

/* Редактирование факультета */

namespace Dotnet.ViewModels
{
	public class EditFacultyViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите название факультета")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите номер факультета")]
		public string Code { get; set; }
		
		public string About { get; set; }


		[Required(ErrorMessage = "Укажите учебное заведение")]
		public int InstitutionId { get; set; }
	}
}