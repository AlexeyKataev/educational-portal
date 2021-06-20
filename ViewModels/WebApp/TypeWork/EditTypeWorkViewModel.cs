using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.TypeWork
{
    public class EditTypeWorkViewModel
    {
		[Required]
		public int Id { get; set; }

		[Required(ErrorMessage = "Укажите название типа работ")]
		public string Name { get; set; }
    }
}