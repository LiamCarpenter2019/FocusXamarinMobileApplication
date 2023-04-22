#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Forms;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class DiarySelectPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly SupervisorApprovalsPageViewModel _approvals;
    private readonly Jobs _jobService;

    private ClaimedNotesFile _claimedNotesItem;
    private RelayCommand<string> _noDelaysOrWorks;

    private RelayCommand<string> _supervisorScreenLoaded;

    public Users _userService;

    public Tuple<Person, List<Assignment>, JobData4Tablet> NavPassedInfo;

    public DiarySelectPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        _approvals = new SupervisorApprovalsPageViewModel();
    }

    public string _projectDate { get; set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }


    private bool _showPayrollCommentsButton { get; set; }
    private bool _additionalWorksComplete { get; set; }
    private bool _delaysComplete { get; set; }
    private bool _diaryComplete { get; set; }
    private bool _showUpload { get; set; }

    public ClaimedNotesFile _bindableClaimedNotes { get; set; }

    public ClaimedNotesFile BindableClaimedNotes
    {
        get => _bindableClaimedNotes;
        set
        {
            _bindableClaimedNotes = value;
            OnPropertyChanged();
        }
    }

    public ClaimedNotesFile ClaimedNotesItem
    {
        get => _claimedNotesItem;
        set
        {
            _claimedNotesItem = value;
            OnPropertyChanged();
        }
    }

    public bool DiaryComplete
    {
        get => _diaryComplete;
        set
        {
            _diaryComplete = value;
            OnPropertyChanged();
        }
    }

    public bool DelaysComplete
    {
        get => _delaysComplete;
        set
        {
            _delaysComplete = value;
            OnPropertyChanged();
        }
    }

    public bool PayrollCommentsComplete
    {
        get => _payrollCommentsComplete;
        set
        {
            _payrollCommentsComplete = value;
            OnPropertyChanged();
        }
    }

    public bool AdditionalWorksComplete
    {
        get => _additionalWorksComplete;
        set
        {
            _additionalWorksComplete = value;
            OnPropertyChanged();
        }
    }

    public bool ShowPayrollCommentsButton
    {
        get => _showPayrollCommentsButton;
        set
        {
            _showPayrollCommentsButton = value;
            OnPropertyChanged();
        }
    }

    public bool ShowUpload
    {
        get => _showUpload;
        set
        {
            _showUpload = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _greenTickJuly17v2 { get; set; } = SimpleStaticHelpers.GetImage("GreenTickJuly17v2");

    public ImageSource GreenTickJuly17v2
    {
        get => _greenTickJuly17v2;
        set
        {
            _greenTickJuly17v2 = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _inputDiary { get; set; } = SimpleStaticHelpers.GetImage("InputDiary");

    public ImageSource InputDiary
    {
        get => _inputDiary;
        set
        {
            _inputDiary = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _delaysImage { get; set; } = SimpleStaticHelpers.GetImage("Delays");

    public ImageSource DelaysImage
    {
        get => _delaysImage;
        set
        {
            _delaysImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _addWorks { get; set; } = SimpleStaticHelpers.GetImage("InputDiary");

    public ImageSource AddWorks
    {
        get => _addWorks;
        set
        {
            _addWorks = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand _navTo { get; set; }
    private RelayCommand<string> _inputDiaryScreen { get; set; }
    private RelayCommand<string> _screenLoaded4InputDiariesSelector { get; set; }
    private RelayCommand<string> _returnToMenuFromDiary { get; set; }
    public List<MenuButtonInfo> MenuButtons { get; set; }

    public RelayCommand<string>
        ScreenLoaded4InputDiariesSelector
    {
        get
        {
            return _screenLoaded4InputDiariesSelector ??= new RelayCommand<string>(async e =>
            {
                NavigationalParameters.ReturnPage = e;
                ShowPayrollCommentsButton = false;
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
                ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";

                BindableClaimedNotes = ClaimedNotesItem =
                    _jobService.GetClaimedNotes(NavigationalParameters.CurrentSelectedJob);

                if (ClaimedNotesItem == null)
                {
                    BindableClaimedNotes = ClaimedNotesItem = new ClaimedNotesFile
                    {
                        RemoteTableId = 0,
                        NotesDate = NavigationalParameters.CurrentSelectedJob.JobDate.Date,
                        ConPrefix = NavigationalParameters.CurrentSelectedJob?.ContractPrefix,
                        ContractId = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                        NotesGang = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString(),
                        QuoteId = NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                        PostedByGangerName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName,
                        ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference,
                        ApprovedBySupervisor = DateTime.Parse("01/01/1900 00:00:00"),
                        PostedByAdmin = DateTime.Parse("01/01/1900 00:00:00")
                    };

                    await _jobService.AddClaimedNote(ClaimedNotesItem);
                }

                DelaysComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.AnyDelays);

                DiaryComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.NotesText);

                AdditionalWorksComplete =
                    !string.IsNullOrEmpty(ClaimedNotesItem?.AnyAddnlWorkReq);

                ShowUpload = false;
            });
        }
    }

    public RelayCommand<string> SupervisorScreenLoaded =>
        _supervisorScreenLoaded ??= new RelayCommand<string>(async e =>
        {
            ProjectName = NavigationalParameters.SelectedTaskItem?.ProjectName;

            ProjectDate = $"{NavigationalParameters.SelectedTaskItem?.JobDate:dd/MM/yyyy}";

            ClaimedNotesItem = _jobService.GetClaimedNotes(NavigationalParameters.SelectedTaskItem);

            BindableClaimedNotes = ClaimedNotesItem;

            ShowPayrollCommentsButton = true;

            DelaysComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.SupervisorDelays);

            DiaryComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.SupervisorText);

            AdditionalWorksComplete =
                !string.IsNullOrEmpty(ClaimedNotesItem?.SupervisorAddnlWork);

            PayrollCommentsComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.PayrollComment);

            if (DelaysComplete && DiaryComplete && AdditionalWorksComplete &&
                NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            {
                ShowUpload = true;
            }
            else
            {
                ShowUpload = false;
            }
        });

    public RelayCommand ReturnToMenuFromDiary => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
        {
            NavigationalParameters.NavigationParameter = "DailyDiaryViewModelKey";

            await NavigateTo(ViewModelLocator.TeamOptionsPage);
        }
        else
        {
            NavigationalParameters.NavigationParameter = "DailyDiaryViewModelKey";

            await NavigateTo(ViewModelLocator.MenuSelectionPage);
        }
    });

    public RelayCommand InputDiaryScreen => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.DIARYAPPROVALS;
        else
            NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.GANGDIARY;

        await NavigateTo(ViewModelLocator.DailyDiaryPage);
    });

    public RelayCommand Delays => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.DELAYSAPPROVALS;
        else
            NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.GANGDELAYS;

        await NavigateTo(ViewModelLocator.DailyDiaryPage);
        //  await NavigateTo(ViewModelLocator.DailyDiaryPage);
    });

    public RelayCommand Works => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.WORKSAPPROVALS;
        else
            NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.GANGWORKS;

        await NavigateTo(ViewModelLocator.DailyDiaryPage);
        //   await NavigateTo(ViewModelLocator.DailyDiaryPage);
    });

    public RelayCommand Payroll => new(async () =>
    {
        NavigationalParameters.DiaryType = NavigationalParameters.DiaryTypes.PAYROL;
        await NavigateTo(ViewModelLocator.DailyDiaryPage);
        //   await NavigateTo(ViewModelLocator.DailyDiaryPage);
    });

    public RelayCommand NoDelays => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            ClaimedNotesItem.SupervisorDelays =
                $"No delays confirmed by {NavigationalParameters.LoggedInUser.FullName}";
        else
            ClaimedNotesItem.AnyDelays =
                $"No delays confirmed by {NavigationalParameters.LoggedInUser.FullName}";

        await _jobService.AddClaimedNote(ClaimedNotesItem);

        BindableClaimedNotes = ClaimedNotesItem;

        DelaysComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.AnyDelays);

        if (DelaysComplete && DiaryComplete && AdditionalWorksComplete &&
            NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            ShowUpload = true;
        else
            ShowUpload = false;
        // NoDelaysNoAddWorks = !NoDelaysNoAddWorks;
        OnPropertyChanged("NoDelaysNoAddWorks");
    });

    public RelayCommand NoWorks => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            ClaimedNotesItem.SupervisorAddnlWork =
                $"No additional works confirmed by {NavigationalParameters.LoggedInUser.FullName}";
        else
            ClaimedNotesItem.AnyAddnlWorkReq =
                $"No additional works confirmed by {NavigationalParameters.LoggedInUser.FullName}";


        await _jobService.AddClaimedNote(ClaimedNotesItem);

        BindableClaimedNotes = ClaimedNotesItem;

        AdditionalWorksComplete = !string.IsNullOrEmpty(ClaimedNotesItem?.AnyAddnlWorkReq);

        if (DelaysComplete && DiaryComplete && AdditionalWorksComplete &&
            NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            ShowUpload = true;
        else
            ShowUpload = false;

        OnPropertyChanged("NoDelaysNoAddWorks");
    });


    public RelayCommand ApproveDiary => new(async () =>
    {
        // ClaimedNotesItem = _jobService.GetClaimedNotes(NavigationalParameters.SelectedTaskItem);
        //Check that supervisor comments have been made in the diary
        if (string.IsNullOrEmpty(ClaimedNotesItem.SupervisorText))
        {
            await Alert("Please add comments before approving", "Comments not made");
        }
        else
        {
            var userChoice = await Alert("Diary Approval?", "Confirm Approve Diary!", "Yes", "No");

            if (userChoice)
            {
                ClaimedNotesItem.ApprovedBySupervisor =
                    DateTime.Parse("02/02/1900 00:00:00");

                ClaimedNotesItem.ApprovedBySupervisorName = NavigationalParameters.LoggedInUser.FullName;

                _userService.UpdateInputDiariesFile(ClaimedNotesItem);

                await NavigateTo(ViewModelLocator.TeamOptionsPage);
            }
        }
    });

    //private void InitMenuButtons(NavigationalParameters.AppTypes appType)
    //{
    //    switch (appType)
    //    {
    //        case NavigationalParameters.AppTypes.SUPERVISOR:
    //            MenuButtons = new List<MenuButtonInfo>
    //            {
    //                new MenuButtonInfo
    //                {
    //                    Enabled = _approvals.CheckWorkItemApproval(NavigationalParameters.CurrentSelectedJob),
    //                    LabelText = "Work Items",
    //                    NavCommand = Measures
    //                },
    //                new MenuButtonInfo
    //                {
    //                    Enabled = _approvals.CheckLateralApproval(NavigationalParameters.CurrentSelectedJob),
    //                    LabelText = "Laterals",
    //                    NavCommand = Laterals
    //                },
    //                new MenuButtonInfo
    //                {
    //                    Enabled = _approvals.CheckTimesheetApproval(NavigationalParameters.CurrentSelectedJob),
    //                    LabelText = "Timesheet",
    //                    NavCommand = TimeSheets
    //                },
    //                new MenuButtonInfo
    //                {
    //                    Enabled = !_approvals.CheckReinstatementApproval(NavigationalParameters.CurrentSelectedJob),
    //                    LabelText = "Reinstatement",
    //                    NavCommand = Reinstatement
    //                }
    //            };
    //            break;

    //        default:

    //            MenuButtons = new List<MenuButtonInfo>
    //            {
    //                new MenuButtonInfo {Enabled = false},
    //                new MenuButtonInfo {Enabled = false},
    //                new MenuButtonInfo {Enabled = false},
    //                new MenuButtonInfo {Enabled = false},
    //                new MenuButtonInfo {Enabled = false}
    //            };
    //            break;
    //    }
    //}

    //private async System.Threading.Tasks.Task<ClaimedNotesFile> GetClaimNotesAsync(JobData4Tablet job)
    //{
    //    var note = _jobService.GetClaimedNotes(job);

    //    if (note == null)
    //    {
    //        note = new ClaimedNotesFile
    //        {
    //            RemoteTableId = 0,
    //            NotesDate = job.JobDate.Date,
    //            ConPrefix = job?.ContractPrefix,
    //            ContractId = job?.ContractNumber.ToString(),
    //            NotesGang = job?.GangLeaderId.ToString(),
    //            QuoteId = job?.QuoteNumber.ToString(),
    //            PostedByGangerName = job?.GangLeaderName,
    //            ContractReference = job?.ContractReference
    //        };

    //        await _jobService.AddClaimedNote(note);
    //    }

    //    return note;
    //}

    public RelayCommand TimeSheets => _navTo = new RelayCommand(async () =>
    {
        // NavigationalParameters.NavigationParameter = _jobData;
        // NavigationalParameters.PassedInfo = Locator.TeamOptionsViewModelKey;
        // _navigationService.NavigateTo(Locator.TimesheetsSelectionViewModelKey, Locator.TeamOptionsViewModelKey);
        //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
        // NavigateTo(Locator.TimesheetsSelectionViewModelKey, Locator.TeamOptionsViewModelKey);
        await NavigateTo(ViewModelLocator.TimesheetSelectionPage);
    });

    public RelayCommand Measures => _navTo = new RelayCommand(async () =>
    {
        //  NavigationalParameters.NavigationParameter = _jobData;
        //NavigationalParameters.PassedInfo = Locator.TeamOptionsViewModelKey;
        // _navigationService.NavigateTo(Locator.ApproveMeasure1Key, Locator.TeamOptionsViewModelKey);
        //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
        //TODO: create measure approval page
        //  await NavigateTo(new ApproveMeasuresPage());
    });

    public RelayCommand Laterals => _navTo = new RelayCommand(async () =>
    {
        //   NavigationalParameters.NavigationParameter = _jobData;
        // NavigationalParameters.PassedInfo = Locator.TeamOptionsViewModelKey;
        //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
        // _navigationService.NavigateTo(Locator.ApproveMeasure2Key, Locator.TeamOptionsViewModelKey);
        //TODO: create measure approval page
        //await NavigateTo(new ApproveLateralsPage());
    });

    public RelayCommand Diary => _navTo = new RelayCommand(async () =>
    {
        // NavigationalParameters.NavigationParameter = _jobData;
        // NavigationalParameters.PassedInfo = Locator.TeamOptionsViewModelKey;
        //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
        //  _navigationService.NavigateTo(Locator.DailyDiaryViewModelKey, Locator.TeamOptionsViewModelKey);
        await NavigateTo(ViewModelLocator.DailyDiaryPage);
    });

    public RelayCommand Reinstatement => _navTo = new RelayCommand(async () =>
    {
        //  NavigationalParameters.NavigationParameter = _jobData;
        //  NavigationalParameters.PassedInfo = Locator.TeamOptionsViewModelKey;
        //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
        // _navigationService.NavigateTo(Locator.ReInstatementPageViewModelKey, Locator.TeamOptionsViewModelKey);
        await NavigateTo(ViewModelLocator.ReInstatementPage);
    });

    public RelayCommand Menu => _navTo = new RelayCommand(async () =>
    {
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    public RelayCommand NavBack => _navTo = new RelayCommand(async () => { NavigateBack(); });

    public Color _buttonColour { get; private set; }
    public string _buttonText { get; private set; }
    public bool _payrollCommentsComplete { get; set; }
}