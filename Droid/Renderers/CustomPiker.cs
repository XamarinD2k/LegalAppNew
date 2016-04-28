using System;
using Xamarin.Forms;
using Android.Widget;
using System.Collections.Generic;
using Xamarin.Forms.Platform.Android;

using LegalApp.Droid;
using System.Collections;

[assembly: ExportRenderer (typeof(Picker), typeof(CustomPickerRenderer))]
namespace LegalApp.Droid
{
	public class CustomPickerRenderer : ViewRenderer<Picker,Spinner>
	{
		Picker picker;
		Spinner spinner;

		protected override void OnElementChanged (ElementChangedEventArgs<Picker> e)
		{
			
			base.OnElementChanged (e);
			if (e.OldElement != null || Element == null) {
				return;
			}
			picker = e.NewElement;
			IList<string> scaleNames = e.NewElement.Items;
			spinner = new Spinner (this.Context);


			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);

			var scaleAdapter = new  ArrayAdapter<string> (this.Context, Android.Resource.Layout.SimpleSpinnerItem, scaleNames);

			scaleAdapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerItem);

			spinner.Adapter = scaleAdapter;


			base.SetNativeControl (spinner);
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			IList<string> scaleNames = picker.Items;
			var scaleAdapter = new  ArrayAdapter<string> (this.Context, Android.Resource.Layout.SimpleSpinnerItem, scaleNames);

			scaleAdapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerItem);
			spinner.Adapter = scaleAdapter;
		}

		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			//picker.SelectedIndex = (e.Position);

		}



	}
}