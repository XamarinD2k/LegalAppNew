using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class CaseDetailsParametes {
		public static string customerName=string.Empty;
		public static string case_id=string.Empty;
		public static string BranchCode=string.Empty;

	}
	public class RestDataDetail
	{
		public string case_id { get; set; }
		public string SuitType { get; set; }
		public string HANDingDocTAdvDt { get; set; }
		public string FilPlaintInCourtDt { get; set; }
		public string AcceptanceDt { get; set; }
		public string session_Id { get; set; }
	}

	public class CurrentStage
	{
		public string code { get; set; }
		public string Description { get; set; }
	}

	public class NextStage
	{
		public string code { get; set; }
		public string Description { get; set; }
	}

	public class DimLegalAdjournment
	{
		public string LegalAdjournmentAlt_Key { get; set; }
		public string Description { get; set; }
		public string GRP { get; set; }
	}

	public class DimLegalAdjournmentReason
	{
		public string LegalAdjournReasonAlt_Key { get; set; }
		public string Description { get; set; }
		public string GRP { get; set; }
	}

	public class AddInterimOrder
	{
		public string Code { get; set; }
		public string Description { get; set; }
	}

	public class Appeal
	{
		public string Code { get; set; }
		public string Description { get; set; }
	}

	public class CaseDetailsModel
	{
		public string error_Code { get; set; }
		public string error_msg { get; set; }
		public List<RestDataDetail> RestDataDetails { get; set; }
		public List<CurrentStage> CurrentStage { get; set; }
		public List<NextStage> NextStage { get; set; }
		public List<DimLegalAdjournment> DimLegalAdjournment { get; set; }
		public List<DimLegalAdjournmentReason> DimLegalAdjournmentReason { get; set; }
		public List<AddInterimOrder> AddInterimOrder { get; set; }
		public List<Appeal> Appeal { get; set; }
	}

}

