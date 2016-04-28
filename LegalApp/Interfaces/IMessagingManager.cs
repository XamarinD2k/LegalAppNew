using System;
using System.Collections.Generic;

namespace LegalApp
{
	public interface IMessagingManager
	{
		bool SendSMS(string To,string Message); 
		bool SendEmail(string[] To,string[] CC,string Subject,string Body);
		void Call (string To);
	}
}

