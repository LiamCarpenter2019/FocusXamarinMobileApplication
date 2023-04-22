namespace FocusXamarinMobileApplication.ViewModels;

public class InputMeasurePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly PhotoSelectionPageViewModel _psvm;
    private double qty;


    public InputMeasurePageViewModel()
    {
        _assigmentService = new Assignments();

        _jobService = new Jobs();

        _userService = new Users();

        _psvm = new PhotoSelectionPageViewModel();
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


    public Assignments _assigmentService { get; }
    public Jobs _jobService { get; }
    public Users _userService { get; }

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

    public bool _showHeaderLabel { get; set; }

    public bool ShowHeaderLabel
    {
        get => _showHeaderLabel;
        set
        {
            _showHeaderLabel = value;
            OnPropertyChanged();
        }
    }

    public bool _showHeaderPicker { get; set; }

    public bool ShowHeaderPicker
    {
        get => _showHeaderPicker;
        set
        {
            _showHeaderPicker = value;
            OnPropertyChanged();
        }
    }

    public bool _lenghtWidthDepthIsVisible { get; set; }

    public bool LenghtWidthDepthIsVisible
    {
        get => _lenghtWidthDepthIsVisible;
        set
        {
            _lenghtWidthDepthIsVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _photButtoVisible { get; set; }

    public bool PhotButtoVisible
    {
        get => _photButtoVisible;
        set
        {
            _photButtoVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _showEndpoints { get; set; }

    public bool ShowEndpoints
    {
        get => _showEndpoints;
        set
        {
            _showEndpoints = value;
            OnPropertyChanged();
        }
    }

    public bool _showPoles { get; set; }

    public bool ShowPoles
    {
        get => _showPoles;
        set
        {
            _showPoles = value;
            OnPropertyChanged();
        }
    }

    public bool _showRuns { get; set; }

    public bool ShowRuns
    {
        get => _showRuns;
        set
        {
            _showRuns = value;
            OnPropertyChanged();
        }
    }

    public long _startReading { get; set; }

    public long StartReading
    {
        get => _startReading;
        set
        {
            _startReading = value;
            OnPropertyChanged();
        }
    }

    public long _endReading { get; set; }

    public long EndReading
    {
        get => _endReading;
        set
        {
            _endReading = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ProjectWorks> _measureList { get; set; }

    public ObservableCollection<ProjectWorks> MeasureList
    {
        get => _measureList;
        set
        {
            _measureList = value;
            OnPropertyChanged();
        }
    }


    public VMexpansionReleaseData _selectedPremises { get; set; }

    public VMexpansionReleaseData SelectedPremises
    {
        get => _selectedPremises;
        set
        {
            _selectedPremises = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<VMexpansionReleaseData> _listOfPremises { get; set; } = new();

    public ObservableCollection<VMexpansionReleaseData> ListOfPremises
    {
        get => _listOfPremises;
        set
        {
            _listOfPremises = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<VMexpansionReleaseData> _selectedPoleList { get; set; } = new();

    public ObservableCollection<VMexpansionReleaseData> SelectedPoleList
    {
        get => _selectedPoleList;
        set
        {
            _selectedPoleList = value;
            OnPropertyChanged();
        }
    }

    public ProjectWorks _selectedRate { get; set; }

    public ProjectWorks SelectedRate
    {
        get => _selectedRate;
        set
        {
            _selectedRate = value;
            ClaimHeader = _selectedRate?.Header;
            BaseUnit = _selectedRate?.BaseUnit;
            ClaimDesc = _selectedRate?.Description;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<CableStockAudit> _listOfCableStockAudit { get; set; } = new();

    public ObservableCollection<CableStockAudit> ListOfCableStockAudit
    {
        get => _listOfCableStockAudit;
        set
        {
            _listOfCableStockAudit = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<CableStockAudit> _listOfCableStockAuditFiltered { get; set; } = new();

    public ObservableCollection<CableStockAudit> ListOfCableStockAuditFiltered
    {
        get => _listOfCableStockAuditFiltered;
        set
        {
            _listOfCableStockAuditFiltered = value;
            OnPropertyChanged();
        }
    }

    public CableStockAudit _selectedCableStockAudit { get; set; }

    public CableStockAudit SelectedCableStockAudit
    {
        get => _selectedCableStockAudit;
        set
        {
            _selectedCableStockAudit = value;

            if (_selectedCableStockAudit != null && NavigationalParameters.CurrentSelectedJob != null)
                SelectedCableStockUse = new CableStockUse
                {
                    ContractID = NavigationalParameters.CurrentSelectedJob?.BaseContractId.ToString(),
                    Date = DateTime.Now,
                    ContractName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                    RemoteTableId = "0",
                    UsedByNBame = NavigationalParameters.LoggedInUser.FullName,
                    UsedById = NavigationalParameters.LoggedInUser.FocusId,
                    CableType = _selectedCableStockAudit?.CableType,
                    CoreCount = _selectedCableStockAudit.FibreCoreCount,
                    DrumNo = _selectedCableStockAudit.DrumNo,
                    ExpectedStartReading = _selectedCableStockAudit.InStock
                };

            OnPropertyChanged();
        }
    }


    public string _searchText { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;

            OnPropertyChanged();
        }
    }


    public CableStockUse _selectedCableStockUse { get; set; }

    public CableStockUse SelectedCableStockUse
    {
        get => _selectedCableStockUse;
        set
        {
            _selectedCableStockUse = value;

            OnPropertyChanged("SelectedCableStockUses");
        }
    }

    public ObservableCollection<CableRuns> _cableRuns { get; set; } = new();

    public ObservableCollection<CableRuns> CableRuns
    {
        get => _cableRuns;
        set
        {
            _cableRuns = value;

            OnPropertyChanged();
        }
    }

    public CableRuns _selectedCableRun { get; set; }

    public CableRuns SelectedCableRun
    {
        get => _selectedCableRun;
        set
        {
            _selectedCableRun = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<CableStockUse> _selectedCableStockUseList { get; set; } = new();

    public ObservableCollection<CableStockUse> SelectedCableStockUseList
    {
        get => _selectedCableStockUseList;
        set
        {
            _selectedCableStockUseList = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<ProjectWorks> _listOfRates { get; set; }

    public ObservableCollection<ProjectWorks> ListOfRates
    {
        get => _listOfRates;
        set
        {
            _listOfRates = value;
            OnPropertyChanged();
        }
    }

    public ClaimedFile ClaimedFile { get; set; }

    public ClaimedFile _claimedFile
    {
        get => _claimedFile;
        set
        {
            _claimedFile = value;
            OnPropertyChanged("ClaimedFile");
        }
    }

    public string _claimHeader { get; set; }

    public string ClaimHeader
    {
        get => _claimHeader;
        set
        {
            _claimHeader = value;
            OnPropertyChanged();
        }
    }

    public string _claimDesc { get; set; }

    public string ClaimDesc
    {
        get => _claimDesc;
        set
        {
            _claimDesc = value;
            OnPropertyChanged();
        }
    }

    public string _baseUnit { get; set; }

    public string BaseUnit
    {
        get => _baseUnit;
        set
        {
            _baseUnit = value;
            OnPropertyChanged();
        }
    }

    public string _qty { get; set; } = "0";

    public string Qty
    {
        get => _qty;
        set
        {
            _qty = value;
            OnPropertyChanged();
        }
    }

    public string _width { get; set; } = "0.00";

    public string Width
    {
        get => _width;
        set
        {
            _width = value;
            OnPropertyChanged();
        }
    }

    public string _depth { get; set; } = "0.00";

    public string Depth
    {
        get => _depth;
        set
        {
            _depth = value;
            OnPropertyChanged();
        }
    }

    public string _qtyName { get; set; } = "Qty";

    public string QtyName
    {
        get => _qtyName;
        set
        {
            _qtyName = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = false;
        ShowSection6 = false;
        ShowHeaderPicker = false;
        ShowHeaderLabel = true;
        ShowEndpoints = false;
        ShowRuns = false;
        LenghtWidthDepthIsVisible = false;
        PhotButtoVisible = false;

        ClaimedFile = NavigationalParameters.ClaimFile;

        SearchText = "";

        if (ClaimedFile?.ClaimQty > 0)
            Qty = ClaimedFile?.ClaimQty.ToString();
        else
            Qty = "";

        Width = "";

        Depth = "";

        StartReading = 0;

        EndReading = 0;

        if (SelectedCableStockUseList == null) SelectedCableStockUseList = new ObservableCollection<CableStockUse>();

        if (NavigationalParameters.ClaimFile?.MeasureEndPoints != null &&
            NavigationalParameters.ClaimFile.MeasureEndPoints.Any())
        {
            SelectedPoleList = new ObservableCollection<VMexpansionReleaseData>();
            foreach (var ep in NavigationalParameters.ClaimFile.MeasureEndPoints)
            {
                var pole = App.Database.GetItems<VMexpansionReleaseData>()
                    .FirstOrDefault(x => x.ClaimId.ToString() == ep.ToString());

                if (SelectedPoleList.All(x => x.ClaimId != pole.ClaimId)) SelectedPoleList.Add(pole);
            }
        }

        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                try
                {
                    ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

                    ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

                    if (NavigationalParameters.ClaimType.ToLower() == "cabling")
                    {
                        ShowSection5 = true;

                        ShowRuns = true;

                        ShowSection6 = true;

                        ListOfCableStockAuditFiltered = ListOfCableStockAudit =
                            new ObservableCollection<CableStockAudit>(_jobService.GetCableAudit());


                        if (SelectedCableRun != null)
                            SelectedCableStockUseList = new ObservableCollection<CableStockUse>(_jobService
                                .GetCableStockAuditMeasures().Where(x =>
                                    x.CableRunIdentifier == SelectedCableRun.CableRunIdentifier));

                        if (SelectedCableRun == null)
                            CableRuns = new ObservableCollection<CableRuns>(
                                _jobService.GetCableRuns(NavigationalParameters.CurrentSelectedJob.QuoteNumber));

                        if (CableRuns == null || !CableRuns.Any())
                        {
                            ShowRuns = false;
                            ShowSection5 = false;
                            ShowSection6 = false;
                        }
                        else
                        {
                            ShowRuns = true;
                            ShowSection5 = true;
                            ShowSection6 = true;
                        }

                        if (NavigationalParameters.CurrentRate?.Header == "7" &&
                            NavigationalParameters.CurrentRate?.Description.ToLower() == "cabling inside buildings")
                        {
                            ListOfCableStockAuditFiltered = new ObservableCollection<CableStockAudit>(_jobService
                                .GetCableAudit().Where(x => x.CableType.ToLower().Contains("internal")));
                            ListOfPremises = new ObservableCollection<VMexpansionReleaseData>(_jobService
                                .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                                .Where(x => x.BuildingStandard.ToLower() == "premises"));
                            ShowSection5 = true;
                            ShowSection6 = true;
                            ShowEndpoints = true;
                            ShowRuns = false;
                        }

                        SelectedCableStockUse ??= new CableStockUse
                        {
                            ContractID = NavigationalParameters.CurrentSelectedJob.BaseContractId.ToString(),
                            Date = DateTime.Now,
                            ContractName = NavigationalParameters.CurrentSelectedJob.ProjectName,
                            RemoteTableId = "0",
                            UsedByNBame = NavigationalParameters.LoggedInUser.FullName,
                            UsedById = NavigationalParameters.LoggedInUser.FocusId
                        };
                    }
                    else if (NavigationalParameters.ClaimType.ToLower() == "leadin")
                    {
                        if (NavigationalParameters.SelectedEndPoint != null)
                            ListOfPremises = new ObservableCollection<VMexpansionReleaseData>(_jobService
                                .GetEndpoints(NavigationalParameters.CurrentSelectedJob)?
                                .Where(x => x.BuildingStandard.ToLower() == "premises"));

                        ShowEndpoints = true;
                        ShowRuns = false;
                    }
                    else if (NavigationalParameters.ClaimType.ToLower() == "pole" ||
                             NavigationalParameters.ClaimType.ToLower() == "chamber")
                    {

                        LenghtWidthDepthIsVisible = true;
                        QtyName = "Qty";
                        ShowSection4 = true;
                        PhotButtoVisible = true;
                    }
                    else if (NavigationalParameters.ClaimType.ToLower() == "Overhead Cable")
                    {
                        ShowSection4 = false;
                        PhotButtoVisible = true;
                        //  if (NavigationalParameters.SelectedAddress != null)
                        //{
                        ListOfPremises = new ObservableCollection<VMexpansionReleaseData>(_jobService
                            .GetEndpoints(NavigationalParameters.CurrentSelectedJob)
                            ?.Where(x => x.BuildingStandard?.ToLower() == "Overhead Cable"));
                        //}
                        ShowPoles = true;
                    }

                    SelectedPremises = NavigationalParameters.SelectedEndPoint;

                    ClaimedFile.ClaimHeader = NavigationalParameters.CurrentRate?.Header;

                    ClaimedFile.ClaimDesc = NavigationalParameters.CurrentRate?.Description;

                    ClaimedFile.BaseUnit = NavigationalParameters.CurrentRate?.BaseUnit;

                    if (!string.IsNullOrEmpty(Depth))
                        ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);
                    else
                        ClaimedFile.ClaimDepth = 0;

                    ClaimedFile.SynCode =
                        NavigationalParameters.CurrentRate?.RemoteTableId.ToString() ?? "0";
                }
                catch (Exception ex)
                {
                    await Alert("Error",
                        "There has been an error retrieveing the data please contact support should this continue",
                        "Ok");
                }

                ClaimHeader = ClaimedFile?.ClaimHeader;
                ClaimDesc = ClaimedFile?.ClaimDesc;
                BaseUnit = ClaimedFile?.BaseUnit;
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                ProjectName = NavigationalParameters.SelectedTaskItem?.ProjectName;
                ProjectDate = NavigationalParameters.SelectedTaskItem?.JobDate.ToString("dd/MM/yyyy");

                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
                {
                    SelectedRate = NavigationalParameters.SelectedWorkItem;
                    ShowHeaderLabel = true;
                    ShowHeaderPicker = false;
                    ClaimHeader = SelectedRate?.Header;
                    ClaimDesc = SelectedRate?.Description;
                    BaseUnit = SelectedRate?.BaseUnit;
                }
                else
                {
                    if (ClaimedFile != null)
                    {
                        ClaimHeader = ClaimedFile?.ClaimHeader; // = navParam2.ClaimHeader;

                        ClaimDesc = ClaimedFile?.ClaimDesc; // = navParam2.ClaimDesc;
                    }

                    switch (NavigationalParameters.SupervisorAction)
                    {
                        case NavigationalParameters.SupervisorMeasureAction.MODIFY:
                            ShowHeaderLabel = true;
                            ShowHeaderPicker = false;
                            ClaimHeader = ClaimedFile?.ClaimHeader;
                            BaseUnit = ClaimedFile?.BaseUnit;
                            ClaimDesc = ClaimedFile?.ClaimDesc;

                            if (!string.IsNullOrEmpty(Width))
                                ClaimedFile.ClaimWidth = Convert.ToDecimal(Width);
                            else
                                ClaimedFile.ClaimWidth = 0;

                            if (!string.IsNullOrEmpty(Depth))
                                ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);
                            else
                                ClaimedFile.ClaimDepth = 0;
                            break;
                        case NavigationalParameters.SupervisorMeasureAction.MODIFY_LATERAL:
                            ClaimHeader = ClaimedFile?.ClaimHeader;
                            BaseUnit = ClaimedFile?.BaseUnit;
                            ClaimDesc = ClaimedFile?.ClaimDesc;
                            if (!string.IsNullOrEmpty(Width))
                                ClaimedFile.ClaimWidth = Convert.ToDecimal(Width);
                            else
                                ClaimedFile.ClaimWidth = 0;

                            if (!string.IsNullOrEmpty(Depth))
                                ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);
                            else
                                ClaimedFile.ClaimDepth = 0;
                            ShowHeaderLabel = true;
                            ShowHeaderPicker = false;
                            LenghtWidthDepthIsVisible = true;
                            break;
                        case NavigationalParameters.SupervisorMeasureAction.ADD_MEASURE:
                            ShowHeaderLabel = false;
                            ShowHeaderPicker = true;
                            LenghtWidthDepthIsVisible = false;

                            ListOfRates = new ObservableCollection<ProjectWorks>(_assigmentService
                                .GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
                                .Where(x => x.AssignmentId == Guid.Empty
                                            && x.Category != "Supervisor"
                                            && x.Category != "Commercial")
                                .Select(i => new ProjectWorks
                                {
                                    RemoteTableId = i.RemoteTableId,
                                    BaseUnit = i?.BaseUnit,
                                    Description = i?.Description,
                                    Header = i?.Header,
                                    QuoteId = i.QuoteId,
                                    Category = i?.Category.ToUpper()
                                }).ToList());
                            break;
                        case NavigationalParameters.SupervisorMeasureAction.ADD_SUPER_CLAIM:
                            ShowHeaderLabel = false;
                            ShowHeaderPicker = true;
                            LenghtWidthDepthIsVisible = false;
                            ListOfRates = new ObservableCollection<ProjectWorks>(_assigmentService
                                .GetRates(NavigationalParameters.SelectedTaskItem.QuoteNumber, "Contract")
                                .Where(x => x.AssignmentId == Guid.Empty
                                            && x.Category.ToLower() == "supervisor")
                                .Select(i => new ProjectWorks
                                {
                                    RemoteTableId = i.RemoteTableId,
                                    BaseUnit = i?.BaseUnit,
                                    Description = i?.Description,
                                    Header = i?.Header,
                                    QuoteId = i.QuoteId,
                                    Category = i?.Category.ToUpper(),
                                    Qty = ""
                                }).ToList());
                            break;
                    }


                    SelectedPremises = NavigationalParameters.SelectedEndPoint;
                    ClaimHeader = ClaimedFile?.ClaimHeader;
                    ClaimDesc = ClaimedFile?.ClaimDesc;
                    BaseUnit = ClaimedFile?.BaseUnit;
                }

                break;
        }
    });

    public RelayCommand SelectedMeasure => new(async () =>
    {
        NavigationalParameters.SelectedWorkItem = SelectedRate;
        ClaimDesc = SelectedRate.Description;
        ClaimHeader = SelectedRate.Header;
        BaseUnit = SelectedRate.BaseUnit;
    });

    public RelayCommand FilterDrumList => new(async () =>
    {
        if (!string.IsNullOrEmpty(SearchText))
        {
            var filteredList = _jobService.GetCableAudit()?.Where(x =>
                x.DrumNo.ToLower().Contains(SearchText?.ToLower()) ||
                x.CableType.ToLower().Contains(SearchText?.ToLower()) ||
                x.Name.ToLower().Contains(SearchText?.ToLower())).ToList();

            if (filteredList != null)
                ListOfCableStockAuditFiltered = new ObservableCollection<CableStockAudit>(filteredList);
        }
    });

    public RelayCommand AddCableMeasure => new(async () =>
    {
        try
        {
            if (StartReading < EndReading)
            {
                await Alert("End reading cannot be higher than the start reading!!", "Error!");
            }
            else
            {
                if (SelectedCableRun == null && NavigationalParameters.ClaimType.ToLower() == "cabling" &&
                    ClaimedFile.ClaimHeader != "7")
                {
                    await Alert("Please select the cable run before adding this measure!", "Error!");
                }
                else if (SelectedPremises == null && NavigationalParameters.ClaimType.ToLower() == "cabling" &&
                         ClaimedFile.ClaimHeader == "7")
                {
                    await Alert("Please select the premises before adding this measure!", "Error!");
                }
                else
                {
                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                        NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                        SelectedCableStockUse.ContractID =
                            NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString();


                    SelectedCableStockUse.HowMuchUsed = Convert.ToInt64(StartReading - EndReading);

                    SelectedCableStockUse.ClaimId = ClaimedFile.ClaimId;

                    SelectedCableStockUse.StartReading = Convert.ToInt64(StartReading);

                    SelectedCableStockUse.EndReading = Convert.ToInt64(EndReading);

                    SelectedCableStockUse.ExpectedStartReading = SelectedCableStockAudit.InStock;

                    SelectedCableStockUse.CableRunIdentifier =
                        NavigationalParameters.ClaimType?.ToLower() == "cabling"
                            ? SelectedCableRun?.CableRunIdentifier
                            : SelectedPremises?.ClaimId.ToString();

                    SelectedCableStockUseList.Add(SelectedCableStockUse);

                    SelectedCableStockAudit.InStock =
                        SelectedCableStockAudit.InStock - SelectedCableStockUse.HowMuchUsed;

                    _jobService.SaveCableAudit(SelectedCableStockAudit);

                    StartReading = 0;

                    EndReading = 0;

                    SelectedCableStockUse = null;

                    SelectedCableStockAudit = null;

                    //SelectedCableRun = null;

                    ListOfCableStockAudit =
                        new ObservableCollection<CableStockAudit>(_jobService.GetCableAudit().Where(x => x.Tested));

                    SelectedCableStockUseList = new ObservableCollection<CableStockUse>(SelectedCableStockUseList);
                }
            }
        }
        catch (Exception ex)
        {
            await Alert("There has been a error whilst creating the cable audit measure!!", "Error!");
        }
    });

    public RelayCommand PictureCommand => new(async () =>
    {
        try
        {
            if (string.IsNullOrEmpty(Qty)) Qty = "0";

            var isIntQty = Convert.ToInt32(Qty);

            ClaimedFile.ClaimQty = isIntQty;

            NavigationalParameters.SelectedEndPoint = SelectedPremises;

           //NavigationalParameters.SelectedCableRun = SelectedCableRun;

            NavigationalParameters.ClaimFile = ClaimedFile;

            switch (NavigationalParameters.AppMode)
            {
                case NavigationalParameters.AppModes.POLEMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.POLEMEASURE;
                    NavigationalParameters.ClaimFile.ClaimQty = 1;
                    NavigationalParameters.SelectedEndPoint.Status = NavigationalParameters.ClaimFile.ClaimHeader;
                    NavigationalParameters.SelectedEndPoint.SavedToServer = SelectedPremises.SavedToServer = false;
                    break;
                case NavigationalParameters.AppModes.CHAMBERMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.CHAMBERMEASURE;
                    break;
                case NavigationalParameters.AppModes.DUCTMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.DUCTMEASURE;
                    break;
                case NavigationalParameters.AppModes.CABLEMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.CABLEMEASURE;
                    break;
                case NavigationalParameters.AppModes.CIVILSMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.CIVILSMEASURE;
                    break;
                case NavigationalParameters.AppModes.LATERALMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.LATERALMEASURE;
                    break;
                case NavigationalParameters.AppModes.POLECABLEMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.POLECABLEMEASURE;
                    break;
                case NavigationalParameters.AppModes.LEADINMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.LEADINMEASURE;
                    break;
                case NavigationalParameters.AppModes.UGCRPMEASURE:
                    NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.UGCRPMEASURE;
                    break;
            }

            ClaimedFile = NavigationalParameters.ClaimFile;

            _jobService.AddClaimedFile(ClaimedFile);

            if (NavigationalParameters.SelectedEndPoint != null)
                await _jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);

            _psvm.TakePhoto.Execute(null);
        }
        catch (Exception ex)
        {
            await Alert("Please ensure the input is of numerical value before proceeding", "Incorect Fomat");
        }
    });

    public RelayCommand Cancel => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER
            || NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
        {
            if (NavigationalParameters.ClaimFile != null
                && NavigationalParameters.SelectedEndPoint != null)
            {
                App.Database.DeleteItem(NavigationalParameters.ClaimFile);

                //App.Database.DeleteItem(NavigationalParameters.SelectedAddress);
            }

            await NavigateTo(ViewModelLocator.MeasureListPage);
        }
        else
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
            {
                NavigateBack();
            }
            else
            {
                NavigationalParameters.ReturnPage = "";

                await NavigateTo(ViewModelLocator.SupervisorMeasuresApprovalsPage);
            }
        }
    });

    public RelayCommand AddPole => new(async () =>
    {
        if (SelectedPremises != null)
        {
            if (SelectedPoleList.All(x => x.ClaimId != SelectedPremises.ClaimId))
            {
                SelectedPoleList.Add(SelectedPremises);
                SelectedPremises.SavedToServer = SelectedPremises.SavedToServer ? false : true;
                _jobService.AddPoleClaimed(SelectedPremises);
            }
            else
            {
                await Alert("Pole already added!!", "You can only add the pole once!");
            }
        }
        else
        {
            await Alert("Please select an endpoint!!",
                "Please select an endpoint which you would like to attach to this claim");
        }
    });

    public RelayCommand RemovePole => new(async () =>
    {
        if (SelectedPremises != null)
        {
            if (SelectedPoleList.Any(x => x.ClaimId == SelectedPremises.ClaimId))
            {
                SelectedPoleList.Remove(SelectedPoleList.FirstOrDefault(x => x.ClaimId == SelectedPremises.ClaimId));
                SelectedPremises.SavedToServer = SelectedPremises.SavedToServer ? false : true;
                _jobService.RemovePoleClaimed(SelectedPremises);
            }
            else
            {
                await Alert("No poles in teh list!!",
                    "Thi pole cannot be removed from the list as it is not a vailid item of the list");
            }
        }
        else
        {
            await Alert("Please select an endpoint!!",
                "Please select an endpoint which you would like to remove from this claim");
        }
    });

    public RelayCommand Submit => new(async () =>
    {
        //NavigationalParameters.ReturnPage = Locator.AddMeasureViewModelKey;
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Qty))
                        {
                            Qty = "0";
                        }
                        else
                        {

                             qty = Convert.ToDouble(Qty);
                        }

                        try
                        {
                            bool userChoice = true;


                            if (qty == 0)
                            {
                                userChoice = await Alert("Warning?", "The measure qty is 0! Would you like to proceed with claiming this value?", "Yes", "No");

                                if (userChoice)
                                {
                                    Analytics.TrackEvent(
              $"Error - Zero Value - Measure passed to the server contains zero value for {NavigationalParameters.SelectedAsset?.StreetName} {ClaimHeader}-{ClaimDesc} By {NavigationalParameters.LoggedInUser?.FullName}");
                                }
                            }

                            if (userChoice)
                            { 
                                ClaimedFile.ClaimQty = Convert.ToDecimal(qty);

                                ClaimedFile.BaseUnit = BaseUnit;

                                ClaimedFile.ClaimDesc = ClaimDesc;

                                ClaimedFile.ClaimHeader = ClaimHeader;

                                //else { await Alert("Please select a premise to allocate the lead in measure!", "Error!"); }

                                SearchText = "";

                                if (NavigationalParameters.ClaimType.ToLower() == "cabling")
                                {
                                    try
                                    {
                                        if (ClaimedFile.ClaimHeader == "7") ClaimedFile.ClaimId = SelectedPremises.ClaimId;

                                        _jobService.AddMeasure(ClaimedFile);

                                        foreach (var item in SelectedCableStockUseList)
                                        {
                                            item.ClaimId = ClaimedFile.ClaimId;

                                            await _jobService.AddCableStockUse(item);
                                        }

                                        await NavigateTo(ViewModelLocator.MeasureListPage);
                                    }
                                    catch (Exception ex)
                                    {
                                        await Alert("Failed to save cable audit measure!!", "Error!");
                                    }
                                }
                                else if (NavigationalParameters.ClaimType.ToLower() == "leadin")
                                {
                                    if (SelectedPremises == null)
                                    {
                                        await Alert("Please select a premises to book this lead in measure against!", "Error!");
                                    }
                                    else
                                    {
                                        ClaimedFile.ClaimId = SelectedPremises.ClaimId;

                                        _jobService.AddMeasure(ClaimedFile);

                                        await NavigateTo(ViewModelLocator.MeasureListPage);
                                    }
                                }
                                else if (NavigationalParameters.ClaimType.ToLower() == "pole" ||
                                NavigationalParameters.ClaimType.ToLower() == "chamber" ||
                                NavigationalParameters.ClaimType.ToLower() == "distribution point" ||
                                NavigationalParameters.ClaimType.ToLower() == "joint enclosure")
                                {
                                    //ClaimedFile.ClaimQty = Qty;
                                    NavigationalParameters.ClaimFile = ClaimedFile;
                                    _jobService.AddMeasure(ClaimedFile);
                                    await NavigateTo(ViewModelLocator.MeasureListPage);
                                }
                                else if (NavigationalParameters.ClaimType.ToLower() == "pc")
                                {
                                    if (SelectedPoleList.Any() || NavigationalParameters.SelectedEndPoint != null)
                                    {
                                        ClaimedFile.ClaimQty = 1;
                                        NavigationalParameters.ClaimFile = ClaimedFile;
                                        _jobService.AddMeasure(ClaimedFile);
                                        await NavigateTo(ViewModelLocator.MeasureListPage);
                                    }
                                    else
                                    {
                                        await Alert("No poles selected!!",
                                            "Please select a pole which you would like to attach to this claim");
                                    }
                                }
                                else
                                {
                                    _jobService.AddMeasure(ClaimedFile);
                                    await NavigateTo(ViewModelLocator.MeasureListPage);
                                }

                                if (NavigationalParameters.SelectedEndPoint != null)
                                {
                                    NavigationalParameters.SelectedEndPoint.Status =
                                        NavigationalParameters.ClaimFile.ClaimHeader;
                                    await _jobService.AddEndPoint(NavigationalParameters.SelectedEndPoint);
                                }

                                if (NavigationalParameters.SelectedAsset != null)
                                {
                                    NavigationalParameters.SelectedAsset.Status =
                                        NavigationalParameters.ClaimFile.ClaimHeader;
                                    await _jobService.AddEndPoint(NavigationalParameters.SelectedAsset);
                                }

                                try
                                {
                                    SubmitPresiteCheck();
                                }
                                catch (Exception ex)
                                {
                                    await Alert("Failed to upload surevy please try again later!",
                                          "You will need to upload from the asset screen to ensure the survey is pushed");
                                }
                              

                                NavigationalParameters.PreSiteQuestions.Clear();
                            }
                        }
                        catch (Exception ex)
                        {
                            await Alert("A value of type decimal must be entered before submitting!", "Error!");
                        }
                    }
                    catch (Exception ex)
                    {
                        await Alert(
                            "There has been an error submiting this measure, Please check the input is a numerical value! should this issue persist please contact support!",
                            "Error!");
                    }
                }
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                {
                    try
                    {
                        var qty = Convert.ToDecimal(Qty);

                        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
                        {
                            if (!string.IsNullOrEmpty(Qty))
                            {
                                NavigationalParameters.SelectedWorkItem.Qty = Qty;
                                NavigationalParameters.SelectedWorkItem.AssignmentId =
                                    (Guid)NavigationalParameters.CurrentCvi.AssignmentId;
                                NavigationalParameters.SelectedWorkItem.Identifier =
                                    NavigationalParameters.CurrentCvi.CviId;
                                NavigationalParameters.SelectedWorkItem.Category =
                                    NavigationalParameters.AppMode.ToString();
                                await _assigmentService.AddRate(NavigationalParameters.SelectedWorkItem);
                                await NavigateTo(ViewModelLocator.RaiseCviMeasurePage);
                            }
                            else
                            {
                                await Alert("A value must be entered before submitting this claim!", "Error!");
                            }
                        }
                        else
                        {
                            switch (NavigationalParameters.SupervisorAction)
                            {
                                case NavigationalParameters.SupervisorMeasureAction.ADD_MEASURE:
                                    ClaimedFile = new ClaimedFile();
                                    //ClaimedFile.ApprovedBySupervisorName =
                                    //     NavigationalParameters.LoggedInUser?.FullName;
                                    ClaimedFile.ClaimQty = Convert.ToDecimal(Qty);
                                    ClaimedFile.QuoteId = NavigationalParameters.SelectedTaskItem?.QuoteNumber.ToString();
                                    ClaimedFile.ContractId = NavigationalParameters.SelectedTaskItem.ContractNumber;
                                    ClaimedFile.PostedByGanger = DateTime.Parse("02/02/1900 00:00:00");
                                    ClaimedFile.PostedByGangerName =
                                        NavigationalParameters.SelectedTaskItem?.GangLeaderName;
                                    ClaimedFile.ApprovedBySupervisor = DateTime.Parse("01/01/1900 00:00:00");
                                    if (!string.IsNullOrEmpty(Width)) ClaimedFile.ClaimWidth = Convert.ToDecimal(Width);
                                    if (!string.IsNullOrEmpty(Depth)) ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);

                                    ClaimedFile.SupervisorChanges =
                                        $"This Measure Data has been input by Supervisor: {ClaimedFile?.ApprovedBySupervisorName} on {DateTime.Now}, Qty input by Supervisor is {Qty}";
                                    break;
                                case NavigationalParameters.SupervisorMeasureAction.MODIFY:
                                    if (string.IsNullOrEmpty(ClaimedFile.OriginalClaimQtyByGang))
                                        ClaimedFile.OriginalClaimQtyByGang =
                                            ClaimedFile?.ClaimQty.ToString();
                                    //ClaimedFile.ApprovedBySupervisorName =
                                    //     NavigationalParameters.LoggedInUser?.FullName;
                                    ClaimedFile.ClaimQty = qty;
                                    ClaimedFile.SupervisorChanges =
                                        $"Measure Data has been changed on {DateTime.Now} by Supervisor: {ClaimedFile?.ApprovedBySupervisorName} on his IPad, old qty was {ClaimedFile?.OriginalClaimQtyByGang}, New Qty is {ClaimedFile?.ClaimQty}";

                                    if (!string.IsNullOrEmpty(Width)) ClaimedFile.ClaimWidth = Convert.ToDecimal(Width);
                                    if (!string.IsNullOrEmpty(Depth)) ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);
                                    break;
                                case NavigationalParameters.SupervisorMeasureAction.MODIFY_LATERAL:
                                    ClaimedFile.OriginalClaimQtyByGang +=
                                        $",{ClaimedFile?.ClaimWidth}";
                                    ClaimedFile.ClaimQty = qty;
                                    //ClaimedFile.ApprovedBySupervisorName =
                                    //     NavigationalParameters.LoggedInUser?.FullName;
                                    ClaimedFile.SupervisorChanges =
                                        $"Measure Data has been changed on {DateTime.Now} by Supervisor: {ClaimedFile?.ApprovedBySupervisorName} on his IPad, old qty was {ClaimedFile?.OriginalClaimQtyByGang}, New Qty is {ClaimedFile?.ClaimQty}";
                                    if (!string.IsNullOrEmpty(Width)) ClaimedFile.ClaimWidth = Convert.ToDecimal(Width);
                                    if (!string.IsNullOrEmpty(Depth)) ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);
                                    break;
                                case NavigationalParameters.SupervisorMeasureAction.ADD_SUPER_CLAIM:
                                    ClaimedFile = new ClaimedFile();
                                    // ClaimedFile.OriginalClaimQtyByGang += $",{ ClaimedFile?.ClaimWidth}";
                                    // ClaimedFile.ClaimWidth = d;
                                    //ClaimedFile.ApprovedBySupervisorName =
                                    //    NavigationalParameters.LoggedInUser?.FullName;
                                    ClaimedFile.QuoteId = NavigationalParameters.SelectedTaskItem?.QuoteNumber.ToString();
                                    ClaimedFile.ContractId = NavigationalParameters.SelectedTaskItem.ContractNumber;
                                    ClaimedFile.SupervisorChanges =
                                        $"This Measure Data has been input by Supervisor {ClaimedFile?.ApprovedBySupervisorName} on {DateTime.Now}, Qty input by Supervisor is {Qty}";
                                    ClaimedFile.ClaimQty = qty;
                                    ClaimedFile.PostedByGanger = DateTime.Parse("02/02/1900 00:00:00");
                                    ClaimedFile.PostedByGangerName =
                                        NavigationalParameters.SelectedTaskItem?.GangLeaderName;
                                    ClaimedFile.ClaimGang =
                                        NavigationalParameters.SelectedTaskItem.GangLeaderId.ToString();
                                    ClaimedFile.ApprovedBySupervisor = DateTime.Now;
                                    if (!string.IsNullOrEmpty(Width)) ClaimedFile.ClaimWidth = Convert.ToDecimal(Width);
                                    if (!string.IsNullOrEmpty(Depth)) ClaimedFile.ClaimDepth = Convert.ToDecimal(Depth);
                                    break;
                            }

                            ClaimedFile.ContractReference =
                                NavigationalParameters.SelectedTaskItem?.ContractReference;

                            ClaimedFile.QuoteId =
                                NavigationalParameters.SelectedTaskItem?.QuoteNumber.ToString();

                            ClaimedFile.ContractId =
                                NavigationalParameters.SelectedTaskItem?.ContractNumber;

                            ClaimedFile.ConPrefix = NavigationalParameters.SelectedTaskItem?.ContractPrefix;

                            ClaimedFile.ClaimSupervisor =
                                NavigationalParameters.SelectedTaskItem?.SupervisorId.ToString();

                            ClaimedFile.ClaimGang =
                                NavigationalParameters.SelectedTaskItem?.GangLeaderId.ToString();

                            ClaimedFile.ClaimDate = NavigationalParameters.SelectedTaskItem.JobDate;

                            if (SelectedRate != null)
                            {
                                ClaimedFile.ClaimHeader = SelectedRate?.Header;

                                ClaimedFile.ClaimDesc = SelectedRate?.Description;

                                ClaimedFile.BaseUnit = SelectedRate?.BaseUnit;
                            }

                            _jobService.AddClaimedFile(ClaimedFile);

                            await NavigateTo(ViewModelLocator.SupervisorMeasuresApprovalsPage);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Alert("Please Input a Decimal Value!", "Error!");
                    }
                }
                break;
        }

        SelectedCableStockUse = null;
        SelectedPremises = null;
        SelectedCableRun = null;
    });

    private async void SubmitPresiteCheck()
    {
        try
        {

            var wa = new WebApi();

            var areWeConnected = await wa.CanWeConnect2Api();

            var dataPassedToserver = new DataPassed2Server();

            var assignmentListToBerUploaded = new List<Assignment>();

            var jobService = new Jobs();


            if (areWeConnected)
            {
                dataPassedToserver.JobData = NavigationalParameters.CurrentSelectedJob;

                dataPassedToserver.Assignments = new List<Assignment>();

                assignmentListToBerUploaded = _assigmentService.GetAsBuiltAssignmentsToUpload();

                if (assignmentListToBerUploaded.Any())
                {
                    foreach (var assignment in assignmentListToBerUploaded)
                    {
                        var answers = (bool)App.Database.GetItems<SurveyAnswers>()?.Any(x =>
                            x.AssignmentId == assignment.AssignmentId && x.RemoteTableId == 0);

                        dataPassedToserver.Category = NavigationalParameters.SurveyType.ToString();

                        if (answers)
                        {
                            dataPassedToserver.Assignments.Add(_assigmentService.GenerateAssignments2SaveById(assignment));
                        }
                        else
                        {
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

                            NavigateBack();
                        }

                        if (NavigationalParameters.AppMode.ToString().ToLower().Contains("presite")
                            || NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt")
                            || NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
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
                        await Alert("There is nothing to upload. ", "Upload  complete");
                    }
                }
                else
                {
                    await Alert("There is nothing to upload. ", "Upload  complete");
                }
            }
            else
            {
                await Alert("Please connect to a network and try again. The surveyhas been saved. ",
                    "No Connectivity");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await Alert("An issue has occured submitting the survey. This has been saved", "Error!");

        }
    }

   
}