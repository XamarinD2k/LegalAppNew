using System;

namespace LegalApp
{
	public interface IMediaManager
	{
		void startRecording(string FileName);
		void stopRecording();
		void startPlaying(string FileName);
		void stopPlaying();
	}
}

