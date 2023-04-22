using FocusXamarinMobileApplication.Views;

namespace FocusXamarinMobileApplication.Services;

public class ViewModelLocator
{
    public const string LogInPage = "LogInPage";
    public const string SettingsPage = "SettingsPage";
    public const string AddWitnessPage = "AddWitnessPage";
    public const string SiteInspectionRatingFailurePage = "SiteInspectionRatingFailurePage";
    public const string SiteInspectionResultPage = "SiteInspectionResultPage";
    public const string BlockageListPage = "BlockageListPage";
    public const string BlockagesInputPage = "BlockagesInputPage";
    public const string CalendarPage = "CalendarPage";
    public const string CameraViewPage = "CameraViewPage";
    public const string DailyChecksIssuePage = "DailyChecksIssuePage";
    public const string DailyDiaryPage = "DailyDiaryPage";
    public const string DailySiteInspectionPage = "DailySiteInspectionPage";
    public const string InvestigationDetailPage = "InvestigationDetailPage";
    public const string InvestigationDetail2Page = "InvestigationDetail2Page";
    public const string DesignMapPage = "DesignMapPage";
    public const string DiarySelectionPage = "DiarySelectionPage";
    public const string DocumentListingPage = "DocumentListingPage";
    public const string DocumentViewPage = "DocumentViewPage";
    public const string EditInjuredPersonPage = "EditInjuredPersonPage";
    public const string FormsMapPage = "FormsMapPage";
    public const string FormsCommentPage = "FormsCommentPage";
    public const string FuelConsumptionPage = "FuelConsumptionPage";
    public const string GangSelectPage = "GangSelectPage";
    public const string ImageEditorPage = "ImageEditorPage";
    public const string InputMeasurePage = "InputMeasurePage";
    public const string InvestigateDamagePage = "InvestigateDamagePage";
    public const string MainListPage = "MainListPage";
    public const string MapDrawingComboPage = "MapDrawingComboPage";
    public const string MapWithPinsPage = "MapWithPinsPage";
    public const string MeasureApprovalPage = "MeasureApprovalPage";
    public const string MeasureListPage = "MeasureListPage";
    public const string MeasureTypeSelectionPage = "MeasureTypeSelectionPage";
    public const string MenuSelectionPage = "MenuSelectionPage";
    public const string MethodologyPage = "MethodologyPage";
    public const string MyPlantScreenPage = "MyPlantScreenPage";
    public const string PermitPage = "PermitPage";
    public const string PhotoSelectionPage = "PhotoSelectionPage";
    public const string PhotoPage = "PhotoPage";
    public const string PlantChecksPage = "PlantChecksPage";
    public const string PlantDetailsPage = "PlantDetailsPage";
    public const string PlantIssuePage = "PlantIssuePage";
    public const string PlantTransferInPage = "PlantTransferInPage";
    public const string PlantTransferListPage = "PlantTransferListPage";
    public const string PlantTransferPage = "PlantTransferPage";
    public const string ProjectImagesPage = "ProjectImagesPage";
    public const string ProjectListPage = "ProjectListPage";
    public const string ProjectSummaryPage = "ProjectSummaryPage";
    public const string RaiseCviDetailsPage = "RaiseCviDetailsPage";
    public const string RaiseCviMeasurePage = "RaiseCviMeasurePage";
    public const string RatePage = "RatePage";
    public const string RegisterNewEventPage = "RegisterNewEventPage";
    public const string ReInstatementPage = "ReInstatementPage";
    public const string SelectDamageDetailsPage = "SelectDamageDetailsPage";
    public const string SelectInvestigationPage = "SelectInvestigationPage";
    public const string SelectPreSitePage = "SelectPreSitePage";
    public const string SelectProjectPage = "SelectProjectPage";
    public const string SelectStreetPage = "SelectStreetPage";

    public const string SelectSurveyTypePage = "SelectSurveyTypePage";
    public const string ServicesCrossedPage = "ServicesCrossedPage";
    public const string SignaturePage = "SignaturePage";
    public const string SupervisorDiaryPage = "SupervisorDiaryPage";
    public const string SupervisorMeasuresApprovalsPage = "SupervisorMeasuresApprovalsPage";
    public const string SupervisorProjectPage = "SupervisorProjectPage";
    public const string SurveyQuestionsPage = "SurveyQuestionsPage";
    public const string TaskListPage = "TaskListPage";
    public const string TeamOptionsPage = "TeamOptionsPage";
    public const string TimesheetPage = "TimesheetPage";
    public const string TimesheetSelectionPage = "TimesheetSelectionPage";
    public const string ToolboxTalkDocumentViewPage = "ToolboxTalkDocumentViewPage";
    public const string UtilityDamagesNavPage = "UtilityDamagesNavPage";
    public const string VisitorLogListPage = "VisitorLogListPage";
    public const string VisitorLogOutPage = "VisitorLogOutPage";
    public const string VisitorLogPage = "VisitorLogPage";
    public const string WeatherEventPage = "WeatherEventPage";
    public const string CommercialListPage = "CommercialListPage";
    public const string OrderBookItemPage = "OrderBookItemPage";
    public const string CalibrationPage = "CalibrationPage";
    public const string EventManagementSelectionPage = "EventManagementSelectionPage";
    public const string OrderListPage = "OrderListPage";
    public const string AdditionalRisksPage = "AdditionalRisksPage";
    public const string ProvingPage = "ProvingPage";
    public const string AddChamber = "AddChamber";
    public const string ProfilePage = "ProfilePage";
    public const string StockFuelPage = "StockFuelPage";
    public const string RatingSurveyQuestionsPage = "RatingSurveyQuestionsPage";
    public const string AsBuildMeasurePage = "AsBuildMeasurePage";
    public const string HybridWebViewPage = "HybridWebViewPage";
    public const string PdfViewPage = "PdfViewPage";
    public const string SelectEndPointPage = "SelectEndPointPage";
    public const string PhotoExamplePage = "PhotoExamplePage";


    public ViewModelLocator()
    {
        var navigation = new NavigationService();

        navigation.Configure(AddChamber, typeof(AddChamberPage));
        navigation.Configure(LogInPage, typeof(LogInPage));
        navigation.Configure(SettingsPage, typeof(SettingsPage));
        navigation.Configure(AddWitnessPage, typeof(AddWitnessPage));
        navigation.Configure(SiteInspectionRatingFailurePage, typeof(SiteInspectionRatingFailurePage));
        navigation.Configure(SiteInspectionResultPage, typeof(SiteInspectionResultPage));
        navigation.Configure(BlockageListPage, typeof(BlockageListPage));
        navigation.Configure(BlockagesInputPage, typeof(BlockagesInputPage));
        navigation.Configure(CalendarPage, typeof(CalendarPage));
        navigation.Configure(CameraViewPage, typeof(CameraViewPage));
        navigation.Configure(DailyChecksIssuePage, typeof(DailyChecksIssuePage));
        navigation.Configure(DailyDiaryPage, typeof(DailyDiaryPage));
        navigation.Configure(DailySiteInspectionPage, typeof(DailySiteInspectionPage));
        navigation.Configure(InvestigationDetailPage, typeof(InvestigationDetailPage));
        navigation.Configure(InvestigationDetail2Page, typeof(InvestigationDetail2Page));
        navigation.Configure(DesignMapPage, typeof(DesignMapPage));
        navigation.Configure(DiarySelectionPage, typeof(DiarySelectionPage));
        navigation.Configure(DocumentListingPage, typeof(DocumentListingPage));
        navigation.Configure(DocumentViewPage, typeof(DocumentViewPage));
        navigation.Configure(EditInjuredPersonPage, typeof(EditInjuredPersonPage));
        navigation.Configure(FormsMapPage, typeof(FormsMapPage));
        navigation.Configure(FuelConsumptionPage, typeof(FuelConsumptionPage));
        navigation.Configure(GangSelectPage, typeof(GangSelectPage));
        navigation.Configure(ImageEditorPage, typeof(ImageEditorPage));
        navigation.Configure(InputMeasurePage, typeof(InputMeasurePage));
        navigation.Configure(InvestigateDamagePage, typeof(InvestigateDamagePage));
        navigation.Configure(MainListPage, typeof(MainListPage));
        navigation.Configure(MapDrawingComboPage, typeof(MapDrawingComboPage));
        navigation.Configure(MapWithPinsPage, typeof(MapWithPinsPage));
        navigation.Configure(MeasureApprovalPage, typeof(MeasureApprovalPage));
        navigation.Configure(MeasureListPage, typeof(MeasureListPage));
        navigation.Configure(MeasureTypeSelectionPage, typeof(MeasureTypeSelectionPage));
        navigation.Configure(MenuSelectionPage, typeof(MenuSelectionPage));
        navigation.Configure(MethodologyPage, typeof(MethodologyPage));
        navigation.Configure(MyPlantScreenPage, typeof(MyPlantScreenPage));
        navigation.Configure(FormsCommentPage, typeof(FormsCommentPage));
        navigation.Configure(PermitPage, typeof(PermitPage));
        navigation.Configure(PhotoSelectionPage, typeof(PhotoSelectionPage));
        navigation.Configure(PhotoPage, typeof(PhotoPage));
        navigation.Configure(MapDrawingComboPage, typeof(MapDrawingComboPage));
        navigation.Configure(PlantChecksPage, typeof(PlantChecksPage));
        navigation.Configure(PlantDetailsPage, typeof(PlantDetailsPage));
        navigation.Configure(PlantIssuePage, typeof(PlantIssuePage));
        navigation.Configure(PlantTransferInPage, typeof(PlantTransferInPage));
        navigation.Configure(PlantTransferListPage, typeof(PlantTransferListPage));
        navigation.Configure(PlantTransferPage, typeof(PlantTransferPage));
        navigation.Configure(ProjectImagesPage, typeof(ProjectImagesPage));
        navigation.Configure(ProjectListPage, typeof(ProjectListPage));
        navigation.Configure(ProjectSummaryPage, typeof(ProjectSummaryPage));
        navigation.Configure(RaiseCviDetailsPage, typeof(RaiseCviDetailsPage));
        navigation.Configure(RaiseCviMeasurePage, typeof(RaiseCviMeasurePage));
        navigation.Configure(RatePage, typeof(RatePage));
        navigation.Configure(RegisterNewEventPage, typeof(RegisterNewEventPage));
        navigation.Configure(ReInstatementPage, typeof(ReInstatementPage));
        navigation.Configure(SelectDamageDetailsPage, typeof(SelectDamageDetailsPage));
        navigation.Configure(SelectInvestigationPage, typeof(SelectInvestigationPage));
        navigation.Configure(SelectPreSitePage, typeof(SelectPreSitePage));
        navigation.Configure(SelectProjectPage, typeof(SelectProjectPage));
        navigation.Configure(SelectStreetPage, typeof(SelectStreetPage));
        navigation.Configure(SelectSurveyTypePage, typeof(SelectSurveyTypePage));
        navigation.Configure(ServicesCrossedPage, typeof(ServicesCrossedPage));
        navigation.Configure(SignaturePage, typeof(SignaturePage));
        navigation.Configure(SupervisorDiaryPage, typeof(SupervisorDiaryPage));
        navigation.Configure(SupervisorMeasuresApprovalsPage, typeof(SupervisorMeasuresApprovalsPage));
        navigation.Configure(SupervisorProjectPage, typeof(SupervisorProjectPage));
        navigation.Configure(SurveyQuestionsPage, typeof(SurveyQuestionsPage));
        navigation.Configure(TaskListPage, typeof(TaskListPage));
        navigation.Configure(TeamOptionsPage, typeof(TeamOptionsPage));
        navigation.Configure(TimesheetPage, typeof(TimesheetPage));
        navigation.Configure(TimesheetSelectionPage, typeof(TimesheetSelectionPage));
        navigation.Configure(ToolboxTalkDocumentViewPage, typeof(ToolboxTalkDocumentViewPage));
        navigation.Configure(UtilityDamagesNavPage, typeof(UtilityDamagesNavPage));
        navigation.Configure(VisitorLogListPage, typeof(VisitorLogListPage));
        navigation.Configure(VisitorLogOutPage, typeof(VisitorLogOutPage));
        navigation.Configure(VisitorLogPage, typeof(VisitorLogPage));
        navigation.Configure(WeatherEventPage, typeof(WeatherEventPage));
        navigation.Configure(DailySiteInspectionPage, typeof(DailySiteInspectionPage));
        navigation.Configure(CommercialListPage, typeof(CommercialListPage));
        navigation.Configure(OrderBookItemPage, typeof(OrderBookItemPage));
        navigation.Configure(CalibrationPage, typeof(CalibrationPage));
        navigation.Configure(EventManagementSelectionPage, typeof(EventManagementSelectionPage));
        navigation.Configure(OrderListPage, typeof(OrderListPage));
        navigation.Configure(AdditionalRisksPage, typeof(AdditionalRisksPage));
        navigation.Configure(ProvingPage, typeof(ProvingPage));
        navigation.Configure(ProfilePage, typeof(ProfilePage));
        navigation.Configure(StockFuelPage, typeof(StockFuelPage));
        navigation.Configure(RatingSurveyQuestionsPage, typeof(RatingSurveyQuestionsPage));
        navigation.Configure(HybridWebViewPage, typeof(HybridWebViewPage));
        navigation.Configure(PdfViewPage, typeof(PdfViewPage));
        navigation.Configure(SelectEndPointPage, typeof(SelectEndPointPage));
        navigation.Configure(AsBuildMeasurePage, typeof(AsBuildMeasurePage));
        navigation.Configure(PhotoExamplePage, typeof(PhotoExamplePage));


        SimpleIoc.Default.Register<AddChamberPageViewModel>();
        SimpleIoc.Default.Register<AddHomesCviPageViewModel>();
        SimpleIoc.Default.Register<AddMeasurePageViewModel>();
        SimpleIoc.Default.Register<AddWitnessPageViewModel>();
        SimpleIoc.Default.Register<SiteInspectionRatingFailurePageViewModel>();
        SimpleIoc.Default.Register<SiteInspectionResultPageViewModel>();
        SimpleIoc.Default.Register<AuthorisationPageViewModel>();
        SimpleIoc.Default.Register<BaseViewModel>();
        SimpleIoc.Default.Register<BlockageInputPageViewModel>();
        SimpleIoc.Default.Register<BlockagePageViewModel>();
        SimpleIoc.Default.Register<CarouselViewModel>();
        SimpleIoc.Default.Register<DesignMapPage>();
        SimpleIoc.Default.Register<DailyChecksIssuePageViewModel>();
        SimpleIoc.Default.Register<DailyDiaryPageViewModel>();
        SimpleIoc.Default.Register<DfePageViewModel>();
        SimpleIoc.Default.Register<DiarySelectPageViewModel>();
        SimpleIoc.Default.Register<DocumentListingPageViewModel>();
        SimpleIoc.Default.Register<DocumentPageViewModel>();
        SimpleIoc.Default.Register<DocumentViewPageViewModel>();
        SimpleIoc.Default.Register<EditInjuredPersonPage>();
        SimpleIoc.Default.Register<FBaseViewModel>();
        SimpleIoc.Default.Register<FormsCommentPageViewModel>();
        SimpleIoc.Default.Register<FormsMapPageViewModel>();
        SimpleIoc.Default.Register<FuelConsumptionPageViewModel>();
        SimpleIoc.Default.Register<GangSelectPageViewModel>();
        SimpleIoc.Default.Register<ImageEditorPageViewModel>();
        SimpleIoc.Default.Register<EditInjuredPersonPageViewModel>();
        SimpleIoc.Default.Register<InputMeasurePageViewModel>();
        SimpleIoc.Default.Register<InvestigateDamagePageViewModel>();
        SimpleIoc.Default.Register<InvestigateDamagePageViewModel>();
        SimpleIoc.Default.Register<InvestigationDetail2PageViewModel>();
        SimpleIoc.Default.Register<LoginPageViewModel>();
        SimpleIoc.Default.Register<MainListPageViewModel>();
        SimpleIoc.Default.Register<MeasureApprovalPageViewModel>();
        SimpleIoc.Default.Register<MeasureListPageViewModel>();
        SimpleIoc.Default.Register<MeasureTypePageViewModel>();
        SimpleIoc.Default.Register<MenuSelectionPageViewModel>();
        SimpleIoc.Default.Register<MethodologyPageViewModel>();
        SimpleIoc.Default.Register<MyPlantScreenPageViewModel>();
        SimpleIoc.Default.Register<PermitPageViewModel>();
        SimpleIoc.Default.Register<PhotoSelectionPageViewModel>();
        SimpleIoc.Default.Register<PhotoPageViewModel>();
        SimpleIoc.Default.Register<PinItemsSourcePageViewModel>();
        SimpleIoc.Default.Register<PlantChecksPageViewModel>();
        SimpleIoc.Default.Register<PlantDetailsPageViewModel>();
        SimpleIoc.Default.Register<PlantIssuesPageViewModel>();
        SimpleIoc.Default.Register<PlantTansferInPageViewModel>();
        SimpleIoc.Default.Register<PlantTransferListPageViewModel>();
        SimpleIoc.Default.Register<PlantTansferPageViewModel>();
        SimpleIoc.Default.Register<ProjectImagesPageViewModel>();
        SimpleIoc.Default.Register<ProjectsPageViewModel>();
        SimpleIoc.Default.Register<ProjectSummaryPageViewModel>();
        SimpleIoc.Default.Register<RaiseCviDetailsPageViewModel>();
        SimpleIoc.Default.Register<RaiseCviMeasurePageViewModel>();
        SimpleIoc.Default.Register<RegisterNewEventPageViewModel>();
        SimpleIoc.Default.Register<ReInstatementPageViewModel>();
        SimpleIoc.Default.Register<SelectDamageDetailsPageViewModel>();
        SimpleIoc.Default.Register<SelectInvestigationPageViewModel>();
        SimpleIoc.Default.Register<SelectPreSitePageViewModel>();
        SimpleIoc.Default.Register<SelectProjectPageViewModel>();
        SimpleIoc.Default.Register<SelectStreetPageViewModel>();
        SimpleIoc.Default.Register<SelectSurveyTypePageViewModel>();
        SimpleIoc.Default.Register<ServicesCrossedPageViewModel>();
        SimpleIoc.Default.Register<SettingsPageViewModel>();
        SimpleIoc.Default.Register<SignaturePageViewModel>();
        SimpleIoc.Default.Register<SupervisorApprovalsPageViewModel>();
        SimpleIoc.Default.Register<SupervisorDiaryPageViewModel>();
        SimpleIoc.Default.Register<SupervisorMeasuresApprovalsPageViewModel>();
        SimpleIoc.Default.Register<SupervisorProjectPageViewModel>();
        SimpleIoc.Default.Register<SurveyQuestionsPageViewModel>();
        SimpleIoc.Default.Register<TaskListPageViewModel>();
        SimpleIoc.Default.Register<TeamOptionsPageViewModel>();
        SimpleIoc.Default.Register<TimesheetsPageViewModel>();
        SimpleIoc.Default.Register<TimesheetsSelectionPageViewModel>();
        SimpleIoc.Default.Register<ToolboxTalkDocumentPageViewModel>();
        SimpleIoc.Default.Register<UtilityDamagesNavPageViewModel>();
        SimpleIoc.Default.Register<VisitorLogListPageViewModel>();
        SimpleIoc.Default.Register<VisitorLogOutPageViewModel>();
        SimpleIoc.Default.Register<VisitorLogPageViewModel>();
        SimpleIoc.Default.Register<VisitorSignaturePageViewModel>();
        SimpleIoc.Default.Register<WeatherEventPageViewModel>();
        SimpleIoc.Default.Register<DailySiteInspectionPageViewModel>();
        SimpleIoc.Default.Register<CommercialListPageViewModel>();
        SimpleIoc.Default.Register<OrderBookItemPageViewModel>();
        SimpleIoc.Default.Register<CalibrationPageViewModel>();
        SimpleIoc.Default.Register<EventManagementSelectionPageViewModel>();
        SimpleIoc.Default.Register<OrderListPageViewModel>();
        SimpleIoc.Default.Register<AdditionalRisksPageViewModel>();
        SimpleIoc.Default.Register<ProvingPageViewModel>();
        SimpleIoc.Default.Register<ProfilePageViewModel>();
        SimpleIoc.Default.Register<StockFuelPageViewModel>();
        SimpleIoc.Default.Register<RatingSurveyQuestionsPageViewModel>();
        SimpleIoc.Default.Register<HybridWebViewPageViewModel>();
        SimpleIoc.Default.Register<SelectEndPointPageViewModel>();
        SimpleIoc.Default.Register<AsBuildMeasurePageViewModel>();
        SimpleIoc.Default.Register<PhotoExamplePageViewModel>();


        SimpleIoc.Default.Register(() => navigation);
    }

    public AsBuildMeasurePageViewModel AsBuildMeasurePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AsBuildMeasurePageViewModel>())
                SimpleIoc.Default.Register<AsBuildMeasurePageViewModel>();
            return SimpleIoc.Default.GetInstance<AsBuildMeasurePageViewModel>();
        }
    }

    public HybridWebViewPageViewModel HybridWebViewPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<HybridWebViewPageViewModel>())
                SimpleIoc.Default.Register<HybridWebViewPageViewModel>();
            return SimpleIoc.Default.GetInstance<HybridWebViewPageViewModel>();
        }
    }

    public SelectEndPointPageViewModel SelectEndPointPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectEndPointPageViewModel>())
                SimpleIoc.Default.Register<SelectEndPointPageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectEndPointPageViewModel>();
        }
    }

    public StockFuelPageViewModel StockFuelPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<StockFuelPageViewModel>())
                SimpleIoc.Default.Register<StockFuelPageViewModel>();
            return SimpleIoc.Default.GetInstance<StockFuelPageViewModel>();
        }
    }

    public AddChamberPageViewModel AddChamberPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AddChamberPageViewModel>())
                SimpleIoc.Default.Register<AddChamberPageViewModel>();
            return SimpleIoc.Default.GetInstance<AddChamberPageViewModel>();
        }
    }

    public ProfilePageViewModel ProfilePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ProfilePageViewModel>())
                SimpleIoc.Default.Register<ProfilePageViewModel>();
            return SimpleIoc.Default.GetInstance<ProfilePageViewModel>();
        }
    }

    public ProvingPageViewModel ProvingPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ProvingPageViewModel>())
                SimpleIoc.Default.Register<ProvingPageViewModel>();
            return SimpleIoc.Default.GetInstance<ProvingPageViewModel>();
        }
    }

    public CalibrationPageViewModel CalibrationPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<CalibrationPageViewModel>())
                SimpleIoc.Default.Register<CalibrationPageViewModel>();
            return SimpleIoc.Default.GetInstance<CalibrationPageViewModel>();
        }
    }

    public OrderBookItemPageViewModel OrderBookItemPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<OrderBookItemPageViewModel>())
                SimpleIoc.Default.Register<OrderBookItemPageViewModel>();
            return SimpleIoc.Default.GetInstance<OrderBookItemPageViewModel>();
        }
    }

    public CommercialListPageViewModel CommercialListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<CommercialListPageViewModel>())
                SimpleIoc.Default.Register<CommercialListPageViewModel>();
            return SimpleIoc.Default.GetInstance<CommercialListPageViewModel>();
        }
    }

    public AddHomesCviPageViewModel AddHomesCviPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AddHomesCviPageViewModel>())
                SimpleIoc.Default.Register<AddHomesCviPageViewModel>();
            return SimpleIoc.Default.GetInstance<AddHomesCviPageViewModel>();
        }
    }

    public AddMeasurePageViewModel AddMeasurePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AddMeasurePageViewModel>())
                SimpleIoc.Default.Register<AddMeasurePageViewModel>();
            return SimpleIoc.Default.GetInstance<AddMeasurePageViewModel>();
        }
    }

    public AddWitnessPageViewModel AddWitnessPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AddWitnessPageViewModel>())
                SimpleIoc.Default.Register<AddWitnessPageViewModel>();
            return SimpleIoc.Default.GetInstance<AddWitnessPageViewModel>();
        }
    }

    public DocumentPageViewModel DesignMapPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DocumentPageViewModel>())
                SimpleIoc.Default.Register<DocumentPageViewModel>();
            return SimpleIoc.Default.GetInstance<DocumentPageViewModel>();
        }
    }

    public SiteInspectionRatingFailurePageViewModel SiteInspectionRatingFailurePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SiteInspectionRatingFailurePageViewModel>())
                SimpleIoc.Default.Register<SiteInspectionRatingFailurePageViewModel>();
            return SimpleIoc.Default.GetInstance<SiteInspectionRatingFailurePageViewModel>();
        }
    }

    public SiteInspectionResultPageViewModel SiteInspectionResultPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SiteInspectionResultPageViewModel>())
                SimpleIoc.Default.Register<SiteInspectionResultPageViewModel>();
            return SimpleIoc.Default.GetInstance<SiteInspectionResultPageViewModel>();
        }
    }

    public AuthorisationPageViewModel AuthorisationPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AuthorisationPageViewModel>())
                SimpleIoc.Default.Register<AuthorisationPageViewModel>();
            return SimpleIoc.Default.GetInstance<AuthorisationPageViewModel>();
        }
    }

    public BaseViewModel BaseViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<BaseViewModel>()) SimpleIoc.Default.Register<BaseViewModel>();
            return SimpleIoc.Default.GetInstance<BaseViewModel>();
        }
    }

    public BlockageInputPageViewModel BlockageInputPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<BlockageInputPageViewModel>())
                SimpleIoc.Default.Register<BlockageInputPageViewModel>();
            return SimpleIoc.Default.GetInstance<BlockageInputPageViewModel>();
        }
    }

    public BlockagePageViewModel BlockagePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<BlockagePageViewModel>())
                SimpleIoc.Default.Register<BlockagePageViewModel>();
            return SimpleIoc.Default.GetInstance<BlockagePageViewModel>();
        }
    }

    public CarouselViewModel CarouselViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<CarouselViewModel>()) SimpleIoc.Default.Register<CarouselViewModel>();
            return SimpleIoc.Default.GetInstance<CarouselViewModel>();
        }
    }

    public DailyChecksIssuePageViewModel DailyChecksIssuePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DailyChecksIssuePageViewModel>())
                SimpleIoc.Default.Register<DailyChecksIssuePageViewModel>();
            return SimpleIoc.Default.GetInstance<DailyChecksIssuePageViewModel>();
        }
    }

    public DailyDiaryPageViewModel DailyDiaryPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DailyDiaryPageViewModel>())
                SimpleIoc.Default.Register<DailyDiaryPageViewModel>();
            return SimpleIoc.Default.GetInstance<DailyDiaryPageViewModel>();
        }
    }

    public DfePageViewModel DfePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DfePageViewModel>()) SimpleIoc.Default.Register<DfePageViewModel>();
            return SimpleIoc.Default.GetInstance<DfePageViewModel>();
        }
    }

    public DiarySelectPageViewModel DiarySelectPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DiarySelectPageViewModel>())
                SimpleIoc.Default.Register<DiarySelectPageViewModel>();
            return SimpleIoc.Default.GetInstance<DiarySelectPageViewModel>();
        }
    }

    public DocumentListingPageViewModel DocumentListingPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DocumentListingPageViewModel>())
                SimpleIoc.Default.Register<DocumentListingPageViewModel>();
            return SimpleIoc.Default.GetInstance<DocumentListingPageViewModel>();
        }
    }

    public DocumentPageViewModel DocumentPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DocumentPageViewModel>())
                SimpleIoc.Default.Register<DocumentPageViewModel>();
            return SimpleIoc.Default.GetInstance<DocumentPageViewModel>();
        }
    }

    public DocumentViewPageViewModel DocumentViewPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DocumentViewPageViewModel>())
                SimpleIoc.Default.Register<DocumentViewPageViewModel>();
            return SimpleIoc.Default.GetInstance<DocumentViewPageViewModel>();
        }
    }


    public EditInjuredPersonPageViewModel EditInjuredPersonPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<EditInjuredPersonPageViewModel>())
                SimpleIoc.Default.Register<EditInjuredPersonPageViewModel>();
            return SimpleIoc.Default.GetInstance<EditInjuredPersonPageViewModel>();
        }
    }

    public FBaseViewModel FBaseViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<FBaseViewModel>()) SimpleIoc.Default.Register<FBaseViewModel>();
            return SimpleIoc.Default.GetInstance<FBaseViewModel>();
        }
    }

    public FormsCommentPageViewModel FormsCommentPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<FormsCommentPageViewModel>())
                SimpleIoc.Default.Register<FormsCommentPageViewModel>();
            return SimpleIoc.Default.GetInstance<FormsCommentPageViewModel>();
        }
    }

    public FormsMapPageViewModel FormsMapPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<FormsMapPageViewModel>())
                SimpleIoc.Default.Register<FormsMapPageViewModel>();
            return SimpleIoc.Default.GetInstance<FormsMapPageViewModel>();
        }
    }

    public FuelConsumptionPageViewModel FuelConsumptionPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<FuelConsumptionPageViewModel>())
                SimpleIoc.Default.Register<FuelConsumptionPageViewModel>();
            return SimpleIoc.Default.GetInstance<FuelConsumptionPageViewModel>();
        }
    }

    public GangSelectPageViewModel GangSelectPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<GangSelectPageViewModel>())
                SimpleIoc.Default.Register<GangSelectPageViewModel>();
            return SimpleIoc.Default.GetInstance<GangSelectPageViewModel>();
        }
    }

    public ImageEditorPageViewModel ImageEditorPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ImageEditorPageViewModel>())
                SimpleIoc.Default.Register<ImageEditorPageViewModel>();
            return SimpleIoc.Default.GetInstance<ImageEditorPageViewModel>();
        }
    }

    public EditInjuredPersonPageViewModel InjuredPersonViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<EditInjuredPersonPageViewModel>())
                SimpleIoc.Default.Register<EditInjuredPersonPageViewModel>();
            return SimpleIoc.Default.GetInstance<EditInjuredPersonPageViewModel>();
        }
    }

    public InputMeasurePageViewModel InputMeasurePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<InputMeasurePageViewModel>())
                SimpleIoc.Default.Register<InputMeasurePageViewModel>();
            return SimpleIoc.Default.GetInstance<InputMeasurePageViewModel>();
        }
    }

    public InvestigateDamagePageViewModel InvestigateDamagePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<InvestigateDamagePageViewModel>())
                SimpleIoc.Default.Register<InvestigateDamagePageViewModel>();
            return SimpleIoc.Default.GetInstance<InvestigateDamagePageViewModel>();
        }
    }

    public InvestigationDetail2PageViewModel InvestigationDetail2PageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<InvestigationDetail2PageViewModel>())
                SimpleIoc.Default.Register<InvestigationDetail2PageViewModel>();
            return SimpleIoc.Default.GetInstance<InvestigationDetail2PageViewModel>();
        }
    }

    public InvestigationDetailPageViewModel InvestigationDetailPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<InvestigationDetailPageViewModel>())
                SimpleIoc.Default.Register<InvestigationDetailPageViewModel>();
            return SimpleIoc.Default.GetInstance<InvestigationDetailPageViewModel>();
        }
    }


    public LoginPageViewModel LoginPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<LoginPageViewModel>()) SimpleIoc.Default.Register<LoginPageViewModel>();
            return SimpleIoc.Default.GetInstance<LoginPageViewModel>();
        }
    }

    public MainListPageViewModel MainListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MainListPageViewModel>())
                SimpleIoc.Default.Register<MainListPageViewModel>();
            return SimpleIoc.Default.GetInstance<MainListPageViewModel>();
        }
    }

    public MeasureApprovalPageViewModel MeasureApprovalPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MeasureApprovalPageViewModel>())
                SimpleIoc.Default.Register<MeasureApprovalPageViewModel>();
            return SimpleIoc.Default.GetInstance<MeasureApprovalPageViewModel>();
        }
    }

    public MeasureListPageViewModel MeasureListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MeasureListPageViewModel>())
                SimpleIoc.Default.Register<MeasureListPageViewModel>();
            return SimpleIoc.Default.GetInstance<MeasureListPageViewModel>();
        }
    }

    public MeasureTypePageViewModel MeasureTypePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MeasureTypePageViewModel>())
                SimpleIoc.Default.Register<MeasureTypePageViewModel>();
            return SimpleIoc.Default.GetInstance<MeasureTypePageViewModel>();
        }
    }

    public MenuSelectionPageViewModel MenuSelectionPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MenuSelectionPageViewModel>())
                SimpleIoc.Default.Register<MenuSelectionPageViewModel>();
            return SimpleIoc.Default.GetInstance<MenuSelectionPageViewModel>();
        }
    }

    public MethodologyPageViewModel MethodologyPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MethodologyPageViewModel>())
                SimpleIoc.Default.Register<MethodologyPageViewModel>();
            return SimpleIoc.Default.GetInstance<MethodologyPageViewModel>();
        }
    }

    public MyPlantScreenPageViewModel MyPlantScreenPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<MyPlantScreenPageViewModel>())
                SimpleIoc.Default.Register<MyPlantScreenPageViewModel>();
            return SimpleIoc.Default.GetInstance<MyPlantScreenPageViewModel>();
        }
    }


    public PhotoPageViewModel PhotoPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PhotoPageViewModel>()) SimpleIoc.Default.Register<PhotoPageViewModel>();
            return SimpleIoc.Default.GetInstance<PhotoPageViewModel>();
        }
    }


    public PermitPageViewModel PermitPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PermitPageViewModel>())
                SimpleIoc.Default.Register<PermitPageViewModel>();
            return SimpleIoc.Default.GetInstance<PermitPageViewModel>();
        }
    }

    public PhotoSelectionPageViewModel PhotoSelectionPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PhotoSelectionPageViewModel>())
                SimpleIoc.Default.Register<PhotoSelectionPageViewModel>();
            return SimpleIoc.Default.GetInstance<PhotoSelectionPageViewModel>();
        }
    }

    public PinItemsSourcePageViewModel PinItemsSourcePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PinItemsSourcePageViewModel>())
                SimpleIoc.Default.Register<PinItemsSourcePageViewModel>();
            return SimpleIoc.Default.GetInstance<PinItemsSourcePageViewModel>();
        }
    }

    public PlantChecksPageViewModel PlantChecksPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PlantChecksPageViewModel>())
                SimpleIoc.Default.Register<PlantChecksPageViewModel>();
            return SimpleIoc.Default.GetInstance<PlantChecksPageViewModel>();
        }
    }

    public PlantDetailsPageViewModel PlantDetailsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PlantDetailsPageViewModel>())
                SimpleIoc.Default.Register<PlantDetailsPageViewModel>();
            return SimpleIoc.Default.GetInstance<PlantDetailsPageViewModel>();
        }
    }

    public PlantIssuesPageViewModel PlantIssuesPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PlantIssuesPageViewModel>())
                SimpleIoc.Default.Register<PlantIssuesPageViewModel>();
            return SimpleIoc.Default.GetInstance<PlantIssuesPageViewModel>();
        }
    }

    public PlantTansferInPageViewModel PlantTansferInPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PlantTansferInPageViewModel>())
                SimpleIoc.Default.Register<PlantTansferInPageViewModel>();
            return SimpleIoc.Default.GetInstance<PlantTansferInPageViewModel>();
        }
    }

    public PlantTransferListPageViewModel PlantTransferListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PlantTransferListPageViewModel>())
                SimpleIoc.Default.Register<PlantTransferListPageViewModel>();
            return SimpleIoc.Default.GetInstance<PlantTransferListPageViewModel>();
        }
    }

    public PlantTansferPageViewModel PlantTransferPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PlantTansferPageViewModel>())
                SimpleIoc.Default.Register<PlantTansferPageViewModel>();
            return SimpleIoc.Default.GetInstance<PlantTansferPageViewModel>();
        }
    }

    public ProjectImagesPageViewModel ProjectImagesPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ProjectImagesPageViewModel>())
                SimpleIoc.Default.Register<ProjectImagesPageViewModel>();
            return SimpleIoc.Default.GetInstance<ProjectImagesPageViewModel>();
        }
    }

    public ProjectsPageViewModel ProjectsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ProjectsPageViewModel>())
                SimpleIoc.Default.Register<ProjectsPageViewModel>();
            return SimpleIoc.Default.GetInstance<ProjectsPageViewModel>();
        }
    }

    public ProjectSummaryPageViewModel ProjectSummaryPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ProjectSummaryPageViewModel>())
                SimpleIoc.Default.Register<ProjectSummaryPageViewModel>();
            return SimpleIoc.Default.GetInstance<ProjectSummaryPageViewModel>();
        }
    }

    public RaiseCviDetailsPageViewModel RaiseCviDetailsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<RaiseCviDetailsPageViewModel>())
                SimpleIoc.Default.Register<RaiseCviDetailsPageViewModel>();
            return SimpleIoc.Default.GetInstance<RaiseCviDetailsPageViewModel>();
        }
    }

    public RaiseCviMeasurePageViewModel RaiseCviMeasurePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<RaiseCviMeasurePageViewModel>())
                SimpleIoc.Default.Register<RaiseCviMeasurePageViewModel>();
            return SimpleIoc.Default.GetInstance<RaiseCviMeasurePageViewModel>();
        }
    }

    public RegisterNewEventPageViewModel RegisterNewEventPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<RegisterNewEventPageViewModel>())
                SimpleIoc.Default.Register<RegisterNewEventPageViewModel>();
            return SimpleIoc.Default.GetInstance<RegisterNewEventPageViewModel>();
        }
    }

    public ReInstatementPageViewModel ReInstatementPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ReInstatementPageViewModel>())
                SimpleIoc.Default.Register<ReInstatementPageViewModel>();
            return SimpleIoc.Default.GetInstance<ReInstatementPageViewModel>();
        }
    }

    public SelectDamageDetailsPageViewModel SelectDamageDetailsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectDamageDetailsPageViewModel>())
                SimpleIoc.Default.Register<SelectDamageDetailsPageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectDamageDetailsPageViewModel>();
        }
    }

    public SelectInvestigationPageViewModel SelectInvestigationPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectInvestigationPageViewModel>())
                SimpleIoc.Default.Register<SelectInvestigationPageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectInvestigationPageViewModel>();
        }
    }

    public SelectPreSitePageViewModel SelectPreSitePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectPreSitePageViewModel>())
                SimpleIoc.Default.Register<SelectPreSitePageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectPreSitePageViewModel>();
        }
    }

    public SelectProjectPageViewModel SelectProjectPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectProjectPageViewModel>())
                SimpleIoc.Default.Register<SelectProjectPageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectProjectPageViewModel>();
        }
    }

    public SelectStreetPageViewModel SelectStreetPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectStreetPageViewModel>())
                SimpleIoc.Default.Register<SelectStreetPageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectStreetPageViewModel>();
        }
    }

    public SelectSurveyTypePageViewModel SelectSurveyTypePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SelectSurveyTypePageViewModel>())
                SimpleIoc.Default.Register<SelectSurveyTypePageViewModel>();
            return SimpleIoc.Default.GetInstance<SelectSurveyTypePageViewModel>();
        }
    }

    public ServicesCrossedPageViewModel ServicesCrossedPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ServicesCrossedPageViewModel>())
                SimpleIoc.Default.Register<ServicesCrossedPageViewModel>();
            return SimpleIoc.Default.GetInstance<ServicesCrossedPageViewModel>();
        }
    }

    public SettingsPageViewModel SettingsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SettingsPageViewModel>())
                SimpleIoc.Default.Register<SettingsPageViewModel>();
            return SimpleIoc.Default.GetInstance<SettingsPageViewModel>();
        }
    }

    public SignaturePageViewModel SignaturePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SignaturePageViewModel>())
                SimpleIoc.Default.Register<SignaturePageViewModel>();
            return SimpleIoc.Default.GetInstance<SignaturePageViewModel>();
        }
    }

    public SupervisorApprovalsPageViewModel SupervisorApprovalsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SupervisorApprovalsPageViewModel>())
                SimpleIoc.Default.Register<SupervisorApprovalsPageViewModel>();
            return SimpleIoc.Default.GetInstance<SupervisorApprovalsPageViewModel>();
        }
    }

    public SupervisorDiaryPageViewModel SupervisorDiaryPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SupervisorDiaryPageViewModel>())
                SimpleIoc.Default.Register<SupervisorDiaryPageViewModel>();
            return SimpleIoc.Default.GetInstance<SupervisorDiaryPageViewModel>();
        }
    }

    public SupervisorMeasuresApprovalsPageViewModel SupervisorMeasuresApprovalsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SupervisorMeasuresApprovalsPageViewModel>())
                SimpleIoc.Default.Register<SupervisorMeasuresApprovalsPageViewModel>();
            return SimpleIoc.Default.GetInstance<SupervisorMeasuresApprovalsPageViewModel>();
        }
    }

    public SupervisorProjectPageViewModel SupervisorProjectPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SupervisorProjectPageViewModel>())
                SimpleIoc.Default.Register<SupervisorProjectPageViewModel>();
            return SimpleIoc.Default.GetInstance<SupervisorProjectPageViewModel>();
        }
    }

    public SurveyQuestionsPageViewModel SurveyQuestionsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<SurveyQuestionsPageViewModel>())
                SimpleIoc.Default.Register<SurveyQuestionsPageViewModel>();
            return SimpleIoc.Default.GetInstance<SurveyQuestionsPageViewModel>();
        }
    }

    public TaskListPageViewModel TaskListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<TaskListPageViewModel>())
                SimpleIoc.Default.Register<TaskListPageViewModel>();
            return SimpleIoc.Default.GetInstance<TaskListPageViewModel>();
        }
    }

    public TeamOptionsPageViewModel TeamOptionsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<TeamOptionsPageViewModel>())
                SimpleIoc.Default.Register<TeamOptionsPageViewModel>();
            return SimpleIoc.Default.GetInstance<TeamOptionsPageViewModel>();
        }
    }

    public TimesheetsPageViewModel TimesheetsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<TimesheetsPageViewModel>())
                SimpleIoc.Default.Register<TimesheetsPageViewModel>();
            return SimpleIoc.Default.GetInstance<TimesheetsPageViewModel>();
        }
    }

    public TimesheetsSelectionPageViewModel TimesheetsSelectionPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<TimesheetsSelectionPageViewModel>())
                SimpleIoc.Default.Register<TimesheetsSelectionPageViewModel>();
            return SimpleIoc.Default.GetInstance<TimesheetsSelectionPageViewModel>();
        }
    }

    public ToolboxTalkDocumentPageViewModel ToolboxTalkDocumentPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<ToolboxTalkDocumentPageViewModel>())
                SimpleIoc.Default.Register<ToolboxTalkDocumentPageViewModel>();
            return SimpleIoc.Default.GetInstance<ToolboxTalkDocumentPageViewModel>();
        }
    }

    public UtilityDamagesNavPageViewModel UtilityDamagesNavPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<UtilityDamagesNavPageViewModel>())
                SimpleIoc.Default.Register<UtilityDamagesNavPageViewModel>();
            return SimpleIoc.Default.GetInstance<UtilityDamagesNavPageViewModel>();
        }
    }

    public VisitorLogListPageViewModel VisitorLogListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<VisitorLogListPageViewModel>())
                SimpleIoc.Default.Register<VisitorLogListPageViewModel>();
            return SimpleIoc.Default.GetInstance<VisitorLogListPageViewModel>();
        }
    }

    public VisitorLogOutPageViewModel VisitorLogOutPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<VisitorLogOutPageViewModel>())
                SimpleIoc.Default.Register<VisitorLogOutPageViewModel>();
            return SimpleIoc.Default.GetInstance<VisitorLogOutPageViewModel>();
        }
    }

    public VisitorLogPageViewModel VisitorLogPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<VisitorLogPageViewModel>())
                SimpleIoc.Default.Register<VisitorLogPageViewModel>();
            return SimpleIoc.Default.GetInstance<VisitorLogPageViewModel>();
        }
    }

    public VisitorSignaturePageViewModel VisitorSignaturePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<VisitorSignaturePageViewModel>())
                SimpleIoc.Default.Register<VisitorSignaturePageViewModel>();
            return SimpleIoc.Default.GetInstance<VisitorSignaturePageViewModel>();
        }
    }

    public WeatherEventPageViewModel WeatherEventPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<WeatherEventPageViewModel>())
                SimpleIoc.Default.Register<WeatherEventPageViewModel>();
            return SimpleIoc.Default.GetInstance<WeatherEventPageViewModel>();
        }
    }

    public DailySiteInspectionPageViewModel DailySiteInspectionPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<DailySiteInspectionPageViewModel>())
                SimpleIoc.Default.Register<DailySiteInspectionPageViewModel>();
            return SimpleIoc.Default.GetInstance<DailySiteInspectionPageViewModel>();
        }
    }

    public EventManagementSelectionPageViewModel EventManagementSelectionPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<EventManagementSelectionPageViewModel>())
                SimpleIoc.Default.Register<EventManagementSelectionPageViewModel>();
            return SimpleIoc.Default.GetInstance<EventManagementSelectionPageViewModel>();
        }
    }

    public OrderListPageViewModel OrderListPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<OrderListPageViewModel>())
                SimpleIoc.Default.Register<OrderListPageViewModel>();
            return SimpleIoc.Default.GetInstance<OrderListPageViewModel>();
        }
    }

    public AdditionalRisksPageViewModel AdditionalRisksPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<AdditionalRisksPageViewModel>())
                SimpleIoc.Default.Register<AdditionalRisksPageViewModel>();
            return SimpleIoc.Default.GetInstance<AdditionalRisksPageViewModel>();
        }
    }

    public RatingSurveyQuestionsPageViewModel RatingSurveyQuestionsPageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<RatingSurveyQuestionsPageViewModel>())
                SimpleIoc.Default.Register<RatingSurveyQuestionsPageViewModel>();
            return SimpleIoc.Default.GetInstance<RatingSurveyQuestionsPageViewModel>();
        }
    }

    public PhotoExamplePageViewModel PhotoExamplePageViewModel
    {
        get
        {
            if (!SimpleIoc.Default.IsRegistered<PhotoExamplePageViewModel>())
                SimpleIoc.Default.Register<PhotoExamplePageViewModel>();
            return SimpleIoc.Default.GetInstance<PhotoExamplePageViewModel>();
        }
    }

    public NavigationService NavigationService => SimpleIoc.Default.GetInstance<NavigationService>();
}