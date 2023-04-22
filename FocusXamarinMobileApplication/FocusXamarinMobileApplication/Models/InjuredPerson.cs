#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class InjuredPerson : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public Guid PublicUtilityDamageGuid { get; set; } = Guid.Empty;
    public int PublicUtilityDamageId { get; set; } = 0;
    public string Position { get; set; } = "";
    public string ContactNumber { get; set; } = "";
    public string Injury { get; set; } = "";
    public string NextOfKinName { get; set; } = "";
    public string NextOfKinNumber { get; set; } = "";
    public string InjuredName { get; set; } = "";
    public DateTime InputOn { get; set; } = DateTime.Now;
    public string InvestigationId { get; set; } = "";
}