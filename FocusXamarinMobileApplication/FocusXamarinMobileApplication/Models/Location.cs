#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Location : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;

    public string Name { get; set; }
    public string FeatureName { get; set; }
    public string AdminArea { get; set; }
    public string SubAdminArea { get; set; }
    public string CountryName { get; set; }
    public string CountryCode { get; set; }
    public string Locality { get; set; }
    public string SubLocality { get; set; }
    public string PostalCode { get; set; }
    public string ThroughFare { get; set; }
    public string SubThroughFare { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public long QuoteId { get; set; }
    public Guid? Identifier { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal? QuestionId { get; set; }
    public string V1 { get; set; }
    public string V2 { get; set; }

    [Ignore] public Position Position { get; set; }
}