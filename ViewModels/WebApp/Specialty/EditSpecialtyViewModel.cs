using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Specialty
{
	public class EditSpecialtyViewModel
	{
		[Required]
		public long Id { get; set; }

		[Required(ErrorMessage = "Укажите название специальности.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите код специальности.")]
		public string Code { get; set; }
		


		[Required(ErrorMessage = "Укажите факультет.")]
		public long FacultyId { get; set; }
	}
}
