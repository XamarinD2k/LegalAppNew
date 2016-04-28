using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using ModernHttpClient;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LegalApp
{
	public class WebAPI
	{
		private string webLinkHost = "http://103.1.114.64:8011/LegalApps1";

		private HttpClient _client;

		public readonly string webLinkLogin;
		public readonly string webLinkCasePending;
		public readonly string webLinkCaseDetails;
		public readonly string webLinkCaseDetailsUpdate;
		public readonly string webLinkReport;
		public readonly string webLinkReportList;
		public readonly string webLinkChangePassword;
		public readonly string webLinkPasswordValidation;

		private string _UserID = string.Empty;

		public string UserID {
			get {
				if (string.IsNullOrEmpty (_UserID)) {
					var sessionManager = DependencyService.Get<ISessionManager> ();
					if (sessionManager != null) {
						_UserID = sessionManager.GetSessionData (Strings.UserID);
					}
				}
				return _UserID;
			}
			set {
				_UserID = value;

			}
		}

		private string _SessionID = string.Empty;

		public string SessionID {
			get {
				if (string.IsNullOrEmpty (_SessionID)) {
					var sessionManager = DependencyService.Get<ISessionManager> ();
					if (sessionManager != null) {
						_SessionID = sessionManager.GetSessionData (Strings.SESSION_ID_KEY);
					}
				}
				return _SessionID;
			}
			set {
				_SessionID = value;

			}
		}

		private string _LoginUser = string.Empty;

		public string LoginUser {
			get {
				if (string.IsNullOrEmpty (_LoginUser)) {
					var sessionManager = DependencyService.Get<ISessionManager> ();
					if (sessionManager != null) {
						_LoginUser = sessionManager.GetSessionData (Strings.UserName);
					}
				}
				return _LoginUser;
			}
			set {
				_LoginUser = value;

			}
		}

		private static WebAPI _instance = null;

		private WebAPI ()
		{
			webLinkLogin = webLinkHost + "/login.aspx";
			webLinkCasePending = webLinkHost + "/cases.aspx";
			webLinkCaseDetails = webLinkHost + "/case_details.aspx";
			webLinkCaseDetailsUpdate = webLinkHost + "/case_update.aspx";
			webLinkReport = webLinkHost + "/StaticReport.aspx";
			webLinkReportList = webLinkHost + "/ReportsLists.aspx";
			webLinkChangePassword = webLinkHost + "/PasswordChange.aspx";
			webLinkPasswordValidation = webLinkHost + "/PasswordValidation.aspx";

			_client = new HttpClient (new NativeMessageHandler (){ DisableCaching = true });
		}

		private static object lockThis = new object ();

		public static WebAPI Instance {
			get {
				lock (lockThis) {
					if (_instance == null)
						_instance = new WebAPI ();
				}

				return _instance;
			}
		}

		/// <summary>
		/// Https the request process.
		/// </summary>
		/// <returns>Process the Http Request.</returns>
		/// <param name="Method">Http Request Method.</param>
		/// <param name="URL">Http Request URL</param>
		/// <param name="content">Parameters to POST request.</param>
		public async Task<string> HttpRequestProcess (string Method, string URL, string content)
		{
			HttpClient client = new HttpClient ();


			var WebURL = new Uri (string.Format (URL, string.Empty));


			try {
				

				// For GET request.
				if (Method.ToUpper () == "GET") {
					var response = await client.GetAsync (WebURL);
					if (response.IsSuccessStatusCode) {
						content = await response.Content.ReadAsStringAsync ();

						return content;
					} else
						return	response.IsSuccessStatusCode.ToString ();
				}
				// For POST request.
				else if (Method.ToUpper () == "POST") {
					var Parameters = new StringContent (content, Encoding.UTF8, "application/json");
					var response = await client.PostAsync (WebURL, Parameters);
					if (response.IsSuccessStatusCode) {
						content = await response.Content.ReadAsStringAsync ();
						return content;
					} else
						return	response.IsSuccessStatusCode.ToString ();
				} else
					return "Error : Invalid Request Method ";

			} catch (Exception e) {
				return e.Message;
			} finally {
				client = null;
			}


		}

	
		public async Task<LoginResponseModel> Login (LoginModel loginCreds)
		{
			//Prepare the Content that is to be sent to the Web Service after converting the object into JSON
			var content = new StringContent (JsonConvert.SerializeObject (loginCreds));

			string URL = webLinkLogin + "?userid=" + loginCreds.UserName.ToEncrypt () + "&password=" + loginCreds.Password.ToEncrypt ();

			//Call the Post WebMethod call asynchronously and wait for the response from the server
			var response = await _client.GetAsync (URL);

			if (response.IsSuccessStatusCode) {
				//Read the string response from the WebServie call and save it in a variable
				string responseString = await response.Content.ReadAsStringAsync ();

				if (String.IsNullOrEmpty (responseString)) {
					return null;
				} else {
					string result = responseString.ToDecrypt ();
					if (!string.IsNullOrEmpty (result)) {
						return  JsonConvert.DeserializeObject<LoginResponseModel> (result);
					} else
						return null;
				}
			}


			return null;
		}

		public async Task<T> HttpRequest<T> (string URL)
		{
			var response = await _client.GetAsync (URL);


			if (response.IsSuccessStatusCode) {
				//Read the string response from the WebServie call and save it in a variable
				string responseString = await response.Content.ReadAsStringAsync ();

				if (String.IsNullOrEmpty (responseString)) {
					return default(T);
				} else {
					var result	= responseString.ToDecrypt ();

					if (!string.IsNullOrEmpty (result)) {

						//var temp =	JsonConvert.DeserializeObject (result);



						return  JsonConvert.DeserializeObject<T> (result);



					} else
						return default(T);
				}
			}

			return default(T);
		}


		public async Task<string> HttpRequest (string URL)
		{
			var response = await _client.GetAsync (URL);

			try {
				if (response.IsSuccessStatusCode) {
					//Read the string response from the WebServie call and save it in a variable
					string responseString = await response.Content.ReadAsStringAsync ();

					if (String.IsNullOrEmpty (responseString)) {
						return null;
					} else {
					
						var result	= responseString.ToDecrypt ();

						if (!string.IsNullOrEmpty (result)) {
							return  result;
						} else
							return null;
					}
				}
			} catch {
				
			}
			return null;
		}

		public async Task<ReportListModel> ReportListHttpRequest (string URL)
		{
			URL = webLinkReportList + (URL.StartsWith ("?") ? URL : "?" + URL);
			var result = await HttpRequest<ReportListModel> (URL);
			return result;
		}

		public async Task<string> ReportHttpRequest (string URL)
		{
			URL = webLinkReport + (URL.StartsWith ("?") ? URL : "?" + URL);
			var result = await HttpRequest (URL);
			return result;
		}

		public async Task<string> ChangePasswordHttpRequest(string OldPassword,string NewPassword)
		{
			var sessionManager = DependencyService.Get<ISessionManager> ();
			string URL = WebAPI.Instance.webLinkChangePassword + "?userid=" + sessionManager.GetSessionData (Strings.LoginID).ToEncrypt () +
			             "&session=" + sessionManager.GetSessionData (Strings.SESSION_ID_KEY).ToEncrypt () +
			             "&OldPassword=" + OldPassword.ToEncrypt () +
			             "&NewPassword=" + NewPassword.ToEncrypt ();

			var result = await HttpRequest (URL);
			return result;
		}

		public async Task<string> PasswordValidationHttpRequest()
		{
			var sessionManager = DependencyService.Get<ISessionManager> ();

			string URL = WebAPI.Instance.webLinkPasswordValidation + "?userid=" + sessionManager.GetSessionData (Strings.LoginID).ToEncrypt () +
			             "&session=" + sessionManager.GetSessionData (Strings.SESSION_ID_KEY).ToEncrypt ();
			
			var result = await HttpRequest (URL);
			return result;
		}
	}
}

