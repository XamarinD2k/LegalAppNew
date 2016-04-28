using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class ChangePassword : ContentPage
	{
		public ChangePassword ()
		{
			InitializeComponent ();
		}
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<ChangePasswordVM> (this, Strings.ChangePassword_SUCCESS, async (ChangePasswordVM sender) => {
				{
					await  DisplayAlert(null,Strings.ChangePassword_SUCCESS,"OK");
				}
			});

			MessagingCenter.Subscribe<ChangePasswordVM> (this, Strings.ChangePassword_FAILURE, async (ChangePasswordVM sender) => {
				{
					await	DisplayAlert("Error",Strings.ChangePassword_FAILURE,"OK");
				}
			});

			MessagingCenter.Subscribe<ChangePasswordVM> (this, Strings.Display_Message, (ChangePasswordVM sender) => {
				DisplayAlert(null, GlobalVariables.DisplayMessage, "Ok");
			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<ChangePasswordVM> (this, Strings.ChangePassword_SUCCESS);
			MessagingCenter.Unsubscribe<ChangePasswordVM> (this, Strings.ChangePassword_FAILURE);
			MessagingCenter.Unsubscribe<ChangePasswordVM> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

