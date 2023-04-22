#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Order : BusinessEntityBase
{
    public long OrderNumber { get; set; } = 0;

    public DateTime Date { get; set; } = DateTime.Now;

    public string Supplier { get; set; } = "";

    public string Contract { get; set; } = "";

    public string OrderByName { get; set; } = "";

    public long QuoteNumber { get; set; } = 0;

    public long OrderedById { get; set; } = 0;

    public string AuthorizedByName { get; set; } = "";

    public string ImageFileName { get; set; } = "";

    public long AuthorizedById { get; set; } = 0;

    public string Comments { get; set; } = "";

    public decimal PriceExcludingVAT { get; set; } = 0;

    public bool Approved { get; set; } = false;

    public bool Void { get; set; } = false;

    public Guid OrderGuid { get; set; } = Guid.NewGuid();

    [Ignore] public List<Pictures4Tablet> Images { get; set; } = new();

    [Ignore] public List<OrderBookItem> OrderBookItem { get; set; } = new();
}