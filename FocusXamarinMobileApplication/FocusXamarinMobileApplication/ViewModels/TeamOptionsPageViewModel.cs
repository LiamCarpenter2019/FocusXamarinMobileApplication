using System;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class TeamOptionsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly SupervisorApprovalsPageViewModel _approvals;
    public Jobs _jobService;

    public TeamOptionsPageViewModel()
    {
        _approvals = new SupervisorApprovalsPageViewModel();
        _jobService = new Jobs();
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

    public string _title { get; set; } = "Approvals";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string _submitButtonText { get; set; }

    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
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

    public bool _showSection1 { get; set; }

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; }

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
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

    public bool _showSection5 { get; set; }

    public bool ShowSection5
    {
        get => _showSection5;
        set
        {
            _showSection5 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection6 { get; set; }

    public bool ShowSection6
    {
        get => _showSection6;
        set
        {
            _showSection6 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection7 { get; set; }

    public bool ShowSection7
    {
        get => _showSection7;
        set
        {
            _showSection7 = value;
            OnPropertyChanged();
        }
    }

    public bool _enableBackButton { get; set; }

    public bool EnableBackButton
    {
        get => _enableBackButton;
        set
        {
            _enableBackButton = value;
            OnPropertyChanged();
        }
    }

    public bool _timesheetsToApprove { get; set; }

    public bool TimesheetsToApprove
    {
        get => _timesheetsToApprove;
        set
        {
            _timesheetsToApprove = value;
            OnPropertyChanged();
        }
    }

    public bool _diariesToApprove { get; set; }

    public bool DiariesToApprove
    {
        get => _diariesToApprove;
        set
        {
            _diariesToApprove = value;
            OnPropertyChanged();
        }
    }

    public bool _measuresToApprove { get; set; }

    public bool MeasuresToApprove
    {
        get => _measuresToApprove;
        set
        {
            _measuresToApprove = value;
            OnPropertyChanged();
        }
    }

    public bool _lateralsToApprove { get; set; }

    public bool LateralsToApprove
    {
        get => _lateralsToApprove;
        set
        {
            _lateralsToApprove = value;
            OnPropertyChanged();
        }
    }

    public Color _timesheetButtonColour { get; set; } = Color.LightGray;

    public Color TimesheetButtonColour
    {
        get => _timesheetButtonColour;
        set
        {
            _timesheetButtonColour = value;
            OnPropertyChanged();
        }
    }

    public Color _diariesButtonColour { get; set; } = Color.LightGray;

    public Color DiariesButtonColour
    {
        get => _diariesButtonColour;
        set
        {
            _diariesButtonColour = value;
            OnPropertyChanged();
        }
    }

    public Color _measuresButtonColour { get; set; } = Color.LightGray;

    public Color MeasuresButtonColour
    {
        get => _measuresButtonColour;
        set
        {
            _measuresButtonColour = value;
            OnPropertyChanged();
        }
    }

    public Color _lateralsButtonColour { get; set; } = Color.LightGray;

    public Color LateralsButtonColour
    {
        get => _lateralsButtonColour;
        set
        {
            _lateralsButtonColour = value;
            OnPropertyChanged();
        }
    }

    public Color _lateralsButtonTextColour { get; set; } = Color.LightGray;

    public Color LateralsButtonTextColour
    {
        get => _lateralsButtonTextColour;
        set
        {
            _lateralsButtonTextColour = value;
            OnPropertyChanged();
        }
    }

    public Color _measuresButtonTextColour { get; set; } = Color.LightGray;

    public Color MeasuresButtonTextColour
    {
        get => _measuresButtonTextColour;
        set
        {
            _measuresButtonTextColour = value;
            OnPropertyChanged();
        }
    }

    public Color _timesheetButtonTextColour { get; set; } = Color.LightGray;

    public Color TimesheetButtonTextColour
    {
        get => _timesheetButtonTextColour;
        set
        {
            _timesheetButtonTextColour = value;
            OnPropertyChanged();
        }
    }

    public Color _diariesButtonTextColour { get; set; } = Color.LightGray;

    public Color DiariesButtonTextColour
    {
        get => _diariesButtonTextColour;
        set
        {
            _diariesButtonTextColour = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectDate = NavigationalParameters.SelectedTaskItem?.Date ??
                      NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        ProjectName = NavigationalParameters.SelectedTaskItem?.ProjectName ??
                      NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        ShowSection6 = true;
        ShowSection7 = true;
        TimesheetsToApprove = false;
        DiariesToApprove = false;
        EnableBackButton = false;

        TimesheetsToApprove = !_approvals.CheckTimesheetApproval(NavigationalParameters.SelectedTaskItem);
        if (TimesheetsToApprove)
        {
            ShowSection7 = false;
            EnableBackButton = true;
            TimesheetButtonColour = Color.LightGray;
            TimesheetButtonTextColour = Color.Black;
        }
        else
        {
            TimesheetButtonColour = Color.Green;
            TimesheetButtonTextColour = Color.White;
        }

        DiariesToApprove = !_approvals.CheckDiaryApproval(NavigationalParameters.SelectedTaskItem);
        if (DiariesToApprove)
        {
            ShowSection7 = false;
            EnableBackButton = true;
            DiariesButtonColour = Color.LightGray;
            DiariesButtonTextColour = Color.Black;
        }
        else
        {
            DiariesButtonColour = Color.Green;
            DiariesButtonTextColour = Color.White;
        }

        MeasuresToApprove = !_approvals.CheckWorkItemApproval(NavigationalParameters.SelectedTaskItem);
        if (MeasuresToApprove)
        {
            ShowSection7 = false;
            EnableBackButton = true;
            MeasuresButtonColour = Color.LightGray;
            MeasuresButtonTextColour = Color.Black;
        }
        else
        {
            MeasuresButtonColour = Color.Green;
            MeasuresButtonTextColour = Color.White;
        }

        LateralsToApprove = !_approvals.CheckLateralApproval(NavigationalParameters.SelectedTaskItem);
        if (LateralsToApprove)
        {
            ShowSection7 = false;
            EnableBackButton = true;
            LateralsButtonColour = Color.LightGray;
            LateralsButtonTextColour = Color.Black;
        }
        else
        {
            LateralsButtonColour = Color.Green;
            LateralsButtonTextColour = Color.White;
        }
    });

    public RelayCommand ApproveTimesheets => new(async () =>
    {
        if (!TimesheetsToApprove)
        {
            await Alert("Warning", "There are no timesheets to approve ");
        }
        else
        {
            //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.TIMESHEETS;
            // NavigationalParameters.PassedInfo = new TimesheetSelectionPage();
            await NavigateTo(ViewModelLocator.TimesheetSelectionPage);
        }
    });

    public RelayCommand ApproveDiary => new(async () =>
    {
        if (!DiariesToApprove)
        {
            await Alert("Warning", "There are no diaries to approve ");
        }
        else
        {
            //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.DIARIES;
            //   NavigationalParameters.PassedInfo = new DiarySelectionPage();
            await NavigateTo(ViewModelLocator.DiarySelectionPage);
        }
    });

    public RelayCommand ApproveMeasures => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.APPROVALS;

        await NavigateTo(ViewModelLocator.SupervisorMeasuresApprovalsPage);
    });

    public RelayCommand ApproveLaterals => new(async () =>
    {
        if (!LateralsToApprove)
            await Alert("Warning", "There are no Laterals to approve ");
        else
            //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
            NavigationalParameters.PassedInfo = "TeamOptionsViewModelKey";
    });

    public RelayCommand ApproveReInstatement => new(async () =>
    {
        //NavigationalParameters.ReturnPage = Locator.TeamOptionsViewModelKey;
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.REINSTATEMENT;
        //  NavigationalParameters.PassedInfo = new ReInstatementPage();
        await NavigateTo(ViewModelLocator.ReInstatementPage);
    });

    public RelayCommand Back => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob == null)
        {
            await NavigateTo(ViewModelLocator.TaskListPage);
        }
        else
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.TASKLIST)
                await NavigateTo(ViewModelLocator.TaskListPage);
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.GANGTOOLBOXTALKS
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.APPROVALS)
                await NavigateTo(ViewModelLocator.GangSelectPage);
            else
                await NavigateTo(ViewModelLocator.SupervisorProjectPage);
        }
    });

    public RelayCommand Submit => new(async () =>
    {
        // var approvals = new SupervisorApprovals();
        IsLoading = true;
        ShowSection7 = false;
        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DEFAULT;
        var itemsToApprove = _approvals.GetItemsForApproval(NavigationalParameters.SelectedTaskItem);
        var approvalInProgress = _approvals.ApprovalInProgress();

        if (itemsToApprove && approvalInProgress)
        {
            await Alert(
                "Approvals required before upload",
                "Approvals required!");
        }
        else
        {
            var dailyUploadData = _jobService.GetApprovalsToUpload(NavigationalParameters.SelectedTaskItem);

            if (dailyUploadData.DailyDiary == null
                && dailyUploadData.LabourFiles == null
                && dailyUploadData.ClaimedFiles == null)
            {
                await Alert("There is nothing to upload", "Error!");

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.TASKLIST;
                await NavigateTo(ViewModelLocator.TaskListPage);
            }
            else
            {
                if (!dailyUploadData.ClaimedFiles.Any())
                {
                    var ok = await Alert(
                        "Warning!! No measures have been entered can you confirm that you have no measures or enter any measures that you have missed",
                        "NO MEASURES HAVE BEEN ENTERED", "Confirm",
                        "Measures");

                    if (ok)
                        try
                        {
                            IsLoading = true;
                            ShowSection7 = false;

                            var success = await _jobService.SaveDailyInputAsync(dailyUploadData);

                            if (!success)
                            {
                                IsLoading = false;
                                ShowSection7 = true;

                                await Alert("Failed to save the daily measures please contact support", "Error!");
                            }
                            else
                            {
                                IsLoading = true;
                                ShowSection7 = false;

                                if (NavigationalParameters.SelectedTaskItem != null)
                                {
                                    NavigationalParameters.SelectedTaskItem.Complete = true;
                                    _jobService.AddApproval(NavigationalParameters.SelectedTaskItem);
                                    NavigationalParameters.SelectedTaskItem = null;
                                }

                                NavigationalParameters.AppMode = NavigationalParameters.AppModes.TASKLIST;
                                // NavigationalParameters.NavigationParameter = _projectJobs;
                                // NavigationalParameters.NavigationService = _navigationService;
                                //NavigationalParameters.ReturnPage = Locator.MenuPageViewModelKey;
                                // NavigationalParameters.PassedInfo = new Views.MainListPage();
                                await NavigateTo(ViewModelLocator.TaskListPage);
                            }

                            IsLoading = false;
                            ShowSection7 = true;
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                            await Alert(
                                "The daily measures Have failed To Save, Please Try again",
                                "Error!");

                            IsLoading = false;
                            ShowSection7 = true;
                        }
                    else
                        await NavigateTo(ViewModelLocator.SupervisorMeasuresApprovalsPage);
                }
                else
                {
                    try
                    {
                        IsLoading = true;

                        var success = await _jobService.SaveDailyInputAsync(dailyUploadData);

                        if (!success)
                        {
                            IsLoading = false;
                            await Alert(
                                "Failed to save the daily measures please contact support",
                                "Error!");
                        }
                        else
                        {
                            IsLoading = false;

                            if (NavigationalParameters.SelectedTaskItem != null)
                            {
                                NavigationalParameters.SelectedTaskItem.Complete = true;
                                _jobService.AddApproval(NavigationalParameters.SelectedTaskItem);
                                NavigationalParameters.SelectedTaskItem = null;
                            }
                            //NavigationalParameters.NavigationParameter =
                            //    new Tuple<Person, List<Assignment>>(NavigationalParameters.LoggedInUser,
                            //        new List<Assignment>());

                            NavigationalParameters.AppMode = NavigationalParameters.AppModes.TASKLIST;
                            await NavigateTo(ViewModelLocator.TaskListPage);
                        }

                        IsLoading = false;
                        ShowSection7 = true;
                    }
                    catch (Exception ex)
                    {
                        var error = ex.ToString();
                        await Alert(
                            "The daily measures Have failed To Save, Please Try again"
                            , "Error!");

                        IsLoading = false;
                        ShowSection7 = true;
                    }
                }
            }
        }
    });
}