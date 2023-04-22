namespace FocusXamarinMobileApplication.ViewModels;

public class PermitPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;

    public PermitPageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        ShowSection6 = true;

        SigDetailArray = new List<AuthorisationDetail>
            { new(), new() };


        switch (NavigationalParameters.AppMode)
        {
            case NavigationalParameters.AppModes.SITECLEAR:


                if (NavigationalParameters.CurrentPermit.Granted)
                {
                    PermitColour = Color.Green;
                    PermitStatus = "Site cleared";
                }
                else
                {
                    PermitColour = Color.Red;
                    PermitStatus = "Site not cleared";
                }

                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                    NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                {
                    ShowSection3 = false;
                    SignatureAButtonText = "Sign as Operative";
                }

                break;
            default:

                SignatureAButtonText = "Sign as Operative";
                SignatureBButtonText = "Sign as Supervisor";

                if (NavigationalParameters.CurrentPermit.Granted)
                {
                    PermitColour = Color.Green;
                    PermitStatus = "Permit Granted";
                }
                else
                {
                    PermitColour = Color.Red;
                    PermitStatus = "Permit denied";
                }

                break;
        }


        var datea = SigDetailArray[0].Date == DateTime.MinValue ? "" : SigDetailArray[0].Date.ToShortDateString();

        SignatureADate = $"Date : {datea}";

        SignedByA = $"Signed by : {SigDetailArray[0].SignedBy}   {SignatureADate}";


        SignatureAimg = SigDetailArray[0].SignatureImg == null
            ? null
            : ImageSource.FromStream(() =>
                new MemoryStream(SigDetailArray[0]
                    .SignatureImg)); //UIImage.LoadFromData(NSData.FromArray(SigDetailArray[0].SignatureImg));

        var dateb = SigDetailArray[1].Date == DateTime.MinValue ? "" : SigDetailArray[1].Date.ToShortDateString();

        SignatureBDate = $"Date : {dateb}";

        SignedByB = $"Issued by : {SigDetailArray[1].SignedBy}   {SignatureBDate}";

        SignatureBimg = SigDetailArray[1].SignatureImg == null
            ? null
            : ImageSource.FromStream(() =>
                new MemoryStream(SigDetailArray[1]
                    .SignatureImg)); //UIImage.LoadFromData(NSData.FromArray(SigDetailArray[1].SignatureImg));


        IsEnabled = SigDetailArray[0].DetailsCorrect() && SigDetailArray[1].DetailsCorrect()
            ? true
            : false;
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

    public List<AuthorisationDetail> SigDetailArray { get; set; }

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

    public bool _isEnabled { get; set; }

    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            _isEnabled = value;
            OnPropertyChanged();
        }
    }

    public string _permitStatus { get; set; }

    public string PermitStatus
    {
        get => _permitStatus;
        set
        {
            _permitStatus = value;
            OnPropertyChanged();
        }
    }

    public Color _permitColour { get; set; } = Color.Green;

    public Color PermitColour
    {
        get => _permitColour;
        set
        {
            _permitColour = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _signatureAimg { get; set; }

    public ImageSource SignatureAimg
    {
        get => _signatureAimg;
        set
        {
            _signatureAimg = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _signatureBimg { get; set; }

    public ImageSource SignatureBimg
    {
        get => _signatureBimg;
        set
        {
            _signatureBimg = value;
            OnPropertyChanged();
        }
    }


    public string _signedByA { get; set; }

    public string SignedByA
    {
        get => _signedByA;
        set
        {
            _signedByA = value;
            OnPropertyChanged();
        }
    }

    public string _signedByB { get; set; }

    public string SignedByB
    {
        get => _signedByB;
        set
        {
            _signedByB = value;
            OnPropertyChanged();
        }
    }


    public string _signatureAButtonText { get; set; }

    public string SignatureAButtonText
    {
        get => _signatureAButtonText;
        set
        {
            _signatureAButtonText = value;
            OnPropertyChanged();
        }
    }

    public string _signatureBButtonText { get; set; }

    public string SignatureBButtonText
    {
        get => _signatureBButtonText;
        set
        {
            _signatureBButtonText = value;
            OnPropertyChanged();
        }
    }


    public string _signatureADate { get; set; }

    public string SignatureADate
    {
        get => _signatureADate;
        set
        {
            _signatureADate = value;
            OnPropertyChanged();
        }
    }

    public string _signatureBDate { get; set; }

    public string SignatureBDate
    {
        get => _signatureBDate;
        set
        {
            _signatureBDate = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<SurveyQuestion> _permitQuestions { get; set; }

    public ObservableCollection<SurveyQuestion> PermitQuestions
    {
        get => _permitQuestions;
        set
        {
            _permitQuestions = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand SignAsOperative => new(async () =>
    {
        SigDetailArray[0].Type = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
        NavigationalParameters.AuthDetail = SigDetailArray[0];

        NavigationalParameters.AuthDetail.Type =
            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;


        NavigationalParameters.AuthDetail.Identifier = NavigationalParameters.CurrentPermit.PermitId;
        NavigationalParameters.AuthDetail.OnBehalf =
            $"{NavigationalParameters.CurrentSelectedJob.JobGang.ProjectManagerFirstName} {NavigationalParameters.CurrentSelectedJob.JobGang.ProjectManagerSurname}";

        //NavigationalParameters.ReturnPage = Locator.PermitPageViewModelKey;
        await NavigateTo(ViewModelLocator.SignaturePage);
    });

    public RelayCommand SignAsSupervisor => new(async () =>
    {
        SigDetailArray[1].Type = NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;
        NavigationalParameters.AuthDetail = SigDetailArray[1];

        NavigationalParameters.AuthDetail.Type =
            NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;

        NavigationalParameters.AuthDetail.Identifier = NavigationalParameters.CurrentPermit.PermitId;
        NavigationalParameters.AuthDetail.OnBehalf =
            $"{NavigationalParameters.CurrentSelectedJob.JobGang.ProjectManagerFirstName} {NavigationalParameters.CurrentSelectedJob.JobGang.ProjectManagerSurname}";

        //NavigationalParameters.ReturnPage = Locator.PermitPageViewModelKey;
        await NavigateTo(ViewModelLocator.SignaturePage);
    });

    public RelayCommand Submit => new(async () =>
    {
        NavigationalParameters.CurrentPermit.SignatureDetails = SigDetailArray;

        NavigationalParameters.CurrentPermit.UpdateFromSig();
        await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
        _jobService.AddPermit(NavigationalParameters.CurrentPermit);
        NavigationalParameters.CurrentPermit = new DigPermit
            { PermitType = NavigationalParameters.SurveyType.ToString() };
        NavigationalParameters.AuthDetail = new AuthorisationDetail();
        //Navigate back to the menu
        NavigationalParameters.NavigationParameter = "PermitComplete";
        //NavigationalParameters.ReturnPage = Locator.PermitPageViewModelKey;

        // NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;
        // NavigationalParameters.PassedInfo = new MenuPage();

        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    public RelayCommand Cancel => new(async () =>
    {
        //NavigationalParameters.ReturnPage = Locator.PermitPageViewModelKey;

        NavigateBack();
    });

    public async void PageError()
    {
        //NavigationalParameters.ReturnPage = Locator.PermitPageViewModelKey;
        await Alert("Incorrect parameters passed", "NAV_PARAM_ERROR");
        NavigateBack();
    }
}