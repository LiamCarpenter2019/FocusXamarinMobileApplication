#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class MyPlantScreenPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public Checks _checkService;
    private Command _plantChecksCommand;
    private Command _refreshCommand;
    private List<Plant4Tablet> AllPlant;

    public MyPlantScreenPageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        _checkService = new Checks();
    }

    public RelayCommand<string> _transferOutStage { get; private set; }
    public RelayCommand<string> _transferIn { get; private set; }
    public RelayCommand<string> _showPlantDetails { get; private set; }
    public RelayCommand<string> _transferInCommand { get; set; }

    public Assignments _assigmentService { get; }
    public Jobs _jobService { get; }
    public Users _userService { get; }

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

    public JobData4Tablet CurrentSelectedJob { get; private set; }
    public bool _showRefresh { get; set; }

    public bool ShowRefresh
    {
        get => _showRefresh;
        set
        {
            _showRefresh = value;
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

    public bool _showPlantChecksButton { get; set; }

    public bool ShowPlantChecksButton
    {
        get => _showPlantChecksButton;
        set
        {
            _showPlantChecksButton = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<PlantViewModel> _myPlantItemList { get; set; } = new();

    public ObservableCollection<PlantViewModel> MyPlantItemList
    {
        get => _myPlantItemList;
        set
        {
            _myPlantItemList = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(() =>
    {
        Title = $"{NavigationalParameters.CurrentSelectedJob.GangLeaderName}";

        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = false;

        CurrentSelectedJob = NavigationalParameters.CurrentSelectedJob;

        NavigationalParameters.AuthDetail = new AuthorisationDetail();

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR &&
            NavigationalParameters.PlantView != NavigationalParameters.PlantViews.GANGVIEW)
        {
            if (NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName.ToLower().Contains("supervisor")))
            {
                NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.SUPERVISOR_EDIT;
                CurrentSelectedJob.OperativeIdsPiped = NavigationalParameters.LoggedInUser?.FocusId + "|";
                CurrentSelectedJob.SupervisorId = NavigationalParameters.CurrentSelectedJob.GangLeaderId =
                    NavigationalParameters.LoggedInUser.FocusId;
                CurrentSelectedJob.GangLeaderName = NavigationalParameters.LoggedInUser?.FullName;
                CurrentSelectedJob.JobDate = DateTime.Now;
                CurrentSelectedJob.JobGang = _jobService.GetGang(CurrentSelectedJob);
                CurrentSelectedJob.JobGang.GangLabourFiles = new List<Person> { NavigationalParameters.LoggedInUser };
                Title = NavigationalParameters.LoggedInUser.FullName;
            }
            else
            {
                NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.SUPERVISOR_VIEW;
                Title = CurrentSelectedJob.GangLeaderName;
            }
        }
        else
        {
            NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.GANGER;
            Title = CurrentSelectedJob.GangLeaderName;
        }

        SortPlantIntoGroupsAndDisplay();

        IsLoading = false;
        ShowRefresh = true;
    });

    public RelayCommand RefreshCommand => new(async () =>
    {
        IsLoading = true;
        ShowSection4 = false;
        ShowRefresh = false;

        await RefeshMyPlantAsync();
        IsLoading = false;
        ShowSection4 = true;
        ShowRefresh = true;
    });

    public RelayCommand Submit => new(async () =>
    {
        var current = Connectivity.NetworkAccess;

        if (current == NetworkAccess.Internet)
        {
            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
            {
                if (NavigationalParameters.AuthDetail == null)
                    NavigationalParameters.AuthDetail = new AuthorisationDetail();
                NavigationalParameters.AuthDetail.Type = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
            }
            else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            {
                if (NavigationalParameters.AuthDetail == null)
                    NavigationalParameters.AuthDetail = new AuthorisationDetail();
                NavigationalParameters.AuthDetail.Type = NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;

                NavigationalParameters.AuthorisationType =
                    NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;
            }

            NavigationalParameters.AuthDetail.SignatureFolderName = "JobPictures";
            await NavigateTo(ViewModelLocator.SignaturePage);
        }
        else
        {
            await Alert("No internet connectivity",
                "There is currently no internet connectivity, please find a better connection and try again.", "Ok");
        }
    });

    public RelayCommand PlantTransferCommand => new(async () =>
    {
        await NavigateTo(ViewModelLocator.PlantTransferListPage);
    });

    public RelayCommand Back => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            await NavigateTo(ViewModelLocator.SupervisorProjectPage);
        else
            await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });


    public async Task ItemInfoCommand(Button button)
    {
        NavigationalParameters.SelectetedPlantItem = button.CommandParameter as PlantViewModel;
        await NavigateTo(ViewModelLocator.PlantDetailsPage);
    }

    public async Task LogIssueCommand(Button button)
    {
        NavigationalParameters.SelectetedPlantItem = button.CommandParameter as PlantViewModel;
        NavigationalParameters.CurrentPlantIssue = new PlantIssue();
        NavigationalParameters.AuthDetail = new AuthorisationDetail();

        await NavigateTo(ViewModelLocator.PlantIssuePage);
    }

    public async Task PlantChecksCommand(Button button)
    {
        NavigationalParameters.SelectetedPlantItem = button.CommandParameter as PlantViewModel;

        NavigationalParameters.PlantView = NavigationalParameters.PlantViews.PLANTCHECKS;

        NavigationalParameters.PlantChecks = new List<YesNoNaPlantCheckQuestionViewModel>();

        var checksToAdd = _checkService.GetRelevantChecks(NavigationalParameters.SelectetedPlantItem.Type).Select(
            check => new YesNoNaPlantCheckQuestionViewModel
            {
                CheckText = check.CheckText,
                ListIndex = check.ListIndex,
                NotifiableResponse = check.NotifiableResponse,
                Id = check.Id,
                Type = check.Type,
                BtnABgColour = Color.LightGray,
                BtnBBgColour = Color.LightGray,
                BtnCBgColour = Color.LightGray,
                ShowButtonA = true,
                ShowButtonB = true,
                ShowButtonC = true
            }).ToList();

        foreach (var check in checksToAdd)
            if (NavigationalParameters.PlantChecks.All(x => x.ListIndex != check.ListIndex))
                NavigationalParameters.PlantChecks.Add(check);

        await NavigateTo(ViewModelLocator.PlantChecksPage);
    }

    public async Task TransferOutCommand(Button button)
    {
        var tempPlant = button.CommandParameter as PlantViewModel;

        NavigationalParameters.SelectetedPlantItem = new PlantTransferViewModel
        {
            RemotePlantId = tempPlant.RemotePlantId,
            RemoteWheraboutsId = tempPlant.RemoteWheraboutsId,
            Group = tempPlant.Group,
            Type = tempPlant.Type,
            Make = tempPlant.Make,
            Model = tempPlant.Model,
            Serial = tempPlant.Serial,
            Ref = tempPlant.Ref,
            Supplier = tempPlant.Supplier,
            Hired = tempPlant.Hired,
            AssetNo = tempPlant.AssetNo,
            NextServiceDate = tempPlant.NextServiceDate,
            NextServiceDateString = tempPlant.NextServiceDate.ToShortDateString(),
            NextServiceType = tempPlant.NextServiceType,
            CurrentStatus = tempPlant.CurrentStatus,
            IssuedToId = tempPlant.IssuedToId,
            IssuedToName = tempPlant.IssuedToName,
            Ontransfer = tempPlant.Ontransfer,
            TransferFromId = tempPlant.TransferFromId,
            TransferFromName = tempPlant.TransferFromName,
            TransferIn = false,
            TransferOutSelected = true,
            TransferToId = tempPlant.TransferToId,
            TransferToName = tempPlant.TransferToName,
            Responses4ThisPlant2day = tempPlant.Responses4ThisPlant2day,
            IsEnabled = true
        };

        NavigationalParameters.PlantTransfers = new PlantTransfers
        {
            TransferToId = NavigationalParameters.SelectetedPlantItem.TransferToId,
            TransferFromId = NavigationalParameters.SelectetedPlantItem.TransferFromId,
            Status = NavigationalParameters.SelectetedPlantItem?.CurrentStatus,
            LastUpdateDateTime = DateTime.Now,
            PlantAssetNo = Convert.ToInt64(NavigationalParameters.SelectetedPlantItem?.AssetNo),
            PlantGroup = NavigationalParameters.SelectetedPlantItem?.Group,
            PlantId = NavigationalParameters.SelectetedPlantItem.RemotePlantId,
            PlantRef = NavigationalParameters.SelectetedPlantItem?.Ref,
            PlantType = NavigationalParameters.SelectetedPlantItem?.Type,
            TransferToName = NavigationalParameters.SelectetedPlantItem?.TransferToName,
            TransferFromName = NavigationalParameters.SelectetedPlantItem?.TransferFromName,
            DateTimeTransferStarted = DateTime.Now
        };

        NavigationalParameters.PlantView = NavigationalParameters.PlantViews.TRANSFEROUT;

        NavigationalParameters.SelectetedPlantItem.TransferOutSelected = true;

        NavigationalParameters.SelectetedPlantItem.Ontransfer = true;

        await NavigateTo(ViewModelLocator.PlantTransferPage);
    }

    private async Task RefeshMyPlantAsync()
    {
        var myOperatives = new List<long>();
        if (NavigationalParameters.CurrentSelectedJob.GangLeaderId > 0
            && NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped != null
            && NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped.Length > 5
            && NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped.Contains("|"))
        {
            myOperatives.Add(NavigationalParameters.CurrentSelectedJob.GangLeaderId);
            var splitOperatives = NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped.Split('|');
            foreach (var operative in splitOperatives)
                if (operative != string.Empty &&
                    Convert.ToInt64(operative) != NavigationalParameters.CurrentSelectedJob.GangLeaderId)
                    myOperatives.Add(Convert.ToInt64(operative));

            var p = new Plant();

            if (await p.GetPlant4AllTheseUsersFromServer(myOperatives))
                SortPlantIntoGroupsAndDisplay();
            else
                await Alert(
                    "Unable to get data from the Server, please try to find better connectivity and try again!",
                    "Error!");
        }
        else
        {
            await Alert("Unable to detail gang structure so I cannot do this this time!",
                "Error!");
        }
    }

    private void SortPlantIntoGroupsAndDisplay()
    {
        AllPlant = new List<Plant4Tablet>();
        MyPlantItemList = new ObservableCollection<PlantViewModel>();

        if (NavigationalParameters.ProjectListMode == NavigationalParameters.ProjectListModes.PROJECTLIST)
        {
            NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.SUPERVISOR_EDIT;

            AllPlant = new Plant().GetAllPlant().Where(x => x.IssuedToId == NavigationalParameters.LoggedInUser.FocusId)
                .ToList();
        }
        else
        {
            var operatives = CurrentSelectedJob?.OperativeIdsPiped.Split('|');

            foreach (var item in operatives)
                if (!string.IsNullOrEmpty(item))
                {
                    var items = new Plant()?.GetAllPlant()?.ToList();

                    AllPlant.AddRange(items.Where(x => x.IssuedToId.ToString() == item).ToList());
                }
            //AllPlant = new Plant().GetAllPlant().Where(x => x.IssuedToId == NavigationalParameters.CurrentSelectedJob.GangLeaderId).ToList();
        }

        // var foundPlant = new Plant().GetAllPlant();


        var date = Convert.ToDateTime("01/01/0001").ToShortDateString();

        var canShowPipedList = string.IsNullOrEmpty(NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped)
            ? $"{NavigationalParameters.LoggedInUser.FocusId}|{NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped}"
            : $"{NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped}";

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
            NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
            canShowPipedList = $"{NavigationalParameters.CurrentSelectedJob.SupervisorId}|{canShowPipedList}";

        if (NavigationalParameters.EntryType == NavigationalParameters.EntryTypes.SUPERVISOR_EDIT)
            canShowPipedList = NavigationalParameters.CurrentSelectedJob.SupervisorId + "|";

        var checkTotalCount = 0;
        var TotalResponseCount = 0;
        var currrentResponses = new List<Checks4TabletResponses>();

        foreach (var item in AllPlant)
        {
            // var checksCompleted = false;

            var allCurrentResponses =
                _checkService.GetAllRelevantResponses4SelectedDate(item.RemotePlantId, DateTime.Now);
            currrentResponses.AddRange(allCurrentResponses);

            var checksCount = _checkService.GetRelevantChecks(item.Type).Count();

            if (allCurrentResponses.Count() > 1 || (allCurrentResponses.FirstOrDefault()?.QuestionId != 99998 &&
                                                    allCurrentResponses.FirstOrDefault()?.QuestionId != 99999))
            {
                TotalResponseCount = TotalResponseCount + allCurrentResponses.Count();

                checkTotalCount = checkTotalCount + checksCount;
            }

            if (allCurrentResponses.Count() >= checksCount)
                item.ChecksComplete = true;
            else
                item.ChecksComplete = allCurrentResponses.Any(x =>
                    x.PlantId == item.RemotePlantId && (x.QuestionId == 99999 || x.QuestionId == 99998));

            var newItem = new PlantViewModel
            {
                AssetNo = item.AssetNo,
                CurrentStatus = item.CurrentStatus,
                Group = item.Group,
                Hired = item.Hired,
                Id = item.Id,
                ChecksComplete = item.ChecksComplete,
                IssuedToId = item.IssuedToId,
                IssuedToName = item.IssuedToName,
                Make = item.Make,
                Model = item.Model,
                Ontransfer = item.Ontransfer,
                NextServiceDate = item.NextServiceDate,
                NextServiceDateString = item.NextServiceDate.ToShortDateString() == date
                    ? "N/A"
                    : item.NextServiceDate.ToShortDateString(),
                NextServiceType = item.NextServiceType,
                Ref = item.Ref,
                RemotePlantId = item.RemotePlantId,
                RemoteWheraboutsId = item.RemoteWheraboutsId,
                Responses4ThisPlant2day = item.Responses4ThisPlant2day,
                Serial = item.Serial,
                Supplier = item.Supplier,
                TransferFromId = item.TransferFromId,
                TransferFromName = item.TransferFromName,
                TransferOutSelected = item.TransferOutSelected,
                TransferToId = item.TransferToId,
                TransferToName = item.TransferToName,
                Type = item.Type,
                IsEnabled = !item.ChecksComplete
            };

            if (canShowPipedList.Contains(item.IssuedToId + "|"))
                if (!MyPlantItemList.Contains(newItem))
                    MyPlantItemList.Add(newItem);
        }

        if (TotalResponseCount >= checkTotalCount && currrentResponses.All(x => x.ServerId == 0))
            ShowSection4 = true;
        else
            ShowSection4 = false;
    }
}