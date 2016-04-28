using System;

namespace LegalApp
{
	public class ReportRequestModel
	{
		public string userid { get; set; }
		public string session { get; set; }
		public string ReportId { get; set; }
		public string UserLevel { get; set; }
		public string ReportType { get; set; }
		public string Client { get; set; }
		public string BranchCode { get; set; }
		public string ListReport { get; set; }
		public string ZoneAlt_Key { get; set; }
		public string RegionAlt_Key { get; set; }
	}
}

