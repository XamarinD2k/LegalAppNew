using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class Login : ContentPage
	{
		public Login ()
		{

			var sessionManager = DependencyService.Get<ISessionManager> ();
			if (sessionManager != null) {
				sessionManager.StoreSessionData (Strings.UserID, string.Empty);
				sessionManager.StoreSessionData (Strings.SESSION_ID_KEY, string.Empty);
				sessionManager.StoreSessionData (Strings.UserName, string.Empty);
				sessionManager.StoreSessionData (Strings.UserLevel, string.Empty);
				sessionManager.StoreSessionData (Strings.UserLocationCode, string.Empty);
				sessionManager.StoreSessionData (Strings.LoginID, string.Empty);
			}

			InitializeComponent ();

			txtUserName.Text = "";
			txtPassword.Text = "";

		}

		/// <summary>
		/// Raises the appearing event.
		/// </summary>
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<VMLogin> (this, Strings.AUTHENTICATION_FAILED, (VMLogin sender) => {
				 DisplayAlert("Incorrect Credentials", "You were not authenticated", "Ok");
			});
		}

		/// <summary>
		/// Raises the disappearing event.
		/// </summary>
		protected override void OnDisappearing ()
		{
			
			MessagingCenter.Unsubscribe<VMLogin> (this, Strings.AUTHENTICATION_FAILED);
			base.OnDisappearing ();
		}

		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async() => {
				var result = await this.DisplayAlert("Alert!", "Do you want to exit?", "Yes", "No");
				if (result)
				{

					var sessionManager = DependencyService.Get<ISessionManager>();

					sessionManager.Exit();
					 // or anything else

				}
			});

			return true;
		}

		public async void OnLogin(object sender, EventArgs e)
		{

			var networkStatus = DependencyService.Get<INetworkConnection> ();

		    networkStatus.CheckNetworkConnection ();
			if (networkStatus.IsConnected) {
				var authenticationResult = await WebAPI.Instance.Login (new LoginModel () {
					UserName = txtUserName.Text,
					Password = txtPassword.Text
				});


				if (authenticationResult != null) {
					if (authenticationResult.error_Code == "0") {

						var sessionManager = DependencyService.Get<ISessionManager> ();
						if (sessionManager != null) {
							sessionManager.StoreSessionData (Strings.UserID, authenticationResult.adv_Id);
							sessionManager.StoreSessionData (Strings.SESSION_ID_KEY, authenticationResult.session_Id);
							sessionManager.StoreSessionData (Strings.UserName, authenticationResult.adv_Name);
							sessionManager.StoreSessionData (Strings.UserLevel, authenticationResult.UserLevel);
							sessionManager.StoreSessionData (Strings.UserLocationCode, authenticationResult.UserLocationCode);
							sessionManager.StoreSessionData (Strings.LoginID, txtUserName.Text);


						}

						Application.Current.MainPage = new MenuPage ();
					} else {
					
						await DisplayAlert ("Incorrect Credentials", "You were not authenticated", "Ok");
					}
				} else {
				
					await DisplayAlert ("Incorrect Credentials", "You were not authenticated", "Ok");
				}
			} else {
				await DisplayAlert ("Network Failure", "Please check your internet connection", "Ok");
			}
		}

	}
}

