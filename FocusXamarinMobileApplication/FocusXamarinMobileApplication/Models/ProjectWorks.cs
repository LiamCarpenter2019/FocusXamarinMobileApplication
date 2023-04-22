#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class ProjectWorks : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public long QuoteId { get; set; } = 0;
    public string Header { get; set; } = "";
    public string Description { get; set; } = "";
    public string Qty { get; set; } = "0";
    public string BaseUnit { get; set; } = "";
    public Guid Identifier { get; set; } = Guid.Empty;
    public Guid AssignmentId { get; set; } = Guid.Empty;
    public Guid? WorksIdForDelete { get; set; } = Guid.NewGuid();
    public string Category { get; set; } = "";
    public string Status { get; set; } = "";
    public string GpsLocation { get; set; }
    public string ShiftPattern { get; set; }
}