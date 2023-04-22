namespace FocusXamarinMobileApplication.ViewModels;

public class DailyDiaryPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Assignments _assignmentService;
    private readonly Jobs _jobService;
    public readonly PhotoSelectionPageViewModel _psvm;
    public readonly Users _userService;

    //public Tuple<Person, List<Models.Assignment>, JobData4Tablet, string, ClaimedNotesFile>
    //    NavPassedInputDiaries;

    private RelayCommand<string> _screenLoaded4InputDiaries;
    private RelayCommand<string> _screenLoaded4ReturnInputDiaries;


    [Time]
    public DailyDiaryPageViewModel()
    {
        _assignmentService = new Assignments();
        _userService = new Users();
        _jobService = new Jobs();
        CameraImage = SimpleStaticHelpers.GetImage("If_camera_W");
        _psvm = new PhotoSelectionPageViewModel();
    }

    public bool _showPickers { get; set; }

    public bool ShowPickers
    {
        get => _showPickers;
        set
        {
            _showPickers = value;
            OnPropertyChanged();
        }
    }

    public bool _showDiaryText { get; set; } = true;

    public bool ShowDiaryText
    {
        get => _showDiaryText;
        set
        {
            _showDiaryText = value;
            OnPropertyChanged();
        }
    }

    public bool _photoButtonVisible { get; set; } = true;

    public bool PhotoButtonVisible
    {
        get => _photoButtonVisible;
        set
        {
            _photoButtonVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _showWeatherButton { get; set; } = true;

    public bool ShowWeatherButton
    {
        get => _showWeatherButton;
        set
        {
            _showWeatherButton = value;
            OnPropertyChanged();
        }
    }

    public bool _showExampleText { get; set; } = true;

    public bool ShowExampleText
    {
        get => _showExampleText;
        set
        {
            _showExampleText = value;
            OnPropertyChanged();
        }
    }

    public bool _startAddressesEditable { get; set; } = true;

    public bool StartAddressesEditable
    {
        get => _startAddressesEditable;
        set
        {
            _startAddressesEditable = value;
            OnPropertyChanged();
        }
    }

    public bool _showDiaryAddresses { get; set; } = true;

    public bool ShowDiaryAddresses
    {
        get => _showDiaryAddresses;
        set
        {
            _showDiaryAddresses = value;
            OnPropertyChanged();
        }
    }


    public bool _exampleTextEditiable { get; set; } = true;

    public bool ExampleTextEditiable
    {
        get => _exampleTextEditiable;
        set
        {
            _exampleTextEditiable = value;
            OnPropertyChanged();
        }
    }

    public bool _showAddressTxtBox { get; set; }

    public bool ShowAddressTxtBox
    {
        get => _showAddressTxtBox;
        set
        {
            _showAddressTxtBox = value;
            OnPropertyChanged();
        }
    }

    private List<VMexpansionReleaseData> _projectEndpoints { get; set; }

    public List<VMexpansionReleaseData> ProjectEndpoints
    {
        get => _projectEndpoints;
        set
        {
            _projectEndpoints = value;
            OnPropertyChanged("ClaimedNotesItem");
        }
    }
    // public List<VMexpansionReleaseData> Endpoints { get; set; }

    private ClaimedNotesFile _claimedNotesItem { get; set; }

    public ClaimedNotesFile ClaimedNotesItem
    {
        get => _claimedNotesItem;
        set
        {
            _claimedNotesItem = value;
            OnPropertyChanged();
        }
    }

    private string _diaryTitle { get; set; } = "";

    public string DiaryTitle
    {
        get => _diaryTitle;
        set
        {
            _diaryTitle = value;
            OnPropertyChanged();
        }
    }


    private string _submitButtonText { get; set; } = "Save";

    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
            OnPropertyChanged();
        }
    }

    private string _displayText { get; set; } = "";

    public string DisplayText
    {
        get => _displayText;
        set
        {
            _displayText = value;
            OnPropertyChanged();
        }
    }

    private string _exampleTextLabel { get; set; } = "";

    public string ExampleTextLabel
    {
        get => _exampleTextLabel;
        set
        {
            _exampleTextLabel = value;
            OnPropertyChanged();
        }
    }


    private string _endAddressText { get; set; } = "";

    public string EndAddressText
    {
        get => _endAddressText;
        set
        {
            _endAddressText = value;
            OnPropertyChanged();
        }
    }

    private string _startAddressText { get; set; } = "";

    public string StartAddressText
    {
        get => _startAddressText;
        set
        {
            _startAddressText = value;
            OnPropertyChanged();
        }
    }

    private string _exampleText { get; set; } = "";

    public string ExampleText
    {
        get => _exampleText;
        set
        {
            _exampleText = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<IGrouping<string, VMexpansionReleaseData>> _streetList { get; set; }

    public ObservableCollection<IGrouping<string, VMexpansionReleaseData>> StreetList
    {
        get => _streetList;
        set
        {
            _streetList = value;
            OnPropertyChanged();
        }
    }

    private Abbreviations _startAbbreviation { get; set; }

    public Abbreviations StartAbbreviation
    {
        get => _startAbbreviation;
        set
        {
            _startAbbreviation = value;
            OnPropertyChanged();
        }
    }

    private Abbreviations _endAbbreviation { get; set; }

    public Abbreviations EndAbbreviation
    {
        get => _endAbbreviation;
        set
        {
            _endAbbreviation = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _cameraImage { get; set; }

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged();
        }
    }

    private VMexpansionReleaseData _startAddress { get; set; }

    public VMexpansionReleaseData StartAddress
    {
        get => _startAddress;
        set
        {
            _startAddress = value;
            OnPropertyChanged();
        }
    }

    private VMexpansionReleaseData _endAddress { get; set; }

    public VMexpansionReleaseData EndAddress
    {
        get => _endAddress;
        set
        {
            _endAddress = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Abbreviations> Abbreviations { get; set; }

    private IGrouping<string, VMexpansionReleaseData> _startStreet { get; set; }

    public IGrouping<string, VMexpansionReleaseData> StartStreet
    {
        get => _startStreet;
        set
        {
            _startStreet = value;
            OnPropertyChanged();
        }
    }

    private IGrouping<string, VMexpansionReleaseData> _endStreet { get; set; }

    public IGrouping<string, VMexpansionReleaseData> EndStreet
    {
        get => _endStreet;
        set
        {
            _endStreet = value;
            OnPropertyChanged();
        }
    }

    //public RelayCommand<Abbreviations> SelectedStartAbbAddress { get; set; }
    //public RelayCommand<Abbreviations> SelectedEndAbbAddress { get; set; }
    //public RelayCommand<VMexpansionReleaseData> SelectedStartAddress { get; set; }
    //public RelayCommand<VMexpansionReleaseData> SelectedEndAddress { get; set; }

    public bool SupervisorDiaryEdit { get; set; }

    //public Tuple<Person, List<Models.Assignment>, JobData4Tablet, ClaimedNotesFile, string, string>
    //    NavPassedInputDiariesPhoto
    //{ get; set; }   

    public string ClaimedDiaryNotesText { get; set; } = "";
    public string ClaimedDelayNotesText { get; set; } = "";
    public string ClaimedAdditionalNotesText { get; set; } = "";
    public string SupervisorDiaryNotes { get; set; } = "";
    public string SupervisorDelayNotes { get; set; } = "";
    public string SupervisorAddWorksNotes { get; set; } = "";

    public string PayrollComments { get; set; } = "";

    public int StartAddressPosition { get; set; } = 0;
    public int EndAddressPosition { get; set; } = 0;
    public int StartAbbreviationPosition { get; set; } = 0;
    public int EndAbbreviationPosition { get; set; } = 0;
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

    public RelayCommand<string> ScreenLoaded4InputDiaries
    {
        get
        {
            return _screenLoaded4InputDiaries ??= new RelayCommand<string>(e =>
            {
                ExampleTextEditiable = true;

                ClaimedNotesItem = _jobService.GetClaimedNotes(NavigationalParameters.CurrentSelectedJob);
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
                ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";
                //ClaimedNotesItem = NavigationalParameters.ClaimedNotes ??= NavigationalParameters
                //    .CurrentSelectedJob?.JobGang
                //    .ClaimedNotes
                //    .FirstOrDefault(
                //        x => x.ContractReference ==
                //             NavigationalParameters.CurrentSelectedJob?.ContractReference &&
                //             x.NotesDate == NavigationalParameters.CurrentSelectedJob?.JobDate.Date &&
                //             x.NotesGang == NavigationalParameters.LoggedInUser?.FocusId.ToString());

                StartAddressText = ClaimedNotesItem?.StartAddress;

                EndAddressText = ClaimedNotesItem?.EndAddress;

                StartAddressesEditable = false;

                PhotoButtonVisible = true;

                ShowExampleText = true;

                SubmitButtonText = "Save";

                if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGDIARY)
                {
                    ShowWeatherButton = true;
                    DisplayText = ClaimedDiaryNotesText = ClaimedNotesItem?.NotesText;
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGWORKS)
                {
                    ShowWeatherButton = false;
                    DisplayText = ClaimedAdditionalNotesText =
                        ClaimedNotesItem?.AnyAddnlWorkReq;
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGDELAYS)
                {
                    ShowWeatherButton = false;
                    DisplayText = ClaimedDelayNotesText = ClaimedNotesItem?.AnyDelays;
                }

                Abbreviations = new ObservableCollection<Abbreviations>(_assignmentService.GetAbbreviations());

                NavigationalParameters.ProjectEndpoints = ListSort.OrderByNumber(_jobService
                    .GetEndpoints(NavigationalParameters.CurrentSelectedJob).ToList());

                StartAbbreviation = Abbreviations?.FirstOrDefault(x =>
                    x.Abb == ClaimedNotesItem?.StartAbbreviationId);
                EndAbbreviation = Abbreviations?.FirstOrDefault(x =>
                    x.Abb == ClaimedNotesItem?.EndAbbreviationId);

                StreetList =
                    new ObservableCollection<IGrouping<string, VMexpansionReleaseData>>(
                        NavigationalParameters.ProjectEndpoints.GroupBy(x => x.StreetName));

                StartAddress = StartStreet?.FirstOrDefault(x =>
                    x.RemoteTableId == ClaimedNotesItem?.StartEndpointId);
                EndAddress = EndStreet?.FirstOrDefault(x =>
                    x.RemoteTableId == ClaimedNotesItem?.EndEndpointId);

                ShowPickers = false; //NavigationalParameters.ProjectEndpoints.Any()
                //&& NavigationalParameters.ProjectEndpoints.All(x => x.Postcode != "NY Net" && !string.IsNullOrEmpty(x.BuildingNumber));
                ShowAddressTxtBox = !ShowPickers;
                ExampleTextLabel = "Example Text";

                if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGDIARY)
                {
                    DisplayText = ClaimedDiaryNotesText;
                    ShowDiaryAddresses = true;
                    DiaryTitle = "Diary";
                    ExampleText =
                        "This section is for your general daily diary. You will describe your day in detail, which would include your work activities, describing where any trial holes took place, or works of significance. This would include areas where you have been forced to go deep / shallow.";
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGDELAYS)
                {
                    DisplayText = ClaimedDelayNotesText;
                    ShowDiaryAddresses = false;
                    DiaryTitle = "Delays";
                    ExampleText =
                        "This section is for anything that has caused you a delay today. Some example entries would be: Major traffic on motorway, Faulty plant or machinery, Asphalt plant closed, Unexpected collections you have had to make Lack of support from grab, reinstators etc";
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGWORKS)
                {
                    DisplayText = ClaimedAdditionalNotesText;
                    ShowDiaryAddresses = false;
                    DiaryTitle = "Additional Works";
                    ExampleText =
                        "This section is for any works that you have completed outside of the normal. Some example entries would be: Hard dig between number 40 and 46 Stanley Street, Additional lateral installed outside 44";
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.PAYROL)
                {
                    ShowDiaryAddresses = true;
                    DiaryTitle = "Diary";
                    ExampleText =
                        "This section is specifically for comments about the teams, and if there is any variation or agreement in place. Some example entries could include: " +
                        "No plant charge today as rained off," +
                        "John Smith was not in today, marked as absent";
                }
            });
        }
    }

    public RelayCommand<string> ScreenLoaded4ReturnInputDiaries
    {
        get
        {
            return _screenLoaded4ReturnInputDiaries ??= new RelayCommand<string>(e =>
            {
                ExampleTextEditiable = true;

                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                    ClaimedNotesItem = _jobService.GetClaimedNotes(NavigationalParameters.SelectedTaskItem);
                else
                    ClaimedNotesItem = _jobService.GetClaimedNotes(NavigationalParameters.CurrentSelectedJob);

                ExampleTextLabel = "Example Text";
                PhotoButtonVisible = true;
                ShowWeatherButton = true;
                ShowExampleText = true;
                StartAddressesEditable = false;
                SubmitButtonText = "Save";
                if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGDIARY)
                {
                    DisplayText = ClaimedDiaryNotesText;
                    ShowDiaryAddresses = true;
                    DiaryTitle = "Diary";
                    ExampleText =
                        "This section is for your general daily diary. You will describe your day in detail, which would include your work activities, describing where any trial holes took place, or works of significance. This would include areas where you have been forced to go deep / shallow.";
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGDELAYS)
                {
                    DisplayText = ClaimedDelayNotesText;
                    ShowDiaryAddresses = false;
                    DiaryTitle = "Delays";
                    ExampleText =
                        "This section is for anything that has caused you a delay today. Some example entries would be: Major traffic on motorway, Faulty plant or machinery, Asphalt plant closed, Unexpected collections you have had to make Lack of support from grab, reinstators etc";
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.GANGWORKS)
                {
                    DisplayText = ClaimedAdditionalNotesText;
                    ShowDiaryAddresses = false;
                    DiaryTitle = "Additional Works";
                    ExampleText =
                        "This section is for any works that you have completed outside of the normal. Some example entries would be: Hard dig between number 40 and 46 Stanley Street, Additional lateral installed outside 44";
                }
                else if (NavigationalParameters.DiaryType == NavigationalParameters.DiaryTypes.PAYROL)
                {
                    ShowDiaryAddresses = true;
                    DiaryTitle = "Payroll";
                    ExampleText =
                        "This section is specifically for comments about the teams, and if there is any variation or agreement in place. Some example entries could include: " +
                        "No plant charge today as rained off," +
                        "John Smith was not in today, marked as absent";
                }

                NavigationalParameters.ProjectEndpoints = _jobService.GetEndpoints(
                    NavigationalParameters.CurrentSelectedJob).ToList();

                ShowPickers = NavigationalParameters.ProjectEndpoints.Any();
                ShowAddressTxtBox = !ShowPickers;
                //Abbreviations = Abbreviations ?? _assignmentService.GetAbbreviations();

                //StartAddress = StartAddress ?? Endpoints?.FirstOrDefault();
                //EndAddress = EndAddress ?? Endpoints?.FirstOrDefault();
                //StartAbbreviation = StartAbbreviation ?? Abbreviations?.FirstOrDefault();
                //EndAbbreviation = EndAbbreviation ?? Abbreviations?.FirstOrDefault();

                StartAddressText = ClaimedNotesItem?.StartAddress;

                EndAddressText = ClaimedNotesItem?.EndAddress;
            });
        }
    }

    public RelayCommand AddDiaryPictures => new(async () =>
    {
        SaveDiary();

        if (StartAddress == null && StartAddressText == null &&
            NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.STARTOFDAY)
        {
            await Alert(
                "Start address muss be entered before a picture can be taken",
                "Error!");
        }
        else if (EndAddress == null && EndAddressText == null &&
                 NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.ENDOFDAY)
        {
            await Alert(
                "End address muss be entered before a picture can be taken",
                "Error!");
        }
        else
        {
            if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.STARTOFDAY)
            {
                ClaimedNotesItem.StartAddress = StartAddressText;
                NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.STARTOFDAY;
            }
            else
            {
                NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.ENDOFDAY;
                ClaimedNotesItem.EndAddress = EndAddressText;
            }

            NavigationalParameters.ClaimedNotes = ClaimedNotesItem;

            _userService.UpdateInputDiariesFile(ClaimedNotesItem);

            _psvm.TakePhoto.Execute(null);
            //await NavigateTo(ViewModelLocator.PhotoSelectionPage);
        }
    });

    public RelayCommand ReturnToDiarySelection => new(async () =>
    {
        _userService.UpdateInputDiariesFile(ClaimedNotesItem);

        //if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
        //    await NavigateTo(ViewModelLocator.TeamOptionsPage);
        //else
        await NavigateTo(ViewModelLocator.DiarySelectionPage);
    });

    public RelayCommand DiarySubmit => new(async () =>
    {
        SaveDiary();

        await NavigateTo(ViewModelLocator.DiarySelectionPage);
    });

    public RelayCommand SetStartOfDayPicture => new(async () =>
    {
        NavigationalParameters.StartOfDayPictureTaken = _jobService.CheckStartOfDayPictureExsits("StartOfDay");
    });

    public RelayCommand<string> ScreenLoadedSupervisor => new(e =>
    {
        ExampleTextLabel = "Supervisor Comments";
        PhotoButtonVisible = false;
        ShowWeatherButton = false;
        ProjectName = NavigationalParameters.SelectedTaskItem?.ProjectName;
        ProjectDate = $"{NavigationalParameters.SelectedTaskItem?.JobDate:dd/MM/yyyy}";
        ExampleTextEditiable = false;
        ClaimedNotesItem = _jobService.GetClaimedNotes(NavigationalParameters.SelectedTaskItem);
        StartAddressText = ClaimedNotesItem?.StartAddress;
        EndAddressText = ClaimedNotesItem?.EndAddress;
        StartAddressesEditable = true;

        switch (NavigationalParameters.DiaryType)
        {
            case NavigationalParameters.DiaryTypes.DIARYAPPROVALS:
                ShowPickers = false;
                ShowDiaryText = true;
                ShowAddressTxtBox = true;
                DiaryTitle = "Diary Approval";
                PhotoButtonVisible = false;
                ShowExampleText = true;
                ShowDiaryAddresses = true;
                DisplayText = ClaimedDiaryNotesText = ClaimedNotesItem.NotesText;
                ExampleText = SupervisorDiaryNotes = ClaimedNotesItem.SupervisorText;
                StartAddressesEditable = true;
                break;
            case NavigationalParameters.DiaryTypes.WORKSAPPROVALS:
                ShowPickers = false;
                ShowAddressTxtBox = false;
                PhotoButtonVisible = false;
                ShowExampleText = true;
                ShowDiaryAddresses = false;
                ShowDiaryText = true;
                DisplayText = ClaimedAdditionalNotesText = ClaimedNotesItem.AnyAddnlWorkReq;
                ExampleText = SupervisorAddWorksNotes = ClaimedNotesItem.SupervisorAddnlWork;
                DiaryTitle = "Additional Works Approval";
                StartAddressesEditable = true;
                break;
            case NavigationalParameters.DiaryTypes.PAYROL:
                ShowPickers = false;
                ShowAddressTxtBox = false;
                ShowExampleText = true;
                ShowDiaryAddresses = false;
                DisplayText = PayrollComments = ClaimedNotesItem.PayrollComment;
                ExampleText = PayrollComments = ClaimedNotesItem.PayrollComment;
                DiaryTitle = "Payroll";
                StartAddressesEditable = false;
                ShowDiaryText = false;
                break;
            default:
                ShowPickers = false;
                ShowAddressTxtBox = false;
                ShowDiaryText = true;
                ShowExampleText = true;
                DisplayText = ClaimedDelayNotesText = ClaimedNotesItem.AnyDelays;
                ExampleText = SupervisorDelayNotes = ClaimedNotesItem.SupervisorDelays;
                DiaryTitle = "Delays Approval";
                ShowDiaryAddresses = false;
                StartAddressesEditable = true;
                break;
        }

        if (NavigationalParameters.SelectedTaskItem != null &&
            NavigationalParameters.SelectedTaskItem.GangLeaderId == 0)
        {
            //SupervisorDiaryEdit = true;
            ProjectDate = $"{DateTime.Now:dd/MM/yyyy}";
            //  SetSupervisorExampleText(e);
        }
        else
        {
            //SupervisorDiaryEdit = false;

            //NavigationalParameters.DiaryType = e;

            ProjectEndpoints =
                _jobService.GetEndpoints(NavigationalParameters.SelectedTaskItem).ToList();
            //Abbreviations = Abbreviations ?? _assignmentService.GetAbbreviations();

            StartAddress ??= ProjectEndpoints?.FirstOrDefault();

            EndAddress ??= ProjectEndpoints?.FirstOrDefault();
            //StartAbbreviation = StartAbbreviation ?? Abbreviations?.FirstOrDefault();
            //EndAbbreviation = EndAbbreviation ?? Abbreviations?.FirstOrDefault();

            //PositionPickers();
        }

        //StartAddressesEditable = true;
    });

    public RelayCommand NavigateToWeatherEvents => new(async () =>
    {
        SaveDiary();

        await NavigateTo(ViewModelLocator.WeatherEventPage);
    });

    public RelayCommand<Abbreviations> SelectedStartAbbreviation => new(e => { StartAbbreviation = e; });

    public RelayCommand<VMexpansionReleaseData> SelectedStartAddress => new(e => { StartAddress = e; });

    public RelayCommand<IGrouping<string, VMexpansionReleaseData>> SelectedStartStreet =>
        new(e => { StartStreet = e; });

    public RelayCommand<Abbreviations> SelectedEndAbbreviation => new(e => { EndAbbreviation = e; });

    public RelayCommand<IGrouping<string, VMexpansionReleaseData>> SelectedEndStreet => new(e => { EndStreet = e; });

    public RelayCommand<VMexpansionReleaseData> SelectedEndAddress => new(e => { EndAddress = e; });

    public RelayCommand Cancel => new(async () => { NavigateBack(); });

    [Time]
    public void SaveDiary()
    {
        switch (NavigationalParameters.DiaryType)
        {
            case NavigationalParameters.DiaryTypes.GANGDIARY:
                ClaimedNotesItem.NotesText = ClaimedDiaryNotesText = DisplayText;
                break;
            case NavigationalParameters.DiaryTypes.GANGWORKS:
                ClaimedNotesItem.AnyAddnlWorkReq = ClaimedAdditionalNotesText = DisplayText;
                break;
            case NavigationalParameters.DiaryTypes.GANGDELAYS:
                ClaimedNotesItem.AnyDelays = ClaimedDelayNotesText = DisplayText;
                break;
            case NavigationalParameters.DiaryTypes.DIARYAPPROVALS:
                ClaimedNotesItem.SupervisorText = SupervisorDiaryNotes = ExampleText;
                break;
            case NavigationalParameters.DiaryTypes.WORKSAPPROVALS:
                ClaimedNotesItem.SupervisorAddnlWork = SupervisorAddWorksNotes = ExampleText;
                break;
            case NavigationalParameters.DiaryTypes.DELAYSAPPROVALS:
                ClaimedNotesItem.SupervisorDelays = SupervisorDelayNotes = ExampleText;
                break;
            case NavigationalParameters.DiaryTypes.PAYROL:
                ClaimedNotesItem.PayrollComment = PayrollComments = ExampleText;
                break;
        }

        ClaimedNotesItem.StartAddress = string.IsNullOrEmpty(StartAddressText)
            ? $"{StartAbbreviation?.FullName} {StartAddress?.BuildingNumber} {StartAddress?.StreetName} {StartAddress?.Postcode}"
            : StartAddressText;

        ClaimedNotesItem.EndAddress = string.IsNullOrEmpty(EndAddressText)
            ? $"{EndAbbreviation?.FullName} {EndAddress?.BuildingNumber} {EndAddress?.StreetName} {EndAddress?.Postcode}"
            : EndAddressText;

        ClaimedNotesItem.StartAbbreviationId = StartAbbreviation?.Abb;
        ClaimedNotesItem.EndAbbreviationId = EndAbbreviation?.Abb;

        ClaimedNotesItem.StartEndpointId =
            StartAddress == null ? 0 : (int)StartAddress.RemoteTableId;
        ClaimedNotesItem.EndEndpointId =
            EndAddress == null ? 0 : (int)EndAddress.RemoteTableId;

        _userService.UpdateInputDiariesFile(ClaimedNotesItem);
    }
}