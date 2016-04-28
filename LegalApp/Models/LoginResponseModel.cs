using System;

namespace LegalApp
{
	public class LoginResponseModel
	{
		public string adv_Id { get; set;}
		public string adv_Name { get; set;}
		public string error_msg { get; set;}
		public string error_Code { get; set;}
		public string session_Id { get; set;}
		public string UserScope{ get; set;}
		public string UserLevel { get; set; }
		public string UserLocationCode { get; set; }
	}
}

