using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Syncfusion.SfChart.XForms;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LegalApp
{
	public class ZoneReportVM : INotifyPropertyChanged
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

		public  ZoneReportVM ()
		{
			LoadZoneReport ();

		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;


				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private bool _ShowReport = false;

		public bool ShowReport {
			get { return _ShowReport; }
			set {
				_ShowReport = value;
				OnPropertyChanged ("ShowReport");
			}
		}

	
		private ObservableCollection<ZoneDetailsReportModel> _ZoneReportResponseList;

		public ObservableCollection<ZoneDetailsReportModel> ZoneReportResponseList {
			get { return _ZoneReportResponseList; }
			set {
				_ZoneReportResponseList = value;
				OnPropertyChanged ("ZoneReportResponseList");
			}
		}

		private ObservableCollection<TotalDetailsModel> _ZoneReportTotalList;

		public ObservableCollection<TotalDetailsModel> ZoneReportTotalList {
			get { return _ZoneReportTotalList; }
			set {
				_ZoneReportTotalList = value;
				OnPropertyChanged ("ZoneReportTotalList");
			}
		}



		public async Task LoadZoneReport ()
		{
			
			await loadListReport ();
		}


		private async Task loadListReport ()
		{
			ActivityIndicator = true;
			ShowReport = false;
			try {
				var result = await ReportRequestAPI.InstanceReportRequestAPI.GetReport ();


				if (result != null) {
					var JsonResult = JObject.Parse (result);
					string ResultCode = JsonResult ["error_Code"].ToString ();
					if (ResultCode == "0") {

						var ReportJSON = JsonConvert.DeserializeObject<ZoneReportModel> (result);
						ZoneReportResponseList = new ObservableCollection<ZoneDetailsReportModel> (ReportJSON.Report_Details);
						ZoneReportTotalList = new ObservableCollection<TotalDetailsModel> (ReportJSON.TotalDetails);
						ShowReport = true;
					} else {
						GlobalVariables.DisplayMessage = JsonResult ["error_msg"].ToString ();
						MessagingCenter.Send<ZoneReportVM> (this, Strings.Display_Message);
					}
				} else {
					GlobalVariables.DisplayMessage = "Error... Please try again";
					MessagingCenter.Send<ZoneReportVM> (this, Strings.Display_Message);
				}
			} catch {
				GlobalVariables.DisplayMessage = "Error... Please try again";
				MessagingCenter.Send<ZoneReportVM> (this, Strings.Display_Message);
			}

			ActivityIndicator = false;
		}


	}
}

