using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class CasePendingResponseModel
	{
		public string error_Code { get; set;}
		public string error_msg { get; set;}
		public List<CasePendingModel> case_list { get; set;}
	}
}

