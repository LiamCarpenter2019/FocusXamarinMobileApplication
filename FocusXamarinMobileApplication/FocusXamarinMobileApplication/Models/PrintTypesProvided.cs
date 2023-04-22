#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class PrintTypesProvided : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public long InvestigationId { get; set; } = 0;
    public long PublicUtilityDamageId { get; set; } = 0;

    public bool Electric { get; set; } = false;
    public bool Gas { get; set; } = false;
    public bool Water { get; set; } = false;
    public bool BT { get; set; } = false;
    public bool Sewer { get; set; } = false;
    public bool CCTV { get; set; } = false;

    public bool? PrintsInColour { get; set; }
    public bool? AdequateInformation { get; set; }
    public bool? PrintsProvided { get; set; }
    public bool IsComplete { get; set; } = false;
}