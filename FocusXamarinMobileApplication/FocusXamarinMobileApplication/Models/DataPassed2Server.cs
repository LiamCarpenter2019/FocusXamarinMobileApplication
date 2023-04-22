namespace FocusXamarinMobileApplication.Models;

public class DataPassed2Server
{
    public string Category { get; set; }
    public string ErrorMessage { get; set; } = "";

    public JobData4Tablet JobData { get; set; }
    public ProjectSummary AllProjectSummaries { get; set; } = new();
    public ClaimedNotesFile DailyDiary { get; set; }
    public ServicesCrossed4Tablet ServicesCrossed { get; set; }
    public OrderBookItem OrderBookItem { get; set; } = new();

    public List<Location> Locations { get; set; } = new();
    public List<Person> PersonData { get; set; } = new();
    public List<Docs4Tablet> DocumentsData { get; set; } = new();
    public List<Pictures4Tablet> JobPictureData { get; set; } = new();
    public List<Plant4Tablet> OperativesPlant { get; set; } = new();
    public List<VMtotalProjectInfo> VMprojectData { get; set; } = new();
    public List<Assignment> Assignments { get; set; } = new();
    public List<Abbreviations> Abbreviations { get; set; } = new();
    public List<VMexpansionReleaseData> EndPoints { get; set; } = new();
    public List<Rates> SchemeWorks { get; set; } = new();
    public List<ProjectWorks> ProjectWorks { get; set; } = new();
    public List<VMl4CabDetail> CabinetDetails { get; set; } = new();
    public List<SurveyQuestion> Questions { get; set; } = new();
    public List<Blockage> BlockageList { get; set; } = new();
    public List<VisitorLog> VisitorLogs { get; set; } = new();
    public List<DigPermit> Permits { get; set; } = new();
    public List<Labour> LabourFiles { get; set; } = new();
    public List<TransferPlantToOperatives> TransferPlantTo { get; set; } = new();
    public List<ClaimedFile> ClaimedFiles { get; set; } = new();
    public List<ReinstatementMeasure> ReinstatementMeasures { get; set; } = new();
    public List<Audit> Audits { get; set; } = new();
    public List<WeatherEvent> WeatherEvents { get; set; } = new();
    public List<CableStockUse> CableAuditMeasures { get; set; } = new();
    public List<FuelConsumption> FuelConsumption { get; set; } = new();
    public List<StockFuelDTO> StockFuel { get; set; } = new();
    public List<CableRuns> CableRuns { get; set; } = new();
    public List<ToolBoxTalks> ToolBoxTalkList { get; set; } = new();
    public List<UsersToolBoxTalks> UserToolboxtalkList { get; set; } = new();
    public List<ClaimedPole> PolesClaimed { get; set; } = new();
}