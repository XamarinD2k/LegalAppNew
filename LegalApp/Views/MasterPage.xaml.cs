using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LegalApp
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListViewMainMenu { get { return listView; } }

		public bool IsVisibleReportSubMenu = true;

		public ListView ListViewReportSubMenu { get { return listViewReport; } }
		public MasterPage ()
		{
			InitializeComponent ();

//			var masterPageItems = new List<MasterPageItem> ();
//
//			masterPageItems.Add (new MasterPageItem {
//				Title = "Case Pending",
//				IconSource = "case1.png",
//				TargetType = typeof(CasePending),
//				hasSubMenu = false
//				//SubMenu = subMenuList
//			});
//
//			masterPageItems.Add (new MasterPageItem {
//				Title = "Logout",
//				IconSource = "logout.png",
//				TargetType = typeof(Login),
//				hasSubMenu = false
//				//SubMenu = subMenuList
//			});
//
//			Type reportPageType = null;
//
//			var sessionManager = DependencyService.Get<ISessionManager> ();
//
//			if (sessionManager.GetSessionData (Strings.UserLevel).ToUpper () == "HO") {
//				reportPageType = typeof(ZoneReportPage);
//			} else if (sessionManager.GetSessionData (Strings.UserLevel).ToUpper () == "ZO") {
//				GlobalVariables.ZoneKey = sessionManager.GetSessionData (Strings.UserLocationCode);
//				reportPageType = typeof(BranchReportPage);
//			} else if (sessionManager.GetSessionData (Strings.UserLevel).ToUpper () == "BO") {
//				GlobalVariables.BranchKey = sessionManager.GetSessionData (Strings.UserLocationCode);
//				reportPageType = typeof(CustomerReportPage);
//			}
//
//			if (reportPageType != null) {
//				masterPageItems.Add (new MasterPageItem {
//					Title = "Report",
//					IconSource = "rprt.png",
//					TargetType = null,
//					hasSubMenu = true
//					//SubMenu = subMenuList
//				});
//
//				var reportSubMenuItems = new List<MasterPageItem> ();
//
//				reportSubMenuItems.Add (new MasterPageItem {
//					Title = "LEGAL-01",
//					IconSource = "case1.png",
//					TargetType = reportPageType,
//					hasSubMenu = false
//					//SubMenu = subMenuList
//				});
//
//				reportSubMenuItems.Add (new MasterPageItem {
//					Title = "LEGAL-02",
//					IconSource = "logout.png",
//					TargetType = typeof(BranchReportPage),
//					hasSubMenu = false
//					//SubMenu = subMenuList
//				});
//
//				listViewReport.ItemsSource = reportSubMenuItems;
//			}
//			listView.ItemsSource = masterPageItems;
		}
	}
}
