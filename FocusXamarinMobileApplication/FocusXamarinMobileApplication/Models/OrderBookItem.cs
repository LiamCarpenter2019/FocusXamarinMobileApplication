#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class OrderBookItem : BusinessEntityBase
{
    public long OrderNumber { get; set; } = 0;

    public string Description { get; set; } = "";

    public long Qty { get; set; } = 0;

    public decimal PricePerUnit { get; set; } = 0;

    public Guid OrderGuid { get; set; } = Guid.NewGuid();
}