using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class GangSelectPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;

    private readonly Users _userService;
    private RelayCommand _logOutCommand;
    private RelayCommand _pageLoaded;
    private RelayCommand<string> _refreshCommand;

    public GangSelectPageViewModel()
    {
        _jobService = new Jobs();

        _userService = new Users();

        _assignmentService = new Assignments();
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

    public bool logonCheckResult { get; set; }
    public List<IGrouping<string, Assignment>> AssignmentGroup { get; set; }
    public string UserName { get; set; }

    public ObservableCollection<JobData4Tablet> _jobTaskList { get; set; }

    public ObservableCollection<JobData4Tablet> JobTaskList
    {
        get => _jobTaskList;
        set
        {
            _jobTaskList = value;
            OnPropertyChanged();
        }
    }

    // public List<TaskViewModel> tempList { get; private set; }
    public List<IGrouping<string, JobData4Tablet>> _projects { get; set; }

    public List<IGrouping<string, JobData4Tablet>> Projects
    {
        get => _projects;
        set
        {
            _projects = value;
            OnPropertyChanged();
        }
    }

    private string _date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

    public string Date
    {
        get => _date;
        set
        {
            _date = value;
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

    public bool _showTick { get; set; }

    public bool ShowTick
    {
        get => _showTick;
        set
        {
            _showTick = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection1 { get; set; } = true;

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; } = true;

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

    public bool _showButtonA { get; set; }

    public bool ShowButtonA
    {
        get => _showButtonA;
        set
        {
            _showButtonA = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonB { get; set; }

    public bool ShowButtonB
    {
        get => _showButtonB;
        set
        {
            _showButtonB = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonC { get; set; }

    public bool ShowButtonC
    {
        get => _showButtonC;
        set
        {
            _showButtonC = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonD { get; set; }

    public bool ShowButtonD
    {
        get => _showButtonD;
        set
        {
            _showButtonD = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonE { get; set; }

    public bool ShowButtonE
    {
        get => _showButtonE;
        set
        {
            _showButtonE = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonF { get; set; }

    public bool ShowButtonF
    {
        get => _showButtonF;
        set
        {
            _showButtonF = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonG { get; set; }

    public bool ShowButtonG
    {
        get => _showButtonG;
        set
        {
            _showButtonG = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonH { get; set; }

    public bool ShowButtonH
    {
        get => _showButtonH;
        set
        {
            _showButtonH = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonI { get; set; }

    public bool ShowButtonI
    {
        get => _showButtonI;
        set
        {
            _showButtonI = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonJ { get; set; }

    public bool ShowButtonJ
    {
        get => _showButtonJ;
        set
        {
            _showButtonJ = value;
            OnPropertyChanged();
        }
    }

    public bool _loading { get; set; }

    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            OnPropertyChanged();
        }
    }

    public bool _showRefresh { get; set; } = true;

    public bool ShowRefresh
    {
        get => _showRefresh;
        set
        {
            _showRefresh = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonAImage { get; set; } = "";

    public ImageSource ButtonAImage
    {
        get => _ButtonAImage;
        set
        {
            _ButtonAImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonBImage { get; set; } = "";

    public ImageSource ButtonBImage
    {
        get => _ButtonBImage;
        set
        {
            _ButtonBImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonCImage { get; set; } = "";

    public ImageSource ButtonCImage
    {
        get => _ButtonCImage;
        set
        {
            _ButtonCImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonDImage { get; set; } = "";

    public ImageSource ButtonDImage
    {
        get => _ButtonDImage;
        set
        {
            _ButtonDImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonEImage { get; set; } = "";

    public ImageSource ButtonEImage
    {
        get => _ButtonEImage;
        set
        {
            _ButtonEImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonFImage { get; set; } = "";

    public ImageSource ButtonFImage
    {
        get => _ButtonFImage;
        set
        {
            _ButtonFImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonGImage { get; set; } = "";

    public ImageSource ButtonGImage
    {
        get => _ButtonGImage;
        set
        {
            _ButtonGImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonHImage { get; set; } = "";

    public ImageSource ButtonHImage
    {
        get => _ButtonHImage;
        set
        {
            _ButtonHImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonIImage { get; set; } = "";

    public ImageSource ButtonIImage
    {
        get => _ButtonIImage;
        set
        {
            _ButtonIImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonJImage { get; set; } = "";

    public ImageSource ButtonJImage
    {
        get => _ButtonJImage;
        set
        {
            _ButtonJImage = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonALabel { get; set; } = "";

    public string ButtonALabel
    {
        get => _ButtonALabel;
        set
        {
            _ButtonALabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonBLabel { get; set; } = "";

    public string ButtonBLabel
    {
        get => _ButtonBLabel;
        set
        {
            _ButtonBLabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonCLabel { get; set; } = "";

    public string ButtonCLabel
    {
        get => _ButtonCLabel;
        set
        {
            _ButtonCLabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonDLabel { get; set; } = "";

    public string ButtonDLabel
    {
        get => _ButtonDLabel;
        set
        {
            _ButtonDLabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonELabel { get; set; } = "";

    public string ButtonELabel
    {
        get => _ButtonELabel;
        set
        {
            _ButtonELabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonFLabel { get; set; } = "";

    public string ButtonFLabel
    {
        get => _ButtonFLabel;
        set
        {
            _ButtonFLabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonGLabel { get; set; } = "";

    public string ButtonGLabel
    {
        get => _ButtonGLabel;
        set
        {
            _ButtonGLabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonHLabel { get; set; } = "";

    public string ButtonHLabel
    {
        get => _ButtonHLabel;
        set
        {
            _ButtonHLabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonILabel { get; set; } = "";

    public string ButtonILabel
    {
        get => _ButtonILabel;
        set
        {
            _ButtonILabel = value;
            OnPropertyChanged();
        }
    }

    public string _ButtonJLabel { get; set; } = "";

    public string ButtonJLabel
    {
        get => _ButtonJLabel;
        set
        {
            _ButtonJLabel = value;
            OnPropertyChanged();
        }
    }

    public string _logOutButtonText { get; set; } = "Back";

    public string LogOutButtonText
    {
        get => _logOutButtonText;
        set
        {
            _logOutButtonText = value;
            OnPropertyChanged();
        }
    }

    public string _title { get; set; } = "";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public GangViewModel _selectedGangItem { get; set; }

    public GangViewModel SelectedGangItem
    {
        get => _selectedGangItem;
        set
        {
            _selectedGangItem = value;
            OnPropertyChanged();
        }
    }

    public ListView _selectedListItem { get; set; }

    public ListView SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            _selectedListItem = value;
            OnPropertyChanged();
        }
    }


    public RelayCommand PageLoaded => _pageLoaded ??= new RelayCommand(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = $"{DateTime.Now.Date:dd/MM/yyyy}";
        //================================ 3rd row==========
        ButtonAImage = SimpleStaticHelpers.GetImage("MyChecks");
        ButtonALabel = "Approvals";

        ButtonBImage = SimpleStaticHelpers.GetImage("MyPlantScreen");
        ButtonBLabel = "View Plant";

        ButtonCImage = SimpleStaticHelpers.GetImage("Gang");
        ButtonCLabel = "View Training";

        ButtonDImage = SimpleStaticHelpers.GetImage("AsBuilt");
        ButtonDLabel = "Reinstatement";

        ButtonEImage = SimpleStaticHelpers.GetImage("EarlyWarningIcon");
        ButtonELabel = "Cvi";
        //=================== 2nd row ===========================================
        ButtonFImage = SimpleStaticHelpers.GetImage("Gang");
        ButtonFLabel = "";

        ButtonGImage = SimpleStaticHelpers.GetImage("AsBuilt");
        ButtonGLabel = "";

        ButtonHImage = SimpleStaticHelpers.GetImage("Strike");
        ButtonHLabel = "";

        ButtonIImage = SimpleStaticHelpers.GetImage("Gang");
        ButtonILabel = "";

        ButtonJImage = NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR
            ? SimpleStaticHelpers.GetImage("ToolboxTalk")
            : "";

        ButtonJLabel = NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR
            ? "ToolBox talks"
            : "";

        NavigationalParameters.LoggedInUser = _userService.Check4ValidLoggedInPerson();

        NavigationalParameters.Abbreviations = _assignmentService.GetAbbreviations()?.ToList();

        NavigationalParameters.AllJobs = _jobService.GetAllJobs()
            .Where(x => x.ProjectName == NavigationalParameters.CurrentSelectedJob.ProjectName)?.ToList();

        AssignmentGroup = NavigationalParameters.AssignmentList?.GroupBy(x => x.Category)?.ToList();

        UserName = NavigationalParameters.LoggedInUser?.FullName;

        try
        {
            AssignmentGroup = NavigationalParameters.AssignmentList?.GroupBy(x => x.Category).ToList();
        }
        catch (Exception ex)
        {
            AssignmentGroup = NavigationalParameters.DataPassedToTablet.Assignments
                ?.GroupBy(x => x.Category).ToList();
        }

        ShowRefresh = false;

        Title = "Gangs";

        ProjectName = "";

        NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.GANGLIST;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
        {
            ShowSection3 = false;
            ShowSection4 = false;
        }
        else
        {
            ShowSection3 = true;
            ShowButtonA = true;
            ShowButtonB = true;
            ShowButtonC = true;
            ShowButtonD = true;
            ShowButtonE = true;

            ShowSection4 = true;
            ShowButtonF = false;
            ShowButtonG = false;
            ShowButtonH = false;
            ShowButtonI = false;
            ShowButtonJ = false;
        }

        ShowSection5 = false;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
            UpdateJobs(NavigationalParameters.AllJobs.Where(x =>
                x.SupervisorId == NavigationalParameters.LoggedInUser.FocusId &&
                x.ProjectName == NavigationalParameters.CurrentSelectedJob.ProjectName &&
                x.JobDate.Date == DateTime.Now.Date).ToList());
        else
            UpdateJobs(NavigationalParameters.AllJobs.Where(x =>
                x.SupervisorId == NavigationalParameters.LoggedInUser.FocusId &&
                x.ProjectName == NavigationalParameters.CurrentSelectedJob.ProjectName).ToList());
    });

    public RelayCommand<string> RefreshCommand
    {
        get
        {
            async void Execute(string e)
            {
                var wa = new WebApi();

                var connectionAvailable = await wa.CanWeConnect2Api();

                if (connectionAvailable)
                {
                    Loading = true;
                    ShowRefresh = false;

                    var wassup = await wa.LogonRequest(NavigationalParameters.LoggedInUser.LoginAlias);

                    if (wassup.Item2.Contains("Good"))
                    {
                        Loading = false;
                        ShowRefresh = true;

                        await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                    }
                    else
                    {
                        Analytics.TrackEvent(
                            $"MainViewModel (Refresh) Data returned from Api BAD for: {NavigationalParameters.LoggedInUser.FullName}");

                        Loading = false;
                        ShowRefresh = true;
                        await Alert("Refresh Failed with this Error: " + wassup, "Error!");
                    }
                }
            }

            return _refreshCommand ??= new RelayCommand<string>(Execute);
        }
    }

    public RelayCommand NavigateToListItemView => new(async () =>
    {
        //    SelectedGangItem =
        //        SelectedListItem.SelectedItem as GangViewModel;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
        {
            SelectedGangItem.IsSelected = true;
            NavigationalParameters.CurrentSelectedJob = SelectedGangItem;
            // NavigationalParameters.ReturnPage = "MainListPageViewModelKey";
            _jobService.UpdateJobToSelected(NavigationalParameters.CurrentSelectedJob);

            await NavigateTo(ViewModelLocator.SiteInspectionResultPage);
            // await NavigateTo(ViewModelLocator.SiteInspectionResultPage);
        }
        else
        {
            NavigationalParameters.CurrentSelectedJob = SelectedGangItem;
            // NavigationalParameters.ReturnPage = "MainListPageViewModelKey";
            _jobService.UpdateJobToSelected(NavigationalParameters.CurrentSelectedJob);

            JobTaskList = null;

            UpdateJobs(NavigationalParameters.AllJobs.Where(x =>
                x.SupervisorId == NavigationalParameters.LoggedInUser.FocusId &&
                x.ProjectName == NavigationalParameters.CurrentSelectedJob.ProjectName).ToList());
        }
    });

    public RelayCommand LogOutCommand => _logOutCommand ??= new RelayCommand(async () =>
    {
        try
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;
            await NavigateTo(ViewModelLocator.MenuSelectionPage);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand NavigateToApprovals => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob != null && NavigationalParameters.CurrentSelectedJob.IsSelected)
        {
            var ItemsToApprove = _jobService.GetTasks()
                .FirstOrDefault(x => x.QuoteNumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber
                                     && x.GangLeaderId == NavigationalParameters.CurrentSelectedJob.GangLeaderId
                                     && x.JobDate == NavigationalParameters.CurrentSelectedJob.JobDate
                                     && x.Category == "Approvals"
                                     && (x.ClaimesApproved == false
                                         || x.LabourApproved == false
                                         || x.DiaryApproved ==
                                         false)); //_approvals.CheckDiaryApproval(NavigationalParameters.CurrentSelectedJob);

            if (ItemsToApprove == null)
            {
                await Alert("Nothing to approve",
                    "there are no measure for this gang to approve please select a different day to continue", "Ok");
            }
            else
            {
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.APPROVALS;

                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.GANGAPPROVALS;

                NavigationalParameters.ReturnPage = "GangList";

                NavigationalParameters.SelectedTaskItem = ItemsToApprove;

                await NavigateTo(ViewModelLocator.TeamOptionsPage);
            }
        }
        else
        {
            await Alert("Please select a Gang", "View Plant");
        }
    });

    public RelayCommand NavigateToPlant => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob != null && NavigationalParameters.CurrentSelectedJob.IsSelected)
        {
            NavigationalParameters.PlantView = NavigationalParameters.PlantViews.GANGVIEW;

            NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUPERVISORGANGPLANT;
            // await NavigateTo(new PlantTransferListPage());
            await NavigateTo(ViewModelLocator.PlantTransferListPage);
        }
        else
        {
            await Alert("Please select a Gang", "View Plant");
        }
    });

    public RelayCommand NavigateToTrainingDocs => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob != null &&
            NavigationalParameters.CurrentSelectedJob?.IsSelected == true)
        {
            //NavigationalParameters.NavigationParameter =
            //    new Tuple<string, JobData4Tablet>("Training", NavigationalParameters.CurrentSelectedJob);


            NavigationalParameters.AppMode = NavigationalParameters.AppModes.TRAININGDOCS;
            // NavigationalParameters.ReturnPage = "GangList";
            await NavigateTo(ViewModelLocator.SignaturePage);
        }
        else
        {
            await Alert("Please select a Gang", "View Plant");
        }
    });

    public RelayCommand NavigateToReInstatement => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob != null &&
            NavigationalParameters.CurrentSelectedJob?.IsSelected == true)
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.REINSTATEMENT;
            await NavigateTo(ViewModelLocator.ReInstatementPage);
        }
        else
        {
            await Alert("Please select a gang", "View Plant");
        }
    });

    public RelayCommand CviCommand => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob != null && NavigationalParameters.CurrentSelectedJob.IsSelected)
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.CVI;

            NavigationalParameters.CurrentAssignment = new Assignment
            {
                AssignmentId = Guid.NewGuid(),
                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                Cnumber = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
                ClientName = NavigationalParameters.CurrentSelectedJob.ClientName,
                ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName,
                CompletedOn = DateTime.Now.Date,
                Category = "CVI",
                PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                Cvi = new List<Cvi>()
            };

            NavigationalParameters.CurrentCvi = new Cvi
            {
                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                SupervisorId = NavigationalParameters.CurrentSelectedJob.SupervisorId.ToString(),
                RelatedTo = "Scd Group Ltd"
            };

            await NavigateTo(ViewModelLocator.RaiseCviMeasurePage);
        }
        else
        {
            await Alert("Please select a gang", "Cvi");
        }
    });

    public RelayCommand ToolBoxTalkCommand => new(async () =>
    {
        if (NavigationalParameters.CurrentSelectedJob.IsSelected)
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGTOOLBOXTALKS;

            await NavigateTo(ViewModelLocator.TaskListPage);
        }
        else
        {
            await Alert("Tool box Talks", "Please select a gang");
        }
    });

    public async void UpdateJobs(List<JobData4Tablet> jobs)
    {
        NavigationalParameters.JobTaskList = new List<JobData4Tablet>();

        JobTaskList = new ObservableCollection<JobData4Tablet>();

        //tempList = new List<TaskViewModel>();

        if (jobs != null && jobs.Any())
        {
            var gangs = jobs.Select(job => new GangViewModel
            {
                GangLeaderName = job.GangLeaderName,
                ProjectManagerId = job.ProjectManagerId,
                GangLeaderId = job.GangLeaderId,
                SupervisorId = job.SupervisorId,
                OperativeNames = job.OperativeNames,
                DailyChecksCompleted = job.DailyChecksCompleted,
                DailyChecksPosted = job.DailyChecksPosted,
                JobId = job.JobId,
                ProjectName = job.ProjectName,
                Date = job.JobDate.Date == DateTime.Now.Date
                    ? "Today"
                    : job?.JobDate.ToShortDateString(),
                QuoteNumber = job.QuoteNumber,
                IsSelected = false,
                SavedToServer = job.SavedToServer,
                ClientName = job.ClientName,
                DateColour = job.JobDate.Date == DateTime.Now.Date
                    ? Color.Green
                    : Color.White,
                JobDate = job.JobDate,
                JobGang = job.JobGang,
                OperativeIdsPiped = job.OperativeIdsPiped,
                ContractReference = job.ContractReference
            }).GroupBy(x => new { x.GangLeaderId, x.Date });

            foreach (var g in gangs)
                try
                {
                    var gangToAdd = g.FirstOrDefault();

                    if (NavigationalParameters.CurrentSelectedJob.GangLeaderId == gangToAdd.GangLeaderId &&
                        NavigationalParameters.CurrentSelectedJob.JobDate.Date == gangToAdd.JobDate.Date)
                    {
                        gangToAdd.IsSelected = true;
                        gangToAdd.BackgroundHighlighted = Color.Orange;
                    }

                    NavigationalParameters.JobTaskList?.Add(g.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                }

            JobTaskList =
                new ObservableCollection<JobData4Tablet>(NavigationalParameters.JobTaskList.OrderBy(x => x.JobDate));
        }
        else
        {
            try
            {
                await Alert("There are currently no jobs assigned on the resource planner please contact support!",
                    "No jobs assigned");
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }
    }
}