using System;
using System.ComponentModel;
using System.IO;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;

namespace FocusXamarinMobileApplication.ViewModels;

public class DocumentViewPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly LocalStorage ls;
    public Jobs _jobService;


    public DocumentViewPageViewModel()
    {
        ls = new LocalStorage();

        _jobService = new Jobs();
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

    public bool _showBackButton { get; set; }

    public bool ShowBackButton
    {
        get => _showBackButton;
        set
        {
            _showBackButton = value;
            OnPropertyChanged();
        }
    }

    public bool _isNotToolboxTalk { get; set; } = true;

    public bool IsNotToolboxTalk
    {
        get => _isNotToolboxTalk;
        set
        {
            _isNotToolboxTalk = value;
            OnPropertyChanged();
        }
    }

    public bool _toolboxTalk { get; set; }

    public bool ToolboxTalk
    {
        get => _toolboxTalk;
        set
        {
            _toolboxTalk = value;
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

    public string DocumentToView { get; private set; }

    public string DocumentUrl { get; private set; }

    private Stream _pdfDocumentStream { get; set; }

    /// <summary>
    ///     The PDF document stream that is loaded into the instance of the PDF viewer.
    /// </summary>
    public Stream PdfDocumentStream
    {
        get => _pdfDocumentStream;
        set
        {
            _pdfDocumentStream = value;
            OnPropertyChanged();
        }
    }

    public bool _showMappingButton { get; private set; }

    public bool ShowMappingButton
    {
        get => _showMappingButton;
        set
        {
            _showMappingButton = value;
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

    private string _pdfDocumentStreamUri { get; set; }

    public string PdfDocumentStreamUri
    {
        get => _pdfDocumentStreamUri;
        set
        {
            _pdfDocumentStreamUri = value;

            OnPropertyChanged();
        }
    }


    public RelayCommand ScreenLoaded => new(async () =>
    {
        try
        {
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

            ShowSection1 = true;

            ShowSection2 = true;

            if (NavigationalParameters.SelectedDocument.FolderPath.ToLower().Contains("rams")
                || NavigationalParameters.SelectedDocument.FolderPath.ToLower().Contains("toolbox"))
            {
                IsNotToolboxTalk = false;

                ToolboxTalk = true;

                ShowBackButton = false;
            }
            else
            {
                ShowBackButton = true;

                IsNotToolboxTalk = true;

                ToolboxTalk = false;
            }

            if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite"))
            {
                ShowBackButton = false;
                ShowMappingButton = true;
                ShowSurveyQuestionsButton = true;
                ShowMapButton = true;
            }
            else
            {
                ShowBackButton = true;
                ShowMappingButton = false;
                ShowMapButton = false;
                ShowSurveyQuestionsButton = false;
            }

            PdfDocumentStream = null;

            try
            {
                var xx = await ls.GetImageStreamFromLocalstorageAsync(
                    $"{NavigationalParameters.SelectedDocument?.TabletDocumentFolder}/{NavigationalParameters.SelectedDocument?.FileName}");

                if (xx != null)
                {
                    PdfDocumentStream = xx;
                }
                else
                {
                    var yy = await ls.GetImageStreamFromLocalstorageAsync(
                        $"{NavigationalParameters.SelectedDocument?.TabletDocumentFolder}/{NavigationalParameters.SelectedDocument?.FileName}");

                    if (yy != null)
                    {
                        PdfDocumentStream = yy;
                    }
                    else
                    {
                        Analytics.TrackEvent(
                            $"Data passed to the {ToString()} has thrown this error for: {NavigationalParameters.LoggedInUser?.FullName}");

                        await Alert("Error",
                            "The document is missing please update the docs and try again! if the docc is still missing please contact support.");
                    }
                }
            }
            catch (Exception ex)
            {
                Analytics.TrackEvent(
                    $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

                await Alert("Error",
                    "The document is missing please update the docs and try again! if the docc is still missing please contact support.");

                var error = ex.ToString();
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    });

    public RelayCommand Cancel => new(async () =>
    {
        NavigationalParameters.ReturnFromDocView = true;
        NavigationalParameters.SelectedDocument = null;
        // PdfDocumentStreamUri = null;
        PdfDocumentStream = null;
        NavigateBack();
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

    public RelayCommand GoToDesign => new(async () =>
    {
        try
        {
            NavigationalParameters.ReturnFromDocView = true;
            NavigationalParameters.SelectedDocument = null;
            PdfDocumentStream = null;
            NavigationalParameters.MapType = "Drawing";
            var docs = new Documents();

            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    NavigationalParameters.SelectedEndPoint?.Qnumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle
                    .Contains($"{NavigationalParameters.SelectedEndPoint?.Qnumber}") && x.DocumentTitle.ToUpper()
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

    public RelayCommand GoToSurvey => new(async () =>
    {
        try
        {
            if (NavigationalParameters.SelectedAsset == null
                && NavigationalParameters.SelectedEndPoint == null)
            {
                if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite"))
                    await NavigateTo(ViewModelLocator.SelectEndPointPage);
                else
                    await NavigateTo(ViewModelLocator.SelectStreetPage);

                await Alert("No survey selected!",
                    "Please select a survey and continue! should you need furthur assistance please contact support!!");
            }
            else
            {
                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
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

    public RelayCommand Submit => new(async () =>
    {
        try
        {
            var ok = await Alert("Warning!! Please ensure all members sign to confirm they acknowledge and understand",
                "You must sign to confirm that you understand", "Confirm",
                "Cancel");

            if (ok)
            {
                NavigationalParameters.ToolBoxTalk = _jobService.GetToolboxTalk(
                    $"{NavigationalParameters.SelectedDocument.DocumentId}_{NavigationalParameters.SelectedDocument.DocumentTitle}");

                if (NavigationalParameters.ToolBoxTalk == null)
                    NavigationalParameters.ToolBoxTalk = new ToolBoxTalks
                    {
                        Code = Guid.NewGuid().ToString(),
                        CreatedBy = "Created on ipad",
                        DateLastDistribution = DateTime.Now,
                        FullName =
                            $"{NavigationalParameters.SelectedDocument.DocumentId}_{NavigationalParameters.SelectedDocument.DocumentTitle}",
                        RemoteTableId = 0,
                        FileName = NavigationalParameters.SelectedDocument.FileName,
                        TrainingProvider = "SCD",
                        DateOfUpload = NavigationalParameters.SelectedDocument.UploadedDate,
                        Version = NavigationalParameters.SelectedDocument.DocumentId
                    };

                NavigationalParameters.AuthDetail.Type = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                await NavigateTo(ViewModelLocator.SignaturePage);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });
}