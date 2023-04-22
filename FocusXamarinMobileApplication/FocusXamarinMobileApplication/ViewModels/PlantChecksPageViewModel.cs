#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class PlantChecksPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public Assignments _assigmentService;
    public Checks _checkService;
    private Command _dontHaveItemCommand;
    private Command _inStorageItemCommand;
    public Jobs _jobService;
    public Plant _plantService;
    private Command<string> _setAllAnswers2Yes;
    public Users _userService;

    public PlantChecksPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        _plantService = new Plant();
        _checkService = new Checks();
    }

    public string _projectDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

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

    public bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
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

    public Plant4Tablet _selectedPlantItem { get; set; }

    public Plant4Tablet SelectedPlantItem
    {
        get => _selectedPlantItem;
        set
        {
            _selectedPlantItem = value;
            OnPropertyChanged();
        }
    }

    public List<Checks4TabletResponses> _allCurrentResponses { get; private set; }

    public List<Checks4TabletResponses> AllCurrentResponses
    {
        get => _allCurrentResponses;
        set
        {
            _allCurrentResponses = value;
            OnPropertyChanged();
        }
    }

    public Checks4TabletResponses _currentCheckAnswer { get; set; }

    public Checks4TabletResponses CurrentCheckAnswer
    {
        get => _currentCheckAnswer;
        set
        {
            _currentCheckAnswer = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<YesNoNaPlantCheckQuestionViewModel> _questionCollection { get; set; }

    public ObservableCollection<YesNoNaPlantCheckQuestionViewModel> QuestionCollection
    {
        get => _questionCollection;
        set
        {
            _questionCollection = value;
            OnPropertyChanged();
        }
    }


    public YesNoNaPlantCheckQuestionViewModel _selectedQuestion { get; set; }

    public YesNoNaPlantCheckQuestionViewModel SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            _selectedQuestion = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Person> _gangMemberList { get; set; }

    public ObservableCollection<Person> GangMemberList
    {
        get => _gangMemberList;
        set
        {
            _gangMemberList = value;
            OnPropertyChanged();
        }
    }

    public Person _selectedUser { get; set; }

    public Person SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

    public int _selectedUserIndex { get; set; }

    public int SelectedUserIndex
    {
        get => _selectedUserIndex;
        set
        {
            _selectedUserIndex = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        Title = "Plant checks";
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        GangMemberList =
            new ObservableCollection<Person>(NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLabourFiles);

        SelectedUser = null;
        NavigationalParameters.PlantView = NavigationalParameters.PlantViews.PLANTCHECKS;
        SelectedPlantItem = NavigationalParameters.SelectetedPlantItem;
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        IsLoading = false;

        if (NavigationalParameters.SelectedUser != null)
        {
            SelectedUser = NavigationalParameters.SelectedUser;
            SelectedUserIndex = GangMemberList.IndexOf(SelectedUser);
        }
        else
        {
            SelectedUser = null;
            SelectedUserIndex = -1;
        }

        RefreshAllCurrentChecks();
    });

    public RelayCommand DontHaveItemCommand => new(async () =>
    {
        if (SelectedUser == null)
        {
            await Alert("Please confirm who is performing the checks on this item!", "Error!");
        }
        else
        {
            var supervisor = new Person();

            if (NavigationalParameters.CurrentSelectedJob != null)
                supervisor = App.Database.GetItems<Person>()?.FirstOrDefault(x =>
                    x.FocusId == NavigationalParameters.CurrentSelectedJob.SupervisorId);

            var questionNumber = 99998;

            var confirmed = await Alert(
                "Please Confirm your selection as you will not be able to change this later!",
                "Confirmation",
                "OK", "Cancel");


            if (confirmed)
            {
                ShowSection1 = false;
                ShowSection2 = false;
                ShowSection3 = false;
                ShowSection4 = false;
                IsLoading = true;

                //var selectedPlantInfo =
                //    _plantService.GetPlantByServerId(
                //        (int)
                //        SelectedPlantItem
                //            .RemotePlantId);

                if (NavigationalParameters.LoggedInUser.FullName != null &&
                    NavigationalParameters.CurrentSelectedJob !=
                    null &&
                    SelectedPlantItem !=
                    null)
                {
                    if (AllCurrentResponses != null
                        && SelectedPlantItem != null)
                        _checkService
                            .DeleteAllInstancesOfThisPlantIdFromDb(
                                (int)
                                SelectedPlantItem
                                    .RemotePlantId);

                    CurrentCheckAnswer =
                        new
                            Checks4TabletResponses
                            {
                                Qnumber = "",
                                RelevantDate
                                    = DateTime
                                        .Now,

                                PlantId =
                                    (int)
                                    SelectedPlantItem
                                        .RemotePlantId,
                                QuestionResponse
                                    = "Dont Have",
                                SupervisorName = supervisor.FullName ?? NavigationalParameters.LoggedInUser?.FullName,
                                GangLeaderName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName ??
                                                 supervisor.FullName,

                                DateTimeOfCheck
                                    = DateTime
                                        .Now,
                                QuestionId =
                                    questionNumber,
                                PlantCheckedByName = SelectedUser?.FullName ?? "",

                                PlantAssignedToName
                                    = NavigationalParameters.SelectetedPlantItem
                                        .IssuedToName,
                                ChecksStatus
                                    = "ChecksConfirmed",
                                Comments =
                                    "",
                                PictureFileName
                                    = "",
                                SignatureFileName
                                    = "",
                                CheckSubmittedBy
                                    = "",
                                ChecksType = NavigationalParameters.SelectetedPlantItem.Group == "Vehicles"
                                    ? "Vehicle"
                                    : "Plant"
                            };

                    _checkService
                        .AddSingleChecksResponse(
                            CurrentCheckAnswer);

                    NavigationalParameters.SelectetedPlantItem.ChecksComplete = true;

                    await _plantService.AddPlantItem(NavigationalParameters.SelectetedPlantItem);
                }

                await NavigateTo(ViewModelLocator.MyPlantScreenPage);
            }
        }
    });

    public RelayCommand InStorageItemCommand => new(async () =>
    {
        if (SelectedUser == null)
        {
            await Alert("Please confirm who is performing the checks on this item!", "Error!");
        }
        else
        {
            var supervisor = new Person();

            if (NavigationalParameters.CurrentSelectedJob != null)
                supervisor = App.Database.GetItems<Person>()?.FirstOrDefault(x =>
                    x.FocusId == NavigationalParameters.CurrentSelectedJob.SupervisorId);

            var questionNumber = 99999;

            var confirmed = await Alert(
                "Please Confirm your selection as you will not be able to change this later!",
                "Confirmation",
                "OK", "Cancel");

            if (confirmed)
            {
                ShowSection1 = false;
                ShowSection2 = false;
                ShowSection3 = false;
                ShowSection4 = false;
                IsLoading = true;

                //var selectedPlantInfo =
                //    _plantService.GetPlantByServerId(
                //        (int)
                //        SelectedPlantItem
                //            .RemotePlantId);

                if (NavigationalParameters.LoggedInUser.FullName != null &&
                    NavigationalParameters.CurrentSelectedJob !=
                    null &&
                    SelectedPlantItem !=
                    null)
                {
                    if (AllCurrentResponses != null
                        && SelectedPlantItem != null)
                        _checkService
                            .DeleteAllInstancesOfThisPlantIdFromDb(
                                (int)
                                SelectedPlantItem
                                    .RemotePlantId);

                    CurrentCheckAnswer =
                        new
                            Checks4TabletResponses
                            {
                                Qnumber = "",
                                RelevantDate
                                    = DateTime
                                        .Now,
                                GangLeaderName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName ??
                                                 supervisor.FullName,
                                SupervisorName = supervisor.FullName ?? NavigationalParameters.LoggedInUser?.FullName,

                                PlantId =
                                    (int)
                                    SelectedPlantItem
                                        .RemotePlantId,
                                QuestionResponse
                                    = "In Storage",
                                DateTimeOfCheck
                                    = DateTime
                                        .Now,
                                QuestionId =
                                    questionNumber,
                                PlantCheckedByName = SelectedUser?.FullName ?? "",

                                PlantAssignedToName
                                    = NavigationalParameters.SelectetedPlantItem
                                        .IssuedToName,
                                ChecksStatus
                                    = "ChecksConfirmed",
                                Comments =
                                    "",
                                PictureFileName
                                    = "",
                                SignatureFileName
                                    = "",
                                CheckSubmittedBy
                                    = "",
                                ChecksType = NavigationalParameters.SelectetedPlantItem.Group == "Vehicles"
                                    ? "Vehicle"
                                    : "Plant"
                            };

                    if (_checkService.CreateDBifNotExists("Checks4TabletResponses"))
                        _checkService
                            .AddSingleChecksResponse(
                                CurrentCheckAnswer);

                    NavigationalParameters.SelectetedPlantItem.ChecksComplete = true;

                    await _plantService.AddPlantItem(NavigationalParameters.SelectetedPlantItem);
                }

                await NavigateTo(ViewModelLocator.MyPlantScreenPage);
            }
        }
    });

    public RelayCommand UpdateSelectedUserCommand => new(async () =>
    {
        NavigationalParameters.SelectedUser = SelectedUser;
    });

    public RelayCommand SetAllAnswers2Yes => new(async () =>
    {
        var supervisor = new Person();

        if (NavigationalParameters.CurrentSelectedJob != null)
            supervisor = App.Database.GetItems<Person>()
                ?.FirstOrDefault(x => x.FocusId == NavigationalParameters.CurrentSelectedJob.SupervisorId);

        if (SelectedPlantItem == null)
        {
            // Need to flash up message that MUST Select a plant item
            await Alert("Please first select an item to Check!", "Error!");
            return;
        }

        if (NavigationalParameters.LoggedInUser.FirstName != null &&
            SelectedPlantItem != null)
        {
            // First off delete any Responses for this item (In AllcurrentResponses & the Db)

            _checkService.DeleteAllInstancesOfThisPlantIdFromDb(
                (int)
                SelectedPlantItem
                    .RemotePlantId);
            // var c4T = _checkService.GetRelevantChecks(SelectedPlantItem.Type);
            if (QuestionCollection != null && QuestionCollection?.Count > 0)
            {
                foreach (var item in QuestionCollection)
                {
                    // Need to create a Yes Response for each item & save it to the Db
                    CurrentCheckAnswer = new Checks4TabletResponses
                    {
                        Qnumber = "",
                        RelevantDate = DateTime.Now,
                        GangLeaderName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName ??
                                         supervisor.FullName,
                        SupervisorName = supervisor.FullName ?? NavigationalParameters.LoggedInUser?.FullName,
                        PlantId = (int)SelectedPlantItem.RemotePlantId,
                        QuestionResponse = "Yes",
                        DateTimeOfCheck = DateTime.Now,
                        QuestionId = item.ListIndex,
                        PlantCheckedByName = "",
                        PlantAssignedToName = SelectedPlantItem.IssuedToName,
                        ChecksStatus = "ChecksConfirmed",
                        ChecksType = NavigationalParameters.SelectetedPlantItem.Group == "Vehicles"
                            ? "Vehicle"
                            : "Plant"
                    };

                    // Its not notifiable so finish the object off & save the data
                    CurrentCheckAnswer.Comments = ""; // No Need Yet
                    CurrentCheckAnswer.PictureFileName = ""; // No Need Yet
                    CurrentCheckAnswer.SignatureFileName = ""; // No Need Yet
                    CurrentCheckAnswer.CheckSubmittedBy = ""; // No Need Yet
                    // Save this change to the DB
                    if (_checkService.CreateDBifNotExists("Checks4TabletResponses"))
                        _checkService.AddSingleChecksResponse(CurrentCheckAnswer);
                }

                RefreshAllCurrentChecks();
            }
            else
            {
                // Cannot find any checks 
                await Alert("Cannot Find any Checks!", "Error!");
            }
        }
    });

    public RelayCommand Submit => new(async () =>
    {
        if (SelectedUser == null)
        {
            await Alert("Error", "Please confirm who is performing the checks on this item!");
        }
        else
        {
            if (AllCurrentResponses.Count() >= QuestionCollection.Count())
            {
                var confirmed = await Alert(
                    "Please Confirm your selections as you will not be able to change this later!",
                    "Confirmation",
                    "OK", "Cancel");

                if (confirmed)
                {
                    ShowSection1 = false;
                    ShowSection2 = false;
                    ShowSection3 = false;
                    ShowSection4 = false;
                    IsLoading = true;

                    await _checkService.ChangeCurrentStatus4PlantId(SelectedPlantItem, AllCurrentResponses,
                        "ChecksConfirmed", SelectedUser);

                    AllCurrentResponses = new List<Checks4TabletResponses>();

                    QuestionCollection = new ObservableCollection<YesNoNaPlantCheckQuestionViewModel>();

                    NavigationalParameters.SelectetedPlantItem.ChecksComplete = true;

                    //time waste
                    await _plantService.AddPlantItem(NavigationalParameters.SelectetedPlantItem);

                    await NavigateTo(ViewModelLocator.MyPlantScreenPage);
                }
            }
            else
            {
                await Alert("Checks Incomplete", "All questins must be complete before submitting the checks", "Ok");
            }
        }
    });

    public RelayCommand Back => new(async () =>
    {
        _checkService.DeleteAllInstancesOfThisPlantIdFromDb((int)NavigationalParameters.SelectetedPlantItem
            .RemotePlantId);

        await NavigateTo(ViewModelLocator.MyPlantScreenPage);
    });

    private void RefreshAllCurrentChecks()
    {
        try
        {
            AllCurrentResponses =
                _checkService.GetAllRelevantResponses4SelectedDate(SelectedPlantItem.RemotePlantId, DateTime.Now);

            var plantChecksList = _checkService.GetRelevantChecks(NavigationalParameters.SelectetedPlantItem.Type);

            foreach (var question in plantChecksList)
            {
                var currentAnswer = AllCurrentResponses?.FirstOrDefault(x => x.QuestionId == question.ListIndex);

                var cq = NavigationalParameters.PlantChecks?.FirstOrDefault(x => x.ListIndex == question.ListIndex);

                if (currentAnswer != null)
                {
                    NavigationalParameters.PlantChecks?.Remove(cq);

                    cq.BtnABgColour = currentAnswer?.QuestionResponse.ToLower() == "yes"
                        ? Color.Green
                        : Color.LightGray;

                    cq.BtnBBgColour = currentAnswer?.QuestionResponse.ToLower() == "no" ? Color.Green : Color.LightGray;

                    cq.BtnCBgColour = currentAnswer?.QuestionResponse.ToLower() == "n/a"
                        ? Color.Green
                        : Color.LightGray;

                    NavigationalParameters.PlantChecks.Add(cq);
                }
            }

            QuestionCollection =
                new ObservableCollection<YesNoNaPlantCheckQuestionViewModel>(
                    NavigationalParameters.PlantChecks?.OrderBy(x => x.ListIndex));
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    [Time]
    public async void GetCurrentAnswer(Button button)
    {
        try
        {
            SelectedQuestion =
                button.CommandParameter as YesNoNaPlantCheckQuestionViewModel;

            CurrentCheckAnswer = _checkService
                .GetAllRelevantResponses4SelectedDate(SelectedPlantItem.RemotePlantId, DateTime.Now)
                .FirstOrDefault(x => x.QuestionId == SelectedQuestion?.ListIndex);

            var supervisor = new Person();

            if (NavigationalParameters.CurrentSelectedJob != null)
                supervisor = App.Database.GetItems<Person>()?.FirstOrDefault(x =>
                    x.FocusId == NavigationalParameters.CurrentSelectedJob.SupervisorId);

            if (CurrentCheckAnswer != null)
                CurrentCheckAnswer.QuestionResponse = button?.Text;
            else
                CurrentCheckAnswer = new Checks4TabletResponses
                {
                    Qnumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                    RelevantDate = DateTime.Now.Date,
                    GangLeaderName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName ?? supervisor.FullName,
                    SupervisorName = supervisor.FullName ?? NavigationalParameters.LoggedInUser?.FullName,
                    PlantId = Convert.ToInt32(NavigationalParameters.SelectetedPlantItem.RemotePlantId),
                    QuestionResponse = button?.Text,
                    DateTimeOfCheck = DateTime.Now,
                    QuestionId = Convert.ToInt32(SelectedQuestion?.ListIndex),
                    PlantCheckedByName = SelectedUser?.FullName,
                    PlantAssignedToName = NavigationalParameters.SelectetedPlantItem?.IssuedToName,
                    ChecksStatus = "ChecksIncomplete",
                    ChecksType = NavigationalParameters.SelectetedPlantItem.Group == "Vehicles" ? "Vehicle" : "Plant"
                };

            _checkService.AddSingleChecksResponse(CurrentCheckAnswer);

            if (SelectedQuestion.NotifiableResponse.ToLower() == button.Text.ToLower())
            {
                NavigationalParameters.CurrentCheckAnswer = CurrentCheckAnswer;
                NavigationalParameters.AllCurrentResponses = AllCurrentResponses;
                await NavigateTo(ViewModelLocator.DailyChecksIssuePage);
            }

            RefreshAllCurrentChecks();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }
}

public class YesNoNaPlantCheckQuestionViewModel : Checks4Tablet
{
    public List<string> Options { get; set; }
    public Color BtnABgColour { get; set; } = Color.LightGray;
    public Color BtnBBgColour { get; set; } = Color.LightGray;
    public Color BtnCBgColour { get; set; } = Color.LightGray;
    public bool ShowButtonA { get; set; } = true;
    public bool ShowButtonB { get; set; } = true;
    public bool ShowButtonC { get; set; } = true;
    public bool IsEnabled { get; set; }
    public Color Checkstaus { get; set; }
}