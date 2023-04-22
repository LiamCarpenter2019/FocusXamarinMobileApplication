#region

using System.Collections.Generic;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Event = FocusXamarinMobileApplication.Models.Event;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class EventManagementSelectionPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;


    private bool _isLoading;

    public EventManagementSelectionPageViewModel()
    {
        _jobService = new Jobs();
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        Title = "Event Management";
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
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

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
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

    public RelayCommand BackCommand => new(async () => { NavigateBack(); });

    public RelayCommand UtilityDamageCommand => new(async () =>
    {
        NavigationalParameters.EventManagementTypeList = new List<EventManagementType>();
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.UTILITYDAMAGE;
        NavigationalParameters.EventManagement = new Event { Category = "UTILITYDAMAGE" };
        NavigationalParameters.RegisterUtilityDamage = new RegisterUtilityDamage
        {
            PublicUtilityDamageGuid = NavigationalParameters.EventManagement.Identifier
        };

        NavigationalParameters.EventManagementTypeList = _jobService.GetEventManagementTypes("UTILITYDAMAGE");

        await NavigateTo(ViewModelLocator.RegisterNewEventPage);
    });

    public RelayCommand AccidentCommand => new(async () =>
    {
        NavigationalParameters.EventManagementTypeList = new List<EventManagementType>();

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.ACCIDENT;
        NavigationalParameters.EventManagement = new Event { Category = "ACCIDENT,INCIDENT" };

        NavigationalParameters.RegisterUtilityDamage = new RegisterUtilityDamage
        {
            PublicUtilityDamageGuid = NavigationalParameters.EventManagement.Identifier
        };

        NavigationalParameters.EventManagementTypeList = _jobService.GetEventManagementTypes("ACCIDENT,INCIDENT");

        await NavigateTo(ViewModelLocator.RegisterNewEventPage);
    });

    public RelayCommand IncidentCommand => new(async () =>
    {
        NavigationalParameters.EventManagementTypeList = new List<EventManagementType>();

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.INCIDENT;
        NavigationalParameters.EventManagement = new Event { Category = "ACCIDENT,INCIDENT" };
        NavigationalParameters.RegisterUtilityDamage = new RegisterUtilityDamage
        {
            PublicUtilityDamageGuid = NavigationalParameters.EventManagement.Identifier
        };

        NavigationalParameters.EventManagementTypeList = _jobService.GetEventManagementTypes("ACCIDENT,INCIDENT");

        await NavigateTo(ViewModelLocator.RegisterNewEventPage);
    });

    public RelayCommand NearMissCommand => new(async () =>
    {
        NavigationalParameters.EventManagementTypeList = new List<EventManagementType>();

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.NEARMISS;
        NavigationalParameters.EventManagement = new Event { Category = "NEARMISS" };
        NavigationalParameters.RegisterUtilityDamage = new RegisterUtilityDamage
        {
            PublicUtilityDamageGuid = NavigationalParameters.EventManagement.Identifier
        };

        NavigationalParameters.EventManagementTypeList = _jobService.GetEventManagementTypes("NEARMISS");

        await NavigateTo(ViewModelLocator.RegisterNewEventPage);
    });
}