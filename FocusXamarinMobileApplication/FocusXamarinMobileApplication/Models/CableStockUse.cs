#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class CableStockUse : BusinessEntityBase
{
    public string RemoteTableId { get; set; } = "0";
    public string DrumNo { get; set; } = "0";
    public string ContractID { get; set; } = "";
    public string ContractName { get; set; } = "";
    public long HowMuchUsed { get; set; } = 0;
    public long UsedById { get; set; } = 0;
    public string UsedByNBame { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
    public string CableType { get; set; } = "";
    public long CoreCount { get; set; } = 0;
    public long StartReading { get; set; } = 0;
    public long EndReading { get; set; } = 0;
    public Guid ClaimId { get; set; } = Guid.Empty;
    public string CableRunIdentifier { get; set; } = "";
    public decimal? ExpectedStartReading { get; set; } = 0;
}