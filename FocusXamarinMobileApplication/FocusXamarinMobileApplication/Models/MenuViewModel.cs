namespace FocusXamarinMobileApplication.Models
{
    public class MenuViewModel : FBaseViewModel
    {
        private readonly Assignments _assignmentService;
        private readonly Services.Checks _checkService;

        private readonly IDialogService _dialogService;
        private readonly Jobs _jobService;

        private readonly INavigationService _navigationService;

        // private JobData4Tablet _selectedJobData;
        private readonly Stopwatch _sw = new Stopwatch();
        private RelayCommand<string> _approveMeasure2;
       //private AuthorisationDetail _authorisation;
        public RelayCommand<string> _checkLogin;
        private RelayCommand<string> _companyDocs;
        private RelayCommand<string> _gangClear;
        private RelayCommand<string> _inputCalibration;
        private RelayCommand<string> _inputDiary;
        private RelayCommand<string> _inputLateral;
        private RelayCommand<string> _inputMeasure;
        private RelayCommand<string> _jobPack;
        private RelayCommand<string> _jobPictures;
        private RelayCommand<string> _myPlant;
        private RelayCommand<string> _mySiteRiskAss;
        private RelayCommand _navigateToProjectDiaries;
        private RelayCommand<string> _operativeDocs;
        private RelayCommand<string> _permitToDig;
        //private List<JobData4Tablet> _projectJobs;
        private RelayCommand<string> _projectSummary;
        private RelayCommand<string> _returnToMainJobScreen;
        private RelayCommand<string> _screenLoaded4Menu;
        private RelayCommand<object> _screenLoaded4MenuSupervisor;
        private RelayCommand _selectAuditGangs;
        private RelayCommand _selectGangs;
        private RelayCommand<string> _selectTimeSheets;
        private RelayCommand<string> _serviciesCrossed;
        private RelayCommand<string> _siteClear;
        private RelayCommand<string> _upload;
        private RelayCommand<string> _visitorsLog;
        public Services.Users UserService;

        // public Tuple<Person, List<Models.Assignment>, JobData4Tablet> NavPassedData { get; set; }
        //public Person LoggedInUser { get; set; }
        //public JobData4Tablet CurrentSelectedJob { get; private set; }


        //public List<ProjectWorks> ProjectWorksList { get; set; } = new List<ProjectWorks>();


        public MenuViewModel(IDialogService dialogService, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            UserService = new Services.Users();
            _assignmentService = new Assignments();
            _jobService = new Jobs();
            SpinnerAction = "SpinnerStop";
            _checkService = new Services.Checks();
            AzureAuth = new AzureAuth();
        }

        public bool SelectGangHidden { get; set; }
        public bool ApprovalUploadRequired { get; set; }
        public bool AllplantAccepted { get; private set; }
        public bool UploadButtonVisable { get; set; }
        public bool CrossedUtilities { get; set; }
        public bool InputDiaryRecord { get; set; }
        public string SpinnerAction { get; set; } = "SpinnerStop";
        public bool EndPointsAvailable { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDate => $"{_jobService.GetCurrentSelectedJob()?.Result.JobDate:dd/MM/yyyy}";

        public bool LabourFiles { get; set; }
        // public bool VisitorsStillToBeLoggedOut { get; set; }

        public ServicesCrossed4Tablet Utility { get; set; }
        public AzureAuth AzureAuth { get; set; }

        public RelayCommand<string> CheckLogin => _checkLogin ??= new RelayCommand<string>(async e =>
        {
            NavigationalParameters.AppMode =
                NavigationalParameters.AppModes.MENU;

            NavigationalParameters.AuthDetail = new AuthorisationDetail();
            NavigationalParameters.logonCheckResult = false;
            var cl = AzureAuth.GetUserAuthInformation();

            if (cl != null && cl.LoginDateTime != null)
            {
                if (NavigationalParameters.InitialLogOn ==
                    DateTime.Parse("01/01/1900 00:00:00"))
                    NavigationalParameters.InitialLogOn =
                        (DateTime)cl.LoginDateTime;

                var nowMinus8Hrs = DateTime.Now.AddHours(-10);

                //DateTime nowMinus8Hrs = DateTime.Now.AddMinutes(-1);
                if (NavigationalParameters.InitialLogOn < nowMinus8Hrs)
                {
                    var n = cl.LoginDateTime;
                    AzureAuth.SignoutUser();
                    NavigationalParameters.logonCheckResult = false;
                    _navigationService.NavigateTo(
                        Locator.LoginPageViewModelKey,
                        Locator.MenuPageViewModelKey);
                }
                else
                {
                    if (cl.LoginDateTime > nowMinus8Hrs &&
                        cl.LoginDateTime <= DateTime.Now)
                    {
                        var n = cl.LoginDateTime;
                        NavigationalParameters.logonCheckResult = true;
                    }
                    else
                    {
                        await _dialogService.ShowMessage(
                            "Warning!! You are about to be loged out",
                            "Logging out", "Continue to logout",
                            "Continue to app", ok =>
                            {
                                if (ok)
                                {
                                    var n = cl.LoginDateTime;
                                    AzureAuth.SignoutUser();
                                    NavigationalParameters
                                        .logonCheckResult = false;
                                    _navigationService.NavigateTo(
                                        Locator.LoginPageViewModelKey,
                                        Locator.MenuPageViewModelKey);
                                }
                                else
                                {
                                    NavigationalParameters
                                        .logonCheckResult = true;
                                    // aa.SaveUpdateAccessToken(cl);
                                }
                            });
                    }
                }
            }
        });
        //Hide SelectGang Button and back button if the current job requires approving.

        public RelayCommand<string> ScreenLoaded4Menu
        {
            get
            {
                return _screenLoaded4Menu ??= new RelayCommand<string>(e =>
                {
                    try
                    {
                        //sw.Restart();
                        //if ( e != null) NavPassedData = e;
                        SetSpinner(false);
                        // LoggedInUser = NavigationalParameters.LoggedInUser;
                        // CurrentSelectedJob = NavigationalParameters.CurrenSelectedJob;
                       // _authorisation = new AuthorisationDetail();

                        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
                        if (NavigationalParameters.CurrentSelectedJob != null)
                        {
                            NavigationalParameters.CurrentSelectedJob.JobGang = _jobService.GetCurrentSelectedGang();

                            Utility = _jobService.GetCrossedUtilities(NavigationalParameters.CurrentSelectedJob);

                            //Debug.WriteLine($"Get job and Gang {_sw.ElapsedMilliseconds / 1000f}");
                            //_sw.Restart();

                            if (Utility == null)
                                CrossedUtilities = false;
                            else
                                CrossedUtilities = !string.IsNullOrEmpty(Utility.ServicesCrossedData1) &&
                                                   Utility.RemoteTableId == 0;

                            NavigationalParameters.VisitorsStillToBeLoggedOut = _jobService
                                .GetVisitors(NavigationalParameters.CurrentSelectedJob)
                                .Any(x => x.OffSiteTime == DateTime.Parse("1/1/1900 00:00:00"));

                            InputDiaryRecord =
                                !string.IsNullOrEmpty(_jobService
                                    .GetClaimedNotes(NavigationalParameters.CurrentSelectedJob)?.NotesText) &&
                                !string.IsNullOrEmpty(_jobService
                                    .GetClaimedNotes(NavigationalParameters.CurrentSelectedJob)?.AnyDelays) &&
                                !string.IsNullOrEmpty(_jobService
                                    .GetClaimedNotes(NavigationalParameters.CurrentSelectedJob)
                                    ?.AnyAddnlWorkReq);

                            LabourFiles = NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles.All(
                                x => x.MyLabour.Exists(
                                    y => y.LabourDate.ToString() ==
                                         NavigationalParameters.CurrentSelectedJob.JobDate.ToString()
                                         && y.ContractReference == NavigationalParameters.CurrentSelectedJob
                                             .ContractReference
                                         && (y.ClaimedYardStart != DateTime.Parse("1/1/1900 00:00:00")
                                             || y.TravelToSite != DateTime.Parse("1/1/1900 00:00:00")
                                             && y.TimeOnSite != DateTime.Parse("1/1/1900 00:00:00")
                                         )));

                            NavigationalParameters.CurrentSelectedJob.JobProjectSummary =
                                _assignmentService.GetProjectSummary(NavigationalParameters.CurrentSelectedJob
                                    .QuoteNumber);

                            EndPointsAvailable =
                                _jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob).Count > 0;

                            // var tempList = _assignmentService
                            //   .GetRates(NavigationalParameters.CurrenSelectedJob.QuoteNumber, "Contract")
                            // .ToList();

                            NavigationalParameters.ProjectWorksList = _assignmentService
                                .GetRates(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract")
                                .Where(x => x.AssignmentId == Guid.Empty)
                                .Select(i => new ProjectWorks
                                {
                                    RemoteTableId = i.RemoteTableId,
                                    BaseUnit = i?.BaseUnit,
                                    Description = i?.Description,
                                    Header = i?.Header,
                                    QuoteId = i.QuoteId,
                                    Category = i?.Category,
                                    AssignmentId = i.AssignmentId,
                                    Identifier = i.Identifier,
                                    Status = i.Status
                                }).ToList();
                        }

                        AllplantAccepted = _jobService.CheckAllPlantHasBeenExcepted();
                        NavigationalParameters.StartOfDayPictureTaken = _jobService.CheckStartOfDayPictureExsits();
                    }
                    catch (Exception exception)
                    {
                        _navigationService.NavigateTo(Locator.MainListPageViewModelKey, Locator.MenuPageViewModelKey);
                    }
                });
            }
        }

        public RelayCommand<string> SelectTimeSheets
        {
            get
            {
                return _selectTimeSheets ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.TIMESHEETS;
                    _navigationService.NavigateTo(Locator.TimesheetsSelectionViewModelKey, Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> InputDiary
        {
            get
            {
                return _inputDiary ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.DIARIES;
                    _navigationService.NavigateTo(Locator.DailyDiaryViewModelKey, Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> InputMeasure
        {
            get
            {
                return _inputMeasure ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.MEASURES;
                    _navigationService.NavigateTo(Locator.MeasureListPageViewModelKey, Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> InputCalibration
        {
            get
            {
                return _inputCalibration ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.CALIBRATION;

                    var cabList =
                        _assignmentService.GetCabData(NavigationalParameters.CurrentSelectedJob.QuoteNumber);

                    var dataToBePassed =
                        new Tuple<Person, List<Models.Assignment>, JobData4Tablet,
                            List<VMl4CabDetail>>(NavigationalParameters.LoggedInUser,
                            NavigationalParameters.AssignmentList, NavigationalParameters.CurrentSelectedJob,
                            cabList);

                    NavigationalParameters.NavigationParameter = dataToBePassed;

                    //_navigationService.NavigateTo(Locator.CabSelectionKey,
                    //    new Tuple<Person, List<Models.Assignment>, JobData4Tablet,
                    //        List<VMl4CabDetail>>(NavigationalParameters.LoggedInUser,
                    //        NavigationalParameters.AssignmentList, NavigationalParameters.CurrentSelectedJob,
                    //        cabList));
                });
            }
        }

        public RelayCommand<string> ReturnToMainJobScreen
        {
            get
            {
                return _returnToMainJobScreen ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;
                    //var dataToBePassed =
                    //    new Tuple<Person, List<Models.Assignment>>(
                    //        NavigationalParameters.LoggedInUser,
                    //        NavigationalParameters.AssignmentList);

                    //  NavigationalParameters.NavigationParameter = dataToBePassed;

                    _navigationService.NavigateTo(Locator.MainListPageViewModelKey, Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> InputLateral
        {
            get
            {
                return _inputLateral ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.LATERALS;

                    if (EndPointsAvailable)
                    {
                        //NavigationalParameters.NavigationParameter =
                        //    new Tuple<Person, List<Models.Assignment>, JobData4Tablet,
                        //        List<ProjectWorks>, string>(NavigationalParameters.LoggedInUser,
                        //        NavigationalParameters.AssignmentList,
                        //        NavigationalParameters.CurrentSelectedJob,
                        //        NavigationalParameters.ProjectWorksList, e);

                      // _navigationService.NavigateTo(Locator.InputLateralKey,Locator.MenuPageViewModelKey);
                    }
                    else
                    {
                        _dialogService.ShowMessage("Thereare currently no end points available",
                            "please contact the support team!");
                    }
                });
            }
        }

        public RelayCommand<string> ServicesCrossed
        {
            get
            {
                return _serviciesCrossed ??= new RelayCommand<string>(async e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.UTILITIES;

                    var crossedUtilities =
                        _jobService.GetCrossedUtilities(NavigationalParameters.CurrentSelectedJob);

                    if (crossedUtilities == null)
                    {
                        crossedUtilities = new ServicesCrossed4Tablet
                        {
                            ContractId = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                            QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                            ContractReference = NavigationalParameters.CurrentSelectedJob.ContractReference,
                            GangLeaderId = NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString(),
                            PostedDate = NavigationalParameters.CurrentSelectedJob.JobDate.Date
                        };

                        await _jobService.AddCrossedUtilities(crossedUtilities);
                    }

                   // NavigationalParameters.NavigationParameter =
                       // new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                           // NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                            //NavigationalParameters.CurrentSelectedJob);

                    _navigationService.NavigateTo(Locator.ServicesCrossedViewModelKey,Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> ProjectSummary
        {
            get
            {
                return _projectSummary ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUMMARY;

                   // NavigationalParameters.NavigationParameter =
                       //new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                           //NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                            //NavigationalParameters.CurrentSelectedJob);

                    _navigationService.NavigateTo(Locator.ProjectSummaryViewModelKey,Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> PermitToDig
        {
            get
            {
                return _permitToDig ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.PERMITTODIG;

                   // NavigationalParameters.NavigationParameter =
                      //  new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                          //  NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                           // NavigationalParameters.CurrentSelectedJob);

                    _navigationService.NavigateTo(Locator.SurveyQuestionsViewModelKey,Locator.PermitPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> GangClear
        {
            get
            {
                return _gangClear ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGCLEAR;
                    NavigationalParameters.SurveyType = "GangClear";
                    //NavigationalParameters.Questions = _assignmentService.GetSurveyQuestions("gang").ToList();
                    NavigationalParameters.NavigationService = _navigationService;
                    NavigationalParameters.PassedInfo = new SelectStreetPage();
                    _navigationService.NavigateTo(Locator.SelectStreetViewModelKey, Locator.MainListPageViewModelKey);


                    //NavigationalParameters.NavigationParameter =
                    //    new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                    //        NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                    //        NavigationalParameters.CurrenSelectedJob);

                    //_navigationService.NavigateTo(Locator.SurveyQuestionsViewModelKey,
                    //    Locator.PermitPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> SiteClear
        {
            get
            {
                return _siteClear ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITECLEAR;
                    NavigationalParameters.SurveyType = "SiteClear";
                    //NavigationalParameters.Questions = _assignmentService.GetSurveyQuestions("gang").ToList();
                    NavigationalParameters.NavigationService = _navigationService;
                    NavigationalParameters.PassedInfo = new SelectStreetPage();
                    _navigationService.NavigateTo(Locator.SelectStreetViewModelKey, Locator.MainListPageViewModelKey);


                    //NavigationalParameters.NavigationParameter =
                    //    new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                    //        NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                    //        NavigationalParameters.CurrenSelectedJob);

                    //_navigationService.NavigateTo(Locator.SurveyQuestionsViewModelKey,
                    //    Locator.PermitPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> JobPack
        {
            get
            {
                return _jobPack ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTDOCS;

                    NavigationalParameters.NavigationParameter =
                        new Tuple<string, JobData4Tablet>("JobPackDocs",
                            NavigationalParameters.CurrentSelectedJob);

                    _navigationService.NavigateTo(Locator.DocumentListingPageViewModelKey,
                        NavigationalParameters.NavigationParameter);
                });
            }
        }

        public RelayCommand<string> OperativeDocs
        {
            get
            {
                return _operativeDocs ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.OPERATIVEDOCS;

                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                    {
                        NavigationalParameters.NavigationParameter =
                            new Tuple<string, JobData4Tablet>("Training",
                                NavigationalParameters.CurrentSelectedJob);
                        _navigationService.NavigateTo(Locator.DocumentListingPageViewModelKey,
                            NavigationalParameters.NavigationParameter);
                    }
                    else
                    {
                        NavigationalParameters.NavigationParameter =
                            new Tuple<string, JobData4Tablet>("OperativeDocs",
                                NavigationalParameters.CurrentSelectedJob);
                        _navigationService.NavigateTo(Locator.SignaturePageViewModelKey, null);
                    }
                });
            }
        }

        public RelayCommand<string> CompanyDocs
        {
            get
            {
                return _companyDocs ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMPANYDOCS;

                    NavigationalParameters.NavigationParameter =
                        new Tuple<string, JobData4Tablet>("CompanyDocs",
                            NavigationalParameters.CurrentSelectedJob);


                    _navigationService.NavigateTo(Locator.DocumentListingPageViewModelKey,
                        NavigationalParameters.NavigationParameter);
                });
            }
        }

        public RelayCommand<string> JobPictures
        {
            get
            {
                return _jobPictures ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTIMAGES;

                    NavigationalParameters.NavigationService = _navigationService;
                    NavigationalParameters.ReturnPage = Locator.MenuPageViewModelKey;
                    NavigationalParameters.PassedInfo = new ProjectImages();

                    _navigationService.NavigateTo(Locator.ProjectImagesViewModelKey, Locator.MenuPageViewModelKey);


                    //_navigationService.NavigateTo(Locator.JobPicturesKey,
                    //    NavigationalParameters.NavigationParameter);
                });
            }
        }

        public RelayCommand<string> MySiteRiskAss
        {
            get
            {
                return _mySiteRiskAss ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITEINSPECTION;

                   // NavigationalParameters.NavigationParameter =
                       // new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                            //NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                           // NavigationalParameters.CurrentSelectedJob);
                    _navigationService.NavigateTo(Locator.DailySiteInspectionViewModelKey, NavigationalParameters.NavigationParameter);
                });
            }
        }

        public RelayCommand<string> MyPlant
        {
            get
            {
                return _myPlant ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.PLANT;

                    if (NavigationalParameters.AppType != NavigationalParameters.AppTypes.SUPERVISOR)
                        NavigationalParameters.NavigationParameter = "GangView";
                    else NavigationalParameters.NavigationParameter = "";
                    _navigationService.NavigateTo(Locator.MyPlantScreenPageViewModelKey,
                        NavigationalParameters.NavigationParameter);
                });
            }
        }

        public RelayCommand<string> VisitorsLog
        {
            get
            {
                return _visitorsLog ??= new RelayCommand<string>(e =>
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.VISITORSLOG;

                    //NavigationalParameters.NavigationParameter =
                      //  new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                         //   NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                         //   NavigationalParameters.CurrentSelectedJob);
                    _navigationService.NavigateTo(Locator.VisitorLogViewModelKey,
                        Locator.MenuPageViewModelKey);
                });
            }
        }

        public RelayCommand<string> Test
        {
            get
            {
                return _approveMeasure2 ??= new RelayCommand<string>(e =>
                {
                  //  NavigationalParameters.NavigationParameter =
                       // new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                          //  NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                          //  NavigationalParameters.CurrentSelectedJob);
                    _navigationService.NavigateTo(e, NavigationalParameters.NavigationParameter);
                });
            }
        }

        public RelayCommand<string> Upload
        {
            get
            {
                return _upload ??= new RelayCommand<string>(async e =>
                {
                    var checksComplete = true;

                    var approvals = new SupervisorApprovals();

                    var itemsToApprove = approvals.GetItemsForApproval(NavigationalParameters.CurrentSelectedJob);
                    var approvalInProgress =
                        approvals.ApprovalInProgress(NavigationalParameters.CurrentSelectedJob);

                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER)
                    {
                        if (!CrossedUtilities)
                        {
                            try
                            {
                                checksComplete = false;
                                await _dialogService.ShowMessage(
                                    "Failed to save the daily measures as utilities crossed needs to be completed",
                                    "Incomplete!");
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                            }
                        }
                        else if (NavigationalParameters.VisitorsStillToBeLoggedOut)
                        {
                            checksComplete = false;
                            await _dialogService.ShowMessage(
                                "Failed to save the daily measures as visitors must be signed out",
                                "Incomplete!");
                        }
                        else if (!InputDiaryRecord)
                        {
                            checksComplete = false;
                            await _dialogService.ShowMessage(
                                "Failed to save the daily measures as daily diary need to be complete",
                                "Incomplete!");
                        }
                        else if (!LabourFiles)
                        {
                            checksComplete = false;
                            await _dialogService.ShowMessage(
                                "Failed to save the daily measures as time sheets need to be completed",
                                "Incomplete!");
                        }
                        else if (!NavigationalParameters.CurrentSelectedJob.DailyChecksCompleted)
                        {
                            checksComplete = false;
                            await _dialogService.ShowMessage(
                                "Failed to save the daily measures need to be completed",
                                "Incomplete!");
                        }                        
                    }

                    if (checksComplete)
                    {
                        var dailyUploadData = _jobService.GetDailyUploadData(
                            new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                                NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                                NavigationalParameters.CurrentSelectedJob));


                        if ((dailyUploadData.DailyDiary == null ||
                             string.IsNullOrEmpty(dailyUploadData.DailyDiary.NotesText)) &&
                            NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER)
                        {
                            await _dialogService.ShowMessage(
                                "Daily diary must be completed, Please Try again",
                                "Error!");

                            SetSpinner(false);
                        }
                        else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER
                                 && dailyUploadData.LabourFiles.Any(x =>
                                     x.TimeLeftSite == DateTime.Parse("1900-01-01 00:00:00.000")
                                     && x.TimeLeftSite == DateTime.Parse("1900-01-01 00:00:00.000")
                                     && x.TimeLeftSite == DateTime.Parse("1900-01-01 00:00:00.000")))
                        {
                            await _dialogService.ShowMessage(
                                "Please complete Time sheets ensuring both start and finish times as required!",
                                "Error!");
                        }
                        else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER && EndPointsAvailable
                                                                            && string.IsNullOrEmpty(
                                                                                dailyUploadData.DailyDiary
                                                                                    .StartAddress.Trim())
                                                                            && (dailyUploadData.DailyDiary
                                                                                    .StartEndpointId == 0
                                                                                || dailyUploadData.DailyDiary
                                                                                    .EndEndpointId == 0))
                        {
                            await _dialogService.ShowMessage(
                                "Please complete start and end of day diary details",
                                "Error!");
                        }
                        else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER && !EndPointsAvailable
                                                                            && (string.IsNullOrEmpty(
                                                                                    dailyUploadData.DailyDiary
                                                                                        .StartAddress.Trim())
                                                                                || string.IsNullOrEmpty(
                                                                                    dailyUploadData.DailyDiary
                                                                                        .EndAddress.Trim())))
                        {
                            await _dialogService.ShowMessage(
                                "Please complete start and end of day diary details",
                                "Error!");
                        }
                        else if
                        (dailyUploadData.DailyDiary == null
                         && dailyUploadData.ClaimedFiles.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.Audits.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.Assignments.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.Permits.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.BlockageList.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.Permits.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.VisitorLogs.All(x => x.RemoteTableId != 0)
                         && dailyUploadData.BlockageList.All(x => x.RemoteTableId != 0))
                        {
                            await _dialogService.ShowMessage(
                                "There is nothing to upload",
                                "Error!");
                        }
                        else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR && itemsToApprove &&
                                 approvalInProgress)
                        {
                            await _dialogService.ShowMessage(
                                "Approvals required before upload",
                                "Approvals required!");
                        }
                        else
                        {
                            if (!dailyUploadData.ClaimedFiles.Any())
                            {
                                await _dialogService.ShowMessage(
                            "Warning!! No measures have been entered can you confirm that you have no measures or enter any measures that you have missed",
                            "NO MEASURES HAVE BEEN ENTERED", "Confirm",
                            "Measures", async ok =>
                            {
                                if (ok)
                                {
                                    try
                                    {
                                        SetSpinner(true);

                                        var success = await _jobService.SaveDailyInputAsync(dailyUploadData);

                                        if (!success)
                                        {
                                            SetSpinner(false);

                                            await _dialogService.ShowMessage(
                                                "Failed to save the daily measures please contact support",
                                                "Error!");
                                        }
                                        else
                                        {
                                            SetSpinner(false);

                                            NavigationalParameters.NavigationParameter =
                                                new Tuple<Person, List<Models.Assignment>>(
                                                    NavigationalParameters.LoggedInUser,
                                                    new List<Models.Assignment>()); // NavPassedData;

                                                 _navigationService.NavigateTo(Locator.MainListPageViewModelKey,
                                                  Locator.MenuPageViewModelKey);
                                        }

                                        SetSpinner(false);
                                    }
                                    catch (Exception ex)
                                    {
                                        var error = ex.ToString();
                                        await _dialogService.ShowMessage(
                                            "The daily measures Have failed To Save, Please Try again",
                                            "Error!");

                                        SetSpinner(false);
                                    }

                                }
                                else
                                {
                                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER)
                                    {
                                        _navigationService.NavigateTo(
                                        Locator.MeasureListPageViewModelKey,
                                        Locator.MenuPageViewModelKey);
                                    }
                                    else
                                    {
                                        _navigationService.NavigateTo(
                                      Locator.TeamOptionsViewModelKey,
                                      Locator.MenuPageViewModelKey);
                                    }
                                }
                            });

                            }
                            else
                            {
                                try
                                {
                                    SetSpinner(true);

                                    var success = await _jobService.SaveDailyInputAsync(dailyUploadData);

                                    if (!success)
                                    {
                                        SetSpinner(false);

                                        await _dialogService.ShowMessage(
                                            "Failed to save the daily measures please contact support",
                                            "Error!");
                                    }
                                    else
                                    {
                                        SetSpinner(false);

                                        NavigationalParameters.NavigationParameter =
                                            new Tuple<Person, List<Models.Assignment>>(
                                                NavigationalParameters.LoggedInUser,
                                                new List<Models.Assignment>()); // NavPassedData;

                                        _navigationService.NavigateTo(Locator.MainListPageViewModelKey,
                                         Locator.MenuPageViewModelKey);
                                    }

                                    SetSpinner(false);
                                }
                                catch (Exception ex)
                                {
                                    var error = ex.ToString();
                                    await _dialogService.ShowMessage(
                                        "The daily measures Have failed To Save, Please Try again",
                                        "Error!");

                                    SetSpinner(false);
                                }
                            }



                        }
                    }
                });
            }
        }

        public RelayCommand RefreshPageChecks => new RelayCommand(() =>
        {
            NavigationalParameters.VisitorsStillToBeLoggedOut = _jobService
                .GetVisitors(NavigationalParameters.CurrentSelectedJob)
                .Any(x => x.OffSiteTime == DateTime.Parse("1/1/1900 00:00:00"));

            // NavigationalParameters.StartOfDayPictureTaken = _jobService.CheckStartOfDayPictureExsits();
        });

        public RelayCommand<object> ScreenLoaded4MenuSupervisor =>
            _screenLoaded4MenuSupervisor ??= new RelayCommand<object>(e =>
            {
                //LoggedInUser = UserService.GetLoggedInUser();


                NavigationalParameters.ProjectJobs = _jobService.GetAllJobs()
              .Where(x =>
                  x.SupervisorId ==
                  NavigationalParameters.LoggedInUser.FocusId &&
                  x.ProjectName ==
                  NavigationalParameters
                      .CurrentSelectedJob.ProjectName)
              .ToList();

                ProjectName =
                        NavigationalParameters
                            .CurrentSelectedJob.ProjectName ??
                        ProjectName;

                NavigationalParameters
                        .CurrentSelectedJob.JobGang =
                    _jobService.GetGang(NavigationalParameters
                        .CurrentSelectedJob);


                UploadButtonVisable = _jobService.CheckUploadVisibility(NavigationalParameters.CurrentSelectedJob);


                AllOperativesToJob();

            });

        public RelayCommand Refresh => new RelayCommand(() =>
        {
            //sw.Restart();
           // var joblist = ;
            NavigationalParameters.ProjectJobs = _jobService.GetAllJobs().FindAll(x => x.ProjectName == NavigationalParameters.CurrentSelectedJob.ProjectName);
            ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
            //AllOperativesToJob();
            var approvals = new SupervisorApprovals();

            var itemsToApprove = approvals.GetItemsForApproval(NavigationalParameters.CurrentSelectedJob);
            var approvalInProgress = approvals.ApprovalInProgress(NavigationalParameters.CurrentSelectedJob);


            if (itemsToApprove && approvalInProgress)
            {
                //Items partially approved
                SelectGangHidden = true;
                ApprovalUploadRequired = false;
            }
            else if (!itemsToApprove && approvalInProgress)
            {
                //Items approved, Upload Required
                SelectGangHidden = false;
                ApprovalUploadRequired = true;
            }
            else
            {
                //Items not found, OK to navigate back to Menu
                SelectGangHidden = false;
                ApprovalUploadRequired = false;
            }

            Debug.WriteLine($"Refresh: {_sw.ElapsedMilliseconds / 1000f}");
        });

        public RelayCommand SelectGangs => _selectGangs ??= new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGSELECTION;

           // NavigationalParameters.NavigationParameter = _projectJobs;
            NavigationalParameters.PassedInfo = Locator.MenuPageViewModelKey;
            _navigationService.NavigateTo(Locator.MainListPageViewModelKey,
                Locator.MenuPageViewModelKey);
        });

        public RelayCommand NavToSupervisorTimeSheets => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTDIARIES;

            NavigationalParameters.CurrentSelectedJob =  NavigationalParameters.ProjectJobs.FirstOrDefault(x => x.SupervisorId ==
                                                                                                                NavigationalParameters
                                                                                                                    .LoggedInUser.FocusId);
            NavigationalParameters.CurrentSelectedJob.GangLeaderId = 0;
            NavigationalParameters.NavigationParameter = NavigationalParameters.CurrentSelectedJob;
            NavigationalParameters.PassedInfo = Locator.MenuPageViewModelKey;
            _navigationService.NavigateTo(Locator.InputSupervisorTimeSheetViewModelKey, Locator.MenuPageViewModelKey);
        });

        public RelayCommand NavToSupervisorDiary => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUPERVISORINPUTDIARIES;
            NavigationalParameters.CurrentSelectedJob =  NavigationalParameters.ProjectJobs.FirstOrDefault(x => x.SupervisorId ==
                                                                                                                NavigationalParameters
                                                                                                                    .LoggedInUser.FocusId);
            NavigationalParameters.CurrentSelectedJob.GangLeaderId = 0;
            NavigationalParameters.NavigationParameter = NavigationalParameters.CurrentSelectedJob;
            NavigationalParameters.PassedInfo = Locator.MenuPageViewModelKey;
            _navigationService.NavigateTo(Locator.DailyDiaryViewModelKey, Locator.MenuPageViewModelKey);
        });

        public RelayCommand NavigateToProjectSummary => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUMMARY;

            NavigationalParameters.CurrentSelectedJob =  NavigationalParameters.ProjectJobs.FirstOrDefault(x => x.SupervisorId ==
                                                                                                                NavigationalParameters
                                                                                                                    .LoggedInUser.FocusId);
            NavigationalParameters.CurrentSelectedJob.JobProjectSummary =
                _assignmentService.GetProjectSummary(NavigationalParameters.CurrentSelectedJob.QuoteNumber);
            NavigationalParameters.NavigationParameter = NavigationalParameters.CurrentSelectedJob;
            NavigationalParameters.PassedInfo = Locator.MainListPageViewModelKey;
            _navigationService.NavigateTo(Locator.ProjectSummaryViewModelKey, Locator.MainListPageViewModelKey);
        });

        public RelayCommand NavigateToPlantScreen => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUPERVISORPLANT;
            //Only navigate if there is a job for today.
            NavigationalParameters.CurrentSelectedJob =  NavigationalParameters.ProjectJobs.FirstOrDefault(x =>
                x.JobDate.Date == DateTime.Now.Date && x.SupervisorId ==
                NavigationalParameters.LoggedInUser.FocusId);
            //if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR) NavigationalParameters.CurrenSelectedJob =_projectJobs[0];
            NavigationalParameters.NavigationParameter = "";
            if (NavigationalParameters.CurrentSelectedJob != null)
            {
                _navigationService.NavigateTo(Locator.MyPlantScreenPageViewModelKey, Locator.MenuPageViewModelKey);
            }
            else
            {
                Debug.WriteLine("No Plant available for today");
                _dialogService.ShowMessage("No Plant available for today", "");
            }
        });

        public RelayCommand NavigateToCvi => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.CVI;

            NavigationalParameters.CurrentSelectedJob =
                NavigationalParameters.ProjectJobs.FirstOrDefault(x => x.SupervisorId == NavigationalParameters.LoggedInUser.FocusId);
            NavigationalParameters.CurrentSelectedJob.JobProjectSummary =
                _assignmentService.GetProjectSummary(NavigationalParameters.CurrentSelectedJob.QuoteNumber);
            //NavigationalParameters.NavigationParameter = NavigationalParameters.CurrenSelectedJob;
            // NavigationalParameters.PassedInfo = Locator.MenuPageViewModelKey;
          //  _navigationService.NavigateTo(Locator.RaiseCvi1Key, Locator.MenuPageViewModelKey);
        });

        public RelayCommand SelectAuditGangs => _selectAuditGangs ??= new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.AUDIT;
           // NavigationalParameters.NavigationParameter = _projectJobs;

           switch (NavigationalParameters.AppType)
           {
               case NavigationalParameters.AppTypes.SUPERVISOR:
                   _navigationService.NavigateTo(Locator.MainListPageViewModelKey,"ProjectAudit");
                   break;
           }
        });

        public RelayCommand NavigateToProjectDiaries => _navigateToProjectDiaries ??= new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTDIARIES;

            var timeSheet =
                UserService.GetSupervisorLabour(NavigationalParameters
                    .LoggedInUser.FocusId);
            if (timeSheet == null)
            {
                _dialogService.ShowMessage(
                    "Please enter a daily diary start time first.",
                    "No Diary Start Time");
            }
            else if (timeSheet.IsSubmited)
            {
                _dialogService.ShowMessage(
                    "Today's Diary has already been completed.",
                    "Diary Uploaded");
            }
            else
            {
                NavigationalParameters.PassedInfo =
                    Locator.MenuPageViewModelKey;
               // NavigationalParameters.NavigationParameter =
                //    _projectJobs;
                _navigationService.NavigateTo(
                    Locator.InputSupervisorTimeSheetViewModelKey,
                    Locator.MenuPageViewModelKey);
            }
        });

        public RelayCommand NavToFormsView => new RelayCommand(() =>
        {
            NavigationalParameters.NavigationService = _navigationService;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.REGISTERUTILITYSTRIKE;
            NavigationalParameters.PassedInfo = new RegisterNewDamagePage();
            _navigationService.NavigateTo(Locator.RegisterNewDamageViewModelKey, Locator.MenuPageViewModelKey);
        });

        public RelayCommand NavToTeamOptions => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.APPROVALS;
            _navigationService.NavigateTo(Locator.TeamOptionsViewModelKey);
        });


        public RelayCommand GoToLogout => new RelayCommand(() =>
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.LOGIN;

            // _dialogService.ShowMessage("Timeout has occured please log in to continue.", "Timed Out");

            _navigationService.NavigateTo(Locator.LoginPageViewModelKey);
        });

        public void GoBack()
        {
            _navigationService.NavigateTo(Locator.MainListPageViewModelKey, Locator.MenuPageViewModelKey);
        }

        private void Authorise()
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.AUTHORISATION;
            //var job = NavPassedData.Item3;
            _navigationService.NavigateTo(Locator.SignaturePageViewModelKey,
                new Tuple<JobData4Tablet, AuthorisationDetail>(NavigationalParameters.CurrentSelectedJob, NavigationalParameters.AuthDetail));
        }

        public bool ConfirmAuthorised()
        {
            return !string.IsNullOrEmpty(NavigationalParameters.AuthDetail?.AuthorisedName);
        }

        //Gets Visibility for button
        public bool GetVisible(Visibility.OptionType optionType)
        {
            return Visibility.GetVisibility(optionType, NavigationalParameters.LoggedInUser,
                NavigationalParameters.CurrentSelectedJob.JobDate);
        }

        public void SetSpinner(bool onOrOff)
        {
            SpinnerAction = onOrOff ? "SpinnerGo" : "SpinnerStop";
            OnPropertyChanged("SpinnerAction");
        }

        /// <summary>
        ///     Adds all operatives in the project to the current selected job
        ///     For use with Operative Docs
        /// </summary>
        private void AllOperativesToJob()
        {
            var allGangs = new List<GangMember>();
            var super = NavigationalParameters.LoggedInUser;

            try
            {
                foreach (var job in  NavigationalParameters.ProjectJobs)
                {
                    job.JobGang = _jobService.GetGang(job);
                    if (job.JobGang.GangMembers != null)
                        foreach (var member in job.JobGang.GangMembers)
                            if (allGangs.All(x => x.Id != member.Id))
                                allGangs.Add(member);
                }

                //NavigationalParameters.CurrenSelectedJob = _selectedJobData;
                NavigationalParameters.CurrentSelectedJob.JobGang.GangMembers = allGangs;
            }
            catch (Exception e)
            {
                var error = e.ToString();
            }

        }
    }
}