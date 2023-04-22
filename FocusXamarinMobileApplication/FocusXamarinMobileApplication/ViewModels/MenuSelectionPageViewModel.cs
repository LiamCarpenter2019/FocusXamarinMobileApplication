#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class MenuSelectionPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Checks _checkService;
    private readonly Jobs _jobService;
    private RelayCommand<string> _checkLogin;
    private RelayCommand<string> _upload;

    public MenuSelectionPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        _assignmentService = new Assignments();
        _checkService = new Checks();
    }

    public Users _userService { get; set; }
    public AzureAuth AzureAuth { get; set; }
    public ServicesCrossed4Tablet Utility { get; set; }
    public List<Checks4TabletResponses> AllCurrentResponses { get; set; }

    public bool LabourFiles { get; set; }
    public bool logonCheckResult { get; set; }
    public bool SelectGangHidden { get; set; }
    public bool ApprovalUploadRequired { get; set; }
    public bool AllplantAccepted { get; set; }
    public bool CrossedUtilities { get; set; }
    public bool InputDiaryRecord { get; set; }
    public bool EndPointsAvailable { get; set; }
    public string[] Options { get; private set; }

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

    public JobData4Tablet _currentSelectedJob { get; set; }

    public JobData4Tablet CurrentSelectedJob
    {
        get => _currentSelectedJob;
        set
        {
            _currentSelectedJob = value;
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

    public bool _showSection7 { get; set; }

    public bool ShowSection7
    {
        get => _showSection7;
        set
        {
            _showSection7 = value;
            OnPropertyChanged();
        }
    }

    //===============================================================
    public ImageSource _buttonAImage { get; set; } = SimpleStaticHelpers.GetImage("Questions");

    public ImageSource ButtonAImage
    {
        get => _buttonAImage;
        set
        {
            _buttonAImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonBImage { get; set; } = SimpleStaticHelpers.GetImage("Timesheets");

    public ImageSource ButtonBImage
    {
        get => _buttonBImage;
        set
        {
            _buttonBImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonCImage { get; set; } = SimpleStaticHelpers.GetImage("InputDiary");

    public ImageSource ButtonCImage
    {
        get => _buttonCImage;
        set
        {
            _buttonCImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonDImage { get; set; } = SimpleStaticHelpers.GetImage("InputMeasure");

    public ImageSource ButtonDImage
    {
        get => _buttonDImage;
        set
        {
            _buttonDImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonEImage { get; set; } = SimpleStaticHelpers.GetImage("UtilitiesCrossed");

    public ImageSource ButtonEImage
    {
        get => _buttonEImage;
        set
        {
            _buttonEImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonFImage { get; set; } = SimpleStaticHelpers.GetImage("LateralConnection");

    public ImageSource ButtonFImage
    {
        get => _buttonFImage;
        set
        {
            _buttonFImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonGImage { get; set; } = SimpleStaticHelpers.GetImage("SiteAudit");

    public ImageSource ButtonGImage
    {
        get => _buttonGImage;
        set
        {
            _buttonGImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonHImage { get; set; } = SimpleStaticHelpers.GetImage("PermittoWork2");

    public ImageSource ButtonHImage
    {
        get => _buttonHImage;
        set
        {
            _buttonHImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonIImage { get; set; } = SimpleStaticHelpers.GetImage("JobPictures");

    public ImageSource ButtonIImage
    {
        get => _buttonIImage;
        set
        {
            _buttonIImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonJImage { get; set; } = SimpleStaticHelpers.GetImage("ProjectSummaryV2");

    public ImageSource ButtonJImage
    {
        get => _buttonJImage;
        set
        {
            _buttonJImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonKImage { get; set; } = SimpleStaticHelpers.GetImage("VisitorsLog");

    public ImageSource ButtonKImage
    {
        get => _buttonKImage;
        set
        {
            _buttonKImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonLImage { get; set; } = SimpleStaticHelpers.GetImage("MyPlantScreen");

    public ImageSource ButtonLImage
    {
        get => _buttonLImage;
        set
        {
            _buttonLImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonMImage { get; set; } = SimpleStaticHelpers.GetImage("DocumentsListing");

    public ImageSource ButtonMImage
    {
        get => _buttonMImage;
        set
        {
            _buttonMImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonNImage { get; set; } = SimpleStaticHelpers.GetImage("DocumentsListing");

    public ImageSource ButtonNImage
    {
        get => _buttonNImage;
        set
        {
            _buttonNImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonOImage { get; set; } = SimpleStaticHelpers.GetImage("SiteAudit");

    public ImageSource ButtonOImage
    {
        get => _buttonOImage;
        set
        {
            _buttonOImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonPImage { get; set; } = SimpleStaticHelpers.GetImage("SiteAudit");

    public ImageSource ButtonPImage
    {
        get => _buttonPImage;
        set
        {
            _buttonPImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonQImage { get; set; } = SimpleStaticHelpers.GetImage("CallabrationIcon");

    public ImageSource ButtonQImage
    {
        get => _buttonQImage;
        set
        {
            _buttonQImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonRImage { get; set; } = SimpleStaticHelpers.GetImage("SiteAudit");

    public ImageSource ButtonRImage
    {
        get => _buttonRImage;
        set
        {
            _buttonRImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonSImage { get; set; } = "";

    public ImageSource ButtonSImage
    {
        get => _buttonSImage;
        set
        {
            _buttonSImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonTImage { get; set; } = "";

    public ImageSource ButtonTImage
    {
        get => _buttonTImage;
        set
        {
            _buttonTImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonUImage { get; set; } = "";

    public ImageSource ButtonUImage
    {
        get => _buttonUImage;
        set
        {
            _buttonUImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonVImage { get; set; } = "";

    public ImageSource ButtonVImage
    {
        get => _buttonVImage;
        set
        {
            _buttonVImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonWImage { get; set; } = "";

    public ImageSource ButtonWImage
    {
        get => _buttonWImage;
        set
        {
            _buttonWImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonXImage { get; set; } = "";

    public ImageSource ButtonXImage
    {
        get => _buttonXImage;
        set
        {
            _buttonXImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _buttonYImage { get; set; } = "";

    public ImageSource ButtonYImage
    {
        get => _buttonYImage;
        set
        {
            _buttonYImage = value;
            OnPropertyChanged();
        }
    }

    public string _buttonAImageLabel { get; set; } = "Risk assesment";

    public string ButtonAImageLabel
    {
        get => _buttonAImageLabel;
        set
        {
            _buttonAImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonBImageLabel { get; set; } = "Timesheets";

    public string ButtonBImageLabel
    {
        get => _buttonBImageLabel;
        set
        {
            _buttonBImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonCImageLabel { get; set; } = "Diary";

    public string ButtonCImageLabel
    {
        get => _buttonCImageLabel;
        set
        {
            _buttonCImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonDImageLabel { get; set; } = "Measures";

    public string ButtonDImageLabel
    {
        get => _buttonDImageLabel;
        set
        {
            _buttonDImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonEImageLabel { get; set; } = "Utilities crossed";

    public string ButtonEImageLabel
    {
        get => _buttonEImageLabel;
        set
        {
            _buttonEImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonFImageLabel { get; set; } = "Laterals";

    public string ButtonFImageLabel
    {
        get => _buttonFImageLabel;
        set
        {
            _buttonFImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonGImageLabel { get; set; } = "Site / Pole clear";

    public string ButtonGImageLabel
    {
        get => _buttonGImageLabel;
        set
        {
            _buttonGImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonHImageLabel { get; set; } = "Permit";

    public string ButtonHImageLabel
    {
        get => _buttonHImageLabel;
        set
        {
            _buttonHImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonIImageLabel { get; set; } = "Project Images";

    public string ButtonIImageLabel
    {
        get => _buttonIImageLabel;
        set
        {
            _buttonIImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonJImageLabel { get; set; } = "Summary";

    public string ButtonJImageLabel
    {
        get => _buttonJImageLabel;
        set
        {
            _buttonJImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonKImageLabel { get; set; } = "Visitor Log";

    public string ButtonKImageLabel
    {
        get => _buttonKImageLabel;
        set
        {
            _buttonKImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonLImageLabel { get; set; } = "Plant";

    public string ButtonLImageLabel
    {
        get => _buttonLImageLabel;
        set
        {
            _buttonLImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonMImageLabel { get; set; } = "Operative Docs";

    public string ButtonMImageLabel
    {
        get => _buttonMImageLabel;
        set
        {
            _buttonMImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonNImageLabel { get; set; } = "Project Docs";

    public string ButtonNImageLabel
    {
        get => _buttonNImageLabel;
        set
        {
            _buttonNImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonOImageLabel { get; set; } = "Audit";

    public string ButtonOImageLabel
    {
        get => _buttonOImageLabel;
        set
        {
            _buttonOImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonPImageLabel { get; set; } = "Register utility strike";

    public string ButtonPImageLabel
    {
        get => _buttonPImageLabel;
        set
        {
            _buttonPImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonQImageLabel { get; set; } = "As Built";

    public string ButtonQImageLabel
    {
        get => _buttonQImageLabel;
        set
        {
            _buttonQImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonRImageLabel { get; set; } = "Remedials";

    public string ButtonRImageLabel
    {
        get => _buttonRImageLabel;
        set
        {
            _buttonRImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonSImageLabel { get; set; } = "";

    public string ButtonSImageLabel
    {
        get => _buttonSImageLabel;
        set
        {
            _buttonSImageLabel = value;
            OnPropertyChanged();
        }
    }

    public string _buttonTImageLabel { get; set; } = "";

    public string ButtonTImageLabel
    {
        get => _buttonTImageLabel;
        set
        {
            _buttonTImageLabel = value;
            OnPropertyChanged();
        }
    }


    public string _buttonUImageLabel { get; set; } = "";

    public string ButtonUImageLabel
    {
        get => _buttonUImageLabel;
        set
        {
            _buttonUImageLabel = value;
            OnPropertyChanged();
        }
    }


    public string _buttonVImageLabel { get; set; } = "";

    public string ButtonVImageLabel
    {
        get => _buttonVImageLabel;
        set
        {
            _buttonVImageLabel = value;
            OnPropertyChanged();
        }
    }


    public string _buttonWImageLabel { get; set; } = "";

    public string ButtonWImageLabel
    {
        get => _buttonWImageLabel;
        set
        {
            _buttonWImageLabel = value;
            OnPropertyChanged();
        }
    }


    public string _buttonXImageLabel { get; set; } = "";

    public string ButtonXImageLabel
    {
        get => _buttonXImageLabel;
        set
        {
            _buttonXImageLabel = value;
            OnPropertyChanged();
        }
    }


    public string _buttonYImageLabel { get; set; } = "";

    public string ButtonYImageLabel
    {
        get => _buttonYImageLabel;
        set
        {
            _buttonYImageLabel = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonA { get; set; } = true;

    public bool ShowButtonA
    {
        get => _showButtonA;
        set
        {
            _showButtonA = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonB { get; set; } = true;

    public bool ShowButtonB
    {
        get => _showButtonB;
        set
        {
            _showButtonB = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonC { get; set; } = true;

    public bool ShowButtonC
    {
        get => _showButtonC;
        set
        {
            _showButtonC = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonD { get; set; } = true;

    public bool ShowButtonD
    {
        get => _showButtonD;
        set
        {
            _showButtonD = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonE { get; set; } = true;

    public bool ShowButtonE
    {
        get => _showButtonE;
        set
        {
            _showButtonE = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonF { get; set; } = true;

    public bool ShowButtonF
    {
        get => _showButtonF;
        set
        {
            _showButtonF = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonG { get; set; } = true;

    public bool ShowButtonG
    {
        get => _showButtonG;
        set
        {
            _showButtonG = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonH { get; set; } = true;

    public bool ShowButtonH
    {
        get => _showButtonH;
        set
        {
            _showButtonH = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonI { get; set; } = true;

    public bool ShowButtonI
    {
        get => _showButtonI;
        set
        {
            _showButtonI = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonJ { get; set; } = true;

    public bool ShowButtonJ
    {
        get => _showButtonJ;
        set
        {
            _showButtonJ = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonK { get; set; } = true;

    public bool ShowButtonK
    {
        get => _showButtonK;
        set
        {
            _showButtonK = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonL { get; set; } = true;

    public bool ShowButtonL
    {
        get => _showButtonL;
        set
        {
            _showButtonL = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonM { get; set; } = true;

    public bool ShowButtonM
    {
        get => _showButtonM;
        set
        {
            _showButtonM = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonN { get; set; } = true;

    public bool ShowButtonN
    {
        get => _showButtonN;
        set
        {
            _showButtonN = value;
            OnPropertyChanged("ShowButtonA");
        }
    }

    public bool _showButtonO { get; set; } = true;

    public bool ShowButtonO
    {
        get => _showButtonO;
        set
        {
            _showButtonO = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonP { get; set; } = true;

    public bool ShowButtonP
    {
        get => _showButtonP;
        set
        {
            _showButtonP = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonQ { get; set; }

    public bool ShowButtonQ
    {
        get => _showButtonQ;
        set
        {
            _showButtonQ = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonR { get; set; } = true;

    public bool ShowButtonR
    {
        get => _showButtonR;
        set
        {
            _showButtonR = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonS { get; set; } = true;

    public bool ShowButtonS
    {
        get => _showButtonS;
        set
        {
            _showButtonS = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonT { get; set; } = true;

    public bool ShowButtonT
    {
        get => _showButtonT;
        set
        {
            _showButtonT = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonU { get; set; } = true;

    public bool ShowButtonU
    {
        get => _showButtonU;
        set
        {
            _showButtonU = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonV { get; set; } = true;

    public bool ShowButtonV
    {
        get => _showButtonV;
        set
        {
            _showButtonV = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonW { get; set; } = true;

    public bool ShowButtonW
    {
        get => _showButtonW;
        set
        {
            _showButtonW = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonX { get; set; } = true;

    public bool ShowButtonX
    {
        get => _showButtonX;
        set
        {
            _showButtonX = value;
            OnPropertyChanged();
        }
    }

    public bool _showButtonY { get; set; } = true;

    public bool ShowButtonY
    {
        get => _showButtonY;
        set
        {
            _showButtonY = value;
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

    public bool _showUploadButton { get; set; }

    public bool ShowUploadButton
    {
        get => _showUploadButton;
        set
        {
            _showUploadButton = value;
            OnPropertyChanged();
        }
    }

    //============ Enable Buttons ========================
    public bool _buttonAIsEnabled { get; set; }

    public bool ButtonAIsEnabled
    {
        get => _buttonAIsEnabled;
        set
        {
            _buttonAIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonBIsEnabled { get; set; }

    public bool ButtonBIsEnabled
    {
        get => _buttonBIsEnabled;
        set
        {
            _buttonBIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonCIsEnabled { get; set; }

    public bool ButtonCIsEnabled
    {
        get => _buttonCIsEnabled;
        set
        {
            _buttonCIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonDIsEnabled { get; set; }

    public bool ButtonDIsEnabled
    {
        get => _buttonDIsEnabled;
        set
        {
            _buttonDIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonEIsEnabled { get; set; }

    public bool ButtonEIsEnabled
    {
        get => _buttonEIsEnabled;
        set
        {
            _buttonEIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonFIsEnabled { get; set; }

    public bool ButtonFIsEnabled
    {
        get => _buttonFIsEnabled;
        set
        {
            _buttonFIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonGIsEnabled { get; set; }

    public bool ButtonGIsEnabled
    {
        get => _buttonGIsEnabled;
        set
        {
            _buttonGIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonHIsEnabled { get; set; }

    public bool ButtonHIsEnabled
    {
        get => _buttonHIsEnabled;
        set
        {
            _buttonHIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonIIsEnabled { get; set; }

    public bool ButtonIIsEnabled
    {
        get => _buttonIIsEnabled;
        set
        {
            _buttonIIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonJIsEnabled { get; set; }

    public bool ButtonJIsEnabled
    {
        get => _buttonJIsEnabled;
        set
        {
            _buttonJIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonKIsEnabled { get; set; }

    public bool ButtonKIsEnabled
    {
        get => _buttonKIsEnabled;
        set
        {
            _buttonKIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonLIsEnabled { get; set; }

    public bool ButtonLIsEnabled
    {
        get => _buttonLIsEnabled;
        set
        {
            _buttonLIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonMIsEnabled { get; set; }

    public bool ButtonMIsEnabled
    {
        get => _buttonMIsEnabled;
        set
        {
            _buttonMIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonNIsEnabled { get; set; }

    public bool ButtonNIsEnabled
    {
        get => _buttonNIsEnabled;
        set
        {
            _buttonNIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonOIsEnabled { get; set; }

    public bool ButtonOIsEnabled
    {
        get => _buttonOIsEnabled;
        set
        {
            _buttonOIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonPIsEnabled { get; set; }

    public bool ButtonPIsEnabled
    {
        get => _buttonPIsEnabled;
        set
        {
            _buttonPIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonQIsEnabled { get; set; }

    public bool ButtonQIsEnabled
    {
        get => _buttonQIsEnabled;
        set
        {
            _buttonQIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonRIsEnabled { get; set; }

    public bool ButtonRIsEnabled
    {
        get => _buttonRIsEnabled;
        set
        {
            _buttonRIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonSIsEnabled { get; set; }

    public bool ButtonSIsEnabled
    {
        get => _buttonSIsEnabled;
        set
        {
            _buttonSIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonTIsEnabled { get; set; }

    public bool ButtonTIsEnabled
    {
        get => _buttonTIsEnabled;
        set
        {
            _buttonTIsEnabled = value;
            OnPropertyChanged();
        }
    }


    public bool _buttonUIsEnabled { get; set; }

    public bool ButtonUIsEnabled
    {
        get => _buttonUIsEnabled;
        set
        {
            _buttonUIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonVIsEnabled { get; set; }

    public bool ButtonVIsEnabled
    {
        get => _buttonVIsEnabled;
        set
        {
            _buttonVIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonWIsEnabled { get; set; }

    public bool ButtonWIsEnabled
    {
        get => _buttonWIsEnabled;
        set
        {
            _buttonWIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonXIsEnabled { get; set; }

    public bool ButtonXIsEnabled
    {
        get => _buttonXIsEnabled;
        set
        {
            _buttonXIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool _buttonYIsEnabled { get; set; }

    public bool ButtonYIsEnabled
    {
        get => _buttonYIsEnabled;
        set
        {
            _buttonYIsEnabled = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoad => new(async () =>
    {
        NavigationalParameters.ResetParams();
        ShowUploadButton = true;
        PageLoadedAsync();
    });


    public RelayCommand CommandA => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.GANGER:
            case NavigationalParameters.AppTypes.YARDMAN:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.RISKASSESMENT;
                NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>();
                NavigationalParameters.AllCurrentResponses = new List<Checks4TabletResponses>();

                var dailySiteInspectionQuestions = _assignmentService.GetSurveyQuestions("DailyCheck")
                    .Where(x => x.QuestionOptions != "Button")
                    .ToList();

                AllCurrentResponses =
                    _checkService.GetAllRelevantResponses4Dalies(CurrentSelectedJob.QuoteNumber.ToString(),
                        CurrentSelectedJob.JobDate, CurrentSelectedJob.GangLeaderName) ??
                    new List<Checks4TabletResponses>();

                foreach (var question in dailySiteInspectionQuestions)
                {
                    Checks4TabletResponses currentAnswer = null;

                    if (AllCurrentResponses.Any(x => x.QuestionId == question.QuestionId))
                        currentAnswer = AllCurrentResponses?.FirstOrDefault(x => x.QuestionId == question.QuestionId);

                    NavigationalParameters.YesNoCollection.Add(new YesNoNaQuestionViewModel
                    {
                        Question = question.Question,
                        QuestionId = question.QuestionId,
                        KeyAnswer = question.KeyAnswer,
                        Category = question.Category,
                        DepthorRating = question.DepthorRating,
                        FollowUpAction = question.FollowUpAction,
                        FurtherQuestionId = question.FurtherQuestionId,
                        Grouping = question.Grouping,
                        InformationTo = question.InformationTo,
                        NotifiableResponse = question.NotifiableResponse,
                        QuestionOptions = question.QuestionOptions,
                        ResponseType = question.ResponseType,
                        Id = question.Id,
                        Stage = question.Stage,
                        BtnYesBgColour = currentAnswer != null && currentAnswer.QuestionResponse
                            .ToLower()
                            .Contains("yes")
                            ? Color.LawnGreen
                            : Color.LightGray,
                        BtnNoBgColour = currentAnswer != null && currentAnswer.QuestionResponse
                            .ToLower()
                            .Contains("no")
                            ? Color.LawnGreen
                            : Color.LightGray,
                        BtnNaBgColour = currentAnswer != null && currentAnswer.QuestionResponse
                            .ToLower()
                            .Contains("n/a")
                            ? Color.LawnGreen
                            : Color.LightGray,
                        ShowButtonA = question.QuestionOptions
                            .ToLower()
                            .Contains("yes"),
                        ShowButtonB = question.QuestionOptions
                            .ToLower()
                            .Contains("no"),
                        ShowButtonC = question.QuestionOptions
                            .ToLower()
                            .Contains("n/a")
                    });
                }

                NavigationalParameters.AllCurrentResponses = AllCurrentResponses;
                NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
                await NavigateTo(ViewModelLocator.DailySiteInspectionPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGSELECTION;
                await NavigateTo(ViewModelLocator.GangSelectPage);
                ;
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandB => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.TIMESHEETS;
                await NavigateTo(ViewModelLocator.TimesheetSelectionPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTDIARIES;

                var timeSheet =
                    _userService.GetSupervisorLabour(NavigationalParameters
                        .LoggedInUser.FocusId);
                if (timeSheet == null)
                    await Alert(
                        "Please enter a daily diary start time first.",
                        "No Diary Start Time");
                else if (timeSheet.IsSubmited)
                    await Alert(
                        "Today's Diary has already been completed.",
                        "Diary Uploaded");
                else
                    await NavigateTo(ViewModelLocator.SupervisorDiaryPage);

                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandC => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.DIARIES;
                await NavigateTo(ViewModelLocator.DiarySelectionPage);
                ;
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.ASBUILT;
                await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandD => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MEASURES;
                await NavigateTo(ViewModelLocator.MeasureTypeSelectionPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:

                if (_jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                    .Any(x => x.BuildingStandard == "street"))
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITECLEAR;
                    //NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.SITECLEAR;
                    //NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(_assignmentService.GetSurveyQuestions("siteclear").Select(question => new YesNoNaQuestionViewModel
                    //{
                    //    BtnNaBgColour = Color.LightGray,
                    //    BtnYesBgColour = Color.LightGray,
                    //    BtnNoBgColour = Color.LightGray,
                    //    BtnNaText = question.QuestionOptions?.Split(',')[2],
                    //    BtnNoText = question.QuestionOptions?.Split(',')[1],
                    //    BtnYesText = question.QuestionOptions?.Split(',')[0],
                    //    Question = question.Question,
                    //    FurtherQuestionId = question.FurtherQuestionId,
                    //    QuestionOptions = question.QuestionOptions,
                    //    Category = question.Category,
                    //    DepthorRating = question.DepthorRating,
                    //    FollowUpAction = question.FollowUpAction,
                    //    Grouping = question.Grouping,
                    //    InformationTo = question.InformationTo,
                    //    KeyAnswer = question.KeyAnswer,
                    //    NotifiableResponse = question.NotifiableResponse,
                    //    QuestionId = question.QuestionId,
                    //    Stage = question.Stage,
                    //    ResponseType = question.ResponseType,
                    //    Id = question.Id,
                    //    IsEnabled = true,
                    //    ShowButtonA = question.QuestionId == 0.10M ? false : true,
                    //    ShowButtonB = question.QuestionId == 0.10M ? false : true,
                    //    ShowButtonC = question.QuestionId == 0.10M ? false : true
                    //}));
                    //NavigationalParameters.Questions = new ObservableCollection<SurveyQuestion>(_assignmentService.GetSurveyQuestions("siteclear"));
                    await NavigateTo(ViewModelLocator.SelectStreetPage);
                }
                else
                {
                    await Alert("There are no streets to select please contact support for furthur assisatance",
                        "No streets available to select ", "Ok");
                }

                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandE => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.UTILITIES;

                var crossedUtilities =
                    _jobService.GetCrossedUtilities(CurrentSelectedJob);

                if (crossedUtilities == null)
                {
                    crossedUtilities = new ServicesCrossed4Tablet
                    {
                        ContractId = CurrentSelectedJob?.ContractNumber.ToString(),
                        QuoteId = CurrentSelectedJob?.QuoteNumber.ToString(),
                        ContractReference = CurrentSelectedJob?.ContractReference,
                        GangLeaderId = CurrentSelectedJob?.GangLeaderId.ToString(),
                        PostedDate = CurrentSelectedJob.JobDate.Date
                    };

                    await _jobService.AddCrossedUtilities(crossedUtilities);
                }

                await NavigateTo(ViewModelLocator.ServicesCrossedPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTIMAGES;
                await NavigateTo(ViewModelLocator.ProjectImagesPage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });
    
    public RelayCommand CommandF => new(async () =>
    {
        //var cableRuns = _jobService.GetCableRuns(CurrentSelectedJob.QuoteNumber);

        //if (!cableRuns.Any())
        //{
        //    await Alert("No Cable Runs", "There are no cable runs on this job to prove or register blockages against. Please contact support for furthur information if required!", "OK");
        //}
        //else
        //{
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.BLOCKAGE;
                await NavigateTo(ViewModelLocator.SelectEndPointPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUMMARY;
                await NavigateTo(ViewModelLocator.ProjectSummaryPage);
                break;
        }
        // }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandG => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                if (!EndPointsAvailable)
                {
                    await Alert("No location available",
                        "There are no locations available on this job to clear!. Please contact support for furthur information if required!");
                }
                else
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITECLEAR;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.SITECLEAR;
                    NavigationalParameters.CurrentAssignment = null;
                    NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
                        _assignmentService.GetSurveyQuestions("siteclear").Select(question =>
                            new YesNoNaQuestionViewModel
                            {
                                BtnNaBgColour = Color.LightGray,
                                BtnYesBgColour = Color.LightGray,
                                BtnNoBgColour = Color.LightGray,
                                BtnNaText = question.QuestionOptions.Split(',')[2],
                                BtnNoText = question.QuestionOptions.Split(',')[1],
                                BtnYesText = question.QuestionOptions.Split(',')[0],
                                Question = question.Question,
                                FurtherQuestionId = question.FurtherQuestionId,
                                QuestionOptions = question.QuestionOptions,
                                Category = question.Category,
                                DepthorRating = question.DepthorRating,
                                FollowUpAction = question.FollowUpAction,
                                Grouping = question.Grouping,
                                InformationTo = question.InformationTo,
                                KeyAnswer = question.KeyAnswer,
                                NotifiableResponse = question.NotifiableResponse,
                                QuestionId = question.QuestionId,
                                Stage = question.Stage,
                                ResponseType = question.ResponseType,
                                Id = question.Id,
                                IsEnabled = true,
                                ShowButtonA = question.QuestionId == 0.10M ? false : true,
                                ShowButtonB = question.QuestionId == 0.10M ? false : true,
                                ShowButtonC = question.QuestionId == 0.10M ? false : true
                            }));

                    await NavigateTo(ViewModelLocator.SelectStreetPage);
                }

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.VISITORSLOG;

                if (CurrentSelectedJob.JobDate.Date == DateTime.Now.Date)
                    await NavigateTo(ViewModelLocator.VisitorLogListPage);
                else
                    await Alert("Error", "Visitors can only be added to todays jobs!");

                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandH => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                if (CurrentSelectedJob.JobDate.Date == DateTime.Now.Date)
                {
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PERMITTODIG;
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.PERMITTODIG;

                    await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                }
                else
                {
                    await Alert("Error",
                        "A permit request can only be performed prior or on the current date of work!");
                }

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITE;
                await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandI => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTIMAGES;
                NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PROJECTIMAGES;
                await NavigateTo(ViewModelLocator.ProjectImagesPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.ReturnFromDocView = false;
                NavigationalParameters.NavigationParameter =
                    new Tuple<string, JobData4Tablet>("Training",
                        CurrentSelectedJob);
                await NavigateTo(ViewModelLocator.DocumentListingPage);
                ;
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandJ => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.SUMMARY;
                await NavigateTo(ViewModelLocator.ProjectSummaryPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.ReturnFromDocView = false;

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTDOCS;
                NavigationalParameters.NavigationParameter =
                    new Tuple<string, JobData4Tablet>("JobPackDocs",
                        CurrentSelectedJob);
                await NavigateTo(ViewModelLocator.DocumentListingPage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandK => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.VISITORSLOG;

                var visitors = _jobService.GetVisitors(CurrentSelectedJob);
                if (CurrentSelectedJob.JobDate.Date == DateTime.Now.Date)
                    await NavigateTo(ViewModelLocator.VisitorLogListPage);
                else
                    await Alert("Error", "Visitors can only be added to todays jobs!");
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITEINSPECTION;
                //  NavigationalParameters.RatingQuestions = new ObservableCollection<RatingQuestionViewModel>(_assignmentService.GetSurveyQuestions("audit"));

                NavigationalParameters.RatingQuestions = new ObservableCollection<RatingQuestionViewModel>(
                    _assignmentService.GetSurveyQuestions("audit").Where(x => x.Category.ToLower() == "audit").Select(
                        question => new RatingQuestionViewModel
                        {
                            Question = question.Question,
                            QuestionId = question.QuestionId,
                            KeyAnswer = question.KeyAnswer,
                            Category = question.Category,
                            DepthorRating = question.DepthorRating,
                            FollowUpAction = question.FollowUpAction,
                            FurtherQuestionId = question.FurtherQuestionId,
                            Grouping = question.Grouping,
                            InformationTo = question.InformationTo,
                            NotifiableResponse = question.NotifiableResponse,
                            QuestionOptions = question.QuestionOptions,
                            ResponseType = question.ResponseType,
                            Id = question.Id,
                            Stage = question.Stage,
                            IsEnabled = true,
                            Rating10 = SimpleStaticHelpers.GetImage("10grey"),
                            Rating9 = SimpleStaticHelpers.GetImage("9grey"),
                            Rating8 = SimpleStaticHelpers.GetImage("8grey"),
                            Rating7 = SimpleStaticHelpers.GetImage("7grey"),
                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowgrey"),
                            Rating5 = SimpleStaticHelpers.GetImage("5grey"),
                            Rating4 = SimpleStaticHelpers.GetImage("4grey"),
                            Rating3 = SimpleStaticHelpers.GetImage("3grey"),
                            Rating2 = SimpleStaticHelpers.GetImage("2grey"),
                            Rating1 = SimpleStaticHelpers.GetImage("1grey"),
                            RatingNA = SimpleStaticHelpers.GetImage("NAgrey")
                        }));

                await NavigateTo(ViewModelLocator.GangSelectPage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandL => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                if (CurrentSelectedJob.JobDate.Date == DateTime.Now.Date)
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.PLANT;
                    await NavigateTo(ViewModelLocator.MyPlantScreenPage);
                }
                else
                {
                    await Alert("Error", "Plant can only be accesed on jobs for the current day!");
                }

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.EVENTMANAGEMENT;
                await NavigateTo(ViewModelLocator.EventManagementSelectionPage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandM => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.ReturnFromDocView = false;

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.OPERATIVEDOCS;
                NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN;
                await NavigateTo(ViewModelLocator.SignaturePage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandN => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.ReturnFromDocView = false;

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.PROJECTDOCS;
                await NavigateTo(ViewModelLocator.DocumentListingPage);
                ;
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandO => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                if (CurrentSelectedJob.JobDate.Date == DateTime.Now.Date)
                {
                    NavigationalParameters.CurrentAssignment = null;
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITEINSPECTION;
                    NavigationalParameters.CurrentAudit = NavigationalParameters.LatestAudit = null;
                    NavigationalParameters.AuditStatus = NavigationalParameters.AuditStatuses.NEW;


                    await NavigateTo(ViewModelLocator.SiteInspectionResultPage);
                }
                else
                {
                    await Alert("Error", "Site inspections can only be completed on todays gangs!");
                }

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandP => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.EVENTMANAGEMENT;
                await NavigateTo(ViewModelLocator.EventManagementSelectionPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandQ => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.CHAMBERAUDIT;

        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.ASBUILT;

                try
                {
                    NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
                        _assignmentService.GetSurveyQuestions("chamber")?
                            .Select(question => new YesNoNaQuestionViewModel
                            {
                                BtnNaBgColour = Color.LightGray,
                                BtnYesBgColour = Color.LightGray,
                                BtnNoBgColour = Color.LightGray,
                                BtnNaText = question?.QuestionOptions?.Split(',')[2],
                                BtnNoText = question?.QuestionOptions?.Split(',')[1],
                                BtnYesText = question?.QuestionOptions?.Split(',')[0],
                                Question = question?.Question,
                                FurtherQuestionId = question?.FurtherQuestionId,
                                QuestionOptions = question?.QuestionOptions,
                                Category = question?.Category,
                                DepthorRating = question.DepthorRating,
                                FollowUpAction = question?.FollowUpAction,
                                Grouping = question?.Grouping,
                                InformationTo = question?.InformationTo,
                                KeyAnswer = question?.KeyAnswer,
                                NotifiableResponse = question?.NotifiableResponse,
                                QuestionId = question.QuestionId,
                                Stage = question?.Stage,
                                ResponseType = question?.ResponseType,
                                Id = question.Id,
                                IsEnabled = true,
                                ShowButtonA = question?.QuestionId == 0.10M ? false : true,
                                ShowButtonB = question?.QuestionId == 0.10M ? false : true,
                                ShowButtonC = question?.QuestionId == 0.10M ? false : true
                            }));
                }
                catch (Exception ex)
                {
                    var errror = ex.ToString();
                }


                await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandR => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.REMEDIAL;
                await NavigateTo(ViewModelLocator.SelectEndPointPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandS => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandT => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.LATERALS;
                var EndPointsAvailable = _jobService.CheckEndpoints(CurrentSelectedJob);
                if (!EndPointsAvailable)
                    await Alert("There are currently no end points available",
                        "please contact the support team!");
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandU => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandV => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandW => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandX => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand CommandY => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:

                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand Back => new(async () =>
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;
                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.JOBLIST;
                await NavigateTo(ViewModelLocator.MainListPage);
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;
                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;
                await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                break;
        }

        NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
    });

    public RelayCommand Upload => new(async () =>
    {
        var userChoice = await Alert("Submit Daily Input?", "Would you like to upload your entire work record for today? Please note that once this has been confirmed then you will not be able to access this record again as everything will be uploaded to the server!. If you have any queries please contact suppoprt!", "Yes", "No");


        if (userChoice)
        {
            if (SelectGangHidden)
            {
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.APPROVALS;
                await NavigateTo(ViewModelLocator.TeamOptionsPage);
            }
            else
            {
                ShowSection3 = false;
                ShowSection4 = false;
                ShowSection5 = false;
                ShowSection2 = false;
                var checksComplete = true;
                var dailyUploadData = _jobService.GetDailyUploadData(
                    new Tuple<Person, List<Assignment>, JobData4Tablet>(
                        NavigationalParameters.LoggedInUser, NavigationalParameters.AssignmentList,
                        CurrentSelectedJob));

                switch (NavigationalParameters.AppType)
                {
                    case NavigationalParameters.AppTypes.YARDMAN:
                    case NavigationalParameters.AppTypes.GANGER:
                        {
                            IsLoading = true;
                            ShowUploadButton = false;
                            if (!CrossedUtilities)
                            {
                                try
                                {
                                    checksComplete = false;
                                    IsLoading = false;
                                    ShowUploadButton = true;
                                    await Alert(
                                        "Failed to save the daily measures as utilities crossed needs to be completed",
                                        "Incomplete!");
                                }
                                catch (Exception ex)
                                {
                                    var error = ex.ToString();
                                }
                            }
                            else if (NavigationalParameters.VisitorsStillToBeLoggedOut)
                            {
                                checksComplete = false;
                                await Alert(
                                    "Failed to save the daily measures as visitors must be signed out",
                                    "Incomplete!");
                                IsLoading = false;
                                ShowUploadButton = true;
                            }
                            else if (!InputDiaryRecord)
                            {
                                checksComplete = false;
                                await Alert(
                                    "Failed to save the daily measures as daily diary need to be complete",
                                    "Incomplete!");
                                IsLoading = false;
                                ShowUploadButton = true;
                            }
                            else if (!LabourFiles)
                            {
                                checksComplete = false;
                                await Alert(
                                    "Failed to save the daily measures as time sheets need to be completed",
                                    "Incomplete!");
                                IsLoading = false;
                                ShowUploadButton = true;
                            }
                            else if (LabourFiles)
                            {
                                foreach (var lf in dailyUploadData.LabourFiles)
                                    if (lf.ClaimedYardEnd.TimeOfDay == TimeSpan.Parse("00:00:00") &&
                                        lf.TimeLeftSite.TimeOfDay == TimeSpan.Parse("00:00:00") &&
                                        lf.TravelFromSite.TimeOfDay == TimeSpan.Parse("00:00:00") &&
                                        lf.LabourType.ToLower() == "w")
                                    {
                                        checksComplete = false;
                                        await Alert(
                                            "Failed to save the daily measures as end time need to be completed! either complete the yard",
                                            "Incomplete!");
                                        IsLoading = false;
                                        ShowUploadButton = true;
                                    }
                            }
                            else if (!CurrentSelectedJob.DailyChecksCompleted)
                            {
                                checksComplete = false;
                                await Alert(
                                    "Failed to save the daily measures need to be completed",
                                    "Incomplete!");
                                IsLoading = false;
                                ShowUploadButton = true;
                            }

                            if (checksComplete)
                            {
                                if (dailyUploadData.DailyDiary == null ||
                                    string.IsNullOrEmpty(dailyUploadData.DailyDiary.NotesText))
                                {
                                    await Alert(
                                        "Daily diary must be completed, Please Try again",
                                        "Error!");

                                    IsLoading = false;
                                    ShowUploadButton = true;
                                }
                                else if (dailyUploadData.LabourFiles.Any(x =>
                                             x.TimeLeftSite == DateTime.Parse("1900-01-01 00:00:00.000")
                                             && x.TimeLeftSite == DateTime.Parse("1900-01-01 00:00:00.000")
                                             && x.TimeLeftSite == DateTime.Parse("1900-01-01 00:00:00.000")))
                                {
                                    await Alert(
                                        "Please complete Time sheets ensuring both start and finish times as required!",
                                        "Error!");
                                }
                                else if (EndPointsAvailable && string.IsNullOrEmpty(dailyUploadData.DailyDiary
                                                                .StartAddress.Trim())
                                                            && (dailyUploadData.DailyDiary
                                                                    .StartEndpointId == 0
                                                                || dailyUploadData.DailyDiary
                                                                    .EndEndpointId == 0))
                                {
                                    await Alert(
                                        "Please complete start and end of day diary details",
                                        "Error!");
                                    IsLoading = false;
                                    ShowUploadButton = true;
                                }
                                else if (!EndPointsAvailable && (string.IsNullOrEmpty(dailyUploadData.DailyDiary
                                                                     .StartAddress.Trim())
                                                                 || string.IsNullOrEmpty(
                                                                     dailyUploadData.DailyDiary
                                                                         .EndAddress.Trim())))
                                {
                                    await Alert(
                                        "Please complete start and end of day diary details",
                                        "Error!");
                                    IsLoading = false;
                                    ShowUploadButton = true;
                                }
                            }
                        }
                        break;
                    case NavigationalParameters.AppTypes.SUPERVISOR:

                        break;
                }

                if (dailyUploadData.DailyDiary == null
                    && dailyUploadData.ClaimedFiles.All(x => x.RemoteTableId != 0)
                    && dailyUploadData.Assignments.All(x => x.RemoteTableId != 0)
                    && dailyUploadData.Permits.All(x => x.RemoteTableId != 0)
                    && dailyUploadData.VisitorLogs.All(x => x.RemoteTableId != 0)
                    && dailyUploadData.JobPictureData.All(x => x.SeverPictureId != 0))
                {
                    await Alert("There is nothing to upload", "Error!");
                }
                else
                {
                    if (checksComplete)
                    {
                        if (!dailyUploadData.ClaimedFiles.Any())
                        {
                            var ok = await Alert(
                                "Warning!! No measures have been entered can you confirm that you have no measures or enter any measures that you have missed",
                                "NO MEASURES HAVE BEEN ENTERED", "Confirm",
                                "Measures");

                            if (ok)
                                try
                                {
                                    IsLoading = true;
                                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DEFAULT;
                                    var success = await _jobService.SaveDailyInputAsync(dailyUploadData);

                                    if (!success)
                                    {
                                        IsLoading = false;
                                        ShowUploadButton = true;
                                        await Alert("Failed to save the daily measures please contact support", "Error!");
                                    }
                                    else
                                    {
                                        IsLoading = true;

                                        NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                                        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                                            NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                                            await NavigateTo(ViewModelLocator.MainListPage);
                                        else
                                            await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                                    }

                                    IsLoading = false;
                                }
                                catch (Exception ex)
                                {
                                    var error = ex.ToString();
                                    await Alert(
                                        "The daily measures Have failed To Save, Please Try again",
                                        "Error!");

                                    IsLoading = false;
                                    ShowUploadButton = true;
                                }
                            else
                                switch (NavigationalParameters.AppType)
                                {
                                    case NavigationalParameters.AppTypes.YARDMAN:
                                    case NavigationalParameters.AppTypes.GANGER:
                                        await NavigateTo(ViewModelLocator.MeasureTypeSelectionPage);
                                        break;
                                    case NavigationalParameters.AppTypes.SUPERVISOR:
                                        await NavigateTo(ViewModelLocator.TeamOptionsPage);
                                        break;
                                }
                        }
                        else
                        {
                            try
                            {
                                IsLoading = true;

                                var success = await _jobService.SaveDailyInputAsync(dailyUploadData);

                                if (!success)
                                {
                                    IsLoading = false;
                                    await Alert(
                                        "Failed to save the daily measures please contact support",
                                        "Error!");
                                }
                                else
                                {
                                    IsLoading = false;

                                    NavigationalParameters.NavigationParameter =
                                        new Tuple<Person, List<Assignment>>(
                                            NavigationalParameters.LoggedInUser,
                                            new List<Assignment>()); // NavPassedData;

                                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                                        NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                                        await NavigateTo(ViewModelLocator.MainListPage);
                                    else
                                        await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                                }

                                IsLoading = false;
                                IsLoading = false;
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                                await Alert(
                                    "The daily measures Have failed To Save, Please Try again",
                                    "Error!");

                                IsLoading = false;
                                ShowUploadButton = true;
                            }
                        }
                    }
                }

                ShowSection3 = false;
                ShowSection4 = false;
                ShowSection5 = false;
                ShowSection2 = false;
            }

            NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
        }
    });


    public RelayCommand<string> CheckLogin => _checkLogin ??= new RelayCommand<string>(async e =>
    {
        NavigationalParameters.AppMode =
            NavigationalParameters.AppModes.MENU;

        AzureAuth = new AzureAuth();

        NavigationalParameters.AuthDetail = new AuthorisationDetail();
        var cl = AzureAuth.GetUserAuthInformation();

        if (cl != null && cl.LoginDateTime != null)
        {
            var nowMinus10Hrs = DateTime.Now.AddHours(-10);

            if (cl.LoginDateTime < nowMinus10Hrs)
            {
                var n = cl.LoginDateTime;
                await AzureAuth.SignoutUser();
                logonCheckResult = false;
                await NavigateTo(ViewModelLocator.LogInPage);
                ;
            }
            else
            {
                if (cl.LoginDateTime > nowMinus10Hrs &&
                    cl.LoginDateTime <= DateTime.Now)
                {
                    var n = cl.LoginDateTime;
                    logonCheckResult = true;
                }
                else
                {
                    var result = await Alert("Warning!! You are about to be loged out",
                        "Logging out", "Ok",
                        "Cancel");


                    if (result)
                    {
                        var n = cl.LoginDateTime;
                        await AzureAuth.SignoutUser();
                        logonCheckResult = false;

                        await NavigateTo(ViewModelLocator.LogInPage);
                        ;
                    }
                    else
                    {
                        logonCheckResult = true;
                    }
                }
            }
        }
    });

    public RelayCommand RefreshPageChecks => new(() =>
    {
        NavigationalParameters.VisitorsStillToBeLoggedOut = _jobService
            .GetVisitors(CurrentSelectedJob)
            .Any(x => x.OffSiteTime == DateTime.Parse("1/1/1900 00:00:00"));

        // NavigationalParameters.StartOfDayPictureTaken = _jobService.CheckStartOfDayPictureExsits();
    });

    [Time]
    public bool ConfirmAuthorised()
    {
        return !string.IsNullOrEmpty(NavigationalParameters.AuthDetail?.AuthorisedName);
    }

    [Time]
    private void AllOperativesToJob()
    {
        var allGangs = new List<GangMember>();
        var super = NavigationalParameters.LoggedInUser;

        try
        {
            foreach (var job in NavigationalParameters.ProjectJobs)
            {
                job.JobGang = _jobService.GetGang(job);
                if (job.JobGang.GangMembers != null)
                    foreach (var member in job.JobGang.GangMembers)
                        if (allGangs.All(x => x.Id != member.Id))
                            allGangs.Add(member);
            }

            CurrentSelectedJob.JobGang.GangMembers = allGangs;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    private void RefreshAllCurrentDailyChecks()
    {
        try
        {
            if (AllCurrentResponses == null)
                AllCurrentResponses = new List<Checks4TabletResponses>();


            var getAllRelevantResponses = _checkService.GetAllRelevantResponses4Dalies(
                CurrentSelectedJob.QuoteNumber.ToString(), DateTime.Now,
                CurrentSelectedJob.GangLeaderName);

            if (getAllRelevantResponses != null) AllCurrentResponses = getAllRelevantResponses;

            var DailyChecks2Show = _checkService.GetMyDailyChecks();
            // RaisePropertyChanged(() => DailyChecks2Show);

            // Now need to correctly set buttons based on status
            if (DailyChecks2Show != null && DailyChecks2Show.Count > 0 && AllCurrentResponses != null &&
                AllCurrentResponses.Count > 0 && CurrentSelectedJob.DailyChecksCompleted == false)
                CurrentSelectedJob.DailyChecksCompleted =
                    AreAllDailyChecksCompleted(DailyChecks2Show, AllCurrentResponses);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async void PageLoadedAsync()
    {
        Title = "MENU";

        CurrentSelectedJob = NavigationalParameters.CurrentSelectedJob;

        ProjectName = CurrentSelectedJob?.ProjectName;

        ProjectDate = $"{CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy")}";

        CurrentSelectedJob.GangLeaderName = _userService
            .GetUserById(Convert.ToInt32(CurrentSelectedJob.GangLeaderId))?.FullName;

        CheckLogin.Execute(null);

        var locationStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (locationStatus != PermissionStatus.Granted)
        {
            var lStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        var cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

        if (cameraStatus != PermissionStatus.Granted)
        {
            var cStatus = await Permissions.RequestAsync<Permissions.Camera>();
        }

        if (ConfirmAuthorised())
        {
            Upload.Execute("Upload");

            ShowUploadButton = false;

            IsLoading = true;
        }

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;

        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.GANGER:
            case NavigationalParameters.AppTypes.YARDMAN:
                ShowSection1 = true;
                ShowSection2 = true;
                ShowSection3 = true;
                ShowSection4 = true;
                ShowSection5 = true;
                ShowSection6 = true;
                ShowSection7 = true;
                ShowUploadButton = true;
                IsLoading = false;

                ProjectName = CurrentSelectedJob?.ProjectName;

                if (CurrentSelectedJob != null)
                {
                    CurrentSelectedJob.JobGang = _jobService.GetCurrentSelectedGang();

                    Utility = _jobService.GetCrossedUtilities(CurrentSelectedJob);

                    if (Utility == null)
                        CrossedUtilities = false;
                    else
                        CrossedUtilities = !string.IsNullOrEmpty(Utility.ServicesCrossedData1) &&
                                           Utility.RemoteTableId == 0;

                    NavigationalParameters.VisitorsStillToBeLoggedOut = _jobService
                        .GetVisitors(CurrentSelectedJob)
                        .Any(x => x.OffSiteTime == DateTime.Parse("1/1/1900 00:00:00"));

                    NavigationalParameters.VisitorCount = _jobService
                        .GetVisitors(CurrentSelectedJob)
                        .Count();


                    InputDiaryRecord =
                        !string.IsNullOrEmpty(_jobService
                            .GetClaimedNotes(CurrentSelectedJob)?.NotesText) &&
                        !string.IsNullOrEmpty(_jobService
                            .GetClaimedNotes(CurrentSelectedJob)?.AnyDelays) &&
                        !string.IsNullOrEmpty(_jobService
                            .GetClaimedNotes(CurrentSelectedJob)
                            ?.AnyAddnlWorkReq);

                    LabourFiles = (bool)CurrentSelectedJob?.JobGang?.GangLabourFiles.All(
                        x => x.MyLabour.Exists(
                            y => y.LabourDate.ToString() ==
                                 CurrentSelectedJob?.JobDate.ToString()
                                 && y.ContractReference == CurrentSelectedJob?
                                     .ContractReference
                                 && (y.ClaimedYardStart != DateTime.Parse("1/1/1900 00:00:00")
                                     || (y.TravelToSite != DateTime.Parse("1/1/1900 00:00:00")
                                         && y.TimeOnSite != DateTime.Parse("1/1/1900 00:00:00"))
                                 )));

                    CurrentSelectedJob.JobProjectSummary =
                        _assignmentService.GetProjectSummary((long)CurrentSelectedJob?
                            .QuoteNumber);

                    EndPointsAvailable =
                        _jobService.GetEndpoints(CurrentSelectedJob)?.Count > 0;

                    NavigationalParameters.ProjectWorksList = _assignmentService
                        .GetRates(CurrentSelectedJob.QuoteNumber, "Contract")
                        .Where(x => x.AssignmentId == Guid.Empty)
                        .Select(i => new ProjectWorks
                        {
                            RemoteTableId = i.RemoteTableId,
                            BaseUnit = i?.BaseUnit,
                            Description = i?.Description,
                            Header = i?.Header,
                            QuoteId = i.QuoteId,
                            Category = i?.Category.ToUpper(),
                            AssignmentId = i.AssignmentId,
                            Identifier = i.Identifier,
                            Status = i.Status
                        }).ToList();
                }
                else
                {
                    CurrentSelectedJob = NavigationalParameters.JobTaskList?.FirstOrDefault(x =>
                        x.GangLeaderId == NavigationalParameters.LoggedInUser.FocusId);
                }

                AllplantAccepted = _jobService.CheckAllPlantHasBeenExcepted();

                NavigationalParameters.StartOfDayPictureTaken = _jobService.CheckStartOfDayPictureExsits("StartOfDay");

                NavigationalParameters.EndOfDayPictureTaken = _jobService.CheckStartOfDayPictureExsits("EndOfDay");

                RefreshAllCurrentDailyChecks();

                ButtonAImage = SimpleStaticHelpers.GetImage("Questions");
                ButtonAImageLabel = "Risk assesment";
                ShowButtonA = true;

                ButtonBImage = SimpleStaticHelpers.GetImage("Timesheets");
                ButtonBImageLabel = "Timesheets";
                ShowButtonB = true;

                ButtonCImage = SimpleStaticHelpers.GetImage("InputDiary");
                ButtonCImageLabel = "Diary";
                ShowButtonC = true;

                ButtonDImage = SimpleStaticHelpers.GetImage("InputMeasure");
                ButtonDImageLabel = "Measures";
                ShowButtonD = true;

                ButtonEImage = SimpleStaticHelpers.GetImage("UtilitiesCrossed");
                ButtonEImageLabel = "Utilities crossed";
                ShowButtonE = true;

                ButtonFImage = SimpleStaticHelpers.GetImage("LateralConnection");
                ButtonFImageLabel = "Blockages";
                ShowButtonF = true;

                ButtonGImage = SimpleStaticHelpers.GetImage("SiteAudit");
                ButtonGImageLabel = "Site clear";
                ShowButtonG = true;

                ButtonHImage = SimpleStaticHelpers.GetImage("PermittoWork2");
                ButtonHImageLabel = "Permit";
                ShowButtonH = true;

                ButtonIImage = SimpleStaticHelpers.GetImage("JobPictures");
                ButtonIImageLabel = "Project images";
                ShowButtonI = true;

                ButtonJImage = SimpleStaticHelpers.GetImage("ProjectSummaryV2");
                ButtonJImageLabel = "Summary";
                ShowButtonJ = true;

                ButtonKImage = SimpleStaticHelpers.GetImage("VisitorsLog");
                ButtonKImageLabel = "Visitor log";
                ShowButtonK = true;

                ButtonLImage = SimpleStaticHelpers.GetImage("MyPlantScreen");
                ButtonLImageLabel = "Plant";
                ShowButtonL = true;

                ButtonMImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                ButtonMImageLabel = "Operative docs";
                ShowButtonM = true;

                ButtonNImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                ButtonNImageLabel = "Project docs";
                ShowButtonN = true;

                ButtonOImage = SimpleStaticHelpers.GetImage("SiteAudit");
                ButtonOImageLabel = "Site Inspection";
                ShowButtonO = true;

                ButtonPImage = SimpleStaticHelpers.GetImage("Strike");
                ButtonPImageLabel = "Event Management";
                ShowButtonP = true;

                ButtonQImage = SimpleStaticHelpers.GetImage("SiteAudit");
                ButtonQImageLabel = "As Built";
                ShowButtonQ = true;

                ButtonRImage = SimpleStaticHelpers.GetImage("SiteAudit");
                ButtonRImageLabel = "Remedials";
                ShowButtonR = false;
                    //NavigationalParameters.LoggedInUser.MyGroups.Any(x =>
                        //x.GroupName.ToLower().Contains("leader"));

                ButtonSImage = "";
                ButtonSImageLabel = "";
                ShowButtonS = true;

                ButtonTImage = "";
                ButtonTImageLabel = "";
                ShowButtonT = true;

                ButtonUImage = "";
                ButtonUImageLabel = "";
                ShowButtonU = true;

                ButtonVImage = "";
                ButtonVImageLabel = "";
                ShowButtonV = true;

                ButtonWImage = "";
                ButtonWImageLabel = "";
                ShowButtonW = true;

                ButtonXImage = "";
                ButtonXImageLabel = "";
                ShowButtonX = true;

                ButtonYImage = "";
                ButtonYImageLabel = "";
                ShowButtonY = true;

                ShowUploadButton = (bool)CurrentSelectedJob?.DailyChecksCompleted && AllplantAccepted;

                var yesterday = DateTime.Now.Date.AddDays(-1);

                if (CurrentSelectedJob?.JobDate > DateTime.Now.Date)
                {
                    ButtonAImage = SimpleStaticHelpers.GetImage("QuestionsRed");
                    ButtonAIsEnabled = false;
                    ButtonBImage = SimpleStaticHelpers.GetImage("TimesheetsRed");
                    ButtonBIsEnabled = false;
                    ButtonCImage = SimpleStaticHelpers.GetImage("InputDiaryRed");
                    ButtonCIsEnabled = false;
                    ButtonDImage = SimpleStaticHelpers.GetImage("InputMeasureRed");
                    ButtonDIsEnabled = false;
                    ButtonEImage = SimpleStaticHelpers.GetImage("UtilitiesCrossedRed");
                    ButtonEIsEnabled = false;
                    ButtonFImage = SimpleStaticHelpers.GetImage("ProjectSummaryV2Red");
                    ButtonFIsEnabled = false;
                    ButtonGImage = SimpleStaticHelpers.GetImage("SiteAuditRed");
                    ButtonGIsEnabled = false;
                    ButtonHImage = SimpleStaticHelpers.GetImage("PermittoWork2Red");
                    ButtonHIsEnabled = false;
                    ButtonIImage = SimpleStaticHelpers.GetImage("JobPicturesRed");
                    ButtonIIsEnabled = false;
                    ButtonJImage = SimpleStaticHelpers.GetImage("ProjectSummaryV2");
                    ButtonJIsEnabled = true;
                    ButtonKImage = SimpleStaticHelpers.GetImage("VisitorsLogRed");
                    ButtonKIsEnabled = false;
                    ButtonLImage = SimpleStaticHelpers.GetImage("MyPlantScreenRed");
                    ButtonLIsEnabled = false;
                    ButtonMImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                    ButtonMIsEnabled = true;
                    ButtonNImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                    ButtonNIsEnabled = true;
                    ButtonOImage = SimpleStaticHelpers.GetImage("SiteAuditRed");
                    ButtonOIsEnabled = true;
                    ButtonPImage = SimpleStaticHelpers.GetImage("StrikeRed");
                    ButtonPIsEnabled =
                        true; //NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName == "Utility Damage");
                    ButtonQImage = SimpleStaticHelpers.GetImage("SiteAudit");
                    ButtonQIsEnabled = true;
                    ButtonRImage = SimpleStaticHelpers.GetImage("SiteAudit");
                    ButtonRIsEnabled = true;
                    ButtonSImage = "";
                    ButtonSIsEnabled = false;
                    ButtonTImage = "";
                    ButtonTIsEnabled = false;

                    ButtonUImage = "";
                    ButtonUIsEnabled = false;

                    ButtonVImage = "";
                    ButtonVIsEnabled = false;

                    ButtonWImage = "";
                    ButtonWIsEnabled = false;

                    ButtonXImage = "";
                    ButtonXIsEnabled = false;

                    ButtonYImage = "";
                    ButtonYIsEnabled = false;

                    ShowUploadButton = false;
                }
                else if (CurrentSelectedJob?.JobDate.Date >= yesterday)
                {
                    if ((bool)CurrentSelectedJob?.DailyChecksCompleted && AllplantAccepted)
                    {
                        ButtonAImage = SimpleStaticHelpers.GetImage("QuestionsGreen");

                        if (CurrentSelectedJob?.JobDate.Date <= yesterday)
                            ButtonAIsEnabled = false;
                        else
                            ButtonAIsEnabled = true;

                        ButtonBImage = LabourFiles
                            ? SimpleStaticHelpers.GetImage("TimesheetsGreen")
                            : SimpleStaticHelpers.GetImage("Timesheets");
                        ButtonBIsEnabled = true;

                        ButtonCImage = InputDiaryRecord
                            ? SimpleStaticHelpers.GetImage("InputDiaryGreen")
                            : NavigationalParameters.StartOfDayPictureTaken
                                ? SimpleStaticHelpers.GetImage("InputDiaryOrange")
                                : SimpleStaticHelpers.GetImage("InputDiary");
                        ButtonCIsEnabled = true;

                        ButtonDIsEnabled = true;

                        ButtonEImage = CrossedUtilities
                            ? SimpleStaticHelpers.GetImage("UtilitiesCrossedGreen")
                            : SimpleStaticHelpers.GetImage("UtilitiesCrossed");
                        ButtonEIsEnabled = true;

                        //ButtonFImage = EndPointsAvailable
                        //            ? SimpleStaticHelpers.GetImage("LateralConnection")
                        //            : SimpleStaticHelpers.GetImage("LateralConnectionRed");
                        ButtonFIsEnabled = true;
                        ButtonGIsEnabled = true;
                        ButtonHIsEnabled = true;
                        ButtonIIsEnabled = true;
                        ButtonJIsEnabled = true;

                        ButtonKImage = NavigationalParameters.VisitorsStillToBeLoggedOut
                            ? SimpleStaticHelpers.GetImage("VisitorsLogOrange")
                            : NavigationalParameters.VisitorCount > 0
                                ? SimpleStaticHelpers.GetImage("VisitorsLogGreen")
                                : SimpleStaticHelpers.GetImage("VisitorsLog");
                        ButtonKIsEnabled = true;

                        ButtonLIsEnabled = true;
                        ButtonMIsEnabled = true;
                        ButtonNIsEnabled = true;
                        ButtonOIsEnabled =
                            true; //NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName == "Auditor");
                        ButtonPIsEnabled =
                            true; //NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName == "Utility Damage");
                        ButtonQIsEnabled = true;
                        ButtonRIsEnabled = true;
                        ButtonSIsEnabled = true;
                        ButtonTIsEnabled = true;

                        ButtonUIsEnabled = true;
                        ButtonVIsEnabled = true;
                        ButtonWIsEnabled = true;
                        ButtonXIsEnabled = true;
                        ButtonYIsEnabled = true;

                        ShowUploadButton = true;
                    }
                    else
                    {
                        if (CurrentSelectedJob.JobDate.Date == yesterday)
                        {
                            ButtonAIsEnabled = CurrentSelectedJob.DailyChecksPosted;

                            if (ButtonAIsEnabled)
                            {
                                ButtonAImage = SimpleStaticHelpers.GetImage("QuestionsGreen");
                                ButtonAIsEnabled = true;
                            }
                            else
                            {
                                ButtonAImage = SimpleStaticHelpers.GetImage("QuestionsRed");
                                ButtonAIsEnabled = false;
                            }
                        }
                        else
                        {
                            ButtonAIsEnabled = CurrentSelectedJob.DailyChecksCompleted;

                            if (ButtonAIsEnabled)
                            {
                                ButtonAImage = SimpleStaticHelpers.GetImage("QuestionsGreen");
                                ButtonAIsEnabled = true;
                            }
                            else
                            {
                                ButtonAImage = SimpleStaticHelpers.GetImage("Questions");
                                ButtonAIsEnabled = true;
                            }
                        }


                        ButtonBImage = SimpleStaticHelpers.GetImage("TimesheetsRed");
                        ButtonBIsEnabled = false;
                        ButtonCImage = SimpleStaticHelpers.GetImage("InputDiaryRed");
                        ButtonCIsEnabled = false;
                        ButtonDImage = SimpleStaticHelpers.GetImage("InputMeasureRed");
                        ButtonDIsEnabled = false;
                        ButtonEImage = SimpleStaticHelpers.GetImage("UtilitiesCrossedRed");
                        ButtonEIsEnabled = false;
                        ButtonFImage = SimpleStaticHelpers.GetImage("LateralConnectionRed");
                        ButtonFIsEnabled = false;
                        ButtonGImage = SimpleStaticHelpers.GetImage("SiteAuditRed");
                        ButtonGIsEnabled = false;
                        ButtonHImage = SimpleStaticHelpers.GetImage("PermittoWork2Red");
                        ButtonHIsEnabled = false;
                        ButtonIImage = SimpleStaticHelpers.GetImage("JobPicturesRed");
                        ButtonIIsEnabled = false;
                        ButtonJImage = SimpleStaticHelpers.GetImage("ProjectSummaryV2");
                        ButtonJIsEnabled = true;
                        ButtonKImage = SimpleStaticHelpers.GetImage("VisitorsLogRed");
                        ButtonKIsEnabled = false;
                        ButtonLImage = SimpleStaticHelpers.GetImage("MyPlantScreen");
                        ButtonLIsEnabled = true;
                        ButtonMImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                        ButtonMIsEnabled = true;
                        ButtonNImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                        ButtonNIsEnabled = true;

                        ButtonOIsEnabled =
                            NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName == "Auditor");

                        if (ButtonOIsEnabled)
                            ButtonOImage = SimpleStaticHelpers.GetImage("SiteAudit");
                        else
                            ButtonOImage = SimpleStaticHelpers.GetImage("SiteAuditRed");


                        ButtonPIsEnabled =
                            true; //NavigationalParameters.LoggedInUser.MyGroups.Any(x => x.GroupName == "Utility Damage");

                        if (ButtonPIsEnabled)
                            ButtonPImage = SimpleStaticHelpers.GetImage("Strike");
                        else
                            ButtonPImage = SimpleStaticHelpers.GetImage("StrikeRed");

                        ButtonQImage = SimpleStaticHelpers.GetImage("SiteAudit");
                        ButtonQIsEnabled = true;
                        ButtonRImage = SimpleStaticHelpers.GetImage("SiteAudit");
                        ButtonRIsEnabled = true;
                        ButtonSImage = "";
                        ButtonSIsEnabled = false;
                        ButtonTImage = "";
                        ButtonTIsEnabled = false;
                        ButtonUImage = "";
                        ButtonUIsEnabled = false;
                        ButtonVImage = "";
                        ButtonVIsEnabled = false;
                        ButtonWImage = "";
                        ButtonWIsEnabled = false;
                        ButtonXImage = "";
                        ButtonXIsEnabled = false;
                        ButtonYImage = "";
                        ButtonYIsEnabled = false;
                    }
                }
                else
                {
                    ButtonAImage = SimpleStaticHelpers.GetImage("QuestionsRed");
                    ButtonAIsEnabled = false;
                    ButtonBImage = SimpleStaticHelpers.GetImage("TimesheetsRed");
                    ButtonBIsEnabled = false;
                    ButtonCImage = SimpleStaticHelpers.GetImage("InputDiaryRed");
                    ButtonCIsEnabled = false;
                    ButtonDImage = SimpleStaticHelpers.GetImage("InputMeasureRed");
                    ButtonDIsEnabled = false;
                    ButtonEImage = SimpleStaticHelpers.GetImage("UtilitiesCrossedRed");
                    ButtonEIsEnabled = false;
                    ButtonFImage = SimpleStaticHelpers.GetImage("LateralConnectionRed");
                    ButtonFIsEnabled = false;
                    ButtonGImage = SimpleStaticHelpers.GetImage("SiteAuditRed");
                    ButtonGIsEnabled = false;
                    ButtonHImage = SimpleStaticHelpers.GetImage("PermittoWork2Red");
                    ButtonHIsEnabled = false;
                    ButtonIImage = SimpleStaticHelpers.GetImage("JobPicturesRed");
                    ButtonIIsEnabled = false;
                    ButtonJImage = SimpleStaticHelpers.GetImage("ProjectSummaryV2Red");
                    ButtonJIsEnabled = false;
                    ButtonKImage = SimpleStaticHelpers.GetImage("VisitorsLogRed");
                    ButtonKIsEnabled = false;
                    ButtonLImage = SimpleStaticHelpers.GetImage("MyPlantScreenRed");
                    ButtonLIsEnabled = false;
                    ButtonMImage = SimpleStaticHelpers.GetImage("DocumentsListingRed");
                    ButtonMIsEnabled = false;
                    ButtonNImage = SimpleStaticHelpers.GetImage("DocumentsListingRed");
                    ButtonNIsEnabled = false;
                    ButtonOImage = SimpleStaticHelpers.GetImage("SiteAuditRed");
                    ButtonOIsEnabled = false;
                    ButtonPImage = SimpleStaticHelpers.GetImage("StrikeRed");
                    ButtonPIsEnabled = false;
                    ButtonQImage = SimpleStaticHelpers.GetImage("SiteAuditRed");
                    
                    ButtonQIsEnabled = false;
                    ButtonRImage = SimpleStaticHelpers.GetImage("SiteAudit");
                    ButtonRIsEnabled = false;
                    ButtonSImage = "";
                    ButtonSIsEnabled = false;
                    ButtonTImage = "";
                    ButtonTIsEnabled = false;
                    ButtonUImage = "";
                    ButtonUIsEnabled = false;
                    ButtonVImage = "";
                    ButtonVIsEnabled = false;
                    ButtonWImage = "";
                    ButtonWIsEnabled = false;
                    ButtonXImage = "";
                    ButtonXIsEnabled = false;
                    ButtonYImage = "";
                    ButtonYIsEnabled = false;
                }

                if (AllplantAccepted == false)
                    await Alert(
                        "Please ensure all plant has been accepted or rejected before continuing. If you feel unsure please contact the plant department with any queries",
                        "Plant To Accept", "Ok");
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
            {
                ShowSection1 = true;
                ShowSection2 = true;
                ShowSection3 = true;
                ShowSection4 = true;
                ShowSection5 = true;
                ShowSection6 = true;
                ShowSection7 = true;

                IsLoading = false;
                ShowUploadButton = true;
                var approvals = new SupervisorApprovalsPageViewModel();

                NavigationalParameters.ProjectJobs = _jobService.GetAllJobs()
                    .Where(x =>
                        x.SupervisorId ==
                        NavigationalParameters.LoggedInUser.FocusId &&
                        x.ProjectName == CurrentSelectedJob?.ProjectName)
                    .ToList();

                CurrentSelectedJob.JobGang =
                    _jobService.GetGang(CurrentSelectedJob);

                var dairyHasBeenSubmitted =
                    _userService.GetSupervisorLabour(NavigationalParameters.LoggedInUser.FocusId);

                // ShowUploadButton = _jobService.CheckUploadVisibility(CurrentSelectedJob);
                var itemsToApprove =
                    _jobService.GetTasks()
                        .Any(x => x.Category.ToLower() ==
                                  "approvals"); //approvals.GetItemsForApproval(CurrentSelectedJob);

                var approvalInProgress = approvals.ApprovalInProgress();


                if (itemsToApprove && approvalInProgress)
                {
                    //Items partially approved
                    SelectGangHidden = true;
                    ApprovalUploadRequired = false;
                }
                else if (!itemsToApprove && approvalInProgress)
                {
                    //Items approved, Upload Required
                    SelectGangHidden = false;
                    ApprovalUploadRequired = true;
                }
                else
                {
                    //Items not found, OK to navigate back to Menu
                    SelectGangHidden = false;
                    ApprovalUploadRequired = false;
                }

                ShowUploadButton = _jobService.CheckUploadVisibility(CurrentSelectedJob);

                AllOperativesToJob();

                ButtonAImage = SimpleStaticHelpers.GetImage("Gang");
                ButtonAImageLabel = "Select Gang";
                ShowButtonA = true;
                ButtonAIsEnabled = true;

                ButtonBImage = dairyHasBeenSubmitted != null && dairyHasBeenSubmitted.IsSubmited
                    ? SimpleStaticHelpers.GetImage("InputDiaryGreen")
                    : SimpleStaticHelpers.GetImage("InputDiary");
                ButtonBImageLabel = "Project diary";
                ShowButtonB = true;
                ButtonBIsEnabled = true;

                ButtonCImage =
                    SimpleStaticHelpers
                        .GetImage("EarlyWarningIcon"); //SimpleStaticHelpers.GetImage("EarlyWarningIcon"); 
                ButtonCImageLabel = "AS built"; // "Early warning";
                ShowButtonC = true;
                ButtonCIsEnabled = true;

                ButtonDImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                ButtonDImageLabel = "Site clear";
                ShowButtonD = true;
                ButtonDIsEnabled = true;

                ButtonEImage = SimpleStaticHelpers.GetImage("JobPictures");
                ButtonEImageLabel = "Project images";
                ShowButtonE = true;
                ButtonEIsEnabled = true;

                ButtonFImage = SimpleStaticHelpers.GetImage("ProjectSummaryV2");
                ButtonFImageLabel = "Summary";
                ShowButtonF = true;
                ButtonFIsEnabled = true;
                ButtonGImage = NavigationalParameters.VisitorsStillToBeLoggedOut
                    ? SimpleStaticHelpers.GetImage("VisitorsLogOrange")
                    : SimpleStaticHelpers.GetImage("VisitorsLog");
                ButtonGImageLabel = "Visitor log";
                ShowButtonG = true;
                ButtonGIsEnabled = true;
                ButtonHImage =
                    SimpleStaticHelpers.GetImage("PreSite"); // SimpleStaticHelpers.GetImage("MyPlantScreen");
                ButtonHImageLabel = "Pre Site"; // "Plant";
                ShowButtonH = true;
                ButtonHIsEnabled = true;
                ButtonIImage = ""; // SimpleStaticHelpers.GetImage("DocumentsListing");  
                ButtonIImageLabel = "";
                ShowButtonI = true;
                ButtonIIsEnabled = false;
                ButtonJImage = SimpleStaticHelpers.GetImage("DocumentsListing");
                ButtonJImageLabel = "Project docs";
                ShowButtonJ = true;
                ButtonJIsEnabled = true;
                ButtonKImage = SimpleStaticHelpers.GetImage("InputDiary");
                ButtonKImageLabel = "Site Inspection";
                ShowButtonK = true;
                ButtonKIsEnabled = true;
                ButtonLImage = SimpleStaticHelpers.GetImage("Strike");
                ButtonLImageLabel = "Event Management";
                ShowButtonL = true;
                ButtonLIsEnabled = true;
                ButtonMImage = "";
                ButtonMImageLabel = "";
                ShowButtonM = false;
                ButtonNImage = "";
                ButtonNImageLabel = "";
                ShowButtonN = false;
                ButtonOImage = "";
                ButtonOImageLabel = "";
                ShowButtonO = false;

                ButtonPImage = "";
                ButtonPImageLabel = "";
                ShowButtonP = false;

                ButtonQImage = "";
                ButtonQImageLabel = "";
                ShowButtonQ = false;

                ButtonRImage = "";
                ButtonRImageLabel = "";
                ShowButtonR = false;

                ButtonSImage = "";
                ButtonSImageLabel = "";
                ShowButtonS = false;

                ButtonTImage = "";
                ButtonTImageLabel = "";
                ShowButtonT = false;
            }
                break;
        }
    }


    [Time]
    private bool AreAllDailyChecksCompleted(List<SurveyQuestion> allTheChecks,
        List<Checks4TabletResponses> allTheResponses)
    {
        var returnValue = true;

        foreach (var checkItem in allTheChecks)
            if (checkItem != null)
            {
                var checkFlag = false;
                foreach (var responses in allTheResponses)
                    if (responses != null)
                        if (checkItem.QuestionId == responses.QuestionId)
                        {
                            checkFlag = true;
                            break;
                        }

                if (checkFlag == false)
                    return false;
            }

        return returnValue;
    }
}