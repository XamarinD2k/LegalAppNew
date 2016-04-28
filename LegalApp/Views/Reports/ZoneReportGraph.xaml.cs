using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Syncfusion.SfChart.XForms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LegalApp
{
	public partial class ZoneReportGraph : ContentPage
	{
		public ObservableCollection<ZoneDetailsReportModel> ZoneReportResponseList = null;
		public ObservableCollection<ChartDataPoint> NoOfAccount = null;
		public ObservableCollection<ChartDataPoint> Amount = null;

		public ZoneReportGraph ()
		{
			GlobalVariables.UserLevel = "HO";
			InitializeComponent ();

			ToolMenuList.Clicked += (object sender, EventArgs e) => {
				Navigation.PopToRootAsync ();
				Navigation.PushAsync (new ZoneReportPage ());
			};

			LoadZoneReport ();
		}


		public async Task LoadZoneReport ()
		{

			try {
				var result = await ReportRequestAPI.InstanceReportRequestAPI.GetReport ();

				if (result != null) {
					var JsonResult = JObject.Parse (result);
					string ResultCode = JsonResult ["error_Code"].ToString ();
					if (ResultCode == "0") {

						var ReportJSON = JsonConvert.DeserializeObject<ZoneReportModel> (result);
						ZoneReportResponseList = new ObservableCollection<ZoneDetailsReportModel> (ReportJSON.Report_Details);
					
					} else {
						await DisplayAlert (null, JsonResult ["error_msg"].ToString (), "Ok");
					}
				} else {
					
					await DisplayAlert (null, "Error... Please try again", "Ok");
				}
			} catch {
				
				await DisplayAlert (null, "Error... Please try again", "Ok");
			}


			if (ZoneReportResponseList != null) {
				LoadGraph ();
			}

		}

		void LoadGraph ()
		{
			try {
				Amount = new ObservableCollection<ChartDataPoint> ();
				NoOfAccount = new ObservableCollection<ChartDataPoint> ();

				foreach (ZoneDetailsReportModel z in ZoneReportResponseList) {

					Amount.Add (new ChartDataPoint (z.ZoneName, Convert.ToDouble (z.Amount)));
					NoOfAccount.Add (new ChartDataPoint (z.ZoneName, Convert.ToDouble (z.NoofAccounts)));
				}

				NumericalAxis amountYaxis = new NumericalAxis ();
				//amountYaxis.OpposedPosition = true;
				amountYaxis.ShowMajorGridLines = true;
				amountYaxis.Title = new ChartAxisTitle (){ Text = "Amount in Lakhs" };

				sfChart.Series.Add (new ColumnSeries () {
					ItemsSource = Amount,
					Label = "Amount (in Lakhs)",
					YAxis = amountYaxis
//					XBindingPath = "XValue",
//					YBindingPath = "YValue"
				});



				NumericalAxis NoOfAccountYaxis = new NumericalAxis ();
				NoOfAccountYaxis.OpposedPosition = true;
				NoOfAccountYaxis.ShowMajorGridLines = false;
				NoOfAccountYaxis.Title = new ChartAxisTitle (){ Text = "No. of Account" };

				sfChart.Series.Add (new LineSeries () {
					ItemsSource = NoOfAccount,
					Label = "No. Of Account",
					YAxis = NoOfAccountYaxis
//					XBindingPath = "XValue",
//					YBindingPath = "YValue"
				});

				sfChart.Series [0].DataMarker = new ChartDataMarker ();
				sfChart.Series [0].DataMarker.ShowLabel = true;
				sfChart.Series [0].DataMarker.LabelStyle.LabelPosition = DataMarkerLabelPosition.Auto;


				sfChart.Series [1].DataMarker = new ChartDataMarker ();
				sfChart.Series [1].DataMarker.ShowLabel = true;
				sfChart.Series [1].DataMarker.LabelStyle.LabelPosition = DataMarkerLabelPosition.Auto;
			} catch {
			}
		}


	}


}

