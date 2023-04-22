#region

using Event = FocusXamarinMobileApplication.Models.Event;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class InvestigationDetail2PageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private string _damageId;
    private bool _isLoading;
    public AuthorisationDetail SigDetails;


    public InvestigationDetail2PageViewModel()
    {
        _jobService = new Jobs();

        Event = NavigationalParameters.EventManagement;

        ProjectDate = DateTime.Now.ToShortDateString();

        ProjectName = Event.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault()?.ProjectName;
    }

    public Event Event { get; private set; }
    public DamageToInvestigate DamageToInvestigate { get; set; }
    public Event CurrentEvent { get; private set; }
    public FocusMobileV3Database _db { get; set; }
    public DamageToInvestigate InvestigationsToSave { get; set; }
    public Investigation _investigationDetails { get; private set; }
    public DateTime RiddorDate { get; set; } = DateTime.Now;
    public TimeSpan RiddorTime { get; set; } = DateTime.Now.TimeOfDay;


    private string _projectName { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    private string _title { get; set; } = "Investigate Utility Damage";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }


    private string _leadingToLabel { get; set; } = "Events leading to the utility damage";

    public string LeadingToLabel
    {
        get => _leadingToLabel;
        set
        {
            _leadingToLabel = value;
            OnPropertyChanged();
        }
    }


    private string _projectDate { get; set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
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

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectDate = DateTime.Now.ToShortDateString();

        Event = NavigationalParameters.EventManagement;

        ProjectName = Event.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault()?.ProjectName;

        Event = NavigationalParameters.EventManagement;

        DamageToInvestigate = Event.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault();

        CurrentEvent = App.Database.GetItems<Event>()
            ?.FirstOrDefault(x => x.Identifier == DamageToInvestigate?.DamageGuid);

        if (DamageToInvestigate != null)
        {
            var investigationId = Convert.ToInt64(DamageToInvestigate?.InvestigationId);

            if (DamageToInvestigate?.CurrentInvestigationStatus == "Not Started")
                DamageToInvestigate.CurrentInvestigationStatus = "In Progress";

            DamageToInvestigate.SavedToServer = false;

            DamageToInvestigate.DamageInvestigated.IsUpdatedToServer = false;

            EventsLeadingTo = DamageToInvestigate?.EventsLeading;

            ImmediateActions = DamageToInvestigate?.ImmediateAction;

            ImmediateCause = DamageToInvestigate?.ImmediateCause;

            DamageId = DamageToInvestigate?.DamageId.ToString();

            DamageToInvestigate.SavedToServer = false;
        }

        DamageToInvestigate.Gangleader = App.Database.GetItems<Person>()
            ?.FirstOrDefault(x => x.FocusId == DamageToInvestigate?.GangLeaderId);

        DamageToInvestigate.Supervisor = App.Database.GetItems<Person>()?
            .FirstOrDefault(x => x.FocusId == DamageToInvestigate?.SupervisorId);

        if (Event.Category.ToLower().Contains("utility"))
        {
            LeadingToLabel = "Events leading to the utility damage";
            Title = "Investigate Utility Damage";
        }
        else if (Event.Category.ToLower().Contains("nearmiss"))
        {
            Title = "Investigate Near Miss";
            LeadingToLabel = "Events leading to the near miss";
        }
        else if (Event.Category.ToLower().Contains("accident"))
        {
            Title = "Investigate Accident";
            LeadingToLabel = "Events leading to the accident";
        }
        else
        {
            Title = "Investigate Incident";
            LeadingToLabel = "Events leading to the incident";
        }

        await _jobService.SaveInvestigateDamage(DamageToInvestigate);

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        NavigationalParameters.EventManagement = Event;
    });

    public RelayCommand MethodologyCommand => new(async () =>
    {
        DamageToInvestigate.EventsLeading = EventsLeadingTo ?? "";

        DamageToInvestigate.ImmediateAction = ImmediateActions ?? "";

        DamageToInvestigate.ImmediateCause = ImmediateCause ?? "";

        App.Database.SaveItem(DamageToInvestigate);

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            ?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        NavigationalParameters.EventManagement = Event;

        await NavigateTo(ViewModelLocator.MethodologyPage);
    });

    public RelayCommand ExecutePartialSubmit => new(async () =>
    {
        IsLoading = true;

        try
        {
            var allanswers = App.Database.GetItems<InvestigationAnswer>();
            if (allanswers.Any())
            {
                var Answers = allanswers?.Where(x =>
                        //x.DamageId.ToString() == DamageToInvestigate.DamageInvestigated.DamageId
                        x.InvestigationId.ToString() == DamageToInvestigate.InvestigationId)
                    .GroupBy(x => x.QuestionId)
                    .ToList();

                foreach (var a in Answers)
                    DamageToInvestigate.DamageInvestigated.InvestigationAnswers.Add(a
                        .OrderByDescending(x => x.InputOn)
                        .FirstOrDefault());
            }

            DamageToInvestigate.EventsLeading = EventsLeadingTo;

            DamageToInvestigate.ImmediateAction = ImmediateActions;

            DamageToInvestigate.ImmediateCause = ImmediateCause;

            App.Database.SaveItem(DamageToInvestigate);

            Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
                .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());
            Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

            foreach (var pic in App.Database.GetItems<Pictures4Tablet>())
                if (pic.Identifier == DamageToInvestigate.DamageGuid)
                {
                    DamageToInvestigate.PreviousImages.Add(pic);
                }
                else
                {
                    var catList = pic.Category.Split('-').ToList();

                    if (catList.Any(x => x == DamageToInvestigate.InvestigationId))
                        DamageToInvestigate.PreviousImages.Add(pic);
                }

            DamageToInvestigate.DamageInvestigated.InvestigationSubmitType = "Partial";

            await _jobService.SaveInvestigateDamage(DamageToInvestigate);

            //await _jobService.SaveCurrentInvestigation(DamageToInvestigate.DamageInvestigated);

            var uploadedSucessfully =
                await _jobService.UploadUtilityInvestigations(DamageToInvestigate, "Partial");

            if (uploadedSucessfully)
            {
                DamageToInvestigate.SavedToServer = true;

                DamageToInvestigate.DamageInvestigated.IsUpdatedToServer = true;

                await _jobService.UpdateInvestigationDetails(DamageToInvestigate);
                Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
                    .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());
                Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);
                NavigationalParameters.EventManagement = Event;

                IsLoading = false;
            }

            NavigationalParameters.EventManagement = Event = null;
            DamageToInvestigate = null;
            await NavigateTo(ViewModelLocator.TaskListPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            await Alert("An issue has occured saving the investigation. Please try again", "Error!");
            IsLoading = false;
        }

        //await NavigateTo(Locator.SignaturePageViewModelKey);
    });

    public RelayCommand ExecuteFullSubmit => new(async () =>
    {
        var allanswers = App.Database.GetItems<InvestigationAnswer>();
        if (allanswers.Any())
        {
            var Answers = allanswers?.Where(x =>
                    //x.DamageId.ToString() == DamageToInvestigate.DamageInvestigated.DamageId
                    x.InvestigationId.ToString() == DamageToInvestigate.InvestigationId)
                .GroupBy(x => x.QuestionId)
                .ToList();

            foreach (var a in Answers)
                DamageToInvestigate.DamageInvestigated.InvestigationAnswers.Add(a
                    .OrderByDescending(x => x.InputOn)
                    .FirstOrDefault());
        }


        DamageToInvestigate.EventsLeading = EventsLeadingTo;

        DamageToInvestigate.ImmediateAction = ImmediateActions;

        DamageToInvestigate.ImmediateCause = ImmediateCause;

        App.Database.SaveItem(DamageToInvestigate);

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        foreach (var pic in App.Database.GetItems<Pictures4Tablet>())
            if (pic.Identifier == DamageToInvestigate.DamageGuid)
            {
                DamageToInvestigate.PreviousImages.Add(pic);
            }
            else
            {
                var catList = pic.Category.Split('-').ToList();

                if (catList.Any(x => x == DamageToInvestigate.InvestigationId))
                    DamageToInvestigate.PreviousImages.Add(pic);
            }

        DamageToInvestigate.DamageInvestigated.InvestigationSubmitType = "Full";

        await _jobService.SaveInvestigateDamage(DamageToInvestigate);

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        NavigationalParameters.EventManagement = Event;

        NavigationalParameters.InvestigationIdToFinalize = DamageToInvestigate.InvestigationId;

        var valid = ValidateInvestigation();

        if (valid)
            await NavigateTo(ViewModelLocator.SignaturePage);
        else
            await Alert("Please Compleate all selections before proceding", "Incomplete");
    });

    private bool ValidateInvestigation()
    {
        if (string.IsNullOrEmpty(EventsLeadingTo) || string.IsNullOrEmpty(ImmediateActions) ||
            string.IsNullOrEmpty(ImmediateCause))
            return false;
        return true;
    }
}