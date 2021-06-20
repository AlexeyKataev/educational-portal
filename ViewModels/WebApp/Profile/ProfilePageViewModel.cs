using System.ComponentModel.DataAnnotations;
using Dotnet.ViewModels.WebApp.Account;

namespace Dotnet.ViewModels.WebApp.Profile
{
	public class ProfilePageViewModel
	{
		public ProfileViewModel ProfileViewModel { get; set; } = null;
		public EditPasswordViewModel EditPasswordViewModel { get; set; } = null;
	}
}