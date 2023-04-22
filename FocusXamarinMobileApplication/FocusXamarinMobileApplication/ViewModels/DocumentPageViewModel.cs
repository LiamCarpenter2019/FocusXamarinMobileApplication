namespace FocusXamarinMobileApplication.ViewModels;

public class DocumentPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public DocumentPageViewModel()
    {
        DocumentUrl =
            "https://maps.hull.gov.uk/myhull.aspx";
        //DocumentUrl = $"{NavigationalParameters.CurrentAssignment.KMZ}";
    }

    public string DocumentUrl { get; set; }

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

    public bool _showDesignButton { get; private set; }

    public bool ShowDesignButton
    {
        get => _showDesignButton;
        set
        {
            _showDesignButton = value;
            OnPropertyChanged();
        }
    }

    public bool _showSurveyQuestionsButton { get; private set; }

    public bool ShowSurveyQuestionsButton
    {
        get => _showSurveyQuestionsButton;
        set
        {
            _showSurveyQuestionsButton = value;
            OnPropertyChanged();
        }
    }

    public bool _showMapButton { get; private set; }

    public bool ShowMapButton
    {
        get => _showMapButton;
        set
        {
            _showMapButton = value;
            OnPropertyChanged();
        }
    }


    public RelayCommand ScreenLoaded => new(async () =>
    {
        try
        {
            ProjectDate =
                NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            ShowMapButton = true;
            ShowDesignButton = true;
            ShowSurveyQuestionsButton = true;


            DocumentUrl =
                "https://maps.hull.gov.uk/myhull.aspx";
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });


    public RelayCommand GoToDesign => new(async () =>
    {
        try
        {
            var docs = new Documents();
            NavigationalParameters.MapType = "Drawing";
            NavigationalParameters.SelectedDocument = docs
                .GetDocuments("/Drawings/", "JobDocs",
                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(), 0)?.FirstOrDefault(x =>
                    x.DocumentTitle
                        .Contains($"{NavigationalParameters.CurrentSelectedJob?.QuoteNumber}") && x.DocumentTitle
                        .ToUpper()
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

    public RelayCommand GoToSurveyQuestions => new(async () =>
    {
        try
        {
            switch (NavigationalParameters.SelectedAsset)
            {
                case null when NavigationalParameters.SelectedEndPoint == null:
                    if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite") || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt"))
                    await NavigateTo(ViewModelLocator.SelectEndPointPage);
                    else
                    await NavigateTo(ViewModelLocator.SelectStreetPage);

                    await Alert("No survey selected!",
                        "Please select a survey and continue! should you need furthur assistance please contact support!!");

                    break;
                default:
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    break;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToMap => new(async () =>
    {
        try
        {
            await NavigateTo(ViewModelLocator.FormsMapPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });


    public RelayCommand Refresh => new(async () =>
    {
        ProjectDate =
            NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        await NavigateTo(ViewModelLocator.DesignMapPage);
    });
}