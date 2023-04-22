using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class PlantTansferPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Checks _checks;
    public Assignments _assigmentService;
    public Jobs _jobService;
    public Plant _plantService;
    public Users _userService;
    public readonly PhotoSelectionPageViewModel _psvm;
    public PlantTansferPageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        _plantService = new Plant();
        _psvm = new PhotoSelectionPageViewModel();
        Operatives2Show = new ObservableCollection<TransferPlantToOperatives>();
        SelectedPlantTransferRecipient = null;
    }


    public string ProjectName { get; set; }
    public string ProjectDate { get; set; }

    private string _title { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    private string _submitButtonText { get; set; }

    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
            OnPropertyChanged();
        }
    }

    private bool _showSection1 { get; set; }

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    private bool _showSection2 { get; set; }

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged();
        }
    }

    private bool _showSection3 { get; set; }

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    private bool _showSection4 { get; set; }

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
        get => _showSection4;
        set
        {
            _showSection5 = value;
            OnPropertyChanged();
        }
    }

    private bool _showSection6 { get; set; }

    public bool ShowSection6
    {
        get => _showSection6;
        set
        {
            _showSection6 = value;
            OnPropertyChanged();
        }
    }

    private string _picture1Url { get; set; }

    public string Picture1Url
    {
        get => _picture1Url;
        set
        {
            _picture1Url = value;
            OnPropertyChanged();
        }
    }

    private string _picture2Url { get; set; }

    public string Picture2Url
    {
        get => _picture2Url;
        set
        {
            _picture2Url = value;
            OnPropertyChanged();
        }
    }


    private string _picture3Url { get; set; }

    public string Picture3Url
    {
        get => _picture3Url;
        set
        {
            _picture3Url = value;
            OnPropertyChanged();
        }
    }


    private string _picture4Url { get; set; }

    public string Picture4Url
    {
        get => _picture4Url;
        set
        {
            _picture4Url = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _picture1 { get; set; }

    public ImageSource Picture1
    {
        get => _picture1;
        set
        {
            _picture1 = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _picture2 { get; set; }

    public ImageSource Picture2
    {
        get => _picture2;
        set
        {
            _picture2 = value;
            OnPropertyChanged();
        }
    }


    private ImageSource _picture3 { get; set; }

    public ImageSource Picture3
    {
        get => _picture3;
        set
        {
            _picture3 = value;
            OnPropertyChanged();
        }
    }


    private ImageSource _picture4 { get; set; }

    public ImageSource Picture4
    {
        get => _picture4;
        set
        {
            _picture4 = value;
            OnPropertyChanged();
        }
    }

    private bool _showRefresh { get; set; }

    public bool ShowRefresh
    {
        get => _showRefresh;
        set
        {
            _showRefresh = value;
            OnPropertyChanged();
        }
    }

    private bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    private bool _showPlantChecksButton { get; set; }

    public bool ShowPlantChecksButton
    {
        get => _showPlantChecksButton;
        set
        {
            _showPlantChecksButton = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TransferPlantToOperatives> _operatives2Show { get; set; }

    public ObservableCollection<TransferPlantToOperatives> Operatives2Show
    {
        get => _operatives2Show;
        set
        {
            _operatives2Show = value;
            OnPropertyChanged();
        }
    }

    private TransferPlantToOperatives _selectedPlantTransferRecipient { get; set; }

    public TransferPlantToOperatives SelectedPlantTransferRecipient
    {
        get => _selectedPlantTransferRecipient;
        set
        {
            _selectedPlantTransferRecipient = value;


            OnPropertyChanged();
        }
    }

    private Plant4Tablet _selectedPlantItem { get; set; }

    public Plant4Tablet SelectedPlantItem
    {
        get => _selectedPlantItem;
        set
        {
            _selectedPlantItem = value;
            OnPropertyChanged();
        }
    }

    private PlantTransfers _currentPlantTransferItem { get; set; }

    public PlantTransfers CurrentPlantTransferItem
    {
        get => _currentPlantTransferItem;
        set
        {
            _currentPlantTransferItem = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _cameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand RefreshCommand => new(async () =>
    {
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PLANTTRANSFEROUT;
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand ScreenLoaded => new(async () =>
    {
        Title = "Transfer plant out";

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

        SelectedPlantItem = NavigationalParameters.SelectetedPlantItem;
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        ShowSection6 = true;
        Picture1Url = "";
        Picture2Url = "";
        Picture3Url = "";
        Picture4Url = "";

        CurrentPlantTransferItem = NavigationalParameters.PlantTransfers;

        var dailySiteInspectionQuestions = _assigmentService.GetSurveyQuestions("")
            .Where(x => x.QuestionOptions != "Button")
            .ToList();

        if (NavigationalParameters.SelectetedPlantItem != null &&
            NavigationalParameters.SelectetedPlantItem.Ontransfer)
            if (NavigationalParameters.SelectetedPlantItem.TransferOutSelected)
            {
                var operatives2Select = _plantService.GetAllPlantTransfer2Users();
                var tempList2 = new List<TransferPlantToOperatives>();
                var tempList = new List<TransferPlantToOperatives>();

                Operatives2Show.Clear();

                var plantInspector = operatives2Select.FirstOrDefault(x => x.FocusId == 320311);
                var returnBase = operatives2Select?.FirstOrDefault(x => x.FocusId == 999999);
                var onHire = operatives2Select?.FirstOrDefault(x => x.FocusId == 999998);

                if (returnBase != null) operatives2Select.Remove(returnBase);

                if (onHire != null) operatives2Select.Remove(onHire);

                if (NavigationalParameters.LoggedInUser.FocusId == 320311)
                {
                    if (plantInspector != null)
                    {
                        operatives2Select.Remove(plantInspector);
                    }

                    tempList.Add(returnBase);

                    tempList.Add(onHire);
                }
                else
                {
                    tempList.Add(plantInspector);
                }

                tempList2.AddRange(operatives2Select.OrderBy(x => x.Surname).ToList());

                tempList.AddRange(tempList2);

                if (SelectedPlantTransferRecipient != null)
                    foreach (var item in tempList)
                        if (item.FocusId != SelectedPlantTransferRecipient.FocusId)
                            item.BackgroundHighlighted = Color.Transparent;
                        else
                            item.BackgroundHighlighted = Color.Orange;

                Operatives2Show = new ObservableCollection<TransferPlantToOperatives>(tempList);
            }
    });

    public RelayCommand Submit => new(async () =>
    {
       
        // Transfer Out
        if (SelectedPlantTransferRecipient != null)
        {
            HideDisplay.Execute(null);
            ShowSection6 = false;
            IsLoading = true;

            NavigationalParameters.PlantTransfers.TransferToId = SelectedPlantTransferRecipient.FocusId;
            NavigationalParameters.PlantTransfers.TransferInOrOut = "Out";

            NavigationalParameters.PlantTransfers.TransferFromId =
                NavigationalParameters.SelectetedPlantItem.IssuedToId;
            NavigationalParameters.PlantTransfers.PlantGroup = NavigationalParameters.SelectetedPlantItem.Group;
            NavigationalParameters.PlantTransfers.PlantType = NavigationalParameters.SelectetedPlantItem.Type;
            NavigationalParameters.PlantTransfers.PlantRef = NavigationalParameters.SelectetedPlantItem.Ref;
            NavigationalParameters.PlantTransfers.PlantId = NavigationalParameters.SelectetedPlantItem.RemotePlantId;
            NavigationalParameters.PlantTransfers.PlantAssetNo =
                Convert.ToInt64(NavigationalParameters.SelectetedPlantItem.AssetNo ?? "0");

            CurrentPlantTransferItem = NavigationalParameters.PlantTransfers;

            NavigationalParameters.AuthDetail.Type =
                NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
            NavigationalParameters.AuthorisationType =
                NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
            NavigationalParameters.AuthDetail.SignatureFolderName = "JobPictures";

            await NavigateTo(ViewModelLocator.SignaturePage);

            // AuthOk4TransferOut();
        }
        else
        {
            await Alert(
                "You MUST Select who you are transferring the Plant to!", "Error!");
        }
    });


    public RelayCommand SaveAfterAuth => new(async () =>
    {
        if (NavigationalParameters.AuthDetail.DetailsCorrect())
        {
            ShowSection6 = false;
            IsLoading = true;

            NavigationalParameters.PlantTransfers.TransferSignature =
                NavigationalParameters.AuthDetail.SignatureFileName;
            NavigationalParameters.PlantTransfers.TransferOutById =
                NavigationalParameters.AuthDetail.FocusId;
            //CurrentPlantTransferItem.TransferFromId = 1;
            NavigationalParameters.PlantTransfers.TransferSignatureStatus =
                "Not Transferred 2 Storage";

            AuthOk4TransferOut();
        }
    });

    public RelayCommand SelectedUserCommand => new(async () =>
    {
        foreach (var item in Operatives2Show)
            if (item.FocusId != SelectedPlantTransferRecipient.FocusId)
                // SelectedPlantTransferRecipient.BackgroundHighlighted = Color.Transparent;
                item.BackgroundHighlighted = Color.Transparent;
            // Operatives2Show.FirstOrDefault(x => x.FocusId == item.FocusId).BackgroundHighlighted = Color.Transparent;
            else
                //  SelectedPlantTransferRecipient.BackgroundHighlighted = Color.Orange;
                item.BackgroundHighlighted = Color.Orange;
        // Operatives2Show.FirstOrDefault(x => x.FocusId == item.FocusId).BackgroundHighlighted = Color.Orange;

        //  Operatives2Show.FirstOrDefault(x => x.FocusId == SelectedPlantTransferRecipient.FocusId).BackgroundHighlighted = Color.Orange;
        Operatives2Show = new ObservableCollection<TransferPlantToOperatives>(Operatives2Show);
    });

    public RelayCommand TakePhoto => new(async () =>
    {
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PLANTTRANSFEROUT;

        _psvm.TakePhoto.Execute(null);
    });

    public RelayCommand HideDisplay => new(async () =>
    {
        IsLoading = true;
        ShowSection1 = false;
        ShowSection2 = false;
        ShowSection3 = false;
        ShowSection4 = false;
        ShowSection5 = false;
        ShowSection6 = false;
    });

    public RelayCommand ShowDisplay => new(async () =>
    {
        IsLoading = false;
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        ShowSection6 = true;
    });

    public RelayCommand Cancel => new(async () => { await NavigateTo(ViewModelLocator.MenuSelectionPage); });

    //private async void UpdateClassSettings(string whichQuestion = "", string questionResult = "")
    //{
    //    CurrentPlantTransferItem.DateTimeTransferStarted = DateTime.Today;
    //    CurrentPlantTransferItem.Status = "In Use";
    //    CurrentPlantTransferItem.LastUpdateDateTime = DateTime.Now;
    //    CurrentPlantTransferItem.PlantId = NavigationalParameters.SelectetedPlantItem.RemotePlantId;

    //    await _plantService.AddPlantTransferItem(CurrentPlantTransferItem);
    //}

    private async void AuthOk4TransferOut()
    {
        // All Ok to send data to server to start Transfer
        // Then save changes to local db to move item from MyPlant Db to TransferOutDb
        var wa = new WebApi();

        var areWeConnected = await wa.CanWeConnect2Api();

        if (areWeConnected)
        {
            var checkingFlag = await CheckImages();

            // OK should be able to send data
            if (checkingFlag.Contains("Not Transferred 2 Storage"))
            {
                await Alert(
                    "Unable to save data to the Server, please try to find better connectivity and try again!",
                    "Error!");
            }
            else
            {
                var serverId = await wa.SavePlantTransferOut2Server(NavigationalParameters.PlantTransfers);

                // If get back server Id then all is good so annotate Plant item & move it fromMy Plant tO Transferred Plant
                if (serverId > 0)
                {
                    // Success in saving data 2 server now need to do a get & get all latest plant with all changes

                    await DoRefreshAfterPosting2Server();

                    await NavigateTo(ViewModelLocator.PlantTransferListPage);
                }
                else
                {
                    await Alert(
                        "Unable to save data to the Server, please try to find better connectivity and try again!",
                        "Error!");
                    NavigateBack();
                }
            }
        }
    }

    private async Task DoRefreshAfterPosting2Server()
    {
        var myOperatives = new List<long>();
        myOperatives.Add(NavigationalParameters.CurrentSelectedJob.GangLeaderId);
        var splitOperatives = NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped.Split('|');
        foreach (var operative in splitOperatives)
            if (operative != string.Empty &&
                Convert.ToInt64(operative) != NavigationalParameters.CurrentSelectedJob.GangLeaderId)
                myOperatives.Add(Convert.ToInt64(operative));

        await _plantService.GetPlant4AllTheseUsersFromServer(myOperatives);
    }

    private async Task<string> CheckImages()
    {
        var status = new[]
        {
            NavigationalParameters.PlantTransfers.Picture1Status,
            NavigationalParameters.PlantTransfers.Picture2Status,
            NavigationalParameters.PlantTransfers.Picture3Status,
            NavigationalParameters.PlantTransfers.Picture4Status
        };
        var fileNames = new[]
        {
            NavigationalParameters.PlantTransfers.Picture1Filename,
            NavigationalParameters.PlantTransfers.Picture2Filename,
            NavigationalParameters.PlantTransfers.Picture3Filename,
            NavigationalParameters.PlantTransfers.Picture4Filename
        };

        var check = "";
        for (var i = 0; i < status.Length; i++)
            if (!string.IsNullOrEmpty(fileNames[i]) && fileNames[i].Contains(".jpg"))
            {
                status[i] = await SendPicture2Azure(fileNames[i]);
                check += $"{status[i]}|";
            }

        return check;
    }

    private async Task<string> SendPicture2Azure(string filenameOfPic2Send)
    {
        // First off try & sync 2 Azure
        var p = new Pictures();
        var transferCheck = await p.SyncPicture2Azure("JobPictures", "PlantTransferPics", filenameOfPic2Send);
        if (transferCheck == "Pic Transferred OK")
            return "Transferred 2 Storage Successfully";
        return "Not Transferred 2 Storage";
    }
}