using System;

namespace LegalApp
{
	public class ServerResponse<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Payload { get; set; }
	}
}

