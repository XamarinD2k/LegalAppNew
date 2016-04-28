using System;
using System.ComponentModel;
using System.Collections.Generic;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace LegalApp
{
	public class ChangePasswordVM : INotifyPropertyChanged
	{
		public ChangePasswordVM ()
		{
			LoadPasswordValidate ();
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this,
					new PropertyChangedEventArgs (propertyName));
			}
		}

		#endregion

		private string _OldPassword = string.Empty;

		public string OldPassword {
			get { return _OldPassword; }
			set {
				_OldPassword = value;
				OnPropertyChanged ("OldPassword");
			}
		}

		private string _NewPassword = string.Empty;

		public string NewPassword {
			get { return _NewPassword; }
			set {
				_NewPassword = value;
				OnPropertyChanged ("NewPassword");
			}
		}

		private string _ConfirmPassword = string.Empty;

		public string ConfirmPassword {
			get { return _ConfirmPassword; }
			set {
				_ConfirmPassword = value;
				OnPropertyChanged ("ConfirmPassword");
			}
		}

		private List<PasswordValidationDetailsModel> _PasswordDetails = new List<PasswordValidationDetailsModel> ();

		public List<PasswordValidationDetailsModel> PasswordDetails {
			get { 
				return _PasswordDetails;
			}
			set {
				_PasswordDetails = value;
				OnPropertyChanged ("PasswordDetails");
			}
		}

		private Command resetCommand = null;
		public Command ResetCommand {
			get {
				return resetCommand ?? new Command (async delegate(object o) {
					if (ValidatePassword ()) {
						await ChangePassword ();
					} else {
						MessagingCenter.Send<ChangePasswordVM> (this, Strings.Display_Message);
					}
				});
			}
		}

		private bool ValidatePassword ()
		{
			foreach (PasswordValidationDetailsModel PVM in PasswordDetails) {
				string ValidationType = PVM.abbrivi;

				switch (ValidationType) {

				case "PWDLEN":
					{
						int minLength = Convert.ToInt16 (PVM.minvalue);
						int maxLength = Convert.ToInt16 (PVM.maxvalue);

						if (!(Convert.ToInt32 (NewPassword.Length) >= minLength && Convert.ToInt32 (NewPassword.Length) <= maxLength)) {
							GlobalVariables.DisplayMessage = "Password Length should be " + minLength + " to " + maxLength;
							return false;
						}
					}
					break;
				case "PWDNUM":
					{
						int minNumericValue = Convert.ToInt16 (PVM.minvalue);
						int maxNumericValue = Convert.ToInt16 (PVM.maxvalue);

						int counter = 0;
						char[] NewPasswordArray = NewPassword.ToCharArray ();
						foreach (char ch in NewPasswordArray) {
							if (char.IsDigit (ch)) {
								counter++;
							}
						} 
						if (!(counter >= minNumericValue && counter <= maxNumericValue)) {
							GlobalVariables.DisplayMessage = "Password should contain " + minNumericValue + " to " + maxNumericValue + " numeric values";
							return false;
						}
					}
					break;
				}

			}

			return true;

		}

		private async Task ChangePassword ()
		{
			if (NewPassword.Equals (ConfirmPassword)) {
				var result = await WebAPI.Instance.ChangePasswordHttpRequest (OldPassword, NewPassword);

				if (result != null) {
					var JsonResult = JObject.Parse (result);
					string ResultCode = JsonResult ["error_Code"].ToString ();
					if (ResultCode == "0") 
						MessagingCenter.Send<ChangePasswordVM> (this, Strings.ChangePassword_SUCCESS);
					else
						MessagingCenter.Send<ChangePasswordVM> (this, Strings.ChangePassword_FAILURE);

				}
			} else {
				GlobalVariables.DisplayMessage = "New Password and Confirm Password must be same";
				MessagingCenter.Send<ChangePasswordVM> (this, Strings.Display_Message);
			}

		}

		public async Task LoadPasswordValidate ()
		{
			var result = await WebAPI.Instance.PasswordValidationHttpRequest ();

			if (result != null) {
				var JsonResult = JObject.Parse (result);
				string ResultCode = JsonResult ["error_Code"].ToString ();
				if (ResultCode == "0") {

					var ResultJSON = JsonConvert.DeserializeObject<PasswordValidationModel> (result);

					PasswordDetails = ResultJSON.PwdValidation;
					//conditions
				}
			}
			//ActivityIndicator = false;
		}

		private Command _ClearCommand;
		public Command ClearCommand {
			get {
				return _ClearCommand ?? new Command (delegate(object o) {
					OldPassword = "";
					NewPassword = "";
					ConfirmPassword = "";
				});
			}
		}
	}
}

