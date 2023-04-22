namespace FocusXamarinMobileApplication.Views;

public partial class MapWithPinsPage : ContentPage
{
    private readonly FormsMapPageViewModel _vm;
    private MapSpan _mapSpan;

    public MapWithPinsPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.FormsMapPageViewModel;

        BindingContext = _vm;

        customMap.PropertyChanged += (sender, e) => { GetStreet(null, null); };

        customMap.CustomPins = _vm.CustomPins;

        customMap.Pins?.Clear();

        foreach (var pin in customMap.CustomPins) customMap.Pins.Add(pin);

        MoveToPosition(null, null);

        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(_vm.CurrentPosition, Distance.FromMiles(1.0)));
    }

    public List<CustomPin> CustomPins { get; set; }
    public Placemark CurrentPlacemark { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        GetStreet(null, null);

        _vm.GetPinsForJob.Execute(null);

        customMap.Pins?.Clear();

        customMap.CustomPins?.Clear();

        customMap.CustomPins.AddRange(_vm.CustomPins);

        foreach (var pin in customMap.CustomPins) customMap.Pins.Add(pin);

        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(_vm.CurrentPosition, Distance.FromMiles(1.0)));
    }

    private async void MoveToPosition(object sender, EventArgs e)
    {
        try
        {
            var location =
                await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best,
                    new TimeSpan(0, 0, 5)));

            if (location != null)
                NavigationalParameters.NewPosition = new Position(location.Latitude, location.Longitude);

            if (Convert.ToDecimal(NavigationalParameters.NewPosition.Latitude) == 0 &&
                Convert.ToDecimal(NavigationalParameters.NewPosition.Longitude) == 0)
                try
                {
                    var defaultLocation = NavigationalParameters.LoggedInUser.CompanyAddress;
                    var defaultlocations = await Geocoding.GetLocationsAsync(defaultLocation);

                    var defaultlocation = defaultlocations?.FirstOrDefault();
                    if (location != null)
                        NavigationalParameters.NewPosition =
                            new Position(defaultlocation.Longitude, defaultlocation.Latitude);
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                    _ = _vm.Alert("No Gps Location available at this time", "Error");
                }

            _mapSpan = MapSpan.FromCenterAndRadius(NavigationalParameters.NewPosition, Distance.FromMiles(0.3));

            customMap.IsShowingUser = true;
            customMap.MoveToRegion(_mapSpan);

            _vm.CurrentPosition = NavigationalParameters.NewPosition;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            _ = _vm.Alert("No Gps Location available at this time", "Error");
        }
    }

    private async void GetStreet(object sender, EventArgs e)
    {
        var placemarks =
            await Geocoding.GetPlacemarksAsync(_vm.CurrentPosition.Latitude, _vm.CurrentPosition.Longitude);

        CurrentPlacemark = placemarks?.FirstOrDefault();

        if (CurrentPlacemark?.PostalCode != null)
        {
            _vm.Location = CurrentPlacemark?.Location;
            _vm.FeatureName = CurrentPlacemark?.FeatureName ?? "Detail Unavailable";

            _vm.AdminArea = CurrentPlacemark?.AdminArea ?? "";
            _vm.SubAdminArea = CurrentPlacemark?.SubAdminArea ?? "";

            _vm.CountryName = CurrentPlacemark?.CountryName ?? "";
            _vm.CountryCode = CurrentPlacemark?.CountryCode ?? "";

            _vm.Locality = CurrentPlacemark?.Locality ?? "";
            _vm.SubLocality = CurrentPlacemark?.SubLocality ?? "";

            _vm.PostalCode = CurrentPlacemark?.PostalCode ?? "";

            _vm.Thoroughfare = CurrentPlacemark?.Thoroughfare ?? "";
            _vm.SubThoroughfare = CurrentPlacemark?.SubThoroughfare ?? "";
        }
    }

    private void StoreLocation(object sender, EventArgs e)
    {
        try
        {
            if (_vm.LocationInputText != " ")
            {
                if (CurrentPlacemark == null) MoveToPosition(null, null);

                //map.Pins.Add(new Pin { Type = PinType.Place, Position = Vm.CurrentPosition, Label = Vm.Thoroughfare, Address = Vm.Address });
                _vm.SaveStreetToDataBase();

                _ = _vm.Alert("Location Saved", "Saved");

                _vm.NavBack.Execute(null);
            }
            else
            {
                _ = _vm.Alert("Please Assign a Name to the pin", "Name Required!!");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();

            _ = _vm.Alert("Location failed to save", "Failed");
        }
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        try
        {
            if (e != null)
                _vm.CurrentPosition = e.Position;
            else
                _vm.LocationInputText = "Location";

            GetStreet(null, null);

            var pin =
                new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(_vm.CurrentPosition.Latitude, _vm.CurrentPosition.Longitude),
                    MarkerId = _vm.LocationInputText,
                    Label = $"{_vm.CurrentPosition.Latitude} {_vm.CurrentPosition.Longitude}",
                    Address =
                        $"{_vm.Thoroughfare}, " +
                        $"{_vm.SubLocality} " +
                        $"{_vm.Locality} " +
                        $"{_vm.AdminArea} " +
                        $"{_vm.SubAdminArea} " +
                        $"{_vm.PostalCode} " +
                        $"{_vm.CountryName} " +
                        $"{_vm.CountryCode}",
                    ImageName = "Monkey.png"

                    //,Url = "http://xamarin.com/about/"
                };

            customMap.Pins?.Clear();
            customMap.CustomPins?.Clear();

            if (_vm.CustomPins.All(x => x.Position != pin.Position)) _vm.CustomPins.Add(pin);

            customMap.CustomPins.AddRange(_vm.CustomPins);

            foreach (var item in customMap.CustomPins) customMap.Pins.Add(item);

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(_vm.CurrentPosition, Distance.FromMiles(1.0)));
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }
}