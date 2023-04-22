namespace FocusXamarinMobileApplication.ViewModels;

public class SupervisorMeasuresApprovalsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    // private readonly SupervisorApprovalsPageViewModel _approvals;
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;

    public SupervisorMeasuresApprovalsPageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
        //   _approvals = new SupervisorApprovalsPageViewModel();
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

    public string _title { get; set; }

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

    public ClaimedFile _selectedSupervisorItems { get; set; }

    public ClaimedFile SelectedSupervisorItems
    {
        get => _selectedSupervisorItems;
        set
        {
            _selectedSupervisorItems = value;
            OnPropertyChanged();
        }
    }

    public List<ClaimedFile> _supervisorItems { get; set; }

    public List<ClaimedFile> SupervisorItems
    {
        get => _supervisorItems;
        set
        {
            _supervisorItems = value;
            OnPropertyChanged();
        }
    }

    public ClaimedFile _selectedWorkItems { get; set; }

    public ClaimedFile SelectedWorkItems
    {
        get => _selectedWorkItems;
        set
        {
            _selectedWorkItems = value;
            OnPropertyChanged();
        }
    }

    public List<ClaimedFile> _workItems { get; set; }

    public List<ClaimedFile> WorkItems
    {
        get => _workItems;
        set
        {
            _workItems = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        IsLoading = false;

        ProjectDate = NavigationalParameters.SelectedTaskItem.JobDate.ToString("dd/MM/yyyy");

        ProjectName = NavigationalParameters.SelectedTaskItem.ProjectName;

        var workList = _assignmentService
            .GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
            .Where(x => x.AssignmentId == Guid.Empty).ToList();

        WorkItems = _jobService.GetClaimedFiles(NavigationalParameters.SelectedTaskItem).Where(x =>
                workList.Exists(y =>
                    y.Header == x.ClaimHeader &&
                    y.Category != "Supervisor")
                && !NavigationalParameters.LateralCodes.Contains(x.ClaimHeader))
            .ToList();

        SupervisorItems = _jobService.GetClaimedFiles(NavigationalParameters.SelectedTaskItem).Where(x =>
            workList.Exists(y => y.Header == x.ClaimHeader && y.Category == "Supervisor")).ToList();
    });

    public RelayCommand ApproveMeasures => new(async () =>
    {
        var measureConfirmation = await Alert("Confirm Approve Measures", "Measures Approval", "OK", "Cancel");

        if (measureConfirmation)
        {
            foreach (var item in WorkItems)
                if (!NavigationalParameters.LateralCodes.Contains(item.ClaimHeader))
                {
                    item.ApprovedBySupervisor = DateTime.Parse("02/02/1900 00:00:00");
                    item.ApprovedBySupervisorName = NavigationalParameters.LoggedInUser.FullName;
                    _jobService.AddClaimedFile(item);
                }

            foreach (var item in SupervisorItems)
                if (!NavigationalParameters.LateralCodes.Contains(item.ClaimHeader))
                {
                    item.ApprovedBySupervisor = DateTime.Parse("02/02/1900 00:00:00");
                    item.ApprovedBySupervisorName = NavigationalParameters.LoggedInUser.FullName;
                    _jobService.AddClaimedFile(item);
                }

            await NavigateTo(ViewModelLocator.TeamOptionsPage);
        }
    });

    public RelayCommand AddWorkItem => new(async () =>
    {
        NavigationalParameters.SupervisorAction = NavigationalParameters.SupervisorMeasureAction.ADD_MEASURE;

        if (_assignmentService.GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
                .Where(x => x.AssignmentId == Guid.Empty && x.Category != "Supervisor")
                .ToList().Count == 0)
        {
            //Alert
            await Alert("No claimable work items found on this project!, Please create a CVI",
                "No Rates Found", "Ok");
        }
        else
        {
            var measures = _assignmentService
                .GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
                .Where(x => x.AssignmentId == Guid.Empty
                            && x.Category != "Supervisor").ToList();

            if (measures.Count() > 0)
                //PassedInfo = ViewModelLocator.ApproveMeasure1Key;
                await NavigateTo(ViewModelLocator.InputMeasurePage);
            else
                //Alert
                await Alert("NNo claimable work items found on this project!",
                    "No Rates Found");
        }
    });

    public RelayCommand AddSupervisorWorkItem => new(async () =>
    {
        NavigationalParameters.SupervisorAction = NavigationalParameters.SupervisorMeasureAction.ADD_SUPER_CLAIM;

        if (_assignmentService.GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
                .Where(x => x.AssignmentId == Guid.Empty && x.Category == "Supervisor")
                .ToList().Count == 0)
        {
            await Alert("No claimable supervisor work items found on this project!, Please create a CVI",
                "No Rates Found", "Ok");
        }
        else
        {
            var measures = _assignmentService
                .GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
                .Where(x => x.AssignmentId == Guid.Empty
                            && x.Category == "Supervisor").ToList();

            if (measures.Count() > 0)
                await NavigateTo(ViewModelLocator.InputMeasurePage);
            else
                await Alert("No claimable supervisor work items found on this project!",
                    "No Rates Found");
        }
    });

    public RelayCommand WorkItemSelected => new(async () =>
    {
        NavigationalParameters.SupervisorAction = NavigationalParameters.SupervisorMeasureAction.MODIFY;

        NavigationalParameters.ClaimFile = SelectedWorkItems;

        await NavigateTo(ViewModelLocator.InputMeasurePage);
    });

    public RelayCommand SupervisorItemSelected => new(async () =>
    {
        await NavigateTo(ViewModelLocator.InputMeasurePage);
    });

    public RelayCommand Back => new(async () => { await NavigateTo(ViewModelLocator.TeamOptionsPage); });
}