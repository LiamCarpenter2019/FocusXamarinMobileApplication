namespace FocusXamarinMobileApplication.Models;

public class ExternalPersonnel : Person
{
    public long RemoteTableId { get; set; } = 0;
    public string Organisation { get; set; } = "";
    public string ReasonForVisit { get; set; } = "";
    public string DateOnSite { get; set; } = "";
    public string TimeArrived { get; set; } = "";
    public string TimeLeft { get; set; } = "";
    public string ContractReference { get; set; } = "";
    public long InvestigationId { get; set; } = 0;
    public int PublicUtilityDamageId { get; set; }
}