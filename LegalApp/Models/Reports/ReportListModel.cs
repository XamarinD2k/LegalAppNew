using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class ReportListDetailsModel
	{
		public string ReportID { get; set; }
		public string ReportRdlName { get; set; }
		public string ReportType { get; set; }
	}

	public class ReportListModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<ReportListDetailsModel> Report_list { get; set; }
	}

}

