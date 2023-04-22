#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class ServicesCrossed4Tablet : BusinessEntityBase
{
    public int RemoteTableId { get; set; } = 0;
    public string ContractReference { get; set; }
    public string QuoteId { get; set; }
    public string ContractId { get; set; }
    public string GangLeaderId { get; set; }
    public string GangLeaderName { get; set; }
    public string ServicesCrossedData1 { get; set; }
    public DateTime PostedDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
}