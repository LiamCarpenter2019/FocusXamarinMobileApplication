using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;

namespace FocusXamarinMobileApplication.ViewModels;

public class SelectStreetPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    public Assignments _assignmentService;

    private bool _isLoading;
    private IGrouping<string, VMexpansionReleaseData> _selectedStreet;
    private bool _showSubmissionButton;
    private bool _showUpload = true;

    private string _streetStatus;
    private string _streetStatusColour;

    public SelectStreetPageViewModel()
    {
        _jobService = new Jobs();

        _assignmentService = new Assignments();
    }

    public bool _showAddStreet { get; set; }

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

    public DataPassed2Server DataPassedToserver { get; set; }

    // private ObservableCollection<Street> _streets;
    public ObservableCollection<IGrouping<string, VMexpansionReleaseData>> _endPoints { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool ShowUpload
    {
        get => _showUpload;
        set
        {
            _showUpload = value;
            OnPropertyChanged();
        }
    }

    public bool ShowAddStreet
    {
        get => _showAddStreet;
        set
        {
            _showAddStreet = value;
            OnPropertyChanged();
        }
    }

    public bool ShowSubmissionButton
    {
        get => _showSubmissionButton;
        set
        {
            _showSubmissionButton = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<IGrouping<string, VMexpansionReleaseData>> EndPoints
    {
        get => _endPoints;
        set
        {
            _endPoints = value;
            OnPropertyChanged();
        }
    }

    public string StreetStatus
    {
        get => _streetStatus;
        set
        {
            _streetStatus = value;
            OnPropertyChanged();
        }
    }

    public string StreetStatusColour
    {
        get => _streetStatusColour;
        set
        {
            _streetStatusColour = value;
            OnPropertyChanged();
        }
    }

    public bool ShowStatus
    {
        get => _showStatus;
        set
        {
            _showStatus = value;
            OnPropertyChanged();
        }
    }

    public IGrouping<string, VMexpansionReleaseData> _StreetSelected { get; set; }

    public IGrouping<string, VMexpansionReleaseData> StreetSelected
    {
        get => _StreetSelected;
        set
        {
            _StreetSelected = value;
            OnPropertyChanged();
        }
    }

    public string _addButtonText { get; set; } = "Select Street";

    public string AddButtonText
    {
        get => _addButtonText;
        set
        {
            _addButtonText = value;
            OnPropertyChanged();
        }
    }

    public string _uploadButtonText { get; set; } = "Upload";

    public string UploadButtonText
    {
        get => _uploadButtonText;
        set
        {
            _uploadButtonText = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        ShowStatus = true;
        ShowUpload = true;
        ShowAddStreet = false;
        AddButtonText = "Add Street";

        switch (NavigationalParameters.AppMode)
        {
            case NavigationalParameters.AppModes.SITECLEAR:
            case NavigationalParameters.AppModes.GANGCLEAR:
                ShowStatus = false;
                ShowUpload = NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.POLEASBUILT
                    ? true
                    : false;
                ShowAddStreet = NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.POLEASBUILT
                    ? false
                    : true;
                break;
            case NavigationalParameters.AppModes.PERMITTODIG:
                ShowAddStreet = true;
                ShowStatus = false;
                ShowUpload = false;
                AddButtonText = "Add Street";
                break;
        }

        EndPointList = _jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob);

        switch (NavigationalParameters.SurveyType)
        {
            case NavigationalParameters.SurveyTypes.SITECLEAR:
                try
                {
                    EndPoints = new ObservableCollection<IGrouping<string, VMexpansionReleaseData>>(EndPointList
                        .Where(x =>
                            x.BuildingStandard?.ToLower() == "street")
                        ?.GroupBy(x => x.StreetName));
                }
                catch (Exception ex)
                {
                    _ = Alert("There are no endpoints available", "No endpoints", "Ok");
                }

                break;
            case NavigationalParameters.SurveyTypes.PRESITECIVILS:
                try
                {
                    EndPoints = new ObservableCollection<IGrouping<string, VMexpansionReleaseData>>(EndPointList
                        .Where(x =>
                            x.BuildingStandard?.ToLower() == "street")
                        ?.GroupBy(x => x.StreetName));
                }
                catch (Exception ex)
                {
                    _ = Alert("There are no endpoints available", "No endpoints", "Ok");
                }

                break;
            default:
                try
                {
                    EndPoints = new ObservableCollection<IGrouping<string, VMexpansionReleaseData>>(EndPointList
                        .Where(x =>
                            x.BuildingStandard?.ToLower() != "premises" &&
                            x.BuildingStandard?.ToLower() != "chamber" &&
                            x.BuildingStandard.ToLower() != "pole")
                        ?.GroupBy(x => x.StreetName));
                }
                catch (Exception ex)
                {
                    _ = Alert("There are no endpoints available", "No endpoints", "Ok");
                }

                break;
        }

        if (EndPoints.Any())
            NavigationalParameters.CurrentEndpointList = true;
        else
            NavigationalParameters.CurrentEndpointList = false;
    });

    public RelayCommand AddStreet => new(async () =>
    {
        NavigationalParameters.MapType = "addnewstreet";

        NavigationalParameters.CurrentAssignment = new Assignment
        {
            Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
            StreetName = NavigationalParameters.SelectedStreet?.FirstOrDefault().StreetName,
            AssignmentId = Guid.NewGuid(),
            Category = NavigationalParameters.SurveyType.ToString().ToUpper(),
            Cnumber = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
            TermContract = NavigationalParameters.CurrentSelectedJob?.BaseContractId,
            ClientName = NavigationalParameters.CurrentSelectedJob?.ClientName,
            Complete = "false",
            LocationName = NavigationalParameters.SelectedStreet?.FirstOrDefault()?.L4Number,
            PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
            Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
            RemoteTableId = 0,
            Type = NavigationalParameters.SurveyType.ToString()
        };

        NavigationalParameters.SelectedAnswer = new SurveyAnswers
        {
            RemoteTableId = 0,
            QuestionId = 0,
            QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
            Notifiable = "true",
            StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
            LocationName = NavigationalParameters.CurrentAssignment?.LocationName,
            Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
            SubmittedDateTime = DateTime.Now,
            CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
            AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId
        };

        NavigationalParameters.SelectedCabinet = new VMl4CabDetail
        {
            Existing = "No",
            LastUpdateTime = DateTime.Now,
            QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
            L4Number = NavigationalParameters.CurrentAssignment?.AssignmentId.ToString(),
            PreSitedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
            CabinetDetails = "Created On Survey"
        };

        NavigationalParameters.SelectedEndPoint = new VMexpansionReleaseData
        {
            Qnumber = Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber),
            Blocked = false,
            L4Number = NavigationalParameters.CurrentAssignment.AssignmentId.ToString(),
            ClaimId = (Guid)NavigationalParameters.SelectedAnswer.Identifier
        };


        if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.CHAMBERAUDIT)
            await NavigateTo(ViewModelLocator.AddChamber);
        if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY)
            await NavigateTo(ViewModelLocator.AddChamber);
        else
            await NavigateTo(ViewModelLocator.FormsMapPage);
    });

    public RelayCommand SelectedStreet => new(async () =>
    {
        try
        {
            NavigationalParameters.PreviewMode = false;
            NavigationalParameters.SelectedAnswer = null;
            NavigationalParameters.SelectedStreet = StreetSelected;
            NavigationalParameters.SelectedEndPoint = NavigationalParameters.SelectedStreet.FirstOrDefault();
            NavigationalParameters.CurrentAssignment = null;

            var ass = _assignmentService.GetCurrentAssignment(
                Convert.ToInt64(NavigationalParameters.SelectedStreet?.FirstOrDefault()?.Qnumber),
                NavigationalParameters.SelectedStreet?.FirstOrDefault().StreetName,
                NavigationalParameters.SurveyType.ToString());

            if (ass != null && ass.PreSiteById == NavigationalParameters.LoggedInUser.FocusId &&
                ass.Complete == "false") NavigationalParameters.CurrentAssignment = ass;

            NavigationalParameters.AnswerList = new List<SurveyAnswers>();

            switch (NavigationalParameters.SurveyType)
            {
                case NavigationalParameters.SurveyTypes.PRESITECIVILS:
                    NavigationalParameters.CurrentAssignment ??= new Assignment
                    {
                        Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                        StreetName = NavigationalParameters.SelectedStreet?.FirstOrDefault().StreetName,
                        AssignmentId = Guid.NewGuid(),
                        Category = NavigationalParameters.SurveyType.ToString(),
                        Cnumber = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                        TermContract = NavigationalParameters.CurrentSelectedJob.BaseContractId,
                        ClientName = NavigationalParameters.CurrentSelectedJob.ClientName,
                        Complete = "false",
                        LocationName = NavigationalParameters.SelectedStreet?.FirstOrDefault()?.L4Number,
                        PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                        Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
                        RemoteTableId = 0,
                        Type = "Civils"
                    };
                    GetSurveyQuestions("civil");
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    break;

                case NavigationalParameters.SurveyTypes.SITECLEAR:
                    NavigationalParameters.CurrentAssignment ??= new Assignment
                    {
                        Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                        StreetName = NavigationalParameters.SelectedStreet?.FirstOrDefault().StreetName,
                        AssignmentId = Guid.NewGuid(),
                        Category = NavigationalParameters.SurveyType.ToString(),
                        Cnumber = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                        TermContract = NavigationalParameters.CurrentSelectedJob.BaseContractId,
                        ClientName = NavigationalParameters.CurrentSelectedJob.ClientName,
                        Complete = "false",
                        PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                        Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
                        RemoteTableId = 0,
                        Type = NavigationalParameters.SurveyType.ToString()
                    };
                    NavigationalParameters.CurrentPermit = new DigPermit
                    {
                        AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                        Distance = 0,
                        FromAddress = "",
                        GpsEnd = "0",
                        GpsStart = "0",
                        RemoteTableId = 0,
                        ToAddress = "",
                        SupervisorFocusId = NavigationalParameters.CurrentSelectedJob.SupervisorId.ToString(),
                        OperativeSignature = "",
                        OperativeFocusId = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                        SupervisorSignature = "",
                        PermitType = NavigationalParameters.SurveyType.ToString()
                    };
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    break;

                case NavigationalParameters.SurveyTypes.PERMITTODIG:
                case NavigationalParameters.SurveyTypes.VERTISHOREPERMIT:
                    NavigationalParameters.CurrentAssignment = new Assignment
                    {
                        Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                        StreetName = NavigationalParameters.SelectedStreet?.FirstOrDefault().StreetName,
                        AssignmentId = Guid.NewGuid(),
                        Category = NavigationalParameters.SurveyType.ToString(),
                        Cnumber = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                        TermContract = NavigationalParameters.CurrentSelectedJob.BaseContractId,
                        ClientName = NavigationalParameters.CurrentSelectedJob.ClientName,
                        Complete = "false",
                        LocationName = NavigationalParameters.SelectedStreet?.FirstOrDefault().L4Number,
                        PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                        Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
                        RemoteTableId = 0,
                        Type = NavigationalParameters.SurveyType.ToString()
                    };
                    NavigationalParameters.CurrentPermit = new DigPermit
                    {
                        AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                        Granted = false,
                        Distance = 0,
                        FromAddress = "",
                        GpsEnd = "0",
                        GpsStart = "0",
                        RemoteTableId = 0,
                        ToAddress = "",
                        SupervisorFocusId = NavigationalParameters.CurrentSelectedJob.SupervisorId.ToString(),
                        OperativeSignature = "",
                        OperativeFocusId = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                        SupervisorSignature = "",
                        PermitType = NavigationalParameters.SurveyType.ToString()
                    };
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    break;
                default:
                    await NavigateTo(ViewModelLocator.SelectStreetPage);
                    break;
            }

            if (NavigationalParameters.CurrentAssignment != null)
                await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await NavigateTo(ViewModelLocator.SelectStreetPage);
        }
    });

    public RelayCommand SubmitPresiteCheck => new(async () =>
    {
        try
        {
            var wa = new WebApi();

            var areWeConnected = await wa.CanWeConnect2Api();
            var dataPassedToserver = new DataPassed2Server();
            var assignmentListToBerUploaded = new List<Assignment>();
            var userChoice = false;

            userChoice = await Alert("Submit Survey?", "Would you like to Upload the survey now?", "Yes", "No");

            var jobService = new Jobs();

            if (userChoice)
            {
                if (areWeConnected)
                {
                    IsLoading = true;

                    ShowUpload = false;

                    // submit presite whern complete
                    dataPassedToserver.JobData = NavigationalParameters.CurrentSelectedJob;

                    dataPassedToserver.Assignments = new List<Assignment>();

                    assignmentListToBerUploaded = _assignmentService.GetAssignmentsToUpload();

                    if (assignmentListToBerUploaded.Any())
                    {
                        foreach (var assignment in assignmentListToBerUploaded)
                        {
                            var answers = (bool)App.Database.GetItems<SurveyAnswers>()?.Any(x =>
                                x.AssignmentId == assignment.AssignmentId && x.RemoteTableId == 0);

                            if (answers)
                                dataPassedToserver.Assignments.Add(
                                    _assignmentService.GenerateAssignments2SaveById(assignment));
                        }

                        dataPassedToserver = jobService.GetNewNewCabinetsAndEndPoints(dataPassedToserver);

                        dataPassedToserver.Category = NavigationalParameters.SurveyType.ToString();

                        var success = await jobService.SaveDailyInputAsync(dataPassedToserver);

                        if (!success)
                        {
                            await Alert("An issue has occured submitting the survey. The survey has been saved",
                                "Error please contact support!");
                            IsLoading = false;
                            ShowUpload = true;
                            NavigateBack();
                        }

                        NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;

                        NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                            await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                        else
                            await NavigateTo(ViewModelLocator.MainListPage);
                    }
                    else
                    {
                        await Alert("There is nothing to upload. ",
                            "Upload  complete");

                        IsLoading = false;

                        ShowUpload = true;
                    }
                }
                else
                {
                    await Alert("Please connect to a network and try again. The surveyhas been saved. ",
                        "No Connectivity");
                    NavigateBack();
                }
            }
            else
            {
                NavigateBack();
            }

            IsLoading = false;
            ShowUpload = true;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await Alert("An issue has occured submitting the survey. This has been saved", "Error!");
            IsLoading = false;
            ShowUpload = true;
        }
    });

    public RelayCommand GoToDesign => new(async () =>
    {
        NavigationalParameters.MapType = "Drawing";
        var docs = new Documents();

        NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                NavigationalParameters.SelectedEndPoint?.Qnumber.ToString(), 0)?
            .FirstOrDefault(x => x.DocumentTitle.ToUpper()
                .Contains($"{NavigationalParameters.SelectedEndPoint?.Qnumber}") && x.DocumentTitle.ToUpper()
                .Contains("HLD"));

        if (NavigationalParameters.SelectedDocument != null)
            await NavigateTo(ViewModelLocator.DocumentViewPage);
        else
            await Alert("Document not found!",
                "The Drawing is missing please enusre the document exsists in the project documents mapping folder in not please contast support!");
    });

    public RelayCommand GoBack => new(async () =>
    {
        switch (NavigationalParameters.SurveyType)
        {
            case NavigationalParameters.SurveyTypes.GANGCLEAR:
            case NavigationalParameters.SurveyTypes.CHAMBERAUDIT:

            case NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY:
            //await NavigateTo(ViewModelLocator.MenuSelectionPage);
            //break;
            case NavigationalParameters.SurveyTypes.PERMITTODIG:

            case NavigationalParameters.SurveyTypes.POLEASBUILT:
            case NavigationalParameters.SurveyTypes.PRESITECIVILS:
            case NavigationalParameters.SurveyTypes.PRESITEPREMISES:
            case NavigationalParameters.SurveyTypes.VERTISHOREPERMIT:
                await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                break;
            case NavigationalParameters.SurveyTypes.SITECLEAR:
                await NavigateTo(ViewModelLocator.MenuSelectionPage);

                break;
            default:
                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                {
                    NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;

                    await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                }
                else
                {
                    await NavigateTo(ViewModelLocator.MainListPage);
                }

                break;
        }
    });

    public RelayCommand Refresh => new(() => { OnPropertyChanged("ShowSubmissionButton"); });

    public bool _showStatus { get; set; } = true;
    public List<VMexpansionReleaseData> EndPointList { get; private set; }
    public List<SurveyAnswers> SurveyAnswers { get; private set; }

    private void GetSurveyQuestions(string questionType)
    {
        try
        {
            var questions = _assignmentService.GetSurveyQuestions(NavigationalParameters.SurveyType.ToString().ToLower()).OrderBy(x => x.QuestionId);

            var surveyQuestions = new List<SurveyQuestion>();
            var currentAnswer = new SurveyAnswers();
            NavigationalParameters.PreSiteQuestions = new List<SurveyQuestion>();
            NavigationalParameters.GenericQuestions = new ObservableCollection<SurveyQuestion>();

            SurveyAnswers =
                _assignmentService.GetRelevantResponses(NavigationalParameters.CurrentAssignment.AssignmentId) ??
                new List<SurveyAnswers>();

            foreach (var question in questions)
            {
                if (SurveyAnswers.Any())
                    if (SurveyAnswers.Any(x => x.QuestionId == question.QuestionId))
                        currentAnswer = SurveyAnswers?.FirstOrDefault(x => x.QuestionId == question.QuestionId && x.Category == question.Category);

                var keyAnswers = question?.KeyAnswer.Split(',').ToList();

                switch (question?.ResponseType)
                {
                    case "Y/N Selection":
                        {
                            try
                            {
                                var questionToAdd = new YesNoNaQuestionViewModel
                                {
                                    Question = question.Question,
                                    QuestionId = question.QuestionId,
                                    KeyAnswer = question.KeyAnswer,
                                    Category = question.Category,
                                    DepthorRating = question.DepthorRating,
                                    FollowUpAction = question.FollowUpAction,
                                    FurtherQuestionId = question.FurtherQuestionId,
                                    Grouping = question.Grouping,
                                    InformationTo = question.InformationTo,
                                    NotifiableResponse = question.NotifiableResponse,
                                    QuestionOptions = question.QuestionOptions,
                                    ResponseType = question.ResponseType,
                                    Id = question.Id,
                                    Stage = question.Stage,
                                    BtnYesText = question.QuestionOptions.Split(',')[0],
                                    BtnNoText = question.QuestionOptions.Split(',')[1],
                                    BtnNaText = question.QuestionOptions.Split(',')[2],
                                    IsEnabled = true,
                                    ShowButtonA = question.QuestionId == 0.10M ? false : true,
                                    ShowButtonB = question.QuestionId == 0.10M ? false : true,
                                    ShowButtonC = question.QuestionId == 0.10M ? false : true,
                                    BtnNaBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(question.QuestionOptions.Split(',')[2].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnNoBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(question.QuestionOptions.Split(',')[1].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnYesBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(question.QuestionOptions.Split(',')[0].ToLower())
                                            ? Color.Green
                                            : Color.LightGray
                                };

                                surveyQuestions.Add(questionToAdd);

                                if (questionToAdd.DepthorRating == 1)
                                    if (NavigationalParameters.PreSiteQuestions.All(x =>
                                            x.QuestionId != questionToAdd.QuestionId))
                                        NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                            }
                        }
                        break;

                    case "multiSelector":
                        {
                            try
                            {
                                var optionsTemp = question?.QuestionOptions.Split(',').ToList();

                                while (optionsTemp.Count < 12) optionsTemp.Add("");

                                var questionToAdd = new MultiQuestionViewModel
                                {
                                    Question = question.Question,
                                    QuestionId = question.QuestionId,
                                    KeyAnswer = question.KeyAnswer,
                                    Category = question.Category,
                                    DepthorRating = question.DepthorRating,
                                    FollowUpAction = question.FollowUpAction,
                                    FurtherQuestionId = question.FurtherQuestionId,
                                    Grouping = question.Grouping,
                                    InformationTo = question.InformationTo,
                                    NotifiableResponse = question.NotifiableResponse,
                                    QuestionOptions = question.QuestionOptions,
                                    ResponseType = question.ResponseType,
                                    Id = question.Id,
                                    Stage = question.Stage,
                                    BtnAText = !string.IsNullOrEmpty(optionsTemp[0]) ? optionsTemp[0] : "",
                                    BtnBText = !string.IsNullOrEmpty(optionsTemp[1]) ? optionsTemp[1] : "",
                                    BtnCText = !string.IsNullOrEmpty(optionsTemp[2]) ? optionsTemp[2] : "",
                                    BtnDText = !string.IsNullOrEmpty(optionsTemp[3]) ? optionsTemp[3] : "",
                                    BtnEText = !string.IsNullOrEmpty(optionsTemp[4]) ? optionsTemp[4] : "",
                                    BtnFText = !string.IsNullOrEmpty(optionsTemp[5]) ? optionsTemp[5] : "",
                                    BtnGText = !string.IsNullOrEmpty(optionsTemp[6]) ? optionsTemp[6] : "",
                                    BtnHText = !string.IsNullOrEmpty(optionsTemp[7]) ? optionsTemp[7] : "",
                                    BtnIText = !string.IsNullOrEmpty(optionsTemp[8]) ? optionsTemp[8] : "",
                                    BtnJText = !string.IsNullOrEmpty(optionsTemp[9]) ? optionsTemp[9] : "",
                                    BtnKText = !string.IsNullOrEmpty(optionsTemp[10]) ? optionsTemp[10] : "",
                                    BtnLText = !string.IsNullOrEmpty(optionsTemp[11]) ? optionsTemp[11] : "",
                                    BtnABgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[0].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnBBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[1].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnCBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[2].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnDBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[3].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnEBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[4].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnFBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[5].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnGBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[6].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnHBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[7].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnIBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[8].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnJBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[9].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnKBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[10].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnLBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[11].ToLower())
                                            ? Color.Green
                                            : Color.LightGray,
                                    BtnAHidden = optionsTemp[0] != "",
                                    BtnBHidden = optionsTemp[1] != "",
                                    BtnCHidden = optionsTemp[2] != "",
                                    BtnDHidden = optionsTemp[3] != "",
                                    BtnEHidden = optionsTemp[4] != "",
                                    BtnFHidden = optionsTemp[5] != "",
                                    BtnGHidden = optionsTemp[6] != "",
                                    BtnHHidden = optionsTemp[7] != "",
                                    BtnIHidden = optionsTemp[8] != "",
                                    BtnJHidden = optionsTemp[9] != "",
                                    BtnKHidden = optionsTemp[10] != "",
                                    BtnLHidden = optionsTemp[11] != "",
                                    IsEnabled = true
                                };

                                surveyQuestions.Add(questionToAdd);

                                if (questionToAdd.DepthorRating == 1)
                                    if (NavigationalParameters.PreSiteQuestions.All(x =>
                                            x.QuestionId != questionToAdd.QuestionId))
                                        NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                            }
                        }
                        break;
                    default:
                        {
                            var questionToAdd = new LocationIdentityQuestionViewModel
                            {
                                Question = question.Question,
                                QuestionId = question.QuestionId,
                                KeyAnswer = question.KeyAnswer,
                                Category = question.Category,
                                DepthorRating = question.DepthorRating,
                                FollowUpAction = question.FollowUpAction,
                                FurtherQuestionId = question.FurtherQuestionId,
                                Grouping = question.Grouping,
                                InformationTo = question.InformationTo,
                                NotifiableResponse = question.NotifiableResponse,
                                QuestionOptions = question.QuestionOptions,
                                ResponseType = question.ResponseType,
                                Id = question.Id,
                                Stage = question.Stage,
                                IsEnabled = true
                            };

                            surveyQuestions.Add(questionToAdd);

                            if (questionToAdd.DepthorRating == 1)
                                if (NavigationalParameters.PreSiteQuestions.All(x =>
                                        x.QuestionId != questionToAdd.QuestionId))
                                    NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                        }
                        break;
                }

                currentAnswer = null;
            }

            NavigationalParameters.GenericQuestions = new ObservableCollection<SurveyQuestion>(surveyQuestions);

            var answerGroup = SurveyAnswers.OrderByDescending(x => x.SubmittedDateTime)?.GroupBy(x => x.QuestionId);

            foreach (var group in answerGroup)
                if (NavigationalParameters.PreSiteQuestions.Any(x => x.QuestionId == group.FirstOrDefault().QuestionId))
                {
                    var qs = NavigationalParameters.PreSiteQuestions?.FirstOrDefault(x =>
                        x.QuestionId == group.FirstOrDefault().QuestionId);

                    var keyAnswers = qs?.KeyAnswer?.Split(',');

                    foreach (var a in group.FirstOrDefault().AnswerGiven.Split(','))
                        if (keyAnswers.Contains(a))
                            if (NavigationalParameters.PreSiteQuestions.All(x => x.QuestionId != qs.QuestionId))
                                NavigationalParameters.PreSiteQuestions?.Add(
                                    NavigationalParameters.GenericQuestions?.FirstOrDefault(x =>
                                        x.QuestionId == qs.FurtherQuestionId));
                }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    public void RefreshEndpoints()
    {
        ShowUpload = true;


        if (EndPoints.Any())
            // Streets = new ObservableCollection<Street>();

            foreach (var item in EndPoints)
            {
                var status = "";
                var streetStatusColour = "";

                var exsistingPreSite =
                    _assignmentService.CheckAssignmentStatus(NavigationalParameters.CurrentSelectedJob, item.Key,
                        NavigationalParameters.SurveyType.ToString());

                if (exsistingPreSite == null)
                {
                    status = "";
                    streetStatusColour = "Green";
                }
                else
                {
                    if (NavigationalParameters.AppMode != NavigationalParameters.AppModes.SITECLEAR
                        || NavigationalParameters.AppMode != NavigationalParameters.AppModes.PERMITTODIG)
                    {
                        if (exsistingPreSite.CompletedOn > DateTime.Parse("01/01/1900 00:00:00") ||
                            exsistingPreSite.RemoteTableId > 0)
                        {
                            status = "In Progress";
                            streetStatusColour = "Green";
                            ShowSubmissionButton = true;
                        }
                        else
                        {
                            status = "In Progress";
                            streetStatusColour = "Red";
                            ShowSubmissionButton = true;
                        }
                    }
                }

                //Streets.Add(new Street
                //{
                //    StreetName = item.Key,
                //    Enpoints = item,
                //    Status = status,
                //    StreetStatusColour = streetStatusColour
                //});
            }
        else
            NavigationalParameters.CurrentEndpointList = false;
    }
}