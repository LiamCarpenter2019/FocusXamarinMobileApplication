using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class SupervisorDiaryPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;
    private readonly SupervisorApprovalsPageViewModel _supervisorService;
    private readonly Users _userService;
    private readonly WebApi _webApi;

    private RelayCommand<string> _returnToMain;
    private RelayCommand<string> _screenLoadedCommand4SupervisorTimeSheet;
    private RelayCommand<string> _updateSupervisorTimesheets;
    private RelayCommand<string> _uploadSupervisorDailyDiary;

    public SupervisorDiaryPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        _assignmentService = new Assignments();
        _supervisorService = new SupervisorApprovalsPageViewModel();
        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
        UploadButtonEnabled = false;
        ShowUploadButton = false;
    }


    public string _projectDate { get; set; } =
        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    public string _diaryText { get; set; }

    public string DiaryText
    {
        get => _diaryText;
        set
        {
            _diaryText = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan _timeOnSite { get; set; }

    public TimeSpan TimeOnSite
    {
        get => _timeOnSite;
        set
        {
            _timeOnSite = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan _timeOffSite { get; set; }

    public TimeSpan TimeOffSite
    {
        get => _timeOffSite;
        set
        {
            _timeOffSite = value;
            OnPropertyChanged("TimeOfSite");
        }
    }

    public UserDailyTime _diary { get; set; }

    public UserDailyTime Diary
    {
        get => _diary;
        set
        {
            _diary = value;
            OnPropertyChanged();
        }
    }

    public UserDailyProjectTimes _userDailyProjectTime { get; set; }

    public UserDailyProjectTimes UserDailyProjectTime
    {
        get => _userDailyProjectTime;
        set
        {
            _userDailyProjectTime = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<UserDailyProjectTimes> _projectDiaries { get; set; }

    public ObservableCollection<UserDailyProjectTimes> ProjectDiaries
    {
        get => _projectDiaries;
        set
        {
            _projectDiaries = value;
            OnPropertyChanged();
        }
    }


    public bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }


    public bool _showSection3 { get; set; }

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection4 { get; set; }

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged();
        }
    }

    private bool _uploadButtonEnabled { get; set; }

    public bool UploadButtonEnabled
    {
        get => _uploadButtonEnabled;
        set
        {
            _uploadButtonEnabled = value;

            OnPropertyChanged("UploadButtonHidden");
        }
    }

    public bool _showUploadButton { get; set; }

    public bool ShowUploadButton
    {
        get => _showUploadButton;
        set
        {
            _showUploadButton = value;

            OnPropertyChanged();
        }
    }

    public bool _showSaveButton { get; set; }

    public bool ShowSaveButton
    {
        get => _showSaveButton;
        set
        {
            _showSaveButton = value;

            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        DiaryText = "";
        TimeOnSite = DateTime.Now.TimeOfDay;
        TimeOffSite = DateTime.Now.TimeOfDay;
        Diary = _userService.GetSupervisorLabour(NavigationalParameters.LoggedInUser.FocusId);

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SUPERVISORINPUTDIARIES)
        {
            DiaryText = Diary?.UserDailyTimeNotesList?.FirstOrDefault()?.Notes;
            TimeOnSite = (TimeSpan)Diary?.InTime;
            TimeOffSite = (TimeSpan)Diary?.OutTime;
            ProjectDiaries = new ObservableCollection<UserDailyProjectTimes>(App.Database
                .GetItems<UserDailyProjectTimes>().Where(x => x.RemoteId == Diary.RemoteId).ToList());
            UploadButtonEnabled = false;
            ShowUploadButton = false;
            ShowSaveButton = true;
        }
        else
        {
            UploadButtonEnabled = false;
            ShowUploadButton = false;
            DiaryText = "";
            ProjectDiaries = new ObservableCollection<UserDailyProjectTimes>(App.Database
                .GetItems<UserDailyProjectTimes>()?
                .Where(x => x.RemoteId == Diary.RemoteId));
        }
    });


    public RelayCommand Submit => new(async () =>
    {
        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SUPERVISORINPUTDIARIES)
        {
            if (Diary.UserDailyTimeNotesList != null &&
                Diary.UserDailyTimeNotesList.Any(x => x.RemoteId == Diary.RemoteId))
                Diary.UserDailyTimeNotesList.FirstOrDefault(x => x.RemoteId == Diary.RemoteId).Notes = DiaryText;
            else
                Diary.UserDailyTimeNotesList = new List<UserDailyTimeNotes>
                {
                    new()
                    {
                        RemoteId = Diary.RemoteId,
                        Id = 0,
                        DailyTimeId = 0,
                        Notes = DiaryText
                    }
                };

            Diary.InTime = TimeOnSite;

            Diary.OutTime = TimeOffSite;

            if (!Diary.IsSubmited)
            {
                if (!string.IsNullOrEmpty(DiaryText) && TimeOnSite != null && TimeOffSite != null)
                {
                    _userService.UpdateSupervisorDailyLabour(Diary);

                    await Alert("The Diary has been saved, please press upload to complete.",
                        "Please Upload");

                    ShowSaveButton = false;

                    ShowUploadButton = true;

                    if (Diary.UserDailyProjectTimesList.Any())
                        ProjectDiaries =
                            new ObservableCollection<UserDailyProjectTimes>(Diary.UserDailyProjectTimesList);
                }
                else
                {
                    await Alert("All details must be complete", "Error!");
                }
            }
            else
            {
                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;
                NavigateBack();
            }
        }
        else
        {
            var projDiary = new UserDailyProjectTimes
            {
                Id = 0,
                DailyTimeId = Diary?.DailyTimeId,
                RemoteId = Diary?.RemoteId,
                ClientName = NavigationalParameters.CurrentSelectedJob?.ClientName,
                CreatedBy = NavigationalParameters.LoggedInUser.FocusId,
                CreatedOn = DateTime.Now,
                LastModifiedBy = NavigationalParameters.LoggedInUser.FocusId,
                LastModifiedOn = DateTime.Now,
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                UserId = NavigationalParameters.LoggedInUser.FocusId,
                ProjectTimeId = 0,
                QuoteId = NavigationalParameters.CurrentSelectedJob?.QuoteNumber,
                StartTime = TimeOnSite,
                Endtime = TimeOffSite,
                GangLeaderName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName,
                OperativeNames = NavigationalParameters.CurrentSelectedJob?.OperativeNames,
                RemoteProjectTimeId = Guid.NewGuid()
            };

            projDiary.DailyProjectNotesList = new List<DailyProjectNotes>
            {
                new()
                {
                    Id = 0,
                    Notes = DiaryText,
                    ProjectTimeId = 0,
                    RemoteProjectTimeId = projDiary?.RemoteProjectTimeId
                }
            };

            Diary.UserDailyProjectTimesList = new List<UserDailyProjectTimes> { projDiary };

            if (!Diary.IsSubmited)
            {
                if (!string.IsNullOrEmpty(DiaryText) && TimeOnSite != null && TimeOffSite != null)
                {
                    _userService.UpdateSupervisorDailyLabour(Diary);

                    await Alert("The Project Diary has been Saved and will be uploaded with the daily diary.",
                        "Please Upload");

                    NavigateBack();
                }
                else
                {
                    await Alert("All details must be complete", "Error!");
                }
            }
            else
            {
                NavigateBack();
            }
        }

        IsLoading = false;
    });

    public RelayCommand Upload => new(async () =>
    {
        var supervisorDiariesToUpload = _userService.GetSupervisorLabour(NavigationalParameters.LoggedInUser.FocusId);

        if (supervisorDiariesToUpload?.InTime != null &&
            supervisorDiariesToUpload?.OutTime != null &&
            !string.IsNullOrEmpty(supervisorDiariesToUpload.UserDailyTimeNotesList?.FirstOrDefault()
                ?.Notes))
        {
            UploadButtonEnabled = false;
            IsLoading = true;
            var success = await new WebApi().UploadSupervisorDailyDiaries(supervisorDiariesToUpload);

            if (success)
            {
                supervisorDiariesToUpload.IsSubmited = true;
                _userService.UpdateSupervisorDailyLabour(supervisorDiariesToUpload);
                await _jobService.UpdateSupervisorDailyDiaries(supervisorDiariesToUpload);

                await Alert("Upload Succesful", "Upload", "Ok");

                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                NavigateBack();
            }
            else
            {
                UploadButtonEnabled = true;
                IsLoading = false;
            }
        }
        else
        {
            await Alert("Error!",
                "Start And End Time must be complete before submission; Please complete and try again",
                "Ok");
        }
    });

    public RelayCommand Cancel => new(async () => { NavigateBack(); });
}