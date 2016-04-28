using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace LegalApp
{
	public class MasterPageVM : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this,
					new PropertyChangedEventArgs (propertyName));
			}
		}

		#endregion

		private ObservableCollection<MasterPageItem> _masterPageItems;

		public ObservableCollection<MasterPageItem> masterPageItems {
			get { return _masterPageItems; }
			set {
				_masterPageItems = value;
				OnPropertyChanged ("masterPageItems");
			}
		}

		private ObservableCollection<MasterPageItem> _reportSubMenuItems;

		public ObservableCollection<MasterPageItem> reportSubMenuItems {
			get { return _reportSubMenuItems; }
			set {
				_reportSubMenuItems = value;
				OnPropertyChanged ("reportSubMenuItems");
			}
		}

		public MasterPageVM ()
		{
			LoadMainMenu ();	
		}

		private async Task LoadMainMenu ()
		{
			masterPageItems = new ObservableCollection<MasterPageItem> ();

			masterPageItems.Add (new MasterPageItem {
				Title = "Logout",

				TargetType = typeof(Login),
				hasSubMenu = false
					//SubMenu = subMenuList
			});


			masterPageItems.Add (new MasterPageItem {
				Title = "Home",

				TargetType = typeof(HomePage),
				hasSubMenu = false
					//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageItem {
				Title = "Change Password",

				TargetType = typeof(ChangePassword),
				hasSubMenu = false
					//SubMenu = subMenuList
			});

			masterPageItems.Add (new MasterPageItem {
				Title = "Case Pending",

				TargetType = typeof(CasePending),
				hasSubMenu = false
				//SubMenu = subMenuList
			});



			masterPageItems.Add (new MasterPageItem {
				Title = "My Location",

				TargetType = typeof(CurrentLocationView),
				hasSubMenu = false
					//SubMenu = subMenuList
			});

			Type reportPageType = null;

			var sessionManager = DependencyService.Get<ISessionManager> ();

			if (sessionManager.GetSessionData (Strings.UserLevel).ToUpper () == "HO") {
				reportPageType = typeof(ZoneReportPage);
			} else if (sessionManager.GetSessionData (Strings.UserLevel).ToUpper () == "ZO") {
				GlobalVariables.ZoneKey = sessionManager.GetSessionData (Strings.UserLocationCode);
				reportPageType = typeof(BranchReportPage);
			} else if (sessionManager.GetSessionData (Strings.UserLevel).ToUpper () == "BO") {
				GlobalVariables.BranchKey = sessionManager.GetSessionData (Strings.UserLocationCode);
				reportPageType = typeof(CustomerReportPage);
			}

			if (reportPageType != null) {
				masterPageItems.Add (new MasterPageItem {
					Title = "Report",

					TargetType = null,
					hasSubMenu = true
					//SubMenu = subMenuList
				});


				var resultReportList = await ReportRequestAPI.InstanceReportRequestAPI.GetReportList();

				if (resultReportList != null) {
					reportSubMenuItems = new ObservableCollection<MasterPageItem> ();

					foreach (ReportListDetailsModel RList in resultReportList.Report_list) {

						reportSubMenuItems.Add (new MasterPageItem {
							Title = RList.ReportRdlName,

							TargetType = reportPageType,
							hasSubMenu = false,
							ReportID = RList.ReportID
							//SubMenu = subMenuList
						});


					}
				}
			}
		}
	}
}

