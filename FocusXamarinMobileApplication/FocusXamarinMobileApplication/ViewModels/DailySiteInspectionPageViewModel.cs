#region

using Location = Xamarin.Essentials.Location;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class DailySiteInspectionPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Assignments _assignmentService;
    public readonly Checks _checkService;
    private readonly Jobs _jobService;

    public readonly Users _userService;
    private RelayCommand<string> _dailiesSegmentedCheckChangedCommand;


    public DailySiteInspectionPageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        _checkService = new Checks();
        _pictureService = new Pictures();
        _wa = new WebApi();
        IsLoading = false;
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
    }

    public Pictures _pictureService { get; }
    public WebApi _wa { get; }

    public string ProjectName => NavigationalParameters.CurrentSelectedJob?.ProjectName;
    public string ProjectDate => $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";

    public Checks4TabletResponses NewResponse2Add { get; private set; }

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

    public bool _showUploadButton { get; set; }

    public bool ShowUploadButton
    {
        get => _showUploadButton;
        set
        {
            _showUploadButton = value;
            OnPropertyChanged();
        }
    }

    public bool _showSaveButton { get; set; }

    public bool ShowSaveButton
    {
        get => _showSaveButton;
        set
        {
            _showSaveButton = value;
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

    public bool _allChecksCompleted { get; set; }

    public bool AllChecksCompleted
    {
        get => _allChecksCompleted;
        set
        {
            _allChecksCompleted = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<YesNoNaQuestionViewModel> _yesNoCollection { get; set; }

    public ObservableCollection<YesNoNaQuestionViewModel> YesNoCollection
    {
        get => _yesNoCollection;
        set
        {
            _yesNoCollection = value;
            OnPropertyChanged();
        }
    }

    public List<Checks4TabletResponses> AllCurrentResponses { get; set; }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ShowSaveButton = false;
        ShowUploadButton = false;
        YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(NavigationalParameters.YesNoCollection);

        RefreshAllCurrentDailyChecks();
    });

    public RelayCommand PostAllChecksCommand => new(async () =>
    {
        try
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                NavigationalParameters.AuthDetail.Type = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
                NavigationalParameters.AuthorisationType =
                    NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
                NavigationalParameters.AuthDetail.SignatureFolderName = "JobPictures";
                await NavigateTo(ViewModelLocator.SignaturePage);
            }
            else
            {
                await Alert("No Internect Connectivity",
                    "There is no internet connection please find better connectity and try again", "Ok");
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand UploadAllChecksCommand => new(async () =>
    {
        var signatureFilename = "";
        var selectedCheckerName = NavigationalParameters.LoggedInUser.FullName;
        var responsesToUpload = new List<Checks4TabletResponses>();

        if (NavigationalParameters.MultSignatures != null &&
            NavigationalParameters.MultSignatures.All(x => !string.IsNullOrEmpty(x.Signature)))
        {
            var areWeConnected = await _wa.CanWeConnect2Api();

            AllCurrentResponses = _checkService.GetAllRelevantResponses4Dalies(
                    NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                    NavigationalParameters.CurrentSelectedJob.JobDate,
                    NavigationalParameters.CurrentSelectedJob.GangLeaderName)?
                .Where(x => x.ChecksStatus == "ChecksIncomplete")?.ToList();

            foreach (var signature in NavigationalParameters.MultSignatures)
            {
                signatureFilename += $"{signature.Signature},";

                NavigationalParameters.MultSignatures.FirstOrDefault(x => x.FocusId == signature.FocusId).HasSigned =
                    false;
            }

            if (areWeConnected)
            {
                var checkingFlag = string.Empty;
                Location location = null;
                var counter = 2;
                //try
                //{
                //    location = await Geolocation.GetLocationAsync();
                //}
                //catch (Exception ex)
                //{

                //        await Alert("No Location", "An error occured while attempring to get the location", "Ok");


                //}

                foreach (var checkItem in AllCurrentResponses)
                {
                    checkItem.ChecksStatus = "ChecksPosted";
                    checkItem.ChecksType = "Dailies";
                    checkItem.DifferentCheckIndicator = counter++;
                    checkItem.SignatureFileName = signatureFilename;
                    checkItem.CheckSubmittedBy = NavigationalParameters.LoggedInUser?.FullName;
                    // checkItem.Location = $"{location?.Latitude} {location?.Longitude}";

                    _checkService.AddSingleChecksResponse(checkItem);

                    responsesToUpload.Add(checkItem);

                    if (!string.IsNullOrEmpty(checkItem.PictureFileName))
                    {
                        checkingFlag +=
                            await SendPicture2Azure("JobPictures", "PicsFromIpad", checkItem?.PictureFileName);

                        try
                        {
                            var pic = _pictureService.GetAllPictures()
                                .FirstOrDefault(X => X.FileName == checkItem.PictureFileName);
                            pic.SeverPictureId = 1;

                            await _pictureService.AddPicture(pic);
                        }
                        catch (Exception ex)
                        {
                            Analytics.TrackEvent(
                                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                        }
                    }
                }

                if (checkingFlag.Contains("Not Transferred 2 Storage"))
                {
                    await Alert(
                        "Unable to save data to the Server at this time, please try to find better connectivity and try again!",
                        "Error!");

                    IsLoading = false;
                }
                else
                {
                    var serverId = await _wa.SaveChecksData2Server(responsesToUpload);

                    // If get back server Id then all is good so annotate Plant item & move it fromMy Plant tO Transferred Plant
                    if (serverId > 0)
                    {
                        IsLoading = false;
                        // Success in saving data 2 server now sort databases & show data correctly
                        await Alert("Checks saved to the server!",
                            "Success!");

                        NavigationalParameters.CurrentSelectedJob.DailyChecksPosted = true;
                    }
                    else
                    {
                        await Alert(
                            "Unable to save data to the Server, please try to find better connectivity and try again!",
                            "Error!");

                        NavigationalParameters.CurrentSelectedJob.DailyChecksPosted = false;
                    }

                    await _jobService.UpdateJob(NavigationalParameters.CurrentSelectedJob);

                    await NavigateTo(ViewModelLocator.MenuSelectionPage);
                }
            }
            else
            {
                await Alert(
                    "Unable to save data to the Server, please try to find better connectivity and try again!",
                    "Error!");
            }
        }
    });

    public RelayCommand AddNewRiskCommand => new(async () =>
    {
        try
        {
            await NavigateTo(ViewModelLocator.AdditionalRisksPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand CheckCompletedCommand => new(async () =>
    {
        try
        {
            var answer = await Alert("are you sure you wish to save your selections to the server?", "Confirmation",
                "Yes", "No");

            if (answer)
            {
                //await _checkService.ChangeCurrentStatus4Dailies(NavigationalParameters.CurrentSelectedJob,
                //    AllCurrentResponses,
                //    "ChecksConfirmed");

                //RefreshAllCurrentDailyChecks();

                NavigationalParameters.CurrentSelectedJob.DailyChecksCompleted = true;

                await _jobService.UpdateJob(NavigationalParameters.CurrentSelectedJob);

                PostAllChecksCommand.Execute(null);

                NavigationalParameters.CurrentSelectedJob.DailyChecksPosted = true;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand Back => new(async () =>
    {
        NavigationalParameters.CurrentSelectedJob.DailyChecksCompleted = false;
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    public async void GetCurrentAnswer(Button button)
    {
        try
        {
            NavigationalParameters.SelectedQuestion =
                button.CommandParameter as YesNoNaQuestionViewModel;

            if (NavigationalParameters.SelectedQuestion != null)
            {
                var answer = AllCurrentResponses?
                    .FirstOrDefault(x =>
                        x.QuestionId == NavigationalParameters.SelectedQuestion.QuestionId &&
                        x.ChecksStatus == "ChecksIncomplete");

                if (answer == null || answer?.QuestionResponse.ToLower() != button.Text.ToLower())
                {
                    var supervisorName = new Person();

                    if (NavigationalParameters.CurrentSelectedJob != null)
                        supervisorName = App.Database.GetItems<Person>()?.FirstOrDefault(x =>
                            x.FocusId == NavigationalParameters.CurrentSelectedJob?.SupervisorId);

                    if (answer != null)
                    {
                        AllCurrentResponses
                            .FirstOrDefault(x => x.QuestionId == NavigationalParameters.SelectedQuestion.QuestionId
                                                 && x.ChecksStatus == "ChecksIncomplete" && x.Id == answer.Id)
                            .QuestionResponse = button.Text;
                    }
                    else
                    {
                        answer = new Checks4TabletResponses
                        {
                            Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber
                                .ToString(),
                            RelevantDate = NavigationalParameters.CurrentSelectedJob.JobDate,
                            GangLeaderName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName,
                            SupervisorName = $"{supervisorName?.FirstName} {supervisorName?.Surname}",
                            PlantId = 0,
                            QuestionResponse = button.Text,
                            DateTimeOfCheck = DateTime.Now,
                            QuestionId = Convert.ToInt32(NavigationalParameters.SelectedQuestion.QuestionId),
                            PlantCheckedByName = "",
                            PlantAssignedToName = "",
                            ChecksType = "Dailies",
                            ChecksStatus = "ChecksIncomplete"
                        };

                        AllCurrentResponses.Add(answer);
                    }

                    _checkService.AddSingleChecksResponse(answer);

                    NavigationalParameters.CurrentCheckAnswer = answer;

                    try
                    {
                        if (NavigationalParameters.CurrentCheckAnswer.QuestionId > 0)
                        {
                            //   var q = QuestionCollection?.FirstOrDefault(x => x.QuestionId == NavigationalParameters.CurrentCheckAnswer.QuestionId);

                            var questionToDelete = YesNoCollection.FirstOrDefault(x =>
                                x.QuestionId == NavigationalParameters.CurrentCheckAnswer?.QuestionId);

                            YesNoCollection.Remove(questionToDelete);

                            questionToDelete.BtnYesBgColour =
                                NavigationalParameters.CurrentCheckAnswer.QuestionResponse.ToLower().Contains("yes")
                                    ? Color.LawnGreen
                                    : Color.LightGray;

                            questionToDelete.BtnNoBgColour =
                                NavigationalParameters.CurrentCheckAnswer.QuestionResponse.ToLower().Contains("no")
                                    ? Color.LawnGreen
                                    : Color.LightGray;

                            questionToDelete.BtnNaBgColour =
                                NavigationalParameters.CurrentCheckAnswer.QuestionResponse.ToLower().Contains("n/a")
                                    ? Color.LawnGreen
                                    : Color.LightGray;

                            YesNoCollection.Add(questionToDelete);
                        }
                    }
                    catch (Exception ex)
                    {
                        var exs = ex.ToString();
                    }


                    RefreshAllCurrentDailyChecks();

                    if (NavigationalParameters.SelectedQuestion.KeyAnswer.ToLower() == button.Text.ToLower())
                        //NavigationalParameters.NavigationParameter = NavigationalParameters.CurrentCheckAnswer;

                        await NavigateTo(ViewModelLocator.DailyChecksIssuePage);
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    private async Task<string> SendPicture2Azure(string localStoragePath, string azurePath,
        string filenameOfPic2Send)
    {
        // First off try & sync 2 Azure
        var p = new Pictures();
        var transferCheck = await p.SyncPicture2Azure(localStoragePath, azurePath, filenameOfPic2Send);
        if (transferCheck == "Pic Transferred OK")
            return "Transferred 2 Storage Successfully";
        return "Not Transferred 2 Storage";
    }

    private void RefreshAllCurrentDailyChecks()
    {
        try
        {
            AllCurrentResponses = _checkService.GetAllRelevantResponses4Dalies(
                NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                NavigationalParameters.CurrentSelectedJob.JobDate,
                NavigationalParameters.CurrentSelectedJob.GangLeaderName) ?? new List<Checks4TabletResponses>();

            if (YesNoCollection != null && YesNoCollection.Any()
                                        && AllCurrentResponses != null && AllCurrentResponses.Any())
                if (AllCurrentResponses?.Count() >= YesNoCollection?.Count())
                {
                    NavigationalParameters.CurrentSelectedJob.DailyChecksCompleted =
                        AreAllDailyChecksCompleted(YesNoCollection, AllCurrentResponses);

                    if (NavigationalParameters.CurrentSelectedJob.DailyChecksCompleted)
                    {
                        AllChecksCompleted = true;

                        if (AllCurrentResponses.All(x => x.ChecksStatus == "ChecksPosted"))
                        {
                            ShowSaveButton = false;
                            ShowUploadButton = false;
                            ShowSection3 = false;
                        }
                        else
                        {
                            ShowSaveButton = true;
                            ShowUploadButton = true;
                            ShowSection3 = true;
                        }
                    }
                    else
                    {
                        AllChecksCompleted = false;
                    }
                }

            NavigationalParameters.YesNoCollection =
                new ObservableCollection<YesNoNaQuestionViewModel>(YesNoCollection.OrderBy(x => x.QuestionId));
            NavigationalParameters.AllCurrentResponses = AllCurrentResponses;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    private bool AreAllDailyChecksCompleted(
        ObservableCollection<YesNoNaQuestionViewModel> allTheChecks,
        List<Checks4TabletResponses> allTheResponses)
    {
        if (allTheResponses.Any())
            foreach (var checkItem in allTheChecks)
                if (checkItem.QuestionId > 0 && allTheResponses.All(x => x.QuestionId != checkItem.QuestionId))
                    return false;

        return true;
    }

    public bool ConfirmAuthorised()
    {
        return NavigationalParameters.AuthDetail.DetailsCorrect();
    }
}

//public class YesNoNaSiteInspectionQuestionViewModel : SurveyQuestion
//{
//    public List<string> Options { get; set; }
//    public Color BtnABgColour { get; set; } = Color.LightGray;
//    public Color BtnBBgColour { get; set; } = Color.LightGray;
//    public Color BtnCBgColour { get; set; } = Color.LightGray;
//    public bool ShowButtonA { get; set; } = true;
//    public bool ShowButtonB { get; set; } = true;
//    public bool ShowButtonC { get; set; } = true;
//    public bool IsEnabled { get; set; }
//}