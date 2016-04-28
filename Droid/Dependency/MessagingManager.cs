using System;
using Xamarin.Forms;
using LegalApp.Droid;
using Android.Content;
using Android.Telephony;
using Android.App;

[assembly: Dependency(typeof(MessagingManager))]
namespace LegalApp.Droid
{
	public class MessagingManager : IMessagingManager
	{
		#region IMessagingManager implementation

		public bool SendSMS (string To, string Message)
		{
			SmsManager sms = SmsManager.Default;
			sms.SendTextMessage (To, null, Message, null, null);

			return true;
		}

		public bool SendEmail (string[] To,string[] CC, string Subject, string Body)
		{
			if (To == null)
				return false;
			
			var email = new Intent(Intent.ActionSend);
			email.PutExtra(Intent.ExtraEmail, To);
			if (CC != null)
				email.PutExtra(Intent.ExtraCc, CC);
			email.PutExtra(Intent.ExtraSubject, Subject);
			email.PutExtra(Intent.ExtraText, Body);
			email.PutExtra(Intent.ExtraHtmlText, true);
			email.SetType("message/rfc822");
			Forms.Context.StartActivity(email);

			return true;
		}

		public void Call (string To)
		{
			String number = "tel:" + To.Trim ();
			Intent callIntent = new Intent (Intent.ActionCall,Android.Net.Uri.Parse (number));

			Forms.Context.StartActivity (callIntent);
		}

		#endregion

		public MessagingManager ()
		{
		}
	}
}

