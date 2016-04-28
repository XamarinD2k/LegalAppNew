using System;

namespace LegalApp
{
	public interface INetworkConnection
	{
		bool IsConnected { get; }
		void CheckNetworkConnection();
		string GetNetworkType();
	}
}

