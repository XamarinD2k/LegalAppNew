using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Diagnostics;
using System.IO;

namespace LegalApp
{
	public class CaseDetailsVM : INotifyPropertyChanged
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

		public  CaseDetailsVM()
		{
			LoadCaseDetails ();
		}

		private string _SuitType = string.Empty;
		public string SuitType 
		{
			get { return _SuitType; }
			set {
				_SuitType = value;
				OnPropertyChanged ("SuitType");
			}
		}

		private string _HANDingDocTAdvDt = string.Empty;
		public string HANDingDocTAdvDt 
		{
			get { return _HANDingDocTAdvDt; }
			set {
				_HANDingDocTAdvDt = value;
				OnPropertyChanged ("HANDingDocTAdvDt");
			}
		}

		private string _AcceptanceDt = string.Empty;
		public string AcceptanceDt 
		{
			get { return _AcceptanceDt; }
			set {
				_AcceptanceDt = value;
				OnPropertyChanged ("AcceptanceDt");
			}
		}

		private string _FilPlaintInCourtDt = string.Empty;
		public string FilPlaintInCourtDt 
		{
			get { return _FilPlaintInCourtDt; }
			set {
				_FilPlaintInCourtDt = value;
				OnPropertyChanged ("FilPlaintInCourtDt");
			}
		}

		private CaseDetailsModel _CaseDetails = new CaseDetailsModel();
		public CaseDetailsModel CaseDetails 
		{
			get { 
				return _CaseDetails;
			}
			set{
				_CaseDetails = value;

				OnPropertyChanged ("CaseDetails");
			}
		}

		public string CustomerName { get {
				var sessionObj = DependencyService.Get<ISessionManager> ();

				return sessionObj.GetSessionData ("customerName"); } }

		private int _currentStateIndex;
		public	int currentStateIndex {
			get
			{ 
				return _currentStateIndex; 
			}
			set { 
				_currentStateIndex = value;
				OnPropertyChanged ("currentStateIndex");
			}
		}

		private int _AppealIndex;
		public	int AppealIndex {
			get
			{ 
				return _AppealIndex; 
			}
			set { 
				_AppealIndex = value;
				OnPropertyChanged ("AppealIndex");
			}
		}

		private int _NextStage;
		public	int NextStage {
			get
			{ 
				return _NextStage; 
			}
			set { 
				_NextStage = value;
				OnPropertyChanged ("NextStage");
			}
		}

		private int _NextPostingIndex;
		public	int NextPostingIndex {
			get
			{ 
				return _NextPostingIndex; 
			}
			set { 
				_NextPostingIndex = value;
				OnPropertyChanged ("NextPostingIndex");
			}
		}

		private int _AdjournetReasonIndex;
		public	int AdjournetReasonIndex {
			get
			{ 
				return _AdjournetReasonIndex; 
			}
			set { 
				_AdjournetReasonIndex = value;
				OnPropertyChanged ("AdjournetReasonIndex");
			}
		}

		private int _AdjournetSoughtByIndex;
		public	int AdjournetSoughtByIndex {
			get
			{ 
				return _AdjournetSoughtByIndex; 
			}
			set { 
				_AdjournetSoughtByIndex = value;
				OnPropertyChanged ("AdjournetSoughtByIndex");
			}
		}

		private int _InterimOrdersIndex;
		public	int InterimOrdersIndex {
			get
			{ 
				return _InterimOrdersIndex; 
			}
			set { 
				_InterimOrdersIndex = value;
				OnPropertyChanged ("InterimOrdersIndex");

			}
		}



		private bool _ActivityIndicator =false;
		public bool ActivityIndicator
		{
			get { return _ActivityIndicator; }
			set{
				_ActivityIndicator = value;
				OnPropertyChanged ("ActivityIndicator");
			}
		}

		private DateTime _NextPostingDate=DateTime.Now;
		public DateTime NextPostingDate
		{
			get {
				return	_NextPostingDate;
			}
			set { 
				_NextPostingDate = value;
				OnPropertyChanged ("NextPostingDate");
			}
		}

		private DateTime _RemarksDate=DateTime.Now;
		public DateTime RemarkDate
		{
			get {
				return	_RemarksDate;
			}
			set { 
				_RemarksDate = value;
				OnPropertyChanged ("RemarkDate");
			}
		}

		private string _Remark;
		public string Remark {
			get {  
				return _Remark;
			}
			set {
				_Remark = value;
				OnPropertyChanged ("Remark");
			}
		}


		public async Task LoadCaseDetails()
		{
			ActivityIndicator = true;

			var sessionObj = DependencyService.Get<ISessionManager> ();


			string case_id = sessionObj.GetSessionData ("case_id");
			string BranchCode = sessionObj.GetSessionData ("BranchCode");

			var result = await WebAPI.Instance.HttpRequest<CaseDetailsModel> (WebAPI.Instance.webLinkCaseDetails + "?adv_id=" + WebAPI.Instance.UserID.ToEncrypt () +
				"&session=" + WebAPI.Instance.SessionID.ToEncrypt () +"&case_id="+ case_id.ToEncrypt() + 
				"&Mode=" + ("1".ToEncrypt ())+"&BranchCode="+BranchCode.ToEncrypt());

			if (result != null) {
				//CaseName = result.case_list.Count.ToString();

				CaseDetails = result;

				MessagingCenter.Send<CaseDetailsVM>(this, Strings.AUTHENTICATION_SUCCESS);

			} 
			ActivityIndicator = false;
		}

		private string _Result;
		public string Result
		{
			get { return _Result; }
			set {
				_Result = value;
				OnPropertyChanged ("Result");
			}
		}

		private Command _SaveCommand = null;
		public Command SaveCommand {
			get {
				return _SaveCommand ?? new Command (async delegate(object o) {

					ActivityIndicator = true;

					string	CurrentFutureStage_Front =	CaseDetails.CurrentStage [currentStateIndex].code;
					string	Appeal = CaseDetails.Appeal [AppealIndex].Code.ToString ();
					string	AdjourmentSougtByAlt_Key = CaseDetails.DimLegalAdjournment [AdjournetSoughtByIndex].LegalAdjournmentAlt_Key;
					string AdInterimOrder_Front = CaseDetails.AddInterimOrder [InterimOrdersIndex].Code;
					string NextPostPurposeAlt_Key = CaseDetails.NextStage [NextPostingIndex].code;
					string AdjourmentReasonAlt_Key = CaseDetails.DimLegalAdjournmentReason [AdjournetReasonIndex].LegalAdjournReasonAlt_Key;





					string URL = WebAPI.Instance.webLinkCaseDetailsUpdate + "?adv_id=" + WebAPI.Instance.UserID.ToEncrypt () +
						"&session=" + WebAPI.Instance.SessionID.ToEncrypt () +"&case_Id="+ CaseDetailsParametes.case_id.ToEncrypt() + 
						"&Mode=" + ("Add".ToEncrypt ())+
						"&CurrentFutureStage_Front="+CurrentFutureStage_Front.ToEncrypt()+
						"&Appeal_front="+Appeal.ToEncrypt()+
						"&AdInterimOrder_Front="+AdInterimOrder_Front.ToEncrypt()+
						"&Remark="+Remark.ToEncrypt()+
						"&RemarkDt_Front="+RemarkDate.Date.ToString("dd-MM-yyyy").ToEncrypt()+
						"&AttachMent="+("A".ToEncrypt())+
						"&NextPostPurposeAlt_Key="+ NextPostPurposeAlt_Key.ToEncrypt()+
						"&AdjourmentSougtByAlt_Key="+ AdjourmentSougtByAlt_Key.ToEncrypt()+
						"&AdjourmentReasonAlt_Key="+ AdjourmentReasonAlt_Key.ToEncrypt()+
						"&NextHearingDt="+ NextPostingDate.Date.ToString("dd-MM-yyyy").ToEncrypt();

//					Result= " AdvID = "+WebAPI.Instance.UserID+"  Session="+WebAPI.Instance.SessionID+"  CaseId="+CaseDetailsParametes.case_id+"  Curr:"+CurrentFutureStage_Front+
//						" App."+Appeal+"  Inter-:"+AdInterimOrder_Front+" RemarkDt:"+RemarksDate.ToString()+" NextPost:"+NextPostPurposeAlt_Key+
//						"  Adj.:"+AdjourmentSougtByAlt_Key+"  Rea.:"+AdjourmentReasonAlt_Key+" NextHr.Dt. :"+NextPostingDate.ToString();
//					

				//	var result = await WebAPI.Instance.HttpRequestProcess("GET",URL,"");

					var result = await WebAPI.Instance.HttpRequest<CaseDetailsSave> (URL);

					if (result != null) {
						//CaseName = result.case_list.Count.ToString();



						if(result.error_Code == "0")
							MessagingCenter.Send<CaseDetailsVM>(this, Strings.CaseDetails_SUCCESS);
						else
							MessagingCenter.Send<CaseDetailsVM>(this, Strings.CaseDetails_FAILURE);

					} 

					//Result = URL;
					ActivityIndicator = false;




				});
			}
		}

		private bool _AttachmentVisible = false;
		public bool AttachmentVisible
		{
			get { return _AttachmentVisible; }
			set{
				_AttachmentVisible = value;
				OnPropertyChanged ("AttachmentVisible");
			}
		}

		private ObservableCollection<AttachmentModel> _AttachImage=null;
		public ObservableCollection<AttachmentModel> AttachImage
		{
			get { return _AttachImage; }
			set {
				_AttachImage = value;
				//AttachmentVisible = (_AttachImage != null) ? (_AttachImage.Count >0 ? true : false) : false;
				OnPropertyChanged ("AttachImage");
			}
		}



		private AttachmentModel _selectedAttachment;
		public AttachmentModel selectedAttachment
		{
			get { return _selectedAttachment; }
			set {
				_selectedAttachment = value;
				OnPropertyChanged ("selectedAttachment");
			}
		}

		private Command _AttachmentCommand;
		public Command AttachmentCommand {
			get { 
				return _AttachmentCommand ?? new Command (async delegate(object o) {
					MediaFile file      = null;
					string    filePath  = string.Empty;
					string    imageName = string.Empty;

					try {
						

						file = await CrossMedia.Current.PickPhotoAsync();
						if(file == null) 
						{ 
							AttachImage = null; 
						}
						else
						{
							
//								imageName = "SomeImageName.jpg";
//
//
//
//							var IS =	ImageSource.FromStream(() =>
//								{
//									var stream = file.GetStream();
//
//									file.Dispose();
//									return stream;
//								});


							if(AttachImage == null)
								AttachImage = new ObservableCollection<AttachmentModel>();



							AttachImage.Add(new AttachmentModel()
								{
									FileName = Path.GetFileName(file.Path),
									_ImageSource = file.Path
								});


							AttachmentVisible = (AttachImage.Count>0);


						}
						/* Add your own logic here for where to save the file */ //_fileHelper.CopyFile(file.Path, imageName);
					} catch(Exception ex) {
						Debug.WriteLine("\nIn PictureViewModel.PickPictureAsync() - Exception:\n{0}\n", ex);                           //TODO: Do something more useful

					} finally 
					{ 
						if(file != null) 
						{ 
							file.Dispose(); 
						} 
					}
				});
			}
		}

		private Command _DeleteAttachment;
		public Command DeleteAttachment
		{
			get { 
				return _DeleteAttachment ?? new Command (async delegate(object o) {
					Result = "Deleted";
				});
			}
		}
		 
	}
}

