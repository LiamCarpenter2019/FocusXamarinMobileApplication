#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class FuelConsumption : BusinessEntityBase
{
    public long RemoteTableId { get; set; }
    public DateTime DateOfEntry { get; set; } = DateTime.Now;
    public string FullName { get; set; }
    public string RegAssetNumber { get; set; }
    public string Type { get; set; }
    public decimal HoursOrMilage { get; set; }
    public decimal StartReading { get; set; }
    public decimal Qty { get; set; } = 0;
    public decimal EndReading { get; set; }
}