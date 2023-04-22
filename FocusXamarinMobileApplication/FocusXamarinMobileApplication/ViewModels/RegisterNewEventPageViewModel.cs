using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class RegisterNewEventPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;

    // private bool _thirdPartyDamage { get; set; }
    public readonly PhotoSelectionPageViewModel _psvm;

    public string _header;

    private DateTime? _incidentDate = DateTime.Now.Date;

    private TimeSpan? _incidentTime = DateTime.Now.TimeOfDay;

    public bool _isLoading;

    public bool _showLoadingPage;

    public string _title;

    public RegisterNewEventPageViewModel()
    {
        _jobService = new Jobs();

        UtilityCompanies = new ObservableCollection<UtilityCompany>(_jobService.GetUtilityCompany());

        _psvm = new PhotoSelectionPageViewModel();

        //DamagePhotos = new List<Pictures4Tablet>();
    }

    public ObservableCollection<string> yesno { get; set; } = new() { "", "YES", "NO" };
    public Pictures _pictureService { get; set; }

    private UtilityCompany _utilityCompanySelection { get; set; }

    public UtilityCompany UtilityCompanySelection
    {
        get => _utilityCompanySelection;
        set
        {
            _utilityCompanySelection = value;

            OnPropertyChanged();
        }
    }

    private PublicUtilityDamageQuestion _selectedDamageType { get; set; }

    public PublicUtilityDamageQuestion SelectedDamageType
    {
        get => _selectedDamageType;
        set
        {
            _selectedDamageType = value;

            OnPropertyChanged();
        }
    }

    public JobData4Tablet SelectedProject { get; set; }

    //public int SelectedDamageLocation { get; set; } = 0;

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


    public bool SupervisorApp => NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR;

    public string ReportedBy { get; set; } = NavigationalParameters.LoggedInUser.FullName;

    public string ReportedByCompany { get; set; } =
        string.IsNullOrEmpty(NavigationalParameters.LoggedInUser.CompanyName)
            ? "SCD"
            : NavigationalParameters.LoggedInUser.CompanyName;

    public string ContractorName { get; set; } = "N/A";
    public string ContractorNumber { get; set; } = "N/A";
    public string ContractorDamageLocation { get; set; } = "N/A";
    public string UtilityReference { get; set; }
    public string UtilityContactName { get; set; }

    private bool _isUtilityDamage { get; set; }
    private bool _showInjuryTable { get; set; }
    private bool _showInjury { get; set; }
    private bool _showGang { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool ShowLoadingPage
    {
        get => _showLoadingPage;
        set
        {
            _showLoadingPage = value;
            OnPropertyChanged();
        }
    }

    public string _notes { get; set; } = "";

    public string Notes
    {
        get => _notes;
        set
        {
            _notes = value;
            NavigationalParameters.RegisterUtilityDamage.TxtNotes = Notes;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string Header
    {
        get => _header;
        set
        {
            _header = value;
            OnPropertyChanged();
        }
    }

    public bool ShowGang
    {
        get => _showGang;
        set
        {
            _showGang = value;
            OnPropertyChanged();
        }
    }

    public bool ShowInjury
    {
        get => _showInjury;
        set
        {
            _showInjury = value;
            OnPropertyChanged();
        }
    }

    public bool ShowInjuryTable
    {
        get => _showInjuryTable;
        set
        {
            _showInjuryTable = value;
            OnPropertyChanged();
        }
    }

    public bool IsUtilityDamage
    {
        get => _isUtilityDamage;
        set
        {
            _isUtilityDamage = value;
            OnPropertyChanged();
        }
    }

    public bool _showSelectedSeverity { get; set; }

    public bool ShowSelectedSeverity
    {
        get => _showSelectedSeverity;
        set
        {
            _showSelectedSeverity = value;
            OnPropertyChanged();
        }
    }

    public bool _isAccident { get; set; }

    public bool IsAccident
    {
        get => _isAccident;
        set
        {
            _isAccident = value;
            OnPropertyChanged();
        }
    }

    public Color _selectedSeverityBackgroundColour { get; set; } = Color.FromHex("#1A142E");

    public Color SelectedSeverityBackgroundColour
    {
        get => _selectedSeverityBackgroundColour;
        set
        {
            _selectedSeverityBackgroundColour = value;
            OnPropertyChanged();
        }
    }

    public DateTime? IncidentDate
    {
        get => _incidentDate;
        set
        {
            _incidentDate = value;
            OnPropertyChanged("IncidentPickerTextColor");
        }
    }

    public TimeSpan? IncidentTime
    {
        get => _incidentTime;
        set
        {
            _incidentTime = value;

            NavigationalParameters.RegisterUtilityDamage.IncidentDateTime = (DateTime)(IncidentDate + IncidentTime);
            OnPropertyChanged("IncidentTimePickerTextColor");
        }
    }

    public Color IncidentPickerTextColor => IncidentDate == null ? Color.Transparent : Color.Black;
    public Color IncidentTimePickerTextColor => IncidentTime == null ? Color.Transparent : Color.Black;

    public IGrouping<string, EventManagementType> _selectedEventManagementType { get; set; }

    public IGrouping<string, EventManagementType> SelectedEventManagementType
    {
        get => _selectedEventManagementType;
        set
        {
            _selectedEventManagementType = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<EventManagementType> _eventTypeList { get; set; } = new();

    public ObservableCollection<EventManagementType> EventTypeList
    {
        get => _eventTypeList;
        set
        {
            _eventTypeList = value;

            OnPropertyChanged();
        }
    }

    public EventManagementType _selectedEventTypeItem { get; set; }

    public EventManagementType SelectedEventTypeItem
    {
        get => _selectedEventTypeItem;
        set
        {
            _selectedEventTypeItem = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<PublicUtilityDamageQuestion> _damageTypesListItems { get; set; }

    public ObservableCollection<PublicUtilityDamageQuestion> DamageTypesListItems
    {
        get => _damageTypesListItems;
        set
        {
            _damageTypesListItems = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<PublicUtilityDamageQuestion> _damageTypes { get; set; }

    public ObservableCollection<PublicUtilityDamageQuestion> DamageTypes
    {
        get => _damageTypes;
        set
        {
            _damageTypes = value;

            OnPropertyChanged();
        }
    }

    private string _operativeNames { get; set; } = "";

    public string OperativeNames
    {
        get => _operativeNames;
        set
        {
            _operativeNames = value;

            OnPropertyChanged();
        }
    }

    private string _thirdPartyDamage { get; set; } = "NO";

    public string ThirdPartyDamage
    {
        get => _thirdPartyDamage;
        set
        {
            _thirdPartyDamage = value;

            OnPropertyChanged();
        }
    }

    private string _selectedDamageLocation { get; set; }

    public string SelectedDamageLocation
    {
        get => _selectedDamageLocation;
        set
        {
            _selectedDamageLocation = value;

            OnPropertyChanged();
        }
    }

    public string _requiresHospital { get; set; } = "NO";

    public string RequiresHospital
    {
        get => _requiresHospital;
        set
        {
            _requiresHospital = value;

            OnPropertyChanged();
        }
    }

    //public List<Pictures4Tablet> DamagePhotos { get; set; }


    public ObservableCollection<string> DamageLocations { get; set; } =
        new() { "", "Verge", "Footway", "Carriageway", "Unmade" };

    public ObservableCollection<InjuredPerson> _injuredPeople { get; set; } = new();

    public ObservableCollection<InjuredPerson> InjuredPeople
    {
        get => _injuredPeople;
        set
        {
            _injuredPeople = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<UtilityCompany> UtilityCompanies { get; set; }

    public ImageSource PhotoImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource Person { get; set; } = SimpleStaticHelpers.GetImage("AddPeople");

    public ImageSource GpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");

    public RelayCommand RefreshData => new(() =>
    {
        SelectedProject = NavigationalParameters.CurrentSelectedJob;

        SelectedProject.GangLeaderName = NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLabourFiles
            .FirstOrDefault(x => x.FocusId == NavigationalParameters.CurrentSelectedJob?.GangLeaderId)?.FullName;

        IsLoading = false;

        ShowLoadingPage = true;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.UTILITYDAMAGE)
            Title = "Utility damage";
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INCIDENT ||
                 NavigationalParameters.AppMode == NavigationalParameters.AppModes.ACCIDENT)
            Title = "Accident / Incident";
        else
            Title = "Near Miss";

        var injured = GetInjuredPersonel();

        if (injured.Any()) InjuredPeople = new ObservableCollection<InjuredPerson>(injured);

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.UTILITYDAMAGE)
        {
            Header = "Utility Damage";

            IsUtilityDamage = true;

            ShowInjuryTable = InjuredPeople != null && InjuredPeople.Any();

            ShowInjury = true;

            IsAccident = true;

            ShowGang = true;
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.ACCIDENT
                 || NavigationalParameters.AppMode == NavigationalParameters.AppModes.INCIDENT)
        {
            IsUtilityDamage = false;

            IsAccident = true;

            ShowInjuryTable = InjuredPeople != null && InjuredPeople.Any();

            if (ShowInjuryTable)
            {
                Header = "Accident";

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.ACCIDENT;

                NavigationalParameters.EventManagement.EventType = SelectedEventTypeItem?.RemoteTableId;
            }
            else
            {
                Header = "Incident";

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.INCIDENT;
            }

            ShowInjury = true;

            ShowGang = true;
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.NEARMISS)
        {
            Header = "Near miss";
            IsUtilityDamage = false;
            ShowInjuryTable = false;
            ShowInjury = false;
            ShowGang = false;
            IsAccident = false;
        }
        else
        {
            IsUtilityDamage = false;
            ShowInjuryTable = false;
            ShowInjury = false;
            ShowGang = false;
            IsAccident = false;
        }

        var opNames = new List<string>();

        OperativeNames = "";

        OperativeNames = $"{SelectedProject.GangLeaderName}, ";

        opNames.Add(SelectedProject.GangLeaderName);

        foreach (var Person in NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles)
            if (opNames.All(x => x != Person.FullName))
            {
                opNames.Add(Person.FullName);

                OperativeNames += $"{Person.FullName}, ";
            }

        EventTypeList = new ObservableCollection<EventManagementType>(NavigationalParameters.EventManagementTypeList);

        var damageTypes = _jobService.GetUtilityDamageQuestions(NavigationalParameters.EventManagement.Category)
            ?.ToList();

        DamageTypes = new ObservableCollection<PublicUtilityDamageQuestion>(damageTypes);

        Notes = $"{NavigationalParameters.RegisterUtilityDamage?.TxtNotes}";

        UtilityCompanySelection = UtilityCompanies.FirstOrDefault(x =>
            x.CompanyId == NavigationalParameters.RegisterUtilityDamage.TxtUtilityId.ToString());

        if (NavigationalParameters.EventManagement.Severity != "" && DamageTypes != null)
        {
            SelectedDamageType = DamageTypes?.FirstOrDefault(x =>
                x.RemoteTableId.ToString() == NavigationalParameters.EventManagement?.Severity);

            if (SelectedDamageType != null)
            {
                if (SelectedDamageType?.DamageColour == "Red")
                    SelectedSeverityBackgroundColour = Color.Red;
                else if (SelectedDamageType?.DamageColour == "Amber")
                    SelectedSeverityBackgroundColour = Color.Orange;
                else if (SelectedDamageType?.DamageColour == "Green")
                    SelectedSeverityBackgroundColour = Color.Green;
                else
                    SelectedSeverityBackgroundColour = Color.FromHex("#1A142E");
            }
        }

        IncidentTime = NavigationalParameters.RegisterUtilityDamage.IncidentDateTime.TimeOfDay;

        SelectedDamageLocation = NavigationalParameters.RegisterUtilityDamage.DamageLocation;

        if (NavigationalParameters.EventManagement.ThirdPartyDamage)
            ThirdPartyDamage = "YES";
        else
            ThirdPartyDamage = "NO";

        if (NavigationalParameters.EventManagement.HospitalVisitRequired)
            RequiresHospital = "YES";
        else
            RequiresHospital = "NO";


        if (NavigationalParameters.CurrentSelectedJob != null)
        {
            SelectedProject = NavigationalParameters.CurrentSelectedJob;

            OnPropertyChanged("SelectedProject");
        }

        if (NavigationalParameters.EventManagement?.EventType > 0)
            SelectedEventTypeItem =
                EventTypeList.FirstOrDefault(x => x.RemoteTableId == NavigationalParameters.EventManagement.EventType);

        if (NavigationalParameters.RegisterUtilityDamage?.DamageTypeId > 0)
            SelectedDamageType = DamageTypesListItems.FirstOrDefault(x =>
                x.RemoteTableId == NavigationalParameters.RegisterUtilityDamage.DamageTypeId);


        NavigationalParameters.EventManagement.Category = NavigationalParameters.AppMode.ToString();
    });

    public RelayCommand EventPicker_SelectedIndexChanged => new(async () =>
    {
        SelectedSeverityBackgroundColour = Color.FromHex("#1A142E");

        if (EventTypeList.Any())
            if (SelectedEventTypeItem != null)
            {
                NavigationalParameters.EventManagement.EventType = SelectedEventTypeItem.RemoteTableId;


                if (SelectedEventTypeItem.Category.ToUpper() == "UTILITYDAMAGE")
                {
                    var dmageTypes = _jobService
                        .GetUtilityDamageQuestions(NavigationalParameters.EventManagement.Category)?.Where(x =>
                            x.DamageGroup.ToLower().Contains(SelectedEventTypeItem?.Title.ToLower())).ToList();

                    DamageTypesListItems = new ObservableCollection<PublicUtilityDamageQuestion>(dmageTypes);
                }
                else
                {
                    var dmageTypes = _jobService
                        .GetUtilityDamageQuestions(NavigationalParameters.EventManagement.Category)?.ToList();

                    DamageTypesListItems = new ObservableCollection<PublicUtilityDamageQuestion>(dmageTypes);
                }
            }
    });

    public RelayCommand SeverityPicker_SelectedIndexChanged => new(async () =>
    {
        if (SelectedDamageType != null)
        {
            if (SelectedDamageType != null)
            {
                NavigationalParameters.EventManagement.Severity = SelectedDamageType?.RemoteTableId.ToString();
                NavigationalParameters.RegisterUtilityDamage.DamageTypeId =
                    Convert.ToInt32(SelectedDamageType?.RemoteTableId);
            }

            if (SelectedDamageType?.DamageColour == "Red")
                SelectedSeverityBackgroundColour = Color.Red;
            else if (SelectedDamageType?.DamageColour == "Amber")
                SelectedSeverityBackgroundColour = Color.Orange;
            else if (SelectedDamageType?.DamageColour == "Green")
                SelectedSeverityBackgroundColour = Color.Green;
            else
                SelectedSeverityBackgroundColour = Color.FromHex("#1A142E");
        }
    });

    public RelayCommand HospitalPicker_SelectedIndexChanged => new(async () =>
    {
        if (NavigationalParameters.EventManagement != null)
        {
            if (RequiresHospital == "YES")
                NavigationalParameters.EventManagement.HospitalVisitRequired = true;
            else
                NavigationalParameters.EventManagement.HospitalVisitRequired = false;
        }
    });

    public RelayCommand ThirdPartyPicker_SelectedIndexChanged => new(async () =>
    {
        if (NavigationalParameters.EventManagement != null)
        {
            if (ThirdPartyDamage == "YES")
                NavigationalParameters.EventManagement.ThirdPartyDamage = true;
            else
                NavigationalParameters.EventManagement.ThirdPartyDamage = false;
        }
    });

    public RelayCommand UtilityPicker_SelectedIndexChanged => new(async () =>
    {
        if (UtilityCompanies.Any())
            if (UtilityCompanySelection != null)
                NavigationalParameters.RegisterUtilityDamage.TxtUtilityId = Convert.ToInt32(UtilityCompanies
                    .FirstOrDefault(x => x.CompanyId == UtilityCompanySelection.CompanyId).CompanyId);
    });

    public RelayCommand DamnagePicker_SelectedIndexChanged => new(async () =>
    {
        if (NavigationalParameters.RegisterUtilityDamage != null)
        {
            if (!string.IsNullOrEmpty(SelectedDamageLocation))
                NavigationalParameters.RegisterUtilityDamage.DamageLocation = SelectedDamageLocation;
            else
                NavigationalParameters.RegisterUtilityDamage.DamageLocation = SelectedDamageLocation;
        }
    });

    public RelayCommand AddInjuredPerson => new(async () =>
    {
        //if (NavigationalParameters.RegisterUtilityDamage != null) { NavigationalParameters.RegisterUtilityDamage.TxtNotes = Notes; }

        await NavigateTo(ViewModelLocator.EditInjuredPersonPage);
    });

    public RelayCommand SelectProject => new(async () =>
    {
        NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.GANGLIST;

        await NavigateTo(ViewModelLocator.GangSelectPage);
    });

    public RelayCommand<int> InjuredPersonSelected => new(async i =>
    {
        NavigationalParameters.InjuredPerson = InjuredPeople[i];

        await NavigateTo(ViewModelLocator.EditInjuredPersonPage);
    });

    public RelayCommand Submit => new(async () =>
    {
        var IncidentDateTime = IncidentDate + IncidentTime;
        var submitDamage1 = true;
        var submitDamage2 = true;
        var submitDamage3 = true;

        if (IncidentTime == TimeSpan.Parse("00:00:00.000"))
        {
            var userChoice = await Alert("Caution!",
                $"Please Confirm the Incident time of {IncidentDateTime} is correct", "Yes", "No");
            if (userChoice)
                submitDamage1 = true;
            else
                submitDamage1 = false;
        }

        if (submitDamage1 && submitDamage2 && submitDamage3)
        {
            if (ValidatePage())
            {
                await CreatePublicUtilityDamage();

                var userChoice2 = await Alert("Submit event?", "Would you like to submit the event now?",
                    "Yes", "No");
                if (userChoice2)
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        IsLoading = true;
                        ShowLoadingPage = false;

                        var result =
                            await _jobService.UploadEventManagementItem(NavigationalParameters
                                .EventManagement); //App.Manager.RegisterStrikes();

                        if (!result)
                        {
                            await Alert(
                                "An issue has occured submitting the event. This has been saved",
                                "Error!");
                            // NavigateBack();
                        }
                        else
                        {
                            SelectedDamageLocation = null;
                            ThirdPartyDamage = null;
                            RequiresHospital = null;
                            SelectedEventTypeItem = null;
                            SelectedDamageType = null;
                            Notes = "";
                            DamageTypesListItems = null;
                            InjuredPeople = null;
                            // DamagePhotos = new List<Pictures4Tablet>();
                            NavigationalParameters.EventManagement = null;
                            NavigationalParameters.RegisterUtilityDamage = null;
                            SelectedSeverityBackgroundColour = Color.FromHex("#1A142E");

                            await NavigateTo(ViewModelLocator.MenuSelectionPage);
                            await Alert("Successfully posted to server", "Success");
                        }
                    }
                    else
                    {
                        await Alert(
                            "Please connect to a network and try again. The event has been saved. ",
                            "No Connectivity");
                        //  NavigateBack();
                    }
                }
            }
            else
            {
                await Alert("Please Ensure All fields are complete including setting the location via the Gps button.", "Not Complete");
            }

            IsLoading = false;
            ShowLoadingPage = true;
        }
    });

    public RelayCommand Delete => new(async () =>
    {
        IsLoading = true;

        var userChoice = await Alert("Caution!",
            "If you delete the event now all data will be lost. Do you wish to continue ", "Yes", "No");
        if (userChoice)
        {
            if (NavigationalParameters.RegisterUtilityDamage != null)
            {
                //await _jobService
                // .DeleteRegisterUtilityDamage(
                //NavigationalParameters
                // .RegisterUtilityDamage); //App.Manager.RegisterStrikes();
                //
                NavigationalParameters.RegisterUtilityDamage = null;
                NavigationalParameters.EventManagement = null;
            }

            IsLoading = false;
            ShowLoadingPage = true;
            NavigateBack();
        }
        else
        {
            IsLoading = false;
            ShowLoadingPage = false;
        }
    });

    public RelayCommand AddImage => new(async () =>
    {
        //var p = new Pictures4Tablet();
        //p.Identifier = NavigationalParameters.RegisterUtilityDamage.PublicUtilityDamageGuid;
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.EVENTMANAGEMENT;
        //NavigationalParameters.SelectedPhoto = p;

        _psvm.TakePhoto.Execute(null);

        //   await NavigateTo(ViewModelLocator.PhotoSelectionPage);
        // DamagePhotos.Add(p);
    });

    public string[] split { get; set; }

    public RelayCommand GoBack => new(async () => { NavigateBack(); });

    public RelayCommand GpsCommand => new(async () =>
    {
        try
        {
            NavigationalParameters.MapType = "EventManagement";
            await NavigateTo(ViewModelLocator.FormsMapPage);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    private async Task<bool> CreatePublicUtilityDamage()
    {
        NavigationalParameters.EventManagement.Date = DateTime.Now;
        NavigationalParameters.EventManagement.InitialDetails =
            $"{NavigationalParameters.EventManagement.Location} : {NavigationalParameters.RegisterUtilityDamage.TxtNotes}";
        NavigationalParameters.EventManagement.RecordedById = NavigationalParameters.LoggedInUser.FocusId;
        NavigationalParameters.EventManagement.RecordedByName = NavigationalParameters.LoggedInUser.FullName;
        NavigationalParameters.EventManagement.RecordedByEmail = NavigationalParameters.LoggedInUser.Email;
        NavigationalParameters.EventManagement.QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;

        //NavigationalParameters.RegisterUtilityDamage.PublicUtilityDamageGuid = NavigationalParameters.EventManagement.Identifier;

        NavigationalParameters.RegisterUtilityDamage.IncidentDateTime =
            IncidentDate != DateTime.MinValue && IncidentTime != null
                ? (DateTime)(IncidentDate + IncidentTime)
                : NavigationalParameters.MinDateTime;

        NavigationalParameters.RegisterUtilityDamage.TxtReporterName =
            $"{NavigationalParameters.LoggedInUser.Surname}, {NavigationalParameters.LoggedInUser.FirstName}";
        NavigationalParameters.RegisterUtilityDamage.TxtReporterCompany = ReportedByCompany ?? "";
        NavigationalParameters.RegisterUtilityDamage.TxtContractorResponsibleContact = ContractorName ?? "";
        NavigationalParameters.RegisterUtilityDamage.TxtContractorResponsibleNumber = ContractorNumber = "";
        NavigationalParameters.RegisterUtilityDamage.TxtLocation = NavigationalParameters.EventManagement.Location;
        NavigationalParameters.RegisterUtilityDamage.TxtAnswerOne = "";
        NavigationalParameters.RegisterUtilityDamage.TxtAnswerTwo = "";
        NavigationalParameters.RegisterUtilityDamage.TxtAnswerThree = "";
        NavigationalParameters.RegisterUtilityDamage.TxtAnswerFour = "";
        NavigationalParameters.RegisterUtilityDamage.IsClientContacted = false;

        NavigationalParameters.RegisterUtilityDamage.UtilityContactedDateTime = NavigationalParameters.MinDateTime;

        NavigationalParameters.RegisterUtilityDamage.TxtUtilityReference = UtilityReference ?? "";
        NavigationalParameters.RegisterUtilityDamage.TxtUtilityContactName = UtilityContactName ?? "";
        NavigationalParameters.RegisterUtilityDamage.TxtUtilityContactNumber =
            UtilityCompanySelection?.Telephone ?? ""; //UtilityContactNumber,

        NavigationalParameters.RegisterUtilityDamage.ClientContacted = NavigationalParameters.MinDateTime;

        NavigationalParameters.RegisterUtilityDamage.InjuredPersonnel = GetInjuredPersonel();
        NavigationalParameters.RegisterUtilityDamage.ChkInjuries = InjuredPeople?.Count > 0;
        NavigationalParameters.RegisterUtilityDamage.ContractReference = SelectedProject?.ContractReference;
        NavigationalParameters.RegisterUtilityDamage.FirstName = NavigationalParameters.LoggedInUser?.FirstName;
        NavigationalParameters.RegisterUtilityDamage.Surname = NavigationalParameters.LoggedInUser?.Surname;

        NavigationalParameters.RegisterUtilityDamage.GangResponsible = GetGangResponsible(SelectedProject,
            NavigationalParameters.RegisterUtilityDamage.PublicUtilityDamageGuid);

        NavigationalParameters.RegisterUtilityDamage.Pictures =
            await GetDamagePictures(NavigationalParameters.EventManagement
                                                       .Identifier);


        await _jobService.SaveRegisterUtilityDamage(NavigationalParameters.RegisterUtilityDamage);
        await _jobService.SaveEventManagementItem(NavigationalParameters.EventManagement);

        NavigationalParameters.EventManagement.UtilityDamages = new List<RegisterUtilityDamage>
            { NavigationalParameters.RegisterUtilityDamage };
        return true;
    }

    private List<InjuredPerson> GetInjuredPersonel()
    {
        return _jobService.GetInjuredPeople().Where(x =>
                x.PublicUtilityDamageGuid == NavigationalParameters.RegisterUtilityDamage.PublicUtilityDamageGuid)
            .ToList();
    }

    private async Task<List<Pictures4Tablet>> GetDamagePictures(Guid damageId)
    {
        var ls = new LocalStorage();
        var picList = new List<Pictures4Tablet>();
        var allPhotoes = App.Database.GetItems<Pictures4Tablet>().ToList();
        var damagePhotoes = allPhotoes?.Where(x => x.Identifier.ToString() == NavigationalParameters.EventManagement
                                                       .Identifier.ToString()
                                                   && x.OperativeId == NavigationalParameters.LoggedInUser.FocusId
                                                   && x.QNumber == NavigationalParameters.CurrentSelectedJob
                                                       .QuoteNumber).ToList();

        foreach (var pic in damagePhotoes?.Where(x => x.Identifier == damageId)?.ToList())
        {
            pic.Image = await ls.GetImageFromLocalstorage("JobPictures", pic.FileName);

            if (!string.IsNullOrEmpty(pic.PicturePath))
            {
                split = pic.PicturePath.Split('/');

                picList.Add(new Pictures4Tablet
                {
                    FileName = pic.FileName,
                    PicturePath = pic.PicturePath.Replace(split?.Last(), ""),
                    DateTimeTaken = NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                        ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                        : DateTime.Now,
                    ContractReference = SelectedProject?.ContractReference,
                    OperativeId = NavigationalParameters.LoggedInUser.FocusId,
                    PictureComments = pic.PictureComments,
                    PictureReason = NavigationalParameters.AppMode.ToString().ToLower(),
                    Identifier = damageId,
                    Category = NavigationalParameters.EventManagement.Category,
                    Latitude = pic.Latitude,
                    Longitude = pic.Longitude,
                    GangLeaderId = pic.GangLeaderId,
                    SupervisorId = pic.SupervisorId,
                    QNumber = pic.QNumber
                });

                await ls.StoreImagesLocallyAndUpdatePath("JobPictures/",
                    picList.Last().FileName, pic.Image);

                //var rootFolder = await ls.GetTabletDocsPath();

                await ls.Save2AzureBlobByByteArray(pic.Image, picList.Last().FileName,
                    "focusspdata/UtilityDamagePics");
            }
        }

        return picList;
    }

    public List<GangResponsible> GetGangResponsible(JobData4Tablet job, Guid damageId)
    {
        var gangResponsible = new List<GangResponsible>();
        job.JobGang = _jobService.GetGang(job);

        foreach (var member in job.JobGang.GangMembers)
            gangResponsible.Add(new GangResponsible
            {
                OperativeID = member.Id,
                GangLeaderID = job.GangLeaderId,
                SupervisorID = job.SupervisorId,
                InputOn = DateTime.Now,
                PublicUtilityDamageGuid = damageId,
                SupervisorName = $"{job.SupervisorId}",
                GangLeaderName = $"{job.GangLeaderId}",
                OperativeName = $"{member.FullName}",
                GangResponsibleNames = $"{job.SupervisorId}-{job.GangLeaderId}-{member.Id}, "
            });

        return gangResponsible;
    }

    public bool ValidatePage()
    {
        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.UTILITYDAMAGE)
        {
            if (UtilityCompanySelection == null) return false;
            if (string.IsNullOrEmpty(ReportedBy)) return false;
            //  if (string.IsNullOrEmpty(ReportedByCompany)) return false;
        }


        // if (string.IsNullOrEmpty(DamageLocations[SelectedDamageIndex])) return false;

        if (IncidentDate == null) return false;
        if (IncidentTime == null) return false;
        if (string.IsNullOrEmpty(NavigationalParameters.EventManagement.Location)) return false;
        // if (SelectedDamageIndex == 0) return false;
        // if (SelectedUtilityCompany == 0) return false;


        return true;
    }

    public bool ValidateEntryText(Entry entry)
    {
        if (string.IsNullOrEmpty(entry.Text))
        {
            entry.TextColor = Color.Black;
            entry.PlaceholderColor = Color.Red;
            return false;
        }

        entry.TextColor = Color.Black;
        entry.PlaceholderColor = Color.Gray;
        return true;
    }

    public bool ValidateEntryNumber(Entry entry)
    {
        if (!int.TryParse(entry.Text, out var result))
        {
            entry.TextColor = Color.Red;
            entry.PlaceholderColor = Color.Red;

            return false;
        }

        entry.TextColor = Color.Black;
        entry.PlaceholderColor = Color.Gray;
        return true;
    }
}