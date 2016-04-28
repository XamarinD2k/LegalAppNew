using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace LegalApp
{
	public class CasePendingVM : INotifyPropertyChanged
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



		public  CasePendingVM ()
		{
			//LoadPendingCases ();
		}



		private string _SearchValue = string.Empty;

		public string SearchValue {
			get { return _SearchValue; }
			set {
				_SearchValue = value;
				OnPropertyChanged ("SearchValue");
				FilterList ();
			}
		}

		private bool _ActivityIndicator = false;

		public bool ActivityIndicator {
			get { return _ActivityIndicator; }
			set {
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private ObservableCollection<CasePendingModel> _CasePendingList;

		public ObservableCollection<CasePendingModel> CasePendingList {
			get {
					if (_CasePendingList == null) {
						LoadPendingCases ();
				}
				return _CasePendingList;
			}
			set {
				_CasePendingList = value;
				OnPropertyChanged ("CasePendingList");
			}
		}

		private ObservableCollection<CasePendingModel> _MainCasePendingList;

		public ObservableCollection<CasePendingModel> MainCasePendingList {
			get { 
				return _MainCasePendingList;
			}
			set {
				_MainCasePendingList = value;
				_CasePendingList = value;
				OnPropertyChanged ("MainCasePendingList");
			}
		}

		private string _Result;

		public string Result {
			get { return _Result; }
			set {
				_Result = value;
				OnPropertyChanged ("Result");
			}
		}

		public async Task LoadPendingCases ()
		{
			ActivityIndicator = true;


			Result = WebAPI.Instance.webLinkCasePending + "?adv_id=" + WebAPI.Instance.UserID.ToEncrypt () + "&session=" + WebAPI.Instance.SessionID.ToEncrypt ();
			var result = await WebAPI.Instance.HttpRequest<CasePendingResponseModel> (WebAPI.Instance.webLinkCasePending + "?adv_id=" + WebAPI.Instance.UserID.ToEncrypt () + "&session=" + WebAPI.Instance.SessionID.ToEncrypt ());

			if (result != null) {
				//CaseName = result.case_list.Count.ToString();

				MainCasePendingList = new  ObservableCollection<CasePendingModel> (result.case_list);
				CasePendingList = MainCasePendingList;

				//CasePendingList = result.casePending;
			} 
			ActivityIndicator = false;

//			else
//				CaseName = "Empty Data";
		}

		private Command fetchCommand = null;

		public Command FetchCommand {
			get {
				return fetchCommand ?? new Command (async delegate(object o) {

					await LoadPendingCases ();

				});
			}
		}

		private Command filterTextChangedCommand = null;

		public Command FilterTextChangedCommand {
			get {
				

				return filterTextChangedCommand ?? new Command (delegate(object o) {

					FilterList ();

				});
			}
		}

		private void FilterList ()
		{
			if (!string.IsNullOrEmpty (SearchValue)) {
				var list = from main in MainCasePendingList
				            where main.CustomerName.Contains (SearchValue.ToUpper ())
				            select main;

				CasePendingList = new ObservableCollection<CasePendingModel> (list);

			} else {
				CasePendingList = MainCasePendingList;
			}
		}

		private Command clearCommand = null;

		public Command ClearCommand {
			get { 
				return clearCommand ?? new Command (delegate(object o) {
					SearchValue = "";
					FilterList ();
				});
					
			}
		}


	}
}

