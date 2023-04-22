#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class EventSeverity : BusinessEntityBase
{
    public int RemoteTableId { get; set; } = 0;
    public string Severity { get; set; } = "";
    public string AlertLevel { get; set; } = "";
    public string Category { get; set; } = "";
}