#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Audit = FocusXamarinMobileApplication.Models.Audit;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SiteInspectionResultPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;
    private readonly Users _userService;

    private ImageSource Rating1;
    private ImageSource Rating10;
    private ImageSource Rating2;
    private ImageSource Rating3;
    private ImageSource Rating4;
    private ImageSource Rating5;
    private ImageSource Rating6;
    private ImageSource Rating7;
    private ImageSource Rating8;
    private ImageSource Rating9;
    private ImageSource RatingNA;

    public SiteInspectionResultPageViewModel()
    {
        _userService = new Users();

        _jobService = new Jobs();

        _assignmentService = new Assignments();
    }

    public JobData4Tablet CurrentSelectedJob { get; private set; }

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

    public string AuditDate { get; set; }


    public decimal TotalSectionTotal1 { get; set; }
    public decimal TotalSectionScore1 { get; set; }
    public decimal TotalSectionTotal2 { get; set; }
    public decimal TotalSectionScore2 { get; set; }
    public decimal TotalSectionTotal3 { get; set; }
    public decimal TotalSectionScore3 { get; set; }
    public decimal TotalSectionTotal4 { get; set; }
    public decimal TotalSectionScore4 { get; set; }
    public decimal TotalSectionScore5 { get; set; }
    public decimal TotalSectionTotal5 { get; set; }
    public decimal TotalSectionTotal6 { get; set; }
    public decimal TotalSectionScore6 { get; set; }
    public decimal TotalSectionTotal7 { get; set; }
    public decimal TotalSectionScore7 { get; set; }
    public decimal TotalSectionTotal8 { get; set; }
    public decimal TotalSectionScore8 { get; set; }
    public decimal TotalSectionTotal9 { get; set; }
    public decimal TotalSectionScore9 { get; set; }
    public decimal TotalSectionTotal10 { get; set; }
    public decimal TotalSectionScore10 { get; set; }

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

    public string _timeTaken { get; set; }

    public string TimeTaken
    {
        get => _timeTaken;
        set
        {
            _timeTaken = value;
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

    public string[] _options { get; set; }

    public string[] Options
    {
        get => _options;
        set
        {
            _options = value;
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

    public bool _showSection5 { get; set; }

    public bool ShowSection5
    {
        get => _showSection5;
        set
        {
            _showSection5 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection6 { get; set; }

    public bool ShowSection6
    {
        get => _showSection6;
        set
        {
            _showSection6 = value;
            OnPropertyChanged();
        }
    }

    public string _gangLeaderName { get; set; }

    public string GangLeaderName
    {
        get => _gangLeaderName;
        set
        {
            _gangLeaderName = value;
            OnPropertyChanged();
        }
    }

    public string _startTime { get; set; }

    public string StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;
            OnPropertyChanged();
        }
    }

    public string _endTime { get; set; }

    public string EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;
            OnPropertyChanged();
        }
    }

    public string _timeTalen { get; set; }

    public string TimeTalen
    {
        get => _timeTalen;
        set
        {
            _timeTalen = value;
            OnPropertyChanged();
        }
    }

    public string _gpsCoOrds { get; set; }

    public string GpsCoOrds
    {
        get => _gpsCoOrds;
        set
        {
            _gpsCoOrds = value;
            OnPropertyChanged();
        }
    }

    public string _overallscore { get; set; }

    public string Overallscore
    {
        get => _overallscore;
        set
        {
            _overallscore = value;
            OnPropertyChanged();
        }
    }

    public string _nonConformance { get; set; }

    public string NonConformance
    {
        get => _nonConformance;
        set
        {
            _nonConformance = value;
            OnPropertyChanged();
        }
    }

    public string _inadequiecies { get; set; }

    public string Inadequiecies
    {
        get => _inadequiecies;
        set
        {
            _inadequiecies = value;
            OnPropertyChanged();
        }
    }

    //========

    public string _generalSiteTidiness { get; set; }

    public string GeneralSiteTidiness
    {
        get => _generalSiteTidiness;
        set
        {
            _generalSiteTidiness = value;
            OnPropertyChanged();
        }
    }

    public string _documentation { get; set; }

    public string Documentation
    {
        get => _documentation;
        set
        {
            _documentation = value;
            OnPropertyChanged();
        }
    }

    public string _equpPlantVeh { get; set; }

    public string EqupPlantVeh
    {
        get => _equpPlantVeh;
        set
        {
            _equpPlantVeh = value;
            OnPropertyChanged();
        }
    }

    public string _pPE { get; set; }

    public string PPE
    {
        get => _pPE;
        set
        {
            _pPE = value;
            OnPropertyChanged();
        }
    }

    public string _highwayWorking { get; set; }

    public string HighwayWorking
    {
        get => _highwayWorking;
        set
        {
            _highwayWorking = value;
            OnPropertyChanged();
        }
    }

    public string _underGroundUtilities { get; set; }

    public string UnderGroundUtilities
    {
        get => _underGroundUtilities;
        set
        {
            _underGroundUtilities = value;
            OnPropertyChanged();
        }
    }

    public string _excavationChambers { get; set; }

    public string ExcavationChambers
    {
        get => _excavationChambers;
        set
        {
            _excavationChambers = value;
            OnPropertyChanged();
        }
    }

    public string _enviromentalWasteManaegment { get; set; }

    public string EnviromentalWasteManaegment
    {
        get => _enviromentalWasteManaegment;
        set
        {
            _enviromentalWasteManaegment = value;
            OnPropertyChanged();
        }
    }

    public string _methodStatement { get; set; }

    public string MethodStatement
    {
        get => _methodStatement;
        set
        {
            _methodStatement = value;
            OnPropertyChanged();
        }
    }

    public string _comments { get; set; }

    public string Comments
    {
        get => _comments;
        set
        {
            _comments = value;
            OnPropertyChanged();
        }
    }

    public Assignment _currentAssignment { get; set; }

    public Assignment CurrentAssignment
    {
        get => _currentAssignment;
        set
        {
            _currentAssignment = value;
            OnPropertyChanged();
        }
    }

    public Audit _currentAudit { get; set; }

    public Audit CurrentAudit
    {
        get => _currentAudit;
        set
        {
            _currentAudit = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<RatingQuestionViewModel> _ratingQuestions { get; set; }

    public ObservableCollection<RatingQuestionViewModel> RatingQuestions
    {
        get => _ratingQuestions;
        set
        {
            _ratingQuestions = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        try
        {
            Title = "Audit";

            CurrentSelectedJob = NavigationalParameters.CurrentSelectedJob;

            CurrentAudit = NavigationalParameters.CurrentAudit;

            ProjectName = CurrentSelectedJob?.ProjectName;

            ProjectDate = CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

            NavigationalParameters.RatingQuestions = RatingQuestions = new ObservableCollection<RatingQuestionViewModel>(
                _assignmentService.GetSurveyQuestions("audit")
                    .Where(x => x.Category.ToLower() == "audit")
                    .Select(question => new RatingQuestionViewModel
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
                        IsEnabled = true,
                        Rating10 = SimpleStaticHelpers.GetImage("10grey"),
                        Rating9 = SimpleStaticHelpers.GetImage("9grey"),
                        Rating8 = SimpleStaticHelpers.GetImage("8grey"),
                        Rating7 = SimpleStaticHelpers.GetImage("7grey"),
                        Rating6 = SimpleStaticHelpers.GetImage("6orbelowgrey"),
                        Rating5 = SimpleStaticHelpers.GetImage("5grey"),
                        Rating4 = SimpleStaticHelpers.GetImage("4grey"),
                        Rating3 = SimpleStaticHelpers.GetImage("3grey"),
                        Rating2 = SimpleStaticHelpers.GetImage("2grey"),
                        Rating1 = SimpleStaticHelpers.GetImage("1grey"),
                        RatingNA = SimpleStaticHelpers.GetImage("NAgrey")
                    }));

            var auditItems = new List<Audit>();

            var assignments = _jobService?.GetAudits(CurrentSelectedJob)
                ?.Where(x => x.PreSiteById == CurrentSelectedJob?.GangLeaderId)
                .OrderByDescending(x => x.CompletedOn)?.ToList();

            if (!assignments.Any() && NavigationalParameters.CurrentAssignment != null) { assignments = new List<Assignment>() { NavigationalParameters.CurrentAssignment }; }

            if (assignments.Any())
            {
                if (CurrentAudit != null)
                {
                    CurrentAssignment = assignments?.FirstOrDefault(x =>
                        x.AssignmentId == CurrentAudit?.AssignmentId && x.Complete == "false");

                    if (CurrentAssignment == null)
                        CurrentAssignment = assignments?.FirstOrDefault(x => x.AssignmentId == CurrentAudit?.AssignmentId);
                }
                else if (CurrentAudit == null)
                {
                    CurrentAssignment = assignments?.FirstOrDefault();
                }

                switch (CurrentAssignment?.Complete.ToLower())
                {
                    case "true" when CurrentAssignment?.RemoteTableId == 0:
                        {
                            NavigationalParameters.AuditStatus =
                                NavigationalParameters.AuditStatuses.SIGNOFF;

                            CurrentAudit = CurrentAudit ?? CurrentAssignment.Audit
                                ?? CurrentAssignment?.Audits?.OrderByDescending(x => x.StartTime)
                                    .FirstOrDefault(x => x.RemoteTableId == 0);

                            CurrentAudit.NcrList = _assignmentService.GetNcrList(CurrentAudit.AuditId);

                            SubmitButtonText = "Upload Inspection";
                            break;
                        }
                    case "false" when CurrentAssignment.RemoteTableId == 0:
                        {
                            NavigationalParameters.AuditStatus = NavigationalParameters.AuditStatuses.INCOMPLETE;

                            SubmitButtonText = "Continue Inspection";

                            if (CurrentAudit == null)
                                CurrentAudit = CurrentAssignment?.Audit ?? CurrentAssignment?.Audits
                                    ?.OrderByDescending(x => x.StartTime).FirstOrDefault(x => x.RemoteTableId == 0);

                            CurrentAssignment.Audit.Answers =
                                _assignmentService.GetRelevantAssignmentsResponses(CurrentAssignment.AssignmentId);

                            if (CurrentAssignment.Audit.Answers.Any())
                                foreach (var answer in CurrentAssignment.Audit.Answers)
                                {
                                    Rating10 = SimpleStaticHelpers.GetImage("10grey");
                                    Rating9 = SimpleStaticHelpers.GetImage("9grey");
                                    Rating8 = SimpleStaticHelpers.GetImage("8grey");
                                    Rating7 = SimpleStaticHelpers.GetImage("7grey");
                                    Rating6 = SimpleStaticHelpers.GetImage("6orbelowgrey");
                                    Rating5 = SimpleStaticHelpers.GetImage("5grey");
                                    Rating4 = SimpleStaticHelpers.GetImage("4grey");
                                    Rating3 = SimpleStaticHelpers.GetImage("3grey");
                                    Rating2 = SimpleStaticHelpers.GetImage("2grey");
                                    Rating1 = SimpleStaticHelpers.GetImage("1grey");
                                    RatingNA = SimpleStaticHelpers.GetImage("NAgrey");

                                    var questionToUpdate =
                                        RatingQuestions.FirstOrDefault(x => x.QuestionId == answer.QuestionId);

                                    RatingQuestions.Remove(questionToUpdate);

                                    switch (answer.AnswerGiven.ToLower())
                                    {
                                        case "1":
                                            Rating1 = SimpleStaticHelpers.GetImage("1red");
                                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                                            break;
                                        case "2":
                                            Rating2 = SimpleStaticHelpers.GetImage("2red");
                                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                                            break;
                                        case "3":
                                            Rating3 = SimpleStaticHelpers.GetImage("3orange");
                                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                                            break;
                                        case "4":
                                            Rating4 = SimpleStaticHelpers.GetImage("4orange");
                                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                                            break;
                                        case "5":
                                            Rating5 = SimpleStaticHelpers.GetImage("5yellow");
                                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                                            break;
                                        case "6":
                                            Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                                            break;
                                        case "7":
                                            Rating7 = SimpleStaticHelpers.GetImage("7yellow");
                                            break;
                                        case "8":
                                            Rating8 = SimpleStaticHelpers.GetImage("8green");
                                            break;
                                        case "9":
                                            Rating9 = SimpleStaticHelpers.GetImage("9green");
                                            break;
                                        case "10":
                                            Rating10 = SimpleStaticHelpers.GetImage("10green");
                                            break;
                                        case "n/a":
                                            RatingNA = SimpleStaticHelpers.GetImage("NAgreen");
                                            break;
                                    }

                                    var questionToAdd = new RatingQuestionViewModel
                                    {
                                        Question = questionToUpdate.Question,
                                        QuestionId = questionToUpdate.QuestionId,
                                        KeyAnswer = questionToUpdate.KeyAnswer,
                                        Category = questionToUpdate.Category,
                                        DepthorRating = questionToUpdate.DepthorRating,
                                        FollowUpAction = questionToUpdate.FollowUpAction,
                                        FurtherQuestionId = questionToUpdate.FurtherQuestionId,
                                        Grouping = questionToUpdate.Grouping,
                                        InformationTo = questionToUpdate.InformationTo,
                                        NotifiableResponse = questionToUpdate.NotifiableResponse,
                                        QuestionOptions = questionToUpdate.QuestionOptions,
                                        ResponseType = questionToUpdate.ResponseType,
                                        Id = questionToUpdate.Id,
                                        Stage = questionToUpdate.Stage,
                                        IsEnabled = true,
                                        Rating10 = Rating10,
                                        Rating9 = Rating9,
                                        Rating8 = Rating8,
                                        Rating7 = Rating7,
                                        Rating6 = Rating6,
                                        Rating5 = Rating5,
                                        Rating4 = Rating4,
                                        Rating3 = Rating3,
                                        Rating2 = Rating2,
                                        Rating1 = Rating1,
                                        RatingNA = RatingNA
                                    };

                                    RatingQuestions.Add(questionToAdd);
                                }


                            CurrentAudit.NcrList = _assignmentService.GetNcrList(CurrentAudit.AuditId);

                            break;
                        }
                    default:
                        {
                            SubmitButtonText = "Start Inspection";

                            if (CurrentAssignment?.Complete.ToLower() == "true"
                                || CurrentAssignment?.RemoteTableId > 0)
                            {
                                NavigationalParameters.AuditStatus =
                                    NavigationalParameters.AuditStatuses.COMPLETE; // PageMode = "AuditResult";

                                CurrentAudit = CurrentAssignment?.Audit ?? CurrentAssignment?.Audits
                                    ?.OrderByDescending(x => x.StartTime)?.FirstOrDefault();
                            }
                            else
                            {
                                NavigationalParameters.AuditStatus =
                                    NavigationalParameters.AuditStatuses.NEW; // PageMode = "AuditResult";
                            }

                            break;
                        }
                }

                if (CurrentAudit != null)
                {
                    var user = _userService.GetUserById(
                        Convert.ToInt32(CurrentAudit?.GangLeaderId));

                    ProjectName = $"{CurrentAudit?.ProjectName}";

                    GpsCoOrds = $"GPS: {CurrentAudit?.GpsStart}";

                    if (CurrentAudit?.AuditDate != DateTime.Parse("01/01/1900 00:00:00"))
                        AuditDate =
                            $"Date: {CurrentAudit?.AuditDate.Date.ToString("dd/MM/yyyy")}";
                    if (CurrentAudit?.StartTime != DateTime.Parse("01/01/1900 00:00:00"))
                        StartTime =
                            $"Start Time: {CurrentAudit?.StartTime.ToString("HH:mm:ss")}";
                    if (CurrentAudit?.EndTime != DateTime.Parse("01/01/1900 00:00:00"))
                        EndTime =
                            $"End Time: {CurrentAudit?.EndTime.ToString("HH:mm:ss")}";

                    var tt = (CurrentAudit?.EndTime - CurrentAudit?.StartTime).ToString();
                    TimeTaken = $"Time Taken: {tt?.Split('.')?.First()}";

                    Overallscore = $"{CurrentAudit?.Score}%";
                    Comments = $"{CurrentAudit?.AdditionalComments}";
                    GangLeaderName = $"Gang Leader: {user?.FirstName} {user?.Surname}";
                    // GangLeaderId = $"{CurrentAudit?.GangLeaderId}";
                    //AuditorFocusId = $"{CurrentAudit?.AuditorFocusId}";
                    NonConformance = $"{CurrentAudit?.NonConformancies}";
                    Inadequiecies = $"{CurrentAudit?.Inadequacies}";
                    GeneralSiteTidiness = $"{CurrentAudit?.Section1}%";
                    Documentation = $"{CurrentAudit?.Section2}%";
                    EqupPlantVeh = $"{CurrentAudit?.Section3}%";
                    PPE = $"{CurrentAudit?.Section4}%";
                    HighwayWorking = $"{CurrentAudit?.Section5}%";
                    UnderGroundUtilities = $"{CurrentAudit?.Section6}%";
                    ExcavationChambers = $"{CurrentAudit?.Section7}%";
                    EnviromentalWasteManaegment = $"{CurrentAudit?.Section8}%";
                    MethodStatement = $"{CurrentAudit?.Section9}%";
                }

                NavigationalParameters.CurrentAssignment = CurrentAssignment;
            }
            else
            {
                NavigationalParameters.AuditStatus =
                    NavigationalParameters.AuditStatuses.NEW; // PageMode = "AuditResult";

                SubmitButtonText = "Start Inspection";
            }

            NavigationalParameters.RatingQuestions = RatingQuestions;

            ShowSection1 = true;
            ShowSection2 = NavigationalParameters.AuditStatus != NavigationalParameters.AuditStatuses.NEW;
            ShowSection3 = NavigationalParameters.AuditStatus != NavigationalParameters.AuditStatuses.NEW;
            ShowSection4 = NavigationalParameters.AuditStatus != NavigationalParameters.AuditStatuses.NEW;
            ShowSection5 = NavigationalParameters.AuditStatus != NavigationalParameters.AuditStatuses.NEW;
            ShowSection6 = true;
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand StartNewAudit => new(async () =>
    {
        //NavigationalParameters.ReturnPage = Locator.AuditResultPageViewModelKey;
        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.AUDIT;

        if (NavigationalParameters.AuditStatus == NavigationalParameters.AuditStatuses.SIGNOFF)
        {
            if (CurrentAudit != null)
            {
                CurrentAudit.AdditionalComments = Comments ?? "";

                await _assignmentService.AddAudits(CurrentAudit);

                NavigationalParameters.AuditStatus = NavigationalParameters.AuditStatuses.COMPLETE;

                NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
                NavigationalParameters.CurrentAudit = CurrentAudit;

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                    // Connection to internet is available
                    await NavigateTo(ViewModelLocator.SignaturePage);
                else
                    await Alert("No internet connectivity",
                        "There is internet connectivity to sbmit this audi please find a better connection and try again!");
            }
            else
            {
                await Alert("Error",
                    "There is no audit to upload or the audit may be corrupt please select done to delete the audit and start a fresh one");
            }
        }
        else
        {
            if (NavigationalParameters.AuditStatus == NavigationalParameters.AuditStatuses.COMPLETE)
            {
                CurrentAudit = null;
                CurrentAssignment = null;
            }

            if (CurrentAssignment == null)
            {
                CurrentAssignment = new Assignment
                {
                    AssignmentId = Guid.NewGuid(),
                    Category = "Audit", //
                    ProjectName =
                        CurrentSelectedJob?.ProjectName, //VM NBU 64132
                    Qnumber = CurrentSelectedJob.QuoteNumber, //"406999",
                    Cnumber = CurrentSelectedJob?.ContractNumber
                        .ToString(), //6026672
                    Complete = "false",
                    RemoteTableId = 0,
                    TermContract = CurrentSelectedJob?.BaseContractId, //6026672
                    PreSiteById = CurrentSelectedJob.GangLeaderId, //100026
                    CompletedOn = CurrentSelectedJob.JobDate,
                    ClientName = CurrentSelectedJob?.ClientName,
                    StreetName = "N/A",
                    Type = ""
                };

                await _assignmentService.AddACurrentAssignment(CurrentAssignment);
            }

            if (CurrentAudit == null)
            {
                if (CurrentAssignment.Audit != null)
                {
                    CurrentAudit = CurrentAssignment.Audit;
                }
                else
                {
                    CurrentAudit = new Audit
                    {
                        AssignmentId =
                            CurrentAssignment.AssignmentId, //34cd145f-7338-434f-af0b-b8f2037d2eef
                        ProjectName =
                            CurrentSelectedJob.ProjectName, //VM NBU 64132
                        Qnumber = CurrentSelectedJob.QuoteNumber, //"406999"
                        RemoteTableId = 0,
                        AuditDate = CurrentSelectedJob.JobDate,
                        GangLeaderId = CurrentSelectedJob.GangLeaderId.ToString(),
                        GpsStart = "",
                        GpsEnd = "",
                        AuditeeFocusId =
                            CurrentSelectedJob.GangLeaderId,
                        // AuditorFocusId = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                        StartTime = DateTime.Now,
                        //ConductedBy = "",
                        AdditionalComments = ""
                    };

                    await _assignmentService.AddAudits(CurrentAudit);
                }
            }

            NavigationalParameters.CurrentSelectedJob = CurrentSelectedJob;
            NavigationalParameters.CurrentAssignment = CurrentAssignment;
            NavigationalParameters.CurrentAudit = CurrentAudit;

            await NavigateTo(ViewModelLocator.RatingSurveyQuestionsPage);
        }
    });

    public RelayCommand Cancel => new(async () =>
    {
        if (CurrentAudit != null)
        {
            var answer = await Alert("Caution",
                "Would you like to Delete audit from the device before returning to the menu page? This action cannot be undone!",
                "Yes! delete the audit", "No! keep the the audit");

            if (answer)
            {
                if (CurrentAudit != null) _assignmentService.DeleteCurrentAudit(CurrentAudit);

                _assignmentService.DeleteAssignment(CurrentAssignment);
            }
        }

        NavigationalParameters.CurrentAssignment = null;
        NavigationalParameters.CurrentAudit = null;
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });
}