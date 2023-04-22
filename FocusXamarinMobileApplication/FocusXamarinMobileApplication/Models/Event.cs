#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Event : BusinessEntityBase

{
    public int RemoteTableId { get; set; } = -1;
    public long? EventType { get; set; } = -1;
    public string Severity { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
    public long RecordedById { get; set; } = 0;
    public string RecordedByName { get; set; } = "";
    public string RecordedByEmail { get; set; } = "";
    public string RecordedByContactNumber { get; set; } = "";
    public string Location { get; set; } = "";
    public Guid Identifier { get; set; } = Guid.NewGuid();
    public long? QNumber { get; set; } = 0;
    public bool InjuredPerson { get; set; } = false;
    public string InitialDetails { get; set; } = "";
    public bool ThirdPartyDamage { get; set; } = false;
    public bool HospitalVisitRequired { get; set; } = false;
    public string Category { get; set; } = "";

    [Ignore] public List<RegisterUtilityDamage> UtilityDamages { get; set; } = new();

    [Ignore] public List<InvestigateDamages> Investigations { get; set; } = new();
}