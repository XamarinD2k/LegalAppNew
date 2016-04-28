using System;
using Xamarin.Forms;
using LegalApp.Droid;
using Android.Media;
using System.IO;

[assembly: Dependency(typeof(MediaManager))]
namespace LegalApp.Droid
{
	public class MediaManager : IMediaManager
	{
		
		MediaRecorder mRecorder = null;
		string FilePath = null;
		private MediaPlayer   mPlayer = null;
		#region IMediaManager implementation
		public void startRecording (string mFileName)
		{
			mRecorder = new MediaRecorder ();
			mRecorder.SetAudioSource (AudioSource.Mic);
			mRecorder.SetOutputFormat (OutputFormat.ThreeGpp);
			mRecorder.SetAudioEncoder (AudioEncoder.AmrNb);
			// Initialized state.
			mRecorder.SetOutputFile (Path.Combine(FilePath,mFileName));
			// DataSourceConfigured state.
			mRecorder.Prepare (); // Prepared state
			mRecorder.Start (); // Recording state.
		}
		public void stopRecording ()
		{
			mRecorder.Stop ();
			mRecorder.Release();
			mRecorder = null;
		}
		public void startPlaying (string mFileName)
		{
			mPlayer = new MediaPlayer();
			try {
				mPlayer.SetDataSource(Path.Combine(FilePath,mFileName));
				mPlayer.Prepare();
				mPlayer.Start();
			} catch  {

			}
		}
		public void stopPlaying ()
		{
			mPlayer.Release();
			mPlayer = null;
		}
		#endregion


		public MediaManager() {
			FilePath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
		}
	}
}

