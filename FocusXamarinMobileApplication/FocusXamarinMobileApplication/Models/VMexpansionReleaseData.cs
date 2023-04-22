#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class VMexpansionReleaseData : BusinessEntityBase
{
    private string businessName = "";

    public long RemoteTableId { get; set; } = 0;

    //  public string EndPointNumber { get; set; } = ;
    public int Qnumber { get; set; } = 0;
    public string VmnbUnumber { get; set; } = "";
    public string L4Number { get; set; } = Guid.NewGuid().ToString();
    public string PlotNumber { get; set; } = "";
    public string Uprn { get; set; } = "";
    public string BuildingNumber { get; set; } = "";
    public string BuildingName { get; set; } = "";
    public string StreetName { get; set; } = "";
    public string TownCity { get; set; } = "";
    public string Postcode { get; set; } = "";
    public string DnAaddressType { get; set; } = "";
    public string BuildingStandard { get; set; } = "";
    public string L4Cab { get; set; } = "";
    public string L5Cab { get; set; } = "";
    public string CatvBb { get; set; } = "";
    public string Telco { get; set; } = "";
    public string ReleasedBy { get; set; } = "";
    public string ReleasedDate { get; set; } = "";
    public string Comments { get; set; } = "";
    public decimal TobyLength { get; set; } = 0;
    public decimal TobyWidth { get; set; } = 0;
    public string DropLength { get; set; } = "";
    public Guid ClaimId { get; set; } = Guid.NewGuid();
    public decimal DuctLabel { get; set; } = 0;
    public bool SavedToServer { get; set; } = true;
    public bool Blocked { get; set; } = false;
    public string SurfaceType { get; set; }
    public decimal Depth { get; set; }
    public string longitude { get; set; }
    public string latitude { get; set; }
    public string Status { get; set; } = "Not Started";

    [Ignore]
    public int Order
    {
        get
        {
            try
            {
                var xx = StreetName.Split('-').LastOrDefault();

                return Convert.ToInt32(xx);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

    [Ignore] public Blockage Blockage { get; set; }

    [Ignore] public string BusinessName => $"{StreetName} - {DropLength}";

    [Ignore]
    public Color StatusColour
    {
        get
        {
            switch (Status.ToLower())
            {
                case "not started":
                    return Color.White;
                //case "survey commenced":
                //case "asbuilt commenced":
                //    return Color.Orange;
                case "asbuilt":
                    return Color.Green;
                default:
                    return Color.Orange;
            }

            ;
        }
    }

    [Ignore] public bool _showRemoveButton { get; set; }

    [Ignore]
    public bool ShowRemoveButton
    {
        get => _showRemoveButton;
        set => _showRemoveButton = value;
    }
}