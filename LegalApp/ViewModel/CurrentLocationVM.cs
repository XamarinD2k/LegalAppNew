using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace LegalApp
{
	public class CurrentLocationVM : INotifyPropertyChanged
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

		public CurrentLocationVM ()
		{
		}

		private string _Latitude;
		public string Latitude 
		{
			get { return _Latitude; }
			set {
				_Latitude = value;
				OnPropertyChanged ("Latitude");
			}
		}

		private string _Longitude;
		public string Longitude 
		{
			get { return _Longitude; }
			set {
				_Longitude = value;
				OnPropertyChanged ("Longitude");
			}
		}

		private string _Address;
		public string Address 
		{
			get { return _Address; }
			set {
				_Address = value;
				OnPropertyChanged ("Address");
			}
		}

		private Command _GetLocation;
		public Command GetLocation
		{
			get {
				return _GetLocation ?? new Command (async delegate(object o) {
					var loc = DependencyService.Get<IMyLocation> ();
				   	loc.locationObtained += (object sender1,
						ILocationEventArgs e1) => {
						var lat = e1.lat;
						var lng = e1.lng;
						Latitude = lat.ToString();
						Longitude = lng.ToString();
					};
				    loc.ObtainMyLocation ();

					string currentAddrdess =string.Empty;
					Geocoder geoCoder = new Geocoder ();
					var possibleAddresses  = await geoCoder.GetAddressesForPositionAsync(new Position(Convert.ToDouble(Latitude),Convert.ToDouble(Longitude)));
					foreach(var Add in possibleAddresses)
					{
						currentAddrdess =	Add;
						break;
					}

					Address = currentAddrdess;

				});
			}
		}

		private Command _GetMap;
		public Command GetMap
		{
			get {
				return _GetMap ?? new Command (delegate(object o) {
					var service = DependencyService.Get<IMyLocation> ();
					service.GetMap ();
				});
			}
		}
	}
}

