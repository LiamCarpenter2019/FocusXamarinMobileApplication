namespace FocusXamarinMobileApplication.ViewModels;

public class CommercialListPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    [Time]
    public CommercialListPageViewModel()
    {
        _assigmentService = new Assignments();

        _jobService = new Jobs();

        _userService = new Users();

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy") ??
                      DateTime.Now.ToString("dd/MM/yyyy");

        Title = "Commercial";
    }

    public Assignments _assigmentService { get; }
    public Jobs _jobService { get; }
    public Users _userService { get; }

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

    public RelayCommand PageLoaded => new(async () =>
    {
        ShowSection1 = true;

        ShowSection2 = true;

        ShowSection3 = true;

        ShowSection4 = true;

        Title = "Commercial";
    });

    public RelayCommand GoBackCommand => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

            await NavigateTo(ViewModelLocator.SupervisorProjectPage);
        }
        else
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

            await NavigateTo(ViewModelLocator.MainListPage);
        }
    });

    public RelayCommand OrderBookCommand => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.ORDERBOOK;


        await NavigateTo(ViewModelLocator.OrderListPage);
    });

    public RelayCommand Back => new(async () => { await NavigateTo(ViewModelLocator.MenuSelectionPage); });
}