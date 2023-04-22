#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SiteInspectionRatingFailurePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentsService;
    private Jobs _jobService;
    private Users _userService;

    public SiteInspectionRatingFailurePageViewModel()
    {
        _userService = new Users();
        _jobService = new Jobs();
        _assignmentsService = new Assignments();
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

    private ImageSource _rating6 { get; set; } = SimpleStaticHelpers.GetImage("6orbelowgrey");

    public ImageSource Rating6
    {
        get => _rating6;
        set
        {
            _rating6 = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _rating5 { get; set; } = SimpleStaticHelpers.GetImage("5grey");

    public ImageSource Rating5
    {
        get => _rating5;
        set
        {
            _rating5 = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _rating4 { get; set; } = SimpleStaticHelpers.GetImage("4grey");

    public ImageSource Rating4
    {
        get => _rating4;
        set
        {
            _rating4 = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _rating3 { get; set; } = SimpleStaticHelpers.GetImage("3grey");

    public ImageSource Rating3
    {
        get => _rating3;
        set
        {
            _rating3 = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _rating2 { get; set; } = SimpleStaticHelpers.GetImage("2grey");

    public ImageSource Rating2
    {
        get => _rating2;
        set
        {
            _rating2 = value;
            OnPropertyChanged();
        }
    }

    private ImageSource _rating1 { get; set; } = SimpleStaticHelpers.GetImage("1grey");

    public ImageSource Rating1
    {
        get => _rating1;
        set
        {
            _rating1 = value;
            OnPropertyChanged();
        }
    }

    public string _rootCause { get; set; }

    public string RootCause
    {
        get => _rootCause;
        set
        {
            _rootCause = value;
            OnPropertyChanged();
        }
    }

    public string _intermediateActionDetails { get; set; }

    public string IntermediateActionDetails
    {
        get => _intermediateActionDetails;
        set
        {
            _intermediateActionDetails = value;
            OnPropertyChanged();
        }
    }

    public string _correctiveActionDetails { get; set; }

    public string CorrectiveActionDetails
    {
        get => _correctiveActionDetails;
        set
        {
            _correctiveActionDetails = value;
            OnPropertyChanged();
        }
    }

    public Color _correctedOnSiteColour { get; set; } = Color.LightGray;

    public Color CorrectedOnSiteColour
    {
        get => _correctedOnSiteColour;
        set
        {
            _correctedOnSiteColour = value;
            OnPropertyChanged();
        }
    }

    public Color _correctedOffSiteColour { get; set; } = Color.LightGray;

    public Color CorrectedOffSiteColour
    {
        get => _correctedOffSiteColour;
        set
        {
            _correctedOffSiteColour = value;
            OnPropertyChanged();
        }
    }


    public bool _correctedOnstie { get; set; }

    public bool CorrectedOnstie
    {
        get => _correctedOnstie;
        set
        {
            _correctedOnstie = value;
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


    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;

        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");

        Title = "Audit failure";

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        ShowSection6 = true;

        CorrectedOnSiteColour = Color.LightGray;
        CorrectedOffSiteColour = Color.LightGray;

        RootCause = NavigationalParameters.CurrentNCR?.RootCause;
        IntermediateActionDetails = NavigationalParameters.CurrentNCR?.IntermediateActions;
        CorrectiveActionDetails = NavigationalParameters.CurrentNCR?.CorrectiveActions;
        CorrectedOnstie = (bool)NavigationalParameters.CurrentNCR?.CorrectedOnSite;

        if ((bool)NavigationalParameters.CurrentNCR?.CorrectedOnSite)
        {
            CorrectedOnSiteColour = Color.Green;
            CorrectedOffSiteColour = Color.LightGray;
        }
        else
        {
            CorrectedOnSiteColour = Color.LightGray;
            CorrectedOffSiteColour = Color.Green;
        }

        NavigationalParameters.SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime).FirstOrDefault(x =>
             x.QuestionId == NavigationalParameters.SelectedQuestion?.QuestionId
             && x.Category?.ToLower() == NavigationalParameters.SelectedQuestion?.Category.ToLower()
             && x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

        if (NavigationalParameters.SelectedAnswer.AnswerGiven == "1")
        {
            Rating1 = SimpleStaticHelpers.GetImage("1red");
            Rating2 = SimpleStaticHelpers.GetImage("2grey");
            Rating3 = SimpleStaticHelpers.GetImage("3grey");
            Rating4 = SimpleStaticHelpers.GetImage("4grey");
            Rating5 = SimpleStaticHelpers.GetImage("5grey");
            Rating6 = SimpleStaticHelpers.GetImage("6grey");
        }
        else if (NavigationalParameters.SelectedAnswer.AnswerGiven == "2")
        {
            Rating1 = SimpleStaticHelpers.GetImage("1grey");
            Rating2 = SimpleStaticHelpers.GetImage("2red");
            Rating3 = SimpleStaticHelpers.GetImage("3grey");
            Rating4 = SimpleStaticHelpers.GetImage("4grey");
            Rating5 = SimpleStaticHelpers.GetImage("5grey");
            Rating6 = SimpleStaticHelpers.GetImage("6rey");
        }
        else if (NavigationalParameters.SelectedAnswer.AnswerGiven == "3")
        {
            Rating1 = SimpleStaticHelpers.GetImage("1grey");
            Rating2 = SimpleStaticHelpers.GetImage("2grey");
            Rating3 = SimpleStaticHelpers.GetImage("3orange");
            Rating4 = SimpleStaticHelpers.GetImage("4grey");
            Rating5 = SimpleStaticHelpers.GetImage("5grey");
            Rating6 = SimpleStaticHelpers.GetImage("6grey");
        }
        else if (NavigationalParameters.SelectedAnswer.AnswerGiven == "4")
        {
            Rating1 = SimpleStaticHelpers.GetImage("1grey");
            Rating2 = SimpleStaticHelpers.GetImage("2grey");
            Rating3 = SimpleStaticHelpers.GetImage("3grey");
            Rating4 = SimpleStaticHelpers.GetImage("4orange");
            Rating5 = SimpleStaticHelpers.GetImage("5grey");
            Rating6 = SimpleStaticHelpers.GetImage("6grey");
        }
        else if (NavigationalParameters.SelectedAnswer.AnswerGiven == "5")
        {
            Rating1 = SimpleStaticHelpers.GetImage("1grey");
            Rating2 = SimpleStaticHelpers.GetImage("2grey");
            Rating3 = SimpleStaticHelpers.GetImage("3grey");
            Rating4 = SimpleStaticHelpers.GetImage("4grey");
            Rating5 = SimpleStaticHelpers.GetImage("5yellow");
            Rating6 = SimpleStaticHelpers.GetImage("6grey");
        }
        else if (NavigationalParameters.SelectedAnswer.AnswerGiven == "6")
        {
            Rating1 = SimpleStaticHelpers.GetImage("1grey");
            Rating2 = SimpleStaticHelpers.GetImage("2grey");
            Rating3 = SimpleStaticHelpers.GetImage("3grey");
            Rating4 = SimpleStaticHelpers.GetImage("4grey");
            Rating5 = SimpleStaticHelpers.GetImage("5grey");
            Rating6 = SimpleStaticHelpers.GetImage("6yellow");
        }
        else
        {
            Rating1 = SimpleStaticHelpers.GetImage("1grey");
            Rating2 = SimpleStaticHelpers.GetImage("2grey");
            Rating3 = SimpleStaticHelpers.GetImage("3grey");
            Rating4 = SimpleStaticHelpers.GetImage("4grey");
            Rating5 = SimpleStaticHelpers.GetImage("5grey");
            Rating6 = SimpleStaticHelpers.GetImage("6grey");
        }

        try
        {
            var location = await Geolocation.GetLocationAsync();

            if (location != null)
            {
                NavigationalParameters.NewPosition = new Position(location.Latitude, location.Longitude);

                var placemarks = await Geocoding.GetPlacemarksAsync(NavigationalParameters.NewPosition.Latitude,
                    NavigationalParameters.NewPosition.Longitude);

                if (placemarks != null)
                {
                    var currentPlacemark = placemarks?.FirstOrDefault();
                    NavigationalParameters.CurrentNCR.ComplainantStreet =
                        currentPlacemark?.FeatureName ?? "Unavailable";
                    NavigationalParameters.CurrentNCR.ComplainantTownCity =
                        currentPlacemark?.AdminArea ?? "Unavailable";
                    NavigationalParameters.CurrentNCR.ComplainantStreet =
                        currentPlacemark?.Thoroughfare ?? "Unavailable";
                    NavigationalParameters.CurrentNCR.ComplainantPostalcode =
                        currentPlacemark?.PostalCode ?? "Unavailable";
                }
            }
        }
        catch (Exception ex)
        {
            await Alert("Gps", "Failed to get current location, Location will default to base", "Ok");
        }
    });


    public RelayCommand CorrectedOnsite => new(async () =>
    {
        NavigationalParameters.CurrentNCR.CorrectedOnSite = CorrectedOnstie = true;

        CorrectedOnSiteColour = Color.Green;
        CorrectedOffSiteColour = Color.LightGray;
    });


    public RelayCommand CorrectedOffsite => new(async () =>
    {
        NavigationalParameters.CurrentNCR.CorrectedOnSite = CorrectedOnstie = false;

        CorrectedOnSiteColour = Color.LightGray;
        CorrectedOffSiteColour = Color.Green;
    });

    public RelayCommand Rating1Command => new(async () =>
    {
        Rating1 = SimpleStaticHelpers.GetImage("1red");
        Rating2 = SimpleStaticHelpers.GetImage("2grey");
        Rating3 = SimpleStaticHelpers.GetImage("3grey");
        Rating4 = SimpleStaticHelpers.GetImage("4grey");
        Rating5 = SimpleStaticHelpers.GetImage("5grey");
        Rating6 = SimpleStaticHelpers.GetImage("6grey");
        NavigationalParameters.CurrentNCR.Severity = "1";
        NavigationalParameters.SelectedAnswer.AnswerGiven = "1";
    });

    public RelayCommand Rating2Command => new(async () =>
    {
        Rating1 = SimpleStaticHelpers.GetImage("1grey");
        Rating2 = SimpleStaticHelpers.GetImage("2red");
        Rating3 = SimpleStaticHelpers.GetImage("3grey");
        Rating4 = SimpleStaticHelpers.GetImage("4grey");
        Rating5 = SimpleStaticHelpers.GetImage("5grey");
        Rating6 = SimpleStaticHelpers.GetImage("6grey");
        NavigationalParameters.CurrentNCR.Severity = "2";
        NavigationalParameters.SelectedAnswer.AnswerGiven = "2";
    });

    public RelayCommand Rating3Command => new(async () =>
    {
        Rating1 = SimpleStaticHelpers.GetImage("1grey");
        Rating2 = SimpleStaticHelpers.GetImage("2grey");
        Rating3 = SimpleStaticHelpers.GetImage("3orange");
        Rating4 = SimpleStaticHelpers.GetImage("4grey");
        Rating5 = SimpleStaticHelpers.GetImage("5grey");
        Rating6 = SimpleStaticHelpers.GetImage("6grey");
        NavigationalParameters.CurrentNCR.Severity = "3";
        NavigationalParameters.SelectedAnswer.AnswerGiven = "3";
    });

    public RelayCommand Rating4Command => new(async () =>
    {
        Rating1 = SimpleStaticHelpers.GetImage("1grey");
        Rating2 = SimpleStaticHelpers.GetImage("2grey");
        Rating3 = SimpleStaticHelpers.GetImage("3grey");
        Rating4 = SimpleStaticHelpers.GetImage("4orange");
        Rating5 = SimpleStaticHelpers.GetImage("5grey");
        Rating6 = SimpleStaticHelpers.GetImage("6grey");
        NavigationalParameters.CurrentNCR.Severity = "4";
        NavigationalParameters.SelectedAnswer.AnswerGiven = "4";
    });

    public RelayCommand Rating5Command => new(async () =>
    {
        Rating1 = SimpleStaticHelpers.GetImage("1grey");
        Rating2 = SimpleStaticHelpers.GetImage("2grey");
        Rating3 = SimpleStaticHelpers.GetImage("3grey");
        Rating4 = SimpleStaticHelpers.GetImage("4grey");
        Rating5 = SimpleStaticHelpers.GetImage("5yellow");
        Rating6 = SimpleStaticHelpers.GetImage("6grey");
        NavigationalParameters.CurrentNCR.Severity = "5";
        NavigationalParameters.SelectedAnswer.AnswerGiven = "5";
    });

    public RelayCommand Rating6Command => new(async () =>
    {
        Rating1 = SimpleStaticHelpers.GetImage("1grey");
        Rating2 = SimpleStaticHelpers.GetImage("2grey");
        Rating3 = SimpleStaticHelpers.GetImage("3grey");
        Rating4 = SimpleStaticHelpers.GetImage("4grey");
        Rating5 = SimpleStaticHelpers.GetImage("5grey");
        Rating6 = SimpleStaticHelpers.GetImage("6yellow");
        NavigationalParameters.CurrentNCR.Severity = "6";
        NavigationalParameters.SelectedAnswer.AnswerGiven = "6";
    });

    public RelayCommand Submit => new(async () =>
    {
        try
        {
            var supervisor = App.Database.GetItems<Person>()
           ?.FirstOrDefault(x => x.FocusId == NavigationalParameters.CurrentSelectedJob?.SupervisorId);

            NavigationalParameters.CurrentNCR.CorrectiveActions = CorrectiveActionDetails;
            NavigationalParameters.CurrentNCR.Category = "Audit";
            NavigationalParameters.CurrentNCR.AuditId = NavigationalParameters.CurrentAudit.AuditId;
            NavigationalParameters.CurrentNCR.RootCause = RootCause;
            NavigationalParameters.CurrentNCR.IntermediateActions = IntermediateActionDetails;
            NavigationalParameters.CurrentNCR.SupervisorId = NavigationalParameters.CurrentSelectedJob.SupervisorId;
            NavigationalParameters.CurrentNCR.CorrectedOnSite = CorrectedOnstie;
            NavigationalParameters.CurrentNCR.ComplainantName = $"{supervisor?.FirstName} {supervisor?.Surname}";
            NavigationalParameters.SelectedAnswer.AnswerGiven = NavigationalParameters.CurrentNCR.Severity;

            await _assignmentsService.SaveNcr(NavigationalParameters.CurrentNCR);

            await _assignmentsService.UpdateAnswer(NavigationalParameters.SelectedAnswer);

            // NavigationalParameters.CurrentAudit.NcrList.Add(NavigationalParameters.CurrentNCR);

            NavigationalParameters.CurrentNCR = null;

            if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.DEFAULT)
                NavigateBack();
            else
                await NavigateTo(ViewModelLocator.RatingSurveyQuestionsPage);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
       
    });

    public RelayCommand DetailsCommand => new(async () =>
    {
        NavigationalParameters.CurrentNCR.CorrectiveActions = CorrectiveActionDetails;
        NavigationalParameters.CurrentNCR.RootCause = RootCause;
        NavigationalParameters.CurrentNCR.IntermediateActions = IntermediateActionDetails;
        NavigationalParameters.CurrentNCR.QuestionId = NavigationalParameters.SelectedAnswer.QuestionId;
        NavigationalParameters.CurrentNCR.SupervisorId = NavigationalParameters.CurrentSelectedJob.SupervisorId;
        NavigationalParameters.CurrentNCR.CorrectedOnSite = CorrectedOnstie;
        NavigationalParameters.SelectedAnswer.AnswerGiven = NavigationalParameters.CurrentNCR.Severity;
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.NCRAUDITDETAILS;
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand IntermediateActionCommand => new(async () =>
    {
        NavigationalParameters.CurrentNCR.CorrectiveActions = CorrectiveActionDetails;
        NavigationalParameters.CurrentNCR.RootCause = RootCause;
        NavigationalParameters.CurrentNCR.IntermediateActions = IntermediateActionDetails;
        NavigationalParameters.CurrentNCR.QuestionId = NavigationalParameters.SelectedAnswer.QuestionId;
        NavigationalParameters.CurrentNCR.SupervisorId = NavigationalParameters.CurrentSelectedJob.SupervisorId;
        NavigationalParameters.CurrentNCR.CorrectedOnSite = CorrectedOnstie;
        NavigationalParameters.SelectedAnswer.AnswerGiven = NavigationalParameters.CurrentNCR.Severity;
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.NCRINTERMEDIATEACTIONS;
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand CorrectiveActionCommand => new(async () =>
    {
        NavigationalParameters.CurrentNCR.CorrectiveActions = CorrectiveActionDetails;
        NavigationalParameters.CurrentNCR.RootCause = RootCause;
        NavigationalParameters.CurrentNCR.IntermediateActions = IntermediateActionDetails;
        NavigationalParameters.CurrentNCR.QuestionId = NavigationalParameters.SelectedAnswer.QuestionId;
        NavigationalParameters.CurrentNCR.SupervisorId = NavigationalParameters.CurrentSelectedJob.SupervisorId;
        NavigationalParameters.CurrentNCR.CorrectedOnSite = CorrectedOnstie;
        NavigationalParameters.SelectedAnswer.AnswerGiven = NavigationalParameters.CurrentNCR.Severity;
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.NCRCORRECTIVEACTIONS;
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand Back => new(async () =>
    {
        await _assignmentsService.DeleteCurrentNcr(NavigationalParameters.CurrentNCR);
        await NavigateTo(ViewModelLocator.RatingSurveyQuestionsPage);
    });
}