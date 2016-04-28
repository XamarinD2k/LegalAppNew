using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LegalApp
{
	public class CustomerReportVM : INotifyPropertyChanged
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

		public CustomerReportVM ()
		{
			LoadCustomerReport ();
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

		private ObservableCollection<CustomerDetailsReportModel> _CustomerReportResponseList; 
		public ObservableCollection<CustomerDetailsReportModel> CustomerReportResponseList
		{
			get { return _CustomerReportResponseList; }
			set {
				_CustomerReportResponseList = value;
				OnPropertyChanged ("CustomerReportResponseList");
			}
		}

		private ObservableCollection<TotalDetailsModel> _CustomerReportTotalList; 
		public ObservableCollection<TotalDetailsModel> CustomerReportTotalList
		{
			get { return _CustomerReportTotalList; }
			set {
				_CustomerReportTotalList = value;
				OnPropertyChanged ("CustomerReportTotalList");
			}
		}

		private async Task LoadCustomerReport ()
		{
			ActivityIndicator = true;
			ShowReport = false;
			try {
				var result = await ReportRequestAPI.InstanceReportRequestAPI.GetReport ();


				if (result != null) {
					var JsonResult = JObject.Parse (result);
					string ResultCode = JsonResult ["error_Code"].ToString ();
					if (ResultCode == "0") {

						var ReportJSON = JsonConvert.DeserializeObject<CustomerReportModel> (result);
						CustomerReportResponseList = new ObservableCollection<CustomerDetailsReportModel> (ReportJSON.Report_Details);
						CustomerReportTotalList = new ObservableCollection<TotalDetailsModel> (ReportJSON.TotalDetails);
						ShowReport = true;
					} else {
						GlobalVariables.DisplayMessage = JsonResult ["error_msg"].ToString ();
						MessagingCenter.Send<CustomerReportVM> (this, Strings.Display_Message);
					}
				} else {
					GlobalVariables.DisplayMessage = "Error... Please try again";
					MessagingCenter.Send<CustomerReportVM> (this, Strings.Display_Message);
				}
			} catch {
				GlobalVariables.DisplayMessage = "Error... Please try again";
				MessagingCenter.Send<CustomerReportVM> (this, Strings.Display_Message);
			}

			ActivityIndicator = false;
		}
	}
}

