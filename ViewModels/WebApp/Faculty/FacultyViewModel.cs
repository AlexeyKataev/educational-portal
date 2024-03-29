using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.WebApp.Faculty
{
	public class FacultyViewModel
	{
		[Required(ErrorMessage = "Укажите название факультета")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите код факультета")]
		public string Code { get; set; }
		
		public string About { get; set; }


		[Required(ErrorMessage = "Укажите учебное заведение")]
		public long InstitutionId { get; set; }
	}
}
