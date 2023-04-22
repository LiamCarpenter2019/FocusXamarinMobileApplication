#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class PlantTransfers : BusinessEntityBase
{
    public long RemoteId { get; set; }
    public long PlantId { get; set; }
    public string PlantGroup { get; set; } // *****
    public string PlantType { get; set; } // *****
    public string PlantRef { get; set; } // *****
    public long PlantAssetNo { get; set; } // *****
    public DateTime DateTimeTransferStarted { get; set; }

    public string TransferInOrOut { get; set; }
    public long TransferFromId { get; set; }
    public string TransferFromName { get; set; } // *****
    public long TransferToId { get; set; }
    public string TransferToName { get; set; } // *****
    public string TransferComments { get; set; }
    public long TransferOutById { get; set; } // *****
    public string TransferOutByName { get; set; } // *****
    public string Picture1Filename { get; set; }
    public string Picture1Status { get; set; }
    public string Picture2Filename { get; set; }
    public string Picture2Status { get; set; }
    public string Picture3Filename { get; set; }
    public string Picture3Status { get; set; }
    public string Picture4Filename { get; set; }
    public string Picture4Status { get; set; }

    public string TransferSignature { get; set; }
    public string TransferSignatureStatus { get; set; } = "Transferred 2 Storage";


    [Ignore] public List<Checks4TabletResponses> IncomingCheckResults { get; set; }

    public string Status { get; set; }

    public DateTime LastUpdateDateTime { get; set; }
}