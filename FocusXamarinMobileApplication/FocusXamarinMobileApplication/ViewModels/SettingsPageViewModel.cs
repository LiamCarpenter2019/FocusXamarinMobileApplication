#region

using Constants = FocusXamarinMobileApplication.Services.Constants;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SettingsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public string AlertTitle { get; private set; }
    public string AlertMessage { get; private set; }

    public bool logonCheckResult { get; set; }


    private bool _loading { get; set; }

    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            OnPropertyChanged();
        }
    }

    private bool _showBackButton { get; set; }

    public bool ShowBackButton
    {
        get => _showBackButton;
        set
        {
            _showBackButton = value;
            OnPropertyChanged();
        }
    }

    private bool _showApiButton { get; set; } = true;

    public bool ShowApiButton
    {
        get => _showApiButton;
        set
        {
            _showApiButton = value;
            OnPropertyChanged();
        }
    }

    private string _currentApiFriendlyName { get; set; }

    public string CurrentApiFriendlyName
    {
        get => _currentApiFriendlyName;
        set
        {
            _currentApiFriendlyName = value;
            OnPropertyChanged();
        }
    }

    private bool _showPinButton { get; set; }

    public bool ShowPinButton
    {
        get => _showPinButton;
        set
        {
            _showPinButton = value;
            OnPropertyChanged();
        }
    }

    private string _enableOrDisableApiButton { get; set; }

    public string EnableOrDisableApiButton
    {
        get => _enableOrDisableApiButton;
        set
        {
            _enableOrDisableApiButton = value;
            OnPropertyChanged();
        }
    }

    private string _apiCode { get; set; }

    public string ApiCode
    {
        get => _apiCode;
        set
        {
            _apiCode = value;
            OnPropertyChanged();
        }
    }

    private string _versionNumber { get; set; }

    public string VersionNumber
    {
        get => _versionNumber;
        set
        {
            _versionNumber = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand<string> _changePin;
    private RelayCommand<string> _purgeDatabase;
    private RelayCommand<string> _screenLoaded;
    private RelayCommand<string> _setApi;

    public SettingsPageViewModel()
    {
        ShowApiButton = true;
        ShowPinButton = false;
    }

    public RelayCommand<string> ScreenLoaded
    {
        get
        {
            return _screenLoaded ??= new RelayCommand<string>(async e =>
            {
                Loading = false;

                if (NavigationalParameters.LoggedInUser != null)
                {
                    ShowBackButton = true;
                    ShowPinButton = true;
                }
                else
                {
                    ShowBackButton = false;

                    ShowPinButton = false;
                }

                VersionNumber = "Version : 6.0.30";

                try
                {
                    //ShowPinButton = NavigationalParameters.ReturnPage == "MainListPageViewModelKey";

                    ShowApiButton = true;

                    var c = new Constants();

                    Analytics.TrackEvent($"Mickie 1: {c.AreThereAnyConstants()}");

                    if (c.AreThereAnyConstants())
                    {
                        Analytics.TrackEvent("Mickie 2");
                        CurrentApiFriendlyName = c.FriendlyName;
                    }
                    else
                    {
                        var wa = new WebApi();

                        var doWeHaveInternet = await wa.CanWeConnect2Api();

                        Analytics.TrackEvent($"Mickie 3: {doWeHaveInternet}");

                        if (doWeHaveInternet)
                            await Alert("Configuration!",
                                "No API Connection set - Please input a Code to Set the API!");
                        else
                            await Alert("Signal!",
                                "No Internet Connection - Please Connect to the internet and input a Code!");
                    }
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
                        $"The app has crashed and has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                }
            });
        }
    }

    public RelayCommand<string> ChangePin
    {
        get
        {
            return _changePin ??= new RelayCommand<string>(async e =>
            {
                NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.CHANGE_PIN;
         
                //NavPassedInfo = e;
                // NavPassedInfo = new Tuple<Person, List<Models.Assignment>, string>(e.Item1,e.Item2,"Settings");
                //NavigationalParameters.ReturnPage = Locator.SettingsPageViewModelKey;

                await NavigateTo(ViewModelLocator.SignaturePage);
                // _navigationService.NavigateTo(Locator.SignaturePageViewModelKey, e);
            });
        }
    }


    public RelayCommand<string> SetApi => _setApi ??= new RelayCommand<string>(async e =>
    {
        if (string.IsNullOrEmpty(ApiCode) || ApiCode.Length < 3 || ApiCode.Length > 7)
        {
            await Alert("Please Input a valid API Code!",
                "Error!");
            return;
        }

        Loading = true;

        ShowApiButton = false;


        // RaisePropertyChanged(() => SpinnerAction);

        var c = new Constants();
        var returnedValue = await c.GetNewApiString(ApiCode);
        if (returnedValue == "Fail")
        {
            Loading = false;
            ShowApiButton = true;
            //RaisePropertyChanged(() => SpinnerAction);
            await Alert("Failed to get new Connection please check Code & try again!",
                "Error!");
        }
        else
        {
            CurrentApiFriendlyName = returnedValue;
            // RaisePropertyChanged(() => CurrentApiFriendlyName);

            //RaisePropertyChanged(() => SpinnerAction);
            // Enable buttons


            var procceed = await Alert("Success", "New API configured! please log in!", "Procceed", "Cancel");

            if (procceed)
            {
                Loading = true;

                ShowApiButton = false;

                new LoginPageViewModel().AutoSignOutFromSettings.Execute(null);
            }
            else
            {
                Loading = false;

                ShowApiButton = true;
            }

            //if (NavigationalParameters.LoggedInUser == null)
            //    NavigateBack();
            //else
            //
        }

        // RaisePropertyChanged(() => CodeInput);
    });

    public RelayCommand Back => new(async () => { NavigateBack(); });

    public RelayCommand<string> PurgeDatabase
    {
        get
        {
            return _purgeDatabase ??= new RelayCommand<string>(async e =>
            {
                try
                {
                    var wa = new WebApi();
                    var job = new Jobs();
                    var assignmentService = new Assignments();
                    var docs = new Documents();
                    var user = new Users();
                    var check = new Checks();
                    var plantData = new Plant();
                    var pic = new Pictures();
                    var _db = new FocusMobileV3Database();
                    var password = DateTime.Now.ToShortDateString().ToCharArray();
                    var passwordCombo = $"{password[3]}{password[4]}{password[2]}{password[1]}";
                    var doWeHaveInternet = await wa.CanWeConnect2Api();

                    if (doWeHaveInternet)
                    {
                        if (passwordCombo == ApiCode)
                        {
                            //Todo back up db file
                            var success = await _db.BackupDatabase(Constants.DatabasePath);

                            if (success)
                            {
                                await user.ClearAllRows();
                                await job.ClearAllRows();
                                assignmentService.ClearAllRows("All");
                                await check.ClearAllRows("Checks4Tablet");
                                await check.ClearAllRows("DeleteAllChecksBeforeToday");
                                await plantData.ClearAllRows("Plant4Tablet");
                                docs.ClearAllRows();
                                await pic.ClearAllRows();
                                App.Database.CreateTable<ApiStructure>();
                                App.Database.CreateTable<AuthenticatedUser>();

                                ShowBackButton = false;
                                ShowPinButton = false;
                                NavigationalParameters.LoggedInUser = null;

                                await Alert(
                                    "The database has been cleared please reset api and log in to pull in a fresh copy!",
                                    "Success!");
                            }
                            else
                            {
                                await Alert(
                                    "Please contact support to receive the password! Enter the password into the Set Client Field and try again!",
                                    "Restricted!");
                            }
                        }
                        else
                        {
                            await Alert(
                                "The password is incorrect please try again! If this error persists please contact support.",
                                "Error!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    var ERROR = ex.ToString();
                    await Alert(
                        "The database has not been successfully wiped please try again if the issue persits please contact support!",
                        "Error!");
                }
            });
        }
    }

}