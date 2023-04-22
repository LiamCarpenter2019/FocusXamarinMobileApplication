#region

using Event = FocusXamarinMobileApplication.Models.Event;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SelectInvestigationPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private ObservableCollection<Event> _eventList;

    public Jobs _jobService;

    private Event _selectedEvent;

    public SelectInvestigationPageViewModel()
    {
        _jobService = new Jobs();
    }

    public ObservableCollection<InvestigateDamages> DamagesToInvestigate { get; set; }


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

    public ObservableCollection<Event> EventList
    {
        get => _eventList;
        set
        {
            _eventList = value;
            OnPropertyChanged();
        }
    }

    public Event SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        // ProjectName = NavigationalParameters.InvestigateDamage?.ProjectName;

        ProjectDate =
            DateTime.Now.Date.ToString("dd/MM/yyyy");

        EventList = _jobService.GetEvent();

        // DamagesToInvestigate = new ObservableCollection<InvestigateDamages>();

        //DamagesToInvestigate = new ObservableCollection<InvestigateDamages>(_jobService.GetInvestigatedDamages()
        //    .OrderByDescending(damage => damage?.DamageId));

        //OnPropertyChanged("DamagesToInvestigate");
    });

    public RelayCommand NavToInvestigation => new(async () =>
    {
        NavigationalParameters.EventManagement = SelectedEvent;

        await NavigateTo(ViewModelLocator.SelectDamageDetailsPage);
    });

    public RelayCommand GoBack => new(async () => { await NavigateTo(ViewModelLocator.SupervisorProjectPage); });
}