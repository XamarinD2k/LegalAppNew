using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class ZoneDetailsReportModel
	{
		public string ZoneAlt_Key { get; set; }
		public string ZoneName { get; set; }
		public string NoofAccounts { get; set; }
		public string Amount { get; set; }
	}



	public class ZoneReportModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<ZoneDetailsReportModel> Report_Details { get; set; }
		public List<TotalDetailsModel> TotalDetails { get; set; }
	}

}

