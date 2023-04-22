#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class VMl4CabDetail : BusinessEntityBase
{
    public string IdGuid { get; set; } = Guid.Empty.ToString();

    // public long RemoteTableId { get; set; } = 0;
    public string VmnbUnumber { get; set; } = "";
    public long QuoteId { get; set; } = 0;
    public string L4Number { get; set; } = Guid.Empty.ToString();
    public string CabinetDetails { get; set; } = "";
    public string Location { get; set; } = "";
    public string Existing { get; set; } = "";
    public int? HomesServed { get; set; } = 0;
    public string UcNc { get; set; } = "";
    public string PreSitedById { get; set; } = "";
    public string PreSiteStatus { get; set; } = "";
    public DateTime LastUpdateTime { get; set; } = DateTime.MinValue;
    public string CityFibreRef { get; set; } = "";

    [Ignore] public List<VMl5CabDetail> L5CabDetails { get; set; } = new();

    [Ignore] public List<VMexpansionReleaseData> EndPoints { get; set; } = new();
}