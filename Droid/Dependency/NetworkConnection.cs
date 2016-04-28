using System;
using Xamarin.Forms;
using LegalApp.Droid;
using Android.Net;
using Android.Content;
using Android.Runtime;
using Android.Telephony;


[assembly: Dependency(typeof(NetworkConnection))]
namespace LegalApp.Droid
{
	public class NetworkConnection : INetworkConnection
	{
		public bool IsConnected { get; set; }
		private NetworkInfo activeNetworkInfo;
		public void CheckNetworkConnection()
		{
			var connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService (Context.ConnectivityService);
			activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
			if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting) {
				IsConnected = true;
			} else {
				IsConnected = false;
			}
		}

		public string GetNetworkType()
		{
			var type =	activeNetworkInfo.Type;

			if (type == ConnectivityType.Wifi)
				return "WIFI";
			else if (type == ConnectivityType.Mobile) {
				TelephonyManager TM = (TelephonyManager)Android.App.Application.Context.GetSystemService (Context.TelephonyService);
				NetworkType networktype = TM.NetworkType;

				switch (networktype) {
				case NetworkType.Cdma:
				case NetworkType.Edge:
				case NetworkType.Gprs:
				case NetworkType.OneXrtt:
				case NetworkType.Iden:
					return "2G";
				
				case NetworkType.Umts:
				case NetworkType.Evdo0:
				case NetworkType.EvdoA:
				case NetworkType.EvdoB:
				case NetworkType.Hsdpa:
				case NetworkType.Hsupa:
				case NetworkType.Hspa:
				case NetworkType.Ehrpd:
				case NetworkType.Hspap:
					return "3G";

				case NetworkType.Lte:
					return "4G";

				default:
					return "UNKNOWN";
				}
			} else
				return "UNKNOWN"; 
		}
	}
}

