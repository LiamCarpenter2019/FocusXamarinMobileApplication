#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class StockFuelDTO : BusinessEntityBase
{
    public long RemoteId { get; set; } = 0;
    public string Type { get; set; } = "";
    public decimal StartReading { get; set; } = 0;
    public decimal EndReading { get; set; } = 0;
    public decimal TotalLitres { get; set; } = 0;
    public long QNumber { get; set; } = 0;
    public long RecievedBy { get; set; } = 0;
    public string SignatureFileName { get; set; } = "";
    public DateTime DateTimeOfDelivery { get; set; } = DateTime.Now;
}