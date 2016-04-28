using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace LegalApp
{
	public class ReportRequestAPI
	{
		private static ReportRequestAPI _instance = null;
		private static object lockThis = new object ();
		private ReportRequestModel reportRequestModel=null;
		private ReportRequestAPI ()
		{
			reportRequestModel = new ReportRequestModel ();
		}

		public static ReportRequestAPI InstanceReportRequestAPI {
			get {
				lock (lockThis) {
					if (_instance == null)
						_instance = new ReportRequestAPI ();
				}

				return _instance;
			}
		}


		public async Task<ReportListModel> GetReportList()
		{
			GetReportRequestModel ();
			string URL = "userid=" + reportRequestModel.userid.ToEncrypt ()
			             + "&session=" + reportRequestModel.session.ToEncrypt ();

			return await WebAPI.Instance.ReportListHttpRequest (URL);
		}



		private void GetReportRequestModel()
		{
			var sessionManager = DependencyService.Get<ISessionManager> ();
			reportRequestModel.userid = sessionManager.GetSessionData (Strings.LoginID);
			reportRequestModel.session = sessionManager.GetSessionData (Strings.SESSION_ID_KEY);
			reportRequestModel.UserLevel = GlobalVariables.UserLevel;
			reportRequestModel.ReportId = GlobalVariables.ReportId;
			reportRequestModel.ReportType = GlobalVariables.ReportType;
			reportRequestModel.Client = GlobalVariables.Client;
			reportRequestModel.BranchCode = GlobalVariables.BranchKey;
			reportRequestModel.ListReport = GlobalVariables.ListReport;
			reportRequestModel.ZoneAlt_Key = GlobalVariables.ZoneKey;
			reportRequestModel.RegionAlt_Key = GlobalVariables.RegionKey;
		}

		public async Task<string> GetReport()
		{
			GetReportRequestModel ();

			string URL = "userid=" + reportRequestModel.userid.ToEncrypt ()
			             + "&session=" + reportRequestModel.session.ToEncrypt ()
			             + "&ReportId=" + reportRequestModel.ReportId.ToEncrypt ()
			             + "&UserLevel=" + reportRequestModel.UserLevel.ToEncrypt ()
			             + "&ReportType=" + reportRequestModel.ReportType.ToEncrypt ()
			             + "&Client=" + reportRequestModel.Client.ToEncrypt ()
			             + "&BranchCode=" + reportRequestModel.BranchCode.ToEncrypt ()
			             + "&ListReport=" + reportRequestModel.ListReport.ToEncrypt ()
			             + "&ZoneAlt_Key=" + reportRequestModel.ZoneAlt_Key.ToEncrypt ()
			             + "&RegionAlt_Key=" + reportRequestModel.RegionAlt_Key.ToEncrypt ();

			return await WebAPI.Instance.ReportHttpRequest (URL);
		}


	}
}

