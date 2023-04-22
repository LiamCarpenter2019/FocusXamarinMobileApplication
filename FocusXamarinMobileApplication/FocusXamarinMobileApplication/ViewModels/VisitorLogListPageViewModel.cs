using System.Collections.ObjectModel;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class VisitorLogListPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assigmentService;
    private readonly Jobs _jobService;
    private RelayCommand<string> _addVisitorRelayCommand;
    private RelayCommand<string> _logOutRelayCommand;

    public VisitorLogListPageViewModel()
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");
        _assigmentService = new Assignments();
        _jobService = new Jobs();

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;

        //  VisitorList = GetVisitors();
    }

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

    public ObservableCollection<VisitorLog> _visitorList { get; set; }

    public ObservableCollection<VisitorLog> VisitorList
    {
        get => _visitorList;
        set
        {
            _visitorList = value;
            OnPropertyChanged();
        }
    }

    public VisitorLog _SelectedVisitor { get; set; }

    public VisitorLog SelectedVisitor
    {
        get => _SelectedVisitor;
        set
        {
            _SelectedVisitor = value;
            OnPropertyChanged();
        }
    }


    public RelayCommand PageLoaded => new(() => { VisitorList = GetVisitors(); });

    public RelayCommand<string> RegisterVisitorCommand
    {
        get
        {
            return _addVisitorRelayCommand ??= new RelayCommand<string>(async e =>
            {
                NavigationalParameters.NewVisitor = new VisitorLog();
                //NavigationalParameters.NavigationParameter = e;
                await NavigateTo(ViewModelLocator.VisitorLogPage);
            });
        }
    }

    public RelayCommand LogOutCommand => new(async () =>
    {
        if (!NavigationalParameters.SelectedVisitor.OffSite)
            await NavigateTo(ViewModelLocator.VisitorLogOutPage);
        else
            await Alert("This user has already been logged out and the details cannot be changed!", "Sorry!");
    });

    public RelayCommand BackCommand => new(async () => { await NavigateTo(ViewModelLocator.MenuSelectionPage); });

    public ObservableCollection<VisitorLog> GetVisitors()
    {
        return new ObservableCollection<VisitorLog>(_jobService.GetVisitors(NavigationalParameters.CurrentSelectedJob));
    }
}