using System.Collections.Generic;

namespace FocusXamarinMobileApplication.Models;

public class DataPassed2XamarinTablets
{
    public List<JobData4Tablet> JobData { get; set; }
    public List<Event> EventList { get; set; } = new();
    public List<EventManagementType> EventsManagementTypes { get; set; } = new();
    public List<TaskItem> TaskList { get; set; }
    public List<Person> PersonData { get; set; }
    public List<Docs4Tablet> DocumentsData { get; set; }
    public List<Pictures4Tablet> JobPictureData { get; set; }
    public List<Plant4Tablet> OperativesPlant { get; set; }
    public List<VMtotalProjectInfo> VMprojectData { get; set; }
    public List<Assignment> Assignments { get; set; }
    public List<Abbreviations> Abbreviations { get; set; }
    public List<VMexpansionReleaseData> EndPoints { get; set; }
    public List<Rates> SchemeWorks { get; set; }
    public List<Location> Locations { get; set; }
    public string ErrorMessage { get; set; }
    public List<Audit> Audits { get; set; }
    public List<VMl4CabDetail> CabinetDetails { get; set; }
    public List<ProjectWorks> ProjectWorkRates { get; set; }
    public List<SurveyQuestion> Questions { get; set; }
    public List<ProjectSummary> AllProjectSummaries { get; set; }
    public List<Blockage> BlockageList { get; set; }
    public List<VisitorLog> VisitorLogs { get; set; }
    public List<DigPermit> Permits { get; set; }
    public List<TransferPlantToOperatives> TransferPlantTo { get; set; }
    public List<Labour> LabourFiles { get; set; }
    public List<Holiday> Holidays { get; set; }
    public List<ServicesCrossed4Tablet> ServicesCrossed { get; set; }
    public List<ClaimedNotesFile> DailyDiary { get; set; }
    public List<ClaimedFile> ClaimedFiles { get; set; }
    public List<Measure> Measures { get; set; }
    public List<UserDailyProjectTimes> UserDailyProjectTimesList { get; set; }
    public List<UserDailyTime> UserDailyTimeList { get; set; }
    public List<UserDailyTimeNotes> UserDailyTimeNotesList { get; set; }
    public List<DailyProjectNotes> DailyProjectNotesList { get; set; }
    public List<InvestigationQuestion> InvestigationQuestions { get; set; }
    public List<UtilityCompany> UtilCompanyList { get; set; }
    public List<PublicUtilityDamageQuestion> DamageTypeList { get; set; }
    public List<InvestigateDamages> RegisteredDamages { get; set; }
    public List<DamageToInvestigate> DamagesToInvestigate { get; set; }
    public List<WeatherEvent> WeatherEvents { get; set; }
    public bool SavedToIpad { get; set; } = false;

    // public UtilityDamageData UtilitydamageData { get; set; } = new UtilityDamageData();
    public List<CableStockAudit> CableStockAudits { get; set; } = new();
    public List<CableStockUse> CableStockUse { get; set; } = new();
    public List<CableRuns> CableRuns { get; set; } = new();
    public List<Checks4Tablet> ChecksDetail { get; set; }

    public List<UsersToolBoxTalks> UserToolboxTalks { get; set; } = new();
    public List<ToolBoxTalks> ToolBoxTalks { get; set; } = new();
    public List<FibreTestResults> FibreTestResults { get; set; } = new();
}