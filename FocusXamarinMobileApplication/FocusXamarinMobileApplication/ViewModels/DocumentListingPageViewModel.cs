#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;
using Constants = FocusXamarinMobileApplication.Services.Constants;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class DocumentListingPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private string _currentPath = "";
    private Command _documentSelected;
    private Command _goBackTappedCommand;
    private string _pathToDisplay = "";
    private Command<string> _refreshTableAfterSelection;
    private Command<string> _returnFromUserConfirmation;
    private Command<string> _updateDocsCommand;

    public DocumentListingPageViewModel()
    {
        docs = new Documents();
        ls = new LocalStorage();
    }

    public Documents docs { get; }
    public LocalStorage ls { get; }
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

    public string _platform { get; set; }

    public string Platform
    {
        get => _platform;
        set
        {
            _platform = value;
            OnPropertyChanged();
        }
    }

    public int CountOfDocsInStorage { get; set; }
    public List<Docs4Tablet> allDocs { get; set; }
    public int CountOfAllDocs { get; set; }
    private long _confirmedUserId => NavigationalParameters.SelectedUser?.FocusId ?? 0;
    public string _whatDocumentsType2Display { get; private set; }
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

    public string _backButtonText { get; set; }

    public string BackButtonText
    {
        get => _backButtonText;
        set
        {
            _backButtonText = value;
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

    public bool _training { get; set; }

    public bool Training
    {
        get => _training;
        set
        {
            _training = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DocumentData2Display> _documentList { get; set; }

    public ObservableCollection<DocumentData2Display> DocumentList
    {
        get => _documentList;
        set
        {
            _documentList = value;
            OnPropertyChanged();
        }
    }

    public DocumentData2Display _selectedDocument { get; set; }

    public DocumentData2Display SelectedDocument
    {
        get => _selectedDocument;
        set
        {
            _selectedDocument = value;
            OnPropertyChanged();
        }
    }

    public string CurrentPath
    {
        get => _currentPath;
        set
        {
            _currentPath = value;
            PathToDisplay = value;
            OnPropertyChanged();
        }
    }

    public string _docStatusMessage { get; private set; }

    public string DocStatusMessage
    {
        get => _docStatusMessage;
        set
        {
            _docStatusMessage = value;
            OnPropertyChanged();
        }
    }

    public bool _goBackButtonStatus { get; set; }

    public bool GoBackButtonStatus
    {
        get => _goBackButtonStatus;
        set
        {
            _goBackButtonStatus = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _backArrow { get; set; } = SimpleStaticHelpers.GetImage("GoBack_icon");

    public ImageSource BackArrow
    {
        get => _backArrow;
        set
        {
            _backArrow = value;
            OnPropertyChanged();
        }
    }

    public string PathToDisplay
    {
        get => _pathToDisplay;
        set
        {
            var s = value.TrimStart('/');
            s = s.Replace("/", " > ");
            _pathToDisplay = s.TrimEnd(" > ".ToCharArray());
            OnPropertyChanged();
        }
    }

    public RelayCommand<string> _updatePlantDocsCommand { get; private set; }

    public RelayCommand Submit => new(async () => { });

    public RelayCommand CheckDocCount => new(async () =>
    {
        var ls = new LocalStorage();
        CountOfDocsInStorage = await ls.CountNumberOfDocumentsInStorage(allDocs);
    });

    public RelayCommand Cancel => new(async () =>
    {
        try
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS)
            {
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                    NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                    await NavigateTo(ViewModelLocator.MainListPage);
                else
                    await NavigateTo(ViewModelLocator.SupervisorProjectPage);
            }
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.OPERATIVEDOCS
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS)
            {
                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                    NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                    NavigateBack();
                else
                    await NavigateTo(ViewModelLocator.SupervisorProjectPage);
            }
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PROJECTDOCS)
            {
                await NavigateTo(ViewModelLocator.MenuSelectionPage);
            }
            else
            {
                await NavigateTo(ViewModelLocator.PlantDetailsPage);
            }
        }
        catch (Exception ex)
        {
        }
    });

    public RelayCommand RefreshTableAfterSelection => new(async () =>
    {
        ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
            NavigationalParameters.CurrentSelectedJob == null
                ? ""
                : NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(), _confirmedUserId);
    });

    public RelayCommand GoBackTappedCommand => new(async () =>
    {
        try
        {
            if (CurrentPath == "/" && GoBackButtonStatus == false) return;

            var splitItUp = CurrentPath.Split('/');

            if (splitItUp.Length - 1 == 1) //There is only 1 slash
            {
                CurrentPath = "/";
                GoBackButtonStatus = false;
                ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
                    NavigationalParameters.CurrentSelectedJob == null
                        ? ""
                        : NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                    _confirmedUserId);
            }
            else
            {
                CurrentPath = "";
                //There is more than 1 slash
                for (var i = 0; i < splitItUp.Length - 2; i++)
                    if (splitItUp[i] != "" && splitItUp[i].Length > 0)
                        CurrentPath += "/" + splitItUp[i];

                CurrentPath += "/";
                ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
                    NavigationalParameters.CurrentSelectedJob == null
                        ? ""
                        : NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                    _confirmedUserId);
            }
        }
        catch (Exception ex)
        {
        }
    });

    public RelayCommand<string> UpdatePlantDocsCommand => _updatePlantDocsCommand ??= new RelayCommand<string>(
        async e =>
        {
            try
            {
                await UpdateAllDocs();
            }
            catch (Exception ex)
            {
            }

            //allDocs = docs.GetAllJobDocuments()
            //    .Where(x => x.Section == "PlantDoc").ToList();

            //CountOfAllDocs = allDocs.Count;
            ////

            //// NavigationalParameters.UpdatingDocuments = UpdateDocsInd = true;

            //foreach (var document in allDocs)
            //{
            //    var result = await ls.CountNumberOfDocumentsInStorage(document);
            //    CountOfDocsInStorage = (CountOfDocsInStorage + result);

            //    if (CountOfAllDocs - CountOfDocsInStorage >= 0)
            //    {
            //        // DocStatusMessage = $"{CountOfAllDocs - CountOfDocsInStorage} out of {CountOfAllDocs} documents need updating";
            //    }
            //    else
            //    {
            //        //  NavigationalParameters.UpdatingDocuments = UpdateDocsInd = false;
            //        break;
            //    }
            //}

            //if (CountOfDocsInStorage == CountOfAllDocs)
            //{
            //    DocStatusMessage = "All Documents Up To Date.";
            //}
            //else
            //{
            //    // DocStatusMessage = $"{CountOfAllDocs - CountOfDocsInStorage} out of {CountOfAllDocs} documents need updating";
            //}

            ////   NavigationalParameters.UpdatingDocuments = UpdateDocsInd = false;
        });

    public RelayCommand DocumentSelected => new(async () =>
    {
        //   if (SelectedDocument != null && NavigationalParameters.UpdatingDocuments == false)

        if (SelectedDocument != null && DocStatusMessage != "Documents updating please wait!")
        {
            NavigationalParameters.SelectedDocument = SelectedDocument;

            if ((bool)NavigationalParameters.SelectedDocument
                    ?.IsItaFolder) //Its a folder so put it in the path, set the go back button active & redraw the table
            {
                if (CurrentPath != "/") GoBackButtonStatus = true;
                var newPath = SimpleStaticHelpers.BuildNewPathOrReturnFilename(CurrentPath,
                    NavigationalParameters.SelectedDocument?.FolderPath,
                    NavigationalParameters.SelectedDocument?.DocumentTitle,
                    NavigationalParameters.SelectedDocument?.FileName);
                CurrentPath = newPath.Substring(1);
                RefreshTableAfterSelection.Execute("");
            }
            else
            {
                try
                {
                    if (Platform == "IOS")
                        await NavigateTo(ViewModelLocator.DocumentViewPage);
                    else
                        await NavigateTo(ViewModelLocator.PdfViewPage);
                }
                catch (Exception ex)
                {
                    await Alert("Error",
                        "There has been an error displaying this content please try again however should this persist please contact support!");
                }
            }
        }
        else
        {
            await Alert("Documents outstanding",
                "There are some documents still to update! If this persists please contact support", "Ok");
        }
    });

    public RelayCommand PageLoaded => new(async () =>
    {
        try
        {
            if (NavigationalParameters.ReturnFromDocView == false)
            {
                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS)
                {
                    NavigationalParameters.SelectedUser = null;
                    CurrentPath = "/";

                    _whatDocumentsType2Display = "CompanyDocs";
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PROJECTDOCS)
                {
                    NavigationalParameters.SelectedUser = null;
                    CurrentPath = "/";

                    _whatDocumentsType2Display = "JobDocs";
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.OPERATIVEDOCS
                         || NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS)
                {
                    CurrentPath = "/";

                    _whatDocumentsType2Display = "OperativeDocs";
                }

                RefreshTableAfterSelection.Execute(null);
            }

            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
            Title = "Documents";
            ShowSection1 = true;
            ShowSection2 = true;
            ShowSection3 = true;
            ShowSection4 = true;
            ShowSection5 = true;
            BackButtonText = "Return to Menu";
            GoBackButtonStatus = true;
        }
        catch (Exception ex)
        {
        }
    });

    public RelayCommand UpdateDocsCommand => new(async () =>
    {
        try
        {
            var wa = new WebApi();
            ShowSection2 = false;
            ShowSection3 = false;
            ShowSection5 = false;
            var connectionAvailable = await wa.CanWeConnect2Api();

            if (connectionAvailable)
            {
                DocStatusMessage = "Checking for new Documents!";
                try
                {
                    var wassup = await wa.LogonRequest(NavigationalParameters.LoggedInUser.LoginAlias);
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
                        $"MainViewModel (Refresh) Data returned from Api BAD for: {NavigationalParameters.LoggedInUser.FullName}");
                }
                finally
                {
                    await UpdateAllDocs();
                }
            }
        }
        catch (Exception ex)
        {
        }
    });

    private void ClearTableView()
    {
        DocumentList = new ObservableCollection<DocumentData2Display>();
        CurrentPath = "/";
    }

    [Time]
    private void ShowCurrentDocumentsTable(string currentPath, string whichDocuments, string quoteNumber,
        long operativeId)
    {
        try
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.OPERATIVEDOCS
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS)
                DocumentList = new ObservableCollection<DocumentData2Display>(docs
                    .GetDocuments(currentPath, whichDocuments,
                        "",
                        operativeId)?.Where(x => x.FolderPath.Contains("Training"))
                    .ToList());
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS)
                DocumentList = new ObservableCollection<DocumentData2Display>(docs
                    .GetDocuments(currentPath, whichDocuments,
                        "" +
                        "",
                        operativeId)
                    .ToList());
            else
                DocumentList =
                    new ObservableCollection<DocumentData2Display>(docs.GetDocuments(currentPath, whichDocuments,
                        quoteNumber, operativeId));

            GoBackButtonStatus = CurrentPath == "/" ? false : true;
        }
        catch (Exception ex)
        {
        }
    }

    [Time]
    private string SortJobPackPath(string path, string qNumber)
    {
        var returnValue = new StringBuilder();

        var splitItUpArray = path.Split('\\');
        var counter = 0;
        foreach (var item in splitItUpArray)
            if (item != null && item.Length > 1)
            {
                if (counter == 0)
                    returnValue.Append($"[{qNumber}]{item}/");
                else
                    returnValue.Append($"{item}/");

                counter++;
            }

        return returnValue.ToString();
    }

    [Time]
    private string SortPlantDocPath(string path, string plantId)
    {
        var returnValue = new StringBuilder();

        var splitItUpArray = path.Split('\\');
        foreach (var item in splitItUpArray)
            if (item != null && item.Length > 1)
                returnValue.Append($"[{plantId}]{item}/");

        return returnValue.ToString();
    }

    private void ShowOperativeDocsAfterAuth(long operativeId)
    {
        ShowCurrentDocumentsTable(CurrentPath, "OperativeDocs", "", operativeId);
    }

    [Time]
    private async Task UpdateAllDocs()
    {
        try
        {
            DocStatusMessage = "Documents updating please wait!";

            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
            Title = "Documents";
            ShowSection1 = true;
           // ShowSection2 = true;
           // ShowSection3 = true;
            ShowSection4 = true;
           // ShowSection5 = true;
            GoBackButtonStatus = true;
            var CountOfDocsDownloaded = 0;

            if (NavigationalParameters.ReturnFromDocView == false)
            {
                PathToDisplay = "";

                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS)
                {
                    NavigationalParameters.SelectedUser = null;
                    BackButtonText = "Return to Main";
                    _whatDocumentsType2Display = "CompanyDocs";
                    CurrentPath = "/";
                    ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display, "", 0);
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PROJECTDOCS)
                {
                    BackButtonText = "Return to Menu";
                    NavigationalParameters.SelectedUser = null;
                    _whatDocumentsType2Display = "JobDocs";
                    CurrentPath = "/";
                    ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
                        NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(), 0);
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.OPERATIVEDOCS
                         || NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS)
                {
                    BackButtonText = "Back";


                    _whatDocumentsType2Display = "OperativeDocs";
                    CurrentPath = "/";
                    ClearTableView();
                }

                CountOfAllDocs = NavigationalParameters.DataPassedToTablet.DocumentsData.Count;
                CountOfDocsInStorage = 0;
            }

            var cv2 = new Constants();
            var SPORootSiteURL = cv2.SPORootSiteUrl;
            var ClientSiteURL = cv2.ClientSideUrl;

            var uri = new Uri(SPORootSiteURL + ClientSiteURL);
            var ta = new TestTokenAuth();
            var clientContext = ta.GetContext(uri);

            foreach (var document in NavigationalParameters.DataPassedToTablet.DocumentsData)
            {
                //var result = await ls.CountNumberOfDocumentsInStorage(document);

                if (clientContext == null || (CountOfDocsDownloaded > 0 && CountOfDocsDownloaded % 100 == 0))
                    clientContext = ta.GetContext(uri);

                var result = await ls.CheckIfDocExistsLocallyAndIfNotThenDownload(document, clientContext);
                if (result == 5)
                {
                    CountOfDocsDownloaded += 1;
                    result -= 4;
                }

                CountOfDocsInStorage += result;
            }

            if (_whatDocumentsType2Display == "OperativeDocs")
            {
                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                    NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                    NavigationalParameters.SelectedUser = NavigationalParameters.LoggedInUser;
                else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR
                         && NavigationalParameters.ProjectListMode ==
                         NavigationalParameters.ProjectListModes.PROJECTLIST)
                    NavigationalParameters.SelectedUser = NavigationalParameters.LoggedInUser;

                if (NavigationalParameters.SelectedUser?.FocusId != 0)
                    ShowOperativeDocsAfterAuth(NavigationalParameters.SelectedUser.FocusId);
                else
                    ClearTableView();
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"DocumentListingviewmodel failed for user: {NavigationalParameters.LoggedInUser.FullName}");
        }
        finally
        {
                    DocStatusMessage = "Update Complete";

                ShowSection2 = true;
                ShowSection3 = true;
                ShowSection5 = true;            
        }
    }
}