using System;
using System.ComponentModel;
using System.IO;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class ToolboxTalkDocumentPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private readonly LocalStorage ls;
    private SupervisorApprovalsPageViewModel _approvals;
    private Assignments _assignmentService;
    private Users _userService;

    public ToolboxTalkDocumentPageViewModel()
    {
        _jobService = new Jobs();

        _userService = new Users();

        _assignmentService = new Assignments();

        ls = new LocalStorage();

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = $"{DateTime.Now.Date:dd/MM/yyyy}";

        ShowSection1 = true;

        ShowSection2 = true;

        NavigationalParameters.CurrentUserToolboxTalks = NavigationalParameters.CurrentUserToolboxTalks
            .Where(x => x.GangLeaderId == NavigationalParameters.SelectedTaskItem.GangLeaderId
                        && x.ToolBoxTalkCode == NavigationalParameters.SelectedTaskItem.Description
                        && string.IsNullOrEmpty(x.SignatureFileName))
            .ToList();
    }


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

    public string DocumentToView { get; private set; }

    public string DocumentUrl { get; private set; }

    private Stream _pdfDocumentStream { get; set; }

    public Stream PdfDocumentStream
    {
        get => _pdfDocumentStream;
        set
        {
            _pdfDocumentStream = value;
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

    public long _minutesTaken { get; set; }

    public long MinutesTaken
    {
        get => _minutesTaken;
        set
        {
            _minutesTaken = value;
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

    public bool _showButtons { get; internal set; }

    public bool ShowButtons
    {
        get => _showButtons;
        set
        {
            _showButtons = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand Cancel => new(async () =>
    {
        //if (NavigationalParameters.ProjectListMode != NavigationalParameters.ProjectListModes.GANGTOOLBOXTALKS)
        //{
        //    NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.TASKLIST;
        //}

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.TASKLIST;

        NavigateBack();
        //await NavigateTo(ViewModelLocator.TaskListPage);
    });

    public RelayCommand Submit => new(async () =>
    {
        foreach (var item in NavigationalParameters.CurrentUserToolboxTalks)
        {
            item.TimeTaken = MinutesTaken;
            _jobService.AddUserToolBoxTalks(item);
        }

        NavigationalParameters.AuthDetail.Type = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;
        NavigationalParameters.AuthDetail.SignatureFolderName = "JobPictures";
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.TOOLBOXTALKS;
        await NavigateTo(ViewModelLocator.SignaturePage);
    });

    public RelayCommand UploadAllToolboxTalksCommand => new(async () =>
    {
        await _jobService.UploadToolBoxTalks(NavigationalParameters.CurrentUserToolboxTalks);

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.TASKLIST;
        NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.TASKLIST;
        await NavigateTo(ViewModelLocator.TaskListPage);
    });

    public RelayCommand GetDocument => new(async () =>
    {
        try
        {
            var toolboxtalk = _jobService.GetToolboxTalk(NavigationalParameters.SelectedTaskItem.Description);
            // PdfDocumentStream = await ls.GetImageStreamFromLocalstorageAsync($"CompanyDocs/{NavigationalParameters.SelectedDocument.FileName}");
            //"WasteManagement[99].pdf"
            PdfDocumentStream = await ls.GetImageStreamFromLocalstorageAsync("CompanyDocs", toolboxtalk.FileName);
        }
        catch (Exception ex)
        {
            await Alert("Error",
                "The document is missing please update the docs and try again! if the docc is still missing please contact support.");
        }
    });
}