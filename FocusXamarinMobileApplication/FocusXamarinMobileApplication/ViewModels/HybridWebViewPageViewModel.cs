#region

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Curnow.Biz.What3WordsV3Net;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class HybridWebViewPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly LocalStorage _ls;

    public readonly PhotoSelectionPageViewModel _psvm;

    private readonly W3WClient w3w;

    private string _adminArea;

    private string _countryCode;

    private string _countryName;

    private string _documentUrl;

    private string _featureName;

    public Jobs _jobService;

    private string _locality;

    public WebViewSource _pdfSource = "https://roadworks.org";


    public Pictures _pictureService;

    private Position _position;


    private string _postalCode;

    private string _streetName;

    private string _subAdminArea;

    private string _subLocality;

    private string _subThoroughfare;

    public string _thoroughfare;

    public HybridWebViewPageViewModel()
    {
        w3w = new W3WClient("8EBC5ZGF");
        _pictureService = new Pictures();
        _jobService = new Jobs();
        _ls = new LocalStorage();
        _psvm = new PhotoSelectionPageViewModel();
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

    public ImageSource Image { get; set; }

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

    public string _title { get; set; } = "";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string _url { get; set; }

    public string Url
    {
        get => _url;
        set
        {
            _url = value;
            OnPropertyChanged();
        }
    }

    private bool _screenShot { get; set; } = true;

    public Position CurrentPosition
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged("Position");
        }
    }

    public Thickness Padding { get; set; }

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

    public string LatLon
    {
        get => _latLon;
        set
        {
            _latLon = value;
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

    public bool _isMapMode { get; set; } = true;

    public bool IsMapMode
    {
        get => _isMapMode;
        set
        {
            _isMapMode = value;
            OnPropertyChanged();
        }
    }

    public bool _isDocMode { get; set; } = true;

    public bool IsDocMode
    {
        get => _isDocMode;
        set
        {
            _isDocMode = value;
            OnPropertyChanged();
        }
    }

    public string Address { get; set; }

    public string Description { get; set; }

    public RelayCommand PageLoaded => new(async () =>
    {
        try
        {
            Url = "";

            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToShortDateString();

            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS ||
                NavigationalParameters.AppMode == NavigationalParameters.AppModes.GANGTRAININGDOCS ||
                NavigationalParameters.AppMode == NavigationalParameters.AppModes.COMPANYDOCS ||
                NavigationalParameters.AppMode == NavigationalParameters.AppModes.PROJECTDOCS ||
                NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS ||
                NavigationalParameters.MapType == "Drawing")
            {
                IsMapMode = false;
                IsDocMode = true;

                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var docPath = Path.Combine(documentsPath, "..", "Library",
                    $"{NavigationalParameters.SelectedDocument?.TabletDocumentFolder}/{NavigationalParameters.SelectedDocument?.FileName}");


                if (!string.IsNullOrEmpty(docPath))
                {
                    PdfSource = docPath;

                    Title = $"{NavigationalParameters.SelectedDocument?.DocumentTitle}";
                }
            }
            else
            {
                IsMapMode = true;
                IsDocMode = false;

                Title = $"{NavigationalParameters.SelectedAsset.BuildingStandard} location";
                Url = "OS.html";
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    });

    public RelayCommand SetCurrentPosition => new(async () =>
    {
        try
        {
            var location =
                await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best,
                    new TimeSpan(0, 0, 5)));

            if (location != null)
                NavigationalParameters.NewPosition =
                    CurrentPosition = new Position(location.Latitude, location.Longitude);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            _ = Alert("No Gps Location available at this time", "Error");
            NavBack.Execute(null);
        }
    });

    public RelayCommand StoreLocation => new(async () =>
    {
        try
        {
            if (LocationInputText != "")
            {
                if (CurrentPlacemark == null) SetCurrentPosition.Execute(null);

                SaveStreetToDataBase(CurrentPlacemark);

                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT)
                    NavigationalParameters.CurrentPlantIssue.LocationText =
                        $"{CurrentPlacemark?.Thoroughfare} (Long:{CurrentPlacemark?.Location?.Longitude} - Lat:{CurrentPlacemark?.Location?.Latitude})";

                try
                {
                    switch (NavigationalParameters.SurveyType)
                    {
                        case NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY:
                        case NavigationalParameters.SurveyTypes.BJEASBUILT:
                        case NavigationalParameters.SurveyTypes.CHAMBERASBUILT:
                        case NavigationalParameters.SurveyTypes.DJEASBUILT:
                        case NavigationalParameters.SurveyTypes.DPASBUILT:
                        case NavigationalParameters.SurveyTypes.FJEASBUILT:
                        case NavigationalParameters.SurveyTypes.POLEASBUILT:
                        case NavigationalParameters.SurveyTypes.POLECABLEMEASURE:
                        case NavigationalParameters.SurveyTypes.CHAMBERMEASURE:
                        case NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE:
                        case NavigationalParameters.SurveyTypes.DJEMEASURE:
                        case NavigationalParameters.SurveyTypes.BJEMEASURE:
                        case NavigationalParameters.SurveyTypes.FJEMEASURE:
                        case NavigationalParameters.SurveyTypes.POLEMEASURE:
                        case NavigationalParameters.SurveyTypes.REMEDIAL:
                            NavigationalParameters.MapType = "AssetLocationMap";

                            await NavigateTo(ViewModelLocator.FormsMapPage);
                            break;
                        default:
                            await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                }
            }
            else
            {
                _ = Alert("Please Assign a Name to the pin", "Name Required!!");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();

            _ = Alert("Location failed to save", "Failed");
        }
    });

    public RelayCommand NavBack => new(async () => { NavigateBack(); });

    public RelayCommand ReturnToQuestions => new(async () =>
    {
        await Alert("There is no drawing available at this time", "!");
        //   await NavigateTo(new SurveyQuestions());
    });

    public string _latLon { get; private set; }

    public async Task CaptureScreenShotAsync(Stream imageStream)
    {
        _pictureService = new Pictures();

        ScreenShot = false;

        try
        {
            var Photo = new Pictures4Tablet
            {
                PicturePath = "JobPictures",
                OperativeId = NavigationalParameters.LoggedInUser?.FocusId,
                OperativeRole = NavigationalParameters.LoggedInUser?.Role,
                QNumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber,
                QuestionId = NavigationalParameters.SelectedQuestion?.QuestionId.ToString() ?? "0",
                ResponseId = NavigationalParameters.SelectedAnswer?.Id.ToString() ?? "0",
                SeverPictureId = 0,
                AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId
            };

            if (NavigationalParameters.CurrentAssignment != null)
                Photo.AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId;

            Photo.Image = Photo.ImgToByteArray(imageStream);

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
                Photo.Latitude = CurrentPosition.Latitude.ToString();

                Photo.Longitude = CurrentPosition.Longitude.ToString();
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

            ScreenShot = true;
        }
        finally
        {
            ScreenShot = true;
        }
    }

    //----------------------------------------------------------------------------------
    public async void FindLocationInfo(string longlat)
    {
        var ll = longlat?.Split(',');
        double longitude = 0;
        double latitude = 0;
        double northing = 0;
        double easting = 0;

        longitude = Convert.ToDouble(ll.FirstOrDefault());

        latitude = Convert.ToDouble(ll.LastOrDefault());

        NavigationalParameters.NewPosition = new Position(latitude, longitude);

        if (NavigationalParameters.SelectedEndPoint != null || NavigationalParameters.SelectedAsset != null)
        {
            Title =
                $"{NavigationalParameters.SelectedEndPoint?.StreetName ?? NavigationalParameters.SelectedAsset.StreetName}";

            if (NavigationalParameters.SelectedEndPoint != null)
            {
                NavigationalParameters.SelectedEndPoint.longitude = longitude.ToString();
                NavigationalParameters.SelectedEndPoint.latitude = latitude.ToString();
            }
        }
        else
        {
            Title = "Add Loaction";
        }

        ScreenShot = true;

        LocationInputText = "";

        LatLon = $"Lon:{longitude.ToString()} Lat:{latitude.ToString()}";

        What3Words(latitude, longitude);

        if (!string.IsNullOrEmpty(longlat))
        {
            var a = await Geocoding.GetPlacemarksAsync(latitude, longitude);

            if (a != null && a.ToList().Count > 0)
            {
                CurrentPlacemark = a?.FirstOrDefault();

                if (CurrentPlacemark?.PostalCode != null)
                {
                    Location = CurrentPlacemark?.Location;

                    FeatureName = CurrentPlacemark?.FeatureName ?? "Detail Unavailable";

                    AdminArea = CurrentPlacemark?.AdminArea ?? "";

                    CountryName = CurrentPlacemark?.CountryName ?? "";

                    Locality = CurrentPlacemark?.Locality ?? "";

                    switch (NavigationalParameters.AppMode)
                    {
                        case NavigationalParameters.AppModes.CABLEMEASURE:
                        case NavigationalParameters.AppModes.CHAMBERMEASURE:
                        case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                        case NavigationalParameters.AppModes.BJEMEASURE:
                        case NavigationalParameters.AppModes.FJEMEASURE:
                        case NavigationalParameters.AppModes.DJEMEASURE:
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
                        case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                        case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                        case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                        case NavigationalParameters.AppModes.REMEDIAL:
                            PostalCode = CurrentPlacemark?.PostalCode ?? "";
                            Thoroughfare = CurrentPlacemark?.Thoroughfare ?? "";
                            break;
                    }
                }
            }
        }

        if (NavigationalParameters.SelectedAsset != null)
            if (NavigationalParameters.SelectedAnswer != null && NavigationalParameters.SelectedAnswer.QuestionId == 0.10M)
            {
                NavigationalParameters.SelectedAsset.longitude = longitude.ToString();
                NavigationalParameters.SelectedAsset.latitude = latitude.ToString();

                NavigationalParameters.SelectedAsset.SavedToServer = false;
                await _jobService.AddEndPoint(NavigationalParameters.SelectedAsset);
            }
    }

    public async void SaveStreetToDataBase(Placemark placemark)
    {
        var jobService = new Jobs();
        var assignmentService = new Assignments();

        switch (NavigationalParameters.MapType)
        {
            case "QuestionGps":
                NavigationalParameters.SelectedAnswer = assignmentService.GetCurrentResponse(
                    NavigationalParameters.CurrentAssignment.AssignmentId,
                    NavigationalParameters.SelectedQuestion.QuestionId);

                NavigationalParameters.SelectedAnswer.GpsCoordinates =
                    $"{placemark?.Location?.Latitude},{placemark?.Location?.Longitude}";

                await jobService.AddLocation(placemark, LocationInputText);

                await assignmentService.SaveCurrentAnswer(NavigationalParameters.SelectedAnswer);
                break;
            case "StartGps":
                NavigationalParameters.CurrentAssignment.StartLocation =
                    $"{placemark?.Location?.Latitude},{placemark?.Location?.Longitude}";

                NavigationalParameters.CurrentAssignment.StreetName =
                    $"{placemark?.Thoroughfare}, {placemark?.PostalCode}";
                await assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

                break;
            case "EndGps":
                NavigationalParameters.CurrentAssignment.EndLocation =
                    $"{placemark?.Location?.Latitude},{placemark?.Location?.Longitude}";
                await assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
                break;
            case "PlantIssue":
                NavigationalParameters.CurrentPlantIssue.LocationText =
                    $"{placemark?.Thoroughfare} : {placemark?.Location.Latitude}, {placemark?.Location.Longitude}";
                break;
            case "BlockageGps":
                // NavigationalParameters.BlockageItem.EndPointId = NavigationalParameters.BlockageItem?.EndPointId ?? 0;
                NavigationalParameters.BlockageItem.LocationGps =
                    $"{placemark?.Thoroughfare} : {placemark?.Location.Latitude}, {placemark?.Location.Longitude}";
                await jobService.AddLocation(placemark, NavigationalParameters.BlockageItem.ImageId.ToString());

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);
                break;
            case "BlockageGpsA":
                // NavigationalParameters.BlockageItem.EndPointId = NavigationalParameters.BlockageItem?.EndPointId ?? 0;
                NavigationalParameters.BlockageItem.PointAGps =
                    $"{placemark?.Thoroughfare} : {placemark?.Location.Latitude}, {placemark?.Location.Longitude}";
                await jobService.AddLocation(placemark, NavigationalParameters.BlockageItem.ImageId.ToString());

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);
                break;
            case "BlockageGpsB":
                // NavigationalParameters.BlockageItem.EndPointId = NavigationalParameters.BlockageItem?.EndPointId ?? 0;
                NavigationalParameters.BlockageItem.PointBGps =
                    $"{placemark?.Thoroughfare} : {placemark?.Location.Latitude}, {placemark?.Location.Longitude}";
                await jobService.AddLocation(placemark, NavigationalParameters.BlockageItem.ImageId.ToString());

                await _jobService.AddBlockage(NavigationalParameters.BlockageItem);
                break;
            case "EventManagement":
                NavigationalParameters.EventManagement.Location =
                    $"{placemark?.Thoroughfare} : {placemark?.Location.Latitude}, {placemark?.Location.Longitude}";
                await jobService.AddLocation(placemark, NavigationalParameters.EventManagement.Identifier.ToString());
                break;
            case "ChamberAudit":
                {
                    var streetExsits = jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Any(x => x.StreetName == placemark?.Thoroughfare);

                    if (!streetExsits)
                        try
                        {
                            NavigationalParameters.SelectedCabinet.Location =
                                $"{placemark?.Location?.Latitude},{placemark?.Location?.Longitude}";
                            NavigationalParameters.SelectedEndPoint.BuildingName = LocationInputText;
                            NavigationalParameters.SelectedEndPoint.TownCity = $"{placemark?.Thoroughfare.ToUpper()}";
                            NavigationalParameters.SelectedEndPoint.Postcode = placemark?.PostalCode;
                            NavigationalParameters.SelectedEndPoint.L4Cab = NavigationalParameters.SelectedCabinet?.L4Number;
                            NavigationalParameters.SelectedEndPoint.L5Cab = "Created on pole pre site survey";
                            NavigationalParameters.SelectedEndPoint.BuildingStandard = "Pole";

                            await jobService.AddArea(NavigationalParameters.SelectedCabinet);
                            await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);
                        }
                        catch (Exception ex)
                        {
                            var error = $"{ex}Error whilst saving the new street";
                        }

                    break;
                }
            case "PoleQuestionGps":
            case "AssetLocationMap":
                {
                    if (!string.IsNullOrEmpty(LocationInputText))
                    {
                        if (NavigationalParameters.SelectedAnswer.QuestionId == 0.10M)
                        {
                            NavigationalParameters.SelectedAsset.BuildingName = LocationInputText;

                            if (placemark != null)
                            {
                                // NavigationalParameters.SelectedCabinet.Location = $"{placemark?.Location?.Latitude}, {placemark?.Location?.Longitude}";
                                // NavigationalParameters.SelectedCabinet.UcNc = $"{placemark?.Location?.Latitude}, {placemark?.Location?.Longitude}";
                                NavigationalParameters.SelectedAnswer.LocationName =
                                    $"{NavigationalParameters.SelectedEndPoint?.latitude ?? NavigationalParameters.SelectedAsset.latitude}, {NavigationalParameters.SelectedEndPoint?.longitude ?? NavigationalParameters.SelectedAsset.longitude}";
                                NavigationalParameters.SelectedAsset.TownCity = $"{Thoroughfare?.ToUpper()}";
                                NavigationalParameters.SelectedAsset.Postcode = placemark?.PostalCode;
                                //  NavigationalParameters.SelectedAsset.longitude = placemark?.Location?.Longitude.ToString();
                                //  NavigationalParameters.SelectedAsset.latitude = placemark?.Location?.Latitude.ToString();
                            }


                            NavigationalParameters.SelectedAnswer.AnswerGiven = $"{ NavigationalParameters.SelectedAnswer.LocationName} - {LocationInputText}";
                            //  NavigationalParameters.SelectedCabinet.Existing = "No";
                            //   NavigationalParameters.SelectedCabinet.LastUpdateTime = DateTime.Now;
                            // NavigationalParameters.SelectedCabinet.QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber;
                            //avigationalParameters.SelectedCabinet.CabinetDetails = "Created on pre site pole survey";

                            NavigationalParameters.SelectedAsset.Qnumber =
                                Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber);

                            NavigationalParameters.SelectedAsset.Blocked = false;

                            NavigationalParameters.SelectedAsset.L5Cab = "updated on pre site pole survey";

                            // NavigationalParameters.SelectedAsset.BuildingStandard = "Pole";
                        }

                        if (placemark != null)
                            NavigationalParameters.SelectedAnswer.GpsCoordinates =
                                $"{placemark?.Location?.Latitude}, {placemark?.Location?.Longitude}";

                        // await jobService.AddArea(NavigationalParameters.SelectedCabinet);
                        await jobService.AddEndPoint(NavigationalParameters.SelectedAsset);

                        await assignmentService.AddAssignmentsResponse(NavigationalParameters.SelectedAnswer);
                    }
                    else
                    {
                        var error = "Please ensure a pin has been placed on the map";
                    }

                    break;
                }
            default:
                {
                    if (!string.IsNullOrEmpty(LocationInputText))
                    {
                        var streetExsits = jobService.GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                            .Any(x => x.StreetName == placemark?.Thoroughfare);

                        if (!streetExsits)
                            try
                            {
                                NavigationalParameters.SelectedCabinet.Location =
                                    $"{placemark?.FeatureName}, {placemark?.AdminArea}, {placemark?.PostalCode}";
                                NavigationalParameters.SelectedCabinet.Existing = "No";
                                NavigationalParameters.SelectedCabinet.LastUpdateTime = DateTime.Now;
                                NavigationalParameters.SelectedCabinet.QuoteId =
                                    NavigationalParameters.CurrentSelectedJob.QuoteNumber;

                                NavigationalParameters.SelectedEndPoint.Qnumber =
                                    Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber);

                                NavigationalParameters.SelectedCabinet.CabinetDetails = "Created on presite";
                                NavigationalParameters.SelectedEndPoint.L5Cab = "Created On Survey";
                                NavigationalParameters.SelectedEndPoint.BuildingStandard = "street";

                                NavigationalParameters.SelectedEndPoint.TownCity = $"{placemark?.Thoroughfare.ToUpper()}";
                                NavigationalParameters.SelectedEndPoint.Postcode = placemark?.PostalCode;

                                NavigationalParameters.SelectedEndPoint.Blocked = false;
                                NavigationalParameters.SelectedEndPoint.L4Cab =
                                    NavigationalParameters.SelectedCabinet?.L4Number;
                                NavigationalParameters.SelectedEndPoint.BuildingName = LocationInputText;
                                NavigationalParameters.SelectedAnswer.LocationName =
                                    $"{placemark?.Location?.Latitude},{placemark?.Location?.Longitude}";

                                await jobService.AddArea(NavigationalParameters.SelectedCabinet);
                                await jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);
                                await assignmentService.AddAssignmentsResponse(NavigationalParameters.SelectedAnswer);
                            }
                            catch (Exception ex)
                            {
                                var error = $"{ex}Error whilst saving the new street";
                            }
                    }
                    else
                    {
                        await Alert("Incomplete",
                            "Please complete all fields by selecting a position on the map and adding a description", "Ok");
                    }

                    break;
                }
        }
    }

    public void What3Words(double latitude, double longitude)
    {
        try
        {
            var response = w3w.ConvertTo3WA(latitude, longitude);

            LocationInputText = $"{response.words}";
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    public string ReturnCurrentLocation()
    {
        return $"LngLat({NavigationalParameters.NewPosition.Longitude},{NavigationalParameters.NewPosition.Latitude})";
    }

    public async Task<bool> GetCurrentPosition()
    {
        return true;
    }
}