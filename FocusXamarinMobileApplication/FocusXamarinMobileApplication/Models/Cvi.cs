#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Cvi : BusinessEntityBase
{
    public string RemoteTableId { get; set; } = "0";
    public string StreetName { get; set; } = "";
    public Guid CviId { get; set; } = Guid.NewGuid();
    public string Qnumber { get; set; } = "";
    public string ConfirmationOfVerbalInstructions { get; set; } = "";

    public string FromAddress { get; set; } = "";
    public string ToAddress { get; set; } = "";

    public DateTime LastUpdateTime { get; set; } = DateTime.Now;

    //public DateTime EstimatedCompletionDate { get; set; } = DateTime.Now;
    public bool Urgent { get; set; } = false;
    public bool ChageInCost { get; set; } = false;
    public bool DelayToProgramme { get; set; } = false;
    public Guid? AssignmentId { get; set; } = Guid.Empty;

    public string ChargedAt { get; set; } = "";
    public bool ScheduleOfRates { get; set; } = true;
    public bool DayWorkRates { get; set; } = false;
    public bool SpecifiedPrice { get; set; } = false;
    public bool PriceToBeAgreed { get; set; } = false;
    public bool Prolongation { get; set; } = false;

    public string Action { get; set; } =
        "We are making necessary arrangements to carry out this work and in the absence of written instruction, this confirmation of your request will be deemed to have the same validity as your written instruction under the contract.";

    public string RelatedTo { get; set; } = "";
    public string SupervisorName { get; set; } = "";
    public string OnBehalfOfClientName { get; set; } = "";
    public string OnBehalfOfClientId { get; set; } = "0";
    public string SupervisorId { get; set; } = "0";

    public string SupervisorSignature { get; set; } = "";
    public string OnBehalfOfClientSignature { get; set; } = "";

    public string PathToPDF { get; set; } = "";
    public string Email { get; set; }

    [Ignore] public List<AuthorisationDetail> SignatureDetails { get; set; } = new();
    [Ignore] public List<ProjectWorks> CviWorks { get; set; } = new();

    [Ignore] public List<Pictures4Tablet> CviPictures { get; set; } = new();
    [Ignore] public VMexpansionReleaseData StartEndpoint { get; set; }
    [Ignore] public VMexpansionReleaseData EndEndpoint { get; set; }
    public bool InstructedByClient { get; set; }
    public string ShiftPattern { get; internal set; }
}