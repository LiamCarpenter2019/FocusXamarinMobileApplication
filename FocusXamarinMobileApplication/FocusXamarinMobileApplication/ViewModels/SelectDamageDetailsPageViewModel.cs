namespace FocusXamarinMobileApplication.ViewModels;

public class SelectDamageDetailsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public DamageToInvestigate _damageToInvestigate;

    public List<DamageToInvestigate> _investigationDetailList;

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

    public List<DamageToInvestigate> InvestigationDetailList
    {
        get => _investigationDetailList;
        set
        {
            _investigationDetailList = value;
            OnPropertyChanged();
        }
    }

    public DamageToInvestigate DamageToInvestigate
    {
        get => _damageToInvestigate;
        set
        {
            _damageToInvestigate = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.InvestigateDamage.ProjectName;

        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

        InvestigationDetailList = NavigationalParameters.InvestigateDamage.DamageDetails;

        OnPropertyChanged("DamageToInvestigate");
    });

    public RelayCommand NavToDamage => new(async () =>
    {
        NavigationalParameters.UtilityDamage = DamageToInvestigate;

        await NavigateTo(ViewModelLocator.InvestigateDamagePage);
    });

    public RelayCommand RefreshData => new(() => { });

    public RelayCommand GoBack => new(async () => await NavigateTo(ViewModelLocator.SelectInvestigationPage));
}