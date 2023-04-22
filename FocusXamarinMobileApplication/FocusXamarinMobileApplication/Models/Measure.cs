#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Measure : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public string TobyId { get; set; } = "";
    public string Type { get; set; } = "";
    public string ItemCode { get; set; } = "";
    public string PropertiesServed { get; set; } = "";
    public string Surface { get; set; } = "";
    public string StartPrefix { get; set; } = "";
    public string StartPoint { get; set; } = "";
    public string EndPrefix { get; set; } = "";
    public string EndPoint { get; set; } = "";
    public decimal Length { get; set; } = 0;
    public decimal Width { get; set; } = 0;
    public string Comments { get; set; } = "";
    public string WalkPoint { get; set; } = "0";
    public Guid MeasureId { get; set; } = Guid.NewGuid();
    public long QNumber { get; set; } = 0;
    public Guid? AssignmentId { get; set; } = Guid.Empty;
    public string Location { get; set; } = "";
    public string CompletedById { get; set; } = "";
    public DateTime CompletedOn { get; set; } = DateTime.Now;
    public int WpIncremet { get; set; } = 0;
    public string Category { get; set; } = "AsBuilt";
}