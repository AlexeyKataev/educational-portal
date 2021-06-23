using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Faculty
{
	public class EditFacultyViewModel
	{
		[Required]
		public ulong Id { get; set; }

		[Required(ErrorMessage = "Укажите название факультета")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите номер факультета")]
		public string Code { get; set; }
		
		public string About { get; set; }


		[Required(ErrorMessage = "Укажите учебное заведение")]
		public ulong InstitutionId { get; set; }
	}
}
