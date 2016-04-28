using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace LegalApp
{
	public class VMLogin : INotifyPropertyChanged
	{
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

		private static string userid = string.Empty;// = "UserName";
		private static string pass = string.Empty;//= "PassWord";

		public  string username { 
			get { return userid; }
			set { 
				userid = value;
				OnPropertyChanged ("username");
			} 
		}

		public  string password { 
			get { return pass; }
			set { 
				pass = value;
				OnPropertyChanged ("password");
			} 
		}

		private string authKey;
		public string AuthKey {
			get {
				return authKey;
			}
			private set {
				authKey = value;

				//TODO: Write code here to save the new Auth key into OS Storage

				OnPropertyChanged ("AuthKey");
			}
		}

		private Command loginCommand = null;
		public Command LoginCommand {
			get {
				return loginCommand ?? new Command (async delegate(object o) {
					
					var authenticationResult = await WebAPI.Instance.Login(new LoginModel()
						{
							UserName = username,
							Password = password
						});


					if(authenticationResult!=null)
					{
						if(authenticationResult.error_Code == "0")
						{

							var sessionManager = DependencyService.Get<ISessionManager>();
							if(sessionManager != null)
							{
								sessionManager.StoreSessionData(Strings.UserID,authenticationResult.adv_Id);
								sessionManager.StoreSessionData(Strings.SESSION_ID_KEY,authenticationResult.session_Id);
								sessionManager.StoreSessionData(Strings.UserName,authenticationResult.adv_Name);
							}

							MessagingCenter.Send<VMLogin>(this, Strings.AUTHENTICATION_SUCCESS);
						}
						else
						{
							this.AuthKey = string.Empty;
							MessagingCenter.Send<VMLogin>(this, Strings.AUTHENTICATION_FAILED);
						}
					}
					else
					{
						this.AuthKey = string.Empty;
						MessagingCenter.Send<VMLogin>(this, Strings.AUTHENTICATION_FAILED);
					}

				});
			}
		}

		public async Task Logout()
		{
			
			var result = await WebAPI.Instance.HttpRequest<object> (WebAPI.Instance.webLinkCaseDetails + "?adv_id=" + WebAPI.Instance.UserID.ToEncrypt () +
				"&session=" + WebAPI.Instance.SessionID.ToEncrypt());

			if (result != null) {
				//CaseName = result.case_list.Count.ToString();



				MessagingCenter.Send<VMLogin>(this, Strings.AUTHENTICATION_SUCCESS);

			} 

		}

		private Command logoutCommand = null;
		public Command LogoutCommand {
			get {
				return logoutCommand ?? new Command (async delegate(object o) {
					await Logout();
				});
			}
		}
	}
}

