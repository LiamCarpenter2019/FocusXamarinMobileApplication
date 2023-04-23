namespace FocusXamarinMobileApplication.Views;

public partial class FormsMapPage : ContentPage
{
    private readonly FormsMapPageViewModel _vm;

    private MapSpan _mapSpan;

    public FormsMapPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.FormsMapPageViewModel;

        BindingContext = _vm;

        double zoomLevel = 5;

        var latlongDegrees = 360 / Math.Pow(2, zoomLevel);

        if (map.VisibleRegion != null)
            map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongDegrees, latlongDegrees));

        MoveToPosition(null, null);
    }

    public Placemark CurrentPlacemark { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        double zoomLevel = 5;

        var latlongDegrees = 360 / Math.Pow(2, zoomLevel);

        if (map.VisibleRegion != null)
            map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongDegrees, latlongDegrees));

        MoveToPosition(null, null);

        _vm.GetStreetAsync.Execute(null);
    }

    private async void MoveToPosition(object sender, EventArgs e)
    {
        try
        {
            var distanceFromPinView = 0.5;

            if (NavigationalParameters.MapType != "AssetLocationMap")
            {
                switch (

                       NavigationalParameters.AppMode)
                {
                    case NavigationalParameters.AppModes.CABLEMEASURE:
                    case NavigationalParameters.AppModes.CHAMBERMEASURE:
                    case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                    case NavigationalParameters.AppModes.DJEMEASURE:
                    case NavigationalParameters.AppModes.FJEMEASURE:
                    case NavigationalParameters.AppModes.BJEMEASURE:
                    case NavigationalParameters.AppModes.POLEMEASURE:
                    case NavigationalParameters.AppModes.DUCTMEASURE:
                    case NavigationalParameters.AppModes.UGCRPMEASURE:
                    case NavigationalParameters.AppModes.CHAMBERASBUILT:
                    case NavigationalParameters.AppModes.ASBUILT:
                    case NavigationalParameters.AppModes.DPASBUILT:
                    case NavigationalParameters.AppModes.BJEASBUILT:
                    case NavigationalParameters.AppModes.DJEASBUILT:
                    case NavigationalParameters.AppModes.FJEASBUILT:
                    case NavigationalParameters.AppModes.PRESITEPOLESURVEY:
                    case NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY:
                    case NavigationalParameters.AppModes.PRESITECHAMBERSURVEY:
                    case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                    case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                    case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                        NavigationalParameters.NewPosition = new Position(
                            Convert.ToDouble(NavigationalParameters.SelectedAsset?.latitude),
                            Convert.ToDouble(NavigationalParameters.SelectedAsset?.longitude));
                        break;
                    default:
                        {
                            var location =
                                await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best,
                                    new TimeSpan(0, 0, 5)));

                            NavigationalParameters.NewPosition = new Position(location.Latitude, location.Longitude);
                            break;
                        }
                }
            }


            if (NavigationalParameters.MapType == "AssetLocations")
            {
                map.Pins.Clear();

                NavigationalParameters.NewPosition = new Position(
                    Convert.ToDouble(NavigationalParameters.ListOfAssets?.FirstOrDefault()?.latitude),
                    Convert.ToDouble(NavigationalParameters.ListOfAssets?.FirstOrDefault()?.longitude));
                distanceFromPinView = 1.5;
                foreach (var item in NavigationalParameters.ListOfAssets)
                    map.Pins?.Add(
                        new CustomPin
                        {
                            Position = new Position(Convert.ToDouble(item?.latitude),
                                Convert.ToDouble(item?.longitude)),
                            Label = item.StreetName,
                            Type = PinType.SavedPin,
                            ImageName = "pinGreen",
                            StyleId = item?.StreetName
                        });
            }
            else if (NavigationalParameters.MapType == "AssetLocationMap"
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DPASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.ASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CABLEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.POLEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DUCTMEASURE)
            {
                if (NavigationalParameters.SelectedAsset != null
                    && !string.IsNullOrEmpty(NavigationalParameters.SelectedAsset?.latitude)
                    && !string.IsNullOrEmpty(NavigationalParameters.SelectedAsset?.latitude))
                    map.Pins?.Add(
                        new CustomPin
                        {
                            Position = new Position(Convert.ToDouble(NavigationalParameters.SelectedAsset?.latitude),
                                Convert.ToDouble(NavigationalParameters.SelectedAsset?.longitude)),
                            Label = NavigationalParameters.SelectedAsset.StreetName,
                            Type = PinType.SavedPin,
                            ImageName = "pinGreen",
                            StyleId = NavigationalParameters.SelectedAsset?.StreetName
                        });
            }
            else
            {
                if (NavigationalParameters.SelectedEndPoint != null
                    && !string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.latitude)
                    && !string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.latitude)
                    && NavigationalParameters.AppMode.ToString().ToLower().Contains("presite"))
                    map.Pins?.Add(
                        new CustomPin
                        {
                            Position = new Position(Convert.ToDouble(NavigationalParameters.SelectedEndPoint?.latitude),
                                Convert.ToDouble(NavigationalParameters.SelectedEndPoint?.longitude)),
                            Label = NavigationalParameters.SelectedEndPoint.StreetName,
                            Type = PinType.SavedPin,
                            ImageName = "pinGreen",
                            StyleId = NavigationalParameters.SelectedEndPoint?.StreetName
                        });
            }

            if (NavigationalParameters.NewPosition != null &&
                NavigationalParameters.NewPosition.Longitude != 0 &&
                NavigationalParameters.NewPosition.Latitude != 0)
            {
                _mapSpan = MapSpan.FromCenterAndRadius(NavigationalParameters.NewPosition, Distance.FromMiles(0.5));

                map.IsShowingUser = true;
                map.MoveToRegion(_mapSpan);

                _vm.CurrentPosition = NavigationalParameters.NewPosition;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            _ = _vm.Alert("No Gps Location available at this time", "Error");
            // _vm.NavBack.Execute(null);
        }
    }

    public void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        try
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DPASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.ASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CABLEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BLOCKAGE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.POLEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DUCTMEASURE)
            {
                if (e != null)
                    _vm.CurrentPosition = e.Position;
                else
                    _vm.LocationInputText = "Location";

                if (_vm.CurrentPosition.Longitude != 0 && _vm.CurrentPosition.Latitude != 0)
                {
                    _vm.GetStreetAsync.Execute(null);

                    if (NavigationalParameters.MapType != "AssetLocations" )
                    {
                        map.Pins?.Clear();


                        if (NavigationalParameters.MapType == "PlantIssue")
                            map.Pins?.Add(
                                new CustomPin
                                {
                                    Position =
                                        new Position(_vm.CurrentPosition.Latitude, _vm.CurrentPosition.Longitude),
                                    Label = NavigationalParameters.SelectetedPlantItem.AssetNo,
                                    Type = PinType.Place,
                                    ImageName = "pinGreen",
                                    StyleId = "Plant Issue"
                                });
                        else
                            map.Pins?.Add(
                                new CustomPin
                                {
                                    Position =
                                        new Position(_vm.CurrentPosition.Latitude, _vm.CurrentPosition.Longitude),
                                    Label = NavigationalParameters.SelectedEndPoint.StreetName ??
                                            NavigationalParameters.SelectedAsset?.StreetName,
                                    Type = PinType.Place,
                                    ImageName = "pinGreen",
                                    StyleId = NavigationalParameters.SelectedEndPoint?.StreetName ??
                                              NavigationalParameters.SelectedAsset?.StreetName
                                });

                    if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DPASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.ASBUILT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CABLEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.POLEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DUCTMEASURE)
                        {
                            if (NavigationalParameters.SelectedAsset != null)
                            {
                                NavigationalParameters.SelectedAsset.longitude =
                                    _vm.CurrentPosition.Longitude.ToString();
                                NavigationalParameters.SelectedAsset.latitude =
                                    _vm.CurrentPosition.Latitude.ToString();
                                NavigationalParameters.SelectedAsset.SavedToServer = false;
                               App.Database.SaveItem(NavigationalParameters.SelectedAsset);
                            }
                        }
                        else
                        {
                            if (NavigationalParameters.SelectedEndPoint != null)
                            {
                                NavigationalParameters.SelectedEndPoint.longitude =
                                    _vm.CurrentPosition.Longitude.ToString();
                                NavigationalParameters.SelectedEndPoint.latitude =
                                    _vm.CurrentPosition.Latitude.ToString();
                                NavigationalParameters.SelectedEndPoint.SavedToServer = false;
                                App.Database.SaveItem(NavigationalParameters.SelectedEndPoint);
                            }
                        }
                    }

                    _vm.What3Words(_vm.CurrentPosition.Latitude, _vm.CurrentPosition.Longitude);
                }
                else
                {
                    DisplayAlert("Location unavailable",
                        "The selected location is unavailable at this time, please try again later", "Close");
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            Console.Write(error);
        }
    }

    public void Save_Clicked(object sender, EventArgs e)
    {
        try
        {
            _vm.btnCapture_Clicked.Execute(null);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();

            _ = _vm.Alert("Location capture failed to save", "Failed");
        }
        finally
        {
            try
            {
                if (_vm.LocationInputText != "")
                {
                    if (CurrentPlacemark == null) MoveToPosition(null, null);

                    _vm.SaveStreetToDataBase();

                    if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT &&
                        CurrentPlacemark != null)
                        NavigationalParameters.CurrentPlantIssue.LocationText =
                            $"{CurrentPlacemark?.Thoroughfare} (Long:{CurrentPlacemark?.Location?.Longitude} - Lat:{CurrentPlacemark?.Location?.Latitude})";

                    // _ = _vm.Alert("Location Saved", "Saved");

                    if (NavigationalParameters.MapType == "AssetLocationMap")
                        _vm.TakePhoto.Execute(null);

                    _vm.Back.Execute(null);
                   
                   
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
    }

    public void StreetView_Clicked(object sender, EventArgs e)
    {
        btnHybrid.BackgroundColor = Color.LightGray;
        btnSattelite.BackgroundColor = Color.LightGray;
        btnStreet.BackgroundColor = Color.Green;
        _vm.SetMapType(MapType.Street);
    }

    public void HybridView_Clicked(object sender, EventArgs e)
    {
        btnHybrid.BackgroundColor = Color.Green;
        btnSattelite.BackgroundColor = Color.LightGray;
        btnStreet.BackgroundColor = Color.LightGray;
        _vm.SetMapType(MapType.Hybrid);
    }

    public void SattelliteView_Clicked(object sender, EventArgs e)
    {
        btnHybrid.BackgroundColor = Color.LightGray;
        btnSattelite.BackgroundColor = Color.Green;
        btnStreet.BackgroundColor = Color.LightGray;
        _vm.SetMapType(MapType.Satellite);
    }
}