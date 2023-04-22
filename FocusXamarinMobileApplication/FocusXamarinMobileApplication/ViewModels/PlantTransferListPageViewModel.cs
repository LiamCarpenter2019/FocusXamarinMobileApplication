#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class PlantTransferListPageViewModel : FBaseViewModel
{
    public PlantTransferListPageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
    }

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

    public ObservableCollection<PlantTransferViewModel> _planItemsToTransferIn { get; set; }

    public ObservableCollection<PlantTransferViewModel> PlanItemsToTransferIn
    {
        get => _planItemsToTransferIn;
        set
        {
            _planItemsToTransferIn = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<PlantTransferViewModel> _planItemsToTransferOut { get; set; }

    public ObservableCollection<PlantTransferViewModel> PlanItemsToTransferOut
    {
        get => _planItemsToTransferOut;
        set
        {
            _planItemsToTransferOut = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        Title = "Plant";

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        //NavigationalParameters.AuthDetail = new AuthorisationDetail();

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR
            && NavigationalParameters.PlantView != NavigationalParameters.PlantViews.GANGVIEW)
        {
            if (NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName.ToLower().Contains("supervisor")))
            {
                NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.SUPERVISOR_EDIT;

                if (NavigationalParameters.CurrentSelectedJob == null)
                {
                    NavigationalParameters.CurrentSelectedJob = _jobService.GetAllJobs()
                        .FirstOrDefault(X => X.SupervisorId == NavigationalParameters.LoggedInUser.FocusId);

                    if (NavigationalParameters.CurrentSelectedJob != null)
                    {
                        NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped =
                            NavigationalParameters.LoggedInUser.FocusId + "|";
                        NavigationalParameters.CurrentSelectedJob.SupervisorId =
                            NavigationalParameters.CurrentSelectedJob.GangLeaderId =
                                NavigationalParameters.LoggedInUser.FocusId;
                        NavigationalParameters.CurrentSelectedJob.GangLeaderName =
                            NavigationalParameters.LoggedInUser?.FullName;
                        NavigationalParameters.CurrentSelectedJob.JobDate = DateTime.Now;
                        //ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");
                        NavigationalParameters.CurrentSelectedJob.JobGang =
                            _jobService.GetGang(NavigationalParameters.CurrentSelectedJob);
                        NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles = new List<Person>
                            { NavigationalParameters.LoggedInUser };
                    }
                }
            }
            else
            {
                NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.SUPERVISOR_VIEW;
            }
        }
        else
        {
            NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.GANGER;
        }

        SortPlantIntoGroupsAndDisplay();

        IsLoading = false;

        ShowRefresh = true;
    });

    public RelayCommand TransferInCommand => new(async () =>
    {
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
        await NavigateTo(ViewModelLocator.PlantTransferInPage);
    });

    public RelayCommand TransferOutCommand => new(async () =>
    {
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
        await NavigateTo(ViewModelLocator.PlantTransferPage);
    });

    public RelayCommand GoBack => new(async () =>
    {
        //NavigateBack();
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SUPERVISORGANGPLANT)
            {
                await NavigateTo(ViewModelLocator.GangSelectPage);
            }
            else
            {
                await NavigateTo(ViewModelLocator.SupervisorProjectPage);
            }
        }
        else
        {
            await NavigateTo(ViewModelLocator.MyPlantScreenPage);
        }
    });

    public RelayCommand MyPlantCommand => new(async () => { await NavigateTo(ViewModelLocator.MyPlantScreenPage); });

    public List<Plant4Tablet> AllPlant { get; private set; }

    private void SortPlantIntoGroupsAndDisplay()
    {
        SortMyProps();

        PlanItemsToTransferOut.Clear();
        PlanItemsToTransferIn.Clear();

        // ----------------------------------       
        AllPlant = new List<Plant4Tablet>();

        if (NavigationalParameters.ProjectListMode == NavigationalParameters.ProjectListModes.PROJECTLIST)
        {
            NavigationalParameters.EntryType = NavigationalParameters.EntryTypes.SUPERVISOR_EDIT;

            AllPlant = new Plant()?
                .GetAllPlant()?
                .Where(x => x.IssuedToId == NavigationalParameters.LoggedInUser.FocusId
                            || x.TransferToId == NavigationalParameters.LoggedInUser.FocusId
                            || x.TransferFromId == NavigationalParameters.LoggedInUser.FocusId)?
                .ToList();
        }
        else
        {
            var operatives = NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped.Split('|');

            foreach (var item in operatives)
                if (!string.IsNullOrEmpty(item))
                {
                    var items = new Plant()?.GetAllPlant()?.ToList();

                    AllPlant.AddRange(items.Where(x =>
                        x.IssuedToId.ToString() == item || x.TransferToId.ToString() == item ||
                        x.TransferFromId.ToString() == item).ToList());
                }
        }

        foreach (var item in AllPlant)
            if (item.Ontransfer)
            {
                if (item.TransferOutSelected)
                {
                    var newItem = new PlantTransferViewModel
                    {
                        AssetNo = item.AssetNo,
                        CurrentStatus = item.CurrentStatus,
                        Group = item.Group,
                        Hired = item.Hired,
                        Id = item.Id,
                        IsEnabled = true,
                        IssuedToId = item.IssuedToId,
                        IssuedToName = item.IssuedToName,
                        Make = item.Make,
                        Model = item.Model,
                        Ontransfer = item.Ontransfer,
                        NextServiceDate = item.NextServiceDate,
                        NextServiceType = item.NextServiceType,
                        Ref = item.Ref,
                        RemotePlantId = item.RemotePlantId,
                        RemoteWheraboutsId = item.RemoteWheraboutsId,
                        Responses4ThisPlant2day = item.Responses4ThisPlant2day,
                        Serial = item.Serial,
                        Supplier = item.Supplier,
                        TransferFromId = item.TransferFromId,
                        TransferFromName = item.TransferFromName,
                        TransferIn = false,
                        TransferOutSelected = item.TransferOutSelected,
                        TransferToId = item.TransferToId,
                        TransferToName = item.TransferToName,
                        Type = item.Type
                    };

                    if (!PlanItemsToTransferOut.Contains(item)) PlanItemsToTransferOut.Add(newItem);
                }
                else
                {
                    var newItem = new PlantTransferViewModel
                    {
                        AssetNo = item.AssetNo,
                        CurrentStatus = item.CurrentStatus,
                        Group = item.Group,
                        Hired = item.Hired,
                        Id = item.Id,
                        IsEnabled = true,
                        IssuedToId = item.IssuedToId,
                        IssuedToName = item.IssuedToName,
                        Make = item.Make,
                        Model = item.Model,
                        Ontransfer = item.Ontransfer,
                        NextServiceDate = item.NextServiceDate,
                        NextServiceType = item.NextServiceType,
                        Ref = item.Ref,
                        RemotePlantId = item.RemotePlantId,
                        RemoteWheraboutsId = item.RemoteWheraboutsId,
                        Responses4ThisPlant2day = item.Responses4ThisPlant2day,
                        Serial = item.Serial,
                        Supplier = item.Supplier,
                        TransferFromId = item.TransferFromId,
                        TransferFromName = item.TransferFromName,
                        TransferIn = true,
                        TransferOutSelected = item.TransferOutSelected,
                        TransferToId = item.TransferToId,
                        TransferToName = item.TransferToName,
                        Type = item.Type
                    };

                    if (!PlanItemsToTransferIn.Contains(newItem)) PlanItemsToTransferIn.Add(newItem);
                }
            }
    }

    private void SortMyProps()
    {
        PlanItemsToTransferOut = new ObservableCollection<PlantTransferViewModel>();

        PlanItemsToTransferIn = new ObservableCollection<PlantTransferViewModel>();
    }
}