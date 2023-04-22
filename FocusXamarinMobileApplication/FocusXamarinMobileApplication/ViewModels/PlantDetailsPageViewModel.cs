using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class PlantDetailsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public PlantDetailsPageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
    }

    public Assignments _assigmentService { get; }
    public Jobs _jobService { get; }
    public Users _userService { get; }

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


    public string _nextServiceDate { get; set; }

    public string NextServiceDate
    {
        get => _nextServiceDate;
        set
        {
            _nextServiceDate = value;
            OnPropertyChanged();
        }
    }


    public string _type { get; set; }

    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropertyChanged();
        }
    }


    public string _supplier { get; set; }

    public string Supplier
    {
        get => _supplier;
        set
        {
            _supplier = value;
            OnPropertyChanged();
        }
    }


    public string _make { get; set; }

    public string Make
    {
        get => _make;
        set
        {
            _make = value;
            OnPropertyChanged();
        }
    }


    public string _nextServiceType { get; set; }

    public string NextServiceType
    {
        get => _nextServiceType;
        set
        {
            _nextServiceType = value;
            OnPropertyChanged();
        }
    }


    public string _assetNo { get; set; }

    public string AssetNo
    {
        get => _assetNo;
        set
        {
            _assetNo = value;
            OnPropertyChanged();
        }
    }


    public string _serial { get; set; }

    public string Serial
    {
        get => _serial;
        set
        {
            _serial = value;
            OnPropertyChanged();
        }
    }

    public string _model { get; set; }

    public string Model
    {
        get => _model;
        set
        {
            _model = value;
            OnPropertyChanged();
        }
    }

    public DocumentData2Display _selectedDocument { get; set; }

    public DocumentData2Display SelectedDocument
    {
        get => _selectedDocument;
        set
        {
            _selectedDocument = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DocumentData2Display> _documentList { get; set; }

    public ObservableCollection<DocumentData2Display> DocumentList
    {
        get => _documentList;
        set
        {
            _documentList = value;
            OnPropertyChanged();
        }
    }


    public ImageSource _fuelIcon { get; set; } = SimpleStaticHelpers.GetImage("FuelIcon");

    public ImageSource FuelIcon
    {
        get => _fuelIcon;
        set
        {
            _fuelIcon = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        Title = "Plant details";

        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;

        if (NavigationalParameters.SelectetedPlantItem != null)
        {
            NextServiceDate = NavigationalParameters.SelectetedPlantItem.NextServiceDate.ToShortDateString();
            FillInSelectedPlant(NavigationalParameters.SelectetedPlantItem);
            FillInDocsTable("/", Convert.ToInt32(NavigationalParameters.SelectetedPlantItem.RemotePlantId));
        }
    });

    public RelayCommand OpenDocumentCommand => new(async () =>
    {
        NavigationalParameters.SelectedDocument = SelectedDocument;

        //var newPath = SimpleStaticHelpers.BuildNewPathOrReturnFilename(CurrentPath,
        //         NavigationalParameters.SelectedDocument?.FolderPath, NavigationalParameters.SelectedDocument?.DocumentTitle,
        //         NavigationalParameters.SelectedDocument.FileName);

        //CurrentPath = newPath.Substring(1);

        NavigationalParameters.SelectedDocument.TabletDocumentFolder = "PlantDocs";

        await NavigateTo(ViewModelLocator.DocumentViewPage);
    });

    public RelayCommand FuelCommand => new(async () => { await NavigateTo(ViewModelLocator.FuelConsumptionPage); });

    public RelayCommand Cancel => new(async () => { NavigateBack(); });

    public string CurrentPath { get; private set; }

    private void FillInSelectedPlant(Plant4Tablet selectedPlant)
    {
        AssetNo = selectedPlant.AssetNo;
        Make = selectedPlant.Make;
        Model = selectedPlant.Model;
        NextServiceDate =
            selectedPlant.NextServiceDate.ToShortDateString() ==
            Convert.ToDateTime("01/01/0001 00:00:00").ToShortDateString()
                ? "N/A"
                : selectedPlant.NextServiceDate.ToShortDateString();
        NextServiceType = selectedPlant.NextServiceType;
        Supplier = selectedPlant.Supplier;
        Serial = selectedPlant.Serial;
        Type = selectedPlant.Type;
    }

    private void FillInDocsTable(string currentPath, int plantId)
    {
        var docs = new Documents();

        DocumentList =
            new ObservableCollection<DocumentData2Display>(docs.GetDocuments(currentPath, "PlantDocs", "", 0, plantId));
    }
}