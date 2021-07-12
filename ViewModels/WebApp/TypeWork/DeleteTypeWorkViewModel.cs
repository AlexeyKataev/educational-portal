using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.WebApp.TypeWork
{
    public class DeleteTypeWorkViewModel
    {
		[Required]
		public long Id { get; set; }
    }
}