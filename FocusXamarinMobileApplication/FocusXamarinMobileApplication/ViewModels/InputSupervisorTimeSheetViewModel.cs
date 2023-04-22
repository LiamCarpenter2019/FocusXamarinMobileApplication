namespace FocusXamarinMobileApplication.ViewModels
{
    public class InputSupervisorTimeSheetViewModel : FBaseViewModel
    {
      
        private readonly Jobs _jobService;
                private readonly Users _userService;
        private readonly WebApi _webApi;
        private RelayCommand<string> _returnToMain;
        private RelayCommand<string> _screenLoadedCommand4SupervisorTimeSheet;

        private string _supervisorEndTimeString;

        private string _supervisorStartTimeString;
        private RelayCommand<DateTime> _updateSelectedTime;
        private RelayCommand<string> _updateSupervisorTimesheets;
        private bool _uploadButtonEnabled;
        private RelayCommand<string> _uploadSupervisorDailyDiary;
        public string PageHome;

        public InputSupervisorTimeSheetViewModel()
        {
          
            _userService = new Users();
            _webApi = new WebApi();
            _jobService = new Jobs();
        }

        public string NavPassedInfo { get; set; }

        public List<JobData4Tablet> JobList { get; set; }
        public Person LoggedInUser { get; set; }

        public UserDailyTime SupervisorTimesheetList { get; set; }
        public UserDailyTime SupervisorDailyDiary { get; private set; }

        public string SupervisorStartTimeString
        {
            get => _supervisorStartTimeString;
            set
            {
                if (TimeSpan.TryParse(value, out var v))
                {
                    _supervisorStartTimeString = v.ToString(@"hh\:mm");
                    SupervisorStartTime = v;
                }
                else
                {
                    _supervisorStartTimeString = "";
                }

                OnPropertyChanged("SupervisorStartTimeString");
            }
        }

        public string SupervisorEndTimeString
        {
            get => _supervisorEndTimeString;
            set
            {
                if (TimeSpan.TryParse(value, out var v))
                {
                    _supervisorEndTimeString = v.ToString(@"hh\:mm");
                    SupervisorEndTime = v;
                }
                else
                {
                    _supervisorEndTimeString = "";
                }

                OnPropertyChanged("SupervisorEndTimeString");
            }
        }

        public TimeSpan? SupervisorStartTime { get; set; }
        public TimeSpan? SupervisorEndTime { get; set; }
        public string SupervisorComments { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDate { get; set; }
        public DateTime SelectedDateTime { get; set; } = DateTime.Now;

        public bool UploadButtonHidden { get; private set; }

        public bool UploadButtonEnabled
        {
            get => _uploadButtonEnabled;
            set
            {
                _uploadButtonEnabled = value;
                UploadButtonHidden = !value;
             
                OnPropertyChanged("UploadButtonHidden");
            }
        }

        public RelayCommand<string> ScreenLoadedCommand4SupervisorTimeSheet
        {
            get
            {
                return _screenLoadedCommand4SupervisorTimeSheet ??= new RelayCommand<string>(e =>
                {
                    PageHome = e;

                    SupervisorStartTimeString = "";
                    SupervisorEndTimeString = "";
                    SupervisorStartTime = null;
                    SupervisorEndTime = null;

                    JobList = NavigationalParameters.NavigationParameter as List<JobData4Tablet>;

                    LoggedInUser = NavigationalParameters.LoggedInUser;

                    SupervisorTimesheetList =
                        _userService.GetSupervisorLabour(LoggedInUser.FocusId) ?? new UserDailyTime();

                    ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

                    UploadButtonEnabled = false;

                    if (PageHome == "Main")
                    {
                        if (SupervisorTimesheetList != null)
                        {
                            if (SupervisorTimesheetList?.InTime != null)
                                SupervisorStartTime = (TimeSpan) SupervisorTimesheetList?.InTime;
                            SupervisorStartTimeString = SupervisorTimesheetList?.InTime.ToString();

                            if (SupervisorTimesheetList?.OutTime != null)
                                SupervisorEndTime = (TimeSpan) SupervisorTimesheetList?.OutTime;
                            SupervisorEndTimeString = SupervisorTimesheetList?.OutTime.ToString();

                            SupervisorComments = SupervisorTimesheetList?.UserDailyTimeNotesList
                                .FirstOrDefault()?.Notes;
                        }

                        if (ValidateTimes() && !string.IsNullOrEmpty(SupervisorComments) &&
                            !SupervisorTimesheetList.IsSubmited) UploadButtonEnabled = true;
                        else UploadButtonEnabled = false;
                    }
                    else
                    {
                        SelectedDateTime = DateTime.Now;
                        SupervisorStartTimeString = "";
                        SupervisorEndTimeString = "";
                        SupervisorStartTime = TimeSpan.Parse("00:00:00");
                        SupervisorEndTime = TimeSpan.Parse("00:00:00");
                        SupervisorComments = "";
                        ProjectName = JobList?.FirstOrDefault()?.ProjectName;
                        ClientName = JobList?.FirstOrDefault()?.ClientName;
                    }

                    OnPropertyChanged("SupervisorTimesheetList");
                });
            }
        }

        public string ClientName { get; set; }

        public RelayCommand<DateTime> UpdateSelectedTime
        {
            get
            {
                return _updateSelectedTime ??= new RelayCommand<DateTime>(e => { SelectedDateTime = e; });
            }
        }

        public RelayCommand<string> UpdateSupervisorTimesheets
        {
            get
            {
                return _updateSupervisorTimesheets ??= new RelayCommand<string>( async e =>
                {
                    if (!string.IsNullOrEmpty(SupervisorStartTimeString))
                    {
                        SupervisorDailyDiary =
                            _userService.GetSupervisorLabour(LoggedInUser.FocusId) ?? new UserDailyTime
                            {
                                CreatedBy = LoggedInUser.FocusId,
                                CreatedOn = DateTime.Now,
                                DailyTimeId = 0,
                                Date = DateTime.Now,
                                InTime = SupervisorStartTime,
                                OutTime = SupervisorEndTime,
                                IsApproved = false,
                                IsSubmited = false,
                                LastModifiedBy = LoggedInUser.FocusId,
                                LastModifiedOn = DateTime.Now,
                                UserId = LoggedInUser.FocusId,
                                IsNightShift = SupervisorEndTime < SupervisorStartTime
                            };

                        if (!SupervisorDailyDiary.IsSubmited)
                        {
                            if (e == "Main")
                            {
                                if (SupervisorDailyDiary.UserDailyTimeNotesList.Count > 0)
                                {
                                    var note = SupervisorDailyDiary.UserDailyTimeNotesList?.FirstOrDefault();
                                    if (note != null) note.Notes = SupervisorComments;

                                    if (!string.IsNullOrEmpty(SupervisorStartTimeString))
                                        SupervisorDailyDiary.InTime = SupervisorStartTime;
                                    if (!string.IsNullOrEmpty(SupervisorEndTimeString))
                                        SupervisorDailyDiary.OutTime = SupervisorEndTime;
                                }
                                else
                                {
                                    SupervisorDailyDiary.UserDailyTimeNotesList?.Add(new UserDailyTimeNotes
                                    {
                                        DailyTimeId = 0,
                                        RemoteId = SupervisorDailyDiary.RemoteId,
                                        Notes = SupervisorComments
                                    });
                                }

                                _userService.UpdateSupervisorDailyLabour(SupervisorDailyDiary);

                                if (ValidateTimes() && !SupervisorDailyDiary.IsSubmited &&
                                    !SupervisorDailyDiary.IsSubmited)
                                {
                                    if (!string.IsNullOrEmpty(SupervisorComments))
                                    {
                                        UploadButtonEnabled = true;
                                       Alert(
                                            "Diary has been submitted, please press upload to complete.",
                                            "Please Upload");
                                    }
                                    else
                                    {
                                       Alert("Please add comments to upload", "Error!");
                                    }
                                }
                                else
                                {
                                    UploadButtonEnabled = false;

                                    await NavigateTo(new MainListPage());
                                }
                            }
                            else
                            {
                                if (ValidateTimes() && !string.IsNullOrEmpty(SupervisorComments))
                                {
                                    var userDailyProjectTime = new UserDailyProjectTimes
                                    {
                                        DailyTimeId = 0,
                                        RemoteId = SupervisorDailyDiary.RemoteId,
                                        ClientName = JobList?.FirstOrDefault()?.ClientName,
                                        CreatedBy = LoggedInUser.FocusId,
                                        CreatedOn = DateTime.Now,
                                        LastModifiedBy = LoggedInUser.FocusId,
                                        LastModifiedOn = DateTime.Now,
                                        ProjectName = ProjectName,
                                        UserId = LoggedInUser.FocusId,
                                        ProjectTimeId = 0,
                                        QuoteId = JobList?.FirstOrDefault()?.QuoteNumber,
                                        StartTime = SupervisorStartTime,
                                        Endtime = SupervisorEndTime
                                    };

                                    var userDailyProjectNote = new DailyProjectNotes
                                    {
                                        Notes = SupervisorComments,
                                        ProjectTimeId = 0,
                                        RemoteId = userDailyProjectTime.RemoteId,
                                        RemoteProjectTimeId = userDailyProjectTime.RemoteProjectTimeId
                                    };

                                    userDailyProjectTime?.DailyProjectNotesList?.Add(userDailyProjectNote);

                                    SupervisorDailyDiary?.UserDailyProjectTimesList?.Add(userDailyProjectTime);

                                    _userService.UpdateSupervisorDailyProjectLabour(SupervisorDailyDiary);

                                    await NavigateTo(new MenuSelectionPage());
                                }
                                else
                                {
                                   Alert(
                                        "Start time, end time & comments must be complete before submission. Please complete and try again",
                                        "Error!");
                                }
                            }
                        }
                        else
                        {
                           Alert("Diary has already been uploaded for today",
                                "Unable to submit");
                        }
                    }
                    else
                    {
                       Alert(
                            "Start Time must be complete before submission; Please complete and try again",
                            "Error!");
                    }
                });
            }
        }

        public RelayCommand<string> ReturnToMain
        {
            get
            {
                return _returnToMain ??= new RelayCommand<string>(async e =>
                {
                    if (e == "Main")
                        await NavigateTo(new MainListPage());
                    else
                        await NavigateTo(new MenuSelectionPage()); 
                });
            }
        }

        public RelayCommand<string> UploadSupervisorDailyDiary
        {
            get
            {
                return _uploadSupervisorDailyDiary ??= new RelayCommand<string>(async e =>
                {
                    var supervisorDiariesToUpload = _userService.GetSupervisorLabour(LoggedInUser.FocusId);

                    if (supervisorDiariesToUpload?.InTime != null &&
                        supervisorDiariesToUpload?.OutTime != null &&
                        !string.IsNullOrEmpty(supervisorDiariesToUpload.UserDailyTimeNotesList?.FirstOrDefault()
                            ?.Notes))
                    {
                        UploadButtonEnabled = false;

                        var success = await _webApi.UploadSupervisorDailyDiaries(supervisorDiariesToUpload);

                        if (success)
                        {
                            supervisorDiariesToUpload.IsSubmited = true;
                            _userService.UpdateSupervisorDailyLabour(supervisorDiariesToUpload);
                            _jobService.UpdateSupervisorDailyDiaries(supervisorDiariesToUpload);

                            await Alert("Upload","Upload Succesful");

                            await NavigateBack();
                        }
                        else
                        {
                            UploadButtonEnabled = true;
                        }
                    }
                    else
                    {
                        await Alert("Error!",
                            "Start And End Time must be complete before submission; Please complete and try again"
                            );
                    }
                });
            }
        }

        private bool ValidateTimes()
        {
            if (!string.IsNullOrEmpty(SupervisorStartTimeString) &&
                !string.IsNullOrEmpty(SupervisorEndTimeString)) return true;
            return false;
        }
    }
}