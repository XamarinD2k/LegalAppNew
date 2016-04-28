using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class ZoneReportPage : ContentPage
	{
		public ZoneReportPage ()
		{
			GlobalVariables.UserLevel = "HO";
			InitializeComponent ();

			listZoneReport.ItemTapped += (object sender, ItemTappedEventArgs e) => {


				GlobalVariables.ZoneKey = ((ZoneDetailsReportModel)listZoneReport.SelectedItem).ZoneAlt_Key;
				Navigation.PushAsync (new BranchReportPage ());
			};

			ToolMenuGraph.Clicked += (object sender, EventArgs e) => {
				Navigation.PopToRootAsync();
				Navigation.PushAsync(new ZoneReportGraph());
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<ZoneReportVM> (this, Strings.Display_Message, (ZoneReportVM sender) => {
				DisplayAlert(null, GlobalVariables.DisplayMessage, "Ok");
			});
		}

		protected override void OnDisappearing ()
		{

			MessagingCenter.Unsubscribe<ZoneReportVM> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

