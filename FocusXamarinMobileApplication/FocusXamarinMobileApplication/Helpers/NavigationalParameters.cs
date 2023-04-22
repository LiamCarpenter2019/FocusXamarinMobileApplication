#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Microsoft.Graph.Models;
using Plugin.Permissions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Audit = FocusXamarinMobileApplication.Models.Audit;
using Constants = FocusXamarinMobileApplication.Services.Constants;
using Event = FocusXamarinMobileApplication.Models.Event;
using Permission = Plugin.Permissions.Abstractions.Permission;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Helpers;

public static class NavigationalParameters
{
    public enum AppModes
    {
        MAIN,
        MENU,
        RISKASSESMENT,
        TIMESHEETS,
        DIARIES,
        MEASURES,
        UTILITIES,
        LATERALS,
        CALIBRATION,
        PERMITTODIG,
        PROJECTIMAGES,
        SUMMARY,
        VISITORSLOG,
        PLANT,
        OPERATIVEDOCS,
        PROJECTDOCS,
        SITEINSPECTION,
        INVESTIGATEUTILITYSTRIKE,
        COMPANYDOCS,
        AUTHORISATION,
        GANGSELECTION,
        PROJECTDIARIES,
        SUPERVISORINPUTDIARIES,
        CVI,
        APPROVALS,
        LOGIN,
        SUPERVISORPLANT,
        PRESITE,
        ASBUILT,
        SETTINGS,
        TASKLIST,
        PRESITEPREMISIS,
        PRESITECIVILS,
        SITECLEAR,
        GANGCLEAR,
        BLOCKAGE,
        DFE,
        TRAININGDOCS,
        REINSTATEMENT,
        TOOLBOXTALKS,
        GANGTOOLBOXTALKS,
        GANGAPPROVALS,
        SUPERVISORGANGPLANT,
        GANGTRAININGDOCS,
        COMMERCIAL,
        ORDERBOOK,
        EVENTMANAGEMENT,
        ACCIDENT,
        INCIDENT,
        NEARMISS,
        UTILITYDAMAGE,
        CHAMBERAUDIT,
        FUEL,
        PRESITEPOLESURVEY,
        POLECABLEMEASURE,
        CIVILSMEASURE,
        LEADINMEASURE,
        REINSTATEMENTMEASURE,
        CABLEMEASURE,
        POLEMEASURE,
        DUCTMEASURE,
        LATERALMEASURE,
        UGCRPMEASURE,
        SITESUPPORTMEASURE,
        CHAMBERMEASURE,
        DISTRIBUTIONPOINTMEASURE,
        CHAMBERASBUILT,
        BJEASBUILT,
        DPASBUILT,
        DJEASBUILT,
        FJEASBUILT,
        POLEASBUILT,       
        REMEDIAL,
        DJEMEASURE,
        FJEMEASURE,
        BJEMEASURE,
        PRESITEPIAPOLESURVEY,
        PRESITECHAMBERSURVEY,
        PRESITECHAMBERPIASURVEY,
        PRESITEDUCTSURVEY,
        PRESITEDUCTPIASURVEY
    }

    public enum AppTypes
    {
        GANGER,
        SUPERVISOR,
        YARDMAN,
        OTHER
    }

    public enum AuditStatuses
    {
        SIGNOFF,
        INCOMPLETE,
        COMPLETE,
        NEW,
        DEFAULT
    }

    //public static IEnumerable<IGrouping<string, EventSeverity>> SeveritList { get; set; }

    public enum AuthorisationTypes
    {
        OPERATIVE_PIN,
        SUPERVISOR_PIN,
        OPERATIVE_SIG,
        SUPERVISOR_SIG,
        CHANGE_PIN,
        SIGNATURE_ONLY,
        SIGNATURE_EMAIL,
        PIN_CONFIRM,
        PIN_AND_SIGNATURE,
        DEFAULT
    }

    public enum CommentTypes
    {
        PERMITQUESTION,
        SITECLEARQUESTION,
        PRESITEPREMISESQUESTION,
        PRESITECIVILSQUESTION,
        PREESITE,
        UTILITYDAMAGE,
        UTILITYDAMAGEQUESTION,
        INVESTIGATEUTILITYSTRIKE,
        INVESTIGATEUTILITYSTRIKEQUESTION,
        DEFAULT
    }

    public enum DiaryTypes
    {
        GANGDIARY,
        GANGDELAYS,
        GANGWORKS,
        PAYROL,
        DIARYAPPROVALS,
        DELAYSAPPROVALS,
        WORKSAPPROVALS,
        DEFAULT
    }

    public enum EntryTypes
    {
        GANGER,
        SUPERVISOR_VIEW,
        SUPERVISOR_EDIT
    }

    public enum ImageTypes
    {
        UTILITY_DAMAGE,
        DAMAGE_INVESTIGATION,
        PRESITE
    }

    public enum PhotoModes
    {
        STARTOFDAY,
        ENDOFDAY,
        LATERAL,
        SITECLEAR,
        PERMIT,
        BLOCKAGE,
        CVI,
        AUDITQUESTIONS,
        RISKASSESMENT,
        LATERALTRACK,
        LATERALTOBY,
        LATERALMEASURE,
        PRESITECIVILS,
        PRESITEPREMISES,
        DFE,
        SURVEYQUESTION,
        SURVEYGENERAL,
        NCRCORRECTIVEACTIONS,
        NCRINTERMEDIATEACTIONS,
        NCRAUDITDETAILS,
        PLANT,
        PLANTISSUE,
        PLANTTRANSFEROUT,
        DEFAULT,
        PLANTTRANSFERIN,
        INVOICE,
        EVENTMANAGEMENT,
        INVESTIGATION,
        CHAMBERAUDIT,
        POLESURVEY,
        UGCRPMEASURE,
        DUCTMEASURE,
        CHAMBERMEASURE,
        CABLEMEASURE,
        CIVILSMEASURE,
        POLECABLEMEASURE,
        LEADINMEASURE,
        POLEMEASURE,
        REINSTATEMENTMEASURE,
        SITESUPPORTMEASURE,
        PROJECTIMAGES,
        DPASBUILT,
        ASBUILT,
        PRESITEPIAPOLESURVEY,
        PRESITECHAMBERSURVEY,
        PRESITEDUCTSURVEY,
        PRESITEDUCTPIASURVEY,
        PRESITECHAMBERPIASURVEY
    }

    public enum PLANTENTRYTYPES
    {
        GANGER,
        SUPERVISOR_VIEW,
        SUPERVISOR_EDIT
    }

    public enum PlantViews
    {
        TRANSFERIN,
        TRANSFEROUT,
        GANGVIEW,
        PLANTCHECKS,
        DEFAULT,
        SUPERVISOR
    }

    public enum ProjectListModes
    {
        TASKLIST,
        PROJECTLIST,
        JOBLIST,
        GANGLIST,
        GANGTOOLBOXTALKS,
        GANGAPPROVALS
    }

    public enum SupervisorMeasureAction
    {
        ADD_MEASURE,
        ADD_SUPER_CLAIM,
        MODIFY,
        MODIFY_LATERAL
    }

    public enum SurveyTypes
    {
        PERMITTODIG,
        PRESITEPREMISES,
        POLESURVEY,
        PRESITECIVILS,
        SITECLEAR,
        GANGCLEAR,
        AUDIT,
        SED,
        INTERNAL,
        DEFAULT,
        CHAMBERAUDIT,
        VERTISHOREPERMIT,
        PRESITEPOLESURVEY,
        CONNEXINCIVILS,
        CONNEXINASBUILT,
        POLEASBUILT,
        POLEMEASURE,
        POLECABLEASBUILT,
        POLECABLEMEASURE,
        CHAMBERMEASURE,
        UGCRPMEASURE,
        UGCRPASBUILT,
        DISTRIBUTIONPOINTMEASURE,
        DUCTMEASURE,
        CHAMBERASBUILT,
        DPASBUILT,
        BJEASBUILT,
        DJEASBUILT,
        FJEASBUILT,       
        REMEDIAL,
        SITEINSPECTION,
        DJEMEASURE,
        BJEMEASURE,
        FJEMEASURE,
        PRESITEPIAPOLESURVEY,
        PRESITECHAMBERSURVEY,
        PRESITECHAMBERPIASURVEY,
        PRESITEDUCTSURVEY,
        PRESITEDUCTPIASURVEY,
        BLOCKAGE
    }

    public enum ViewStates
    {
        NOT_SELECTED,
        SELECTED,
        CORRECT_PIN,
        COMPLETE
    }

    public enum WeatherType
    {
        LIGHTRAIN,
        SUNSHINE,
        SNOW,
        STORM,
        SUNNYSPELLS,
        CLOUDY,
        WIND
    }

    public static readonly DateTime MinDateTime = DateTime.Parse("01/01/1900 00:00:00");
    public static readonly DateTime ApprovedDate = DateTime.Parse("02/02/1900 00:00:00");

    public static List<EventManagementType> EventManagementTypeList { get; set; }

    public static AuthorisationTypes AuthorisationType { get; set; }
    public static PhotoModes PhotoMode { get; set; }
    public static DiaryTypes DiaryType { get; set; } = DiaryTypes.GANGDIARY;
    public static WeatherType WeatherCondition { get; set; }
    public static SupervisorMeasureAction SupervisorAction { get; set; }

    public static NavigationPage MainPage { get; set; }
    public static object NavigationParameter { get; set; }
    public static object PassedInfo { get; set; }
    public static Constants Constants { get; set; }

    public static ObservableCollection<string> CviActions { get; set; } = new()
    {
        "Wait on formal instruction",
        "Making arrangements"
    };

    public static ObservableCollection<string> CviChargedAt { get; set; } = new()
    {
        "Schedule of rates",
        "Daywork rates",
        "Price to be agreed",
        "Allowance not included"
    };

    public static ObservableCollection<string> LateralCodes { get; set; } = new()
    {
        "1005", "1011", "1017", "1065", "1069", "1073", "4004", "4008", "4012", "4016", "4020", "D101"
    };

    public static ObservableCollection<string> PictureReasons { get; set; } = new()
    {
        "Select a Reason",
        "Waypoint",
        "Incident / Strike",
        "Condition",
        "H S E", "Obstruction",
        "Site Clear",
        "General",
        "Trial Holes", "Route"
    };

    public static ObservableCollection<string> ReinstatementMaterials { get; set; } =
        new()
        {
            "AC Medium & Open Graded Surface & Binder Courses",
            "AC Dense Surface, Base & Binder Courses",
            "HRA Surface, Base & Binder Courses",
            "SMA Surface & Binder Courses",
            "EME2 Binder Course",
            "Precast Concrete Flags (PCC)",
            "Tactile Blister Flags",
            "Tactile Corduroy Flags",
            "Blocks Footway",
            "Setts Footway",
            "Blocks Carriageway",
            "Setts Carriageway",
            "Concrete",
            "Reinforced Concrete",
            "High Friction Surfacing",
            "Resin Bound Surfacing",
            "Patterned Concrete",
            "Other Specialist Surfacing"
        };

    public static ObservableCollection<string> MaterialSizes { get; set; } = new()
    {
        "6mm",
        "10mm",
        "14mm",
        "20mm",
        "900 x 600",
        "600 x 600",
        "450 x 450",
        "300 x 300"
    };

    public static ObservableCollection<string> BlockageReasons { get; set; } = new()
    {
        "Tube missing in toby",
        "Tube box missing",
        "Tube missing in Cabinet",
        "Partial air",
        "Full Blockage",
        "Chamber frame damage",
        "Chamber lid top damage",
        "Snapped Rope",
        "Rope stuck in duct",
        "Duct damage",
        "Debris in duct",
        "Third party damage",
        "Other"
    };


    public static DataPassed2XamarinTablets DataPassedToTablet { get; set; }

    public static List<JobData4Tablet> JobTaskList { get; set; } = new();

    public static List<Assignment> AssignmentList { get; set; } = new();
    public static List<Rates> AllRates { get; set; } = new();
    public static List<VMexpansionReleaseData> AllEndpoints { get; set; } = new();
    public static List<VMexpansionReleaseData> Endpoints { get; set; } = new();
    public static List<VMexpansionReleaseData> SelectedAddresses { get; set; } = new();
    public static List<VMexpansionReleaseData> ProjectEndpoints { get; set; } = new();

    public static List<ProjectWorks> T6 { get; set; } = new();
    public static List<ProjectWorks> DfeWorkItems { get; set; } = new();
    public static List<ProjectWorks> DfeRatesToAdd { get; set; } = new();
    public static List<ProjectWorks> ProjectWorksList { get; set; } = new();

    public static List<Abbreviations> Abbreviations { get; set; } = new();
    public static List<Pin> CurrentMapPins { get; set; } = new();
    public static List<RegisterUtilityDamage> StrikesToSend { get; set; } = new();
    public static List<SurveyAnswers> AnswerList { get; set; } = new();
    public static List<ClaimedFile> ClaimList { get; set; } = new();
    public static List<Docs4Tablet> MissingDocuments { get; set; } = new();

    public static List<VMl4CabDetail> CabinetList { get; set; }
    public static List<Pictures4Tablet> CviPictures { get; set; }
    public static List<JobData4Tablet> AllJobs { get; set; }
    public static List<JobData4Tablet> JobsToApprove { get; set; } = new();
    public static List<JobData4Tablet> ProjectJobs { get; set; }
    public static List<SurveyQuestion> DailySiteInspectionQuestions { get; set; }
    public static List<Person> MultSignatures { get; set; }
    public static List<UsersToolBoxTalks> CurrentUserToolboxTalks { get; set; }

    public static List<Checks4TabletResponses> AllCurrentResponses { get; set; }

    //public static List<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();
    public static List<ProjectWorks> CurrentWorksList { get; set; }

    public static ObservableCollection<YesNoNaQuestionViewModel> YesNoCollection { get; set; }
    public static ObservableCollection<RatingQuestionViewModel> RatingQuestions { get; set; } = new();
    public static ObservableCollection<SurveyQuestion> GenericQuestions { get; set; } = new();
    public static List<SurveyQuestion> PreSiteQuestions { get; set; } = new();

    public static List<IGrouping<string, VMexpansionReleaseData>> AssignmentStreetList { get; set; } = new();

    public static IGrouping<string, VMexpansionReleaseData> SelectedStreet { get; set; }
    public static VMexpansionReleaseData SelectedAsset { get; set; }


    public static JobData4Tablet PreviouslySelectedJob { get; set; }

    public static JobData4Tablet CurrentSelectedJob { get; set; }

    //public static DateTime InitialLogOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public static Person LoggedInUser { get; set; }
    public static Person SelectedUser { get; set; } = new();
    public static ReinstatementMeasure ReinstatmentMeasureToDelete { get; set; }
    public static Witness Witness { get; set; }
    public static Stream PdfDocumentStream { get; set; }
    public static PlantIssue CurrentPlantIssue { get; set; }
    public static PlantTransfers PlantTransfers { get; set; }
    public static InjuredPerson InjuredPerson { get; set; }
    public static Order Order { get; set; }
    public static TaskItem SelectedTaskItem { get; set; }
    public static AuthenticatedUser AuthorisedUser { get; set; }
    public static VisitorLog NewVisitor { get; set; }
    public static Assignment AuditResult { get; set; }
    public static Assignment CurrentAssignment { get; set; } = new();
    public static Assignment AssignmentToBeUpdated { get; set; }
    public static AuthorisationDetail AuthDetail { get; set; } = new();
    public static AuthorisationDetail TlDetail { get; set; }
    public static AuthorisationDetail AuditorDetail { get; set; }
    public static MapLaunchOptions MapLaunchOptions { get; set; }
    public static RegisterUtilityDamage RegisterUtilityDamage { get; set; }
    public static DamageToInvestigate UtilityDamage { get; set; }
    public static InvestigateDamages InvestigateDamage { get; set; }
    public static InvestigationAnswer CurrentInvestigationAnswer { get; set; }
    public static ClaimedNotesFile ClaimedNotes { get; set; }
    public static ClaimedFile ClaimFile { get; set; }
    public static DateTime InitialLogOn { get; set; }

    private static Dictionary<ImageTypes, string> ImageTypeDict => new()
    {
        { ImageTypes.UTILITY_DAMAGE, "DMG" },
        { ImageTypes.DAMAGE_INVESTIGATION, "DMG" },
        { ImageTypes.PRESITE, "PS" }
    };

    public static Position NewPosition { get; set; } = new(53.788876, -1.345389);

    public static AppTypes AppType { get; set; } = AppTypes.GANGER;
    public static AppModes AppMode { get; set; } = AppModes.MENU;
    public static ImageTypes ImageType { get; set; }
    public static ProjectListModes ProjectListMode { get; internal set; }
    public static SurveyTypes SurveyType { get; set; }
    public static EntryTypes EntryType { get; set; }
    public static CommentTypes CommentType { get; set; }
    public static AuditStatuses AuditStatus { get; set; }
    public static PlantViews PlantView { get; set; }

    public static bool logonCheckResult { get; set; }
    public static bool StartOfDayPictureTaken { get; set; }
    public static bool EndOfDayPictureTaken { get; set; }
    public static bool VisitorsStillToBeLoggedOut { get; set; }
    public static bool CompleteUpload { get; set; }
    public static bool TimeSheetsToApprove { get; set; }
    public static bool DiariesToApprove { get; set; }
    public static bool CurrentEndpointList { get; set; }
    public static bool PreviewMode { get; set; }
    public static bool LateralsToApprove { get; set; }
    public static bool MeasuresToApprove { get; set; }
    public static bool ReinstatmentMeasureToApprove { get; set; }
    public static string PsEmergencyDetails { get; set; }
    public static string MapType { get; set; }
    public static string CurrentImageType => ImageTypeDict[ImageType];
    public static string LocationInputText { get; set; }
    public static string PreviousPageKey { get; set; }
    public static string ReturnPage { get; set; }
    public static string InvestigationIdToFinalize { get; set; }
    public static string ActionToPerform { get; set; }
    public static string ClaimType { get; set; }
    public static int VisitorCount { get; set; }

    //  public static bool UpdatingDocuments { get; set; } = false;
    public static Rates SelectedRate { get; set; }
    public static SurveyQuestion SelectedQuestion { get; set; }
    public static SurveyAnswers SelectedAnswer { get; set; }
    public static ProjectWorks SelectedWorkItem { get; set; }
    public static FibreTestResults SelectedCalibrationTest { get; set; }
    public static VMexpansionReleaseData SelectedEndPoint { get; set; }
    //public static VMexpansionReleaseData SelectedEndPoint { get; set; }
    public static VMexpansionReleaseData SelectedCalibration { get; set; }
    public static Plant4Tablet SelectetedPlantItem { get; set; }
    public static Labour SelectedLabourFile { get; set; }
    public static VMl4CabDetail SelectedCabinet { get; set; }
    public static DigPermit CurrentPermit { get; set; }
    public static Dfe CurrentDfe { get; set; }
    public static Audit LatestAudit { get; set; }
    public static Audit CurrentAudit { get; set; }
    public static ProjectWorks CurrentRate { get; set; }
    public static Cvi CurrentCvi { get; set; }
    public static Pictures4Tablet PictureToEdit { get; set; } = new();
    public static Blockage BlockageItem { get; set; }
    public static Person SelectedOperative { get; set; }
    public static VisitorLog SelectedVisitor { get; set; }
    public static Checks4TabletResponses CurrentCheckAnswer { get; set; }
    public static Ncr CurrentNCR { get; set; }
    public static DocumentData2Display SelectedDocument { get; set; }
    public static Pictures4Tablet SelectedPhoto { get; set; }
    public static Event EventManagement { get; set; }
   // public static CableRuns SelectedCableRun { get; set; }
    public static bool ReturnFromDocView { get; set; }
    public static string NCRACTION { get; set; }
    public static List<YesNoNaPlantCheckQuestionViewModel> PlantChecks { get; set; }
    public static UsersToolBoxTalks UserSignedDocument { get; set; }
    public static ToolBoxTalks ToolBoxTalk { get; set; }
    public static RatingQuestionViewModel SelectedRatingQuestion { get; set; }
    public static ObservableCollection<VMexpansionReleaseData> ListOfAssets { get; set; }
    public static int Itteration { get; set; } = 0;
    public static bool AddingNewAsset { get; set; } = false;

    public static async void CheckPermissions()
    {
        //SET Camera & Storage Permissions if not set
        var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
        var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
        var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

        if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted ||
            locationStatus != PermissionStatus.Granted)
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera, Permission.Storage,
                Permission.Location);
    }

    public static void ResetParams()
    {
        NavigationParameter = null;
        PassedInfo = null;
        PreviousPageKey = null;
        AssignmentList = null;
        //LoggedInUser = null;
        SelectedUser = null;
        //CurrenSelectedJob = null;
        TimeSheetsToApprove = false;
        DiariesToApprove = false;
        LateralsToApprove = false;
        MeasuresToApprove = false;
        T6 = null;
        ReinstatmentMeasureToApprove = false;
        AuditResult = null;

        CurrentAssignment = null;
        CurrentEndpointList = false;
        ProjectWorksList = null;
        AssignmentToBeUpdated = null;
        CurrentMapPins = null;
        Endpoints = null;
        AuthorisedUser = new AuthenticatedUser();

        AuthDetail = new AuthorisationDetail();
        TlDetail = new AuthorisationDetail();
        AuditorDetail = new AuthorisationDetail();

        VisitorsStillToBeLoggedOut = false;
        VisitorCount = 0;

        MultSignatures = null;
        SurveyType = SurveyTypes.DEFAULT;
        DiaryType = DiaryTypes.DEFAULT;
        PlantView = PlantViews.DEFAULT;
        AuditStatus = AuditStatuses.DEFAULT;
        PhotoMode = PhotoModes.DEFAULT;

        SelectedRate = null;
        SelectedQuestion = null;
        SelectedAnswer = null;
        SelectedWorkItem = null;
        SelectedCalibrationTest = null;
        SelectedEndPoint = null;
        SelectedEndPoint = null;
        SelectedCalibration = null;
        SelectetedPlantItem = null;
        SelectedLabourFile = null;
        SelectedCabinet = null;
        CurrentPermit = null;
        CurrentDfe = null;
        LatestAudit = null;
        CurrentAudit = null;
        CurrentRate = null;
        CurrentCvi = null;
        PictureToEdit = null;
        BlockageItem = null;
        SelectedOperative = null;
        SelectedVisitor = null;
        CurrentCheckAnswer = null;
        CurrentNCR = null;
        SelectedDocument = null;
        SelectedPhoto = null;

        AnswerList = new List<SurveyAnswers>();
        YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>();
        AllCurrentResponses = new List<Checks4TabletResponses>();
    }
}