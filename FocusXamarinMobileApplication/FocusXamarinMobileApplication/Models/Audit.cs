#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Audit : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public Guid AuditId { get; set; } = Guid.NewGuid();
    public Guid AssignmentId { get; set; } = Guid.Empty;
    public long Qnumber { get; set; }
    public string GpsStart { get; set; } = "";
    public string GpsEnd { get; set; } = "";

    public string ProjectName { get; set; } = "";

    //--------------------Audit Info -------------------------
    public string Score { get; set; } = "";

    [Ignore] public long? MaxScore { get; set; } = 0;

    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; } = DateTime.Now;
    public DateTime AuditDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");

    public string AuditTime { get; set; } = "00:00:00";

    //-----------------Sections-------------------------
    public long? NonConformancies { get; set; }
    public long? Inadequacies { get; set; }
    public string Section1 { get; set; }
    public string Section2 { get; set; }
    public string Section3 { get; set; }
    public string Section4 { get; set; }
    public string Section5 { get; set; }
    public string Section6 { get; set; }
    public string Section7 { get; set; }
    public string Section8 { get; set; }
    public string Section9 { get; set; }
    public string Section10 { get; set; }

    // --------------Auditees/er Details------------------------------------
    public string GangLeaderId { get; set; } = "";
    public string ConductedBy { get; set; } = "";
    public long AuditorsFocusId { get; set; } = 0;
    public string AuditorSignature { get; set; } = "";
    public long AuditeeFocusId { get; set; } = 0;
    public string AuditeeSignature { get; set; } = "";
    public string AdditionalComments { get; set; }
    public string AuditorEmail { get; set; }

    [Ignore] public List<SurveyAnswers> Answers { get; set; } = new();
    [Ignore] public List<Ncr> NcrList { get; set; } = new();
    [Ignore] public List<AuthorisationDetail> SignatureDetails { get; set; }
}