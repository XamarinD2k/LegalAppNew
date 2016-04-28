using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class AudioRecorder : ContentPage
	{
		public AudioRecorder ()
		{
			InitializeComponent ();
			StartPlaying.IsEnabled = false;
			StopPlaying.IsEnabled = false;
			StopRecording.IsEnabled = false;

			var Recording = DependencyService.Get<IMediaManager>();

			StartRecording.Clicked += (object sender, EventArgs e) => {
				
				Recording.startRecording ("temp1.3gp");

				StartRecording.IsEnabled = false;
				StopRecording.IsEnabled = true;

				StartPlaying.IsEnabled = false;
				StopPlaying.IsEnabled = false;

				Status.Text = "Recording Running...";
			};

			StopRecording.Clicked += (object sender, EventArgs e) => {
				Recording.stopRecording ();

				StartRecording.IsEnabled = true;
				StopRecording.IsEnabled = false;
				StartPlaying.IsEnabled = true;
				StopPlaying.IsEnabled = false;

				Status.Text = "Recording completed. File saved successfully...";
			};

			StartPlaying.Clicked += (object sender, EventArgs e) => {
				Recording.startPlaying ("temp1.3gp");

				StartRecording.IsEnabled = false;
				StopRecording.IsEnabled = false;
				StartPlaying.IsEnabled = false;
				StopPlaying.IsEnabled = true;
				Status.Text = "Recording Playing...";
			};

			StopPlaying.Clicked += (object sender, EventArgs e) => {
				Recording.stopPlaying ();

				StartRecording.IsEnabled = true;
				StopRecording.IsEnabled = false;
				StartPlaying.IsEnabled = true;
				StopPlaying.IsEnabled = false;
				Status.Text = "";
			};
		}
	}
}

