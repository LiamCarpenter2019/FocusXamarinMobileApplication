#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Plant4Tablet : BusinessEntityBase
{
    public long RemotePlantId { get; set; }
    public long RemoteWheraboutsId { get; set; }
    public string Group { get; set; }
    public string Type { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Serial { get; set; }
    public string Ref { get; set; }
    public string Supplier { get; set; }
    public bool Hired { get; set; }
    public string AssetNo { get; set; }
    public DateTime NextServiceDate { get; set; }
    public string NextServiceType { get; set; }
    public string CurrentStatus { get; set; } // MyPossession|TransferIn|TransferOut
    public long IssuedToId { get; set; }

    public string IssuedToName { get; set; }

    // New 22nd July 2017 - Michael
    public bool Ontransfer { get; set; }
    public bool TransferOutSelected { get; set; }
    public string TransferToName { get; set; }
    public long TransferToId { get; set; }
    public string TransferFromName { get; set; }
    public long TransferFromId { get; set; }

    [Ignore] public List<Checks4TabletResponses> Responses4ThisPlant2day { get; set; }
    public bool ChecksComplete { get; set; }
}