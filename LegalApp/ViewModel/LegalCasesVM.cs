using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace LegalApp
{
	public class LegalCasesVM : INotifyPropertyChanged
	{
		public LegalCasesVM ()
		{
		}

		private string sessionID = string.Empty;
		public string SessionID {
			get {
				if (string.IsNullOrEmpty (sessionID)) {
					var sessionManager = DependencyService.Get<ISessionManager> ();
					if (sessionManager != null) {
						sessionID = sessionManager.GetSessionData (Strings.SESSION_ID_KEY);
					}
				}
				return sessionID;
			}
			set {
				sessionID = value;
				OnPropertyChanged ("SessionID");
			}
		}

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
	}
}

