using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Dotnet.ViewModels.Institution
{
	public class EditInstitutionViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите название учебного заведения.")]
		public string Name { get; set; }

		public string AddressId { get; set; }
		
		public string ContactInformationId { get; set; }	
	}
}
