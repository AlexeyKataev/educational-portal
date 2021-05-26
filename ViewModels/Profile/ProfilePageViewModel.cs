using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels.Profile
{
	public class ProfilePageViewModel
	{
		public ProfileViewModel ProfileViewModel { get; set; } = null;
		public EditPasswordViewModel EditPasswordViewModel { get; set; } = null;
	}
}