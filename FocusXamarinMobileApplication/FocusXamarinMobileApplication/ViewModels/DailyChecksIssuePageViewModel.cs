using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Services;
using MethodTimer;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class DailyChecksIssuePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly PhotoSelectionPageViewModel _psvm;

    private readonly Checks check;

    [Time]
    public DailyChecksIssuePageViewModel()
    {
        check = new Checks();
        Title = "New issue to report!";
        _psvm = new PhotoSelectionPageViewModel();
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

    //  public Checks4TabletResponses answerToUpdate { get; set; }
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

    public bool _showSection1 { get; set; } = true;

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; } = true;

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection3 { get; set; } = true;

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection4 { get; set; } = true;

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged();
        }
    }


    public string _issueDetails { get; set; }

    public string IssueDetails
    {
        get => _issueDetails;
        set
        {
            _issueDetails = value;
            OnPropertyChanged();
        }
    }

    public bool _issuePreviouslyReported { get; set; }

    public bool IssuePreviouslyReported
    {
        get => _issuePreviouslyReported;
        set
        {
            _issuePreviouslyReported = value;
            OnPropertyChanged();
        }
    }

    public Color _issuePreviouslyReportedColour { get; set; } = Color.LightGray;

    public Color IssuePreviouslyReportedColour
    {
        get => _issuePreviouslyReportedColour;
        set
        {
            _issuePreviouslyReportedColour = value;
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

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";

        IssueDetails = "";

        IssuePreviouslyReported = false;

        if (NavigationalParameters.CurrentCheckAnswer?.Comments != null)
        {
            IssueDetails = NavigationalParameters.CurrentCheckAnswer.Comments;
            IssuePreviouslyReported = NavigationalParameters.CurrentCheckAnswer.Comments.Contains("Already Reported");

            if (IssuePreviouslyReported)
            {
                Title = "This issue has been previously reported!";
                IssuePreviouslyReportedColour = Color.Green;
                NavigationalParameters.CurrentCheckAnswer.Comments = $"[Already Reported] {IssueDetails}";
            }
        }
        else
        {
            Title = "New issue to report!";
            IssuePreviouslyReportedColour = Color.LightGray;
            IssueDetails = "";
            IssuePreviouslyReported = false;
        }
    });


    public RelayCommand IssueAlreadyReported => new(async () =>
    {
        IssuePreviouslyReported = !IssuePreviouslyReported;
        // IssueDetails = NavigationalParameters.CurrentCheckAnswer.Comments;
        switch (IssuePreviouslyReported)
        {
            case true:
                Title = "This issue has been previously reported!";
                IssuePreviouslyReportedColour = Color.Green;
                NavigationalParameters.CurrentCheckAnswer.Comments = $"[Already Reported] {IssueDetails}";
                break;
            default:
                Title = "New issue to report!";
                IssuePreviouslyReportedColour = Color.LightGray;
                break;
        }
    });

    public RelayCommand Photo => new(async () =>
    {
        NavigationalParameters.CurrentCheckAnswer.Comments = IssueDetails;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT)
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PLANT;
        else
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.RISKASSESMENT;
        //NavigationalParameters.ReturnPage = Locator.DailyChecksIssueViewModelKey;
        //await NavigateTo(ViewModelLocator.ProjectImagesPage);
        _psvm.TakePhoto.Execute(null);
    });

    public RelayCommand Cancel => new(async () =>
    {
        if (NavigationalParameters.AllCurrentResponses != null)
            NavigationalParameters.AllCurrentResponses?.Remove(
                NavigationalParameters.AllCurrentResponses?.FirstOrDefault(x =>
                    x.QuestionId == NavigationalParameters.CurrentCheckAnswer?.QuestionId
                    && x.Qnumber == NavigationalParameters.CurrentCheckAnswer.Qnumber
                    && x.RelevantDate == NavigationalParameters.CurrentCheckAnswer.RelevantDate));

        check.DeleteSingleResponse(NavigationalParameters.CurrentCheckAnswer);

        if (NavigationalParameters.YesNoCollection != null)
            if (NavigationalParameters.YesNoCollection.Any() &&
                NavigationalParameters.CurrentCheckAnswer?.QuestionId > 0)
            {
                NavigationalParameters.YesNoCollection
                    .FirstOrDefault(x => x.QuestionId == NavigationalParameters.CurrentCheckAnswer?.QuestionId)
                    .BtnYesBgColour = Color.LightGray;
                NavigationalParameters.YesNoCollection
                    .FirstOrDefault(x => x.QuestionId == NavigationalParameters.CurrentCheckAnswer?.QuestionId)
                    .BtnNoBgColour = Color.LightGray;
                NavigationalParameters.YesNoCollection
                    .FirstOrDefault(x => x.QuestionId == NavigationalParameters.CurrentCheckAnswer?.QuestionId)
                    .BtnNaBgColour = Color.LightGray;

                //if (questionToDelete != null)
                //{
                //    NavigationalParameters.YesNoCollection?.Remove(questionToDelete);

                //    questionToDelete.BtnYesBgColour = Color.LightGray;
                //    questionToDelete
                //    questionToDelete.BtnNaBgColour = Color.LightGray;

                //    NavigationalParameters.YesNoCollection?.Add(questionToDelete);
                //}
            }

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.RISKASSESMENT)
            await NavigateTo(ViewModelLocator.DailySiteInspectionPage);
        else
            await NavigateTo(ViewModelLocator.PlantChecksPage);
    });

    public RelayCommand Submit => new(async () =>
    {
        // Ok its register new issue
        // MUST have Comments
        // Picture is not mandatory

        if (IssueDetails != "")
        {
            // Ok save the Comments to PassedData
            NavigationalParameters.CurrentCheckAnswer.Comments = IssueDetails;
        }
        else
        {
            // Need to flash up message that Comments are Manadatory
            await Alert("Please input some Comments!",
                "Error!");
            return;
        }

        // Now all Ok to Save this change to the DB
        //   var check = new Services.Checks();
        if (check.CreateDBifNotExists("Checks4TabletResponses"))
            //if (NavigationalParameters.AllCurrentResponses != null)
            //{
            //    NavigationalParameters.AllCurrentResponses?.Remove(NavigationalParameters.AllCurrentResponses?.FirstOrDefault(x => x.Id == NavigationalParameters.CurrentCheckAnswer.Id));
            //}
            //else
            //{
            //    NavigationalParameters.AllCurrentResponses = new List<Checks4TabletResponses>();
            //}

            //NavigationalParameters.AllCurrentResponses.Add(NavigationalParameters.CurrentCheckAnswer);

            check.AddSingleChecksResponse(NavigationalParameters.CurrentCheckAnswer);

        IssueDetails = "";

        IssuePreviouslyReported = false;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.RISKASSESMENT)
            await NavigateTo(ViewModelLocator.DailySiteInspectionPage);
        else
            await NavigateTo(ViewModelLocator.PlantChecksPage);
    });
}