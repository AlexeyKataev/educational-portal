using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.Institution
{
    public class InstitutionViewModel
    {
		[Required(ErrorMessage = "Укажите название учебного заведения")]
		public string Name { get; set; }

		public string AddressId { get; set; }

		public string ContactInformationId { get; set; }
    }
}