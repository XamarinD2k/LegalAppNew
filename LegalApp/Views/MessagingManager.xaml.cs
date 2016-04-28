using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class MessagingManager : ContentPage
	{
		public MessagingManager ()
		{
			InitializeComponent ();

			var EmailService = DependencyService.Get<IMessagingManager>();

			SendEmail.Clicked += (object sender, EventArgs e) => {
				
				string[] ListToEmail = MailTo.Text.Split(';');
				string[] ListCcEmail = MailCC.Text.Split(';');
				string Subject = MailSubject.Text;
				string Body = BodyEmail.Text;

				EmailService.SendEmail(ListToEmail,ListCcEmail,Subject,Body);

			};

			MakeCall.Clicked += (object sender, EventArgs e) => {
				EmailService.Call(CallTo.Text);	
			};
		}
	}
}