using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class UtilityDamagesNavPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private bool _isLoading;

    public UtilityDamagesNavPageViewModel()
    {
        _jobService = new Jobs();
        SavedDamageCount = _jobService.GetRegisterUtilityDamages().Count;
    }


    public int SavedDamageCount { get; set; }
    public bool ShowSubmissionButton => SavedDamageCount > 0 && IsLoading == false;

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

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
            OnPropertyChanged("ShowSubmissionButton");
        }
    }

    public RelayCommand RegisterDamage => new(async () => { await NavigateTo(ViewModelLocator.RegisterNewEventPage); });

    public RelayCommand InvestigateDamage =>
        new(async () => { await NavigateTo(ViewModelLocator.SelectInvestigationPage); });

    public RelayCommand PageLoaded => new(() =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
    });

    public RelayCommand Refresh => new(() =>
    {
        SavedDamageCount = _jobService.GetRegisterUtilityDamages().Count;
        OnPropertyChanged("SavedDamageCount");
        OnPropertyChanged("ShowSubmissionButton");
    });

    public RelayCommand GoBack => new(async () => { NavigateBack(); });
}