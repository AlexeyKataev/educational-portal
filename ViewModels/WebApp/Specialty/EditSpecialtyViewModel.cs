using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Specialty
{
	public class EditSpecialtyViewModel
	{
		[Required]
		public ulong Id { get; set; }

		[Required(ErrorMessage = "Укажите название специальности.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите код специальности.")]
		public string Code { get; set; }
		


		[Required(ErrorMessage = "Укажите факультет.")]
		public ulong FacultyId { get; set; }
	}
}
