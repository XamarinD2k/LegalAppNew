using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LegalApp
{
	public class BranchReportVM : INotifyPropertyChanged
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

		public BranchReportVM ()
		{
			LoadBranchReport ();
		}

		private bool _ActivityIndicator =false;
		public bool ActivityIndicator
		{
			get { return _ActivityIndicator; }
			set{
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private bool _ShowReport =false;
		public bool ShowReport
		{
			get { return _ShowReport; }
			set{
				_ShowReport = value;
				OnPropertyChanged ("ShowReport");
			}
		}

		private ObservableCollection<BranchDetailsReportModel> _BranchReportResponseList; 
		public ObservableCollection<BranchDetailsReportModel> BranchReportResponseList
		{
			get { return _BranchReportResponseList; }
			set {
				_BranchReportResponseList = value;
				OnPropertyChanged ("BranchReportResponseList");
			}
		}

		private ObservableCollection<TotalDetailsModel> _BranchReportTotalList; 
		public ObservableCollection<TotalDetailsModel> BranchReportTotalList
		{
			get { return _BranchReportTotalList; }
			set {
				_BranchReportTotalList = value;
				OnPropertyChanged ("BranchReportTotalList");
			}
		}

//		public async Task LoadBranchReport()
//		{
//			ActivityIndicator = true;
//			ReportRequestModel reportRequestModel = new ReportRequestModel ();
//			reportRequestModel.userid = "LEGAL";
//			reportRequestModel.session = "865020472";
//			reportRequestModel.UserLevel = "ZO";
//			reportRequestModel.ReportId = "LEGAL-01";
//			reportRequestModel.ReportType = "1";
//			reportRequestModel.Client = "BOM";
//			reportRequestModel.BranchCode = "999";
//			reportRequestModel.ListReport = "N";
//			reportRequestModel.ZoneAlt_Key = GlobalVariables.ZoneKey;
//			reportRequestModel.RegionAlt_Key = "21";
//
//			var result = await WebAPI.Instance.BranchReportHttpRequest (reportRequestModel);
//			if (result != null) {
//				BranchReportResponseList = new ObservableCollection<BranchDetailsReportModel> (result.Report_Details);
//				BranchReportTotalList = new ObservableCollection<TotalDetailsModel> (result.TotalDetails);
//			}
//			ActivityIndicator = false;
//		}

		private async Task LoadBranchReport ()
		{
			ActivityIndicator = true;
			ShowReport = false;
			try {
				var result = await ReportRequestAPI.InstanceReportRequestAPI.GetReport ();


				if (result != null) {
					var JsonResult = JObject.Parse (result);
					string ResultCode = JsonResult ["error_Code"].ToString ();
					if (ResultCode == "0") {

						var ReportJSON = JsonConvert.DeserializeObject<BranchReportModel> (result);
						BranchReportResponseList = new ObservableCollection<BranchDetailsReportModel> (ReportJSON.Report_Details);
						BranchReportTotalList = new ObservableCollection<TotalDetailsModel> (ReportJSON.TotalDetails);
						ShowReport = true;
					} else {
						GlobalVariables.DisplayMessage = JsonResult ["error_msg"].ToString ();
						MessagingCenter.Send<BranchReportVM> (this, Strings.Display_Message);
					}
				} else {
					GlobalVariables.DisplayMessage = "Error... Please try again";
					MessagingCenter.Send<BranchReportVM> (this, Strings.Display_Message);
				}
			} catch {
				GlobalVariables.DisplayMessage = "Error... Please try again";
				MessagingCenter.Send<BranchReportVM> (this, Strings.Display_Message);
			}

			ActivityIndicator = false;
		}
	}
}

