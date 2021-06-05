using System.ComponentModel.DataAnnotations;
 
namespace Dotnet.ViewModels.TypeWork
{
    public class DeleteTypeWorkViewModel
    {
		[Required]
		public int Id { get; set; }
    }
}