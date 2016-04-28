using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class PasswordValidationDetailsModel
	{
		public string abbrivi { get; set; }
		public string parameterType { get; set; }
		public string parameterValue { get; set; }
		public string minvalue { get; set; }
		public string maxvalue { get; set; }
	}

	public class PasswordValidationModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<PasswordValidationDetailsModel> PwdValidation { get; set; }
	}
}

