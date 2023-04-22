#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Event = FocusXamarinMobileApplication.Models.Event;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class InvestigationDetailPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private string _clientName;
    private string _damageId;
    private string _incidentDateTime;
    private bool _isLoading;
    private string _location;


    private ObservableCollection<string> _riddorClassifications = new()
    {
        "", "Accident(RIDDOR)", "Accident(NON-RIDDOR)", "Near Miss", "Loss Event", "Dangerous Occurance(RIDDOR)"
    };

    private string _selectedDamageLocation = "";

    private string _selectedStrikeClassification = "";

    private string _selectedSurfaceType = "";

    private ObservableCollection<string> _yesNoEmpty = new() { "", "Yes", "No" };


    // private int _selectedDamageIndex;

    public AuthorisationDetail SigDetails;

    public InvestigationDetailPageViewModel()
    {
        _jobService = new Jobs();

        Event = NavigationalParameters.EventManagement;

        ForwardButtonText = "Go to methodology ==>";
    }

    public Event Event { get; private set; }
    public DamageToInvestigate DamageToInvestigate { get; set; }
    public Event CurrentEvent { get; private set; }
    public FocusMobileV3Database _db { get; set; }
    public DamageToInvestigate InvestigationsToSave { get; set; }
    public Investigation _investigationDetails { get; private set; }
    public DateTime RiddorDate { get; set; } = DateTime.Now;
    public TimeSpan RiddorTime { get; set; } = DateTime.Now.TimeOfDay;

    public string _contractReference { get; private set; }
    public string ContractReference { get; set; }
    public string DamageLocation { get; private set; }

    public string _projectDate { get; private set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; private set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    public bool _visible { get; private set; }

    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
            OnPropertyChanged();
        }
    }


    public bool _showRiddor { get; set; }

    public bool ShowRiddor
    {
        get => _showRiddor;
        set
        {
            _showRiddor = value;
            OnPropertyChanged();
        }
    }

    public string SelectedDamageLocation
    {
        get => _selectedDamageLocation;
        set
        {
            _selectedDamageLocation = value;
            OnPropertyChanged();
        }
    }

    public string SelectedStrikeClassification
    {
        get => _selectedStrikeClassification;
        set
        {
            _selectedStrikeClassification = value;

            if (SelectedStrikeClassification.ToUpper().Contains("(RIDDOR)"))
                ShowRiddor = true;
            else
                ShowRiddor = false;

            OnPropertyChanged();
        }
    }

    public string SelectedSurfaceType
    {
        get => _selectedSurfaceType;
        set
        {
            _selectedSurfaceType = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> YesNoEmpty
    {
        get => _yesNoEmpty;
        set
        {
            _yesNoEmpty = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> DamageLocations { get; set; } =
        new() { "", "Footway", "Carriageway", "Unmade" };

    public ObservableCollection<string> SurfaceTypes { get; set; } =
        new() { "", "Asphalt", "Concrete", "Flags", "Unmade" };

    public ObservableCollection<string> RiddorClassifications
    {
        get => _riddorClassifications;
        set
        {
            _riddorClassifications = value;
            OnPropertyChanged();
        }
    }

    public List<PublicUtilityDamageQuestion> _damages { get; set; }

    public List<PublicUtilityDamageQuestion> Damages
    {
        get => _damages;
        set
        {
            _damages = value;
            OnPropertyChanged();
        }
    }

    public PublicUtilityDamageQuestion _damageTypeSelected { get; set; }

    public PublicUtilityDamageQuestion DamageTypeSelected
    {
        get => _damageTypeSelected;
        set
        {
            DamageAnswerOne = "";
            DamageAnswerTwo = "";
            DamageAnswerThree = "";
            DamageAnswerFour = "";

            _damageTypeSelected = value;

            QuestionOne = DamageTypeSelected?.QuestionOne;
            QuestionTwo = DamageTypeSelected?.QuestionTwo;
            QuestionThree = DamageTypeSelected?.QuestionThree;
            QuestionFour = DamageTypeSelected?.QuestionFour;

            if (DamageTypeSelected?.Id.ToString() == DamageTypeAnswer?.DamageTypeID)
            {
                DamageAnswerOne = DamageTypeAnswer?.Answer1;
                DamageAnswerTwo = DamageTypeAnswer?.Answer2;
                DamageAnswerThree = DamageTypeAnswer?.Answer3;
                DamageAnswerFour = DamageTypeAnswer?.Answer4;
            }

            OnPropertyChanged();
        }
    }

    public DamageTypeAnswers _damageTypeAnswer { get; set; }

    public DamageTypeAnswers DamageTypeAnswer
    {
        get => _damageTypeAnswer;
        set
        {
            _damageTypeAnswer = value;
            OnPropertyChanged();
        }
    }

    public bool _answerSelection { get; set; }

    public bool AnswerSelection
    {
        get => _answerSelection;
        set
        {
            _answerSelection = value;
            OnPropertyChanged();
        }
    }

    public int _anserSelectionIndex { get; set; }

    public int AnserSelectionIndex
    {
        get => _anserSelectionIndex;
        set
        {
            _anserSelectionIndex = value;
            OnPropertyChanged();
        }
    }

    private string _printsProvided { get; set; }

    public string PrintsProvided
    {
        get => _printsProvided;
        set
        {
            _printsProvided = value;
            OnPropertyChanged();
        }
    }

    private int _selectedPrintsProvided { get; set; }

    public int SelectedPrintsProvided
    {
        get => _selectedPrintsProvided;
        set
        {
            _selectedPrintsProvided = value;
            OnPropertyChanged();
        }
    }

    private string _adequateInformation { get; set; }

    public string AdequateInformation
    {
        get => _adequateInformation;
        set
        {
            _adequateInformation = value;
            OnPropertyChanged();
        }
    }

    public int _selectedAdequateInformation { get; private set; }

    public int SelectedAdequateInformation
    {
        get => _selectedAdequateInformation;
        set
        {
            _selectedAdequateInformation = value;
            OnPropertyChanged();
        }
    }

    private string _printsInColour { get; set; }

    public string PrintsInColour
    {
        get => _printsInColour;
        set
        {
            _printsInColour = value;
            OnPropertyChanged();
        }
    }

    public int _selectedPrintsInColour { get; private set; }

    public int SelectedPrintsInColour
    {
        get => _selectedPrintsInColour;
        set
        {
            _selectedPrintsInColour = value;
            OnPropertyChanged();
        }
    }

    public string _damageAnswerOne { get; set; }

    public string DamageAnswerOne
    {
        get => _damageAnswerOne;
        set
        {
            _damageAnswerOne = value;
            OnPropertyChanged();
        }
    }

    public string _damageAnswerTwo { get; set; }

    public string DamageAnswerTwo
    {
        get => _damageAnswerTwo;
        set
        {
            _damageAnswerTwo = value;
            OnPropertyChanged();
        }
    }

    public string _damageAnswerThree { get; set; }

    public string DamageAnswerThree
    {
        get => _damageAnswerThree;
        set
        {
            _damageAnswerThree = value;
            OnPropertyChanged();
        }
    }

    public string _damageAnswerFour { get; set; }

    public string DamageAnswerFour
    {
        get => _damageAnswerFour;
        set
        {
            _damageAnswerFour = value;
            OnPropertyChanged();
        }
    }

    private string _eventsLeadingTo { get; set; }

    public string EventsLeadingTo
    {
        get => _eventsLeadingTo;
        set
        {
            _eventsLeadingTo = value;
            OnPropertyChanged();
        }
    }

    private string _immediateCause { get; set; }

    public string ImmediateActions
    {
        get => _immediateActions;
        set
        {
            _immediateActions = value;
            OnPropertyChanged();
        }
    }

    private string _immediateActions { get; set; }

    public string ImmediateCause
    {
        get => _immediateCause;
        set
        {
            _immediateCause = value;
            OnPropertyChanged();
        }
    }

    public bool _electric { get; private set; }
    public PrintTypesProvided PrintTypesProvided { get; private set; }

    public bool Electric
    {
        get => _electric;
        set
        {
            _electric = value;
            OnPropertyChanged();
        }
    }

    public bool _gas { get; private set; }

    public bool Gas
    {
        get => _gas;
        set
        {
            _gas = value;
            OnPropertyChanged();
        }
    }

    public bool _water { get; private set; }

    public bool Water
    {
        get => _water;
        set
        {
            _water = value;
            OnPropertyChanged();
        }
    }

    public bool _BT { get; private set; }

    public bool BT
    {
        get => _BT;
        set
        {
            _BT = value;
            OnPropertyChanged();
        }
    }

    public bool _sewer { get; private set; }

    public bool Sewer
    {
        get => _sewer;
        set
        {
            _sewer = value;
            OnPropertyChanged();
        }
    }

    private string _forwardButtonText { get; set; }

    public string ForwardButtonText
    {
        get => _forwardButtonText;
        set
        {
            _forwardButtonText = value;

            OnPropertyChanged();
        }
    }

    public bool _cctv { get; private set; }

    public bool CCTV
    {
        get => _cctv;
        set
        {
            _cctv = value;
            OnPropertyChanged();
        }
    }

    public string ClientName
    {
        get => _clientName;
        set
        {
            _clientName = value;
            OnPropertyChanged();
        }
    }

    public string Location
    {
        get => _location;
        set
        {
            _location = value;
            OnPropertyChanged();
        }
    }

    public string IncidentDateTime
    {
        get => _incidentDateTime;
        set
        {
            _incidentDateTime = value;
            OnPropertyChanged();
        }
    }

    public string DamageId
    {
        get => _damageId;
        set
        {
            _damageId = value;
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

    public string _questionOne { get; set; } = "N/A";

    public string QuestionOne
    {
        get => _questionOne;
        set
        {
            _questionOne = value;
            OnPropertyChanged();
        }
    }


    public string _questionTwo { get; set; } = "N/A";

    public string QuestionTwo
    {
        get => _questionTwo;
        set
        {
            _questionTwo = value;
            OnPropertyChanged();
        }
    }

    public string _questionThree { get; set; } = "N/A";

    public string QuestionThree
    {
        get => _questionThree;
        set
        {
            _questionThree = value;
            OnPropertyChanged();
        }
    }

    public bool _showQ4 { get; private set; }

    public bool ShowQ4
    {
        get => _showQ4;
        set
        {
            _showQ4 = value;
            OnPropertyChanged();
        }
    }

    public string _questionFour { get; set; } = "N/A";

    public string QuestionFour
    {
        get => _questionFour;
        set
        {
            _questionFour = value;

            if (string.IsNullOrEmpty(QuestionFour))
                ShowQ4 = false;
            else
                ShowQ4 = true;
            OnPropertyChanged();
        }
    }


    public string _title { get; set; } = "Damage Details";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string UserPin { get; set; }
    public object SelectedItem { get; internal set; }
    public string StrikeClassType { get; private set; }
    public bool ExcludeChildren { get; } = true;
    public string StrikeClassification { get; set; }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectDate = DateTime.Now.ToShortDateString();

        ProjectName = Event?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault()?.ProjectName;

        Event = NavigationalParameters.EventManagement;

        DamageToInvestigate = Event.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault();

        //InvestigationDetails = DamageToInvestigate?.DamageInvestigated;

        CurrentEvent = App.Database.GetItems<Event>()
            ?.FirstOrDefault(x => x.Identifier == DamageToInvestigate?.DamageGuid);

        Title = $"{Event?.Category} DETAILS - {DamageToInvestigate?.IncidentDateTime}";

        Damages = App.Database.GetItems<PublicUtilityDamageQuestion>().ToList();

        DamageTypeSelected = Damages.FirstOrDefault(x =>
            x.RemoteTableId.ToString() == CurrentEvent?.EventType.ToString());

        ForwardButtonText = "Go to methodology ==>";

        if (DamageToInvestigate != null)
        {
            ContractReference = DamageToInvestigate?.ContractReference;

            ClientName = DamageToInvestigate?.ClientName;

            Location = DamageToInvestigate?.Location;

            if (DamageToInvestigate?.CurrentInvestigationStatus == "Not Started")
                DamageToInvestigate.CurrentInvestigationStatus = "In Progress";

            DamageToInvestigate.SavedToServer = false;

            QuestionOne = DamageTypeSelected?.QuestionOne;

            QuestionTwo = DamageTypeSelected?.QuestionTwo;

            QuestionThree = DamageTypeSelected?.QuestionThree;

            QuestionFour = DamageTypeSelected?.QuestionFour;

            DamageTypeAnswer = App.Database.GetItems<DamageTypeAnswers>()
                .Where(x => x.PublicUtilityDamageID == DamageToInvestigate?.DamageId
                            && x.InvestigationID.ToString() == DamageToInvestigate?.InvestigationId)
                ?.OrderByDescending(x => x.InputOn)?
                .FirstOrDefault();

            if (DamageTypeAnswer == null)
                DamageTypeAnswer = new DamageTypeAnswers
                {
                    InvestigationID = Convert.ToInt32(DamageToInvestigate?.InvestigationId),
                    PublicUtilityDamageID = DamageToInvestigate.DamageId,
                    Answer1 = "",
                    Answer2 = "",
                    Answer3 = "",
                    Answer4 = "",
                    DamageLocation = "",
                    DamageTypeID = "0",
                    InputOn = DateTime.Now,
                    IsFinal = false,
                    SurfaceMaterial = ""
                };

            App.Database.SaveItem(DamageTypeAnswer);

            DamageAnswerOne = DamageTypeAnswer?.Answer1;

            DamageAnswerTwo = DamageTypeAnswer?.Answer2;

            DamageAnswerThree = DamageTypeAnswer?.Answer3;

            DamageAnswerFour = DamageTypeAnswer?.Answer4;

            SelectedDamageLocation = DamageTypeAnswer?.DamageLocation;

            SelectedSurfaceType = DamageTypeAnswer?.SurfaceMaterial;

            SelectedStrikeClassification = DamageToInvestigate?.StrikeClassification ?? "";

            if (DamageToInvestigate?.IncidentDateTime > DateTime.MinValue)
                RiddorDate = (DateTime)DamageToInvestigate?.IncidentDateTime.Date;
            else
                RiddorDate = DateTime.Now.Date;

            if (DamageToInvestigate?.IncidentDateTime != null)
                RiddorTime = (TimeSpan)DamageToInvestigate?.IncidentDateTime.TimeOfDay;
            else
                RiddorTime = DateTime.Now.TimeOfDay;

            PrintTypesProvided =
                App.Database.GetItems<PrintTypesProvided>()?.Where(x =>
                        x.PublicUtilityDamageId == DamageToInvestigate?.DamageId &&
                        x.InvestigationId.ToString() == DamageToInvestigate?.InvestigationId)
                    ?.OrderByDescending(x => x.RemoteTableId)
                    ?.FirstOrDefault();

            if (PrintTypesProvided == null)
            {
                PrintTypesProvided = DamageToInvestigate?.PrintTypesProvided;

                if (PrintTypesProvided == null)
                    new PrintTypesProvided
                    {
                        InvestigationId = Convert.ToInt64(DamageToInvestigate?.InvestigationId),
                        AdequateInformation = false,
                        PrintsInColour = false,
                        PrintsProvided = false,
                        IsComplete = false,
                        RemoteTableId = 0,
                        PublicUtilityDamageId =
                            DamageToInvestigate.DamageId,
                        BT = false,
                        CCTV = false,
                        Electric = false,
                        Gas = false,
                        Sewer = false,
                        Water = false
                    };
            }


            Electric = PrintTypesProvided.Electric;

            Gas = PrintTypesProvided.Gas;

            Water = PrintTypesProvided.Water;

            BT = PrintTypesProvided.BT;

            Sewer = PrintTypesProvided.Sewer;

            CCTV = PrintTypesProvided.CCTV;

            SelectedPrintsProvided = 0;

            SelectedAdequateInformation = 0;

            SelectedPrintsInColour = 0;

            if (PrintTypesProvided?.PrintsProvided == true)
            {
                PrintsProvided = "Yes";
                SelectedPrintsProvided = 1;
            }
            else if (PrintTypesProvided?.PrintsProvided == false)
            {
                PrintsProvided = "No";
                SelectedPrintsProvided = 2;
            }
            else
            {
                PrintsProvided = "";
                SelectedPrintsProvided = 0;
            }

            if (PrintTypesProvided?.AdequateInformation == true)
            {
                AdequateInformation = "Yes";
                SelectedAdequateInformation = 1;
            }
            else if (PrintTypesProvided?.AdequateInformation == false)
            {
                AdequateInformation = "No";
                SelectedAdequateInformation = 2;
            }
            else
            {
                AdequateInformation = "";
                SelectedAdequateInformation = 0;
            }

            if (PrintTypesProvided?.PrintsInColour == true)
            {
                PrintsInColour = "Yes";
                SelectedPrintsInColour = 1;
            }
            else if (PrintTypesProvided?.PrintsInColour == false)
            {
                PrintsInColour = "No";
                SelectedPrintsInColour = 2;
            }
            else
            {
                PrintsInColour = "";
                SelectedPrintsInColour = 0;
            }

            SelectedPrintsProvided =
                YesNoEmpty.IndexOf(PrintsProvided) < 0 ? 0 : YesNoEmpty.IndexOf(PrintsProvided);

            SelectedAdequateInformation = YesNoEmpty.IndexOf(AdequateInformation) < 0
                ? 0
                : YesNoEmpty.IndexOf(AdequateInformation);

            SelectedPrintsInColour =
                YesNoEmpty.IndexOf(PrintsInColour) < 0 ? 0 : YesNoEmpty.IndexOf(PrintsInColour);

            App.Database.SaveItem(PrintTypesProvided);

            DamageToInvestigate.SavedToServer = false;
        }

        DamageToInvestigate.Gangleader = App.Database.GetItems<Person>()
            ?.FirstOrDefault(x => x.FocusId == DamageToInvestigate?.GangLeaderId);

        DamageToInvestigate.Supervisor = App.Database.GetItems<Person>()?
            .FirstOrDefault(x => x.FocusId == DamageToInvestigate?.SupervisorId);

        await _jobService.SaveInvestigateDamage(DamageToInvestigate);
        // DamageToInvestigate.DamageInvestigated = App.Database.GetItems<Investigation>()?.FirstOrDefault
        //(x => x.RemoteTabledId.ToString() == DamageToInvestigate.InvestigationId);
        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        NavigationalParameters.EventManagement = Event;
    });

    public RelayCommand MethodologyCommand => new(async () =>
    {
        SaveDetails();

        var valid = ValidateInvestigation();

        if (valid)
            await NavigateTo(ViewModelLocator.MethodologyPage);
        else
            await Alert("Please Compleate all selections before proceding", "Incomplete");
    });

    public RelayCommand GoBackToGeneral => new(async () =>
    {
        SaveDetails();

        await NavigateTo(ViewModelLocator.InvestigateDamagePage);
    });

    private bool ValidateInvestigation()
    {
        if (string.IsNullOrEmpty(DamageAnswerOne) || string.IsNullOrEmpty(SelectedStrikeClassification) ||
            string.IsNullOrEmpty(SelectedDamageLocation) || string.IsNullOrEmpty(SelectedSurfaceType))
            return false;
        return true;
    }

    public void SaveDetails()
    {
        //---------------------------------damage type answer -------------------------
        DamageTypeAnswer.PublicUtilityDamageID = DamageToInvestigate.DamageId;
        DamageTypeAnswer.InvestigationID = Convert.ToInt32(DamageToInvestigate.InvestigationId);

        DamageTypeAnswer.DamageLocation = SelectedDamageLocation;
        DamageTypeAnswer.DamageTypeID = DamageTypeSelected?.RemoteTableId.ToString() ?? "0";
        DamageTypeAnswer.Answer1 = DamageAnswerOne;
        DamageTypeAnswer.Answer2 = DamageAnswerTwo;
        DamageTypeAnswer.Answer3 = DamageAnswerThree;
        DamageTypeAnswer.Answer4 = DamageAnswerFour;
        DamageTypeAnswer.InputOn = DateTime.Now;
        DamageTypeAnswer.SavedToServer = false;
        DamageTypeAnswer.IsFinal = false;
        DamageTypeAnswer.SurfaceMaterial = SelectedSurfaceType;
        App.Database.SaveItem(DamageTypeAnswer);

        //----------------prints provided -----------------------
        PrintTypesProvided.PublicUtilityDamageId = DamageToInvestigate.DamageId;
        PrintTypesProvided.InvestigationId = Convert.ToInt32(DamageToInvestigate.InvestigationId);
        PrintTypesProvided.PrintsProvided = PrintsProvided.ToLower() == "yes";
        PrintTypesProvided.AdequateInformation = AdequateInformation.ToLower() == "yes";
        PrintTypesProvided.PrintsInColour = PrintsInColour.ToLower() == "yes";
        PrintTypesProvided.Electric = Electric;
        PrintTypesProvided.Gas = Gas;
        PrintTypesProvided.Water = Water;
        PrintTypesProvided.BT = BT;
        PrintTypesProvided.Sewer = Sewer;
        PrintTypesProvided.CCTV = CCTV;
        App.Database.SaveItem(PrintTypesProvided);

        //-------------------- investigation -------------------------
        DamageToInvestigate.RiddorDate = RiddorDate;
        DamageToInvestigate.RiddorTime = RiddorTime;

        DamageToInvestigate.StrikeClassification = SelectedStrikeClassification;
        App.Database.SaveItem(DamageToInvestigate);

        DamageToInvestigate.PrintTypesProvided = PrintTypesProvided;
        DamageToInvestigate.DamageTypeAnswer = DamageTypeAnswer;

        var allanswers = App.Database.GetItems<InvestigationAnswer>();

        if (allanswers.Any())
        {
            var Answers = allanswers?.Where(x =>
                    x.InvestigationId.ToString() == DamageToInvestigate.InvestigationId)?
                .GroupBy(x => x.QuestionId)?
                .ToList();

            foreach (var a in Answers)
                DamageToInvestigate.DamageInvestigated.InvestigationAnswers.Add(a.OrderByDescending(x => x.InputOn)?
                    .FirstOrDefault());
        }

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            ?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());
        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);
        NavigationalParameters.EventManagement = Event;
    }
}