#region

using Audit = FocusXamarinMobileApplication.Models.Audit;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SelectEndPointPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;
    private readonly Pictures _pictureService;

    private bool _isLoading;
    private bool _showSubmissionButton;
    private bool _showUpload = true;

    public SelectEndPointPageViewModel()
    {
        _jobService = new Jobs();

        _assignmentService = new Assignments();

        _pictureService = new Pictures();
    }

    public bool _showAddAsset { get; set; }

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

    public string _searchText { get; set; }= "";

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged("SearchText");
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

    public DataPassed2Server DataPassedToserver { get; set; }

    public ObservableCollection<VMexpansionReleaseData> _assets { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool ShowUpload
    {
        get => _showUpload;
        set
        {
            _showUpload = value;
            OnPropertyChanged();
        }
    }

    public bool ShowAddAsset
    {
        get => _showAddAsset;
        set
        {
            _showAddAsset = value;
            OnPropertyChanged("ShowAddAsset");
        }
    }

    public bool ShowSubmissionButton
    {
        get => _showSubmissionButton;
        set
        {
            _showSubmissionButton = value;
            OnPropertyChanged();
        }
    }

    public List<SurveyAnswers> SurveyAnswers { get; private set; }

    private ObservableCollection<VMexpansionReleaseData> _assetList { get; set; }

    public ObservableCollection<VMexpansionReleaseData> AssetList
    {
        get => _assetList;
        set
        {
            _assetList = value;
            OnPropertyChanged();
        }
    }

    public bool _showStatus { get; set; } = true;

    public bool ShowStatus
    {
        get => _showStatus;
        set
        {
            _showStatus = value;
            OnPropertyChanged();
        }
    }

    public bool _showDesign { get; set; } = true;

    public bool ShowDesign
    {
        get => _showDesign;
        set
        {
            _showDesign = value;
            OnPropertyChanged();
        }
    }

    public bool _showMap { get; set; } = true;

    public bool ShowMap
    {
        get => _showMap;
        set
        {
            _showMap = value;
            OnPropertyChanged();
        }
    }

    public VMexpansionReleaseData _assetSelected { get; set; }

    public VMexpansionReleaseData AssetSelected
    {
        get => _assetSelected;
        set
        {
            _assetSelected = value;
            OnPropertyChanged();
        }
    }

    public string _addButtonText { get; set; } = "Select Street";

    public string AddButtonText
    {
        get => _addButtonText;
        set
        {
            _addButtonText = value;
            OnPropertyChanged();
        }
    }

    public string _uploadButtonText { get; set; } = "Upload";

    public string UploadButtonText
    {
        get => _uploadButtonText;
        set
        {
            _uploadButtonText = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        try
        {
            NavigationalParameters.SelectedEndPoint = null;
            NavigationalParameters.SelectedAsset = null;
            NavigationalParameters.SelectedQuestion = null;
            NavigationalParameters.SelectedAnswer = null;
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
            ShowStatus = true;
            ShowUpload = true;
            ShowAddAsset = true;
            AddButtonText = "Add Asset";
            ShowDesign = true;
            ShowMap = true;

            var assets = _jobService
                .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                .Where(x => x.Status.ToLower() != "removed").ToList();

            foreach (var asset in assets)
            {
                asset.ShowRemoveButton = false;
            }

            switch (NavigationalParameters.AppMode)
            {
                case NavigationalParameters.AppModes.CHAMBERAUDIT:
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Chamber";

                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.CHAMBERAUDIT;
                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = false;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets
                        .Where(x => x.BuildingStandard.ToLower() == "chamber" && x.Status.ToLower() != "removed")
                        .OrderBy(x => x.Order).ToList());

                    break;
                case NavigationalParameters.AppModes.PRESITEPREMISIS:
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Premises";
                    UploadButtonText = "Upload Premises";


                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPREMISES;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets
                        .Where(x => x.BuildingStandard.ToLower() == "premises" && x.Status.ToLower() != "removed")
                        .OrderBy(x => x.Order).ToList());

                    break;
                case NavigationalParameters.AppModes.PRESITEPOLESURVEY:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY;
                    Title = "Select a pole";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Pole";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "pole" && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.PRESITECHAMBERSURVEY:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITECHAMBERSURVEY;
                    Title = "Select a chamber";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add chamber";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "chamber" && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPIAPOLESURVEY;
                    Title = "Select a pole";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Pole";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "pole" && x.DropLength.ToString().ToLower().Contains("pia") && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITECHAMBERPIASURVEY;
                    Title = "Select a chamber";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add chamber";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "chamber" && x.DropLength.ToString().ToLower().Contains("pia") && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEDUCTSURVEY;
                    Title = "Select a duct section";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add duct section";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "duct" && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.BLOCKAGE:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.BLOCKAGE;
                    Title = "Select a duct section";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = false;
                    AddButtonText = "Add duct section";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = false;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "duct" && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEDUCTPIASURVEY;
                    Title = "Select a duct section";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add duct section hamber";
                    UploadButtonText = "Upload";

                    foreach (var asset in assets)
                    {
                        asset.ShowRemoveButton = true;
                    }

                    AssetList = new ObservableCollection<VMexpansionReleaseData>(assets.Where(x => x.BuildingStandard.ToLower() == "duct" && x.DropLength.ToString().ToLower().Contains("pia") && x.Status.ToLower() != "removed").OrderBy(x => x.Order).ToList());
                    break;
                case NavigationalParameters.AppModes.POLEASBUILT:
                    Title = "Select a pole";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Pole";
                    UploadButtonText = "Upload";
                    ShowDesign = false;
                    ShowMap = false;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.POLEASBUILT;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower() == "pole" && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.CHAMBERASBUILT:
                case NavigationalParameters.AppModes.CHAMBERMEASURE:
                    Title = "Select a chamber";
                   // ShowAddAsset = false;
                    ShowStatus = false;
                    ShowUpload = true;
                   // AddButtonText = "";
                    UploadButtonText = "Upload";
                    ShowDesign = true;
                    ShowMap = true;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower() == "chamber" && x.Status.ToLower() != "removed")
                        .ToList().OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.DPASBUILT:
                    Title = "Select a distribution point";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add DP";
                    UploadButtonText = "Upload";
                    ShowDesign = true;
                    ShowMap = true;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DPASBUILT;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob).Where(x =>
                            x.BuildingStandard.ToLower() == "distributionpoint" && x.Status.ToLower() != "removed")
                        .ToList().OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.BJEASBUILT:
                    Title = "Select a backhaul joint enclosure";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add BJE";
                    UploadButtonText = "Upload";
                    ShowDesign = true;
                    ShowMap = true;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.BJEASBUILT;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob).Where(x =>
                            x.BuildingStandard.ToLower() == "backhauljointenclosure" && x.Status.ToLower() != "removed")
                        .ToList().OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.DJEASBUILT:
                    Title = "Select a distribution joint enclosure";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add DJE";
                    UploadButtonText = "Upload";
                    ShowDesign = true;
                    ShowMap = true;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DJEASBUILT;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob).Where(x =>
                            x.BuildingStandard.ToLower() == "distributionjointenclosure" &&
                            x.Status.ToLower() != "removed").ToList().OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.FJEASBUILT:
                    Title = "Select a fibre joint enclosure";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add FJE";
                    UploadButtonText = "Upload";
                    ShowDesign = true;
                    ShowMap = true;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.FJEASBUILT;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob).Where(x =>
                            x.BuildingStandard.ToLower() == "fibrejointenclosure" && x.Status.ToLower() != "removed")
                        .ToList().OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.POLEMEASURE:
                    Title = "Select a pole";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Pole";
                    UploadButtonText = "Upload";
                    ShowDesign = false;
                    ShowMap = false;
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.POLEMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower() == "pole" && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.DUCTMEASURE:
                    Title = "Select the duct";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Duct";
                    ShowDesign = false;
                    ShowMap = false;
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DUCTMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower() == "duct" && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                    Title = "Select a distribution point";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    ShowDesign = false;
                    ShowMap = false;
                    AddButtonText = "";
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob).Where(x =>
                            x.BuildingStandard.ToLower() == "distributionpoint" && x.Status.ToLower() != "removed")
                        .ToList().OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.UGCRPMEASURE:
                    Title = "Select a asset";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Asset";
                    ShowDesign = false;
                    ShowMap = false;
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.UGCRPMEASURE;
                    break;
                case NavigationalParameters.AppModes.DJEMEASURE:
                    Title = "Select a asset";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add DJE";
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DJEMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower().Contains("DJE") && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.BJEMEASURE:
                    Title = "Select a asset";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add BJE";
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.BJEMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower().Contains("BJE") && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.FJEMEASURE:
                    Title = "Select a asset";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add FJE";
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.FJEMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower().Contains("FJE") && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.POLECABLEMEASURE:
                    Title = "Select a asset";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "Add Pole";
                    UploadButtonText = "Upload";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.POLECABLEMEASURE;
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower() == "pole" && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                case NavigationalParameters.AppModes.REMEDIAL:
                    Title = "Select a work item";
                    ShowAddAsset = true;
                    ShowStatus = false;
                    ShowUpload = true;
                    AddButtonText = "";
                    NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.REMEDIAL;
                    UploadButtonText = "Upload";
                    AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                        .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                        .Where(x => x.BuildingStandard.ToLower().Contains("remedial") && x.Status.ToLower() != "removed").ToList()
                        .OrderBy(x => x.Order));
                    break;
                default:
                    if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY)
                    {
                        Title = "Select Asset";

                        AddButtonText = "Add Asset";

                        UploadButtonText = "Upload survey";

                        ShowUpload = true;

                        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY;

                        AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                            .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                            .Where(x => x.BuildingStandard.ToLower() == "pole" && x.Status.ToLower() != "removed")
                            .ToList().OrderBy(x => x.Order));
                    }
                    else
                    {
                        AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                            .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                            .Where(x => x.Status.ToLower() != "removed").ToList().OrderBy(x => x.Order));

                        Title = "Select street";

                        AddButtonText = "Add street";

                        UploadButtonText = "Upload Pre Site";

                        ShowUpload = true;
                    }

                    ShowAddAsset = true;

                    ShowStatus = false;

                    break;
            }

            if (AssetList.Any())
            {
                NavigationalParameters.ListOfAssets = new ObservableCollection<VMexpansionReleaseData>(AssetList);

                NavigationalParameters.CurrentEndpointList = true;
            }
            else
            {
                NavigationalParameters.CurrentEndpointList = false;

                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT ||
                    NavigationalParameters.AppMode == NavigationalParameters.AppModes.DPASBUILT
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEASBUILT
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.POLEASBUILT
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEASBUILT
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEASBUILT)
                {
                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.ASBUILT;

                    await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.REMEDIAL)
                {
                    await NavigateTo(ViewModelLocator.MenuSelectionPage);
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.BLOCKAGE)
                {
                    await NavigateTo(ViewModelLocator.MenuSelectionPage);
                }
                else
                {
                    //NavigationalParameters.AppMode = NavigationalParameters.AppModes.MEASURES;

                    await NavigateTo(ViewModelLocator.MeasureTypeSelectionPage);
                }

                await Alert("There are no assets available to select",
                    "Please contact support for furthur assistance!");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand AddAsset => new(async () =>
    {
        try
        {
            NavigationalParameters.MapType = "AssetLocationMap";
            NavigationalParameters.AddingNewAsset = true;
            NavigationalParameters.CurrentAssignment = new Assignment
            {
                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                AssignmentId = Guid.NewGuid(),
                Category = NavigationalParameters.SurveyType.ToString(),
                Cnumber = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                TermContract = NavigationalParameters.CurrentSelectedJob?.BaseContractId,
                ClientName = NavigationalParameters.CurrentSelectedJob?.ClientName,
                Complete = "false",
                PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
                RemoteTableId = 0,
                Type = NavigationalParameters.SurveyType.ToString()
            };


            NavigationalParameters.SelectedQuestion = App.Database.GetItems<SurveyQuestion>()?.FirstOrDefault(x =>
                x.QuestionId == 0.10M && x.Category == NavigationalParameters.CurrentAssignment?.Category);

            NavigationalParameters.SelectedAnswer = new SurveyAnswers
            {
                QuestionId = 0,
                QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                Category = NavigationalParameters.CurrentAssignment.Category,
                CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                SubmittedDateTime = DateTime.Now
            };

            NavigationalParameters.SelectedCabinet = new VMl4CabDetail
            {
                Existing = "No",
                LastUpdateTime = DateTime.Now,
                QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                L4Number = NavigationalParameters.CurrentAssignment?.AssignmentId.ToString(),
                PreSitedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                CabinetDetails = "Created On Survey"
            };

            var assetCategory = "";

            if (NavigationalParameters.AppMode.ToString().ToLower().Contains("chamber"))
            {
                assetCategory = "Chamber";
            }
            else if (NavigationalParameters.AppMode.ToString().ToLower().Contains("pole"))
            {
                assetCategory = "Pole";
            }
            else if (NavigationalParameters.AppMode.ToString().ToLower().Contains("duct"))
            {
                assetCategory = "Duct";
            }
            else if (NavigationalParameters.AppMode.ToString().ToLower().Contains("distributionpoint"))
            {
                assetCategory = "DistributionPoint";
            }
            else if (NavigationalParameters.AppMode.ToString().ToLower().Contains("cabinet"))
            {
                assetCategory = "DistributionPoint";
            }
            else
            {
                assetCategory = "JointEnclosure";
            }

            NavigationalParameters.SelectedAsset = new VMexpansionReleaseData
            {
                Qnumber = Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.QuoteNumber),
                Blocked = false,
                L4Number = NavigationalParameters.CurrentAssignment.AssignmentId.ToString(),
                ClaimId = NavigationalParameters.CurrentAssignment?.AssignmentId ?? Guid.NewGuid(),
                BuildingStandard = assetCategory,
                Status = "new asset",
                RemoteTableId = 0,
                SavedToServer = false,
                DropLength = "New asset requires survey"
            };

            var asset = AssetList?.OrderByDescending(x => x.Order)?.FirstOrDefault();

            if (!string.IsNullOrEmpty(asset.StreetName))
            {
                var nameList = asset.StreetName.Split("-")?.ToList();

                var assetNumber = nameList.Last();

                var newAssetName = "";

                //if (asset.BuildingStandard.ToLower() == "pole")
                //{
                //    newAssetName = asset.StreetName.Replace(assetNumber, (Convert.ToInt32(nameList?.LastOrDefault()) + 1).ToString());
                //}
                //else
                //{
                    try
                    {
                        Convert.ToInt32(nameList?.LastOrDefault());
                        newAssetName = asset.StreetName.Replace($"-{assetNumber}", $"-{Convert.ToInt32(nameList?.LastOrDefault()) + 1}");

                        NavigationalParameters.SelectedAsset.VmnbUnumber =
                       (Convert.ToInt32(nameList?.LastOrDefault()) + 1).ToString();
                    }
                    catch (Exception ex)
                    {
                        var assetyName = nameList?.LastOrDefault().ToString().ToLower();

                        if (assetyName.StartsWith('c') == true)
                        {
                            var xx = Convert.ToInt32(assetyName.Replace('C', ' ')) + 1;
                            newAssetName = asset.StreetName.Replace(assetNumber, $"C{xx}");

                            NavigationalParameters.SelectedAsset.VmnbUnumber =
                       (Convert.ToInt32(xx + 1)).ToString();
                        }
                        else
                        {   var claimId = Guid.NewGuid();
                            NavigationalParameters.SelectedAsset.ClaimId = claimId;
                            newAssetName = $"New-{asset.BuildingStandard}-{claimId}";
                        }                     
                    }      
                //}

                if (AssetList.All(x => x.StreetName != newAssetName))
                {
                    NavigationalParameters.CurrentAssignment.StreetName =
                        NavigationalParameters.SelectedAnswer.StreetName =
                            NavigationalParameters.SelectedAsset.StreetName = newAssetName;

                
                    NavigationalParameters.CurrentAssignment.StreetName =
                       NavigationalParameters.SelectedAnswer.StreetName =
                           NavigationalParameters.SelectedAsset.TownCity = asset.TownCity;

                    NavigationalParameters.CurrentAssignment.LocationName =
                        NavigationalParameters.SelectedAsset?.L4Number;

                    App.Database.SaveItem(NavigationalParameters.CurrentAssignment);

                    App.Database.SaveItem(NavigationalParameters.SelectedAnswer);

                    App.Database.SaveItem(NavigationalParameters.SelectedAsset);
                }
                else
                {
                    await Alert("Asset currently exsists please contact support!", "Error", "Ok");
                }               
            }

            if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.CHAMBERAUDIT)
                await NavigateTo(ViewModelLocator.AddChamber);

            if (NavigationalParameters.SurveyType.ToString().ToLower().Contains("presite") 
             || NavigationalParameters.SurveyType.ToString().ToLower().Contains("asbuilt")
             || NavigationalParameters.SurveyType.ToString().ToLower().Contains("measure"))
                await NavigateTo(ViewModelLocator.HybridWebViewPage);
            else
                await NavigateTo(ViewModelLocator.FormsMapPage);
        }
        catch (Exception ex)
        {
            NavigationalParameters.SelectedAsset.StreetName = "InvalidPoleId";

            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand SelectedAsset => new(async () =>
    {
        try
        {
            NavigationalParameters.PreviewMode = false;
            NavigationalParameters.SelectedAnswer = null;
            NavigationalParameters.SelectedAsset = AssetSelected;
            NavigationalParameters.SelectedEndPoint = NavigationalParameters.SelectedAsset;
            NavigationalParameters.CurrentAssignment = null;
      
            var ass = _assignmentService.GetCurrentAssignment(
                Convert.ToInt64(NavigationalParameters.SelectedAsset?.Qnumber),
                NavigationalParameters.SelectedAsset?.StreetName, NavigationalParameters.SurveyType.ToString());

            if (ass != null && ass?.PreSiteById == NavigationalParameters.LoggedInUser.FocusId &&
                ass?.Complete == "false")
            {
                NavigationalParameters.CurrentAssignment = ass;
            }

            NavigationalParameters.AnswerList = new List<SurveyAnswers>();

            switch (NavigationalParameters.SurveyType)
            {
                case NavigationalParameters.SurveyTypes.PRESITEPREMISES:
                    {
                       
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.PRESITECHAMBERPIASURVEY:
                {

                    SetUpSurveyAsync();
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                }
                    break;
                case NavigationalParameters.SurveyTypes.PRESITECHAMBERSURVEY:
                {

                    SetUpSurveyAsync();
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                }
                    break;
                case NavigationalParameters.SurveyTypes.PRESITEPIAPOLESURVEY:
                {

                    SetUpSurveyAsync();
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                }
                    break;
                case NavigationalParameters.SurveyTypes.PRESITEDUCTSURVEY:
                {

                    SetUpSurveyAsync();
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                }
                    break;
                case NavigationalParameters.SurveyTypes.PRESITEDUCTPIASURVEY:
                {
                    SetUpSurveyAsync();
                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                }
                    break;
                case NavigationalParameters.SurveyTypes.BLOCKAGE:
                {
                    SetUpSurveyAsync();
                    await NavigateTo(ViewModelLocator.BlockageListPage);
                }
                    break;
                case NavigationalParameters.SurveyTypes.CHAMBERAUDIT:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.POLEASBUILT:
                    {
                        NavigationalParameters.SelectedAsset.L4Number = Guid.NewGuid().ToString();
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.CHAMBERASBUILT:
                    {
                        NavigationalParameters.SelectedAsset.L4Number = Guid.NewGuid().ToString();
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.DPASBUILT:
                    {
                        NavigationalParameters.SelectedAsset.ClaimId = Guid.NewGuid();
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.BJEASBUILT:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.DJEASBUILT:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.FJEASBUILT:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.SITEINSPECTION:
                    {
                        if (NavigationalParameters.CurrentAssignment == null)
                        {
                            NavigationalParameters.CurrentAssignment = new Assignment
                            {
                                AssignmentId = Guid.NewGuid(),
                                Category = "Audit", //
                                ProjectName =
                                    NavigationalParameters.CurrentSelectedJob.ProjectName, //VM NBU 64132
                                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber, //"406999",
                                Cnumber = NavigationalParameters.CurrentSelectedJob.ContractNumber
                                    .ToString(), //6026672
                                Complete = "false",
                                RemoteTableId = 0,
                                TermContract = NavigationalParameters.CurrentSelectedJob.BaseContractId, //6026672
                                PreSiteById = NavigationalParameters.CurrentSelectedJob.GangLeaderId, //100026
                                CompletedOn = NavigationalParameters.CurrentSelectedJob.JobDate,
                                ClientName = NavigationalParameters.CurrentSelectedJob.ClientName,
                                StreetName = "N/A",
                                Type = NavigationalParameters.SurveyType.ToString()
                            };

                            await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
                        }

                        if (NavigationalParameters.CurrentAudit == null)
                        {
                            NavigationalParameters.CurrentAudit = new Audit
                            {
                                AssignmentId =
                                    NavigationalParameters.CurrentAssignment
                                        .AssignmentId, //34cd145f-7338-434f-af0b-b8f2037d2eef
                                ProjectName =
                                    NavigationalParameters.CurrentSelectedJob.ProjectName, //VM NBU 64132
                                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber, //"406999"
                                RemoteTableId = 0,
                                AuditDate = NavigationalParameters.CurrentSelectedJob.JobDate,
                                GangLeaderId = NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString(),
                                GpsStart = "",
                                GpsEnd = "",
                                AuditeeFocusId =
                                    NavigationalParameters.CurrentSelectedJob.GangLeaderId,
                                // AuditorFocusId = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                                StartTime = DateTime.Now,
                                //ConductedBy = "",
                                AdditionalComments = ""
                            };

                            await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
                        }

                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.POLEMEASURE:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.MeasureListPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.DUCTMEASURE:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.MeasureListPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.DISTRIBUTIONPOINTMEASURE:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.MeasureListPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.CHAMBERMEASURE:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.MeasureListPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.UGCRPMEASURE:
                    {
                         SetUpSurveyAsync();

                        await NavigateTo(ViewModelLocator.MeasureListPage);
                    }
                    break;
                case NavigationalParameters.SurveyTypes.POLECABLEMEASURE:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.MeasureListPage);
                    }
                    break;            
                case NavigationalParameters.SurveyTypes.REMEDIAL:
                    {
                         SetUpSurveyAsync();
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                    break;
                default:
                    {
                        await NavigateTo(ViewModelLocator.SelectStreetPage);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoToDesign => new(async () =>
    {
        try
        {
            var docs = new Documents();
            NavigationalParameters.MapType = "Drawing";
            var qNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;

            NavigationalParameters.SelectedDocument = docs
                .GetDocuments("/Drawings/", "JobDocs", qNumber.ToString(), 0)?
                .FirstOrDefault(x => x.DocumentTitle
                    .Contains($"{qNumber}") && x.DocumentTitle.ToUpper()
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

    public RelayCommand GoToMap => new(async () =>
    {
        try
        {
            NavigationalParameters.MapType = "AssetLocations";

            await NavigateTo(ViewModelLocator.FormsMapPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand ClearCommand => new(async () =>
    {
        try
        {
            SearchText = "";
            PageLoaded.Execute(null);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand SearchCommand => new(async () =>
    {
        try
        {
            AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                       .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                       .Where(x => x.StreetName.ToLower().Contains(SearchText)).ToList()
                       .OrderBy(x => x.Order));
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand GoBack => new(async () =>
    {
        try
        {
            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            {
                NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;

                await NavigateTo(ViewModelLocator.MenuSelectionPage);
            }
            else
            {
                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.POLEMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.POLECABLEMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.UGCRPMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.UGCRPMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DJEMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.FJEMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.BJEMEASURE
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.DUCTMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERMEASURE
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.MEASURES)
                {
                    await NavigateTo(ViewModelLocator.MeasureTypeSelectionPage);
                }
                else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.REMEDIAL)
                {
                    await NavigateTo(ViewModelLocator.MenuSelectionPage);
                }
                else
                {
                    switch (NavigationalParameters.SurveyType)
                    {
                        case NavigationalParameters.SurveyTypes.CHAMBERAUDIT:
                        case NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY:
                        case NavigationalParameters.SurveyTypes.POLEASBUILT:
                        case NavigationalParameters.SurveyTypes.PRESITEPREMISES:
                        case NavigationalParameters.SurveyTypes.CHAMBERASBUILT:
                        case NavigationalParameters.SurveyTypes.DPASBUILT:
                        case NavigationalParameters.SurveyTypes.BJEASBUILT:
                        case NavigationalParameters.SurveyTypes.FJEASBUILT:
                        case NavigationalParameters.SurveyTypes.DJEASBUILT:

                            await NavigateTo(ViewModelLocator.SelectSurveyTypePage);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                           $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }

    });

    public RelayCommand Refresh => new(() => { OnPropertyChanged("ShowSubmissionButton"); });

    public RelayCommand SubmitPresiteCheck => new(async () =>
    {
        try
        {

            var wa = new WebApi();

            var areWeConnected = await wa.CanWeConnect2Api();

            var dataPassedToserver = new DataPassed2Server();

            var assignmentListToBerUploaded = new List<Assignment>();

            var userChoice = false;

            userChoice = await Alert("Submit Survey?", "Would you like to Upload the survey now?", "Yes", "No");

            var jobService = new Jobs();

            if (userChoice)
            {
                if (areWeConnected)
                {
                    IsLoading = true;

                    ShowUpload = false;

                    // submit presite whern complete
                    dataPassedToserver.JobData = NavigationalParameters.CurrentSelectedJob;

                    dataPassedToserver.Assignments = new List<Assignment>();

                    if (NavigationalParameters.AppMode.ToString().ToLower().Contains("measure")                         
                               || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt"))
                        assignmentListToBerUploaded = _assignmentService.GetAsBuiltAssignmentsToUpload();
                    else
                        assignmentListToBerUploaded = _assignmentService.GetAssignmentsToUpload();

                    if (assignmentListToBerUploaded.Any())
                    {
                        foreach (var assignment in assignmentListToBerUploaded)
                        {
                            var answers = (bool)App.Database.GetItems<SurveyAnswers>()?.Any(x =>
                                x.AssignmentId == assignment.AssignmentId && x.RemoteTableId == 0);
                            var type = NavigationalParameters.SurveyType.ToString();

                            if (answers)
                            {
                                dataPassedToserver.Category = type;

                                dataPassedToserver.Assignments.Add(
                                    _assignmentService.GenerateAssignments2SaveById(assignment));
                            }
                            else {
                                App.Database.DeleteItem(assignment);
                            }
                        }

                        if (dataPassedToserver.Category != null)
                        {
                            dataPassedToserver = jobService.GetNewNewCabinetsAndEndPoints(dataPassedToserver);

                            var success = await jobService.SaveDailyInputAsync(dataPassedToserver);

                            dataPassedToserver = null;

                            if (!success)
                            {
                                await Alert("An issue has occured submitting the survey. The survey has been saved",
                                    "Error please contact support!");

                                IsLoading = false;

                                ShowUpload = true;

                                NavigateBack();
                            }
                           
                            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                            {
                                if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite")
                                    || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt")                                 
                                    || NavigationalParameters.AppMode.ToString().Contains("Measure"))
                                    {
                                        await NavigateTo(ViewModelLocator.SelectEndPointPage);
                                    }
                                    else
                                    {
                                        await NavigateTo(ViewModelLocator.MenuSelectionPage);
                                    }
                            }
                            else
                            {
                                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CHAMBERASBUILT
                                    || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt")
                                    || NavigationalParameters.AppMode.ToString().Contains("Measure"))
                                    {
                                        await NavigateTo(ViewModelLocator.SelectEndPointPage);
                                    }
                                    else
                                    {
                                        await NavigateTo(ViewModelLocator.MenuSelectionPage);
                                    }
                            }
                        }
                        else
                        {
                            await Alert("There is nothing to upload. ", "Upload  complete");

                            IsLoading = false;

                            ShowUpload = true;
                        }
                    }
                    else
                    {
                        await Alert("There is nothing to upload. ", "Upload  complete");

                        IsLoading = false;

                        ShowUpload = true;
                    }
                }
                else
                {
                    await Alert("Please connect to a network and try again. The surveyhas been saved. ",
                        "No Connectivity");
                }
            }


            IsLoading = false;
            ShowUpload = true;
            //}
            //else
            //{
            //    await Alert("Please complete all questions taking photoes where possible", "Survey incomplete", "Ok");
            //}
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await Alert("An issue has occured submitting the survey. This has been saved", "Error!");
            IsLoading = false;
            ShowUpload = true;
        }
    });

    public async void RemoveAsset(Button button)
    {
        try
        {
            NavigationalParameters.SelectedAsset = button.CommandParameter as VMexpansionReleaseData;

            var confim = await Alert(
                $"Are you sure you wish to remove pole {NavigationalParameters.SelectedAsset.StreetName} from Q{NavigationalParameters.SelectedAsset?.Qnumber}?. This will be updated locally until you upload surveys!!",
                "Remove Pole?", "Yes", "No");

            if (confim)
            {
                NavigationalParameters.SelectedAsset.SavedToServer = false;
                NavigationalParameters.SelectedAsset.Status = "Removed";

                App.Database.SaveItem(NavigationalParameters.SelectedAsset);
                // App.Database.SaveItem(NavigationalParameters.CurrentAssignment);
                AssetList = new ObservableCollection<VMexpansionReleaseData>(_jobService
                    .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                    ?.Where(x => x.Status.ToLower() != "removed")?.ToList().OrderBy(x => x.Order));
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    private void SetUpSurveyAsync()
    {
        NavigationalParameters.CurrentAssignment ??= new Assignment
        {
            Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
            StreetName = NavigationalParameters.SelectedAsset?.StreetName,
            AssignmentId = Guid.NewGuid(),
            Category = NavigationalParameters.SurveyType.ToString(),
            Cnumber = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
            TermContract = NavigationalParameters.CurrentSelectedJob?.BaseContractId,
            ClientName = NavigationalParameters.CurrentSelectedJob?.ClientName,
            Complete = "false",
            LocationName = NavigationalParameters.SelectedEndPoint?.L4Number,
            PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
            Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
            RemoteTableId = 0,
            Type = NavigationalParameters.SurveyType.ToString()
        };

        if (string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.L4Number) ||
            NavigationalParameters.SelectedEndPoint.L4Number == Guid.Empty.ToString())
            NavigationalParameters.SelectedEndPoint.L4Number = Guid.NewGuid().ToString();

        if (string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.ClaimId.ToString()) ||
            NavigationalParameters.SelectedEndPoint.ClaimId.ToString() == Guid.Empty.ToString())
            NavigationalParameters.SelectedEndPoint.ClaimId = Guid.NewGuid();

        NavigationalParameters.CurrentAssignment.LocationName = NavigationalParameters.SelectedEndPoint?.L4Number;
        NavigationalParameters.SelectedEndPoint.SavedToServer = false;

        NavigationalParameters.SelectedCabinet = null;
        NavigationalParameters.SelectedCabinet =
            _jobService.GetCabinets(NavigationalParameters.SelectedEndPoint.L4Number);
        NavigationalParameters.SelectedCabinet ??= new VMl4CabDetail
        {
            Existing = "No",
            LastUpdateTime = DateTime.Now,
            QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
            PreSitedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
            CabinetDetails = "Created On Survey",
            IdGuid = NavigationalParameters.CurrentAssignment.AssignmentId.ToString(),
            L4Number = NavigationalParameters.SelectedEndPoint.L4Number,
            CityFibreRef = NavigationalParameters.SelectedEndPoint.StreetName
        };

        if (string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.L4Number))
            NavigationalParameters.SelectedCabinet.L4Number = NavigationalParameters.SelectedEndPoint?.L4Number;

           _jobService.AddArea(NavigationalParameters.SelectedCabinet);

           _jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);

          _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

        GetSurveyQuestions();

        NavigationalParameters.SelectedAnswer = null;
    }

    private void GetSurveyQuestions()
    {
        try
        {
            var questions = _assignmentService.GetSurveyQuestions(NavigationalParameters.SurveyType.ToString())?.OrderBy(x => x.QuestionId)?.ToList();


            if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.CHAMBERASBUILT)
            {                      
                if (NavigationalParameters.CurrentSelectedJob.ClientName.ToLower().Contains("nynet"))
                {
                    questions = questions.Where(x => x.Stage.ToLower() == "nynetchamberasbuilt").ToList();
                }
                else if (NavigationalParameters.CurrentSelectedJob.ClientName.ToLower().Contains("derby"))
                {
                    questions = questions.Where(x => x.Stage.ToLower() == "derbychamberasbuilt").ToList();
                }
                else if (NavigationalParameters.CurrentSelectedJob.ClientName.ToLower().Contains("bam"))
                {
                    questions = questions.Where(x => x.Stage.ToLower() == "bamchamberasbuilt").ToList();
                }
                else
                {
                    questions = questions.Where(x => x.Stage.ToLower() == "chamberasbuilt").ToList();
                }                        
            }


            var surveyQuestions = new List<SurveyQuestion>();
            SurveyAnswers currentAnswer = null;
            NavigationalParameters.PreSiteQuestions = new List<SurveyQuestion>();
            NavigationalParameters.GenericQuestions = new ObservableCollection<SurveyQuestion>();
            SurveyAnswers = new List<SurveyAnswers>();

            if (NavigationalParameters.CurrentAssignment != null)
            {
                SurveyAnswers =
                    _assignmentService.GetRelevantResponses(NavigationalParameters.CurrentAssignment.AssignmentId) ??
                    new List<SurveyAnswers>();
            }

            foreach (var question in questions)
            {
                if (SurveyAnswers != null && SurveyAnswers.Any())
                {
                    if (SurveyAnswers.Any(x => x.QuestionId == question.QuestionId))
                    {
                        currentAnswer = SurveyAnswers?.FirstOrDefault(x => x.QuestionId == question.QuestionId && x.Category == question.Category);
                    }
                }

                var picExsists = _pictureService.GetAllPictures().Any(x =>
                    x.QNumber == NavigationalParameters.CurrentAssignment?.Qnumber &&
                    x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId &&
                    x.QuestionId == question?.QuestionId.ToString());

                var keyAnswers = question?.KeyAnswer.Split(',').ToList();

                switch (question?.ResponseType)
                {
                    case "Y/N Selection":
                        {
                            try
                            {
                                var questionToAdd = new YesNoNaQuestionViewModel
                                {
                                    Question = question?.Question,
                                    QuestionId = question.QuestionId,
                                    KeyAnswer = question?.KeyAnswer,
                                    Category = question?.Category.ToUpper(),
                                    DepthorRating = question.DepthorRating,
                                    FollowUpAction = question?.FollowUpAction,
                                    FurtherQuestionId = question?.FurtherQuestionId,
                                    Grouping = question?.Grouping,
                                    InformationTo = question?.InformationTo,
                                    NotifiableResponse = question?.NotifiableResponse,
                                    QuestionOptions = question?.QuestionOptions,
                                    ResponseType = question?.ResponseType,
                                    Id = question.Id,
                                    Stage = question?.Stage,
                                    BtnYesText = question?.QuestionOptions.Split(',')[0],
                                    BtnNoText = question?.QuestionOptions.Split(',')[1],
                                    BtnNaText = question?.QuestionOptions.Split(',')[2],
                                    IsEnabled = true,
                                    ShowButtonA = question?.QuestionId == 0.10M ? false : true,
                                    ShowButtonB = question?.QuestionId == 0.10M ? false : true,
                                    ShowButtonC = question?.QuestionId == 0.10M ? false : true,
                                    BtnNaBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(question.QuestionOptions.Split(',')[2].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnNoBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(question.QuestionOptions.Split(',')[1].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnYesBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(question.QuestionOptions.Split(',')[0].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    QuestionTextColour = Color.FromHex("#292F62")
                                };


                                if (currentAnswer != null && !string.IsNullOrEmpty(question.FollowUpAction))
                                {
                                    questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                                }
                                else if (currentAnswer != null && string.IsNullOrEmpty(question.FollowUpAction))
                                {
                                    questionToAdd.QuestionTextColour = Color.DarkGreen;
                                }

                                surveyQuestions.Add(questionToAdd);

                                if (questionToAdd.DepthorRating == 1)
                                    if (NavigationalParameters.PreSiteQuestions.All(x =>
                                            x.QuestionId != questionToAdd.QuestionId))
                                        NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                                Analytics.TrackEvent(
                                    $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                            }
                        }
                        break;
                    case "numberSelector":
                        {
                            try
                            {
                                var questionToAdd = new FreeTextQuestionViewModel
                                {
                                    Question = question?.Question,
                                    QuestionId = question.QuestionId,
                                    KeyAnswer = question?.KeyAnswer,
                                    Category = question?.Category.ToUpper(),
                                    DepthorRating = question.DepthorRating,
                                    FollowUpAction = question?.FollowUpAction,
                                    FurtherQuestionId = question?.FurtherQuestionId,
                                    Grouping = question?.Grouping,
                                    InformationTo = question?.InformationTo,
                                    NotifiableResponse = question?.NotifiableResponse,
                                    QuestionOptions = question?.QuestionOptions,
                                    ResponseType = question?.ResponseType,
                                    Id = question.Id,
                                    Stage = question?.Stage,
                                    IsEnabled = true,
                                    QuestionTextColour = Color.FromHex("#292F62"),
                                    PickerId = $"{question.QuestionId}"
                                };


                                if (currentAnswer != null && !string.IsNullOrEmpty(question.FollowUpAction))
                                {
                                    questionToAdd.AnswerGiven = Convert.ToInt16(currentAnswer.AnswerGiven);

                                    questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                                }
                                else if (currentAnswer != null && currentAnswer.AnswerGiven != "0" &&
                                         string.IsNullOrEmpty(question.FollowUpAction))
                                {
                                    questionToAdd.AnswerGiven = Convert.ToInt16(currentAnswer.AnswerGiven);

                                    questionToAdd.QuestionTextColour = Color.DarkGreen;
                                }
                                else if (currentAnswer == null || currentAnswer.AnswerGiven == "0")
                                {
                                    questionToAdd.QuestionTextColour = Color.FromHex("#292F62");
                                }

                                surveyQuestions.Add(questionToAdd);

                                if (questionToAdd.DepthorRating == 1)
                                {
                                    if (NavigationalParameters.PreSiteQuestions.All(x =>
                                            x.QuestionId != questionToAdd.QuestionId))
                                    {
                                        NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                                Analytics.TrackEvent(
                                    $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                            }
                        }
                        break;
                    case "multiSelector":
                        {
                            try
                            {
                                var optionsTemp = question?.QuestionOptions.Split(',').ToList();

                                while (optionsTemp.Count < 12) optionsTemp.Add("");

                                var questionToAdd = new MultiQuestionViewModel
                                {
                                    Question = question.Question,
                                    QuestionId = question.QuestionId,
                                    KeyAnswer = question.KeyAnswer,
                                    Category = question.Category,
                                    DepthorRating = question.DepthorRating,
                                    FollowUpAction = question.FollowUpAction,
                                    FurtherQuestionId = question.FurtherQuestionId,
                                    Grouping = question.Grouping,
                                    InformationTo = question.InformationTo,
                                    NotifiableResponse = question.NotifiableResponse,
                                    QuestionOptions = question.QuestionOptions,
                                    ResponseType = question.ResponseType,
                                    Id = question.Id,
                                    Stage = question.Stage,
                                    BtnAText = !string.IsNullOrEmpty(optionsTemp[0]) ? optionsTemp[0] : "",
                                    BtnBText = !string.IsNullOrEmpty(optionsTemp[1]) ? optionsTemp[1] : "",
                                    BtnCText = !string.IsNullOrEmpty(optionsTemp[2]) ? optionsTemp[2] : "",
                                    BtnDText = !string.IsNullOrEmpty(optionsTemp[3]) ? optionsTemp[3] : "",
                                    BtnEText = !string.IsNullOrEmpty(optionsTemp[4]) ? optionsTemp[4] : "",
                                    BtnFText = !string.IsNullOrEmpty(optionsTemp[5]) ? optionsTemp[5] : "",
                                    BtnGText = !string.IsNullOrEmpty(optionsTemp[6]) ? optionsTemp[6] : "",
                                    BtnHText = !string.IsNullOrEmpty(optionsTemp[7]) ? optionsTemp[7] : "",
                                    BtnIText = !string.IsNullOrEmpty(optionsTemp[8]) ? optionsTemp[8] : "",
                                    BtnJText = !string.IsNullOrEmpty(optionsTemp[9]) ? optionsTemp[9] : "",
                                    BtnKText = !string.IsNullOrEmpty(optionsTemp[10]) ? optionsTemp[10] : "",
                                    BtnLText = !string.IsNullOrEmpty(optionsTemp[11]) ? optionsTemp[11] : "",
                                    BtnABgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[0].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnBBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[1].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnCBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[2].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnDBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[3].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnEBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[4].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnFBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[5].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnGBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[6].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnHBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[7].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnIBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[8].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnJBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[9].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnKBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[10].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnLBgColour =
                                        currentAnswer != null && currentAnswer.AnswerGiven.ToLower()
                                            .Contains(optionsTemp[11].ToLower())
                                            ? Color.LawnGreen
                                            : Color.LightGray,
                                    BtnAHidden = optionsTemp[0] != "",
                                    BtnBHidden = optionsTemp[1] != "",
                                    BtnCHidden = optionsTemp[2] != "",
                                    BtnDHidden = optionsTemp[3] != "",
                                    BtnEHidden = optionsTemp[4] != "",
                                    BtnFHidden = optionsTemp[5] != "",
                                    BtnGHidden = optionsTemp[6] != "",
                                    BtnHHidden = optionsTemp[7] != "",
                                    BtnIHidden = optionsTemp[8] != "",
                                    BtnJHidden = optionsTemp[9] != "",
                                    BtnKHidden = optionsTemp[10] != "",
                                    BtnLHidden = optionsTemp[11] != "",
                                    IsEnabled = true,
                                    QuestionTextColour = Color.FromHex("#292F62")
                                };

                                if (currentAnswer != null && !string.IsNullOrEmpty(question.FollowUpAction))
                                {
                                    questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                                }
                                else if (currentAnswer != null && string.IsNullOrEmpty(question.FollowUpAction))
                                {
                                    questionToAdd.QuestionTextColour = Color.DarkGreen;
                                }


                                surveyQuestions.Add(questionToAdd);

                                if (questionToAdd.DepthorRating == 1)
                                    if (NavigationalParameters.PreSiteQuestions.All(x =>
                                            x.QuestionId != questionToAdd.QuestionId))
                                        NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                                Analytics.TrackEvent(
                                    $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
                            }
                        }
                        break;
                    default:
                        {
                            var questionToAdd = new LocationIdentityQuestionViewModel
                            {
                                Question = question.Question,
                                QuestionId = question.QuestionId,
                                KeyAnswer = question.KeyAnswer,
                                Category = question.Category,
                                DepthorRating = question.DepthorRating,
                                FollowUpAction = question.FollowUpAction,
                                FurtherQuestionId = question.FurtherQuestionId,
                                Grouping = question.Grouping,
                                InformationTo = question.InformationTo,
                                NotifiableResponse = question.NotifiableResponse,
                                QuestionOptions = question.QuestionOptions,
                                ResponseType = question.ResponseType,
                                Id = question.Id,
                                Stage = question.Stage,
                                IsEnabled = true,
                                QuestionTextColour = Color.FromHex("#292F62")
                            };

                            if (currentAnswer != null && !string.IsNullOrEmpty(question.FollowUpAction))
                            {
                                questionToAdd.QuestionTextColour = picExsists ? Color.DarkGreen : Color.Red;
                            }
                            else if (currentAnswer != null && string.IsNullOrEmpty(question.FollowUpAction))
                            {
                                questionToAdd.QuestionTextColour = Color.DarkGreen;
                            }
                            else
                            {
                                questionToAdd.QuestionTextColour =
                                    questionToAdd.QuestionTextColour = Color.FromHex("#292F62");
                            }

                            surveyQuestions.Add(questionToAdd);

                            if (questionToAdd.DepthorRating == 1)
                                if (NavigationalParameters.PreSiteQuestions.All(x =>
                                        x.QuestionId != questionToAdd.QuestionId))
                                    NavigationalParameters.PreSiteQuestions.Add(questionToAdd);
                        }
                        break;
                }

                currentAnswer = null;
            }

            NavigationalParameters.GenericQuestions = new ObservableCollection<SurveyQuestion>(surveyQuestions);

            var answerGroup = SurveyAnswers.OrderByDescending(x => x.SubmittedDateTime)?.GroupBy(x => x.QuestionId);

            foreach (var group in answerGroup)
                if (NavigationalParameters.PreSiteQuestions.Any(x => x.QuestionId == group.FirstOrDefault().QuestionId))
                {
                    var qs = NavigationalParameters.PreSiteQuestions?.FirstOrDefault(x =>
                        x.QuestionId == group.FirstOrDefault().QuestionId);

                    var keyAnswers = qs?.KeyAnswer?.Split(',');

                    foreach (var a in group.FirstOrDefault().AnswerGiven.Split(','))
                        if (keyAnswers.Contains(a))
                            if (NavigationalParameters.PreSiteQuestions.All(x => x.QuestionId != qs.QuestionId))
                                NavigationalParameters.PreSiteQuestions?.Add(
                                    NavigationalParameters.GenericQuestions?.FirstOrDefault(x =>
                                        x.QuestionId == qs.FurtherQuestionId));
                }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    public void RefreshEndpoints()
    {
        ShowUpload = true;
    }
}
