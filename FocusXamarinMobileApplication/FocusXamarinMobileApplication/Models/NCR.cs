#region

#endregion

using System;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class Ncr : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public Guid AuditId { get; set; } = Guid.Empty;
    public decimal QuestionId { get; set; } = 0;
    public Guid NcrId { get; set; } = Guid.NewGuid();
    public string PreventiveAction { get; set; }
    public string Severity { get; set; } = "";
    public string RootCause { get; set; } = "";
    public string IntermediateActions { get; set; } = "";
    public bool? CorrectedOnSite { get; set; } = false;
    public string CorrectiveActions { get; set; } = "";
    public string AllocatedNcr { get; set; } = "";
    public string Category { get; set; } = "";
    public long SupervisorId { get; set; }
    public string ComplainantName { get; set; }
    public string ComplainantTelephone { get; set; }
    public string ComplainantEmail { get; set; }
    public string ComplainantAddress { get; set; }
    public string ComplainantStreet { get; set; }
    public string ComplainantTownCity { get; set; }
    public string ComplainantPostalcode { get; set; }

    [Ignore] public SurveyAnswers SurveyAnswer { get; set; }
}


//public string recordedBy { get; set; }
//public string source { get; set; }
//public string sourceOtherText { get; set; }
//public string comments { get; set; }
//public string antCompDate { get; set; }
//public string responsibility { get; set; }
//public string relevantContract { get; set; }
//public List<string> gangResponsible { get; set; } xx

//public string NCRno { get; set; }
//public string contractReference { get; set; }
//public string recordedDate { get; set; }

//public string contractName { get; set; }
//public string clientName { get; set; }
//public string clientContact { get; set; }

//public string theProblem { get; set; }
//public string theCause { get; set; }
//public string causeAssessedBy { get; set; }
//public string correctiveAction { get; set; }
//public string correctiveActionBy { get; set; }
//public string associatedProblem { get; set; }
//public string preventativeAction { get; set; }
//public string preventativeActionBy { get; set; }

//public string supervisor { get; set; } xx
//public string Ganger { get; set; }
//public string Operatives { get; set; }

//public string closedOffBy { get; set; }
//public string closedOffDate { get; set; }
//public string agreedBy { get; set; }
//public string agreedByDate { get; set; }