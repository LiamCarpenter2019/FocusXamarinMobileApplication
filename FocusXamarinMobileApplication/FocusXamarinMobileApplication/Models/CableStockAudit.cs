#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class CableStockAudit : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public string DrumNo { get; set; } = "0";
    public string CableType { get; set; } = "";
    public long FibreCoreCount { get; set; } = 0;
    public long InStock { get; set; } = 0;
    public bool Tested { get; set; } = false;

    [Ignore] public string Name { get; set; } = "";
    [Ignore] public List<CableStockUse> CableStockItems { get; set; } = new();
}