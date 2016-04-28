using System;
using Xamarin.Forms;
using Android.Content;
using Android.Preferences;
using LegalApp.Droid;

[assembly:Dependency(typeof(SessionManager))]
namespace LegalApp.Droid
{
	/// <summary>
	/// Session manager for Android platform
	/// </summary>
	public class SessionManager : ISessionManager
	{
		public SessionManager ()
		{
		}

		#region ISessionManager implementation

		/// <summary>
		/// Stores the session data.
		/// </summary>
		/// <param name="SessionID">Session I.</param>
		public void StoreSessionData (string Key,string Value)
		{
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (Android.App.Application.Context);
			ISharedPreferencesEditor editor = prefs.Edit ();
			editor.PutString (Key, Value);                                                                                                                                                                                                                          
			editor.Apply();        
		}

		/// <summary>
		/// Gets the session data.
		/// </summary>
		/// <returns>The session data.</returns>
		public string GetSessionData (string Key)
		{
			ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences (Android.App.Application.Context);
			string sessionID = prefs.GetString (Key, string.Empty);

			return sessionID;
		}

		public void Exit()
		{
			Android.OS.Process.KillProcess (Android.OS.Process.MyPid());
		}

		#endregion
	}
}

