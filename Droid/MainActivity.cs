using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LegalApp.Droid
{
	[Activity (Label = "LegalApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
			Xamarin.FormsMaps.Init(this, bundle);
			LoadApplication (new App ());


		}


		protected override void OnResume ()
		{
			base.OnResume ();
		}

		protected override void OnStop ()
		{
			base.OnStop ();
		}

		protected override void OnStart ()
		{
			base.OnStart ();
		}

		protected override void OnRestart ()
		{
			
			base.OnRestart ();
			//LoadApplication (new App ());
		}
	}
}

