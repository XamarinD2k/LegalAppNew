using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class MenuPage : MasterDetailPage
	{
		public MenuPage ()
		{
			InitializeComponent ();
			MasterPage.ListViewMainMenu.ItemSelected += OnItemSelected;
			MasterPage.ListViewReportSubMenu.ItemSelected += OnItemSelected;

			if (Device.OS == TargetPlatform.Android) 
			{
				Master.Icon = "menu.png";
			}

		}

		private	async void Logout()
		{
			var result = await this.DisplayAlert("Alert!", "Do you want to logout?", "Yes", "No");
			if (result)
			{
				Application.Current.MainPage = new Login ();
			}
		}

		void  OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			ListView LV = (ListView)sender;


			var item = e.SelectedItem as MasterPageItem;

			if(item!=null)
			{
				
				if (item.Title.ToUpper () == "Logout".ToUpper ()) {
					//MasterPage.ListViewMainMenu.SelectedItem = null;
					IsPresented = false;
					Logout ();

				} else {
					if (item.hasSubMenu) {
						MasterPage.ListViewReportSubMenu.IsVisible = !MasterPage.ListViewReportSubMenu.IsVisible;
						//LV.SelectedItem = null;
					} else {
						GlobalVariables.ReportId = string.IsNullOrEmpty (item.ReportID) ? string.Empty : item.ReportID;
						var sessionManager = DependencyService.Get<ISessionManager> ();
						GlobalVariables.UserLevel = sessionManager.GetSessionData (Strings.UserLevel);
						Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
						//LV.SelectedItem = null;
						IsPresented = false;
					}
				}

				LV.SelectedItem = null;
			}

		}

	}
}

