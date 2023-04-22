namespace FocusXamarinMobileApplication.ViewModels;

public class AdditionalRisksPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    [Time]
    public AdditionalRisksPageViewModel()
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");
        _checkService = new Checks();
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

    public Checks _checkService { get; set; }


    public List<Checks4TabletResponses> _additionalResponses { get; set; }

    public List<Checks4TabletResponses> AdditionalResponses
    {
        get => _additionalResponses;
        set
        {
            _additionalResponses = value;
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

    public string _questionResponseText { get; set; }

    public string QuestionResponseText
    {
        get => _questionResponseText;
        set
        {
            _questionResponseText = value;
            OnPropertyChanged();
        }
    }

    public string _commentsText { get; set; }

    public string CommentsText
    {
        get => _commentsText;
        set
        {
            _commentsText = value;
            OnPropertyChanged();
        }
    }

    public Checks4TabletResponses _currentResponse { get; set; }

    public Checks4TabletResponses CurrentResponse
    {
        get => _currentResponse;
        set
        {
            _currentResponse = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded =>
        new(async () => { AdditionalResponses = _checkService.GetAdditionalResponses(); });

    public RelayCommand InadequateControlCommand => new(async () =>
    {
        await NavigateTo(ViewModelLocator.DailyChecksIssuePage);
    });

    public RelayCommand SaveCommand => new(async () =>
    {
        if (string.IsNullOrEmpty(QuestionResponseText))
            await Alert("Please enter a short description of the hazzard ", "Error", "Ok");
        else if (string.IsNullOrEmpty(CommentsText))
            await Alert("Please enter the dtails of the hazzard ", "Error", "Ok");
        else
            try
            {
                CurrentResponse = new Checks4TabletResponses
                {
                    ServerId = 0,
                    Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                    RelevantDate = NavigationalParameters.CurrentSelectedJob.JobDate,
                    GangLeaderName = NavigationalParameters.CurrentSelectedJob.GangLeaderName,
                    PlantId = 0,
                    QuestionResponse = QuestionResponseText,
                    DateTimeOfCheck = DateTime.Now,
                    QuestionId = 0,
                    PlantCheckedByName = NavigationalParameters.LoggedInUser.FullName,
                    PlantAssignedToName = "",
                    Comments = CommentsText,
                    PictureFileName = "",
                    PictureLatitude = "",
                    PictureLongitude = "",
                    SignatureFileName = "",
                    CheckSubmittedBy = NavigationalParameters.LoggedInUser.FullName,
                    ChecksStatus = "ChecksIncomplete",
                    ChecksType = "Dailies"
                };

                _checkService.AddSingleChecksResponse(CurrentResponse);
            }
            catch (Exception ex)
            {
                await Alert("The additional answer failed to save plese try again or contact support!", "Failure",
                    "Ok");
            }
            finally
            {
                AdditionalResponses = _checkService.GetAdditionalResponses();
                NavigationalParameters.CurrentCheckAnswer = CurrentResponse;
                CommentsText = "";
                QuestionResponseText = "";
            }
    });

    public RelayCommand BackCommand => new(async () => { NavigateBack(); });

    public RelayCommand DeleteRecordCommand => new(async () =>
    {
        _checkService.DeleteSingleResponse(NavigationalParameters.CurrentCheckAnswer);

        AdditionalResponses = _checkService.GetAdditionalResponses();
    });
}