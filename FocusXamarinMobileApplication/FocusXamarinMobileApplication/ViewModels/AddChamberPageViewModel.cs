namespace FocusXamarinMobileApplication.ViewModels;

public class AddChamberPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public AddChamberPageViewModel()
    {
        _assigmentService = new Assignments();

        _jobService = new Jobs();

        _userService = new Users();
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

    public List<string> _poleTypes { get; set; }

    public List<string> PoleTypes
    {
        get => _poleTypes;
        set
        {
            _poleTypes = value;
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

    public string _selectedPoleType { get; set; }

    public string SelectedPoleType
    {
        get => _selectedPoleType;
        set
        {
            _selectedPoleType = value;

            switch (SelectedPoleType)
            {
                case "11 Light Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "17000";
                    break;
                case "10 Light Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "18000";
                    break;
                case "9 Light Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "15500";
                    break;
                case "11 Medium Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "17000";
                    break;
                case "10 Medium Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "18000";
                    break;
                case "9 Medium Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "15500";
                    break;
                case "11 Stout Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "17000";
                    break;
                case "10 Stout Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "18000";
                    break;
                case "9 Stout Good Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "15500";
                    break;
                case "11 Light Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "20000";
                    break;
                case "10 Light Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "21000";
                    break;
                case "9 Light Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "21500";
                    break;
                case "11 Medium Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "20000";
                    break;
                case "10 Medium Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "21000";
                    break;
                case "9 Medium Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "21500";
                    break;
                case "11 Stout Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "20000";
                    break;
                case "10 Stout Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "21000";
                    break;
                case "9 Stout Bad Conditions":
                    Length = "450";
                    Width = "450";
                    Depth = "21500";
                    break;
            }

            OnPropertyChanged();
        }
    }

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

    public VMl4CabDetail _cabDetail { get; set; }

    public VMl4CabDetail CabDetail
    {
        get => _cabDetail;
        set
        {
            _cabDetail = value;

            OnPropertyChanged();
        }
    }

    public VMexpansionReleaseData _chamber { get; set; }

    public VMexpansionReleaseData Chamber
    {
        get => _chamber;
        set
        {
            _chamber = value;

            OnPropertyChanged();
        }
    }

    public string _length { get; set; } = "0";

    public string Length
    {
        get => _length;
        set
        {
            _length = value;

            OnPropertyChanged();
        }
    }

    public string _width { get; set; } = "0";

    public string Width
    {
        get => _width;
        set
        {
            _width = value;

            OnPropertyChanged();
        }
    }

    public string _depth { get; set; } = "0";

    public string Depth
    {
        get => _depth;
        set
        {
            _depth = value;

            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoad => new(async () =>
    {
        try
        {
            ShowSection1 = true;

            ShowSection2 = true;

            ShowSection3 = true;

            ShowSection4 = true;

            Chamber = NavigationalParameters.SelectedEndPoint;

            CabDetail = NavigationalParameters.SelectedCabinet;

            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

            IsLoading = false;

            if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY)
            {
                Title = "Add Pole";
                Chamber.TobyLength = 450;
                Length = "450";
                Chamber.TobyWidth = 450;
                Width = "450";

                PoleTypes = new List<string>
                {
                    "11 Light Good Conditions",
                    "10 Light Good Conditions",
                    "9 Light Good Conditions",
                    "11 Medium Good Conditions",
                    "10 Medium Good Conditions",
                    "9 Medium Good Conditions",
                    "11 Stout Good Conditions",
                    "10 Stout Good Conditions",
                    "9 Stout Good Conditions",
                    "11 Light Bad Conditions",
                    "10 Light Bad Conditions",
                    "9 Light Bad Conditions",
                    "11 Medium Bad Conditions",
                    "10 Medium Bad Conditions",
                    "9 Medium Bad Conditions",
                    "11 Stout Bad Conditions",
                    "10 Stout Bad Conditions",
                    "9 Stout Bad Conditions"
                };
            }
            else
            {
                Title = "Add Chamber";

                PoleTypes = new List<string>
                {
                    "Chamber Type A", "Chamber Type B", "Chamber Type C"
                };
            }
        }
        catch (Exception ex)
        {
            await Alert("Error",
                "There has been an error loading the page! please try again or contact support if this issue persists",
                "Ok");
        }
    });

    public RelayCommand PictureCommand => new(async () =>
    {
        try
        {
            NavigationalParameters.SelectedPhoto = new Pictures4Tablet();
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.CHAMBERAUDIT;
            Chamber.TobyLength = Convert.ToDecimal(Length);
            Chamber.TobyWidth = Convert.ToDecimal(Width);
            Chamber.Depth = Convert.ToDecimal(Width);
            NavigationalParameters.SelectedEndPoint = Chamber;
            NavigationalParameters.SelectedCabinet = CabDetail;

            await NavigateTo(ViewModelLocator.ProjectImagesPage);
        }
        catch (Exception ex)
        {
            await Alert("Error",
                "There has been an error saving the picture! please try again or contact support if this issue persists",
                "Ok");
        }
    });

    public RelayCommand GpsCommand => new(async () =>
    {
        Chamber.TobyLength = Convert.ToDecimal(Length);
        Chamber.TobyWidth = Convert.ToDecimal(Width);
        Chamber.Depth = Convert.ToDecimal(Width);
        NavigationalParameters.SelectedEndPoint = Chamber;
        NavigationalParameters.SelectedCabinet = CabDetail;

        if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY)
            NavigationalParameters.MapType = "PRESITEPOLESURVEY";
        else
            NavigationalParameters.MapType = "ChamberAudit";


        await NavigateTo(ViewModelLocator.FormsMapPage);
    });

    public RelayCommand SaveCommand => new(async () =>
    {
        try
        {
            var chambers = _jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob);

            if (chambers != null && chambers.Any(x => x.StreetName.ToLower() == Chamber.StreetName.ToLower()
                                                      && x.BuildingStandard.ToLower() ==
                                                      Chamber.BuildingStandard.ToLower() &&
                                                      x.Qnumber == Chamber.Qnumber))
            {
                await Alert("Duplication",
                    "ALl names must be unique per job please provide another name and try again!");
            }
            else
            {
                //Chamber = chambers.FirstOrDefault(x => x.StreetName.ToLower() == Chamber.StreetName.ToLower()
                //                                           && x.BuildingStandard.ToLower() == Chamber.BuildingStandard.ToLower());

                Chamber.TobyLength = Convert.ToDecimal(Length);

                Chamber.TobyWidth = Convert.ToDecimal(Width);

                Chamber.Depth = Convert.ToDecimal(Width);

                Chamber.SavedToServer = false;

                Chamber.DropLength = SelectedPoleType;

                if (string.IsNullOrEmpty(Chamber.ClaimId.ToString())) Chamber.ClaimId = Guid.NewGuid();

                if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY)
                    Chamber.BuildingStandard = "pole";
                else
                    Chamber.BuildingStandard = "chamber";


                NavigationalParameters.SelectedEndPoint = Chamber;

                NavigationalParameters.CurrentAssignment.StreetName = NavigationalParameters.SelectedEndPoint.StreetName;

                await _jobService.AddArea(NavigationalParameters.SelectedCabinet);

                await _jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);

                await _assigmentService.SaveCurrentAnswer(NavigationalParameters.SelectedAnswer);

                var comment = new SurveyComments
                {
                    AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                    Category = NavigationalParameters.CurrentAssignment.Category,
                    CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                    Text =
                        $"measurents are Length:{NavigationalParameters.SelectedEndPoint.TobyLength} x Width:{NavigationalParameters.SelectedEndPoint.TobyWidth} x Depth:{NavigationalParameters.SelectedEndPoint.Depth}",
                    Identifier = NavigationalParameters.SelectedAnswer.Identifier,
                    QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                    StreetName = NavigationalParameters.SelectedEndPoint.StreetName,
                    SubmittedDateTime = DateTime.Now,
                    QuestionId = 0
                };


                await _assigmentService.AddSurveyComments(comment);

                await _assigmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

                await NavigateTo(ViewModelLocator.SelectStreetPage);
                Length = "";
                Width = "";
                Depth = "";
                Chamber = null;
                CabDetail = null;
                NavigationalParameters.SelectedCabinet = null;
                NavigationalParameters.SelectedEndPoint = null;
                NavigationalParameters.CurrentAssignment = null;
            }
        }
        catch (Exception ex)
        {
            await Alert("Error",
                "There has been an error saving the chamber! please try again or contact support if this issue persists",
                "Ok");
        }
    });

    public RelayCommand DeleteCommand => new(async () =>
    {
        try
        {
            await NavigateTo(ViewModelLocator.SelectStreetPage);
            Length = "";
            Width = "";
            Depth = "";
            Chamber = null;
            CabDetail = null;
            NavigationalParameters.SelectedCabinet = null;
            NavigationalParameters.SelectedEndPoint = null;
            NavigationalParameters.CurrentAssignment = null;
        }
        catch (Exception ex)
        {
            var x = ex.ToString();
        }
    });
}