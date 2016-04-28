using System;

namespace LegalApp
{
	public interface IMyLocation {
		void ObtainMyLocation();
		event EventHandler<ILocationEventArgs> locationObtained;

		string CurrentAddress { get; set; }
		void  GetMap();
	}
	public interface ILocationEventArgs {
		double lat { get; set;}
		double lng { get; set;}
	}
}

