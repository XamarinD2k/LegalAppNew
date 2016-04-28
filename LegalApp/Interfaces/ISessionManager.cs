using System;

namespace LegalApp
{
	public interface ISessionManager
	{
		void StoreSessionData (string Key,string Value);
		string GetSessionData (string Key); 
		void Exit();
	}
}

