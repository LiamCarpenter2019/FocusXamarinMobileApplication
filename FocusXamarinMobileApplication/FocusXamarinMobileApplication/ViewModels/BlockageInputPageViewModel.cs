namespace FocusXamarinMobileApplication.ViewModels;

public class BlockageInputPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    [Time]
    public BlockageInputPageViewModel()
    {
        _assigmentService = new Assignments();

        _jobService = new Jobs();

        _userService = new Users();

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

        ShowSection1 = true;

        ShowSection2 = true;

        ShowSection3 = false;

        ShowClearance = false;

        IsLoading = false;

        BlockageReasons = NavigationalParameters.BlockageReasons;
    }

    public Assignments _assigmentService { get; set; }
    public Jobs _jobService { get; set; }
    public Users _userService { get; set; }

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

    public string _blockageButtonText { get; set; }

    public string BlockageButtonText
    {
        get => _blockageButtonText;
        set
        {
            _blockageButtonText = value;
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

    public bool _showClearance { get; set; }

    public bool ShowClearance
    {
        get => _showClearance;
        set
        {
            _showClearance = value;
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

    public bool _canWrite { get; set; }

    public bool CanWrite
    {
        get => _canWrite;
        set
        {
            _canWrite = value;
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

    public Color _blockageButtonColour { get; set; }

    public Color BlockageButtonColour
    {
        get => _blockageButtonColour;
        set
        {
            _blockageButtonColour = value;
            OnPropertyChanged();
        }
    }

    public Blockage _selectedBlockage { get; set; }

    public Blockage SelectedBlockage
    {
        get => _selectedBlockage;
        set
        {
            _selectedBlockage = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan _startTime { get; set; }

    public TimeSpan StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;

            OnPropertyChanged();
        }
    }

    public TimeSpan _endTime { get; set; }

    public TimeSpan EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> _blockageReasons { get; set; }

    public ObservableCollection<string> BlockageReasons
    {
        get => _blockageReasons;
        set
        {
            _blockageReasons = value;
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

    public string _referenceA { get; set; }

    public string ReferenceA
    {
        get => _referenceA;
        set
        {
            _referenceA = value;

            OnPropertyChanged();
        }
    }

    public string _referenceB { get; set; }

    public string ReferenceB
    {
        get => _referenceB;
        set
        {
            _referenceB = value;

            OnPropertyChanged();
        }
    }

    public string _lengthA { get; set; }

    public string LengthA
    {
        get => _lengthA;
        set
        {
            _lengthA = value;

            OnPropertyChanged();
        }
    }

    public string _lengthB { get; set; }

    public string LengthB
    {
        get => _lengthB;
        set
        {
            _lengthB = value;

            OnPropertyChanged();
        }
    }

    public string _comments { get; set; }

    public string Comments
    {
        get => _comments;
        set
        {
            _comments = value;

            OnPropertyChanged();
        }
    }

    public string _location { get; set; }

    public string Location
    {
        get => _location;
        set
        {
            _location = value;

            OnPropertyChanged();
        }
    }


    //public CableRuns _selectedCableRun { get; set; }

    //public CableRuns SelectedCableRun
    //{
    //    get => _selectedCableRun;
    //    set
    //    {
    //        _selectedCableRun = value;

    //        OnPropertyChanged("SelectedCableRun");
    //    }
    //}

    public ImageSource _gpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");

    public ImageSource GpsImage
    {
        get => _gpsImage;
        set
        {
            _gpsImage = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand RefreshCommand => new(async () =>
    {
        SelectedBlockage = NavigationalParameters.BlockageItem;

        if (SelectedBlockage.StartTime.HasValue) StartTime = SelectedBlockage.StartTime.Value.TimeOfDay;

        if (SelectedBlockage.EndTime.HasValue) EndTime = SelectedBlockage.EndTime.Value.TimeOfDay;

        ReferenceA = SelectedBlockage?.PointARef;

        ReferenceB = SelectedBlockage?.PointBRef;

        LengthA = SelectedBlockage?.LengthFromCab;

        LengthB = SelectedBlockage?.LengthFromToby;

        Location = SelectedBlockage?.LocationGps;

        Comments = SelectedBlockage?.Comments;

        if (SelectedBlockage?.RemoteTableId > 0)
        {
            ShowSection3 = true;

            CanWrite = true;

            ShowSection4 = SelectedBlockage.Cleared == true;
        }
        else
        {
            ShowSection3 = false;

            CanWrite = false;

            ShowSection4 = true;
        }

        if (NavigationalParameters.BlockageItem.Cleared == true)
        {
            BlockageButtonText = "Mark as Blocked";
            BlockageButtonColour = Color.Red;
        }
        else
        {
            BlockageButtonText = "Mark blockage as cleared";
            BlockageButtonColour = Color.Green;
        }
    });

    public RelayCommand MarkAsClearedCommand => new(async () =>
    {
        ShowClearance = !ShowClearance;

        if (ShowClearance)
        {
            BlockageButtonText = "Mark as Blocked";
            BlockageButtonColour = Color.Red;
            SelectedBlockage.Cleared = true;
            ShowSection4 = true;
        }
        else
        {
            BlockageButtonText = "Mark blockage as cleared";
            BlockageButtonColour = Color.Green;
            SelectedBlockage.Cleared = false;
            ShowSection4 = false;
        }
    });

    public RelayCommand SaveBlockage => new(async () =>
    {
        IsLoading = true;
        ShowSection4 = false;

        var isValid = true;
        if (SelectedBlockage.Reason != "")
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedBlockage.LengthFromCab))
                    Convert.ToDecimal(SelectedBlockage.LengthFromCab);
            }
            catch
            {
                isValid = false;
                await Alert("please enter a decimal value for length from cab/chamber!", "Incorrect Format!");
            }

            try
            {
                if (!string.IsNullOrEmpty(SelectedBlockage.LengthFromToby))
                    Convert.ToDecimal(SelectedBlockage.LengthFromToby);
            }
            catch
            {
                isValid = false;
                await Alert("please enter a decimal value for length from toby!", "Incorrect Format!");
            }

            SelectedBlockage.PointARef = ReferenceA;
            SelectedBlockage.PointBRef = ReferenceB;
            SelectedBlockage.LengthFromCab = LengthA;
            SelectedBlockage.LengthFromToby = LengthB;
            SelectedBlockage.Comments = Comments;
            SelectedBlockage.LocationGps = Location;

            if (SelectedBlockage.RemoteTableId > 0)
            {
                SelectedBlockage.StartTime = DateTime.Now.Date + StartTime;
                SelectedBlockage.EndTime = DateTime.Now.Date + EndTime;
                SelectedBlockage.ClearedBy = NavigationalParameters.LoggedInUser.FocusId;


                isValid = (bool)SelectedBlockage.Cleared;
            }

            if (isValid)
            {
                //var uploadComplete = await _jobService.UploadCableRun(NavigationalParameters.SelectedAsset);

                //if (uploadComplete)
                //{
                //    NavigationalParameters.SelectedCableRun.SavedToServer = true;
                //    NavigationalParameters.SelectedCableRun.BackgroungColour = Color.Green;
                //}
                //else
                //{
                //    await Alert("Error", "Error upldating cable run ");
                //}
                NavigationalParameters.SelectedAsset.Blocked = false;
                await _jobService.AddEndPoint(NavigationalParameters.SelectedAsset);

                NavigationalParameters.BlockageItem = SelectedBlockage;

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);

               // await _jobService.UploadBlockageAsync(NavigationalParameters.BlockageItem);

                //NavigationalParameters.BlockageItem = null;

                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);

                // SelectedCableRun = null;
                IsLoading = false;
            }
            else
            {
                NavigationalParameters.SelectedAsset.Blocked = true;
                await _jobService.AddEndPoint(NavigationalParameters.SelectedAsset);

                await Alert("Invalid entry please try again or contact support!", "Error!");
                ShowSection4 = true;
                IsLoading = false;
            }
        }
        else
        {
            await Alert("All selections for the blockage must be selected!", "Error!");
            IsLoading = false;
            ShowSection4 = true;
        }
    });

    public RelayCommand PictureCommand => new(async () =>
    {
        try
        {
            var location =
                await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best,
                    new TimeSpan(0, 0, 5)));

            if (location != null)
                NavigationalParameters.NewPosition = new Position(location.Latitude, location.Longitude);
            SelectedBlockage.StartTime = DateTime.Now.Date + StartTime;
            SelectedBlockage.EndTime = DateTime.Now.Date + EndTime;
            SelectedBlockage.PointARef = ReferenceA;
            SelectedBlockage.PointBRef = ReferenceB;
            SelectedBlockage.LengthFromCab = LengthA;
            SelectedBlockage.LengthFromToby = LengthB;
            SelectedBlockage.Comments = Comments;
            SelectedBlockage.LocationGps = Location;
            NavigationalParameters.BlockageItem = SelectedBlockage;

            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.BLOCKAGE;

            await NavigateTo(ViewModelLocator.PhotoSelectionPage);
        }
        catch (Exception ex)
        {
        }
    });

    public RelayCommand GpsCommand => new(async () =>
    {
        SelectedBlockage.StartTime = DateTime.Now.Date + StartTime;
        SelectedBlockage.EndTime = DateTime.Now.Date + EndTime;
        SelectedBlockage.PointARef = ReferenceA;
        SelectedBlockage.PointBRef = ReferenceB;
        SelectedBlockage.LengthFromCab = LengthA;
        SelectedBlockage.LengthFromToby = LengthB;
        SelectedBlockage.Comments = Comments;
        SelectedBlockage.LocationGps = Location;
        NavigationalParameters.BlockageItem = SelectedBlockage;

        NavigationalParameters.MapType = "BlockageGps";

        //await NavigateTo(ViewModelLocator.FormsMapPage);
        await NavigateTo(ViewModelLocator.FormsMapPage);
    });

    public RelayCommand GpsPointACommand => new(async () =>
    {
        SelectedBlockage.StartTime = DateTime.Now.Date + StartTime;
        SelectedBlockage.EndTime = DateTime.Now.Date + EndTime;
        SelectedBlockage.PointARef = ReferenceA;
        SelectedBlockage.PointBRef = ReferenceB;
        SelectedBlockage.LengthFromCab = LengthA;
        SelectedBlockage.LengthFromToby = LengthB;
        NavigationalParameters.BlockageItem = SelectedBlockage;

        NavigationalParameters.MapType = "BlockageGpsA";

        //await NavigateTo(ViewModelLocator.FormsMapPage);
        await NavigateTo(ViewModelLocator.FormsMapPage);
    });

    public RelayCommand GpsPointBCommand => new(async () =>
    {
        SelectedBlockage.StartTime = DateTime.Now.Date + StartTime;
        SelectedBlockage.EndTime = DateTime.Now.Date + EndTime;
        SelectedBlockage.PointARef = ReferenceA;
        SelectedBlockage.PointBRef = ReferenceB;
        SelectedBlockage.LengthFromCab = LengthA;
        SelectedBlockage.LengthFromToby = LengthB;
        SelectedBlockage.Comments = Comments;
        SelectedBlockage.LocationGps = Location;
        NavigationalParameters.BlockageItem = SelectedBlockage;

        NavigationalParameters.MapType = "BlockageGpsB";

        //await NavigateTo(ViewModelLocator.FormsMapPage);
        await NavigateTo(ViewModelLocator.FormsMapPage);
    });

    public RelayCommand BackCommand => new(async () => { await NavigateTo(ViewModelLocator.BlockageListPage); });
}