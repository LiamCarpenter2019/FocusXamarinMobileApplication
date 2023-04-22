namespace FocusXamarinMobileApplication.ViewModels;

public class AddWitnessPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    //private readonly ISignatureService signatureService;

    private readonly Jobs _jobService = new();
    private Witness _newWitness;

    private TimeSpan _witnessTime = DateTime.Now.TimeOfDay;
    //public event PropertyChangedEventHandler PropertyChanged;

    [Time]
    public AddWitnessPageViewModel()
    {
        NewWitness = NavigationalParameters.Witness ?? new Witness();
        OnPropertyChanged("Witness");
    }

    public DamageToInvestigate DamageToInvestigate { get; set; }
    public RegisterUtilityDamage RegisterUtilityDamage { get; private set; }
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

    private DateTime _witnessDate { get; set; } = DateTime.Now.Date;

    public Witness NewWitness
    {
        get => _newWitness;
        set
        {
            _newWitness = value;
            OnPropertyChanged();
        }
    }

    public DateTime WitnessDate
    {
        get => _witnessDate;
        set
        {
            _witnessDate = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan WitnessTime
    {
        get => _witnessTime;
        set
        {
            _witnessTime = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        DamageToInvestigate = NavigationalParameters.EventManagement.Investigations?.FirstOrDefault()?.DamageDetails
            ?.FirstOrDefault();

        RegisterUtilityDamage = NavigationalParameters.RegisterUtilityDamage;
    });


    public RelayCommand GoBack => new(async () => { NavigateBack(); });

    public RelayCommand AddWitnessCommand => new(async () =>
    {
        if (string.IsNullOrWhiteSpace(NewWitness?.Name))
        {
            await Alert("Please Enter an Name", "Error");
        }
        else if (string.IsNullOrWhiteSpace(NewWitness?.Statement))
        {
            await Alert("Please Enter an Statement", "Error");
        }
        //else if (string.IsNullOrWhiteSpace(NewWitness?.Email))
        //{
        //    await Alert("Please Enter an Email Address", "Error");
        //}
        else
        {
            // NewWitness.PublicUtilityDamageGuid = NavigationalParameters.CurrentDamageDetail.PublicUtilityDamageGuid;
            if (NewWitness != null)
            {
                NewWitness.PublicUtilityDamageId =
                    DamageToInvestigate?.DamageId.ToString() ?? RegisterUtilityDamage.DamageId;
                NewWitness.InvestigationId = DamageToInvestigate?.DamageInvestigated?.RemoteTabledId.ToString();

                await _jobService.AddWitness(NewWitness);
            }

            NavigateBack();
        }
    });

    public RelayCommand DeleteWitness => new(async () =>
    {
        await _jobService.DeleteWitness(NewWitness);
        NavigateBack();
    });
}