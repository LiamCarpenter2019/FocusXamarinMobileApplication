#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Assignment : BusinessEntityBase
{
    public Guid AssignmentId { get; set; } = Guid.Empty;
    public long RemoteTableId { get; set; } = 0;
    public string LocationName { get; set; } = "";
    public long LocationId { get; set; } = 0;
    public long Qnumber { get; set; } = 0;
    public string Cnumber { get; set; } = "";
    public long Worksnumber { get; set; } = 0;
    public string ClientName { get; set; } = "";
    public string ProjectName { get; set; } = "";
    public DateTime CompletedOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string Complete { get; set; } = "false";
    public string Category { get; set; } = "";
    public string StreetName { get; set; } = "";
    public long PreSiteById { get; set; } = 0;
    public long? TermContract { get; set; } = 0;
    public string Converted { get; set; } = "";
    public string StartLocation { get; set; }
    public string EndLocation { get; set; }
    public string Type { get; set; }

    [Ignore] public Audit Audit { get; set; }
    // [Ignore] public List<VMl4CabDetail> AssignmentAreas { get; set; } = new List<VMl4CabDetail>();

    [Ignore] public List<Pictures4Tablet> SurveyPictures { get; set; } = new();

    [Ignore] public List<SurveyAnswers> SurveyAnswers { get; set; } = new();

    // [Ignore] public List<Rates> SchemeWorks { get; set; } = new List<Rates>();

    [Ignore] public List<ProjectWorks> ProjectWorks { get; set; } = new();

    [Ignore] public List<SurveyComments> SurveyComments { get; set; } = new();

    [Ignore] public List<Dfe> DfeList { get; set; } = new();

    [Ignore] public List<Measure> Measures { get; set; } = new();

    [Ignore] public List<AuthorisationDetail> SignatureDetails { get; set; }

    //     [Ignore] public List<Abbreviations> Abbreviations { get; set; } = new List<Abbreviations>();

    [Ignore] public List<DigPermit> PermitToDigList { get; set; } = new();
    [Ignore] public List<Audit> Audits { get; set; } = new();
    [Ignore] public List<Cvi> Cvi { get; set; } = new();

    [Ignore] public List<Location> LocationList { get; set; }
    [Ignore] public Color StatusColour { get; set; }

    [Ignore] public string StatusText { get; set; }

    [Ignore] public bool Editable { get; set; }
    //public string KMZ { get; set; }
}