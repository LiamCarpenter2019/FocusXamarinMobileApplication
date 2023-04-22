#region

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
using Application = Xamarin.Forms.Application;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SupervisorProjectPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly SupervisorApprovalsPageViewModel _approvals;

    private readonly Assignments _assignmentService;

    private readonly Jobs _jobService;

    private readonly Users _userService;

    private RelayCommand<string> _checkLogin;

    private RelayCommand _logOutCommand;

    private RelayCommand<string> _refreshCommand;

    public SupervisorProjectPageViewModel()
    {
        _jobService = new Jobs();

        _userService = new Users();

        _assignmentService = new Assignments();

        _approvals = new SupervisorApprovalsPageViewModel();

        UserName = NavigationalParameters.LoggedInUser?.FullName;
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

    public bool logonCheckResult { get; set; }

    public List<IGrouping<string, Assignment>> AssignmentGroup { get; set; }

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

    public List<TaskViewModel> tempList { get; private set; }

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

    private string _userName { get; set; } = NavigationalParameters.LoggedInUser.FullName;

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
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

    public bool _showButtonK { get; set; }

    public bool ShowButtonK
    {
        get => _showButtonK;
        set
        {
            _showButtonK = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonL { get; set; }

    public bool ShowButtonL
    {
        get => _showButtonL;
        set
        {
            _showButtonL = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonM { get; set; }

    public bool ShowButtonM
    {
        get => _showButtonM;
        set
        {
            _showButtonM = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonN { get; set; }

    public bool ShowButtonN
    {
        get => _showButtonN;
        set
        {
            _showButtonN = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonO { get; set; }

    public bool ShowButtonO
    {
        get => _showButtonO;
        set
        {
            _showButtonO = value;
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

    public ImageSource _ButtonKImage { get; set; } = "";

    public ImageSource ButtonKImage
    {
        get => _ButtonKImage;
        set
        {
            _ButtonKImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonLImage { get; set; } = "";

    public ImageSource ButtonLImage
    {
        get => _ButtonLImage;
        set
        {
            _ButtonLImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonMImage { get; set; } = "";

    public ImageSource ButtonMImage
    {
        get => _ButtonMImage;
        set
        {
            _ButtonMImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonNImage { get; set; } = "";

    public ImageSource ButtonNImage
    {
        get => _ButtonNImage;
        set
        {
            _ButtonNImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _ButtonOImage { get; set; } = "";

    public ImageSource ButtonOImage
    {
        get => _ButtonOImage;
        set
        {
            _ButtonOImage = value;
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

    public ProjectViewModel _selectedProjectItem { get; set; }

    public ProjectViewModel SelectedProjectItem
    {
        get => _selectedProjectItem;
        set
        {
            _selectedProjectItem = value;
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

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = $"{DateTime.Now.Date:dd/MM/yyyy}";
        //=================== 1st row ===========================================
        ButtonAImage = SimpleStaticHelpers.GetImage("Settings");
        ButtonALabel = "Settings";

        ButtonBImage = SimpleStaticHelpers.GetImage("DocumentsListing");
        ButtonBLabel = "Company Docs";

        ButtonCImage = SimpleStaticHelpers.GetImage("InputDiary");
        ButtonCLabel = "Daily Diary";

        ButtonDImage = SimpleStaticHelpers.GetImage("MyPlantScreen");
        ButtonDLabel = "Plant";

        ButtonEImage = SimpleStaticHelpers.GetImage("Gang");
        ButtonELabel = "View Training";

        //=================== 2nd row ===========================================
        //ButtonFImage = SimpleStaticHelpers.GetImage("AsBuilt");
        //ButtonFLabel = "As Built";

        //ButtonFImage = SimpleStaticHelpers.GetImage("");
        //ButtonFLabel = "";

        //ButtonGImage = SimpleStaticHelpers.GetImage("Strike");
        //ButtonGLabel = "Investigation";

        ButtonGImage = SimpleStaticHelpers.GetImage("");
        ButtonGLabel = "";

        ButtonHImage = SimpleStaticHelpers.GetImage("Gang");
        ButtonHLabel = "Task List";

        //ButtonIImage = SimpleStaticHelpers.GetImage("InputDiary");
        //ButtonILabel = "Commercial";

        ButtonIImage = SimpleStaticHelpers.GetImage("");
        ButtonILabel = "";

        ButtonJImage = SimpleStaticHelpers.GetImage("");
        ButtonJLabel = "";

        NavigationalParameters.LoggedInUser = _userService.Check4ValidLoggedInPerson();

        NavigationalParameters.Abbreviations = _assignmentService.GetAbbreviations()?.ToList();

        NavigationalParameters.AllJobs = _jobService.GetAllJobs()?.ToList();

        AssignmentGroup = NavigationalParameters.AssignmentList?.GroupBy(x => x.Category)?.ToList();

        UserName = NavigationalParameters.LoggedInUser?.FullName;

        ShowSection3 = true;
        ShowButtonA = true;
        ShowButtonB = true;
        ShowButtonC = true;
        ShowButtonD = true;
        ShowButtonE = true;

        ShowSection4 = true;
        ShowButtonF = true;
        ShowButtonG = true;
        ShowButtonH = true;
        ShowButtonI = true;
        ShowButtonJ = true;

        ProjectName = "";

        NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;

        NavigationalParameters.CurrentSelectedJob =
            NavigationalParameters.AllJobs?.FirstOrDefault(x => x.JobDate.Date == DateTime.Now.Date);

        UpdateJobs(NavigationalParameters.AllJobs
            ?.Where(x => x.SupervisorId == NavigationalParameters.LoggedInUser?.FocusId).ToList());
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
        NavigationalParameters.CurrentSelectedJob = SelectedProjectItem;
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;
        //  NavigationalParameters.ReturnPage = "MainListPageViewModelKey";
        _jobService.UpdateJobToSelected(NavigationalParameters.CurrentSelectedJob);

        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    public RelayCommand LogOutCommand => _logOutCommand ??= new RelayCommand(async () =>
    {
        try
        {
            var signout = await Alert("Sign out?", "Do you want to sign out?", "Yes", "No");

            if (signout)
            {
                await (Application.Current as App)?.SignOut();

                NavigationalParameters.ResetParams();

                await NavigateTo(ViewModelLocator.LogInPage);
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand NavToComercialCommand => new(async () =>
    {
        //NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMMERCIAL;
        //await NavigateTo(ViewModelLocator.CommercialListPage);
    });

    public RelayCommand NavToSettings => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.SETTINGS;
        await NavigateTo(ViewModelLocator.SettingsPage);
    });

    public RelayCommand NavToDocuments => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMPANYDOCS;

        await NavigateTo(ViewModelLocator.DocumentListingPage);
        ;
    });

    public RelayCommand NavToDailyDiary => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUPERVISORINPUTDIARIES;
        //TODO: create supervisor diary page
        await NavigateTo(ViewModelLocator.SupervisorDiaryPage);
    });

    public RelayCommand NavigateToPlant => new(async () =>
    {
        NavigationalParameters.PlantView = NavigationalParameters.PlantViews.SUPERVISOR;

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.PLANT;

        await NavigateTo(ViewModelLocator.MyPlantScreenPage);
    });

    public RelayCommand NavigateToSupervisorTrainingDocs => new(async () =>
    {
        NavigationalParameters.CurrentSelectedJob = NavigationalParameters.AllJobs?.FirstOrDefault();
        NavigationalParameters.ReturnFromDocView = false;
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.TRAININGDOCS;

        await NavigateTo(ViewModelLocator.DocumentListingPage);
        ;
    });

    public RelayCommand NavToAsBuilt => new(async () =>
    {
        //NavigationalParameters.AppMode = NavigationalParameters.AppModes.ASBUILT;

        //if (AssignmentGroup.Any(x => x.Key == "AsBuilt"))
        //{
        //    NavigationalParameters.AppMode = NavigationalParameters.AppModes.ASBUILT;
        //    await NavigateTo(ViewModelLocator.SelectSurveyTypePage);;
        //}
        //else
        //{
        //    await Alert("No Assignments Available", "As Built");
        //}
    });

    public RelayCommand NavToInvestigationView => new(async () =>
    {
        //var dl = App.Database.GetItems<InvestigateDamages>().ToList();
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE;
        //  NavigationalParameters.CurrenSelectedJob =
        //    JobsTaskList.FirstOrDefault(x => x.ProjectName == "VM NBU 148527");

        await NavigateTo(ViewModelLocator.SelectInvestigationPage);
        //"http://maps.apple.com/?q=Great+George+St+Leeds+LS1+3EX"
        //NavigationalParameters.NavigationService = _navigationService;
        //NavigationalParameters.PassedInfo = new SelectInvestigationPage();
        //_navigationService.NavigateTo(Locator.FormsViewKey, Locator.MainListPageViewModelKey);
    });

    public RelayCommand NavToTaskList => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.TASKLIST;
        NavigationalParameters.CurrentSelectedJob = null;
        await NavigateTo(ViewModelLocator.TaskListPage);
    });

    public RelayCommand NoDataError => new(async () =>
    {
        await Alert("No Job Data Available, please contact your supervisor.",
            "Data Return Error");
        //_navigationService.GoBack();
        NavigationalParameters.NavigationParameter = "init";
        await NavigateTo(ViewModelLocator.LogInPage);
        ;
    });

    public RelayCommand<string> CheckLogin => _checkLogin ??= new RelayCommand<string>(async e =>
    {
        // NavigationalParameters.AppMode = NavigationalParameters.AppModes.LOGIN;
        var aa = new AzureAuth();

        var cl = aa.GetUserAuthInformation();

        if (cl != null && cl.LoginDateTime != null)
        {
            // NavigationalParameters.InitialLogOn =                     (DateTime) cl.LoginDateTime;

            var nowMinus10Hrs = DateTime.Now.AddHours(-10);

            //DateTime nowMinus10Hrs = DateTime.Now.AddMinutes(-1);

            if (cl.LoginDateTime < nowMinus10Hrs)
            {
                // var n = cl.LoginDateTime;
                await aa.SignoutUser();

                // NavigationalParameters.ReturnPage = "MainListPageViewModelKey";
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.LOGIN;
                await NavigateTo(ViewModelLocator.LogInPage);
                ;
                //_navigationService.NavigateTo(Locator.LoginPageViewModelKey, Locator.MainListPageViewModelKey);

                logonCheckResult = false;
            }
            else
            {
                if (cl.LoginDateTime > nowMinus10Hrs &&
                    cl.LoginDateTime <= DateTime.Now)
                {
                    var n = cl.LoginDateTime;
                    logonCheckResult = true;
                }
                else
                {
                    await Alert(
                        "Warning!! You are about to be logged out",
                        "Logging out");

                    var n = cl.LoginDateTime;
                    await aa.SignoutUser();
                    logonCheckResult = false;

                    //NavigationalParameters.ReturnPage = "MainListPageViewModelKey";
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.LOGIN;
                    await NavigateTo(ViewModelLocator.LogInPage);
                    ;
                }
            }
        }
    });

    public async void UpdateJobs(List<JobData4Tablet> jobs)
    {
        NavigationalParameters.JobTaskList = new List<JobData4Tablet>();

        JobTaskList = new ObservableCollection<JobData4Tablet>();

        tempList = new List<TaskViewModel>();

        ProjectName = "";

        if (jobs != null && jobs.Any())
        {
            var gangs = jobs.Select(job => new ProjectViewModel
            {
                GangLeaderName = job.GangLeaderName,
                GangLeaderId = job.GangLeaderId,
                SupervisorId = job.SupervisorId,
                ProjectManagerId = job.ProjectManagerId,
                OperativeNames = job.OperativeNames,
                DailyChecksCompleted = job.DailyChecksCompleted,
                DailyChecksPosted = job.DailyChecksPosted,
                JobId = job.JobId,
                ProjectName = job.ProjectName,
                Date = DateTime.Now.Date.ToShortDateString(),
                QuoteNumber = job.QuoteNumber,
                IsSelected = job.IsSelected,
                SavedToServer = job.SavedToServer,
                ClientName = job.ClientName,
                DateColour = job.JobDate.Date == DateTime.Now.Date
                    ? Color.Green
                    : Color.White,
                JobDate = job.JobDate,
                GangCount = "0",
                JobGang = job.JobGang,
                OperativeIdsPiped = job.OperativeIdsPiped,
                ContractReference = job.ContractReference
            }).GroupBy(x => x.ProjectName);

            foreach (var g in gangs)
                try
                {
                    var jobToAdd = g.FirstOrDefault();
                    jobToAdd.GangCount = g.GroupBy(x => x.GangLeaderId).Count().ToString();

                    if (NavigationalParameters.CurrentSelectedJob.ProjectName == jobToAdd.ProjectName)
                    {
                        jobToAdd.IsSelected = true;
                        jobToAdd.BackgroundHighlighted = Color.Orange;
                    }

                    NavigationalParameters.JobTaskList?.Add(jobToAdd);
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                }

            JobTaskList = new ObservableCollection<JobData4Tablet>(NavigationalParameters.JobTaskList);
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