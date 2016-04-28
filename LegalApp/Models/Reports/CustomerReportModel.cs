using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class CustomerDetailsReportModel
	{
		public string CustID { get; set; }
		public string NoofAccounts { get; set; }
		public string Amount { get; set; }
	}

	public class CustomerReportModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<CustomerDetailsReportModel> Report_Details { get; set; }
		public List<TotalDetailsModel> TotalDetails { get; set; }
	}
}