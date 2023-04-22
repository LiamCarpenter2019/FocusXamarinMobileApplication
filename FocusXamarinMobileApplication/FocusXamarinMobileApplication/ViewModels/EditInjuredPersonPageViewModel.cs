namespace FocusXamarinMobileApplication.ViewModels;

public class EditInjuredPersonPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;

    public EditInjuredPersonPageViewModel()
    {
        _jobService = new Jobs();
    }

    public RegisterUtilityDamage RegisterUtilityDamage { get; set; }
    public bool DeleteVisible { get; set; }

    public InjuredPerson _person { get; set; }

    public InjuredPerson Person
    {
        get => _person;
        set
        {
            _person = value;
            OnPropertyChanged();
        }
    }

    public DamageToInvestigate DamageToInvestigate { get; set; }

    public RelayCommand PageLoaded => new(async () =>
    {
        DeleteVisible = false;

        Person = new InjuredPerson();

        DamageToInvestigate = NavigationalParameters.EventManagement.Investigations?.FirstOrDefault()?.DamageDetails
            ?.FirstOrDefault();

        RegisterUtilityDamage = NavigationalParameters.RegisterUtilityDamage;
    });

    public RelayCommand AddInjured => new(async () =>
    {
        if (ValidateEntry())
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.ACCIDENT
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.UTILITYDAMAGE
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.NEARMISS
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.INCIDENT)
            {
                //Person.PublicUtilityDamageId = Convert.ToInt32(registerUtilityDamage?.DamageId);
                Person.PublicUtilityDamageGuid = RegisterUtilityDamage.PublicUtilityDamageGuid;

                Person.InvestigationId = "0";
                // Person.PublicUtilityDamageGuid = DamageToInvestigate.DamageGuid;
            }
            else
            {
                Person.PublicUtilityDamageId = DamageToInvestigate.DamageId;
                //Person.PublicUtilityDamageGuid = NavigationalParameters.CurrentDamageDetail.PublicUtilityDamageGuid;
                Person.InvestigationId = DamageToInvestigate?.InvestigationId ?? "0";

                Person.PublicUtilityDamageGuid = DamageToInvestigate.DamageGuid;
            }

            _jobService.AddInjuredPerson(Person);

            NavigateBack();
            //await NavigateTo(ViewModelLocator.RegisterNewDamagePage);
        }
        else
        {
            await Alert(
                "Please enter at least the name of the injured person and the details of the injury before continuing!",
                "Not complete!");
        }
    });

    //public RelayCommand AddInvestigationInjured => new  RelayCommand(async () =>
    //{
    //    if (ValidateEntry())
    //    {

    //        _jobService.AddInjuredPerson(Person);
    //        OnPropertyChanged("InjuredPerson");
    //        NavigateBack();
    //    }
    //    else
    //    {
    //        await Alert("Please enter a name before saving", "Not complete!");
    //    }
    //});

    public RelayCommand DeletePerson => new(async () =>
    {
        //lock (App.Locker)
        //{
        _jobService.DeleteInjuredPerson(Person);
        //}
        NavigateBack();
    });

    public RelayCommand Cancel => new(async () => { NavigateBack(); });

    private bool ValidateEntry()
    {
        if (string.IsNullOrEmpty(Person?.InjuredName)) return false;
        //   if (string.IsNullOrEmpty(Person?.Position)) return false;
        //  if (string.IsNullOrEmpty(Person?.ContactNumber)) return false;
        if (string.IsNullOrEmpty(Person?.Injury)) return false;
        //       if (string.IsNullOrEmpty(Person?.NextOfKinName)) return false;
        //     if (string.IsNullOrEmpty(Person?.NextOfKinNumber)) return false;
        return true;
    }
}