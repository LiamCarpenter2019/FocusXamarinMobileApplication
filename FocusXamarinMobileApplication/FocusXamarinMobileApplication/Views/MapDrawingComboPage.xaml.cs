namespace FocusXamarinMobileApplication.Views;

public partial class MapDrawingComboPage : ContentPage
{
    private readonly FormsMapPageViewModel _vm;
    private MapSpan _mapSpan;

    public MapDrawingComboPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.FormsMapPageViewModel;
        BindingContext = _vm;
        MoveToPosition(null, null);

        map.PropertyChanged += (sender, e) => { GetStreet(null, null); };

        var ls = new LocalStorage();
        var d = new Documents();

        var rootFolder = ls.GetTabletDocsPath();

        //PdfView.Source = Vm.PdfSource;  
    }

    public Placemark CurrentPlacemark { get; set; }
    public string DocumentUrl { get; }

    private async void MoveToPosition(object sender, EventArgs e)
    {
        try
        {
            var location =
                await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best,
                    new TimeSpan(0, 0, 5)));
            var position = new Position(location.Latitude, location.Longitude);

            _mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromMiles(0.1));

            map.IsShowingUser = true;
            map.MoveToRegion(_mapSpan);

            _vm.CurrentPosition = position;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    private async void GetStreet(object sender, EventArgs e)
    {
        var placemarks =
            await Geocoding.GetPlacemarksAsync(_vm.CurrentPosition.Latitude, _vm.CurrentPosition.Longitude);

        CurrentPlacemark = placemarks?.FirstOrDefault();

        if (CurrentPlacemark != null)
        {
            _vm.Location = CurrentPlacemark?.Location;
            _vm.FeatureName = CurrentPlacemark?.FeatureName;

            _vm.AdminArea = CurrentPlacemark?.AdminArea;
            _vm.SubAdminArea = CurrentPlacemark?.SubAdminArea;

            _vm.CountryName = CurrentPlacemark?.CountryName;
            _vm.CountryCode = CurrentPlacemark?.CountryCode;

            _vm.Locality = CurrentPlacemark?.Locality;
            _vm.SubLocality = CurrentPlacemark?.SubLocality;

            _vm.PostalCode = CurrentPlacemark?.PostalCode;

            _vm.Thoroughfare = CurrentPlacemark?.Thoroughfare;
            _vm.SubThoroughfare = CurrentPlacemark?.SubThoroughfare;
        }
    }
}