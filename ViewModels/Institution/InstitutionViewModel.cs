using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels
{
    public class InstitutionViewModel
    {
		[Required(ErrorMessage = "Укажите название учебного заведения")]
		public string Name { get; set; }

		public int AddressId { get; set; }

		public int ContactInformationId { get; set; }
    }
}