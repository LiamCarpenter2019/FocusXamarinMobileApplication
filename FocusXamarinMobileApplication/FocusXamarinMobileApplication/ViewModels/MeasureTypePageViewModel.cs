using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class MeasureTypePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;
    private JobData4Tablet _jobData;


    public MeasureTypePageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();

        Title = "Measure Type";
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
    }

    public string _projectDate { get; set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged("ProjectDate");
        }
    }

    public string _projectName { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged("ProjectName");
        }
    }


    public string _title { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged("Title");
        }
    }

    public string _submitButtonText { get; set; }

    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
            OnPropertyChanged("SubmitButtonText");
        }
    }

    public bool _showMeasureButtons { get; set; }

    public bool ShowMeasureButtons
    {
        get => _showMeasureButtons;
        set
        {
            _showMeasureButtons = value;
            OnPropertyChanged("ShowMeasureButtons");
        }
    }

    public bool _showSection1 { get; set; }

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged("ShowSection1");
        }
    }

    public bool _showSection2 { get; set; }

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged("ShowSection2");
        }
    }

    public bool _showSection3 { get; set; }

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged("ShowSection3");
        }
    }

    public bool _showSection4 { get; set; }

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged("ShowSection4");
        }
    }

    public string _measureCategory { get; set; }

    public string MeasureCategory
    {
        get => _measureCategory;
        set
        {
            _measureCategory = value;
            OnPropertyChanged("MeasureCategory");
        }
    }


    public List<ProjectWorks> _currentWorksList { get; set; }

    public List<ProjectWorks> CurrentWorksList
    {
        get => _currentWorksList;
        set
        {
            _currentWorksList = value;
            OnPropertyChanged("CurrentWorksList");
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

        CurrentWorksList = _assignmentService
            .GetRates(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract")
            ?.Where(x => x.AssignmentId == Guid.Empty
                         && x.WorksIdForDelete == Guid.Empty)
            ?.Select(i => new ProjectWorks
            {
                RemoteTableId = i.RemoteTableId,
                BaseUnit = i?.BaseUnit,
                Description = i?.Description,
                Header = i?.Header,
                QuoteId = i.QuoteId,
                Category = i?.Category.ToUpper()
            }).ToList();


        var leadInRates = _jobService.GetAllRates()?.Where(x => x.Category.ToLower() == "leadin")?.Select(i =>
            new ProjectWorks
            {
                RemoteTableId = i.RemoteTableId,
                BaseUnit = i?.BaseUnit,
                Description = i?.WorkDescription,
                Header = i?.WorkHeader,
                QuoteId = i.BaseContractId,
                Category = i?.Category.ToUpper()
            }).ToList();

        CurrentWorksList.AddRange(leadInRates);

        foreach (var item in CurrentWorksList)
            if (NavigationalParameters.ProjectWorksList.All(x => x.Header != item.Header))
                NavigationalParameters.ProjectWorksList.Add(item);

        if (NavigationalParameters.CurrentSelectedJob.ClientName.Contains("Conne"))
        {
            ShowMeasureButtons = false;
        }
        else
        {
            ShowMeasureButtons = true;
        }
    });

    public RelayCommand ButtonACommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Civils";
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.CIVILSMEASURE;
        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == NavigationalParameters.ClaimType.ToLower()).ToList();

        await NavigateTo(ViewModelLocator.MeasureListPage);
    });

    public RelayCommand ButtonBCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Reinstator";
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.REINSTATEMENTMEASURE;
        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList.Where(x =>
                // x.Category == "Cabling" &&
                (x.Header.StartsWith("R", StringComparison.CurrentCulture)
                 && x.Header.ToLower() != "gr1") || x.Category.ToLower() == NavigationalParameters.ClaimType.ToLower())
            .ToList();


        await NavigateTo(ViewModelLocator.MeasureListPage);
    });

    public RelayCommand ButtonCCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Cabling";
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.CABLEMEASURE;
        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList.Where(x =>
                x.Category.ToLower() == NavigationalParameters.ClaimType.ToLower() ||
                (x.Category.ToLower() == NavigationalParameters.ClaimType.ToLower() &&
                 !x.Header.StartsWith("R",
                     StringComparison.CurrentCulture)) || x.Category.ToLower().Contains("cable"))
            .ToList();

        await NavigateTo(ViewModelLocator.MeasureListPage);
    });

    public RelayCommand ButtonDCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Site Support";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITESUPPORTMEASURE;
        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList.Where(x =>
            (x.Category.ToLower() == NavigationalParameters.ClaimType.ToLower() && x.Header.ToLower() == "gr1") ||
            x.Category.ToLower() == "site support").ToList();

        await NavigateTo(ViewModelLocator.MeasureListPage);
    });

    public RelayCommand ButtonECommand => new(async () =>
    {
        var EndPointsAvailable = _jobService.CheckEndpoints(NavigationalParameters.CurrentSelectedJob);

        if (EndPointsAvailable)
        {
            NavigationalParameters.ClaimType = MeasureCategory = "LeadIn";
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.LEADINMEASURE;
            NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
                .Where(x => x.Category.ToLower() == NavigationalParameters.ClaimType.ToLower()).ToList();

            await NavigateTo(ViewModelLocator.MeasureListPage);
        }
        else
        {
            await Alert("No enpoints available for the lead in", "");
        }
    });

    //----------------

    public RelayCommand ButtonFCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Pole";

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.POLEMEASURE;

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.POLEMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "pole").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonGCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Duct";

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DUCTMEASURE;

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.DUCTMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "duct").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonHCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Distribution Point";

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE;

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "distribution point").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonICommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Chamber";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.CHAMBERMEASURE;

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.CHAMBERMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "chamber").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonJCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "UGCRP";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.UGCRPMEASURE;

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.UGCRPMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "ugcrp").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonKCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "Overhead Cable";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.POLECABLEMEASURE;

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.POLECABLEMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "overhead cable").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonLCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "DJEMEASURE";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.DJEMEASURE;

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DJEMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "DJEMEASURE").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonMCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "BJEMEASURE";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.BJEMEASURE;

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.BJEMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "BJEMEASURE").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand ButtonNCommand => new(async () =>
    {
        NavigationalParameters.ClaimType = MeasureCategory = "FJEMEASURE";

        NavigationalParameters.AppMode = NavigationalParameters.AppModes.FJEMEASURE;

        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.FJEMEASURE;

        NavigationalParameters.CurrentWorksList = NavigationalParameters.ProjectWorksList
            .Where(x => x.Category.ToLower() == "FJEMEASURE").ToList();

        if (NavigationalParameters.CurrentWorksList.Any())
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await Alert("No work items available; Plesase contact support!", "");
    });

    public RelayCommand BackCommand => new(async () =>
    {
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });
}