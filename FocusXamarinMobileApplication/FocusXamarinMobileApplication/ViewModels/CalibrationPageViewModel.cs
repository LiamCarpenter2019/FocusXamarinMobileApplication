namespace FocusXamarinMobileApplication.ViewModels;

public class CalibrationPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    [Time]
    public CalibrationPageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        FibreTestList =
            new ObservableCollection<FibreTestResults>(
                _jobService.GetFibreTests(NavigationalParameters.CurrentSelectedJob));
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

    public ObservableCollection<FibreTestResults> _fibreTestList { get; set; }

    public ObservableCollection<FibreTestResults> FibreTestList
    {
        get => _fibreTestList;
        set
        {
            _fibreTestList = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<FibreTestResults> _FilteredFibreTestList { get; set; }

    public ObservableCollection<FibreTestResults> FilteredFibreTestList
    {
        get => _FilteredFibreTestList;
        set
        {
            _FilteredFibreTestList = value;
            OnPropertyChanged();
        }
    }

    public FibreTestResults _selectedCalibrationTest { get; set; }

    public FibreTestResults SelectedCalibrationTest
    {
        get => _selectedCalibrationTest;
        set
        {
            _selectedCalibrationTest = value;
            OnPropertyChanged();
        }
    }

    public string _searchText { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            SearchProjectWorks();
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        FilteredFibreTestList = FibreTestList;
        SelectedCalibrationTest = NavigationalParameters.SelectedCalibrationTest;
    });

    public RelayCommand SelectedCalibratrionTest => new(async () =>
    {
        NavigationalParameters.SelectedCalibrationTest = SelectedCalibrationTest;
    });

    public RelayCommand SaveCommand => new(async () =>
    {
        NavigationalParameters.SelectedCalibrationTest = SelectedCalibrationTest;

        _jobService.SaveFibreTest(NavigationalParameters.SelectedCalibrationTest);
    });

    public RelayCommand GoBack => new(async () => { await NavigateTo(ViewModelLocator.MenuSelectionPage); });

    [Time]
    public void SearchProjectWorks()
    {
        FilteredFibreTestList = new ObservableCollection<FibreTestResults>(string.IsNullOrEmpty(SearchText.ToLower())
            ? FibreTestList
            : FibreTestList.Where(x =>
                x.Identifier.ToLower().Contains(SearchText.ToLower()) ||
                x.Customer_Name.ToLower().Contains(SearchText.ToLower())));
    }
}