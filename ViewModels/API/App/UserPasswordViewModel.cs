using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dotnet.ViewModels.API
{
	public class UserPasswordViewModel
    {
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private string oldPassword = "";
        public string OldPassword
        {
            get => oldPassword;
            set
            {
                oldPassword = value;
            }
        }

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private string newPassword = "";
        public string NewPassword
        {
            get => newPassword;
            set
            {
                if (value.Length > 1 && value != " ") newPassword = value;
            }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private string newPasswordRepeat = "";
        public string NewPasswordRepeat
        {
            get => newPasswordRepeat;
            set
            {
                if (value.Length > 1 && value != " ") newPasswordRepeat = value;
            }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool IsValidValues
        {
            get
            {
                if (NewPassword == NewPasswordRepeat && OldPassword != null && NewPassword != null && newPasswordRepeat != null) 
                    return true;
                else return false;
            }
        }
    }
}