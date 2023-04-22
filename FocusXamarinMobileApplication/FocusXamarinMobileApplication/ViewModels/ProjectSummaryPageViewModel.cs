#region

using System;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;
using Device = Xamarin.Forms.Device;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class ProjectSummaryPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private RelayCommand<string> _openMapPage;

    private RelayCommand<string> _screenLoaded;

    public ProjectSummaryPageViewModel()
    {
        _jobService = new Jobs();
    }

    public ProjectSummary _projectSummary { get; set; }

    public ProjectSummary ProjectSummary
    {
        get => _projectSummary;
        set
        {
            _projectSummary = value;
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

    public string _userName { get; set; }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }

    public string _hospitalInfo { get; set; }

    public string HospitalInfo
    {
        get => _hospitalInfo;
        set
        {
            _hospitalInfo = value;
            OnPropertyChanged();
        }
    }

    public string _date { get; set; }

    public string Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand<string> ScreenLoaded
    {
        get
        {
            return _screenLoaded ??= new RelayCommand<string>(e =>
            {
                ProjectSummary = _jobService.GetProjectSummary(NavigationalParameters.CurrentSelectedJob);
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
                Date = NavigationalParameters.CurrentSelectedJob?.JobDate.ToShortDateString();
                UserName = NavigationalParameters.LoggedInUser.FullName;

                HospitalInfo = "";

                foreach (var info in ProjectSummary.EmergencyDetails.Split(','))
                    if (info.Contains(":"))
                    {
                        var hospital = info.Split(':');

                        HospitalInfo += $"{hospital[1]}\n";
                    }
                    else
                    {
                        HospitalInfo += $"{info}\n";
                    }
            });
        }
    }

    public RelayCommand Back => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;
        //NavigationalParameters.ReturnPage = Locator.ProjectSummaryViewModelKey;

        NavigateBack();
        // _navigationService.NavigateTo(Locator.MenuPageViewModelKey);
    });

    public RelayCommand<string> OpenMapPage
    {
        get
        {
            return _openMapPage ??= new RelayCommand<string>(e =>
            {
                try
                {
                    var address = "";
                    //var location = new Location(53.802893, -1.552579);
                    //var placemarks = await Geocoding.GetPlacemarksAsync(53.802893, -1.552579);

                    // ProjectSummary.EmergencyDetails = $"United Kingdom, GB, Great George St, Leeds, LS1 3EX";

                    NavigationalParameters.PsEmergencyDetails =
                        ProjectSummary.EmergencyDetails.Replace("Name:", "");

                    foreach (var item in NavigationalParameters.PsEmergencyDetails.Split(','))
                        address = address + $"{item}+";

                    address = address.TrimEnd('+');
                    address = address.Replace(" ", "");
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            //https://developer.apple.com/library/ios/featuredarticles/iPhoneURLScheme_Reference/MapLinks/MapLinks.html
                            //http://maps.apple.com/?q=394+Pacific+Ave+San+Francisco+CA
                            //Device.OpenUri(new Uri("http://maps.apple.com/?q={"+NavigationalParameters.PSEmergencyDetails+"}"));

                            Device.OpenUri(new Uri($"http://maps.apple.com/?q={address}"));
                            break;
                        case Device.Android:
                            // opens the Maps app directly
                            //"394+Pacific+Ave+San+Francisco+CA"
                            Device.OpenUri(new Uri($"geo:0,0?q={address}"));
                            break;
                        case Device.UWP:
                            //394 Pacific Ave San Francisco CA
                            Device.OpenUri(new Uri($"bingmaps:?where={address}"));
                            break;
                    }

                    //NavigationalParameters.MapLaunchOptions = new MapLaunchOptions
                    //{
                    //     NavigationMode = NavigationMode.Driving
                    //};

                    //var position = await SimpleStaticHelpers.GetCurrentPosition(10);
                    //await Map.OpenAsync(location, options);

                    //await Map.OpenAsync(placemark, options);

                    //NavigationalParameters.NavigationService = _navigationService;
                    //NavigationalParameters.PassedInfo = new FormsMapPage();
                    //_navigationService.NavigateTo(Locator.FormsViewKey, Locator.MainListPageViewModelKey);
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
                        $"the app has crashed and has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

                    var error = ex.ToString();
                }
            });
        }
    }
}