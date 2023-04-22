using System;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class ServicesCrossedPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private RelayCommand<string> _screenLoadedCommand4CrossedUtilities;
    private RelayCommand<string> _submit;

    public ServicesCrossedPageViewModel()
    {
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToShortDateString();
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        Title = "Services Crossed";
        _jobService = new Jobs();
    }


    public string _projectDate { get; set; } =
        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    public string _title { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string _submitButtonText { get; set; }

    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection1 { get; set; } = true;

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; } = true;

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection3 { get; set; } = true;

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection4 { get; set; } = true;

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged();
        }
    }


    public bool _noServicesCrossed { get; set; }

    public bool NoServicesCrossed
    {
        get => _noServicesCrossed;
        set
        {
            _noServicesCrossed = value;
            OnPropertyChanged();
        }
    }

    public bool _inputVerified { get; set; }

    public bool InputVerified
    {
        get => _inputVerified;
        set
        {
            _inputVerified = value;
            OnPropertyChanged();
        }
    }

    public ServicesCrossed4Tablet _crossedUtilities { get; set; }

    public ServicesCrossed4Tablet CrossedUtilities
    {
        get => _crossedUtilities;
        set
        {
            _crossedUtilities = value;
            OnPropertyChanged();
        }
    }

    public string _electricCrossed { get; set; }

    public string ElectricCrossed
    {
        get => _electricCrossed;
        set
        {
            _electricCrossed = value;
            OnPropertyChanged();
        }
    }

    public string _gasCrossed { get; set; }

    public string GasCrossed
    {
        get => _gasCrossed;
        set
        {
            _gasCrossed = value;
            OnPropertyChanged();
        }
    }

    public string _streetLightCrossed { get; set; }

    public string StreetLightCrossed
    {
        get => _streetLightCrossed;
        set
        {
            _streetLightCrossed = value;
            OnPropertyChanged();
        }
    }

    public string _telecomsCrossed { get; set; }

    public string TelecomsCrossed
    {
        get => _telecomsCrossed;
        set
        {
            _telecomsCrossed = value;
            OnPropertyChanged();
        }
    }

    public string _waterCrossed { get; set; }

    public string WaterCrossed
    {
        get => _waterCrossed;
        set
        {
            _waterCrossed = value;
            OnPropertyChanged();
        }
    }

    public string _other { get; set; }

    public string Other
    {
        get => _other;
        set
        {
            _other = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand<string>
        ScreenLoadedCommand4CrossedUtilities
    {
        get
        {
            return _screenLoadedCommand4CrossedUtilities ??= new RelayCommand<string>(e =>
            {
                // NavPassedInfo = e;

                // CurrentSelectedJob = NavigationalParameters.CurrentSelectedJob;
                CrossedUtilities = _jobService.GetCrossedUtilities(NavigationalParameters.CurrentSelectedJob);

                if (CrossedUtilities == null) return;
                CrossedUtilities.ContractReference = NavigationalParameters.CurrentSelectedJob.ContractReference;

                if (string.IsNullOrEmpty(CrossedUtilities.ServicesCrossedData1))
                {
                    ElectricCrossed = "";
                    GasCrossed = "";
                    StreetLightCrossed = "";
                    TelecomsCrossed = "";
                    WaterCrossed = "";
                    Other = "";
                    NoServicesCrossed = false;
                    return;
                }

                if (CrossedUtilities.ServicesCrossedData1 == "NO Services Crossed")
                {
                    ElectricCrossed = "0";
                    GasCrossed = "0";
                    StreetLightCrossed = "0";
                    TelecomsCrossed = "0";
                    WaterCrossed = "0";
                    Other = "0";
                    NoServicesCrossed = false;
                    return;
                }

                foreach (var service in CrossedUtilities.ServicesCrossedData1.Split('^').ToList())
                    if (service.Contains("Electric"))
                        ElectricCrossed = service.Split('?').First();
                    else if (service.Contains("Gas"))
                        GasCrossed = service.Split('?').First();
                    else if (service.Contains("Street"))
                        StreetLightCrossed = service.Split('?').First();
                    else if (service.Contains("Telecomms"))
                        TelecomsCrossed = service.Split('?').First();
                    else if (service.Contains("Water"))
                        WaterCrossed = service.Split('?').First();
                    else if (service.Contains("Other"))
                        Other = service.Split('?').First();
                    else if (service.Contains("Services")) NoServicesCrossed = true;
            });
        }
    }

    public RelayCommand Cancel => new(async () =>
    {
        var crossedUtilitiesData = "";

        crossedUtilitiesData += "NO Services Crossed";
        ElectricCrossed = "0";
        GasCrossed = "0";
        StreetLightCrossed = "0";
        TelecomsCrossed = "0";
        WaterCrossed = "0";
        Other = "0";
        InputVerified = true;

        if (InputVerified)
        {
            CrossedUtilities.ServicesCrossedData1 = crossedUtilitiesData;
            CrossedUtilities.PostedDate = NavigationalParameters.CurrentSelectedJob.JobDate;
            CrossedUtilities.GangLeaderId =
                NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString();
            CrossedUtilities.GangLeaderName = NavigationalParameters.CurrentSelectedJob.GangLeaderName;

            NoServicesCrossed = false;

            if (!string.IsNullOrEmpty(crossedUtilitiesData))
                try
                {
                    _jobService.AddCrossedUtilities(CrossedUtilities);

                    //  NavigationalParameters.NavigationParameter =
                    // new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                    //  NavPassedInfo.Item1, NavPassedInfo.Item2, NavigationalParameters.CurrentSelectedJob);
                    //_navigationService.NavigateTo(Locator.MenuPageViewModelKey,
                    //    Locator.ServicesCrossedViewModelKey);
                    //NavigationalParameters.ReturnPage = Locator.ServicesCrossedViewModelKey;

                    // NavigationalParameters.NavigationService = _navigationService;
                    //NavigationalParameters.ReturnPage = Locator.MenuPageViewModelKey;
                    // NavigationalParameters.PassedInfo = new MenuPage();
                    //   _navigationService.NavigateTo(Locator.FormsViewKey, Locator.MenuPageViewModelKey);
                    await NavigateTo(ViewModelLocator.MenuSelectionPage);
                }
                catch
                {
                    await Alert(
                        "Failed To Save Utilities Crossed, Please check and try again",
                        "Error!");
                }
            else
                await Alert(
                    "Please input any services crosses", "Error!");
        }
        else
        {
            await Alert(
                "Incorrect format please ensure only numbers are submited within the selectecd fields",
                "Error!");
        }
    });


    public RelayCommand<string> Submit
    {
        get
        {
            return _submit ??= new RelayCommand<string>(async e =>
            {
                var crossedUtilitiesData = "";


                InputVerified = true;

                if (ElectricCrossed != "")
                    try
                    {
                        var result = Convert.ToInt32(ElectricCrossed);
                        crossedUtilitiesData += $"{ElectricCrossed}?HV Electric^";
                    }
                    catch
                    {
                        InputVerified = false;
                        ElectricCrossed = "";
                    }
                else
                    ElectricCrossed = "0";

                if (GasCrossed != "")
                    try
                    {
                        Convert.ToInt32(GasCrossed);
                        crossedUtilitiesData += $"{GasCrossed}?Gas^";
                    }
                    catch
                    {
                        InputVerified = false;
                        GasCrossed = "";
                    }
                else
                    GasCrossed = "0";

                if (StreetLightCrossed != "")
                    try
                    {
                        Convert.ToInt32(StreetLightCrossed);
                        crossedUtilitiesData += $"{StreetLightCrossed}?Street Lighting^";
                    }
                    catch
                    {
                        InputVerified = false;
                        StreetLightCrossed = "";
                    }
                else
                    StreetLightCrossed = "0";

                if (TelecomsCrossed != "")
                    try
                    {
                        Convert.ToInt32(TelecomsCrossed);
                        crossedUtilitiesData += $"{TelecomsCrossed}?Telecomms^";
                    }
                    catch
                    {
                        InputVerified = false;
                        TelecomsCrossed = "";
                    }
                else
                    TelecomsCrossed = "0";

                if (WaterCrossed != "")
                    try
                    {
                        Convert.ToInt32(WaterCrossed);
                        crossedUtilitiesData += $"{WaterCrossed}?Water^";
                    }
                    catch
                    {
                        InputVerified = false;
                        WaterCrossed = "";
                    }
                else
                    WaterCrossed = "0";

                if (Other != "")
                    try
                    {
                        Convert.ToInt32(Other);
                        crossedUtilitiesData += $"{Other}?Other^";
                    }
                    catch
                    {
                        InputVerified = false;
                        Other = "";
                    }
                else
                    Other = "0";


                if (InputVerified)
                {
                    CrossedUtilities.ServicesCrossedData1 = crossedUtilitiesData;
                    CrossedUtilities.PostedDate = NavigationalParameters.CurrentSelectedJob.JobDate;
                    CrossedUtilities.GangLeaderId =
                        NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString();
                    CrossedUtilities.GangLeaderName = NavigationalParameters.CurrentSelectedJob.GangLeaderName;

                    NoServicesCrossed = false;

                    if (!string.IsNullOrEmpty(crossedUtilitiesData))
                        try
                        {
                            _jobService.AddCrossedUtilities(CrossedUtilities);

                            //  NavigationalParameters.NavigationParameter =
                            // new Tuple<Person, List<Models.Assignment>, JobData4Tablet>(
                            //  NavPassedInfo.Item1, NavPassedInfo.Item2, NavigationalParameters.CurrentSelectedJob);
                            //_navigationService.NavigateTo(Locator.MenuPageViewModelKey,
                            //    Locator.ServicesCrossedViewModelKey);
                            //NavigationalParameters.ReturnPage = Locator.ServicesCrossedViewModelKey;

                            //  NavigationalParameters.NavigationService = _navigationService;
                            //NavigationalParameters.ReturnPage = Locator.MenuPageViewModelKey;
                            //  NavigationalParameters.PassedInfo = new MenuPage();
                            //   _navigationService.NavigateTo(Locator.FormsViewKey, Locator.MenuPageViewModelKey);
                            await NavigateTo(ViewModelLocator.MenuSelectionPage);
                        }
                        catch
                        {
                            await Alert(
                                "Failed To Save Utilities Crossed, Please check and try again",
                                "Error!");
                        }
                    else
                        await Alert(
                            "Please input any services crosses", "Error!");
                }
                else
                {
                    await Alert(
                        "Incorrect format please ensure only numbers are submited within the selectecd fields",
                        "Error!");
                }
            });
        }
    }

    public RelayCommand Back => new(async () => { NavigateBack(); });
}