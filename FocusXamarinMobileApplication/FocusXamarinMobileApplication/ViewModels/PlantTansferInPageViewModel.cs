using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class PlantTansferInPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public Assignments _assigmentService;
    private Command _cancel;
    public Checks _checkService;
    private Command _dontHaveItemCommand;
    private Command _inStorageItemCommand;
    public Jobs _jobService;
    public Plant _plantService;
    private Command _refreshAllCurrentChecks;
    private Command _rejectPlantItemCommand;
    private Command _saveAfterAuth;
    private Command _submit;
    private Command _takePhoto;
    private Command _takePicture1;
    private Command _takePicture2;
    private Command _takePicture3;
    private Command _takePicture4;
    public Users _userService;

    public PlantTansferInPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        _plantService = new Plant();
        _checkService = new Checks();
        QuestionCollection = new ObservableCollection<YesNoNaPlantInspectionQuestionViewModel>();

        Checks2Show = new List<Checks4Tablet>();
    }

    public string _projectDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

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

    public bool _showSubmitButton { get; set; }

    public bool ShowSubmitButton
    {
        get => _showSubmitButton;
        set
        {
            _showSubmitButton = value;
            OnPropertyChanged();
        }
    }

    public string _picture1Url { get; set; }

    public string Picture1Url
    {
        get => _picture1Url;
        set
        {
            _picture1Url = value;
            OnPropertyChanged();
        }
    }

    public string _picture2Url { get; set; }

    public string Picture2Url
    {
        get => _picture2Url;
        set
        {
            _picture2Url = value;
            OnPropertyChanged();
        }
    }


    public string _picture3Url { get; set; }

    public string Picture3Url
    {
        get => _picture3Url;
        set
        {
            _picture3Url = value;
            OnPropertyChanged();
        }
    }


    public string _picture4Url { get; set; }

    public string Picture4Url
    {
        get => _picture4Url;
        set
        {
            _picture4Url = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _picture1 { get; set; }

    public ImageSource Picture1
    {
        get => _picture1;
        set
        {
            _picture1 = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _picture2 { get; set; }

    public ImageSource Picture2
    {
        get => _picture2;
        set
        {
            _picture2 = value;
            OnPropertyChanged();
        }
    }


    public ImageSource _picture3 { get; set; }

    public ImageSource Picture3
    {
        get => _picture3;
        set
        {
            _picture3 = value;
            OnPropertyChanged();
        }
    }


    public ImageSource _picture4 { get; set; }

    public ImageSource Picture4
    {
        get => _picture4;
        set
        {
            _picture4 = value;
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

    public PlantTransfers _currentPlantTransferItem { get; set; }

    public PlantTransfers CurrentPlantTransferItem
    {
        get => _currentPlantTransferItem;
        set
        {
            _currentPlantTransferItem = value;
            OnPropertyChanged();
        }
    }

    public Plant4Tablet _selectedPlantItem { get; set; }

    public Plant4Tablet SelectedPlantItem
    {
        get => _selectedPlantItem;
        set
        {
            _selectedPlantItem = value;
            OnPropertyChanged();
        }
    }

    public List<Checks4Tablet> _checks2Show { get; set; }

    public List<Checks4Tablet> Checks2Show
    {
        get => _checks2Show;
        set
        {
            _checks2Show = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<YesNoNaPlantInspectionQuestionViewModel> _questionCollection { get; set; }

    public ObservableCollection<YesNoNaPlantInspectionQuestionViewModel> QuestionCollection
    {
        get => _questionCollection;
        set
        {
            _questionCollection = value;
            OnPropertyChanged();
        }
    }

    public List<Checks4TabletResponses> _allCurrentResponses { get; private set; }

    public List<Checks4TabletResponses> AllCurrentResponses
    {
        get => _allCurrentResponses;
        set
        {
            _allCurrentResponses = value;
            OnPropertyChanged();
        }
    }

    public Checks4TabletResponses _newResponse2Add { get; set; }

    public Checks4TabletResponses NewResponse2Add
    {
        get => _newResponse2Add;
        set
        {
            _newResponse2Add = value;
            OnPropertyChanged();
        }
    }

    public Checks4Tablet _selectedQuestion { get; set; }

    public Checks4Tablet SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            _selectedQuestion = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _cameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged();
        }
    }

    public List<Pictures4Tablet> _allImages { get; set; }

    public List<Pictures4Tablet> AllImages
    {
        get => _allImages;
        set
        {
            _allImages = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand TakePhoto => new(async () =>
    {
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PLANTTRANSFERIN;

        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand RefreshAllCurrentChecks => new(async () =>
    {
        try
        {
            Title = "Transfer plant in";

            ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;

            ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");

            SelectedPlantItem = NavigationalParameters.SelectetedPlantItem;

            ShowSection1 = true;
            ShowSection2 = true;
            ShowSection3 = true;
            ShowSection4 = true;
            ShowSection5 = true;
            ShowSection6 = true;

            ShowSubmitButton = true;

            Picture1Url = "";

            Picture2Url = "";

            Picture3Url = "";

            Picture4Url = "";

            NavigationalParameters.AuthDetail = new AuthorisationDetail();
            var tempListOfQuestions = new List<YesNoNaPlantInspectionQuestionViewModel>();

            if (Checks2Show != null && Checks2Show.All(x => x.Type != NavigationalParameters.SelectetedPlantItem.Type))
                Checks2Show = _checkService.GetRelevantChecks(NavigationalParameters.SelectetedPlantItem.Type);
            // _authorisationDetail = new AuthorisationDetail();
            if (NavigationalParameters.SelectetedPlantItem != null &&
                NavigationalParameters.SelectetedPlantItem.Ontransfer)
                CurrentPlantTransferItem = NavigationalParameters.PlantTransfers;

            if (NavigationalParameters.SelectetedPlantItem == null) return;

            AllCurrentResponses =
                _checkService.GetAllRelevantResponses4SelectedDate(
                    NavigationalParameters.SelectetedPlantItem.RemotePlantId,
                    NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                    NavigationalParameters.CurrentSelectedJob.JobDate);

            foreach (var question in Checks2Show)
            {
                var currentAnswer = AllCurrentResponses?.FirstOrDefault(x => x.QuestionId == question.ListIndex)
                    ?.QuestionResponse;

                var buttonAColour = Color.LightGray;
                var buttonBColour = Color.LightGray;
                var buttonCColour = Color.LightGray;

                if (currentAnswer != null)
                {
                    buttonAColour = currentAnswer
                        .ToLower()
                        .Contains("yes")
                        ? Color.Green
                        : Color.LightGray;
                    buttonBColour = currentAnswer
                        .ToLower()
                        .Contains("no")
                        ? Color.Green
                        : Color.LightGray;
                    buttonCColour = currentAnswer
                        .ToLower()
                        .Contains("n/a")
                        ? Color.Green
                        : Color.LightGray;
                }

                tempListOfQuestions.Add(new YesNoNaPlantInspectionQuestionViewModel
                {
                    CheckText = question.CheckText,
                    ListIndex = question.ListIndex,
                    NotifiableResponse = question.NotifiableResponse,
                    Id = question.Id,
                    BtnABgColour = buttonAColour,
                    BtnBBgColour = buttonBColour,
                    BtnCBgColour = buttonCColour,
                    ShowButtonA = true,
                    ShowButtonB = true,
                    ShowButtonC = true
                });
            }

            QuestionCollection = new ObservableCollection<YesNoNaPlantInspectionQuestionViewModel>(tempListOfQuestions);

            SetButtonsBasedOnStatus();
        }
        catch (Exception)
        {
            var n = 1;
        }
    });

    public RelayCommand SaveAfterAuth => new(async () =>
    {
        if (NavigationalParameters.AuthDetail.DetailsCorrect())
        {
            NavigationalParameters.PlantTransfers.TransferSignature = CurrentPlantTransferItem.TransferSignature =
                NavigationalParameters.AuthDetail.SignatureFileName;
            NavigationalParameters.PlantTransfers.TransferOutById = CurrentPlantTransferItem.TransferOutById =
                NavigationalParameters.AuthDetail.FocusId;

            AuthOk4TransferIn();
        }
    });


    public RelayCommand RejectPlantItemCommand => new(async () =>
    {
        AllImages = _plantService.GetPlantImages(NavigationalParameters.PlantTransfers.PlantAssetNo);

        if (CurrentPlantTransferItem.TransferComments == string.Empty)
        {
            await Alert("You MUST input some comments, Reason for Rejection!",
                "Error!");
        }
        else
        {
            if (CurrentPlantTransferItem == null ||
                NavigationalParameters.SelectetedPlantItem == null) return;


            NavigationalParameters.PlantTransfers.Status = CurrentPlantTransferItem.Status = "Reject";

            NavigationalParameters.PlantTransfers.TransferFromId = CurrentPlantTransferItem.TransferFromId =
                NavigationalParameters.SelectetedPlantItem.TransferFromId;

            NavigationalParameters.PlantTransfers.TransferToId = CurrentPlantTransferItem.TransferToId =
                NavigationalParameters.SelectetedPlantItem.TransferToId;

            var wa = new WebApi();

            var areWeConnected = await wa.CanWeConnect2Api();

            if (areWeConnected)
            {
                var checkingFlag = await CheckImages(AllImages);

                // OK should be able to send data
                if (checkingFlag.Contains("Not Transferred 2 Storage"))
                {
                    await Alert(
                        "Unable to save data to the Server, please try to find better connectivity and try again!",
                        "Error!");
                }
                else
                {
                    IsLoading = true;
                    ShowSection6 = false;
                    var serverId = await wa.RejectPlantItem(NavigationalParameters.PlantTransfers);

                    // If get back server Id then all is good so annotate Plant item & move it fromMy Plant tO Transferred Plant
                    if (serverId > 0)
                    {
                        // Success in saving data 2 server now sort databases & show data correctly

                        //  _plantService.RemovePlantById(NavigationalParameters.PlantTransfers.PlantId);

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

    public RelayCommand Submit => new(async () =>
    {
        HideDisplay.Execute(null);
        // Transfer In
        CurrentPlantTransferItem.TransferInOrOut = "In";

        CurrentPlantTransferItem.TransferFromId =
            NavigationalParameters.SelectetedPlantItem.TransferFromId;
        CurrentPlantTransferItem.TransferToId = NavigationalParameters.SelectetedPlantItem.TransferToId;
        CurrentPlantTransferItem.PlantAssetNo =
            Convert.ToInt64(NavigationalParameters.SelectetedPlantItem.AssetNo ?? "0");
        CurrentPlantTransferItem.PlantGroup =
            NavigationalParameters.SelectetedPlantItem.Group; // Need to be added
        CurrentPlantTransferItem.PlantType =
            NavigationalParameters.SelectetedPlantItem.Type; // Need to be added
        CurrentPlantTransferItem.PlantRef =
            NavigationalParameters.SelectetedPlantItem.Ref; // Need to be added

        CurrentPlantTransferItem.PlantId = NavigationalParameters.SelectetedPlantItem.RemotePlantId;

        NavigationalParameters.PlantTransfers = CurrentPlantTransferItem;

        NavigationalParameters.PlantView = NavigationalParameters.PlantViews.TRANSFERIN;

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
            NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
        {
            NavigationalParameters.AuthDetail.Type =
                NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
            NavigationalParameters.AuthorisationType =
                NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
        }
        else
        {
            NavigationalParameters.AuthDetail.Type =
                NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;
            NavigationalParameters.AuthorisationType =
                NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;
        }

        NavigationalParameters.AuthDetail.SignatureFolderName = "JobPictures";

        await NavigateTo(ViewModelLocator.SignaturePage);
    });

    public RelayCommand Cancel => new(async () =>
    {
        //  NavigationalParameters.ReturnPage = "Locator.TransferPlantInKey";
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    private void SetButtonsBasedOnStatus()
    {
        if (Checks2Show != null && Checks2Show.Count > 0 && AllCurrentResponses != null &&
            AllCurrentResponses.Count > 0)
        {
            var distinctAnswerList = new List<Checks4TabletResponses>();

            foreach (var item in AllCurrentResponses)
                if (distinctAnswerList.All(x => x.QuestionId != item.QuestionId))
                    distinctAnswerList.Add(item);

            if (Checks2Show?.Count == distinctAnswerList?.Count) ShowSubmitButton = true;
        }
    }


    private async void UpdateClassSettings(string whichQuestion = "", string questionResult = "")
    {
        CurrentPlantTransferItem.DateTimeTransferStarted = DateTime.Today;
        CurrentPlantTransferItem.Status = "In Use";
        CurrentPlantTransferItem.LastUpdateDateTime = DateTime.Now;
        CurrentPlantTransferItem.PlantId = NavigationalParameters.SelectetedPlantItem.RemotePlantId;

        await _plantService.AddPlantTransferItem(CurrentPlantTransferItem);
    }

    public async void GetCurrentAnswer(Button button)
    {
        try
        {
            SelectedQuestion =
                button.CommandParameter as YesNoNaPlantInspectionQuestionViewModel;

            NavigationalParameters.CurrentCheckAnswer = AllCurrentResponses?
                .OrderByDescending(x => x.DateTimeOfCheck)?
                .FirstOrDefault(x => x.QuestionId == SelectedQuestion.ListIndex);


            var buttonAColour = Color.LightGray;
            var buttonBColour = Color.LightGray;
            var buttonCColour = Color.LightGray;

            if (NavigationalParameters.CurrentCheckAnswer != null)
                NavigationalParameters.CurrentCheckAnswer.QuestionResponse = button.Text;
            else
                NavigationalParameters.CurrentCheckAnswer = new Checks4TabletResponses
                {
                    Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber
                        .ToString(),
                    RelevantDate = DateTime.Now,
                    GangLeaderName = NavigationalParameters.CurrentSelectedJob.GangLeaderName,
                    PlantId = Convert.ToInt32(NavigationalParameters.SelectetedPlantItem.RemotePlantId),
                    QuestionResponse = button.Text,
                    DateTimeOfCheck = DateTime.Now,
                    QuestionId = Convert.ToInt32(SelectedQuestion.ListIndex),
                    PlantCheckedByName = NavigationalParameters.LoggedInUser.FullName,
                    PlantAssignedToName = NavigationalParameters.CurrentSelectedJob.GangLeaderName,
                    ChecksStatus = "ChecksIncomplete"
                };

            _checkService.AddSingleChecksResponse(NavigationalParameters.CurrentCheckAnswer);

            buttonAColour = NavigationalParameters.CurrentCheckAnswer.QuestionResponse
                .ToLower()
                .Contains("yes")
                ? Color.Green
                : Color.LightGray;
            buttonBColour = NavigationalParameters.CurrentCheckAnswer.QuestionResponse
                .ToLower()
                .Contains("no")
                ? Color.Green
                : Color.LightGray;
            buttonCColour = NavigationalParameters.CurrentCheckAnswer.QuestionResponse
                .ToLower()
                .Contains("n/a")
                ? Color.Green
                : Color.LightGray;


            QuestionCollection.Remove(QuestionCollection.First(x => x.ListIndex == SelectedQuestion.ListIndex));

            QuestionCollection.Add(new YesNoNaPlantInspectionQuestionViewModel
            {
                CheckText = SelectedQuestion.CheckText,
                ListIndex = SelectedQuestion.ListIndex,
                NotifiableResponse = SelectedQuestion.NotifiableResponse,
                Id = SelectedQuestion.Id,
                BtnABgColour = buttonAColour,
                BtnBBgColour = buttonBColour,
                BtnCBgColour = buttonCColour,
                ShowButtonA = true,
                ShowButtonB = true,
                ShowButtonC = true
            });

            QuestionCollection = new ObservableCollection<YesNoNaPlantInspectionQuestionViewModel>(QuestionCollection);

            // RefreshAllCurrentChecks.Execute(null);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    private async void AuthOk4TransferIn()
    {
        var p = new Pictures();

        var AllCurrentResponses =
            _checkService.GetAllRelevantResponses4SelectedDate(
                NavigationalParameters.SelectetedPlantItem.RemotePlantId,
                NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                NavigationalParameters.CurrentSelectedJob.JobDate);

        AllImages = _plantService.GetPlantImages(NavigationalParameters.PlantTransfers.PlantAssetNo);

        if (AllCurrentResponses == null || AllCurrentResponses.Count == 0)
        {
            await Alert("Checks Incomplete!", "Error!");
            return;
        }

        var modifiedResponses = new List<Checks4TabletResponses>();

        foreach (var item in AllCurrentResponses)
        {
            var clone = item;
            clone.CheckSubmittedBy = NavigationalParameters.AuthDetail.AuthorisedName;
            clone.SignatureFileName = CurrentPlantTransferItem.TransferSignature;
            clone.PlantCheckedByName = NavigationalParameters.AuthDetail.AuthorisedName;
            clone.ChecksStatus = "ChecksPosted";
            clone.PictureLatitude = "";
            clone.PictureLongitude = "";
            modifiedResponses.Add(clone);
        }

        NavigationalParameters.PlantTransfers.IncomingCheckResults =
            CurrentPlantTransferItem.IncomingCheckResults = modifiedResponses;

        var wa = new WebApi();
        var areWeConnected = await wa.CanWeConnect2Api();
        if (areWeConnected)
        {
            var checkingFlag = await CheckImages(AllImages);

            // OK should be able to send data
            if (checkingFlag.Contains("Not Transferred 2 Storage"))
            {
                await Alert(
                    "Unable to save data to the Server, please try to find better connectivity and try again!",
                    "Error!");
            }
            else
            {
                var serverId = await wa.SavePlantTransferIn2Tablet(NavigationalParameters.PlantTransfers);
                // If get back server Id then all is good so annotate Plant item & move it fromMy Plant tO Transferred Plant
                if (serverId > 0)
                {
                    // Success in saving data 2 server now sort databases & show data correctly
                    await DoRefreshAfterPosting2Server();

                    await NavigateTo(ViewModelLocator.PlantTransferListPage);
                }
                else
                {
                    await Alert(
                        "Unable to save data to the Server, please try to find better connectivity and try again!",
                        "Error!");

                    await NavigateTo(ViewModelLocator.PlantTransferListPage);
                }
            }
        }
    }

    private async Task<string> CheckImages(List<Pictures4Tablet> PlantImages)
    {
        var check = "";

        foreach (var item in PlantImages)
            if (!string.IsNullOrEmpty(item.FileName))
                check += await SendPicture2Azure(item.FileName);

        return check;
    }

    private async Task<string> SendPicture2Azure(string filenameOfPic2Send)
    {
        // First off try & sync 2 Azure
        var p = new Pictures();
        var transferCheck = await p.SyncPicture2Azure("JobPictures", "PicsFromIpad", filenameOfPic2Send);
        if (transferCheck == "Pic Transferred OK")
            return "Transferred 2 Storage Successfully";
        return "Not Transferred 2 Storage";
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
}

public class YesNoNaPlantInspectionQuestionViewModel : Checks4Tablet
{
    public List<string> Options { get; set; }
    public Color BtnABgColour { get; set; } = Color.LightGray;
    public Color BtnBBgColour { get; set; } = Color.LightGray;
    public Color BtnCBgColour { get; set; } = Color.LightGray;
    public bool ShowButtonA { get; set; } = true;
    public bool ShowButtonB { get; set; } = true;
    public bool ShowButtonC { get; set; } = true;
    public bool IsEnabled { get; set; }
}