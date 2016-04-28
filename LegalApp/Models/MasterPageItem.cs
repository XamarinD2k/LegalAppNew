using System;
using System.Collections.Generic;

namespace LegalApp
{
	public class MasterPageItem
	{

			public string Title{ set; get;}

			public string IconSource{ set; get;}

			public Type TargetType{ set; get;}

		public bool hasSubMenu { get; set; }

		public string ReportID { get; set; }
		//public List<SubMenuItem> SubMenu { get; set;}
	}

	public class SubMenuItem
	{
		public string SubMenuTitle { get; set;}
	}
}

