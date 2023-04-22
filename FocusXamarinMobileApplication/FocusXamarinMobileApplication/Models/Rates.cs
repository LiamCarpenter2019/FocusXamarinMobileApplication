#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Rates : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public string WorkHeader { get; set; } = "";
    public string WorkDescription { get; set; } = "";
    public string BaseUnit { get; set; } = "";
    public long BaseContractId { get; set; }
    public string Category { get; set; } = "";
}