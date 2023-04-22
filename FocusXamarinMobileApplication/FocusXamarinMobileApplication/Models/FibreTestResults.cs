#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class FibreTestResults : BusinessEntityBase
{
    public int RemoteTableId { get; set; }
    public string Exchange_Code { get; set; }
    public string ASuite { get; set; }
    public string ARack { get; set; }
    public long? APosition { get; set; }
    public decimal? AttenuationAB1310nm { get; set; }
    public decimal? AttenuationAB1550nm { get; set; }
    public string Customer_Name { get; set; }
    public string BSuite { get; set; }
    public string BRack { get; set; }
    public string BPosition { get; set; }
    public decimal? AttenuationBA1310nm { get; set; }
    public decimal? AttenuationBA1550nm { get; set; }
    public decimal? AverageAttenuation1310nm { get; set; }
    public decimal? AverageAttenuation1550nm { get; set; }
    public long? TotalSplicesAB { get; set; }
    public decimal? DistanceAB { get; set; }
    public DateTime? TestDate { get; set; }
    public long? TesterId { get; set; }
    public string Identifier { get; set; }
    public bool TestPassed { get; set; }
    public long? QuoteId { get; set; }
    public decimal? ILMAAB1310 { get; set; }
    public decimal? ILMABA1310 { get; set; }
    public decimal? ILMAAB1550 { get; set; }
    public decimal? ILMABA1550 { get; set; }
    public decimal? AverageILMAAB1310 { get; set; }
    public decimal? AverageILMAAB1550 { get; set; }
}