namespace FocusXamarinMobileApplication.ViewModels;

public class BlockagePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    [Time]
    public BlockagePageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
    }

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


    public ObservableCollection<Blockage> _blockages { get; set; }

    public ObservableCollection<Blockage> Blockages
    {
        get => _blockages;
        set
        {
            _blockages = value;
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

    public Assignments _assigmentService { get; }
    public Jobs _jobService { get; }
    public Users _userService { get; }

    public RelayCommand RefreshCommand => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        Title = $"{NavigationalParameters.SelectedAsset.StreetName} Blockages";
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;

        NavigationalParameters.BlockageItem = SelectedBlockage = null;
        Blockages = new ObservableCollection<Blockage>(
            _jobService.GetBlockages(NavigationalParameters.SelectedAsset?.ClaimId.ToString()));
    });

    public RelayCommand CloseBlockageCommand => new(async () =>
    {
        try
        {
            NavigationalParameters.BlockageItem = SelectedBlockage;
            await NavigateTo(ViewModelLocator.BlockagesInputPage);
        }
        catch (Exception ex)
        {
            await Alert("Error", "There is an erro within the selection please contact support", "Ok");
        }
    });

    public RelayCommand CreateNewBlockage => new(async () =>
    {
        NavigationalParameters.BlockageItem = new Blockage
        {
            QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
            EndPointId = 0,
            LengthFromCab = "",
            Reason = "",
            LengthFromToby = "",
            Comments = "",
            RemoteTableId = 0,
            Cleared = false,
            ClearenceComments = "",
            EndTime = null,
            StartTime = null,
            Id = 0,
            ClearedBy = 0,
            LocationGps = "",
            RegisteredBy = NavigationalParameters.LoggedInUser.FocusId,
            CableRunId = NavigationalParameters.SelectedAsset?.ClaimId.ToString()
        };

        await NavigateTo(ViewModelLocator.BlockagesInputPage);
    });

    public RelayCommand BackCommand => new(async () => { await NavigateTo(ViewModelLocator.SelectEndPointPage); });
}