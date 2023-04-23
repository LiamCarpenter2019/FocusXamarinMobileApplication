#region

using Application = Xamarin.Forms.Application;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class LoginPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private RelayCommand<string> _checkLogin;

    private RelayCommand<string> _screenLoaded;
    public Documents Docs;
    public DocumentListingPageViewModel Documents;

    public LoginPageViewModel()
    {
        Docs = new Documents();
        Documents = new DocumentListingPageViewModel();
        AzureAuth = new AzureAuth();
        HarmonixLogo = SimpleStaticHelpers.GetImage("HarmonixLogo");
    }

    public AzureAuth AzureAuth { get; set; }

    private bool _LogonCheckResult { get; set; } = true;

    public bool LogonCheckResult
    {
        get => _LogonCheckResult;
        set
        {
            _LogonCheckResult = value;
            OnPropertyChanged();
        }
    }

    //================
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

    private bool _showButtons { get; set; } = true;

    public bool ShowButtons
    {
        get => _showButtons;
        set
        {
            _showButtons = value;
            OnPropertyChanged();
        }
    }


    private ImageSource _harmonixLogo { get; set; }

    public ImageSource HarmonixLogo
    {
        get => _harmonixLogo;
        set
        {
            _harmonixLogo = value;
            OnPropertyChanged("HarmonixLogo");
        }
    }

    private ImageSource _settings { get; set; } = SimpleStaticHelpers.GetImage("Settings");

    public ImageSource Settings
    {
        get => _settings;
        set
        {
            _settings = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand<string> ScreenLoaded
    {
        get
        {
            return _screenLoaded ??= new RelayCommand<string>(
                async e =>
                {
                    NavigationalParameters.AppMode =
                        NavigationalParameters.AppModes.LOGIN;
                    ShowButtons = true;

                   // NavigationalParameters.ReturnPage = "LogInViewModelKey";

                    switch (e.ToString())
                    {
                        case "init":

                            if (!NavigationalParameters.Constants.AreThereAnyConstants())
                            {
                                var p = new Person();

                                NavigationalParameters.AssignmentList = new List<Assignment>();

                                NavigationalParameters.AppMode = NavigationalParameters.AppModes.SETTINGS;

                                //NavigationalParameters.ReturnPage = "LoginPageViewModelKey";

                                await NavigateTo(ViewModelLocator.SettingsPage);
                            }

                            break;
                    }

                    CheckLogin.Execute(null);
                });
        }
    }

    public RelayCommand<string> CheckLogin => _checkLogin ??= new RelayCommand<string>(async e =>
    {
        NavigationalParameters.AppMode =
            NavigationalParameters.AppModes.LOGIN;

        var cl = AzureAuth.GetUserAuthInformation();

        if (cl?.LoginDateTime != null)
        {
            var nowMinus10Hrs = DateTime.Now.AddHours(-10);

            if (cl.LoginDateTime > nowMinus10Hrs &&
                cl.LoginDateTime <= DateTime.Now)
            {
                var n = cl.LoginDateTime;
                LogonCheckResult = true;
            }
            else
            {
                var n = cl.LoginDateTime;
                await AzureAuth.SignoutUser();
                LogonCheckResult = false;

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                try
                {
                    if (NavigationalParameters.LoggedInUser != null)
                    {
                        if (NavigationalParameters.LoggedInUser.MemberPin != "1234"
                        && NavigationalParameters.LoggedInUser.MemberPin != "4321"
                        && NavigationalParameters.LoggedInUser.MemberPin != NavigationalParameters.LoggedInUser.MemberPin.Reverse())
                        {
                            if (NavigationalParameters.LoggedInUser.MyGroups.Any(x =>
                                    x.GroupName.ToLower().Contains("leader")))
                            {
                                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                                NavigationalParameters.AppType = NavigationalParameters.AppTypes.GANGER;

                                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.JOBLIST;

                                await NavigateTo(ViewModelLocator.MainListPage);
                            }
                            else if (NavigationalParameters.LoggedInUser.MyGroups.Any(x =>
                                         x.GroupName.ToLower().Contains("yardman")))
                            {
                                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                                NavigationalParameters.AppType = NavigationalParameters.AppTypes.YARDMAN;

                                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.JOBLIST;

                                await NavigateTo(ViewModelLocator.MainListPage);
                            }
                            else
                            {
                                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                                NavigationalParameters.AppType = NavigationalParameters.AppTypes.SUPERVISOR;

                                NavigationalParameters.ProjectListMode =
                                    NavigationalParameters.ProjectListModes.PROJECTLIST;

                                await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                            }
                        }
                        else {
                            Analytics.TrackEvent($"Password change request: {NavigationalParameters.LoggedInUser.FullName} requested to change password on {DateTime.Now}");

                            await Alert("Please change your password!! you will not be able to access the app in full untill you have completed this action", "Password change request", "Ok");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
                        $"Log In FAILED For: {NavigationalParameters.LoggedInUser.FullName} with error {ex}");

                    await Alert("User in not assigned to an appropriate group please try again", "Error", "Ok");
                }
                finally
                {
                    Loading = false;
                }
            }
        }
    });

    public RelayCommand NavToSettings => new(async () => { await NavigateTo(ViewModelLocator.SettingsPage); });

    public RelayCommand OnSignOut => new(async () =>
    {
        var signout = await Alert("Sign out?", "Do you want to sign out?", "Yes", "No");

        if (!signout) return;

        await (Application.Current as App)?.SignOut();

        LogonCheckResult = ((App)Application.Current).IsSignedOut;

        if (LogonCheckResult) await NavigateTo(ViewModelLocator.LogInPage);
    });

    public RelayCommand AutoSignOutFromSettings => new(async () =>
    {
        await (Application.Current as App)?.SignOut();

        LogonCheckResult = ((App)Application.Current).IsSignedOut;

        if (LogonCheckResult) await NavigateTo(ViewModelLocator.LogInPage);
    });

    public RelayCommand OnSignIn => new(async () =>
    {
        try
        {
            if (!NavigationalParameters.Constants.AreThereAnyConstants())
            {
                await NavigateTo(ViewModelLocator.SettingsPage);
            }
            else
            {
                await (Application.Current as App)?.SignIn();

                if (!((App)Application.Current).IsSignedIn) return;

                Loading = true;

                var au = new AzureAuth().GetUserAuthInformation();

                NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                NavigationalParameters.ReturnPage = "LoginPageViewModelKey";

                await ScreenLoadedCommand4LoginScreenWithAzureAuthAsync(NavigationalParameters.AuthorisedUser);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent($"Log In FAILED For: {NavigationalParameters.LoggedInUser?.FullName} with error {ex}");

            await Alert("Authentication Error", ex.Message);
        }
    });

    public async Task ScreenLoadedCommand4LoginScreenWithAzureAuthAsync(
        AuthenticatedUser au)
    {
        try
        {
            if (au != null && au.UserPrincipalName != null && au.UserPrincipalName.Length > 3 &&
                au.UserPrincipalName.Contains("@"))
            {
                var wa = new WebApi();
                var connectionAvailable = await wa.CanWeConnect2Api();
                if (connectionAvailable)
                {
                    Loading = true;

                    ShowButtons = false;

                    var lr = await wa.LogonRequestAzureAuth(au);

                    if (lr.Item2.Contains("Good"))
                    {
                        Analytics.TrackEvent($"Log In For: {lr.Item1?.FullName}");

                        NavigationalParameters.LoggedInUser = lr?.Item1;

                        if (NavigationalParameters.LoggedInUser.MyGroups.Any(x =>
                                x.GroupName.ToLower().Contains("leader")))
                        {
                            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                            NavigationalParameters.AppType = NavigationalParameters.AppTypes.GANGER;

                            await NavigateTo(ViewModelLocator.MainListPage);
                        }
                        else if (NavigationalParameters.LoggedInUser.MyGroups.Any(x =>
                                     x.GroupName.ToLower().Contains("yardman")))
                        {
                            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                            NavigationalParameters.AppType = NavigationalParameters.AppTypes.YARDMAN;

                            await NavigateTo(ViewModelLocator.MainListPage);
                        }
                        else
                        {
                            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MAIN;

                            NavigationalParameters.AppType = NavigationalParameters.AppTypes.SUPERVISOR;

                            await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                        }
                    }
                    else
                    {
                        Analytics.TrackEvent($"Log In FAILED For: {lr.Item1?.FullName}");

                        await Alert(
                            "It has been unable retrieve the information from the server please try again or contact your administrator",
                            "Error");
                    }
                }
                else
                {
                    Analytics.TrackEvent($"Log In FAILED (No connectivity) For: {au.UserPrincipalName}");

                    await Alert("Connectivity Issue - Please try to find a better connection & try again!", "Error!");
                }
            }
            else
            {
                Analytics.TrackEvent("Log In FAILED (Unknown User)");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent($"Log In FAILED For: {NavigationalParameters.LoggedInUser.FullName} with error {ex}");

            await Alert(
                "It has been unable retrieve the information from the server please try again or contact your administrator",
                "Error");
        }
        finally
        {
            Loading = false;
        }
    }
}