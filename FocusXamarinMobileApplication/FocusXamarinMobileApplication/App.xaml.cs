using FocusXamarinMobileApplication.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Syncfusion.Licensing;
using Application = Xamarin.Forms.Application;
using Constants = FocusXamarinMobileApplication.Services.Constants;

namespace FocusXamarinMobileApplication
{
    public partial class App : Application
    {
        public bool IsSignedIn
        {
            get => isSignedIn;
            set
            {
                isSignedIn = value;
                OnPropertyChanged();
                OnPropertyChanged("IsSignedOut");
            }
        }

        public bool IsSignedOut => !isSignedIn;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }

        public string UserEmail
        {
            get => userEmail;
            set
            {
                userEmail = value;
                OnPropertyChanged();
            }
        }

        public ImageSource UserPhoto
        {
            get => userPhoto;
            set
            {
                userPhoto = value;
                OnPropertyChanged();
            }
        }

        public static object ParentWindow { get; set; }

        public static FocusMobileV3Database Database
        {
            get
            {
                if (database == null) database = new FocusMobileV3Database();
                return database;
            }
        }

        public AuthenticationResult result { get; private set; }

        public static ViewModelLocator ViewModelLocator =>
            _viewModelLocator ??= new ViewModelLocator();

        private static ViewModelLocator _viewModelLocator;

        // UIParent used by Android version of the app
        public static object AuthUIParent = null;

        // Keychain security group used by iOS version of the app
        public static string iOSKeychainSecurityGroup = null;

        // Microsoft Authentication client for native/mobile apps
        public static IPublicClientApplication PCA;

        // Microsoft Graph client
        public static GraphServiceClient GraphClient;

        // Microsoft Graph client
        public static AuthenticationService _authenticationService;

        //private static Locator _locator;
        //public static Locator Locator { get { return _locator ?? (_locator = new Locator()); } }

        private static FocusMobileV3Database database;

        // Microsoft Graph permissions used by app
        public readonly string[] Scopes = OAuthSettings.Scopes.Split(' ');

        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        //public static string AzureBackendUrl =
        //    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        //public static bool UseMockDataStore = true;
        // Is a user signed in?
        private bool isSignedIn;

        // The user's email address
        private string userEmail;

        // The user's display name
        private string userName;

        // The user's profile photo
        private ImageSource userPhoto;
        public App()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5gSX9bd0BhXn5ZeHM=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1QdkRrWHpcd3VcRWg=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViUX5bcnNXR2Rb;MTU2NDQzNEAzMjMxMmUzMTJlMzMzNUdJK1owK1BEUVBvdlkrYy9VRWJyaVUwZmZuN0hpV3dVMElRUHYyZXFoOE09;MTU2NDQzNUAzMjMxMmUzMTJlMzMzNUwrZE1JQzVSR1gzZnk2dEgzTmEyRVliaEVJYm5OU3RvL2V3Ry9XcWY3dUU9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUZjWHdfdXZSRWFVVg==;MTU2NDQzN0AzMjMxMmUzMTJlMzMzNUxZOEhjNG9lbHltSTB3NWN2YTlQNllwbE5kU0N5dWhhSVpBazcrVHJyV1k9;MTU2NDQzOEAzMjMxMmUzMTJlMzMzNVl6UHc1UzEzNVVGUUFlamlRVElVUXpibFloMlYwdVVuQnE1R0dFbnM2cUk9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViUX5bcnNRR2Vb;MTU2NDQ0MEAzMjMxMmUzMTJlMzMzNWVnSFN0VmM0SVowT3hHTUcxUkp1RllMWDk4dzR6cWdxdzQ4M2VUMzY3anc9;MTU2NDQ0MUAzMjMxMmUzMTJlMzMzNWVrYzlBY1JYaFh4MEJEQXhhSzRPYzRRSGxuOGZnb0lnQkl5SENmeWVTUk09;MTU2NDQ0MkAzMjMxMmUzMTJlMzMzNUxZOEhjNG9lbHltSTB3NWN2YTlQNllwbE5kU0N5dWhhSVpBazcrVHJyV1k9");
            _authenticationService = new AuthenticationService();

            InitializeComponent();

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else
            //    DependencyService.Register<AzureDataStore>();
            var builder = PublicClientApplicationBuilder
                .Create(OAuthSettings.ApplicationId)
                .WithRedirectUri(OAuthSettings.RedirectUri);

            if (!string.IsNullOrEmpty(iOSKeychainSecurityGroup))
                builder = builder.WithIosKeychainSecurityGroup(iOSKeychainSecurityGroup);

            PCA = builder.Build();

            NavigationalParameters.Constants = new Constants();
            //NavigationalParameters.MainPage = MainPage = new NavigationPage(new WelcomePage());
            NavigationalParameters.MainPage = new NavigationPage();

            var navigationPage = new NavigationPage(new LogInPage());
            ViewModelLocator.NavigationService.Initialize(navigationPage);

            MainPage = navigationPage;
            //NavigationalParameters.MainPage = MainPage;
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=76433af0-a754-4e7d-ac06-ae57dfc8e0db",
           typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async Task SignIn()
        {
            var aa = new AzureAuth();
            // First, attempt silent sign in
            // If the user's information is already in the app's cache,
            // they won't have to sign in again.
            try
            {
                var graphUser = await _authenticationService.Authenticate();

                NavigationalParameters.AuthorisedUser = graphUser;

                await aa.SaveUpdateAccessToken(graphUser);

                IsSignedIn = true;
            }
            catch (MsalUiRequiredException ex)
            {
                IsSignedIn = false;

                // A MsalUiRequiredException happened on AcquireTokenSilentAsync. This indicates you need to call AcquireTokenAsync to acquire a token
                Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                //authResult = await PCA.AcquireTokenInteractive(Scopes)
                //                                  .ExecuteAsync()
                //                                  .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Authentication failed. See exception messsage for more details: " + ex.Message);
            }
        }

        public async Task SignOut()
        {
            // Get all cached accounts for the app
            // (Should only be one)
            var accounts = await PCA.GetAccountsAsync();
            while (accounts.Any())
            {
                // Remove the account info from the cache
                await PCA.RemoveAsync(accounts.First());
                accounts = await PCA.GetAccountsAsync();
            }

            UserPhoto = null;
            UserName = string.Empty;
            UserEmail = string.Empty;
            IsSignedIn = false;
        }

    }
}
