namespace FocusXamarinMobileApplication.ViewModels;

public class
    PlantIssuesPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Checks _checkService;
    public Assignments _assigmentService;
    private Command _cancel;
    private Command _gpsCommand;
    public Jobs _jobService;
    private Command _nb1Command;
    private Command _nb2Command;
    public Plant _plantService;
    private Command _sendToServer;
    private Command _submit;
    private Command _takePhoto;
    private Command _takePicture2;
    private Command _takePicture3;
    private Command _takePicture4;
    public Users _userService;
    private Command _yb1Command;
    private Command _yb2Command;

    public PlantIssuesPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        _plantService = new Plant();
        _checkService = new Checks();
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

    public ImageSource _gpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps");

    public ImageSource GpsImage
    {
        get => _gpsImage;
        set
        {
            _gpsImage = value;
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

    public Color _yesButton1backgroundColour { get; set; } = Color.LightGray;

    public Color YesButton1backgroundColour
    {
        get => _yesButton1backgroundColour;
        set
        {
            _yesButton1backgroundColour = value;
            OnPropertyChanged();
        }
    }

    public Color _yesButton2backgroundColour { get; set; } = Color.LightGray;

    public Color YesButton2backgroundColour
    {
        get => _yesButton2backgroundColour;
        set
        {
            _yesButton2backgroundColour = value;
            OnPropertyChanged();
        }
    }

    public Color _noButton1backgroundColour { get; set; } = Color.LightGray;

    public Color NoButton1backgroundColour
    {
        get => _noButton1backgroundColour;
        set
        {
            _noButton1backgroundColour = value;
            OnPropertyChanged();
        }
    }

    public Color _noButton2backgroundColour { get; set; } = Color.LightGray;

    public Color NoButton2backgroundColour
    {
        get => _noButton2backgroundColour;
        set
        {
            _noButton2backgroundColour = value;
            OnPropertyChanged();
        }
    }

    public PlantIssue _currentPlantIssue { get; set; }

    public PlantIssue CurrentPlantIssue
    {
        get => _currentPlantIssue;
        set
        {
            _currentPlantIssue = value;
            OnPropertyChanged();
        }
    }

    public Plant4Tablet _selectedPlantItem { get; private set; }

    public Plant4Tablet SelectedPlantItem
    {
        get => _selectedPlantItem;
        set
        {
            _selectedPlantItem = value;
            OnPropertyChanged();
        }
    }

    public string _q1Reason { get; set; }

    public string Q1Reason
    {
        get => _q1Reason;
        set
        {
            _q1Reason = value;
            OnPropertyChanged();
        }
    }

    public string _q2Reason { get; set; }

    public string Q2Reason
    {
        get => _q2Reason;
        set
        {
            _q2Reason = value;
            OnPropertyChanged();
        }
    }

    public string _locationText { get; set; }

    public string LocationText
    {
        get => _locationText;
        set
        {
            _locationText = value;
            //  NavigationalParameters.CurrentPlantIssue.LocationText = LocationText;

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

    public RelayCommand PageLoaded => new(async () =>
    {
        Title = "Plant Issue";
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        //LocationText = "";
        SelectedPlantItem = NavigationalParameters.SelectetedPlantItem;
        LocationText = NavigationalParameters.CurrentPlantIssue.LocationText;
        CurrentPlantIssue = NavigationalParameters.CurrentPlantIssue;

        Picture1Url = "";
        Picture2Url = "";
        Picture3Url = "";
        Picture4Url = "";

        if (CurrentPlantIssue?.FirstQuestion.ToLower() == "yes")
        {
            YesButton1backgroundColour = Color.Green;
            NoButton1backgroundColour = Color.LightGray;
        }
        else if (CurrentPlantIssue?.FirstQuestion.ToLower() == "no")
        {
            YesButton1backgroundColour = Color.LightGray;
            NoButton1backgroundColour = Color.Green;
        }
        else
        {
            YesButton1backgroundColour = Color.LightGray;
            NoButton1backgroundColour = Color.LightGray;
        }

        if (CurrentPlantIssue.SecondQuestion.ToLower() == "yes")
        {
            YesButton2backgroundColour = Color.Green;
            NoButton2backgroundColour = Color.LightGray;
        }
        else if (CurrentPlantIssue?.SecondQuestion.ToLower() == "no")
        {
            YesButton2backgroundColour = Color.LightGray;
            NoButton2backgroundColour = Color.Green;
        }
        else
        {
            YesButton2backgroundColour = Color.LightGray;
            NoButton2backgroundColour = Color.LightGray;
        }

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
        {
            IsLoading = false;

            var job = new Jobs();

            if (NavigationalParameters.EntryType == NavigationalParameters.EntryTypes.SUPERVISOR_EDIT)
            {
                NavigationalParameters.CurrentSelectedJob.JobGang =
                    _jobService.GetGang(NavigationalParameters.CurrentSelectedJob);

                NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles.Clear();

                NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles.Add(
                    _userService.GetLoggedInUser());
            }
            else
            {
                NavigationalParameters.CurrentSelectedJob.JobGang =
                    _jobService.GetGang(NavigationalParameters.CurrentSelectedJob);

                NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles.Insert(0,
                    _userService.GetLoggedInUser());
            }
        }

        NavigationalParameters.CurrentPlantIssue = CurrentPlantIssue;
    });

    public RelayCommand TakePhoto => new(async () =>
    {
        NavigationalParameters.CurrentPlantIssue.LocationText = LocationText;
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PLANTISSUE;
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
        CurrentPlantIssue = NavigationalParameters.CurrentPlantIssue;
    });

    public RelayCommand Yb1Command => new(async () =>
    {
        CurrentPlantIssue.FirstQuestion = "Yes";

        YesButton1backgroundColour = Color.Green;
        NoButton1backgroundColour = Color.LightGray;
    });

    public RelayCommand Nb1Command => new(async () =>
    {
        CurrentPlantIssue.FirstQuestion = "No";
        YesButton1backgroundColour = Color.LightGray;
        NoButton1backgroundColour = Color.Green;
    });

    public RelayCommand Yb2Command => new(async () =>
    {
        CurrentPlantIssue.SecondQuestion = "Yes";

        YesButton2backgroundColour = Color.Green;
        NoButton2backgroundColour = Color.LightGray;
    });

    public RelayCommand Nb2Command => new(async () =>
    {
        CurrentPlantIssue.SecondQuestion = "No";
        YesButton2backgroundColour = Color.LightGray;
        NoButton2backgroundColour = Color.Green;
    });

    public RelayCommand Submit => new(async () =>
    {
        if (CurrentPlantIssue.FirstQuestion == "" || CurrentPlantIssue.SecondQuestion == "" ||
            CurrentPlantIssue.Comments == "" || LocationText == "")
        {
            await Alert("Please Complete All fields before continuing!", "Error!");
        }
        else
        {
            HideDisplay.Execute(null);
            CurrentPlantIssue.DateTimeReported = DateTime.Today;
            CurrentPlantIssue.Status = "In Use";
            CurrentPlantIssue.LastUpdateDateTime = DateTime.Now;
            CurrentPlantIssue.PlantId = NavigationalParameters.SelectetedPlantItem.RemotePlantId;
            CurrentPlantIssue.InUseByName = NavigationalParameters.SelectetedPlantItem?.IssuedToName;
            CurrentPlantIssue.LocationText = LocationText;

            NavigationalParameters.AuthDetail = new AuthorisationDetail();
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

            await NavigateTo(ViewModelLocator.SignaturePage);
        }
    });

    public RelayCommand SendToServer => new(async () =>
    {
        if (!string.IsNullOrEmpty(NavigationalParameters.AuthDetail.SignedBy))
        {
            //
            // Need to check if we have a confirmed by PIN user
            //
            if (NavigationalParameters.SelectedUser?.FirstName == "")
            {
                await Alert(
                    "Please Select who is reporting this issue!",
                    "Error!");

                IsLoading = false;

                ShowSection6 = true;
            }
            else
            {
                IsLoading = true;
                ShowSection6 = false;

                var wa = new WebApi();
                var areWeConnected = await wa.CanWeConnect2Api();
                if (areWeConnected)
                {
                    var checkingFlag = "";
                    // OK should be able to send data
                    if (CurrentPlantIssue.Picture1Filename != null &&
                        CurrentPlantIssue.Picture1Filename.Contains(
                            ".jpg") && CurrentPlantIssue
                            .Picture1Status ==
                        "Not Transferred 2 Storage")
                    {
                        CurrentPlantIssue.Picture1Status =
                            await SendPicture2Azure(CurrentPlantIssue
                                .Picture1Filename);
                        checkingFlag +=
                            $"{CurrentPlantIssue.Picture1Status}|";
                    }

                    if (CurrentPlantIssue.Picture2Filename != null &&
                        CurrentPlantIssue.Picture2Filename.Contains(
                            ".jpg") && CurrentPlantIssue
                            .Picture2Status ==
                        "Not Transferred 2 Storage")
                    {
                        CurrentPlantIssue.Picture2Status =
                            await SendPicture2Azure(CurrentPlantIssue
                                .Picture2Filename);
                        checkingFlag +=
                            $"{CurrentPlantIssue.Picture2Status}|";
                    }

                    if (CurrentPlantIssue.Picture3Filename != null &&
                        CurrentPlantIssue.Picture3Filename.Contains(
                            ".jpg") && CurrentPlantIssue
                            .Picture3Status ==
                        "Not Transferred 2 Storage")
                    {
                        CurrentPlantIssue.Picture3Status =
                            await SendPicture2Azure(CurrentPlantIssue
                                .Picture3Filename);
                        checkingFlag +=
                            $"{CurrentPlantIssue.Picture3Status}|";
                    }

                    if (CurrentPlantIssue.Picture4Filename != null &&
                        CurrentPlantIssue.Picture4Filename.Contains(
                            ".jpg") && CurrentPlantIssue
                            .Picture4Status ==
                        "Not Transferred 2 Storage")
                    {
                        CurrentPlantIssue.Picture4Status =
                            await SendPicture2Azure(CurrentPlantIssue
                                .Picture4Filename);
                        checkingFlag +=
                            $"{CurrentPlantIssue.Picture4Status}|";
                    }

                    if (checkingFlag.Contains(
                            "Not Transferred 2 Storage"))
                    {
                        await Alert(
                            "Unable to save data to the Server!",
                            "Error!");

                        IsLoading = false;
                        ShowSection6 = true;
                    }
                    else
                    {
                        CurrentPlantIssue.LocationLatitude =
                            string.Empty;
                        CurrentPlantIssue.LocationLongitude =
                            string.Empty;
                        if (NavigationalParameters.CurrentSelectedJob != null &&
                            NavigationalParameters.CurrentSelectedJob.QuoteNumber > 0)
                        {
                            CurrentPlantIssue.RelevantJobCnumber =
                                NavigationalParameters.CurrentSelectedJob.ContractNumber
                                    .ToString();
                            var packedoutWorksNo =
                                $"000000{NavigationalParameters.CurrentSelectedJob.WorksNumber.ToString()}";
                            CurrentPlantIssue.RelevantJobWorksNumber
                                = packedoutWorksNo.Substring(
                                    packedoutWorksNo.Length - 6);
                            CurrentPlantIssue.RelevantJobQnumber =
                                NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString();
                        }

                        // TEMP as not checking yet
                        CurrentPlantIssue.ReportedByName =
                            NavigationalParameters.LoggedInUser.FullName;
                        CurrentPlantIssue.InUseByName = CurrentPlantIssue.InUseByName;


                        CurrentPlantIssue.GangLeaderEmailAddress =
                            _plantService.GetEmailAddress(NavigationalParameters.SelectetedPlantItem.IssuedToId);
                        CurrentPlantIssue.SupervisorEmailaddress =
                            _plantService.GetEmailAddress(NavigationalParameters.CurrentSelectedJob.SupervisorId);
                        //var n = CurrentPlantIssue;
                        // All good to send data 2 the server
                        var serverId = await wa.SavePlantIssue2Server(CurrentPlantIssue);

                        if (serverId > 0)
                        {
                            // All Good - now show message & exit the page
                            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                                await Alert("Issue reported.", "Success");
                            else
                                await Alert(
                                    "Issue reported. If the issue is stopping work then please also inform your Supervisor!",
                                    "Success");

                            NavigationalParameters.AuthDetail = null;
                            IsLoading = false;
                            ShowSection6 = true;
                            await NavigateTo(ViewModelLocator.MyPlantScreenPage);
                        }
                        else
                        {
                            await Alert(
                                "Unable to save data to the Server, please try to find better connectivity and try again!",
                                "Error!");
                            IsLoading = false;
                            ShowSection6 = true;
                        }
                    }
                }
                else
                {
                    await Alert("No data connectivity, please try to find better connectivity and try again!",
                        "Error!");
                    IsLoading = false;
                    ShowSection6 = true;
                }
            }

            NavigationalParameters.SelectetedPlantItem = null;
            CurrentPlantIssue = null;
            LocationText = "";
            IsLoading = false;
            ShowSection6 = true;
        }
    });

    public RelayCommand GpsCommand => new(async () =>
    {
        NavigationalParameters.MapType = "PlantIssue";
        NavigationalParameters.CurrentPlantIssue.LocationText = LocationText;
        await NavigateTo(ViewModelLocator.FormsMapPage);
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

    public RelayCommand Cancel => new(async () =>
    {
        LocationText = "";

        CurrentPlantIssue = null;

        await NavigateTo(ViewModelLocator.MyPlantScreenPage);
    });

    public bool CheckCompleted()
    {
        return !string.IsNullOrEmpty(CurrentPlantIssue.FirstQuestion)
               && !string.IsNullOrEmpty(CurrentPlantIssue.SecondQuestion)
               && !string.IsNullOrEmpty(CurrentPlantIssue.LocationText);
    }

    public bool UserSelected()
    {
        return !string.IsNullOrEmpty(NavigationalParameters.AuthDetail.AuthorisedName);
    }

    private async Task<string> SendPicture2Azure(string filenameOfPic2Send)
    {
        // First off try & sync 2 Azure
        var transferCheck = "";
        var p = new Pictures();

        if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTISSUE)
            transferCheck = await p.SyncPicture2Azure("JobPictures", "PlantIssuePics", filenameOfPic2Send);
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTTRANSFERIN)
            transferCheck = await p.SyncPicture2Azure("JobPictures", "PicsFromIpad", filenameOfPic2Send);
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTTRANSFEROUT)
            transferCheck = await p.SyncPicture2Azure("JobPictures", "PicsFromIpad", filenameOfPic2Send);

        return transferCheck == "Pic Transferred OK"
            ? "Transferred 2 Storage Successfully"
            : "Not Transferred 2 Storage";
    }
}