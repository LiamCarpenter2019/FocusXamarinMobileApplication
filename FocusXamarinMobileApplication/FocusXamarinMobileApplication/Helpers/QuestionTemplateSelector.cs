namespace FocusXamarinMobileApplication.Helpers;

public class QuestionTemplateSelector : DataTemplateSelector
{
    public DataTemplate YesNoNaQuestionTemplate { get; set; }
    public DataTemplate MultiSelectorQuestionTemplate { get; set; }
    public DataTemplate RatingQuestionsTemplate { get; set; }
    public DataTemplate LocationIdentityQuestionTemplate { get; set; }
    public DataTemplate FreeTextQuestionTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is YesNoNaQuestionViewModel)
            return YesNoNaQuestionTemplate;
        if (item is MultiQuestionViewModel)
            return MultiSelectorQuestionTemplate;
        if (item is LocationIdentityQuestionViewModel)
            return LocationIdentityQuestionTemplate;
        if (item is FreeTextQuestionViewModel)
            return FreeTextQuestionTemplate;
        return RatingQuestionsTemplate;
    }
}

public class LocationIdentityQuestionViewModel : SurveyQuestion
{
    public bool _isEnabled { get; set; }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = value;
    }

    public ImageSource CameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");
    public ImageSource Documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");
    public ImageSource GpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");
    public string AnswerGiven { get; set; }

    // Make base class for this logic, something like BindableBase
}

public class YesNoNaQuestionViewModel : SurveyQuestion
{
    public List<string> Options { get; set; }

    public Color _btnYesBgColour { get; set; }

    public Color BtnYesBgColour
    {
        get => _btnYesBgColour;
        set
        {
            _btnYesBgColour = value;
            OnPropertyChanged("BtnYesBgColour");
        }
    }

    public Color _btnNoBgColour { get; set; }

    public Color BtnNoBgColour
    {
        get => _btnNoBgColour;
        set
        {
            _btnNoBgColour = value;
            OnPropertyChanged("BtnNoBgColour");
        }
    }

    public Color _btnNaBgColour { get; set; }

    public Color BtnNaBgColour
    {
        get => _btnNaBgColour;
        set
        {
            _btnNaBgColour = value;
            OnPropertyChanged("BtnNaBgColour");
        }
    }

    public string BtnYesText { get; set; } = "YES";
    public string BtnNoText { get; set; } = "NO";
    public string BtnNaText { get; set; } = "N/A";
    public ImageSource CameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");
    public ImageSource Documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");
    public ImageSource GpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");
    public bool IsEnabled { get; set; }
    public bool ShowButtonA { get; set; } = true;
    public bool ShowButtonB { get; set; } = true;
    public bool ShowButtonC { get; set; } = true;
}

public class FreeTextQuestionViewModel : SurveyQuestion
{
    public List<string> Options { get; set; }

    public ImageSource CameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");
    public ImageSource Documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");
    public ImageSource GpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");
    public bool IsEnabled { get; set; }
    public bool ShowButtonA { get; set; } = true;
    public bool ShowButtonB { get; set; } = true;
    public bool ShowButtonC { get; set; } = true;
    public string PickerId { get; set; } = "";
    public int AnswerGiven { get; set; }

    public List<int> NumberList { get; set; } = new()
    {
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
        30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48
    };
}

public class MultiQuestionViewModel : SurveyQuestion
{
    private Color btnABgColour;
    private Color btnBBgColour;
    private Color btnCBgColour;
    private Color btnDBgColour;
    private Color btnEBgColour;
    private Color btnFBgColour;
    private Color btnGBgColour;
    private Color btnHBgColour;
    private Color btnIBgColour;
    private Color btnJBgColour;

    public List<string> Options { get; set; }
    public string BtnAText { get; set; }
    public string BtnBText { get; set; }
    public string BtnDText { get; set; }
    public string BtnCText { get; set; }
    public string BtnEText { get; set; }
    public string BtnFText { get; set; }
    public string BtnGText { get; set; }
    public string BtnHText { get; set; }

    public string BtnLText { get; set; }
    public string BtnKText { get; set; }
    public string BtnJText { get; set; }
    public string BtnIText { get; set; }


    public Color _btnLBgColour { get; set; }

    public Color BtnLBgColour
    {
        get => _btnLBgColour;
        set
        {
            _btnLBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color _btnKBgColour { get; set; }

    public Color BtnKBgColour
    {
        get => _btnKBgColour;
        set
        {
            _btnKBgColour = value;
            OnPropertyChanged();
        }
    }


    public Color BtnJBgColour
    {
        get => btnJBgColour;
        set
        {
            btnJBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnIBgColour
    {
        get => btnIBgColour;
        set
        {
            btnIBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnGBgColour
    {
        get => btnGBgColour;
        set
        {
            btnGBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnFBgColour
    {
        get => btnFBgColour;
        set
        {
            btnFBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnHBgColour
    {
        get => btnHBgColour;
        set
        {
            btnHBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnEBgColour
    {
        get => btnEBgColour;
        set
        {
            btnEBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnDBgColour
    {
        get => btnDBgColour;
        set
        {
            btnDBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnCBgColour
    {
        get => btnCBgColour;
        set
        {
            btnCBgColour = value;
            OnPropertyChanged("BtnBBgColour");
        }
    }

    public Color BtnBBgColour
    {
        get => btnBBgColour;
        set
        {
            btnBBgColour = value;
            OnPropertyChanged();
        }
    }

    public Color BtnABgColour
    {
        get => btnABgColour;
        set
        {
            btnABgColour = value;
            OnPropertyChanged();
        }
    }


    public bool BtnAHidden { get; set; }
    public bool BtnBHidden { get; set; }
    public bool BtnCHidden { get; set; }
    public bool BtnDHidden { get; set; }
    public bool BtnEHidden { get; set; }
    public bool BtnFHidden { get; set; }
    public bool BtnGHidden { get; set; }
    public bool BtnHHidden { get; set; }
    public bool BtnIHidden { get; set; }
    public bool BtnJHidden { get; set; }
    public bool BtnKHidden { get; set; }
    public bool BtnLHidden { get; set; }
    public ImageSource CameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");
    public ImageSource Documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");
    public ImageSource GpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");
    public bool IsEnabled { get; set; }

    // Make base class for this logic, something like BindableBase
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class RatingQuestionViewModel : SurveyQuestion

{
    private ImageSource rating1;
    private ImageSource rating10;
    private ImageSource rating2;
    private ImageSource rating3;
    private ImageSource rating4;
    private ImageSource rating5;
    private ImageSource rating6;
    private ImageSource rating7;
    private ImageSource rating8;
    private ImageSource rating9;
    private ImageSource ratingNA;

    public List<string> Options { get; set; }

    public ImageSource Rating1
    {
        get => rating1;
        set
        {
            rating1 = value;
            OnPropertyChanged("Rating1");
        }
    }

    public ImageSource Rating2
    {
        get => rating2;
        set
        {
            rating2 = value;
            OnPropertyChanged("Rating2");
        }
    }

    public ImageSource Rating3
    {
        get => rating3;
        set
        {
            rating3 = value;
            OnPropertyChanged("Rating3");
        }
    }

    public ImageSource Rating4
    {
        get => rating4;
        set
        {
            rating4 = value;
            OnPropertyChanged("Rating4");
        }
    }

    public ImageSource Rating5
    {
        get => rating5;
        set
        {
            rating5 = value;
            OnPropertyChanged("Rating5");
        }
    }

    public ImageSource Rating6
    {
        get => rating6;
        set
        {
            rating6 = value;
            OnPropertyChanged("Rating6");
        }
    }

    public ImageSource Rating7
    {
        get => rating7;
        set
        {
            rating7 = value;
            OnPropertyChanged("Rating7");
        }
    }

    public ImageSource Rating8
    {
        get => rating8;
        set
        {
            rating8 = value;
            OnPropertyChanged("Rating8");
        }
    }

    public ImageSource Rating9
    {
        get => rating9;
        set
        {
            rating9 = value;
            OnPropertyChanged("Rating9");
        }
    }

    public ImageSource Rating10
    {
        get => rating10;
        set
        {
            rating10 = value;
            OnPropertyChanged("Rating10");
        }
    }

    public ImageSource RatingNA
    {
        get => ratingNA;
        set
        {
            ratingNA = value;
            OnPropertyChanged("RatingNA");
        }
    }

    public ImageSource CameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");
    public ImageSource Documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");
    public ImageSource GpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");
    public bool IsEnabled { get; set; }
}