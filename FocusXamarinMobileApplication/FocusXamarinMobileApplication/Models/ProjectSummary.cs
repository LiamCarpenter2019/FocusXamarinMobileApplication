#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class ProjectSummary : BusinessEntityBase
{
    public long RemoteTableId { get; set; }
    public long QNumber { get; set; } = 0;
    public string Commercial { get; set; } = "";
    public string Operational { get; set; } = "";
    public string Sheq { get; set; } = "";
    public string EmergencyDetails { get; set; } = "";
}