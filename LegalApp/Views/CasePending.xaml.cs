using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class CasePending : ContentPage
	{
			
		public CasePending ()
		{
			InitializeComponent ();
			UserName.Text = "Name : " + WebAPI.Instance.LoginUser;

			//LoadData ();
		}

		protected  override void OnAppearing ()
		{
			base.OnAppearing ();
		}

		public void OnCaseSelect(object sender, EventArgs e)
		{
			CasePendingModel C = (CasePendingModel)listCheckPending.SelectedItem;

			var sessionObj =	DependencyService.Get<ISessionManager> ();

			sessionObj.StoreSessionData ("customerName", C.CustomerName);
			sessionObj.StoreSessionData ("case_id", C.case_id);
			sessionObj.StoreSessionData ("BranchCode", C.BranchCode);

//			CaseDetailsParametes.customerName = C.CustomerName;
//			CaseDetailsParametes.case_id = C.case_id;
//			CaseDetailsParametes.BranchCode = C.BranchCode;

			this.Navigation.PushAsync(new CaseDetails());
		}
		/// <summary>
		/// Raises the disappearing event.
		/// </summary>
		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
		}

		protected override bool OnBackButtonPressed()
		{
			Device.BeginInvokeOnMainThread(async() => {
				var result = await this.DisplayAlert("Confirm Message", "Are you sure want to exit the app?", "Yes", "No");
				if (result)
				{

					var sessionManager = DependencyService.Get<ISessionManager>();

					sessionManager.Exit();
					// or anything else

				}
			});

			return true;
		}
	}
}

