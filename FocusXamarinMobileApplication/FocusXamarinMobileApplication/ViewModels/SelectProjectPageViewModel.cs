//using System.Security.Cryptography;

//using GalaSoft.MvvmLight.Helpers;

#region

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SelectProjectPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private bool _isLoading;
    public Assignments AssignmentService;
    public Jobs JobService;
    public Users UserService;

    //public SelectProjectPageViewModel(string type)
    //{
    //    UserService = new Users();
    //    JobService = new Jobs();
    //    AssignmentService = new Assignments();
    //    Type = type;
    //}

    public SelectProjectPageViewModel()
    {
        UserService = new Users();
        JobService = new Jobs();
        AssignmentService = new Assignments();
    }

    public string _title { get; set; }

    public JobData4Tablet ProjectSelected { get; set; }
    private bool _gangVisible { get; set; }
    private ObservableCollection<JobData4Tablet> _distinctProjectsList { get; set; }
    public string Type { get; set; }

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

    public ObservableCollection<Assignment> PreSites { get; set; }

    public ObservableCollection<JobData4Tablet> DistinctProjectsList
    {
        get => _distinctProjectsList;
        set
        {
            _distinctProjectsList = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool GangVisible
    {
        get => _gangVisible;
        set
        {
            _gangVisible = value;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
        DistinctProjectsList = new ObservableCollection<JobData4Tablet>();

        var jobList = JobService.GetAllJobs()
            .Where(x => x.SupervisorId == NavigationalParameters.LoggedInUser.FocusId).ToList();

        switch (Type)
        {
            case "ProjectSelect":
            {
                GangVisible = false;
                Title = "Project Selection";
                foreach (var job in jobList)
                    switch (NavigationalParameters.AppMode)
                    {
                        case NavigationalParameters.AppModes.PRESITE:
                        {
                            switch (NavigationalParameters.SurveyType)
                            {
                                case NavigationalParameters.SurveyTypes.SED:
                                    ///if (NavigationalParameters.AssignmentList.Any(x => x.AssignmentId == Guid.Empty p&& x.Category == "PreSiteCivils" && x.Qnumber == job.QuoteNumber))
                                    //{
                                    if (DistinctProjectsList.All(x =>
                                            x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    {
                                        job.OperativeNames = "";
                                        DistinctProjectsList.Add(job);
                                    }

                                    // }
                                    break;

                                case NavigationalParameters.SurveyTypes.CHAMBERAUDIT:
                                    ///if (NavigationalParameters.AssignmentList.Any(x => x.AssignmentId == Guid.Empty && x.Category == "PreSiteCivils" && x.Qnumber == job.QuoteNumber))
                                    //{
                                    if (DistinctProjectsList.All(x =>
                                            x.ProjectName.ToLower() != job.ProjectName.ToLower())
                                        && job.ProjectName.ToLower().Contains("2020 fibre maintenance"))
                                    {
                                        job.OperativeNames = "";
                                        DistinctProjectsList.Add(job);
                                    }

                                    // }
                                    break;
                                case NavigationalParameters.SurveyTypes.PRESITECIVILS:
                                    ///if (NavigationalParameters.AssignmentList.Any(x => x.AssignmentId == Guid.Empty && x.Category == "PreSiteCivils" && x.Qnumber == job.QuoteNumber))
                                    //{
                                    if (DistinctProjectsList.All(x =>
                                            x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    {
                                        job.OperativeNames = "";
                                        DistinctProjectsList.Add(job);
                                    }

                                    // }
                                    break;
                                case NavigationalParameters.SurveyTypes.PRESITEPREMISES:
                                    //if (NavigationalParameters.AssignmentList.Any(x => x.AssignmentId == Guid.Empty
                                    //    && x.Category == "PreSitePremises"
                                    //    && x.Qnumber == job.QuoteNumber)
                                    //&& DistinctProjectsList.All(x => x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    if (DistinctProjectsList.All(x =>
                                            x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    {
                                        job.OperativeNames = "";

                                        DistinctProjectsList.Add(job);
                                    }

                                    break;
                                case NavigationalParameters.SurveyTypes.GANGCLEAR:
                                    //if (NavigationalParameters.AssignmentList.Any(x => x.AssignmentId == Guid.Empty
                                    //    && x.Category == "GangClear"
                                    //    && x.Qnumber == job.QuoteNumber)
                                    //    && DistinctProjectsList.All(x => x.ProjectName.ToLower() != job.ProjectName.ToLower()))

                                    if (DistinctProjectsList.All(x =>
                                            x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    {
                                        job.OperativeNames = "";

                                        DistinctProjectsList.Add(job);
                                    }

                                    break;
                                case NavigationalParameters.SurveyTypes.SITECLEAR:
                                    //if (NavigationalParameters.AssignmentList.Any(x => x.AssignmentId == Guid.Empty
                                    //   && x.Category == "SiteClear"
                                    //   && x.Qnumber == job.QuoteNumber)
                                    //   && DistinctProjectsList.All(x => x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    if (DistinctProjectsList.All(x =>
                                            x.ProjectName.ToLower() != job.ProjectName.ToLower()))
                                    {
                                        job.OperativeNames = "";

                                        DistinctProjectsList.Add(job);
                                    }

                                    break;
                            }

                            break;
                        }

                        default:
                        {
                            if (DistinctProjectsList.All(x => x.ProjectName != job.ProjectName))
                            {
                                job.OperativeNames = "";

                                DistinctProjectsList.Add(job);
                            }

                            break;
                        }
                    }

                break;
            }

            default:
            {
                GangVisible = true;
                Title = "Select Gang";
                foreach (var job in jobList)
                    if (!DistinctProjectsList.Any(y =>
                            y.ProjectName == job.ProjectName
                            && y.OperativeIdsPiped == job.OperativeIdsPiped))
                    {
                        var operatives = job.OperativeIdsPiped.Split('|');

                        foreach (var opId in operatives)
                            if (int.TryParse(opId, out var opName))
                                job.OperativeNames += $"{UserService.GetUserById(opName)?.FullName}, ";

                        if (!string.IsNullOrEmpty(job.OperativeNames))
                            job.OperativeNames =
                                job.OperativeNames.Remove(
                                    job.OperativeNames.LastIndexOf(",", StringComparison.Ordinal),
                                    1);

                        DistinctProjectsList.Add(job);
                    }

                //Fix for current select project
                DistinctProjectsList = new ObservableCollection<JobData4Tablet>(DistinctProjectsList
                    .Where(x => x.ProjectName == NavigationalParameters.CurrentSelectedJob.ProjectName));
                break;
            }
        }

        if (DistinctProjectsList == null || !DistinctProjectsList.Any())
            _ = Alert("There is no survey of this type available; please contact support for furthur detail!",
                "Error!");
        //GoBack.Execute("");
    });


    public RelayCommand GoBack => new(async () =>
    {
        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.EVENTMANAGEMENT)
            NavigateBack();
        else
            await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
        ;
    });

    public RelayCommand Refresh => new(() =>
    {
        //OnPropertyChanged("SavedDamageCount");
        OnPropertyChanged("ShowSubmissionButton");
    });

    public RelayCommand<JobData4Tablet> SelectedProject => new(async i =>
    {
        if (i != null)
        {
            NavigationalParameters.CurrentSelectedJob = i;

            switch (NavigationalParameters.AppMode)
            {
                case NavigationalParameters.AppModes.EVENTMANAGEMENT:
                    NavigateBack();
                    break;
                case NavigationalParameters.AppModes.PRESITEPREMISIS:
                    EndPoints = NavigationalParameters.DataPassedToTablet.EndPoints?
                        .Where(x => x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                                    x.BuildingStandard?.ToLower() == "premises")?
                        .GroupBy(x => x.BuildingName)?
                        .ToList();

                    if (EndPoints.Any())
                        await NavigateTo(ViewModelLocator.SelectStreetPage);
                    else
                        await Alert("There is nothing to pre site", "No endpoints available!");
                    break;
                case NavigationalParameters.AppModes.PRESITECIVILS:
                    EndPoints = NavigationalParameters.DataPassedToTablet.EndPoints?
                        .Where(x => x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                                    x.BuildingStandard?.ToLower() == "street")?
                        .GroupBy(x => x.BuildingName)?
                        .ToList();

                    if (EndPoints.Any())
                        await NavigateTo(ViewModelLocator.SelectStreetPage);
                    else
                        await Alert("There is nothing to pre site", "No endpoints available!");
                    break;
                default:
                    EndPoints = NavigationalParameters.DataPassedToTablet.EndPoints?
                        .Where(x => x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                                    x.BuildingStandard?.ToLower() == "street")?
                        .GroupBy(x => x.StreetName)?
                        .ToList();

                    if (EndPoints.Any())
                        await NavigateTo(ViewModelLocator.SelectStreetPage);
                    else
                        await Alert("No endpoints available!", "There is nothing to pre site");
                    break;
            }
        }
    });

    public List<IGrouping<string, VMexpansionReleaseData>> EndPoints { get; set; }
}