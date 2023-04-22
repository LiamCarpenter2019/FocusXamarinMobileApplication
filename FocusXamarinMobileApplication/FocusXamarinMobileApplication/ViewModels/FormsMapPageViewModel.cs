#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Curnow.Biz.What3WordsV3Net;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class FormsMapPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly PhotoSelectionPageViewModel _psvm;
    private readonly W3WClient w3w;
    private string _adminArea;

    private string _countryCode;

    private string _countryName;

    private string _documentUrl;

    private string _featureName;
    public Jobs _jobService;

    private string _latLon;

    private string _locality;


    public MapType _mapType;
    public WebViewSource _pdfSource = "https://roadworks.org";
    public Pictures _pictureService;

    private Position _position;

    private string _postalCode;

    private string _streetName;

    private string _subAdminArea;

    private string _subLocality;

    private string _subThoroughfare;

    public string _thoroughfare;
    private MapType mapType;

    public FormsMapPageViewModel()
    {
        w3w = new W3WClient("8EBC5ZGF");

        _pictureService = new Pictures();

        _jobService = new Jobs();

        _ls = new LocalStorage();

        _ = GetCurrentPosition();

        _psvm = new PhotoSelectionPageViewModel();

        GetPinsForJob.Execute(null);
    }

    private bool _screenShot { get; set; } = true;

    private LocalStorage _ls { get; }

    public ObservableCollection<Location> Locations { get; set; }

    public bool ScreenShot
    {
        get => _screenShot;
        set
        {
            _screenShot = value;
            OnPropertyChanged();
        }
    }


    public string StreetName
    {
        get => _streetName;
        set
        {
            _streetName = value;
            OnPropertyChanged();
        }
    }


    public WebViewSource PdfSource
    {
        get => _pdfSource;
        set
        {
            _pdfSource = value;
            OnPropertyChanged();
        }
    }

    public Position CurrentPosition
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged("Position");
        }
    }

    public string AdminArea
    {
        get => _adminArea;
        set
        {
            _adminArea = value;
            OnPropertyChanged();
        }
    }

    public string DocumentUrl
    {
        get => _documentUrl;
        set
        {
            _documentUrl = value;
            OnPropertyChanged();
        }
    }

    public string LatLon
    {
        get => _latLon;
        set
        {
            _latLon = value;
            OnPropertyChanged();
        }
    }

    public string SubAdminArea
    {
        get => _subAdminArea;
        set
        {
            _subAdminArea = value;
            OnPropertyChanged();
        }
    }

    public string CountryName
    {
        get => _countryName;
        set
        {
            _countryName = value;
            OnPropertyChanged();
        }
    }

    public Placemark CurrentPlacemark { get; set; }

    public Location Location { get; set; }

    public string CountryCode
    {
        get => _countryCode;
        set
        {
            _countryCode = value;
            OnPropertyChanged();
        }
    }

    public string SubLocality
    {
        get => _subLocality;
        set
        {
            _subLocality = value;
            OnPropertyChanged();
        }
    }

    public string PostalCode
    {
        get => _postalCode;
        set
        {
            _postalCode = value;
            OnPropertyChanged();
        }
    }

    public string Thoroughfare
    {
        get => _thoroughfare;
        set
        {
            _thoroughfare = value;
            OnPropertyChanged();
        }
    }

    public string SubThoroughfare
    {
        get => _subThoroughfare;
        set
        {
            _subThoroughfare = value;
            OnPropertyChanged();
        }
    }

    public string FeatureName
    {
        get => _featureName;
        set
        {
            _featureName = value;
            OnPropertyChanged();
        }
    }

    public string Locality
    {
        get => _locality;
        set
        {
            _locality = value;
            OnPropertyChanged();
        }
    }

    public string _locationInputText { get; set; } = "";

    public string LocationInputText
    {
        get => _locationInputText;
        set
        {
            _locationInputText = value;
            OnPropertyChanged();
        }
    }

    public bool _showSurveyQuestionsButton { get; set; } = true;

    public bool ShowSurveyQuestionsButton
    {
        get => _showSurveyQuestionsButton;
        set
        {
            _showSurveyQuestionsButton = value;
            OnPropertyChanged();
        }
    }

    public bool _showSave { get; set; } = true;

    public bool ShowSave
    {
        get => _showSave;
        set
        {
            _showSave = value;
            OnPropertyChanged("ShowSave");
        }
    }

    public bool _showBackButton { get; set; } = true;

    public bool ShowBackButton
    {
        get => _showBackButton;
        set
        {
            _showBackButton = value;
            OnPropertyChanged();
        }
    }

    public MapType MapType
    {
        get => _mapType;
        set
        {
            _mapType = value;
            OnPropertyChanged();
        }
    }

    public List<CustomPin> CustomPins { get; set; } = new();


    public string ProjectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;

    public string ProjectDate { get; set; } =
        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

    public string Address { get; set; }

    public string Description { get; set; }

    public RelayCommand GetPinsForJob => new(async () => { CustomPins = await _jobService.GetPinsForJobAsync(); });

    public RelayCommand NavBack => new(async () =>
    {
        try
        {
            if (NavigationalParameters.MapType == "PlantIssue"
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION
                || NavigationalParameters.MapType == "EventManagement")
                NavigateBack();
            else
                //await NavigateTo(ViewModelLocator.SelectStreetPage);
                await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand Back => new(async () =>
    {
        if (NavigationalParameters.AddingNewAsset == true)
        {
            await NavigateTo(ViewModelLocator.SelectEndPointPage);

        }
        else
        {
            NavigateBack();
        }
       
    });

    public RelayCommand GoToSurveyQuestions => new(async () =>
    {
        try
        {
            if (NavigationalParameters.SelectedAsset == null
                && NavigationalParameters.SelectedEndPoint == null)
            {
                if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite")
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT)
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

    public RelayCommand GetStreetAsync => new(async () =>
    {
        try
        {
            if (NavigationalParameters.MapType == "AssetLocations") { ShowSave = false; }
            else if (NavigationalParameters.MapType == "PoleLocation") { ShowSave = false;  } else { ShowSave = true;  }

            if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite")
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT)
            {
                ShowBackButton = false;
                ShowSurveyQuestionsButton = true;
            }
            else
            {
                ShowBackButton = true;
                ShowSurveyQuestionsButton = false;
            }

            if (NavigationalParameters.SelectedEndPoint != null || NavigationalParameters.SelectedAsset != null)
                Title =
                    $"{NavigationalParameters.SelectedEndPoint?.StreetName ?? NavigationalParameters.SelectedAsset.StreetName}";
            else
                Title = "Add Loaction";

            if (NavigationalParameters.NewPosition == null && CurrentPosition == null) _ = GetCurrentPosition();

            if ((NavigationalParameters.MapType == "AssetLocationMap" || NavigationalParameters.MapType.ToLower() == "eventmanagement") && NavigationalParameters.NewPosition != null)
                CurrentPosition = NavigationalParameters.NewPosition;

            LocationInputText = "";


            if (CurrentPosition != null
                && CurrentPosition.Latitude != 0
                && CurrentPosition.Longitude != 0)
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    var a = await Geocoding.GetPlacemarksAsync(CurrentPosition.Latitude, CurrentPosition.Longitude);

                    try
                    {
                        if (a != null)
                        {
                            CurrentPlacemark = a.FirstOrDefault();

                            if (CurrentPlacemark?.PostalCode != null)
                            {
                                Location = CurrentPlacemark?.Location;

                                FeatureName = CurrentPlacemark?.FeatureName ?? "";

                                AdminArea = CurrentPlacemark?.AdminArea ?? "";

                                CountryName = CurrentPlacemark?.CountryName ?? "";

                                Locality = CurrentPlacemark?.Locality ?? "";

                                if (NavigationalParameters.AppMode !=
                                    NavigationalParameters.AppModes.PRESITEPOLESURVEY &&
                                    NavigationalParameters.AppMode != NavigationalParameters.AppModes.CHAMBERASBUILT)
                                {
                                    PostalCode = CurrentPlacemark?.PostalCode ?? "";

                                    Thoroughfare = CurrentPlacemark?.Thoroughfare ?? "";

                                    What3Words(CurrentPosition.Latitude, CurrentPosition.Longitude);
                                }
                                else
                                {
                                    What3Words(
                                        Convert.ToDouble(NavigationalParameters.SelectedAsset?.latitude ??
                                                         NavigationalParameters.SelectedEndPoint?.latitude),
                                        Convert.ToDouble(NavigationalParameters.SelectedAsset?.longitude ??
                                                         NavigationalParameters.SelectedEndPoint?.longitude));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Analytics.TrackEvent(
                            $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                        await Alert("GPS resolve issue",
                            "The location can not be retrurned as there is a issue resolving the gps please check your connectivity and try again if this issue persist please contact support!.",
                            "Ok");
                    }
                }
                else
                {
                    LocationInputText = FeatureName = "";

                    AdminArea = "";

                    CountryName = "";

                    Locality = "";

                    PostalCode = "";

                    Thoroughfare = "";


                    await Alert("No connectivity",
                        "There is no connectivity to the internet to resolve the long and lat of this location.", "Ok");
                }

                LatLon = $"Lat:{CurrentPosition.Latitude} Lon:{CurrentPosition.Longitude} ";
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
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
        //NavigationalParameters.MapType = "PreSiteDrawing";
        try
        {
            var docs = new Documents();
            NavigationalParameters.MapType = "Drawing";
            NavigationalParameters.SelectedDocument = docs.GetDocuments("/Drawings/", "JobDocs",
                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle
                    .Contains($"{NavigationalParameters.CurrentSelectedJob?.QuoteNumber}") && x.DocumentTitle.ToUpper()
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

    public RelayCommand Refresh => new(async () =>
    {
        PdfSource = "";
        PdfSource = "https://roadworks.org";
    });

    public RelayCommand TakePhoto => new(async () =>
    {
        try
        {
            _psvm.TakePhoto.Execute(null);       
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand SaveNewLocation => new(async () =>
    {
        try
        {
            if (!string.IsNullOrEmpty(LocationInputText))
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(NavigationalParameters.NewPosition.Latitude,
                    NavigationalParameters.NewPosition.Longitude);

                if (placemarks != null)
                {
                    CurrentPlacemark = placemarks?.FirstOrDefault();

                    Location = CurrentPlacemark?.Location;

                    FeatureName = CurrentPlacemark?.FeatureName ?? "";

                    AdminArea = CurrentPlacemark?.AdminArea ?? "Unavailable";

                    SubAdminArea = CurrentPlacemark?.SubAdminArea ?? "Unavailable";

                    CountryName = CurrentPlacemark?.CountryName ?? "Unavailable";

                    CountryCode = CurrentPlacemark?.CountryCode ?? "Unavailable";

                    Locality = CurrentPlacemark?.Locality ?? "Unavailable";

                    SubLocality = CurrentPlacemark?.SubLocality ?? "Unavailable";

                    PostalCode = CurrentPlacemark?.PostalCode ?? "Unavailable";

                    Thoroughfare = CurrentPlacemark?.Thoroughfare ?? "Unavailable";

                    SubThoroughfare = CurrentPlacemark?.SubThoroughfare ?? "Unavailable";

                    switch (NavigationalParameters.AppMode)
                    {
                        case NavigationalParameters.AppModes.CHAMBERASBUILT:
                        case NavigationalParameters.AppModes.DPASBUILT:
                        case NavigationalParameters.AppModes.BJEASBUILT:
                        case NavigationalParameters.AppModes.DJEASBUILT:
                        case NavigationalParameters.AppModes.FJEASBUILT:
                        case NavigationalParameters.AppModes.ASBUILT:
                        case NavigationalParameters.AppModes.PRESITEPOLESURVEY:
                        case NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY:
                        case NavigationalParameters.AppModes.PRESITECHAMBERSURVEY:
                        case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                        case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                        case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                        case NavigationalParameters.AppModes.CABLEMEASURE:
                        case NavigationalParameters.AppModes.CHAMBERMEASURE:
                        case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                        case NavigationalParameters.AppModes.DJEMEASURE:
                        case NavigationalParameters.AppModes.FJEMEASURE:
                        case NavigationalParameters.AppModes.BJEMEASURE:
                        case NavigationalParameters.AppModes.POLEMEASURE:
                        case NavigationalParameters.AppModes.DUCTMEASURE:
                        case NavigationalParameters.AppModes.UGCRPMEASURE:
                            LatLon =
                                $"Lon:{NavigationalParameters.SelectedAsset?.longitude ?? NavigationalParameters.SelectedEndPoint?.longitude} Lat:{NavigationalParameters.SelectedAsset?.latitude ?? NavigationalParameters.SelectedEndPoint?.latitude}";
                            break;
                    }


                    SaveStreetToDataBase();
                }
            }
            else
            {
                await Alert("Incomplete",
                    "Please Complete all fields by selecting a position on the map and adding a description", "Ok");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GetPosition => new(async () => { await GetCurrentPosition(); });

    public RelayCommand ReturnToQuestions => new(async () =>
    {
        await Alert("There is no drawing available at this time", "!");
        //   await NavigateTo(new SurveyQuestions());
    });


    public RelayCommand btnCapture_Clicked => new(async () =>
    {
        try
        {
            _pictureService = new Pictures();

            ScreenShot = false;

            var screenshot = await Screenshot.CaptureAsync();

            var stream = await screenshot.OpenReadAsync();


            var Photo = new Pictures4Tablet
            {
                PicturePath = "JobPictures",
                OperativeId = NavigationalParameters.LoggedInUser?.FocusId,
                OperativeRole = NavigationalParameters.LoggedInUser?.Role,
                QNumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber,
                QuestionId = NavigationalParameters.SelectedQuestion?.QuestionId.ToString() ?? "0",
                ResponseId = NavigationalParameters.SelectedAnswer?.Id.ToString() ?? "0",
                SeverPictureId = 0
            };

            if (NavigationalParameters.CurrentAssignment != null)
                Photo.AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId;

            Photo.Image = Photo.ImgToByteArray(stream);
            Photo.OperativeId = NavigationalParameters.LoggedInUser?.FocusId;
            Photo.OperativeRole = NavigationalParameters.LoggedInUser?.Role;
            Photo.Category = NavigationalParameters.AppMode.ToString();


            var date = DateTime.Now.Date.ToShortDateString();

            var dateString = date.Replace("/", "");

            Photo.FileName =
                $"{dateString}_{Guid.NewGuid()}_{NavigationalParameters.LoggedInUser.FocusId}.jpg";

            Photo.GangLeaderId = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
            Photo.SupervisorId = NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();
            Photo.ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference;
            Photo.DateTimeTaken = NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                : DateTime.Now;
            Photo.JobId = NavigationalParameters.CurrentSelectedJob.JobId;
            Photo.StreetName = NavigationalParameters.SelectedEndPoint?.StreetName ??
                               NavigationalParameters.SelectedAsset?.StreetName;
            Photo.PictureReason = "AssetLocationMap";
            Photo.Category = NavigationalParameters.SurveyType.ToString();
            Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
            Photo.QuestionId = "0";

            if (CurrentPosition != null)
            {
                if (NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt") || NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
                {
                    Photo.Latitude = NavigationalParameters.SelectedAsset?.latitude ??
                                     NavigationalParameters.SelectedEndPoint?.latitude;
                    Photo.Longitude = NavigationalParameters.SelectedAsset?.longitude ??
                                      NavigationalParameters.SelectedEndPoint?.longitude;
                }
                else
                {
                    Photo.Latitude = CurrentPosition.Latitude.ToString();
                    Photo.Longitude = CurrentPosition.Longitude.ToString();
                }
            }

            var PhotosToDelete = _pictureService.GetAllPictures()?.Where(x => x.QNumber == Photo.QNumber
                                                                              && x.PictureReason.ToLower() ==
                                                                              "AssetLocationMap"
                                                                              && x.StreetName == NavigationalParameters
                                                                                  .CurrentAssignment?.StreetName)
                ?.ToList();

            foreach (var item in PhotosToDelete) await _pictureService.DeleteJobPicture(item);

            await _pictureService.AddPicture(Photo);

            await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            ScreenShot = true;
        }
        finally
        {
            ScreenShot = true;
        }
    });

    public ImageSource Image { get; private set; }

    public void SetMapType(MapType mapType)
    {
        MapType = mapType;
    }

    [Time]
    public void What3Words(double latitude, double longitude)
    {
        try
        {
            var response = w3w.ConvertTo3WA(latitude, longitude);

            LocationInputText = $"{response.words}";

            if (NavigationalParameters.SelectedAnswer != null)
            {
                NavigationalParameters.SelectedAnswer.AnswerGiven = $"{NavigationalParameters.SelectedAnswer?.AnswerGiven} - {LocationInputText}";
                App.Database.SaveItem(NavigationalParameters.SelectedAnswer);
            }

            if (NavigationalParameters.SelectedCabinet != null)
                NavigationalParameters.SelectedCabinet.Location = $"{latitude},{longitude}";
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    public async Task<bool> GetCurrentPosition()
    {
        try
        {
            var location = await Geolocation.GetLocationAsync();

            if (location != null)
                NavigationalParameters.NewPosition =
                    CurrentPosition = new Position(location.Latitude, location.Longitude);

            return true;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            return false;
        }
    }

    [Time]
    public async void SaveStreetToDataBase()
    {
        try
        {
            var jobService = new Jobs();
            var assignmentService = new Assignments();

            if (NavigationalParameters.MapType == "QuestionGps")
            {
                NavigationalParameters.SelectedAnswer = assignmentService.GetCurrentResponse(
                    NavigationalParameters.CurrentAssignment.AssignmentId,
                    NavigationalParameters.SelectedQuestion.QuestionId);

                NavigationalParameters.SelectedAnswer.GpsCoordinates =
                    $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";

                await jobService.AddLocation(CurrentPlacemark, LocationInputText);

                await assignmentService.SaveCurrentAnswer(NavigationalParameters.SelectedAnswer);
            }
            else if (NavigationalParameters.MapType == "StartGps")
            {
                NavigationalParameters.CurrentAssignment.StartLocation =
                    $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";

                NavigationalParameters.CurrentAssignment.StreetName =
                    $"{CurrentPlacemark?.Thoroughfare}, {CurrentPlacemark?.PostalCode}";
                await assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
            }
            else if (NavigationalParameters.MapType == "EndGps")
            {
                NavigationalParameters.CurrentAssignment.EndLocation =
                    $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";
                await assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
            }
            else if (NavigationalParameters.MapType == "PlantIssue")
            {
                NavigationalParameters.CurrentPlantIssue.LocationText =
                    $"{CurrentPlacemark?.Thoroughfare} : {CurrentPlacemark?.Location.Latitude}, {CurrentPlacemark?.Location.Longitude}";
            }
            else if (NavigationalParameters.MapType == "BlockageGps")
            {
                // NavigationalParameters.BlockageItem.EndPointId = NavigationalParameters.BlockageItem?.EndPointId ?? 0;
                NavigationalParameters.BlockageItem.LocationGps =
                    $"{CurrentPlacemark?.Thoroughfare} : {CurrentPlacemark?.Location.Latitude}, {CurrentPlacemark?.Location.Longitude}";

                await jobService.AddLocation(CurrentPlacemark, NavigationalParameters.BlockageItem.ImageId.ToString());

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);
            }
            else if (NavigationalParameters.MapType == "BlockageGpsA")
            {
                NavigationalParameters.BlockageItem.PointAGps =
                    $"{CurrentPlacemark?.Thoroughfare} : {CurrentPlacemark?.Location.Latitude}, {CurrentPlacemark?.Location.Longitude}";

                await jobService.AddLocation(CurrentPlacemark, NavigationalParameters.BlockageItem.ImageId.ToString());

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);
            }
            else if (NavigationalParameters.MapType == "BlockageGpsB")
            {
                NavigationalParameters.BlockageItem.PointBGps =
                    $"{CurrentPlacemark?.Thoroughfare} : {CurrentPlacemark?.Location.Latitude}, {CurrentPlacemark?.Location.Longitude}";

                await jobService.AddLocation(CurrentPlacemark, NavigationalParameters.BlockageItem.ImageId.ToString());

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);
            }
            else if (NavigationalParameters.MapType == "EventManagement")
            {
                NavigationalParameters.EventManagement.Location =
                    $"{CurrentPlacemark?.Thoroughfare} : {CurrentPlacemark?.Location.Latitude}, {CurrentPlacemark?.Location.Longitude}";
                await jobService.AddLocation(CurrentPlacemark,
                    NavigationalParameters.EventManagement.Identifier.ToString());
            }
            else if (NavigationalParameters.MapType == "ChamberAudit")
            {
                var streetExsits = jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                    .Any(x => x.StreetName == CurrentPlacemark?.Thoroughfare);

                if (!streetExsits)
                    try
                    {
                        NavigationalParameters.SelectedCabinet.Location =
                            $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";

                        NavigationalParameters.SelectedCabinet.CityFibreRef = LocationInputText;

                        NavigationalParameters.SelectedEndPoint.TownCity = $"{CurrentPlacemark?.Thoroughfare.ToUpper()}";

                        NavigationalParameters.SelectedEndPoint.Postcode = CurrentPlacemark?.PostalCode;

                        NavigationalParameters.SelectedEndPoint.L4Cab = NavigationalParameters.SelectedCabinet?.L4Number;

                        NavigationalParameters.SelectedEndPoint.L5Cab = "Created on pole pre site survey";

                        NavigationalParameters.SelectedEndPoint.latitude =
                            CurrentPlacemark?.Location?.Latitude.ToString();

                        NavigationalParameters.SelectedEndPoint.longitude =
                            CurrentPlacemark?.Location?.Longitude.ToString();

                        NavigationalParameters.SelectedEndPoint.BuildingStandard = "chamber";

                        NavigationalParameters.SelectedEndPoint.Status = "Surveyed";

                        await jobService.AddArea(NavigationalParameters.SelectedCabinet);

                        await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);
                    }
                    catch (Exception ex)
                    {
                        Analytics.TrackEvent(
                            $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

                        var error = $"{ex}Error whilst saving the new street";
                    }
            }
            else if (NavigationalParameters.MapType == "PoleQuestionGps")
            {
                if (!string.IsNullOrEmpty(LocationInputText))
                {
                    if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M)
                    {
                        if (CurrentPlacemark != null)
                        {
                            NavigationalParameters.SelectedAnswer.LocationName =
                                $"{CurrentPlacemark?.Location?.Latitude}, {CurrentPlacemark?.Location?.Longitude}";

                            NavigationalParameters.SelectedCabinet.CityFibreRef =
                                $"{NavigationalParameters.SelectedEndPoint.StreetName}";
                            NavigationalParameters.SelectedCabinet.Location =
                                $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";
                            NavigationalParameters.SelectedCabinet.Existing = "No";
                            NavigationalParameters.SelectedCabinet.LastUpdateTime = DateTime.Now;
                            NavigationalParameters.SelectedCabinet.QuoteId =
                                NavigationalParameters.CurrentSelectedJob.QuoteNumber;
                            NavigationalParameters.SelectedCabinet.CabinetDetails = "Created on pre site pole survey";
                            NavigationalParameters.SelectedCabinet.CityFibreRef =
                                NavigationalParameters.SelectedEndPoint.BuildingName = LocationInputText;

                            NavigationalParameters.SelectedEndPoint.BuildingName = LocationInputText;
                            NavigationalParameters.SelectedEndPoint.Qnumber =
                                Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber);
                            NavigationalParameters.SelectedEndPoint.TownCity = $"{Thoroughfare.ToUpper()}";
                            NavigationalParameters.SelectedEndPoint.Postcode = CurrentPlacemark?.PostalCode;
                            NavigationalParameters.SelectedEndPoint.Blocked = false;
                            NavigationalParameters.SelectedEndPoint.L5Cab = "";
                            NavigationalParameters.SelectedEndPoint.BuildingStandard = "Pole";
                        }
                    }
                    else
                    {
                        if (CurrentPlacemark != null)
                        {
                            NavigationalParameters.SelectedAnswer.LocationName = LocationInputText;

                            NavigationalParameters.SelectedAnswer.GpsCoordinates =
                                $"{CurrentPlacemark?.Location?.Latitude}, {CurrentPlacemark?.Location?.Longitude}";
                        }
                    }

                    await jobService.AddArea(NavigationalParameters.SelectedCabinet);

                    await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);

                    await assignmentService.AddAssignmentsResponse(NavigationalParameters.SelectedAnswer);
                }
                else
                {
                    var error = "Please ensure a pin has been placed on the map";
                }
            }
            else if (NavigationalParameters.MapType == "AssetLocationMap")
            {
                if (!string.IsNullOrEmpty(LocationInputText))
                {
                    if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M)
                    {
                        if (CurrentPlacemark != null)
                        {
                            NavigationalParameters.SelectedAnswer.LocationName =
                                $"{CurrentPlacemark?.Location?.Latitude}, {CurrentPlacemark?.Location?.Longitude}";

                            NavigationalParameters.SelectedCabinet.CityFibreRef =
                                $"{NavigationalParameters.SelectedAsset.StreetName}";
                            NavigationalParameters.SelectedCabinet.Location =
                                $"{NavigationalParameters.SelectedAsset.latitude},{NavigationalParameters.SelectedAsset?.longitude}";
                            NavigationalParameters.SelectedCabinet.Existing = "No";
                            NavigationalParameters.SelectedCabinet.LastUpdateTime = DateTime.Now;
                            NavigationalParameters.SelectedCabinet.QuoteId =
                                NavigationalParameters.CurrentSelectedJob.QuoteNumber;
                            NavigationalParameters.SelectedCabinet.CabinetDetails = "Created on pre site pole survey";
                            NavigationalParameters.SelectedCabinet.CityFibreRef =
                                NavigationalParameters.SelectedAsset.BuildingName = LocationInputText;

                            NavigationalParameters.SelectedAsset.BuildingName = LocationInputText;
                            NavigationalParameters.SelectedAsset.TownCity = $"{Thoroughfare?.ToUpper()}";
                            NavigationalParameters.SelectedAsset.Postcode = CurrentPlacemark?.PostalCode;
                        }
                    }
                    else
                    {
                        if (CurrentPlacemark != null)
                        {
                            NavigationalParameters.SelectedAnswer.LocationName = LocationInputText;

                            NavigationalParameters.SelectedAnswer.GpsCoordinates =
                                $"{CurrentPlacemark?.Location?.Latitude}, {CurrentPlacemark?.Location?.Longitude}";
                        }
                    }

                    if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M)
                        NavigationalParameters.SelectedAnswer.AnswerGiven =
                            NavigationalParameters.SelectedAnswer.GpsCoordinates;

                    await jobService.AddArea(NavigationalParameters.SelectedCabinet);

                    await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);

                    await assignmentService.AddAssignmentsResponse(NavigationalParameters.SelectedAnswer);
                }
                else
                {
                    var error = "Please ensure a pin has been placed on the map";
                }
            }        
            else if (NavigationalParameters.MapType == "addnewstreet")
            {
                var streetExsits = jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                    .Any(x => x.StreetName == CurrentPlacemark?.Thoroughfare);

                if (!streetExsits && CurrentPlacemark != null)
                    try
                    {
                        NavigationalParameters.SelectedEndPoint.Qnumber =
                            Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber);
                        NavigationalParameters.SelectedEndPoint.L5Cab = "Created On Survey";
                        NavigationalParameters.SelectedEndPoint.BuildingStandard = "street";
                        NavigationalParameters.SelectedEndPoint.StreetName = CurrentPlacemark?.Thoroughfare;
                        NavigationalParameters.SelectedEndPoint.TownCity = $"{CurrentPlacemark?.Thoroughfare.ToUpper()}";
                        NavigationalParameters.SelectedEndPoint.Postcode = CurrentPlacemark?.PostalCode;
                        NavigationalParameters.SelectedEndPoint.Blocked = false;
                        NavigationalParameters.SelectedEndPoint.L4Cab = NavigationalParameters.SelectedCabinet?.L4Number;
                        NavigationalParameters.SelectedEndPoint.BuildingName = LocationInputText;
                        NavigationalParameters.SelectedEndPoint.longitude =
                            CurrentPlacemark?.Location?.Longitude.ToString();
                        NavigationalParameters.SelectedEndPoint.latitude =
                            CurrentPlacemark?.Location?.Latitude.ToString();
                        NavigationalParameters.SelectedEndPoint.RemoteTableId = 0;
                        NavigationalParameters.SelectedEndPoint.ClaimId = Guid.NewGuid();

                        await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);
                    }
                    catch (Exception ex)
                    {
                        Analytics.TrackEvent(
                            $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

                        var error = $"{ex}Error whilst saving the new street";
                    }
            }
            else
            {
                if (!string.IsNullOrEmpty(LocationInputText))
                {
                    var streetExsits = jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Any(x => x.StreetName == CurrentPlacemark?.Thoroughfare);

                    if (!streetExsits && CurrentPlacemark != null)
                        try
                        {
                            NavigationalParameters.SelectedCabinet.Location =
                                $"{CurrentPlacemark?.FeatureName}, {CurrentPlacemark?.AdminArea}, {CurrentPlacemark?.PostalCode}";

                            NavigationalParameters.SelectedCabinet.Existing = "No";

                            NavigationalParameters.SelectedCabinet.LastUpdateTime = DateTime.Now;

                            NavigationalParameters.SelectedCabinet.QuoteId =
                                NavigationalParameters.CurrentSelectedJob.QuoteNumber;
                            NavigationalParameters.SelectedCabinet.Location =
                                $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";

                            NavigationalParameters.SelectedEndPoint.Qnumber =
                                Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber);

                            NavigationalParameters.SelectedCabinet.CabinetDetails = "Created on presite";

                            NavigationalParameters.SelectedEndPoint.L5Cab = "Created On Survey";

                            NavigationalParameters.SelectedEndPoint.BuildingStandard = "street";

                            NavigationalParameters.SelectedEndPoint.TownCity =
                                $"{CurrentPlacemark?.Thoroughfare.ToUpper()}";

                            NavigationalParameters.SelectedEndPoint.Postcode = CurrentPlacemark?.PostalCode;

                            NavigationalParameters.SelectedEndPoint.Blocked = false;

                            NavigationalParameters.SelectedEndPoint.L4Cab =
                                NavigationalParameters.SelectedCabinet?.L4Number;

                            NavigationalParameters.SelectedEndPoint.BuildingName = LocationInputText;

                            NavigationalParameters.SelectedAnswer.LocationName =
                                $"{CurrentPlacemark?.Location?.Latitude},{CurrentPlacemark?.Location?.Longitude}";

                            await jobService.AddArea(NavigationalParameters.SelectedCabinet);

                            await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);

                            await assignmentService.AddAssignmentsResponse(NavigationalParameters.SelectedAnswer);
                        }
                        catch (Exception ex)
                        {
                            Analytics.TrackEvent(
                                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                            var error = $"{ex}Error whilst saving the new street";
                        }
                }
                else
                {
                    await Alert("Incomplete",
                        "Please complete all fields by selecting a position on the map and adding a description", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }
}