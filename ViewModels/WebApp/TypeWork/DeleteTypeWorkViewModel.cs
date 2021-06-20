using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.TypeWork
{
    public class DeleteTypeWorkViewModel
    {
		[Required]
		public int Id { get; set; }
    }
}