using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class CaseDetails : ContentPage
	{
		public CaseDetails ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			MessagingCenter.Subscribe<CaseDetailsVM> (this, Strings.AUTHENTICATION_SUCCESS, (CaseDetailsVM sender) => {
				CaseDetailsVM vm = BindingContext as CaseDetailsVM;
				if (vm != null) {
						
					try {
						vm.HANDingDocTAdvDt = vm.CaseDetails.RestDataDetails[0].HANDingDocTAdvDt.Split(' ')[0];
					}
					catch{ vm.HANDingDocTAdvDt=""; }

					try{
						vm.AcceptanceDt= vm.CaseDetails.RestDataDetails[0].AcceptanceDt.Split(' ')[0];
					}
					catch{
						vm.AcceptanceDt ="";
					}

					try{
						vm.FilPlaintInCourtDt = vm.CaseDetails.RestDataDetails[0].FilPlaintInCourtDt.Split(' ')[0];
					}
					catch{
						vm.FilPlaintInCourtDt="";
					}

						this.pickerCurrentState.Items.Clear();
						foreach (var currentValue in vm.CaseDetails.CurrentStage)
						{
							pickerCurrentState.Items.Add(currentValue.Description);
						}
						pickerCurrentState.SelectedIndex = 0;
				

						this.pickerAppeal.Items.Clear();
						foreach (var currentValue in vm.CaseDetails.Appeal)
						{
							pickerAppeal.Items.Add(currentValue.Description);
						}
						pickerAppeal.SelectedIndex = 0;
						
						this.pickerAdjournetReason.Items.Clear();
						foreach (var currentValue in vm.CaseDetails.DimLegalAdjournmentReason)
						{
							pickerAdjournetReason.Items.Add(currentValue.Description);
						}
						pickerAdjournetReason.SelectedIndex = 0;
						
						this.pickerAdjournetSoughtBy.Items.Clear();
						foreach (var currentValue in vm.CaseDetails.DimLegalAdjournment)
						{
							pickerAdjournetSoughtBy.Items.Add(currentValue.Description);
						}
						pickerAdjournetSoughtBy.SelectedIndex = 0;
						
						this.pickerNextState.Items.Clear();
						foreach (var currentValue in vm.CaseDetails.NextStage)
						{
							pickerNextState.Items.Add(currentValue.Description);
						}
						pickerNextState.SelectedIndex = 0;
						
						this.pickerInterimOrders.Items.Clear();
						foreach (var currentValue in vm.CaseDetails.AddInterimOrder)
						{
							pickerInterimOrders.Items.Add(currentValue.Description);
						}
						pickerInterimOrders.SelectedIndex = 0;


				}
			});


			MessagingCenter.Subscribe<CaseDetailsVM> (this, Strings.CaseDetails_SUCCESS, async (CaseDetailsVM sender) => {
				{
					await	this.Navigation.PopAsync();
				}
			});

			MessagingCenter.Subscribe<CaseDetailsVM> (this, Strings.CaseDetails_FAILURE, async (CaseDetailsVM sender) => {
				{
					await	DisplayAlert("Error",Strings.CaseDetails_FAILURE,"OK");
				}
			});
		}

		protected override void OnDisappearing ()
		{
			MessagingCenter.Unsubscribe<CaseDetailsVM> (this, Strings.CaseDetails_SUCCESS);
			MessagingCenter.Unsubscribe<CaseDetailsVM> (this, Strings.CaseDetails_FAILURE);
			base.OnDisappearing ();
		}
	}
}

