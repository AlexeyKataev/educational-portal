using System.ComponentModel.DataAnnotations;

/* Объект "специальность" */

namespace Dotnet.ViewModels
{
	public class SpecialtyViewModel
	{
		[Required(ErrorMessage = "Укажите название специальности")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Укажите код специальности")]
		public string Code { get; set; }
		


		[Required(ErrorMessage = "Укажите факультет")]
		public int FacultyId { get; set; }
	}
}
