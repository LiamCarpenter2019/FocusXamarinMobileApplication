#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Forms;
using Event = FocusXamarinMobileApplication.Models.Event;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class MethodologyPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    //private readonly ISignatureService signatureService;
    private const string FileFormat = "{0:dd-MM-yyyy_hh-mm-ss_tt}.{1}";
    private readonly Jobs _jobService;
    private readonly Users _userService;

    private string _clientName;
    private string _damageId;
    private string _incidentDateTime;
    private bool _isLoading;
    private string _location;


    private ObservableCollection<string> _yesNoEmpty = new() { "", "Yes", "No" };

    public AuthorisationDetail SigDetails;

    public MethodologyPageViewModel()
    {
        _db = new FocusMobileV3Database();

        _jobService = new Jobs();

        _userService = new Users();

        Event = NavigationalParameters.EventManagement;

        if (Event.Category.ToUpper().Contains("UTILITYDAMAGE"))
            BackButtonText = "<== Back to utility information";
        else
            BackButtonText = "<== Back to damage details";
    }

    public Event Event { get; private set; }

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

    public ObservableCollection<string> DamageLocations { get; set; } =
        new() { "", "Footway", "Carriageway", "Unmade" };

    public Investigation _investigationDetails { get; set; }
    public FocusMobileV3Database _db { get; set; }
    public DamageToInvestigate DamageToInvestigate { get; set; }
    public PublicUtilityDamageQuestion SelectedDamageType { get; set; }

    public int _anserSelectionIndex { get; set; }
    public bool _answerSelection { get; set; }
    public bool _visible { get; set; }

    private List<InvestigationQuestion> _investigationQuestions { get; set; }
    public List<PublicUtilityDamageQuestion> DamageTypes { get; set; } = new();

    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
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

    public List<InvestigationQuestion> InvestigationQuestions
    {
        get => _investigationQuestions;
        set
        {
            _investigationQuestions = value;
            OnPropertyChanged();
        }
    }

    public bool AnswerSelection
    {
        get => _answerSelection;
        set
        {
            _answerSelection = value;
            OnPropertyChanged();
        }
    }

    public int AnserSelectionIndex
    {
        get => _anserSelectionIndex;
        set
        {
            _anserSelectionIndex = value;
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

    public string BackButtonText
    {
        get => _backButtonText;
        set
        {
            _backButtonText = value;
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

    public Investigation InvestigationDetails
    {
        get => _investigationDetails;
        set
        {
            _investigationDetails = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _cameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");

    public ImageSource Documents
    {
        get => _documents;
        set
        {
            _documents = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand RefreshData => new(() =>
    {
        Event = NavigationalParameters.EventManagement;

        ProjectName = Event?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault()?.ProjectName;

        ProjectDate = DateTime.Now.ToShortDateString();

        if (Event.Category.ToUpper().Contains("UTILITYDAMAGE"))
            BackButtonText = "<== Back to utility information";
        else
            BackButtonText = "<== Back to damage details";

        DamageToInvestigate = Event?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault();

        if (DamageToInvestigate != null)
        {
            DamageToInvestigate.SavedToServer = false;

            _ = _jobService.SaveInvestigateDamage(DamageToInvestigate);

            Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
                .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

            Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

            NavigationalParameters.EventManagement = Event;

            RefreshQuestions();
        }
    });

    public RelayCommand DamageDetailsCommand => new(async () =>
    {
        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        NavigationalParameters.EventManagement = Event;

        if (NavigationalParameters.EventManagement.Category.ToUpper().Contains("UTILITYDAMAGE"))
            await NavigateTo(ViewModelLocator.InvestigationDetailPage);
        else
            await NavigateTo(ViewModelLocator.InvestigateDamagePage);
    });

    public RelayCommand DamageDetails2Command => new(async () =>
    {
        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);

        NavigationalParameters.EventManagement = Event;

        var valid = ValidateInvestigation();

        if (valid)
            await NavigateTo(ViewModelLocator.InvestigationDetail2Page);
        else
            await Alert("Please Compleate all selections before proceding", "Incomplete");
    });

    public string _backButtonText { get; private set; }


    private bool ValidateInvestigation()
    {
        var Answers = App.Database.GetItems<InvestigationAnswer>()?.Where(x =>
                //x.DamageId.ToString() == DamageToInvestigate.DamageInvestigated.DamageId
                x.InvestigationId == DamageToInvestigate?.DamageInvestigated?.RemoteTabledId)?
            .GroupBy(x => x.QuestionId)?
            .ToList();


        if (Answers.Any())
            return true;
        return false;
    }

    public void RefreshQuestions()
    {
        var InvestigationQuestionList = new List<InvestigationQuestion>();

        var invQuestions = _jobService.GetInvestigationQuestions()
            .Where(x => x.Category.ToLower()
                .Contains(NavigationalParameters.EventManagement.Category.ToLower()));

        DamageToInvestigate?.DamageInvestigated?.InvestigationAnswers.Clear();

        var allanswers = App.Database.GetItems<InvestigationAnswer>();

        if (allanswers.Any())
        {
            var Answers = allanswers?.Where(x =>
                    //x.DamageId.ToString() == DamageToInvestigate.DamageInvestigated.DamageId
                    x.InvestigationId.ToString() == DamageToInvestigate?.InvestigationId)?
                .GroupBy(x => x.QuestionId)?
                .ToList();

            foreach (var a in Answers)
                DamageToInvestigate.DamageInvestigated.InvestigationAnswers.Add(a.OrderByDescending(x => x.InputOn)
                    .FirstOrDefault());
        }

        foreach (var q in invQuestions)
            if (InvestigationQuestionList.All(x =>
                    x.QuestionId != q.QuestionId))
            {
                var ca = DamageToInvestigate?.DamageInvestigated?.InvestigationAnswers?
                    .Where(x => x.QuestionId == q.QuestionId)
                    .OrderByDescending(x => x.InputOn)
                    ?.FirstOrDefault();

                q.Btn1 = Color.LightGray;
                q.Btn2 = Color.LightGray;
                q.Btn3 = Color.LightGray;
                // q.Btn4 = Color.LightGray;
                // q.IsVisible = q.QuestionId == "16";
                q.Answer1 = q.QuestionId == "16" ? "Power" : "Yes";
                q.Answer2 = q.QuestionId == "16" ? "Radio" : "No";
                q.Answer3 = q.QuestionId == "16" ? "Line / Genny" : "N/A";
                // q.Answer4 = q.QuestionId == "16" ? "Sonde" : "";

                if (ca != null)
                {
                    if (q.QuestionId == "16")
                    {
                        if ((bool)ca?.Answer.ToLower().Contains("power")) q.Btn1 = Color.LawnGreen;

                        if ((bool)ca?.Answer.ToLower().Contains("radio")) q.Btn2 = Color.LawnGreen;

                        if ((bool)ca?.Answer.ToLower().Contains("line / genny")) q.Btn3 = Color.LawnGreen;
                    }
                    else
                    {
                        switch (ca?.Answer.ToLower())
                        {
                            case "yes":
                            case "true":
                            case "1":
                                q.Btn1 = Color.LawnGreen;
                                q.Btn2 = Color.LightGray;
                                q.Btn3 = Color.LightGray;
                                break;
                            case "no":
                            case "false":
                            case "2":
                                q.Btn2 = Color.LawnGreen;
                                q.Btn1 = Color.LightGray;
                                q.Btn3 = Color.LightGray;
                                break;
                            case "n/a":
                            case "3":
                                q.Btn3 = Color.LawnGreen;
                                q.Btn1 = Color.LightGray;
                                q.Btn2 = Color.LightGray;
                                break;
                            default:
                                q.Btn1 = Color.LightGray;
                                q.Btn2 = Color.LightGray;
                                q.Btn3 = Color.LightGray;
                                break;
                        }
                    }
                }

                InvestigationQuestionList.Add(q);
            }

        InvestigationQuestions = InvestigationQuestionList;
        OnPropertyChanged("InvestigationQuestions");
    }

    public async Task GetCurrentAnswer(Button button)
    {
        var question = button.CommandParameter as InvestigationQuestion;

        var currentInvestigationAnswer = App.Database.GetItems<InvestigationAnswer>()?.Where(x =>
                x.QuestionId == question.QuestionId
                && x.InvestigationId.ToString() == DamageToInvestigate.InvestigationId)
            .OrderByDescending(x => x.InputOn)
            .FirstOrDefault();

        currentInvestigationAnswer = currentInvestigationAnswer ??
                                     new InvestigationAnswer
                                     {
                                         QuestionId = question?.QuestionId,
                                         InvestigationId = Convert.ToInt64(DamageToInvestigate?.InvestigationId),
                                         PublicUtilityDamageGuid = DamageToInvestigate.DamageGuid,
                                         Comment = "",
                                         IsSelected = false,
                                         Ticked = false,
                                         Answer = "",
                                         DamageId = Convert.ToInt64(DamageToInvestigate.DamageId)
                                     };

        if (question.QuestionId == "16")
        {
            var answersTemp = currentInvestigationAnswer?.Answer.Split(',').ToList();

            var answers = new List<string>();

            if (answersTemp.All(x => x.ToLower() != button.Text.ToLower())) answers.Add(button.Text);
            //else
            //{
            //    answersTemp.Remove(button.Text.ToLower());
            //}                   

            foreach (var a in answersTemp.Where(a => a.ToLower() != button.Text.ToLower())
                         .Where(a => !string.IsNullOrEmpty(a)))
                if (answers.All(x => x.ToLower() != a.ToLower()))
                    answers.Add(a);
                else
                    answers.Remove(a);


            currentInvestigationAnswer.Answer = "";

            foreach (var answer in answers) currentInvestigationAnswer.Answer += $"{answer},";
            //currentInvestigationAnswer.Answer += $"{button.Text},";
        }
        else
        {
            currentInvestigationAnswer.Answer = button.Text;
        }


        currentInvestigationAnswer.InputOn = DateTime.Now;

        App.Database.SaveItem(currentInvestigationAnswer);
    }

    public async void GoPhoto(ImageButton imageButton)
    {
        var question = imageButton?.CommandParameter as InvestigationQuestion;

        if (question != null)
        {
            var currentInvestigationAnswer = App.Database.GetItems<InvestigationAnswer>()?.Where(x =>
                                                     x.QuestionId == question.QuestionId
                                                     && x.InvestigationId.ToString() ==
                                                     DamageToInvestigate.InvestigationId)
                                                 .OrderByDescending(x => x.InputOn)?.FirstOrDefault() ??
                                             new InvestigationAnswer
                                             {
                                                 QuestionId = question?.QuestionId,
                                                 InvestigationId = Convert.ToInt64(DamageToInvestigate.InvestigationId),
                                                 PublicUtilityDamageGuid = DamageToInvestigate.DamageGuid,
                                                 Comment = "",
                                                 IsSelected = false,
                                                 Ticked = false,
                                                 Answer = "",
                                                 DamageId = Convert.ToInt64(DamageToInvestigate.DamageId)
                                             };

            if (currentInvestigationAnswer != null)
            {
                NavigationalParameters.CurrentInvestigationAnswer = currentInvestigationAnswer;
                NavigationalParameters.CurrentInvestigationAnswer.InputOn = DateTime.Now;
                NavigationalParameters.CurrentInvestigationAnswer.Ticked = false;
                App.Database.SaveItem(NavigationalParameters.CurrentInvestigationAnswer);
            }
        }


        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());
        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);
        NavigationalParameters.EventManagement = Event;

        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.INVESTIGATION;
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    }

    public async void GoComments(ImageButton imageButton)
    {
        var question = imageButton.CommandParameter as InvestigationQuestion;

        var currentInvestigationAnswer = App.Database.GetItems<InvestigationAnswer>()?.Where(x =>
                                                 x.QuestionId == question.QuestionId
                                                 && x.InvestigationId == DamageToInvestigate.DamageInvestigated
                                                     .RemoteTabledId)
                                             .OrderByDescending(x => x.InputOn).FirstOrDefault() ??
                                         new InvestigationAnswer
                                         {
                                             QuestionId = question?.QuestionId,
                                             InvestigationId = Convert.ToInt64(DamageToInvestigate.InvestigationId),
                                             PublicUtilityDamageGuid = DamageToInvestigate.DamageGuid,
                                             Comment = "",
                                             IsSelected = false,
                                             Ticked = false,
                                             Answer = "",
                                             DamageId = Convert.ToInt64(DamageToInvestigate.DamageId)
                                         };

        NavigationalParameters.CurrentInvestigationAnswer = currentInvestigationAnswer;
        NavigationalParameters.CurrentInvestigationAnswer.InputOn = DateTime.Now;
        NavigationalParameters.CurrentInvestigationAnswer.Ticked = false;
        App.Database.SaveItem(NavigationalParameters.CurrentInvestigationAnswer);

        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Remove(NavigationalParameters.EventManagement
            .Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());
        Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);
        NavigationalParameters.EventManagement = Event;
        await NavigateTo(ViewModelLocator.FormsCommentPage);
    }
}