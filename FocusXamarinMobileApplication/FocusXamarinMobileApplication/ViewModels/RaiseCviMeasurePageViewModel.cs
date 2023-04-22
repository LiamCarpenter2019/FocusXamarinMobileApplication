using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class RaiseCviMeasurePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Assignments _assignmentService;
    public readonly Jobs _jobService;
    public readonly PhotoSelectionPageViewModel _psvm;
    public readonly Users _userService;

    private List<ProjectWorks> _allCviRates = new();

    private Cvi _currentCvi;

    private List<ProjectWorks> _cviRates;

    private string _filterText;

    private ProjectWorks _selectedRate;

    public RaiseCviMeasurePageViewModel()
    {
        _assignmentService = new Assignments();
        _userService = new Users();
        _jobService = new Jobs();
        _psvm = new PhotoSelectionPageViewModel();

        ClearUnusedRates();
    }


    public string _projectDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
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

    public bool _showSection1 { get; set; }

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; }

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection3 { get; set; }

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection4 { get; set; }

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged();
        }
    }

    public List<ProjectWorks> CviRates
    {
        get => _cviRates;
        set
        {
            _cviRates = value;
            OnPropertyChanged();
        }
    }

    public List<ProjectWorks> AllCviRates
    {
        get => _allCviRates;
        set
        {
            _allCviRates = value;
            OnPropertyChanged();
        }
    }

    public ProjectWorks SelectedRate
    {
        get => _selectedRate;
        set
        {
            _selectedRate = value;
            OnPropertyChanged();
        }
    }

    public Cvi CurrentCvi
    {
        get => _currentCvi;
        set
        {
            _currentCvi = value;
            OnPropertyChanged();
        }
    }

    public string FilterText
    {
        get => _filterText;
        set
        {
            _filterText = value;

            UpdateCviRates(_filterText);
            OnPropertyChanged();
        }
    }

    public ImageSource _cameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged();
        }
    }

    public bool _showUpload { get; set; }

    public bool ShowUpload
    {
        get => _showUpload;
        set
        {
            _showUpload = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowUpload = true;
        CurrentCvi = NavigationalParameters.CurrentCvi;

        var cviRates = _assignmentService
            .GetRatesCvi(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract").ToList();

        NavigationalParameters.T6 = _assignmentService
            .GetRates(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract")
            .Where(x =>
                x.Identifier == Guid.Empty).ToList();

        if (cviRates.Any()) NavigationalParameters.T6.AddRange(cviRates);

        try
        {
            AllCviRates = NavigationalParameters.T6?.GroupBy(x => x.Header)?
                .Select(i => new ProjectWorks
                {
                    //DfeId = i.FirstOrDefault()?.DfeId,
                    Identifier = CurrentCvi.CviId,
                    BaseUnit = i.FirstOrDefault()?.BaseUnit,
                    Description = i.FirstOrDefault()?.Description,
                    Header = i.FirstOrDefault()?.Header,
                    QuoteId = i.FirstOrDefault().QuoteId,
                    Qty = i.Sum(x => Convert.ToDecimal(x.Qty)).ToString(),
                    AssignmentId = i.FirstOrDefault().AssignmentId,
                    Category = "CVI"
                }).ToList();
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand SelectedRateCommand => new(async () =>
    {
        NavigationalParameters.SelectedWorkItem = SelectedRate;

        await NavigateTo(ViewModelLocator.InputMeasurePage);
    });


    public RelayCommand Cancel => new(async () =>
    {
        var confirmCancel = await Alert("Warnining", "Do you want to delete the entire cvi record", "Yes", "No");

        if (confirmCancel)
        {
            _assignmentService.DeleteCvi(NavigationalParameters.CurrentAssignment);

            NavigationalParameters.CurrentAssignment = null;
            NavigationalParameters.CurrentCvi = null;

            NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGSELECTION;
            await NavigateTo(ViewModelLocator.GangSelectPage);
            ;
        }
    });

    public RelayCommand Submit => new(async () =>
    {
        var confirmSubmit =
            await Alert(
                "Please confirm all details are correct before procceding to the next screen as as the information submitted cannot be updated after.",
                "Confirmation", "Confirm", "Cancel");

        if (confirmSubmit)
        {
            NavigationalParameters.CurrentCvi = CurrentCvi;

            NavigationalParameters.CurrentAssignment.Cvi.Add(NavigationalParameters.CurrentCvi);

            await _assignmentService.SaveCvi(NavigationalParameters.CurrentAssignment);

            await NavigateTo(ViewModelLocator.RaiseCviDetailsPage);
        }
    });

    public RelayCommand TakePhoto => new(async () =>
    {
        NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.CVI;

        _psvm.TakePhoto.Execute(null);
        // await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand Back => new(async () => { NavigateBack(); });


    private async void ClearUnusedRates()
    {
        var _pictureService = new Pictures();
        var cviRates = _assignmentService
            .GetRatesCvi(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract")
            .Where(x => x.RemoteTableId == 0).ToList();

        foreach (var rate in cviRates) await _assignmentService.RemoveCviRates(rate.Identifier);

        var assignmentsToDelete = _assignmentService.GenerateAssignments2Save("CVI");

        foreach (var assignment in assignmentsToDelete)
        {
            _assignmentService.DeleteCvi(assignment);
            var picsaToDelete = _pictureService.GetAllPictures().Where(x => x.AssignmentId == assignment.AssignmentId)
                .ToList();
            foreach (var pic in picsaToDelete) await _pictureService.DeleteJobPicture(pic);
        }
    }

    private void UpdateCviRates(string filterText)
    {
        AllCviRates = NavigationalParameters.T6?.Where(x => x.Description.Contains(filterText)).GroupBy(x => x.Header)?
            .Select(i => new ProjectWorks
            {
                //DfeId = i.FirstOrDefault()?.DfeId,
                Identifier = CurrentCvi.CviId,
                BaseUnit = i.FirstOrDefault()?.BaseUnit,
                Description = i.FirstOrDefault()?.Description,
                Header = i.FirstOrDefault()?.Header,
                QuoteId = i.FirstOrDefault().QuoteId,
                Qty = i.Sum(x => Convert.ToDecimal(x.Qty)).ToString(),
                AssignmentId = i.FirstOrDefault().AssignmentId,
                Category = "CVI"
            }).ToList();
    }
}