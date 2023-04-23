using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services.Interfaces;
using FocusXamarinMobileApplication.ViewModels;
using FocusXamarinMobileApplication.Views;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace FocusXamarinMobileApplication.Services
{
   public class DocumentsListingViewModel : FBaseViewModel
    {
        public DocumentsListingViewModel()
        {
            _currentSelectedJob = new JobData4Tablet();
            CurrentSelectedJobDate = "";
            CurrentSelectedJobCnumber = "";
            CurrentSelectedJobName = "";
            _whatDocumentsType2Display = "";

            UpdateDocsInd = false;
            //MessengerInstance.Register<MessageConfirmation>(this, DataReturnedFromUserConfirmation);
        }

        public async void GoBack()
        {
            if (NavigationalParameters.PreviousPageKey == "MenuPageViewModelKey")
            {
                NavigationalParameters.NavigationParameter = null;
              //  //NavigationalParameters.ReturnPage = Locator.DocumentListingPageViewModel;
             //   NavigationalParameters.PassedInfo = new MenuPage();
                await NavigateTo(new MenuSelectionPage());
                NavigationalParameters.PreviousPageKey = "";
            }
            else
            {
                NavigationalParameters.NavigationParameter = null;
               await NavigateBack();
            }
        }

        /*public void DataReturnedFromUserConfirmation(MessageConfirmation opId)
        {
            _confirmedUserId = opId.OperativeId;
        }*/

        private void ShowCurrentDocumentsTable(string currentPath, string whichDocuments, string quoteNumber,
            long operativeId)
        {
            var docs = new Services.Documents();

            if (Training)
                DocumentList = docs
                    .GetDocuments(currentPath, whichDocuments,
                        NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                        operativeId)?.Where(x => x.FolderPath.Contains("Training"))
                    .ToList();
            else
                DocumentList = docs.GetDocuments(currentPath, whichDocuments, quoteNumber, operativeId);

            GoBackButtonStatus = CurrentPath == "/" ? "DontShow" : "ShowGoBack";
            OnPropertyChanged("CurrentPath");
            OnPropertyChanged("GoBackButtonStatus");
            OnPropertyChanged("DocumentList");
            OnPropertyChanged("PathToDisplay");
        }

        private void ClearTableView()
        {
            DocumentList = new List<DocumentData2Display>();
            CurrentPath = "/";
            GoBackButtonStatus = "DontShow";

            OnPropertyChanged("CurrentPath");
            OnPropertyChanged("GoBackButtonStatus");
            OnPropertyChanged("DocumentList");
            OnPropertyChanged("PathToDisplay");
        }

        #region Public Properties

        public List<DocumentData2Display> DocumentList { get; private set; }
        private string _currentPath = "";

        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                _currentPath = value;
                PathToDisplay = value;
            }
        }

        public string GoBackButtonStatus { get; set; }
        public int CountOfDocsInStorage { get; set; }
        public int CountOfAllDocs { get; set; }
        public string DocStatusMessage { get; set; }
        public bool UpdateDocsInd { get; set; }
        public string CurrentSelectedJobDate { get; set; }
        public string CurrentSelectedJobCnumber { get; set; }
        public string CurrentSelectedJobName { get; set; }
        private string _pathToDisplay = "";

        public string PathToDisplay
        {
            get => _pathToDisplay;
            set
            {
                var s = value.TrimStart('/');
                s = s.Replace("/", " > ");
                _pathToDisplay = s.TrimEnd(" > ".ToCharArray());
            }
        }

        #endregion

        #region Private Properties

        private RelayCommand _goBackTappedCommand;
        private RelayCommand<string> _updateDocsCommand;

        private RelayCommand<string> _returnFromUserConfirmation;

        //private RelayCommand<Tuple<string, JobData4Tablet>> _screenLoadedCommand4SurveyDocsListing;
        private RelayCommand<Tuple<string, JobData4Tablet>> _screenLoadedCommand4DocsListing;
        private RelayCommand<string> _refreshTableAfterSelection;
        private string _whatDocumentsType2Display;
        private RelayCommand<string> _updatePlantDocsCommand;
        public bool Training { get; set; }

        private JobData4Tablet _currentSelectedJob;
        //private RelayCommand<string> _updatePlantDocsCommand;

        private long _confirmedUserId => NavigationalParameters.SelectedUser?.FocusId ?? 0L;

      
        
        #endregion

        #region Relay Commands

        public RelayCommand<string> UpdatePlantDocsCommand => _updatePlantDocsCommand ??= new RelayCommand<string>(
            async e =>
            {
                IDocuments docs = new Services.Documents();
                var ls = new LocalStorage();
                var allDocs = docs.GetAllJobDocuments()
                    .Where(x => x.Section == "PlantDoc").ToList();
                CountOfAllDocs = allDocs.Count;
                //

                CountOfDocsInStorage =
                    await ls.CountNumberOfDocumentsInStorage(
                        allDocs); // NOW Check If all docs are loaded

                if (CountOfDocsInStorage == CountOfAllDocs)
                {
                    DocStatusMessage =
                        "All Documents Up To Date.";
                    base.OnPropertyChanged("DocStatusMessage");

                }
                else
                {
                    DocStatusMessage =
                        $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                    base.OnPropertyChanged("DocStatusMessage");

                    var z = 1;

                    UpdateDocsInd = true;
                    base.OnPropertyChanged("UpdateDocsInd");

                    // allDocs = docs.GetAllJobDocuments();

                    foreach (var item in allDocs)
                    {
                        //Debug.WriteLine($"PlantID: {item.PlantID} Path: {item.FolderPath} DocTitle: {item.DocumentTitle} ");
                        var localFolderName = "";
                        var remoteFolderName = "";

                        localFolderName = "PlantDocs";
                        if (await ls.DoesDocExistOnTablet(
                            localFolderName, item.FileName))
                        {
                            remoteFolderName =
                                $"PlantFiles/{SortPlantDocPath(item.FolderPath, item.PlantId)}";
                            var updateDocResult =
                                await ls.UpdateFileFromAzure(
                                    item.FileName,
                                    localFolderName,
                                    remoteFolderName);

                            if (updateDocResult == "Good")
                            {
                                CountOfDocsInStorage++;
                                DocStatusMessage =
                                    $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                                base.OnPropertyChanged("DocStatusMessage");

                            }
                        }
                    }

                    DocStatusMessage =
                        "Please Wait. Updates complete - Checking.";
                    base.OnPropertyChanged("DocStatusMessage");


                    CountOfDocsInStorage =
                        await ls.CountNumberOfDocumentsInStorage(
                            allDocs);
                    CountOfAllDocs = allDocs.Count;
                    DocStatusMessage =
                        CountOfDocsInStorage == CountOfAllDocs
                            ? "All Documents Up To Date."
                            : $"{CountOfAllDocs - CountOfDocsInStorage} out of {CountOfAllDocs} documents need updating";
                    base.OnPropertyChanged("DocStatusMessage");

                    UpdateDocsInd = false;
                    base.OnPropertyChanged("UpdateDocsInd");
                }
            });

        public RelayCommand<string> ReturnFromUserConfirmation
        {
            get
            {
                return _returnFromUserConfirmation ??= new RelayCommand<string>(e =>
                {
                    var n = _confirmedUserId;
                    if (n >= 0 || Training)
                        if (Training && (n == NavigationalParameters.LoggedInUser.FocusId || n == 0))
                            n = NavigationalParameters.LoggedInUser.FocusId;

                    ShowOperativeDocsAfterAuth(n);
                    //else
                    //_whatDocumentsType2Display = "";
                });
            }
        }

        //public RelayCommand<StandardLibrary.Models.Assignment> ScreenLoadedCommand4SurveyDocsListing
        //{
        //    get
        //    {
        //        return _screenLoadedCommand4SurveyDocsListing ??
        //               (_screenLoadedCommand4SurveyDocsListing = new RelayCommand<StandardLibrary.Models.Assignment>(
        //                   async e =>
        //                   {
        //                       if (e != null)
        //                       {
        //                           CurrentSelectedJobDate = "";
        //                           CurrentSelectedJobCnumber = "";
        //                           CurrentSelectedJobName = "";

        //                           _whatDocumentsType2Display = "";
        //                           CurrentPath = "/";

        //                           var job = new Jobs();
        //                           var allJobs = await job.GetAllJobs();
        //                           //DEbug Only
        //                           foreach (var item in allJobs) //
        //                               item.QuoteNumber = 406850;
        //                           _currentSelectedJob = allJobs.FirstOrDefault(x =>
        //                               x.QuoteNumber.ToString() == e.Qnumber.ToString());
        //                           if (_currentSelectedJob != null)
        //                           {
        //                               _currentSelectedJob.IsSelected = true;

        //                               CurrentSelectedJobDate = _currentSelectedJob.JobDate.ToString("dd/MM/yyyy");
        //                               CurrentSelectedJobCnumber = _currentSelectedJob.ContractNumber.ToString();
        //                               CurrentSelectedJobName = _currentSelectedJob.ProjectName;
        //                           }

        //                           //
        //                           ClearTableView("Please make a selection using the Buttons above!");
        //                           //
        //                           IDocuments docs = new Services.Documents();
        //                           var allDocs = docs.GetAllJobDocuments();
        //                           CountOfAllDocs = allDocs.Count;
        //                           //
        //                           var ls = new LocalStorage();
        //                           CountOfDocsInStorage =
        //                               await ls.CountNumberOfDocumentsInStorage(
        //                                   allDocs); // NOW Check If all docs are loaded
        //                           if (CountOfDocsInStorage == CountOfAllDocs)
        //                               DocStatusMessage = $"All Documents Up To Date.";
        //                           else
        //                               DocStatusMessage =
        //                                   $"{CountOfAllDocs - CountOfDocsInStorage} out of {CountOfAllDocs} documents need updating";
        //                           OnPropertyChanged("DocStatusMessage");
        //                       }
        //                       else
        //                       {
        //                           if (_whatDocumentsType2Display == "OperativeDocs")
        //                           {
        //                               if (_confirmedUserId != 0)
        //                                   ShowOperativeDocsAfterAuth(_confirmedUserId);
        //                               else
        //                                   ClearTableView("Please make a selection using the Buttons above!");
        //                           }
        //                       }

        //                       OnPropertyChanged(() => CurrentSelectedJobDate);
        //                       OnPropertyChanged(() => CurrentSelectedJobCnumber);
        //                       OnPropertyChanged(() => CurrentSelectedJobName);
        //                   }));
        //    }
        //}

        public RelayCommand<Tuple<string, JobData4Tablet>> ScreenLoadedCommand4DocsListing
        {
            get
            {
                return _screenLoadedCommand4DocsListing ??= new RelayCommand<Tuple<string, JobData4Tablet>>(async e =>
                {
                    //_confirmedUserId = 0;

                    Training = NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS;

                    if (NavigationalParameters.CurrentSelectedJob != null)
                        _currentSelectedJob = NavigationalParameters.CurrentSelectedJob;

                    if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS)
                    {
                        NavigationalParameters.SelectedUser = null;
                        _whatDocumentsType2Display = "CompanyDocs";
                        CurrentPath = "/";
                        ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display, "", 0);
                    }
                    else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PROJECTDOCS)
                    {
                        NavigationalParameters.SelectedUser = null;
                        CurrentSelectedJobDate = _currentSelectedJob.JobDate.ToString("dd/MM/yyyy");
                        CurrentSelectedJobCnumber = _currentSelectedJob.ContractNumber.ToString();
                        CurrentSelectedJobName = _currentSelectedJob.ProjectName;

                        _whatDocumentsType2Display = "JobDocs";
                        CurrentPath = "/";
                        ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
                            _currentSelectedJob.QuoteNumber.ToString(), 0);
                    }
                    else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.OPERATIVEDOCS)
                    {
                        // OperativeDocs
                        CurrentSelectedJobDate = _currentSelectedJob.JobDate.ToString("dd/MM/yyyy");
                        CurrentSelectedJobCnumber = _currentSelectedJob.ContractNumber.ToString();
                        CurrentSelectedJobName = _currentSelectedJob.ProjectName;
                        //
                        _whatDocumentsType2Display = "OperativeDocs";
                        CurrentPath = "/";
                        //
                        ClearTableView();

                        //var nav = ServiceLocator.Current.GetInstance<INavigationService>();
                        //NavigationalParameters.NavigationParameter = null;
                        //nav.NavigateTo(Locator.SignaturePageViewModelKey, null);
                    }

                    IDocuments docs = new Services.Documents();
                    var allDocs = docs.GetAllJobDocuments();
                    CountOfAllDocs = allDocs.Count;
                    //
                    var ls = new LocalStorage();
                    CountOfDocsInStorage =
                        await ls.CountNumberOfDocumentsInStorage(allDocs); // NOW Check If all docs are loaded
                    if (CountOfDocsInStorage == CountOfAllDocs)
                        DocStatusMessage = "All Documents Up To Date.";
                    else
                        DocStatusMessage =
                            $"{CountOfAllDocs - CountOfDocsInStorage} out of {CountOfAllDocs} documents need updating";

                    OnPropertyChanged("DocStatusMessage");

                    if (_whatDocumentsType2Display == "OperativeDocs")
                    {
                        if (Training) NavigationalParameters.SelectedUser = NavigationalParameters.LoggedInUser;

                        if (NavigationalParameters.SelectedUser?.FocusId != 0)
                            ShowOperativeDocsAfterAuth(NavigationalParameters.SelectedUser.FocusId);
                        else
                            ClearTableView();
                    }

                    OnPropertyChanged("CurrentSelectedJobDate");
                    OnPropertyChanged("CurrentSelectedJobCnumber");
                    OnPropertyChanged("CurrentSelectedJobName");
                });
            }
        }

        private void ShowOperativeDocsAfterAuth(long operativeId)
        {
            ShowCurrentDocumentsTable(CurrentPath, "OperativeDocs", "", operativeId);
        }

        public RelayCommand GoBackTappedCommand
        {
            get
            {
                return _goBackTappedCommand ??= new RelayCommand(() =>
                {
                    if (CurrentPath == "/" && GoBackButtonStatus != "ShowGoBack") return;

                    var splitItUp = CurrentPath.Split('/');

                    if (splitItUp.Length - 1 == 1) //There is only 1 slash
                    {
                        CurrentPath = "/";
                        GoBackButtonStatus = "DontShow";
                        ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
                            _currentSelectedJob == null ? "" : _currentSelectedJob.QuoteNumber.ToString(),
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
                            _currentSelectedJob == null ? "" : _currentSelectedJob.QuoteNumber.ToString(),
                            _confirmedUserId);
                    }
                });
            }
        }


        public RelayCommand<string> UpdateDocsCommand => _updateDocsCommand ??= new RelayCommand<string>(async e =>
        {
            IDocuments docs = new Services.Documents();
            var ls = new LocalStorage();
            var allDocs =  NavigationalParameters.DataPassedToTablet.DocumentsData;
            CountOfAllDocs = allDocs.Count;
            //

            CountOfDocsInStorage =
                await ls.CountNumberOfDocumentsInStorage(
                    allDocs); // NOW Check If all docs are loaded

            if (CountOfDocsInStorage == CountOfAllDocs)
            {
                DocStatusMessage = "All Documents Up To Date.";
                OnPropertyChanged("DocStatusMessage");
            }
            else
            {
                DocStatusMessage =
                    $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                base.OnPropertyChanged("DocStatusMessage");

                var z = 1;

                UpdateDocsInd = true;
                base.OnPropertyChanged("UpdateDocsInd");

                // allDocs = docs.GetAllJobDocuments();

                foreach (var item in allDocs)
                {
                    //Debug.WriteLine($"PlantID: {item.PlantID} Path: {item.FolderPath} DocTitle: {item.DocumentTitle} ");
                    var localFolderName = "";
                    var remoteFolderName = "";
                    switch (item.DocType)
                    {
                        case "CompanyDocs":
                            localFolderName = "CompanyDocs";
                            if (await ls.DoesDocExistOnTablet(
                                localFolderName, item.FileName))
                            {
                                remoteFolderName =
                                    $"CompanyDocs{item.FolderPath.Replace("\\", "/")}/";
                                var updateDocResult =
                                    await ls.UpdateFileFromAzure(
                                        item.FileName, localFolderName,
                                        remoteFolderName);

                                if (updateDocResult == "Good")
                                {
                                    CountOfDocsInStorage++;
                                    DocStatusMessage =
                                        $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                                    base.OnPropertyChanged("DocStatusMessage");
                                }
                            }

                            break;
                        case "OperativeDocs":
                            localFolderName = "OperativeDocs";
                            if (await ls.DoesDocExistOnTablet(
                                localFolderName, item.FileName))
                            {
                                remoteFolderName = "OperativesData/";
                                var updateDocResult =
                                    await ls.UpdateFileFromAzure(
                                        item.FileName, localFolderName,
                                        remoteFolderName);

                                if (updateDocResult == "Good")
                                {
                                    CountOfDocsInStorage++;
                                    DocStatusMessage =
                                        $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                                    base.OnPropertyChanged("DocStatusMessage");
                                }
                            }

                            break;
                        case "JobDocs":
                            localFolderName = "JobPackFiles";
                            if (await ls.DoesDocExistOnTablet(
                                localFolderName, item.FileName))
                            {
                                remoteFolderName =
                                    $"JobPackFiles/{SortJobPackPath(item.FolderPath, item.QNumber)}";
                                var updateDocResult =
                                    await ls.UpdateFileFromAzure(
                                        item.FileName, localFolderName,
                                        remoteFolderName);

                                if (updateDocResult == "Good")
                                {
                                    CountOfDocsInStorage++;
                                    DocStatusMessage =
                                        $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                                    base.OnPropertyChanged("DocStatusMessage");
                                }
                            }

                            break;
                        case "PlantDocs":
                            localFolderName = "PlantDocs";
                            if (await ls.DoesDocExistOnTablet(
                                localFolderName, item.FileName))
                            {
                                remoteFolderName =
                                    $"PlantFiles/{SortPlantDocPath(item.FolderPath, item.PlantId)}";
                                var updateDocResult =
                                    await ls.UpdateFileFromAzure(
                                        item.FileName, localFolderName,
                                        remoteFolderName);

                                if (updateDocResult == "Good")
                                {
                                    CountOfDocsInStorage++;
                                    DocStatusMessage =
                                        $"Please Wait. Updating Files. {CountOfAllDocs - CountOfDocsInStorage} files still to update.";
                                    base.OnPropertyChanged("DocStatusMessage");
                                }
                            }

                            break;
                    }
                }

                DocStatusMessage =
                    "Please Wait. Updates complete - Checking.";
                base.OnPropertyChanged("DocStatusMessage");

                CountOfDocsInStorage =
                    await ls.CountNumberOfDocumentsInStorage(allDocs);
                CountOfAllDocs = allDocs.Count;
                DocStatusMessage =
                    CountOfDocsInStorage == CountOfAllDocs
                        ? "All Documents Up To Date."
                        : $"{CountOfAllDocs - CountOfDocsInStorage} out of {CountOfAllDocs} documents need updating";
                base.OnPropertyChanged("DocStatusMessage");
                UpdateDocsInd = false;
                base.OnPropertyChanged("UpdateDocsInd");
            }
        });

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

        private string SortPlantDocPath(string path, string plantId)
        {
            var returnValue = new StringBuilder();

            var splitItUpArray = path.Split('\\');
            foreach (var item in splitItUpArray)
                if (item != null && item.Length > 1)
                    returnValue.Append($"[{plantId}]{item}/");

            return returnValue.ToString();
        }

        public RelayCommand<string> RefreshTableAfterSelection
        {
            get
            {
                return _refreshTableAfterSelection ??= new RelayCommand<string>(e =>
                {
                    ShowCurrentDocumentsTable(CurrentPath, _whatDocumentsType2Display,
                        _currentSelectedJob == null ? "" : _currentSelectedJob.QuoteNumber.ToString(),
                        _confirmedUserId);
                });
            }
        }

        #endregion
    }
    
   
}