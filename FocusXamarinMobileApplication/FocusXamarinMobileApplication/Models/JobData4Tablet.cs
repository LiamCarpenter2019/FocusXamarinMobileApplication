#region

#endregion

using System.Text.Json.Serialization;

namespace FocusXamarinMobileApplication.Models;

//[Table("JobData")]
public class JobData4Tablet : BusinessEntityBase
{
    //private bool disposed = false;
    //[PrimaryKey, AutoIncrement]
    public long RemoteTableId { get; set; } = 0;
    public long JobId { get; set; }
    public long QuoteNumber { get; set; }
    public long ContractNumber { get; set; }
    public long WorksNumber { get; set; }
    public long VariationNumber { get; set; }

    [Ignore] public string JobDateString { get; set; } = "";

    public DateTime _jobDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");

    public DateTime JobDate
    {
        get => _jobDate;
        set
        {
            _jobDate = value;
            JobDateString = $"{JobDate:dd/MM/yyyy}";
        }
    }

    public string ProjectName { get; set; } = "";
    public string ClientName { get; set; }
    public long ProjectManagerId { get; set; }
    public long SupervisorId { get; set; }
    public long GangLeaderId { get; set; }
    public string OperativeIdsPiped { get; set; }
    public string GangLeaderName { get; set; }
    public string OperativeNames { get; set; }
    public string ContractPrefix { get; set; }
    public bool SavedToServer { get; set; } = false;
    public string ContractReference { get; set; }
    public long SubcontractorLabourTeamId { get; set; }
    public long SubContractorCompanyId { get; set; }
    public long BaseContractId { get; set; }
    public bool DailyChecksCompleted { get; set; } = false;
    public bool DailyChecksPosted { get; set; } = false;


    [JsonIgnore] public bool IsSelected { get; set; }

    [Ignore] public Gang JobGang { get; set; }

    [Ignore] public ProjectSummary JobProjectSummary { get; set; }

    //  [Ignore] public ServicesCrossed4Tablet CrossedUtilities { get; set; }
    [Ignore] public List<ServicesCrossed4Tablet> CrossedUtilities { get; set; } = new();

    [Ignore] public List<ClaimedNotesFile> ClaimedNotes { get; set; } = new();
    [Ignore] public List<VisitorLog> VisitorList { get; set; } = new();
    [Ignore] public List<ClaimedFile> ClaimedFiles { get; set; } = new();
    [Ignore] public List<Labour> LabourFiles { get; set; } = new();
    [Ignore] public List<Audit> Audits { get; set; } = new();
}