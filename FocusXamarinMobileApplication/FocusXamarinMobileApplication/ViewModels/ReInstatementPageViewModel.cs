namespace FocusXamarinMobileApplication.ViewModels;

public class ReInstatementPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public Assignments _assignmentService;
    public Jobs _jobService;


    public ReInstatementPageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
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


    public string _length { get; set; } = "";
    public string _width { get; set; } = "";
    public string _depth { get; set; } = "";
    public string _selectedMaterialSize { get; set; }
    public ObservableCollection<ReinstatementMeasure> _currentReinstatementMeasures { get; set; }
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


    public bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
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


    public bool _showSection5 { get; set; }

    public bool ShowSection5
    {
        get => _showSection5;
        set
        {
            _showSection5 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection6 { get; set; }

    public bool ShowSection6
    {
        get => _showSection6;
        set
        {
            _showSection6 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection7 { get; set; } = true;

    public bool ShowSection7
    {
        get => _showSection7;
        set
        {
            _showSection7 = value;
            OnPropertyChanged();
        }
    }

    public string _selectedReinstatement { get; set; }

    public string SelectedReinstatement
    {
        get => _selectedReinstatement;
        set
        {
            _selectedReinstatement = value;
            OnPropertyChanged();
        }
    }

    public string SelectedMaterialSize
    {
        get => _selectedMaterialSize;
        set
        {
            _selectedMaterialSize = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> _reinstatementMaterials { get; set; }

    public ObservableCollection<string> ReinstatementMaterials
    {
        get => _reinstatementMaterials;
        set
        {
            _reinstatementMaterials = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> _materialSizeList { get; set; }

    public ObservableCollection<string> MaterialSizeList
    {
        get => _materialSizeList;
        set
        {
            _materialSizeList = value;
            OnPropertyChanged();
        }
    }

    public string Length
    {
        get => _length;
        set
        {
            _length = value;
            OnPropertyChanged();
        }
    }

    public string Width
    {
        get => _width;
        set
        {
            _width = value;
            OnPropertyChanged();
        }
    }

    public string Depth
    {
        get => _depth;
        set
        {
            _depth = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ReinstatementMeasure> CurrentReinstatementMeasures
    {
        get => _currentReinstatementMeasures;
        set
        {
            _currentReinstatementMeasures = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        if (NavigationalParameters.SelectedTaskItem != null)
        {
            ProjectDate = NavigationalParameters.SelectedTaskItem?.JobDate.ToString("dd/MM/yyyy");
            ProjectName = NavigationalParameters.SelectedTaskItem?.ProjectName;
        }

        if (NavigationalParameters.CurrentSelectedJob != null)
        {
            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        }

        ReinstatementMaterials = new ObservableCollection<string>(NavigationalParameters.ReinstatementMaterials);

        MaterialSizeList = new ObservableCollection<string>(NavigationalParameters.MaterialSizes);

        CurrentReinstatementMeasures = new ObservableCollection<ReinstatementMeasure>(
            _jobService.GetReinstatementMeasures(NavigationalParameters.SelectedTaskItem));

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = false;
        ShowSection5 = true;
        ShowSection6 = true;
        ShowSection7 = true;
        IsLoading = false;
    });

    public RelayCommand Submit => new(async () =>
    {
        var validEntry = false;

        try
        {
            var x = Convert.ToDecimal(Length);

            var y = Convert.ToDecimal(Width);

            var z = Convert.ToDecimal(Depth);

            if (!string.IsNullOrEmpty(SelectedReinstatement) && !string.IsNullOrEmpty(SelectedMaterialSize))
                validEntry = true;
        }
        catch (Exception ex)
        {
            await Alert(
                "All fields must be complete with required selections made and measurements in the correct format!",
                "Invalid Entry", "Ok");
            validEntry = false;
        }

        if (validEntry)
        {
            var reinstatementToSave = new ReinstatementMeasure
            {
                QuoteNumber = NavigationalParameters.SelectedTaskItem.QuoteNumber,
                JobDate = NavigationalParameters.SelectedTaskItem.JobDate,
                //DateSubmitted = DateTime.Now,
                SupervisorId = NavigationalParameters.SelectedTaskItem.SupervisorId,
                GangLeaderId = NavigationalParameters.SelectedTaskItem.GangLeaderId,
                Length = Convert.ToDecimal(Length),
                Width = Convert.ToDecimal(Width),
                Depth = Convert.ToDecimal(Depth),
                Material = SelectedReinstatement,
                MaterialSize = SelectedMaterialSize
            };

            await _jobService.AddReinstatmentMeasure(reinstatementToSave);

            CurrentReinstatementMeasures = new ObservableCollection<ReinstatementMeasure>(
                _jobService.GetReinstatementMeasures(NavigationalParameters.SelectedTaskItem));

            Length = "";
            Width = "";
            Depth = "";

            ReinstatementMaterials = new ObservableCollection<string>(NavigationalParameters.ReinstatementMaterials);
            MaterialSizeList = new ObservableCollection<string>(NavigationalParameters.MaterialSizes);
        }
    });

    public RelayCommand Back => new(async () => { NavigateBack(); });

    public RelayCommand Upload => new(async () =>
    {
        IsLoading = true;
        ShowSection7 = false;
        var success = await _jobService.UploadReInstatementMeasuresAsync();

        if (success)
        {
            await Alert(
                "Upload Compleate!",
                "Success", "Ok");

            IsLoading = false;
            ShowSection7 = true;
        }
        else
        {
            await Alert(
                "There has been an error uploading please try again or contact support!",
                "Success", "Ok");

            IsLoading = false;
            ShowSection7 = true;
        }

        IsLoading = false;

        ShowSection7 = true;

        NavigateBack();
    });

    public RelayCommand DeleteMeasure => new(async () =>
    {
        await _jobService.DeleteReinstatementMeasure(NavigationalParameters.ReinstatmentMeasureToDelete);

        CurrentReinstatementMeasures =
            new ObservableCollection<ReinstatementMeasure>(
                _jobService.GetReinstatementMeasures(NavigationalParameters.SelectedTaskItem));
    });


    public RelayCommand SetSizePickerList => new(async () =>
    {
        switch (SelectedReinstatement)
        {
            case "AC Medium & Open Graded Surface & Binder Courses":
            case "AC Dense Surface, Base & Binder Courses":
            case "HRA Surface, Base & Binder Courses":
            case "SMA Surface &Binder Courses":
                MaterialSizeList = new ObservableCollection<string>
                {
                    "6mm",
                    "10mm",
                    "14mm",
                    "20mm"
                };
                ShowSection3 = true;
                ShowSection4 = false;
                break;
            case "EME2 Binder Course":
                MaterialSizeList = new ObservableCollection<string>
                {
                    "6mm",
                    "10mm",
                    "14mm"
                };
                ShowSection3 = true;
                ShowSection4 = false;
                break;
            case "Precast Concrete Flags (PCC)":
                MaterialSizeList = new ObservableCollection<string>
                {
                    "900 x 600",
                    "600 x 600",
                    "450 x 450",
                    "300 x 300"
                };
                ShowSection3 = true;
                ShowSection4 = false;
                break;
            case "actile Blister Flags":
            case "Tactile Corduroy Flags":
                MaterialSizeList = new ObservableCollection<string>
                {
                    "600 x 600",
                    "450 x 450",
                    "300 x 300"
                };
                ShowSection3 = true;
                ShowSection4 = false;
                break;
            default:
                ShowSection3 = false;
                ShowSection4 = true;
                break;
        }
    });
}