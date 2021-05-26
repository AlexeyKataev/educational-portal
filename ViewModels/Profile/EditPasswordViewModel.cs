using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.Profile
{
	public class EditPasswordViewModel
	{
		[Required]
		public string Password { get; set; }
		[Required]
		public string Password2 { get; set; }
	}
}