using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class BranchDetailsReportModel
	{
		public string BranchCode { get; set; }
		public string BranchName { get; set; }
		public string NoofAccounts { get; set; }
		public string Amount { get; set; }
	}



	public class BranchReportModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<BranchDetailsReportModel> Report_Details { get; set; }
		public List<TotalDetailsModel> TotalDetails { get; set; }
	}

}

