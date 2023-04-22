#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class ReinstatementMeasure : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public long QuoteNumber { get; set; }
    public DateTime JobDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime DateSubmitted { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public long SupervisorId { get; set; }
    public long GangLeaderId { get; set; }
    public string SavedToServer { get; set; } = "false";
    public string ContractReference { get; set; }
    public string ItemCode { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }
    public string Material { get; set; }
    public string MaterialSize { get; set; }
}