using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.TypeWork
{
    public class TypeWorkViewModel
    {
		[Required(ErrorMessage = "Укажите название типа задания")]
		public string Name { get; set; }
    }
}