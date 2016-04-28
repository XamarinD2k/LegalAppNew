using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class BranchReportPage : ContentPage
	{
		public BranchReportPage ()
		{
			GlobalVariables.UserLevel = "ZO";
			InitializeComponent ();

			listBranchReport.ItemTapped += (object sender, ItemTappedEventArgs e) => {
				GlobalVariables.BranchKey =  ((BranchDetailsReportModel)listBranchReport.SelectedItem).BranchCode;
				Navigation.PushAsync(new CustomerReportPage());
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<BranchReportVM> (this, Strings.Display_Message, (BranchReportVM sender) => {
				DisplayAlert(null, GlobalVariables.DisplayMessage, "Ok");
			});
		}

		protected override void OnDisappearing ()
		{

			MessagingCenter.Unsubscribe<BranchReportVM> (this, Strings.Display_Message);
			base.OnDisappearing ();
		}
	}
}

