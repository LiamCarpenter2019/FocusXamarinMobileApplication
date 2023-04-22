using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class SurveyQuestionsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Assignments _assignmentService;
    public readonly Jobs _jobService;
    public readonly LocalStorage _ls;
    public readonly Pictures _picService;
    public readonly PhotoSelectionPageViewModel _psvm;
    public readonly Users _userService;

    private bool _isLoading;

    private ObservableCollection<SurveyQuestion> _questionCollection;

    private bool _showSubmissionButton;

    private string _streetName;


    private List<SurveyAnswers> _surveyAnswers;

    private ObservableCollection<YesNoNaQuestionViewModel> _yesNoQuestions;
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

    public SurveyQuestionsPageViewModel()
    {
        NavigationalParameters.NewPosition = new Position(53.789391, -1.343984);

        SelectedAnswer = null;

        _assignmentService = new Assignments();

        _userService = new Users();

        _jobService = new Jobs();

        _assignmentService = new Assignments();

        _picService = new Pictures();

        _ls = new LocalStorage();

        _psvm = new PhotoSelectionPageViewModel();
    }

    public Stopwatch sw { get; set; }
    public YesNoNaQuestionViewModel answer { get; set; }


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

    public bool _showCivilsButtons { get; set; }

    public string[] Options { get; set; }

    // public List<SurveyAnswers> Answers { get; set; }
    public List<IGrouping<decimal, SurveyAnswers>> tempAnswers { get; set; }

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

    public decimal TotalSection1Percentage { get; set; }
    public decimal TotalSection2Percentage { get; set; }
    public decimal TotalSection3Percentage { get; set; }
    public decimal TotalSection4Percentage { get; set; }
    public decimal TotalSection5Percentage { get; set; }
    public decimal TotalSection6Percentage { get; set; }
    public decimal TotalSection7Percentage { get; set; }
    public decimal TotalSection8Percentage { get; set; }
    public decimal TotalSection9Percentage { get; set; }

    public ImageSource Source { get; private set; }
    public string Reason { get; private set; }

    public ObservableCollection<SurveyQuestion> QuestionCollection
    {
        get => _questionCollection;
        set
        {
            _questionCollection = value;
            OnPropertyChanged("QuestionCollection");
        }
    }

    public List<SurveyAnswers> SurveyAnswers
    {
        get => _surveyAnswers;
        set
        {
            _surveyAnswers = value;
            OnPropertyChanged("SurveyAnswers");
        }
    }

    private ObservableCollection<SurveyQuestion> _filteredQuestionCollection { get; set; }

    public ObservableCollection<SurveyQuestion> FilteredQuestionCollection
    {
        get => _filteredQuestionCollection;
        set
        {
            _filteredQuestionCollection = value;
            OnPropertyChanged("FilteredQuestionCollection");
        }
    }

    private bool _showPage { get; set; }

    public bool ShowPage
    {
        get => _showPage;
        set
        {
            _showPage = value;
            OnPropertyChanged("ShowPage");
        }
    }

    private SurveyQuestion _selectedQuestion { get; set; }

    public SurveyQuestion SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            _selectedQuestion = value;
            OnPropertyChanged("SelectedQuestion");
        }
    }

    private SurveyAnswers _selectedAnswer { get; set; }

    public SurveyAnswers SelectedAnswer
    {
        get => _selectedAnswer;
        set
        {
            _selectedAnswer = value;
            OnPropertyChanged("SelectedAnswer");
        }
    }

    private int _selectedIndex { get; set; }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            _selectedIndex = value;
            OnPropertyChanged("SelectedIndex");
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged("IsLoading");
        }
    }

    
    private bool _enableSubmit { get; set; }

    public bool EnableSubmit
{
        get => _enableSubmit;
        set
        {
            _enableSubmit = value;
            OnPropertyChanged("EnableSubmit");
        }
    }

    private bool _canEdit { get; set; }

    public bool CanEdit
    {
        get => _canEdit;
        set
        {
            _canEdit = value;
            OnPropertyChanged("CanEdit");
        }
    }

    public ImageSource _cameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged("CameraImage");
        }
    }

    public ImageSource _documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");

    public ImageSource Documents
    {
        get => _documents;
        set
        {
            _documents = value;
            OnPropertyChanged("DocumentsImage");
        }
    }

    public ImageSource _gpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");

    public ImageSource GpsImage
    {
        get => _gpsImage;
        set
        {
            _gpsImage = value;
            OnPropertyChanged("GpsImage");
        }
    }

    public bool _showDistance { get; set; }

    public bool ShowDistance
    {
        get => _showDistance;
        set
        {
            _showDistance = value;
            OnPropertyChanged("ShowDistance");
        }
    }

    public bool _showProjectsButton { get; set; }

    public bool ShowProjectsButton
    {
        get => _showProjectsButton;
        set
        {
            _showProjectsButton = value;
            OnPropertyChanged("ShowProjectsButton");
        }
    }

    public bool _showIfNotAudit { get; set; }

    public bool ShowIfNotAudit
    {
        get => _showIfNotAudit;
        set
        {
            _showIfNotAudit = value;
            OnPropertyChanged("ShowIfNotAudit");
        }
    }

    public bool _showall { get; set; }

    public bool ShowAll
    {
        get => _showall;
        set
        {
            _showall = value;
            OnPropertyChanged("ShowAll");
        }
    }

    public bool _showAsBuiltButtons { get; set; }

    public bool ShowAsBuiltButtons
    {
        get => _showAsBuiltButtons;
        set
        {
            _showAsBuiltButtons = value;
            OnPropertyChanged("ShowAsBuiltButtons");
        }
    }

    public bool _showBackButton { get; set; }

    public bool ShowBackButton
    {
        get => _showBackButton;
        set
        {
            _showBackButton = value;
            OnPropertyChanged("ShowBackButton");
        }
    }

    public string _distance { get; set; } = "";

    public string Distance
    {
        get => _distance;
        set
        {
            _distance = value;

            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PERMITTODIG)
            {
                try
                {
                    if (Distance != "0" && Distance != "")
                        NavigationalParameters.CurrentPermit.Distance = Convert.ToDecimal(Distance);

                    OnPropertyChanged();
                }
                catch (Exception ex)
                {
                    _ = Alert("Invalid format", "please ensure the distance is in decimal format only");
                }
            }
            else
            {
                _distance = value;
                OnPropertyChanged();
            }
        }
    }

    private Color _buttonAColour { get; set; } = Color.LightGray;
    private Color _buttonBColour { get; set; } = Color.LightGray;
    private Color _buttonCColour { get; set; } = Color.LightGray;
    private Color _buttonDColour { get; set; } = Color.LightGray;
    private Color _buttonEColour { get; set; } = Color.LightGray;
    private Color _buttonFColour { get; set; } = Color.LightGray;
    private Color _buttonGColour { get; set; } = Color.LightGray;
    private Color _buttonHColour { get; set; } = Color.LightGray;

    public string FilteredQuestionBy { get; set; }

    public Color ButtonAColour
    {
        get => _buttonAColour;
        set
        {
            _buttonAColour = value;
            OnPropertyChanged("ButtonAColour");
        }
    }

    public Color ButtonBColour
    {
        get => _buttonBColour;
        set
        {
            _buttonBColour = value;
            OnPropertyChanged("ButtonBColour");
        }
    }

    public Color ButtonCColour
    {
        get => _buttonCColour;
        set
        {
            _buttonCColour = value;
            OnPropertyChanged("ButtonCColour");
        }
    }

    public Color ButtonDColour
    {
        get => _buttonDColour;
        set
        {
            _buttonDColour = value;
            OnPropertyChanged("ButtonDColour");
        }
    }

    public Color ButtonEColour
    {
        get => _buttonEColour;
        set
        {
            _buttonEColour = value;
            OnPropertyChanged("ButtonEColour");
        }
    }

    public Color ButtonFColour
    {
        get => _buttonFColour;
        set
        {
            _buttonFColour = value;
            OnPropertyChanged("ButtonFColour");
        }
    }

    public Color ButtonGColour
    {
        get => _buttonGColour;
        set
        {
            _buttonGColour = value;
            OnPropertyChanged("ButtonGColour");
        }
    }

    public Color ButtonHColour
    {
        get => _buttonHColour;
        set
        {
            _buttonHColour = value;
            OnPropertyChanged("ButtonHColour");
        }
    }

    public string StreetName
    {
        get => _streetName;
        set
        {
            _streetName = value;
            OnPropertyChanged("StreetName");
        }
    }

    public bool ShowCivilsButtons
    {
        get => _showCivilsButtons;
        set
        {
            _showCivilsButtons = value;
            OnPropertyChanged("ShowCivilsButtons");
        }
    }

    public bool ShowSubmissionButton
    {
        get => _showSubmissionButton;
        set
        {
            _showSubmissionButton = value;
            OnPropertyChanged("ShowSubmissionButton");
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        try
        {
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            IsLoading = false;
            EnableSubmit = true;
            ShowPage = true;
            StreetName = NavigationalParameters.SelectedStreet?.FirstOrDefault()?.BusinessName ??
                         NavigationalParameters.SelectedAsset?.BusinessName;

            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    [Time]
    public void RefreshQuestionList()
    {
        try
        {
            if (NavigationalParameters.CurrentAssignment != null)
            {
                SurveyAnswers = _assignmentService.GetRelevantResponses(NavigationalParameters.CurrentAssignment.AssignmentId) ??
                    new List<SurveyAnswers>();
            }

            if ((NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.DPASBUILT
                || NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE)
                && NavigationalParameters.ClaimFile == null)
            {
                //NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DPASBUILT;

                QuestionCollection = NavigationalParameters.PreSiteQuestions.Any()
                   ?
                   new ObservableCollection<SurveyQuestion>(
                       NavigationalParameters.PreSiteQuestions.OrderBy(x => x.QuestionId))
                   : NavigationalParameters.YesNoCollection.Any()
                       ? new ObservableCollection<SurveyQuestion>(
                           NavigationalParameters.YesNoCollection.OrderBy(x => x.QuestionId))
                       :
                       new ObservableCollection<SurveyQuestion>(
                           NavigationalParameters.GenericQuestions.OrderBy(x => x.QuestionId));

                if (NavigationalParameters.CurrentAssignment != null)
                {
                    SurveyAnswers =
                        _assignmentService.GetRelevantResponses(NavigationalParameters.CurrentAssignment.AssignmentId) ??
                        new List<SurveyAnswers>();
                }

                var endOfLine = App.Database.GetItems<SurveyAnswers>()?.Any(x => x.QuestionId == 1 
                && x.Category.ToLower() == NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE.ToString().ToLower() 
                && x.AnswerGiven.ToLower() == "end of line") ?? true;

                var adp = App.Database.GetItems<SurveyAnswers>()?.FirstOrDefault(x => x.QuestionId == 2 && x.Category.ToLower() == NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE.ToString().ToLower())?.AnswerGiven ?? NavigationalParameters.SelectedAsset?.DropLength;

                var additionCables = App.Database.GetItems<SurveyAnswers>()?.Any(x => x.QuestionId == 3 && x.Category.ToLower() == NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE.ToString().ToLower() && x.AnswerGiven.ToLower() == "yes") ?? false;

                if (adp == "ADP 3 x 1:8")
                {
                    foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.DepthorRating is 1 or 2 or 3))
                    {
                        foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 4 or 5).ToList())
                        {
                            App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);

                            if (SurveyAnswers != null)
                            {
                                SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                            }
                            QuestionCollection.Remove(questionToRemove);
                        }

                        if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                        {
                            QuestionCollection.Add(item);
                        }
                    }
                }
                else if (adp == "ADP 2 x 1:8" && !endOfLine && !additionCables)
                {
                    foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.DepthorRating is 1 or 2 or 5))
                    {
                        foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 3 or 4).ToList())
                        {
                            App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                            if (SurveyAnswers != null)
                            {
                                SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                            }
                            QuestionCollection.Remove(questionToRemove);
                        }

                        if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                        {
                            QuestionCollection.Add(item);
                        }
                    }
                }
                else if (adp == "ADP 1 x 1:8" && !endOfLine && !additionCables)
                {
                    foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.DepthorRating is 1 or 5))
                    {
                        foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 2 or 3 or 4).ToList())
                        {
                            App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                            if (SurveyAnswers != null)
                            {
                                SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                            }
                            QuestionCollection.Remove(questionToRemove);

                        }

                        if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                        {
                            QuestionCollection.Add(item);
                        }
                    }
                }
                else
                {
                    if (adp == "ADP 2 x 1:8")
                    {
                        foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.DepthorRating is 1 or 2 or 4 or 5))
                        {
                            foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating == 3).ToList())
                            {
                                App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                                if (SurveyAnswers != null)
                                {
                                    SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                                }
                                QuestionCollection.Remove(questionToRemove);
                            }

                            if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                            {
                                QuestionCollection.Add(item);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.DepthorRating is 1 or 4 or 5))
                        {
                            foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 2 or 3).ToList())
                            {
                                App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                                if (SurveyAnswers != null)
                                {
                                    SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                                }
                                QuestionCollection.Remove(questionToRemove);
                            }

                            if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                            {
                                if (item.DepthorRating != 2)
                                {
                                    QuestionCollection.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            else if ((NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.DPASBUILT
                || NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE)
                && NavigationalParameters.ClaimFile != null)
            {
                var questions = NavigationalParameters.PreSiteQuestions.Any()
                    ?
                    new ObservableCollection<SurveyQuestion>(
                        NavigationalParameters.PreSiteQuestions.OrderBy(x => x.QuestionId))
                    : NavigationalParameters.YesNoCollection.Any()
                        ? new ObservableCollection<SurveyQuestion>(
                            NavigationalParameters.YesNoCollection.OrderBy(x => x.QuestionId))
                        : new ObservableCollection<SurveyQuestion>(
                            NavigationalParameters.GenericQuestions.OrderBy(x => x.QuestionId));

                QuestionCollection = new ObservableCollection<SurveyQuestion>(questions.Where(x => x.Stage.ToLower().Contains(NavigationalParameters.ClaimFile.ClaimHeader.ToLower()) ));

                if (NavigationalParameters.CurrentAssignment != null)
                {
                    SurveyAnswers =
                        _assignmentService.GetRelevantResponses(NavigationalParameters.CurrentAssignment.AssignmentId) ??
                        new List<SurveyAnswers>();
                }

                var endOfLine = App.Database.GetItems<SurveyAnswers>()?.Any(x => x.QuestionId == 1 && x.Category.ToLower() == NavigationalParameters.SurveyTypes.DPASBUILT.ToString().ToLower() && x.AnswerGiven.ToLower() == "end of line") ?? true;
                var adp = App.Database.GetItems<SurveyAnswers>()?.FirstOrDefault(x => x.QuestionId == 2 && x.Category.ToLower() == NavigationalParameters.SurveyTypes.DPASBUILT.ToString().ToLower())?.AnswerGiven ?? NavigationalParameters.SelectedAsset?.DropLength;
                var additionCables = App.Database.GetItems<SurveyAnswers>()?.Any(x => x.QuestionId == 3 && x.Category.ToLower() == NavigationalParameters.SurveyTypes.DPASBUILT.ToString().ToLower() && x.AnswerGiven.ToLower() == "yes") ?? false;

                if (adp == "ADP 3 x 1:8")
                {
                    foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.Stage.ToLower() == NavigationalParameters.ClaimFile.ClaimHeader.ToLower() && x.DepthorRating is 1 or 2 or 3))
                    {
                        foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 4 or 5).ToList())
                        {
                            App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);

                            if (SurveyAnswers != null)
                            {
                                SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category.ToLower() == questionToRemove.Category.ToLower());
                            }
                            QuestionCollection.Remove(questionToRemove);
                        }

                        if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                        {
                            QuestionCollection.Add(item);
                        }
                    }
                }
                else if (adp == "ADP 2 x 1:8" && !endOfLine && !additionCables)
                {
                    foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.Stage.ToLower() == NavigationalParameters.ClaimFile.ClaimHeader.ToLower() && x.DepthorRating is 1 or 2 or 5))
                    {
                        foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 3 or 4).ToList())
                        {
                            App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                            if (SurveyAnswers != null)
                            {
                                SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category.ToLower() == questionToRemove.Category.ToLower());
                            }
                            QuestionCollection.Remove(questionToRemove);
                        }

                        if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                        {
                            QuestionCollection.Add(item);
                        }
                    }
                }
                else if (adp == "ADP 1 x 1:8" && !endOfLine && !additionCables)
                {
                    foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.Stage.ToLower() == NavigationalParameters.ClaimFile.ClaimHeader.ToLower() && x.DepthorRating is 1 or 5))
                    {
                        foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating == 2 || x.DepthorRating == 3 || x.DepthorRating == 4).ToList())
                        {
                            App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                            if (SurveyAnswers != null)
                            {
                                SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category.ToLower() == questionToRemove.Category.ToLower());
                            }
                            QuestionCollection.Remove(questionToRemove);

                        }

                        if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                        {
                            QuestionCollection.Add(item);
                        }
                    }
                }
                else
                {
                    if (adp == "ADP 2 x 1:8")
                    {
                        foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.Stage.ToLower() == NavigationalParameters.ClaimFile.ClaimHeader.ToLower() && x.DepthorRating is 1 or 2 or 4 or 5))
                        {
                            foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating == 3).ToList())
                            {
                                App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                                if (SurveyAnswers != null)
                                {
                                    SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                                }
                                QuestionCollection.Remove(questionToRemove);
                            }

                            if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                            {
                                QuestionCollection.Add(item);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in NavigationalParameters.GenericQuestions.Where(x => x.Stage.ToLower() == NavigationalParameters.ClaimFile.ClaimHeader.ToLower() && x.DepthorRating is 1 or 4 or 5))
                        {
                            foreach (var questionToRemove in QuestionCollection.Where(x => x.DepthorRating is 2 or 3).ToList())
                            {
                                App.Database.DeleteAllItems(questionToRemove.QuestionId, NavigationalParameters.CurrentAssignment.AssignmentId);
                                if (SurveyAnswers != null)
                                {
                                    SurveyAnswers.RemoveAll(x => x.QuestionId == questionToRemove.QuestionId && x.Category == questionToRemove.Category);
                                }
                                QuestionCollection.Remove(questionToRemove);
                            }

                            if (QuestionCollection.All(x => x.QuestionId != item.QuestionId))
                            {
                                if (item.DepthorRating != 2)
                                {
                                    QuestionCollection.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                QuestionCollection = NavigationalParameters.PreSiteQuestions.Any()
                                 ?
                                 new ObservableCollection<SurveyQuestion>(
                                     NavigationalParameters.PreSiteQuestions.OrderBy(x => x.QuestionId))
                                 : NavigationalParameters.YesNoCollection.Any()
                                     ? new ObservableCollection<SurveyQuestion>(
                                         NavigationalParameters.YesNoCollection.OrderBy(x => x.QuestionId))
                                     :
                                     new ObservableCollection<SurveyQuestion>(
                                         NavigationalParameters.GenericQuestions.OrderBy(x => x.QuestionId));

                if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.POLEMEASURE)
                {
                    var questionType = NavigationalParameters.ClaimFile.ClaimHeader?.Split('/')[1];
                    
                    if (questionType.ToUpper().Contains("AB")//aborted hole
                        || questionType.ToUpper().Contains("TH")//trial hole
                        || questionType.ToUpper().Contains("EXC"))//excavation of hole
                    {
                        QuestionCollection = new ObservableCollection<SurveyQuestion>(QuestionCollection?.Where(x => x.Stage.ToUpper().Contains("EXC") && x.Category.ToUpper()
                        == NavigationalParameters.SurveyType.ToString().ToUpper()));
                    }
                    else
                    {
                        QuestionCollection = new ObservableCollection<SurveyQuestion>(QuestionCollection?.Where(x => x.Stage.ToUpper().Contains(questionType.ToUpper())));
                    }
                }            
            }
      
            
            NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();

            if (NavigationalParameters.CurrentPermit != null)
            {
                if (NavigationalParameters.CurrentPermit?.Distance == 0)
                    Distance = "";
                else
                    Distance = NavigationalParameters.CurrentPermit?.Distance.ToString();
            }
            else
            {
                Distance = "";
            }

            ShowProjectsButton = true;

            switch (NavigationalParameters.SurveyType)
            {
                case NavigationalParameters.SurveyTypes.PRESITECIVILS:
                    ShowCivilsButtons = true;
                    ShowDistance = false;
                    ShowBackButton = false;
                    ShowIfNotAudit = true;
                    ShowAsBuiltButtons = false;
                    FilterQuestions(FilteredQuestionBy?.ToLower() ?? "");
                    break;
                case NavigationalParameters.SurveyTypes.BLOCKAGE:
                    ShowCivilsButtons = false;
                    ShowDistance = false;
                    ShowBackButton = false;
                    ShowIfNotAudit = true;
                    ShowAsBuiltButtons = false;
                    FilterQuestions(FilteredQuestionBy?.ToLower() ?? "");
                    break;
                case NavigationalParameters.SurveyTypes.CHAMBERASBUILT:
                case NavigationalParameters.SurveyTypes.DPASBUILT:
                case NavigationalParameters.SurveyTypes.DJEASBUILT:
                case NavigationalParameters.SurveyTypes.BJEASBUILT:
                case NavigationalParameters.SurveyTypes.FJEASBUILT:
                case NavigationalParameters.SurveyTypes.POLEASBUILT:
                    ShowCivilsButtons = false;
                    ShowDistance = false;
                    ShowBackButton = false;
                    ShowIfNotAudit = false;
                    ShowAsBuiltButtons = true;


                    if (QuestionCollection.Any() && NavigationalParameters.SelectedAnswer != null)
                    {
                        var picExsists = _picService.GetAllPictures().Any(x =>
                            x.QuestionId == NavigationalParameters.SelectedAnswer.QuestionId.ToString() &&
                            x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId);

                        if (!string.IsNullOrEmpty(QuestionCollection.FirstOrDefault(x =>
                                x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId &&
                                NavigationalParameters.SelectedAnswer?.QNumber ==
                                NavigationalParameters.CurrentSelectedJob?.QuoteNumber).FollowUpAction))
                        {
                            QuestionCollection.FirstOrDefault(x =>
                                    x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId &&
                                    NavigationalParameters.SelectedAnswer?.QNumber ==
                                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber).QuestionTextColour =
                                picExsists ? Color.DarkGreen : Color.DarkRed;
                        }
                        else
                        {
                            QuestionCollection.FirstOrDefault(x =>
                                    x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId &&
                                    NavigationalParameters.SelectedAnswer?.QNumber ==
                                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber).QuestionTextColour =
                                Color.DarkGreen;
                        }
                    }

                    FilteredQuestionCollection =
                        new ObservableCollection<SurveyQuestion>(QuestionCollection.OrderBy(x => x.QuestionId));
                    break;
                case NavigationalParameters.SurveyTypes.PRESITEPREMISES:
                    ShowCivilsButtons = false;
                    ShowDistance = false;
                    ShowIfNotAudit = true;
                    ShowBackButton = false;
                    ShowAsBuiltButtons = false;
                    FilterQuestions(FilteredQuestionBy?.ToLower() ?? "");
                    break;
                case NavigationalParameters.SurveyTypes.CHAMBERAUDIT:
                    ShowCivilsButtons = false;
                    ShowIfNotAudit = false;
                    ShowAsBuiltButtons = false;
                    FilteredQuestionCollection = QuestionCollection;
                    break;
                case NavigationalParameters.SurveyTypes.SITECLEAR:
                    ShowCivilsButtons = false;
                    ShowDistance = false;
                    ShowIfNotAudit = false;
                    ShowAsBuiltButtons = false;
                    FilteredQuestionCollection = QuestionCollection;
                    break;
                case NavigationalParameters.SurveyTypes.POLEMEASURE:
                case NavigationalParameters.SurveyTypes.POLECABLEMEASURE:
                case NavigationalParameters.SurveyTypes.UGCRPMEASURE:
                case NavigationalParameters.SurveyTypes.CHAMBERMEASURE:
                case NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE:
                case NavigationalParameters.SurveyTypes.FJEMEASURE:
                case NavigationalParameters.SurveyTypes.BJEMEASURE:
                case NavigationalParameters.SurveyTypes.DJEMEASURE:
                case NavigationalParameters.SurveyTypes.DUCTMEASURE:
                case NavigationalParameters.SurveyTypes.REMEDIAL:
                    ShowCivilsButtons = false;
                    ShowDistance = false;
                    ShowBackButton = true;
                    ShowProjectsButton = false;
                    ShowIfNotAudit = false;
                    ShowAsBuiltButtons = true;

                    if (QuestionCollection.Any() && NavigationalParameters.SelectedAnswer != null)
                    {
                        var picExsists = _picService.GetAllPictures().Any(x =>

                        x.QuestionId == NavigationalParameters.SelectedAnswer.QuestionId.ToString() &&
                            x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId);

                        if (!string.IsNullOrEmpty(QuestionCollection.FirstOrDefault(x =>
                                x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId &&
                                NavigationalParameters.SelectedAnswer?.QNumber ==
                                NavigationalParameters.CurrentSelectedJob?.QuoteNumber).FollowUpAction))
                        {
                            QuestionCollection.FirstOrDefault(x =>
                                    x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId &&
                                    NavigationalParameters.SelectedAnswer?.QNumber ==
                                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber).QuestionTextColour =
                                picExsists ? Color.DarkGreen : Color.DarkRed;
                        }
                        else
                        {
                            QuestionCollection.FirstOrDefault(x =>
                                    x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId &&
                                    NavigationalParameters.SelectedAnswer?.QNumber ==
                                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber).QuestionTextColour =
                                Color.DarkGreen;
                        }
                    }
                    FilteredQuestionCollection = QuestionCollection;
                    break;
                case NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY:
                    ShowCivilsButtons = false;
                    ShowDistance = false;
                    ShowBackButton = false;
                    ShowIfNotAudit = true;
                    ShowAsBuiltButtons = false;
                    FilterQuestions(FilteredQuestionBy?.ToLower() ?? "");
                    break;
                case NavigationalParameters.SurveyTypes.PERMITTODIG:
                case NavigationalParameters.SurveyTypes.VERTISHOREPERMIT:
                    ShowCivilsButtons = false;
                    ShowDistance = true;
                    ShowIfNotAudit = true;
                    FilteredQuestionCollection = QuestionCollection;
                    break;
            }

            ShowSubmissionButton = QuestionCollection?.Count <= SurveyAnswers?.Count;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    public RelayCommand GoComments => new(async () =>
    {
        try
        {
            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                .FirstOrDefault(x =>
                    x.QuestionId == SelectedQuestion.QuestionId &&
                    x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            SelectedAnswer ??= new SurveyAnswers
            {
                RemoteTableId = 0,
                QuestionId = (decimal)SelectedQuestion?.QuestionId,
                QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                Notifiable = SelectedQuestion?.NotifiableResponse,
                StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                LocationName = NavigationalParameters.CurrentAssignment.LocationName,
                SubmittedDateTime = DateTime.Now,
                CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId,
                Rating = SelectedQuestion.DepthorRating,
                AnswerGiven = "Yes"
            };

            await _assignmentService.SaveCurrentAnswer(SelectedAnswer);

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M) SelectedIndex = 1;

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M) SelectedIndex = 1;

            UpdateQuestions(SelectedAnswer);

            await NavigateTo(ViewModelLocator.FormsCommentPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    });

    public RelayCommand GoQuestionGps => new(async () =>
    {
        try
        {
            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                .FirstOrDefault(x =>
                    x.QuestionId == SelectedQuestion.QuestionId &&
                    x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            SelectedAnswer ??= new SurveyAnswers
            {
                RemoteTableId = 0,
                QuestionId = (decimal)SelectedQuestion?.QuestionId,
                QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                Notifiable = SelectedQuestion?.NotifiableResponse,
                StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                LocationName = NavigationalParameters.CurrentAssignment.LocationName,
                //Comment = "
                SubmittedDateTime = DateTime.Now,
                CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId,
                // Identifier = NavigationalParameters.CurrentAudit.AuditId,
                Rating = SelectedQuestion.DepthorRating,
                AnswerGiven = "Yes"
            };

            await _assignmentService.SaveCurrentAnswer(SelectedAnswer);

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M) SelectedIndex = 2;

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            switch (NavigationalParameters.SurveyType)
            {
                case NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY:
                case NavigationalParameters.SurveyTypes.PRESITEPREMISES:
                case NavigationalParameters.SurveyTypes.POLEASBUILT:
                case NavigationalParameters.SurveyTypes.CHAMBERASBUILT:
                case NavigationalParameters.SurveyTypes.DPASBUILT:
                case NavigationalParameters.SurveyTypes.DJEASBUILT:
                case NavigationalParameters.SurveyTypes.FJEASBUILT:
                case NavigationalParameters.SurveyTypes.BJEASBUILT:
                case NavigationalParameters.SurveyTypes.CHAMBERMEASURE:
                case NavigationalParameters.SurveyTypes.POLEMEASURE:
                case NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE:
                case NavigationalParameters.SurveyTypes.DUCTMEASURE:
                case NavigationalParameters.SurveyTypes.BJEMEASURE:
                case NavigationalParameters.SurveyTypes.FJEMEASURE:
                case NavigationalParameters.SurveyTypes.DJEMEASURE:
                case NavigationalParameters.SurveyTypes.UGCRPMEASURE:
                case NavigationalParameters.SurveyTypes.POLECABLEMEASURE:
                case NavigationalParameters.SurveyTypes.REMEDIAL:
                    NavigationalParameters.MapType = "PoleQuestionGps";

                    if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M)
                    {
                        SelectedIndex = 0;
                        await NavigateTo(ViewModelLocator.HybridWebViewPage);
                    }
                    else
                    {
                        await NavigateTo(ViewModelLocator.FormsMapPage);
                    }

                    break;
                default:
                    NavigationalParameters.MapType = "QuestionGps";
                    await NavigateTo(ViewModelLocator.FormsMapPage);
                    break;
            }

            UpdateQuestions(SelectedAnswer);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    });

    public RelayCommand GoBack => new(async () =>
    {
        try
        {
            switch (NavigationalParameters.AppMode)
            {
                case NavigationalParameters.AppModes.PRESITEPOLESURVEY:
                case NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY:
                case NavigationalParameters.AppModes.PRESITECHAMBERSURVEY:
                case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                case NavigationalParameters.AppModes.PRESITECIVILS:
                case NavigationalParameters.AppModes.PRESITEPREMISIS:
                case NavigationalParameters.AppModes.PRESITE:
                case NavigationalParameters.AppModes.POLEASBUILT:
                case NavigationalParameters.AppModes.CHAMBERASBUILT:
                case NavigationalParameters.AppModes.DPASBUILT:
                case NavigationalParameters.AppModes.BJEASBUILT:
                case NavigationalParameters.AppModes.DJEASBUILT:
                case NavigationalParameters.AppModes.FJEASBUILT:
                    {
                        var confim =
                            await Alert(
                                "All data will be lost and the as built survey will have to be restarted! please confirm that you want to delete all data entered so far",
                                "Warning All data will be lost!?", "Confirm", "Decline");

                        if (confim)
                        {
                            try
                            {
                                if (SurveyAnswers.Any())
                                {
                                    var itemsToDelete = App.Database.GetItems<SurveyAnswers>().Where(x =>
                                        x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId).ToList();

                                    await _assignmentService.RemoveSurveyAnswers(itemsToDelete);

                                    SurveyAnswers = null;
                                }

                                _assignmentService.DeleteAssignment(NavigationalParameters.CurrentAssignment);

                                await NavigateTo(ViewModelLocator.SelectEndPointPage);
                            }
                            catch (Exception ex)
                            {
                                Analytics.TrackEvent(
                                    $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                            }
                        }

                        break;
                    }

                case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                case NavigationalParameters.AppModes.DUCTMEASURE:
                case NavigationalParameters.AppModes.POLECABLEMEASURE:
                case NavigationalParameters.AppModes.POLEMEASURE:
                case NavigationalParameters.AppModes.CHAMBERMEASURE:
                case NavigationalParameters.AppModes.REMEDIAL:
                    await NavigateTo(ViewModelLocator.SelectEndPointPage);
                    break;
                default:
                    await NavigateTo(ViewModelLocator.SelectStreetPage);
                    break;
            }

            NavigationalParameters.PreSiteQuestions.Clear();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToAllocatedProjects => new(async () =>
    {
        try
        {
            NavigationalParameters.ReturnPage = "SurveyQuestionsViewModelKey";

            NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;


            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            {
                switch (NavigationalParameters.AppMode)
                {
                    case NavigationalParameters.AppModes.PRESITEPOLESURVEY:
                    case NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY:
                    case NavigationalParameters.AppModes.PRESITECHAMBERSURVEY:
                    case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                    case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                    case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                    case NavigationalParameters.AppModes.PRESITECIVILS:
                    case NavigationalParameters.AppModes.PRESITEPREMISIS:
                    case NavigationalParameters.AppModes.PRESITE:
                    case NavigationalParameters.AppModes.CHAMBERASBUILT:
                    case NavigationalParameters.AppModes.DPASBUILT:
                    case NavigationalParameters.AppModes.BJEASBUILT:
                    case NavigationalParameters.AppModes.DJEASBUILT:
                    case NavigationalParameters.AppModes.FJEASBUILT:
                        await NavigateTo(ViewModelLocator.SelectEndPointPage);
                        break;
                    default:
                        await NavigateTo(ViewModelLocator.SelectStreetPage);
                        break;
                }

                await Alert("No survey selected!",
                    "Please select a survey and continue! should you need furthur assistance please contact support!!");
            }
            else
            {
                await NavigateTo(ViewModelLocator.ProjectListPage);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoStartGps => new(async () =>
    {
        try
        {
            if (NavigationalParameters.PreviewMode)
            {
                await Alert("You cannot add a start location whilst in prieview mode", "Prieview Mode!");
            }
            else
            {
                NavigationalParameters.ReturnPage = "SurveyQuestionsViewModelKey";
                NavigationalParameters.MapType = "StartGps";

                await NavigateTo(ViewModelLocator.FormsMapPage);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToGps => new(async () =>
    {
        try
        {
            NavigationalParameters.MapType = "PoleLocation";

            await NavigateTo(ViewModelLocator.FormsMapPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToMapping => new(async () =>
    {
        try
        {
            await NavigateTo(ViewModelLocator.DesignMapPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoEndGps => new(async () =>
    {
        try
        {
            if (NavigationalParameters.PreviewMode)
            {
                await Alert("You cannot add a end location whilst in prieview mode", "Prieview Mode!");
            }
            else
            {
                NavigationalParameters.MapType = "EndGps";
                await NavigateTo(ViewModelLocator.FormsMapPage);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToDesign => new(async () =>
    {
        NavigationalParameters.MapType = "Drawing";
        try
        {
            NavigationalParameters.SelectedDocument = null;

            var docs = new Documents();
            NavigationalParameters.MapType = "Drawing";
            var QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;

            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    QNumber.ToString(), 0)?
                .FirstOrDefault(x => (x.DocumentTitle
                    .Contains($"{QNumber}") || x.QNumber == QNumber.ToString()) && x.DocumentTitle.ToUpper()
                    .Contains("HLD"));

            if (NavigationalParameters.SelectedDocument != null)
                await NavigateTo(ViewModelLocator.HybridWebViewPage);
            else
                await Alert("Document not found!",
                    "The Drawing is missing please enusre the document exsists in the project documents mapping folder in not please contast support!");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToSLD => new(async () =>
    {
        // NavigationalParameters.MapType = "PreSiteDrawing";
        try
        {
            NavigationalParameters.SelectedDocument = null;
            NavigationalParameters.MapType = "Drawing";
            var docs = new Documents();

            var QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;


            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    QNumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle.ToUpper()
                    .Contains("SLD"));


            if (NavigationalParameters.SelectedDocument != null)
                await NavigateTo(ViewModelLocator.HybridWebViewPage);
            else
                await Alert("Document not found!",
                    "The Drawing is missing please enusre the document exsists in the project documents mapping folder in not please contast support!");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToSM => new(async () =>
    {
        // NavigationalParameters.MapType = "PreSiteDrawing";
        try
        {
            NavigationalParameters.SelectedDocument = null;
            NavigationalParameters.MapType = "Drawing";
            var docs = new Documents();

            var QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;


            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    QNumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle.ToUpper().Contains("MATRIX"));


            if (NavigationalParameters.SelectedDocument != null)
                await NavigateTo(ViewModelLocator.HybridWebViewPage);
            else
                await Alert("Document not found!",
                    "The Drawing is missing please enusre the document exsists in the project documents mapping folder in not please contast support!");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToOverview => new(async () =>
    {
        // NavigationalParameters.MapType = "PreSiteDrawing";
        try
        {
            NavigationalParameters.SelectedDocument = null;
            NavigationalParameters.MapType = "Drawing";
            var docs = new Documents();

            var QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;


            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    QNumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle.ToUpper().Contains("OVERVIEW"));


            if (NavigationalParameters.SelectedDocument != null)
                await NavigateTo(ViewModelLocator.HybridWebViewPage);
            else
                await Alert("Document not found!",
                    "The Drawing is missing please enusre the document exsists in the project documents mapping folder in not please contast support!");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToCivils => new(async () =>
    {
        // NavigationalParameters.MapType = "PreSiteDrawing";
        try
        {
            NavigationalParameters.SelectedDocument = null;
            NavigationalParameters.MapType = "Drawing";
            var docs = new Documents();

            var QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;


            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    QNumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle.ToUpper().Contains("CIVILS"));


            if (NavigationalParameters.SelectedDocument != null)
                await NavigateTo(ViewModelLocator.HybridWebViewPage);
            else
                await Alert("Document not found!",
                    "The Drawing is missing please enusre the document exsists in the project documents mapping folder in not please contast support!");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoPicture => new(async () =>
    {
        try
        {
            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                .FirstOrDefault(x =>
                    x.QuestionId == SelectedQuestion.QuestionId &&
                    x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            SelectedAnswer ??= new SurveyAnswers
            {
                RemoteTableId = 0,
                QuestionId = (decimal)SelectedQuestion?.QuestionId,
                QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                Notifiable = SelectedQuestion?.NotifiableResponse,
                StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                LocationName = NavigationalParameters.CurrentAssignment.LocationName,
                SubmittedDateTime = DateTime.Now,
                CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId,
                Rating = SelectedQuestion.DepthorRating,
                AnswerGiven = "Yes"
            };

            await _assignmentService.SaveCurrentAnswer(SelectedAnswer);

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M) SelectedIndex = 1;

            switch (NavigationalParameters.AppMode)
            {
                case NavigationalParameters.AppModes.SITEINSPECTION:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.AUDITQUESTIONS;
                    await NavigateTo(ViewModelLocator.ProjectImagesPage);

                    break;
                case NavigationalParameters.AppModes.CHAMBERAUDIT:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.CHAMBERAUDIT;
                    await NavigateTo(ViewModelLocator.ProjectImagesPage);
                    break;
                case NavigationalParameters.AppModes.DPASBUILT:
                case NavigationalParameters.AppModes.BJEASBUILT:
                case NavigationalParameters.AppModes.FJEASBUILT:
                case NavigationalParameters.AppModes.CHAMBERASBUILT:
                case NavigationalParameters.AppModes.POLEASBUILT:
                case NavigationalParameters.AppModes.ASBUILT:
                case NavigationalParameters.AppModes.DJEASBUILT:
                case NavigationalParameters.AppModes.CHAMBERMEASURE:
                case NavigationalParameters.AppModes.CABLEMEASURE:
                case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                case NavigationalParameters.AppModes.BJEMEASURE:
                case NavigationalParameters.AppModes.FJEMEASURE:
                case NavigationalParameters.AppModes.DJEMEASURE:
                case NavigationalParameters.AppModes.DUCTMEASURE:
                case NavigationalParameters.AppModes.POLECABLEMEASURE:
                case NavigationalParameters.AppModes.POLEMEASURE:
                case NavigationalParameters.AppModes.UGCRPMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.ASBUILT;

                    if (!string.IsNullOrEmpty(NavigationalParameters.SelectedQuestion?.FollowUpAction))
                    {
                        await NavigateTo(ViewModelLocator.PhotoExamplePage);
                    }
                    else
                    {
                        _psvm.TakePhoto.Execute(null);
                    }

                    break;
                default:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.SURVEYQUESTION;
                    _psvm.TakePhoto.Execute(null);
                    break;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent($"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }

    });

    public RelayCommand GoToDfe => new(async () =>
    {
        try
        {
            if (NavigationalParameters.PreviewMode)
                await Alert("You cannot add a dfe whilst in prieview mode", "Prieview Mode!");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });


    public async void CheckSelectedAnswer()
    {
        try
        {
            var answers = App.Database.GetItems<SurveyAnswers>().ToList();

            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime).FirstOrDefault(x =>
                x.QuestionId == SelectedQuestion?.QuestionId
                && x.Category?.ToLower() == SelectedQuestion?.Category.ToLower()
                && x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            if (NavigationalParameters.SelectedQuestion.ResponseType == "mark location on map, select house, comments, photos")
            {
                if (SelectedAnswer == null)
                {
                    SelectedAnswer = new SurveyAnswers
                    {
                        RemoteTableId = 0,
                        CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                        Notifiable = SelectedQuestion?.NotifiableResponse,
                        AssignmentId = NavigationalParameters.CurrentAssignment?.AssignmentId,
                        Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                        QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                        QuestionId = SelectedQuestion.QuestionId,
                        SubmittedDateTime = DateTime.Now,
                        Complete = "false",
                        AnswerGiven = "true",
                        AppStatus = "IPad"
                    };

                    App.Database.SaveItem(SelectedAnswer);

                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async Task QuestionRatingCommand(ImageButton button)
    {
        try
        {
            var Score = button.ClassId;

            NavigationalParameters.SelectedQuestion =
                SelectedQuestion = button.CommandParameter as RatingQuestionViewModel;

            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                .FirstOrDefault(x =>
                    x.QuestionId == SelectedQuestion.QuestionId &&
                    x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            if (SelectedAnswer == null)
                SelectedAnswer = new SurveyAnswers
                {
                    RemoteTableId = 0,
                    QuestionId = (decimal)SelectedQuestion?.QuestionId,
                    QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                    AnswerGiven = Score,
                    Notifiable = SelectedQuestion?.NotifiableResponse,
                    StreetName = SelectedQuestion.Category,
                    Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                    SubmittedDateTime = DateTime.Now,
                    CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                    AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId,
                    Rating = SelectedQuestion.DepthorRating
                };

            SelectedAnswer.AnswerGiven = Score;

            SelectedAnswer.Complete = "true";

            SurveyAnswers.Add(SelectedAnswer);
            //  NavigationalParameters.AnswerList.Remove(SelectedAnswer);
            //  NavigationalParameters.AnswerList.Add(SelectedAnswer);
            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            await _assignmentService.AddAssignmentsResponse(SelectedAnswer);

            if (NavigationalParameters.CurrentAudit != null)
                await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);

            if (Score.ToLower() != "n/a" && Convert.ToInt32(Score) <= 6)
            {
                NavigationalParameters.CurrentNCR = new Ncr();
                NavigationalParameters.CurrentNCR.QuestionId = SelectedAnswer.QuestionId;
                await NavigateTo(ViewModelLocator.SiteInspectionRatingFailurePage);
            }

            UpdateQuestions(SelectedAnswer);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async Task GetCurrentMultiAnswerAsync(Button button)
    {
        try
        {
            NavigationalParameters.SelectedQuestion =
                SelectedQuestion = button.CommandParameter as MultiQuestionViewModel;

            var answerToSave = "";

            var keyAnswers = SelectedQuestion.KeyAnswer.Split(',').ToList();

            var multiAnswers = new List<string>();

            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                .FirstOrDefault(x =>
                    x.QuestionId == SelectedQuestion.QuestionId &&
                    x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            if (SelectedAnswer == null)
            {
                // multiAnswers.Add(button.Text);

                SelectedAnswer = new SurveyAnswers
                {
                    RemoteTableId = 0,
                    CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                    Notifiable = SelectedQuestion.NotifiableResponse,
                    AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                    Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                    QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                    StreetName = NavigationalParameters.CurrentAssignment.StreetName,
                    QuestionId = SelectedQuestion.QuestionId,
                    Complete = "",
                    AppStatus = "IPad"
                };

                SurveyAnswers?.Add(SelectedAnswer);
            }

            multiAnswers = SelectedAnswer.AnswerGiven.Split(',')?.ToList();

            multiAnswers.Remove("");

            if (multiAnswers.Any(x => x.ToLower() == button.Text.ToLower()))
            {
                multiAnswers.Remove(button.Text);

                if (!multiAnswers.Any(x => keyAnswers.Any(y => y.ToLower() == x.ToLower())))
                {
                    var questionId = Convert.ToInt32(SelectedQuestion?.QuestionId.ToString().Split(".")[0]);

                    if (questionId > 0)
                    {
                        var xx = NavigationalParameters.PreSiteQuestions.Where(x =>
                            x.QuestionId > SelectedQuestion?.QuestionId && x.QuestionId < questionId + 1).ToList();

                        foreach (var q in xx)
                        {
                            QuestionCollection.Remove(q);

                            var answerToRemove = SurveyAnswers?.FirstOrDefault(x => x.QuestionId == q.QuestionId && x.Category == q.Category);

                            if (answerToRemove != null)
                            {
                                SurveyAnswers.Remove(answerToRemove);

                                _assignmentService.RemoveSurveyAnswer(answerToRemove.QuestionId,
                                    NavigationalParameters.CurrentAssignment.AssignmentId);
                            }
                        }
                    }
                }
            }
            else
            {
                multiAnswers.Add(button.Text);

                if (multiAnswers.Any(x => keyAnswers.Any(y => y.ToLower() == x.ToLower())))
                    if (QuestionCollection.All(x => x.QuestionId != SelectedQuestion?.FurtherQuestionId) &&
                        SelectedQuestion?.FurtherQuestionId > 0)
                    {
                        var questionToAdd =
                            NavigationalParameters.GenericQuestions?.FirstOrDefault(x =>
                                x.QuestionId ==
                                SelectedQuestion
                                    ?.FurtherQuestionId); //_assignmentService.GetSurveyQuestions(SelectedQuestion.Category)?.FirstOrDefault(x => x.QuestionId == SelectedQuestion?.FurtherQuestionId);

                        if (questionToAdd != null) QuestionCollection.Add(questionToAdd);
                    }
            }

            foreach (var a in multiAnswers)
                if (!string.IsNullOrEmpty(a))
                    answerToSave += $"{a},";

            SelectedAnswer.AnswerGiven = answerToSave;

            if (NavigationalParameters.AppMode.ToString().ToLower().Contains("chamber") && NavigationalParameters.SelectedQuestion.Question.ToLower().Contains("confirm chamber size"))
            {
                NavigationalParameters.SelectedAsset.DropLength = button.Text;

                App.Database.SaveItem(NavigationalParameters.SelectedAsset);
            }

            SurveyAnswers.Remove(SelectedAnswer);

            SurveyAnswers.Add(SelectedAnswer);

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            App.Database.SaveItem(SelectedAnswer);

            NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();

            UpdateQuestions(SelectedAnswer);

            if (!string.IsNullOrEmpty(NavigationalParameters.SelectedQuestion.FollowUpAction))
            {
                await Alert("Photo", "Please take a photo making sure it is clear and visible");

                await NavigateTo(ViewModelLocator.PhotoExamplePage);

                NavigationalParameters.SelectedPhoto = null;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async void SaveEntryAnswer(string answerGiven, Entry text)
    {
        try
        {
            if (SelectedQuestion != null)
            {
                var questionId = Convert.ToDecimal(SelectedQuestion.QuestionId);

                // SelectedQuestion = QuestionCollection.FirstOrDefault(x => x.QuestionId == x.QuestionId && x.Category == NavigationalParameters.CurrentAssignment.Category.ToString());

                SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                    .FirstOrDefault(x =>
                        x.QuestionId == questionId &&
                        x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                        x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

                SelectedAnswer ??= new SurveyAnswers
                {
                    RemoteTableId = 0,
                    QuestionId = questionId,
                    QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                    Notifiable = SelectedQuestion?.NotifiableResponse,
                    StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                    Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                    LocationName = NavigationalParameters.CurrentAssignment.LocationName,
                    SubmittedDateTime = DateTime.Now,
                    CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                    AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId,
                    Rating = SelectedQuestion.DepthorRating
                };

                SelectedAnswer.AnswerGiven = answerGiven;

                await _assignmentService.SaveCurrentAnswer(SelectedAnswer);

                UpdateQuestions(SelectedAnswer);

                NavigationalParameters.SelectedAnswer = SelectedAnswer;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public async Task GetCurrentAnswerAsync(Button button)
    {
        try
        {
            NavigationalParameters.SelectedQuestion =
                SelectedQuestion = button.CommandParameter as YesNoNaQuestionViewModel;

            var optionsTemp = SelectedQuestion.QuestionOptions?.Split(',')?.ToList();

            SelectedAnswer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                .FirstOrDefault(x =>
                    x.QuestionId == SelectedQuestion.QuestionId &&
                    x.QNumber == NavigationalParameters.CurrentAssignment.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            SurveyAnswers?.Remove(SelectedAnswer);

            if (SelectedAnswer == null)
                SelectedAnswer = new SurveyAnswers
                {
                    RemoteTableId = 0,
                    CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                    Notifiable = SelectedQuestion?.NotifiableResponse,
                    AssignmentId = NavigationalParameters.CurrentAssignment?.AssignmentId,
                    Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                    QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                    StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                    QuestionId = SelectedQuestion.QuestionId,
                    SubmittedDateTime = DateTime.Now,
                    Complete = "",
                    AnswerGiven = button.Text ?? "Yes",
                    AppStatus = "IPad"
                };
            else
                SelectedAnswer.AnswerGiven = button.Text;

            App.Database.SaveItem(SelectedAnswer);

            if (SelectedQuestion.KeyAnswer.ToLower().Contains(button.Text.ToLower()))
            {
                if (SelectedQuestion.FurtherQuestionId > 0)
                    if (QuestionCollection.All(x => x.QuestionId != SelectedQuestion.FurtherQuestionId))
                    {
                        QuestionCollection.Add(NavigationalParameters.GenericQuestions.FirstOrDefault(x =>
                            x.QuestionId == SelectedQuestion.FurtherQuestionId));
                        //  var questionToAdd = _assignmentService.GetSurveyQuestions(SelectedQuestion.Category)?.FirstOrDefault(x => x.QuestionId == SelectedQuestion.FurtherQuestionId);
                        NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();
                    }
            }
            else if (!SelectedQuestion.KeyAnswer.ToLower().Contains(button.Text.ToLower()))
            {
                if (SelectedQuestion.FurtherQuestionId > 0 && SelectedQuestion.FurtherQuestionId != 0.10M && SelectedQuestion.FurtherQuestionId != 0.7M)
                {
                    var questionId = Convert.ToInt32(SelectedQuestion.QuestionId.ToString().Split(".")[0]);

                    var xx = QuestionCollection.Where(x =>
                        x.QuestionId > SelectedQuestion.QuestionId && x.QuestionId < questionId + 1).ToList();

                    foreach (var q in xx)
                    {
                        QuestionCollection.Remove(q);

                        var answerToRemove = SurveyAnswers?.FirstOrDefault(x => x.QuestionId == q.QuestionId && x.Category == q.Category);

                        if (answerToRemove != null)
                        {
                            SurveyAnswers.Remove(answerToRemove);

                            _assignmentService.RemoveSurveyAnswer(answerToRemove.QuestionId,
                                NavigationalParameters.CurrentAssignment.AssignmentId);
                        }
                    }
                }
                else if (SelectedQuestion.FurtherQuestionId == 0.10M)
                {

                    var xx = QuestionCollection.Where(x =>
                        x.QuestionId == 0.10M).ToList();

                    foreach (var q in xx)
                    {
                        QuestionCollection.Remove(q);

                        var answerToRemove = SurveyAnswers?.FirstOrDefault(x => x.QuestionId == q.QuestionId);

                        if (answerToRemove != null)
                        {
                            SurveyAnswers.Remove(answerToRemove);

                            _assignmentService.RemoveSurveyAnswer(answerToRemove.QuestionId,
                                NavigationalParameters.CurrentAssignment.AssignmentId);
                        }
                    }
                }
                else if (SelectedQuestion.FurtherQuestionId == 0.7M)
                {

                    var xx = QuestionCollection.Where(x =>
                        x.QuestionId == 0.7M).ToList();

                    foreach (var q in xx)
                    {
                        QuestionCollection.Remove(q);

                        var answerToRemove = SurveyAnswers?.FirstOrDefault(x => x.QuestionId == q.QuestionId && x.Category == q.Category);

                        if (answerToRemove != null)
                        {
                            SurveyAnswers.Remove(answerToRemove);

                            _assignmentService.RemoveSurveyAnswer(answerToRemove.QuestionId,
                                NavigationalParameters.CurrentAssignment.AssignmentId);
                        }
                    }

                }

                NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();
            }

            SurveyAnswers?.Add(SelectedAnswer);

            NavigationalParameters.SelectedAnswer = SelectedAnswer;

            UpdateQuestions(SelectedAnswer);

            if (!string.IsNullOrEmpty(NavigationalParameters.SelectedQuestion?.FollowUpAction) && SelectedAnswer?.AnswerGiven != "N/A")
            {
                await Alert("Photo", "Please take a photo making sure it is clear and visible");

                await NavigateTo(ViewModelLocator.PhotoExamplePage);

                NavigationalParameters.SelectedPhoto = null;
            }

            if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.DPASBUILT)
            {
                RefreshQuestionList();
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public void UpdateQuestions(SurveyAnswers SelectedAnswer)
    {
        try
        {
            SelectedIndex = FilteredQuestionCollection.IndexOf(
                FilteredQuestionCollection.FirstOrDefault(x => x.QuestionId == SelectedQuestion.QuestionId));

            SelectedAnswer.Category = NavigationalParameters.SurveyType.ToString();
            SelectedAnswer.AppStatus = NavigationalParameters.ClaimFile?.ClaimHeader;

            App.Database.SaveItem(SelectedAnswer);

            var optionsTemp = SelectedQuestion?.QuestionOptions.Split(',').ToList();

            while (optionsTemp.Count < 12) optionsTemp.Add("");

            var keyAnswers = SelectedQuestion?.KeyAnswer.Split(',').ToList();

            var multiAnswers = new List<string>();

            switch (SelectedQuestion?.ResponseType)
            {
                case "Y/N Selection":
                    {
                        try
                        {
                            QuestionCollection.Remove(QuestionCollection?.FirstOrDefault(x => x.QuestionId == SelectedQuestion.QuestionId && x.Category == SelectedQuestion.Category));
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }

                        var questionToAdd = new YesNoNaQuestionViewModel
                        {
                            Question = SelectedQuestion.Question,
                            QuestionId = SelectedAnswer.QuestionId,
                            KeyAnswer = SelectedQuestion.KeyAnswer,
                            Category = SelectedQuestion.Category,
                            DepthorRating = SelectedQuestion.DepthorRating,
                            FollowUpAction = SelectedQuestion.FollowUpAction,
                            FurtherQuestionId = SelectedQuestion.FurtherQuestionId,
                            Grouping = SelectedQuestion.Grouping,
                            InformationTo = SelectedQuestion.InformationTo,
                            NotifiableResponse = SelectedQuestion.NotifiableResponse,
                            QuestionOptions = SelectedQuestion.QuestionOptions,
                            ResponseType = SelectedQuestion.ResponseType,
                            Id = SelectedQuestion.Id,
                            Stage = SelectedQuestion.Stage,
                            BtnYesText = optionsTemp[0],
                            BtnNoText = optionsTemp[1],
                            BtnNaText = optionsTemp[2],
                            IsEnabled = true,
                            ShowButtonA = SelectedQuestion.QuestionId == 0.10M ? false : true,
                            ShowButtonB = SelectedQuestion.QuestionId == 0.10M ? false : true,
                            ShowButtonC = SelectedQuestion.QuestionId == 0.10M ? false : true,
                            QuestionTextColour = Color.FromHex("#292F62")
                        };

                        if (SelectedAnswer != null && !string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            var picExsists = _picService.GetAllPictures().Any(x =>
                                x.QuestionId == SelectedQuestion.QuestionId.ToString() && x.AssignmentId ==
                                NavigationalParameters.CurrentAssignment.AssignmentId);

                            questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                        }
                        else if (SelectedAnswer != null && string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            questionToAdd.QuestionTextColour = Color.DarkGreen;
                        }

                        string answerOption = optionsTemp[0].ToString().ToLower();

                        if (SelectedAnswer.AnswerGiven.ToLower() == answerOption)
                        {
                            questionToAdd.BtnYesBgColour = Color.LawnGreen;
                        }
                        else
                        {
                            questionToAdd.BtnYesBgColour = Color.LightGray;
                        }

                        string  answerOption1 = optionsTemp[1].ToString().ToLower();

                        if (SelectedAnswer.AnswerGiven.ToLower() == answerOption1)
                        {
                            questionToAdd.BtnNoBgColour = Color.LawnGreen;
                        }
                        else
                        {
                            questionToAdd.BtnNoBgColour = Color.LightGray;
                        }

                        string answerOption2 = optionsTemp[2].ToString().ToLower();

                        if (SelectedAnswer.AnswerGiven.ToLower() == answerOption2)
                        {
                            questionToAdd.BtnNaBgColour = Color.LawnGreen;
                        }
                        else
                        {
                            questionToAdd.BtnNaBgColour = Color.LightGray;
                        }


                        QuestionCollection.Add(questionToAdd);

                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(QuestionCollection.OrderBy(x => x.QuestionId));

                        NavigationalParameters.PreSiteQuestions = FilteredQuestionCollection.ToList();

                        break;
                    }
                case "multiSelector":
                    {
                        try
                        {
                               QuestionCollection.Remove(QuestionCollection?.FirstOrDefault(x => x.QuestionId == SelectedQuestion.QuestionId && x.Category == SelectedQuestion.Category));
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }

                        multiAnswers = SelectedAnswer.AnswerGiven.Split(',').ToList();

                        var questionToAdd = new MultiQuestionViewModel
                        {
                            Question = SelectedQuestion.Question,
                            QuestionId = SelectedQuestion.QuestionId,
                            KeyAnswer = SelectedQuestion.KeyAnswer,
                            Category = SelectedQuestion.Category,
                            DepthorRating = SelectedQuestion.DepthorRating,
                            FollowUpAction = SelectedQuestion.FollowUpAction,
                            FurtherQuestionId = SelectedQuestion.FurtherQuestionId,
                            Grouping = SelectedQuestion.Grouping,
                            InformationTo = SelectedQuestion.InformationTo,
                            NotifiableResponse = SelectedQuestion.NotifiableResponse,
                            QuestionOptions = SelectedQuestion.QuestionOptions,
                            ResponseType = SelectedQuestion.ResponseType,
                            Id = SelectedQuestion.Id,
                            Stage = SelectedQuestion.Stage,
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

                            BtnABgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[0].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnBBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[1].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnCBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[2].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnDBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[3].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnEBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[4].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnFBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[5].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnGBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[6].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnHBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[7].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnIBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[8].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnJBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[9].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnKBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[10].ToLower())
                                ? Color.LawnGreen
                                : Color.LightGray,

                            BtnLBgColour = multiAnswers.Any(x => x.ToLower() == optionsTemp[11].ToLower())
                                ? Color.LawnGreen
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
                            IsEnabled = true,
                            QuestionTextColour = Color.FromHex("#292F62")
                        };


                        if (SelectedAnswer != null && !string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            var picExsists = _picService.GetAllPictures().Any(x =>
                                x.QuestionId == SelectedQuestion.QuestionId.ToString() && x.AssignmentId ==
                                NavigationalParameters.CurrentAssignment.AssignmentId);

                            questionToAdd.QuestionTextColour = picExsists ? Color.Green : Color.Red;
                        }
                        else if (SelectedAnswer != null && string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            questionToAdd.QuestionTextColour = Color.DarkGreen;
                        }

                        //    FilteredQuestionCollection.Add(questionToAdd);

                        try
                        {
                            QuestionCollection.Add(questionToAdd);

                            FilteredQuestionCollection =
                                new ObservableCollection<SurveyQuestion>(QuestionCollection.OrderBy(x => x.QuestionId));

                            NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }

                        break;
                    }
                case "numberSelector":
                    {
                        try
                        {
                            QuestionCollection.Remove(QuestionCollection?.FirstOrDefault(x => x.QuestionId == SelectedQuestion.QuestionId && x.Category == SelectedQuestion.Category));
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        var questionToAdd = new FreeTextQuestionViewModel
                        {
                            Question = SelectedQuestion.Question,
                            QuestionId = SelectedAnswer.QuestionId,
                            KeyAnswer = SelectedQuestion.KeyAnswer,
                            Category = SelectedQuestion.Category,
                            DepthorRating = SelectedQuestion.DepthorRating,
                            FollowUpAction = SelectedQuestion.FollowUpAction,
                            FurtherQuestionId = SelectedQuestion.FurtherQuestionId,
                            Grouping = SelectedQuestion.Grouping,
                            InformationTo = SelectedQuestion.InformationTo,
                            NotifiableResponse = SelectedQuestion.NotifiableResponse,
                            QuestionOptions = SelectedQuestion.QuestionOptions,
                            ResponseType = SelectedQuestion.ResponseType,
                            Id = SelectedQuestion.Id,
                            Stage = SelectedQuestion.Stage,
                            IsEnabled = true,
                            AnswerGiven = Convert.ToInt32(SelectedAnswer.AnswerGiven),
                            QuestionTextColour = Color.FromHex("#292F62")
                        };

                        if (SelectedAnswer != null && !string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            var picExsists = _picService.GetAllPictures().Any(x =>
                                x.QuestionId == SelectedQuestion.QuestionId.ToString() && x.AssignmentId ==
                                NavigationalParameters.CurrentAssignment.AssignmentId);

                            questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                        }
                        else if (SelectedAnswer != null && string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            questionToAdd.QuestionTextColour = Color.DarkGreen;
                        }

                        QuestionCollection.Add(questionToAdd);

                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(QuestionCollection.OrderBy(x => x.QuestionId));

                        NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();

                        break;
                    }
                default:
                    {
                        try
                        {
                            QuestionCollection.Remove(QuestionCollection?.FirstOrDefault(x => x.QuestionId == SelectedQuestion.QuestionId && x.Category == SelectedQuestion.Category));
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        var questionToAdd = new LocationIdentityQuestionViewModel
                        {
                            Question = SelectedQuestion.Question,
                            QuestionId = SelectedAnswer.QuestionId,
                            KeyAnswer = SelectedQuestion.KeyAnswer,
                            Category = SelectedQuestion.Category,
                            DepthorRating = SelectedQuestion.DepthorRating,
                            FollowUpAction = SelectedQuestion.FollowUpAction,
                            FurtherQuestionId = SelectedQuestion.FurtherQuestionId,
                            Grouping = SelectedQuestion.Grouping,
                            InformationTo = SelectedQuestion.InformationTo,
                            NotifiableResponse = SelectedQuestion.NotifiableResponse,
                            QuestionOptions = SelectedQuestion.QuestionOptions,
                            ResponseType = SelectedQuestion.ResponseType,
                            Id = SelectedQuestion.Id,
                            Stage = SelectedQuestion.Stage,
                            IsEnabled = true,
                            AnswerGiven = "Yes",
                            QuestionTextColour = Color.FromHex("#292F62")
                        };

                        if (SelectedAnswer != null && !string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            var picExsists2 = _picService.GetAllPictures().Where(x =>
                              x.QuestionId == SelectedQuestion.QuestionId.ToString() && x.AssignmentId ==
                              NavigationalParameters.CurrentAssignment.AssignmentId).ToList();

                            var picExsists = _picService.GetAllPictures().Any(x =>
                               x.QuestionId == SelectedQuestion.QuestionId.ToString() && x.AssignmentId ==
                               NavigationalParameters.CurrentAssignment.AssignmentId);

                            questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                        }
                        else if (SelectedAnswer != null && string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        {
                            questionToAdd.QuestionTextColour = Color.DarkGreen;
                        }

                        QuestionCollection.Add(questionToAdd);

                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(QuestionCollection.OrderBy(x => x.QuestionId));

                        NavigationalParameters.PreSiteQuestions = QuestionCollection.ToList();

                        //if (FilteredQuestionCollection.Any(x =>
                        //        x.QuestionId == SelectedAnswer.QuestionId && x.Category == SelectedAnswer.Category))
                        //{
                        //    if (SelectedAnswer != null && !string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        //    {
                        //        var picExsists = _picService.GetAllPictures().Any(x =>
                        //            x.QuestionId == SelectedQuestion.QuestionId.ToString() && x.AssignmentId ==
                        //            NavigationalParameters.CurrentAssignment.AssignmentId);

                        //        FilteredQuestionCollection.FirstOrDefault(x =>
                        //                x.QuestionId == SelectedAnswer.QuestionId && x.Category == SelectedAnswer.Category)
                        //            .QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                        //    }
                        //    else if (SelectedAnswer != null && string.IsNullOrEmpty(SelectedQuestion.FollowUpAction))
                        //    {
                        //        FilteredQuestionCollection.FirstOrDefault(x =>
                        //                x.QuestionId == SelectedAnswer.QuestionId && x.Category == SelectedAnswer.Category)
                        //            .QuestionTextColour = Color.DarkGreen;
                        //    }

                        //}

                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public void FilterQuestions(string filter)
    {
        try
        {
            if (QuestionCollection.Any())
                // if (!string.IsNullOrEmpty(filter))
                switch (filter.ToLower())
                {
                    case "civils":
                        ButtonAColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    case "third party apparatus":
                        ButtonBColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    case "hse":
                        ButtonCColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    case "wayleaves / easement":
                        ButtonDColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    case "reinstatement":
                        ButtonEColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    case "tm requirements":
                        ButtonFColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    case "noticing / coordination":
                        ButtonGColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .Where(x => x.Stage.ToLower() == filter.ToLower())
                                    .OrderBy(x => x.QuestionId));
                        break;
                    default:
                        ButtonHColour = Color.Green;
                        FilteredQuestionCollection =
                            new ObservableCollection<SurveyQuestion>(
                                QuestionCollection
                                    .OrderBy(x => x.QuestionId));
                        break;
                }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    public RelayCommand SavePresiteCheck => new(async () =>
    {
        try
        {
            ShowPage = false;

            var allPhotosTaken = "There is no photo taken against the following questions: ";

            foreach (var ques in FilteredQuestionCollection?.Where(x => !string.IsNullOrEmpty(x.FollowUpAction)))
            {
                var answer = App.Database.GetItems<SurveyAnswers>()?.OrderByDescending(x => x.SubmittedDateTime)
                    .FirstOrDefault(x =>
                        x.QuestionId == ques.QuestionId &&
                        x.QNumber == NavigationalParameters.CurrentAssignment?.Qnumber &&
                        x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

                if (answer != null && answer.AnswerGiven != "N/A")
                {
                    var pics = _picService.GetAllPictures()?.Where(x =>
                        x.QuestionId == ques.QuestionId.ToString() &&
                        x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

                    if (!pics.Any())
                    {
                        allPhotosTaken += $"{ques.Question}, ";
                    }
                }
            }

            if (allPhotosTaken != "There is no photo taken against the following questions: ")
            {
                await Alert("Missing Photos", allPhotosTaken);

                ShowPage = true;
            }
            else
            {
                IsLoading = true;
                EnableSubmit = false;
                NavigationalParameters.AuthDetail ??= new AuthorisationDetail();

                NavigationalParameters.CurrentAssignment.CompletedOn = DateTime.Now;

                NavigationalParameters.SelectedAsset.SavedToServer = false;

                //NavigationalParameters.CurrentAssignment.Complete = "false";

                if ((NavigationalParameters.SelectedEndPoint != null || NavigationalParameters.SelectedAsset != null) &&
                    !string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.L4Number))
                {
                    NavigationalParameters.CurrentAssignment.LocationName =
                        NavigationalParameters.SelectedEndPoint?.L4Number ??
                        NavigationalParameters.SelectedAsset?.L4Number;
                 
                        NavigationalParameters.SelectedAsset.Status = NavigationalParameters.AppMode.ToString().ToUpper();
                }

                App.Database.SaveItem(NavigationalParameters.SelectedAsset);

                await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

                switch (NavigationalParameters.SurveyType)
                {
                    case NavigationalParameters.SurveyTypes.PERMITTODIG:
                        NavigationalParameters.AppMode = NavigationalParameters.AppModes.PERMITTODIG;
                        if (SurveyAnswers?.Count() >= FilteredQuestionCollection?.Count())
                        {
                            try
                            {
                                var distance = Convert.ToDecimal(Distance);

                                NavigationalParameters.AuthorisationType =
                                    NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
                                NavigationalParameters.CurrentPermit.Granted = true;
                                NavigationalParameters.CurrentPermit.Distance = distance;

                                foreach (var answer in from answer in SurveyAnswers
                                                       let keyAnswer =
                                                           FilteredQuestionCollection
                                                               ?.FirstOrDefault(x => x.QuestionId == answer?.QuestionId)?.KeyAnswer
                                                               .ToLower()
                                                       where keyAnswer.ToLower().Contains(answer.AnswerGiven?.ToLower()) == false
                                                       select answer) NavigationalParameters.CurrentPermit.Granted = false;

                                var current = Connectivity.NetworkAccess;

                                if (current == NetworkAccess.Internet)
                                {
                                    await NavigateTo(ViewModelLocator.SignaturePage);

                                    Distance = "";
                                }
                                else
                                {
                                    await Alert("No internet connectivity",
                                        "Subbmission failed due to lack of connectivity, please find a better connection and try again",
                                        "Ok");
                                    IsLoading = false;
                                    EnableSubmit = true;
                                    ShowPage = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                await Alert("Please enter a decimal value for the distance ", "Incomplete", "Ok");
                                IsLoading = false;
                                EnableSubmit = true;
                                ShowPage = true;
                            }
                        }
                        else
                        {
                            await Alert("All question need to be answered before submitting", "Incomplete", "Ok");
                            IsLoading = false;
                            EnableSubmit = true;
                            ShowPage = true;
                        }
                        break;
                    case NavigationalParameters.SurveyTypes.VERTISHOREPERMIT:
                        NavigationalParameters.AppMode = NavigationalParameters.AppModes.PERMITTODIG;
                        if (SurveyAnswers?.Count() <= FilteredQuestionCollection?.Count())
                        {
                            try
                            {
                                var distance = Convert.ToDecimal(Distance);

                                NavigationalParameters.AuthorisationType =
                                    NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
                                NavigationalParameters.CurrentPermit.Granted = true;
                                NavigationalParameters.CurrentPermit.Distance = distance;

                                foreach (var answer in from answer in SurveyAnswers
                                                       let keyAnswer =
                                                           FilteredQuestionCollection
                                                               ?.FirstOrDefault(x => x.QuestionId == answer?.QuestionId)?.KeyAnswer
                                                               .ToLower()
                                                       where keyAnswer.ToLower().Contains(answer.AnswerGiven.ToLower()) == false
                                                       select answer) NavigationalParameters.CurrentPermit.Granted = false;

                                var current = Connectivity.NetworkAccess;

                                if (current == NetworkAccess.Internet)
                                {
                                    // Connection to internet is available
                                    await NavigateTo(ViewModelLocator.SignaturePage);
                                    Distance = "";
                                }
                                else
                                {
                                    await Alert("No internet connectivity",
                                        "Subbmission failed due to lack of connectivity, please find a better connection and try again",
                                        "Ok");
                                    ShowPage = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                await Alert("Please enter a decimal value for the distance ", "Incomplete", "Ok");
                                ShowPage = true;
                            }
                        }
                        else
                        {
                            await Alert("All question need to be answered before submitting", "Incomplete", "Ok");
                            ShowPage = true;
                        }
                        break;
                    case NavigationalParameters.SurveyTypes.SITECLEAR:
                        NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITECLEAR;
                        if (SurveyAnswers?.Count() >= FilteredQuestionCollection?.Count())
                        {
                            NavigationalParameters.AuthorisationType =
                                NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                            NavigationalParameters.CurrentPermit.Granted = true;

                            foreach (var answer in SurveyAnswers?.Where(answer =>
                                         (bool)FilteredQuestionCollection
                                             ?.FirstOrDefault(x => x.QuestionId == answer?.QuestionId)?.KeyAnswer
                                             .ToLower()?.Contains(answer?.AnswerGiven?.ToLower())))
                                NavigationalParameters.CurrentPermit.Granted = false;

                            var current = Connectivity.NetworkAccess;

                            if (current == NetworkAccess.Internet)
                                // Connection to internet is available
                                await NavigateTo(ViewModelLocator.SignaturePage);
                            else
                                await Alert("No internet connectivity",
                                    "Subbmission failed due to lack of connectivity, please find a better connection and try again",
                                    "Ok");
                            ShowPage = true;
                        }
                        else
                        {
                            await Alert("Complete survey needs completing before submitting", "Incomplete", "Ok");
                            ShowPage = true;
                        }
                        break;
                    case NavigationalParameters.SurveyTypes.POLEMEASURE:
                    case NavigationalParameters.SurveyTypes.POLECABLEMEASURE:
                    case NavigationalParameters.SurveyTypes.CHAMBERMEASURE:
                    case NavigationalParameters.SurveyTypes.UGCRPMEASURE:
                    case NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE:
                    case NavigationalParameters.SurveyTypes.DUCTMEASURE:
                    case NavigationalParameters.SurveyTypes.CHAMBERASBUILT:
                    case NavigationalParameters.SurveyTypes.DPASBUILT:
                    case NavigationalParameters.SurveyTypes.POLEASBUILT:
                    case NavigationalParameters.SurveyTypes.BJEASBUILT:
                    case NavigationalParameters.SurveyTypes.DJEASBUILT:
                    case NavigationalParameters.SurveyTypes.FJEASBUILT:
                        { 
                            NavigationalParameters.SelectedAsset.Status = NavigationalParameters.SurveyType.ToString().ToUpper();
                            App.Database.SaveItem(NavigationalParameters.SelectedAsset);
                            App.Database.SaveItem(NavigationalParameters.CurrentAssignment);

                            if (NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
                            {
                                App.Database.SaveItem(NavigationalParameters.SelectedAsset);

                                var ans = SurveyAnswers?.Where(x => x.AppStatus.ToLower() == NavigationalParameters.ClaimFile?.ClaimHeader.ToLower()).ToList();

                               // if (ans.Count() >= FilteredQuestionCollection?.Count() - 1)
                               // {
                                var claimFileExsists = App.Database.GetItems<ClaimedFile>()?.FirstOrDefault(x => x.QuoteId == NavigationalParameters.ClaimFile?.QuoteId
                                && x.ClaimHeader == NavigationalParameters.ClaimFile?.ClaimHeader && x.ClaimId == NavigationalParameters.ClaimFile?.ClaimId);

                                if (claimFileExsists == null || claimFileExsists.ClaimQty <= 0)
                                {
                                    NavigationalParameters.ClaimFile.ClaimQty = 1;

                                    App.Database.SaveItem(NavigationalParameters.ClaimFile);
                                }

                                SubmitPresiteCheck();
                              //  }
                              //  else
                              //  {
                              //      await Alert("Complete questions with supporting images before submitting", "Incomplete", "Ok");
                               // }
                            }
                            else
                            {
                                SubmitPresiteCheck();

                                //await NavigateTo(ViewModelLocator.SelectEndPointPage);
                            }
                        }
                        break;
                    default:
                        SubmitPresiteCheck();
                      //  await NavigateTo(ViewModelLocator.SelectEndPointPage);
                        break;
                }

                IsLoading = false;
                EnableSubmit = true;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await Alert("Error!", "An issue has occured saving the survey. This has been saved");
            IsLoading = false;
            EnableSubmit = true;
            ShowPage = true;
        }
    });

    private async void SubmitPresiteCheck()
    {
        try
        {
            var wa = new WebApi();

            var areWeConnected = await wa.CanWeConnect2Api();

            var dataPassedToserver = new DataPassed2Server();

            var assignmentListToBerUploaded = new List<Assignment>();

            var jobService = new Jobs();


            if (areWeConnected)
            {
                await Alert("Saving", "The survey is currently being uploaded to the server." +
                    " This may take a little longer if signal is bad please continue to wait until this has completed! Appologies for any inconvienince should this issue persist please contact support!",
                                     "Ok");

                dataPassedToserver.JobData = NavigationalParameters.CurrentSelectedJob;

                dataPassedToserver.Assignments = new List<Assignment>();

                if (NavigationalParameters.AppMode.ToString().ToLower().Contains("measure")
                    || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt")
                    || NavigationalParameters.AppMode.ToString().ToLower().Contains("presite"))
                {
                    assignmentListToBerUploaded = new List<Assignment>() { NavigationalParameters.CurrentAssignment };
                }
                else
                {
                    assignmentListToBerUploaded = _assignmentService.GetAsBuiltAssignmentsToUpload();
                }

                if (assignmentListToBerUploaded.Any())
                {
                    foreach (var assignment in assignmentListToBerUploaded)
                    {
                        var answers = (bool)App.Database.GetItems<SurveyAnswers>()?.Any(x =>
                            x.AssignmentId == assignment.AssignmentId && x.RemoteTableId == 0);

                        dataPassedToserver.Category = NavigationalParameters.SurveyType.ToString();

                        if (answers)
                        {
                            dataPassedToserver.Assignments.Add(_assignmentService.GenerateAssignments2SaveById(assignment));
                        }
                        else
                        {
                            App.Database.DeleteItem(assignment);

                            continue;
                        }
                    }

                    if (dataPassedToserver.Category != null)
                    {
                        dataPassedToserver = jobService.GetNewNewCabinetsAndEndPoints(dataPassedToserver);

                        var success = await jobService.SaveDailyInputAsync(dataPassedToserver);

                        dataPassedToserver = null;

                        if (!success)
                        {
                            await Alert("An issue has occured submitting the survey. The survey has been saved",
                                "Error please contact support!");
                            ShowPage = true;
                            NavigateBack();
                        }

                        if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite")
                        || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt")
                        || NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
                        {
                            await NavigateTo(ViewModelLocator.SelectEndPointPage);
                        }
                        else
                        {
                            await NavigateTo(ViewModelLocator.MenuSelectionPage);
                        }
                    }
                    else
                    {
                        await Alert("There is nothing to upload. ", "Upload  complete");
                        ShowPage = true;
                    }
                }
                else
                {
                    await Alert("There is nothing to upload. ", "Upload  complete");
                    ShowPage = true;
                }
            }
            else
            {
                await Alert("Please connect to a network and try again. The survey has been saved in the pad and requires uplading when possible. ",
                    "Caution - No Connectivity");
                ShowPage = true;
                await NavigateTo(ViewModelLocator.MeasureListPage);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
            ShowPage = true;
            await Alert("An issue has occured submitting the survey. This has been saved", "Error!");
        }
    }
}
