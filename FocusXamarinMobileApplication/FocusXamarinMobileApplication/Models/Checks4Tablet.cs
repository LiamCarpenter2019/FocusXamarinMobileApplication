#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Checks4Tablet : BusinessEntityBase
{
    public string Type { get; set; }
    public string CheckText { get; set; }
    public string ButtonType { get; set; }
    public int ListIndex { get; set; }
    public string NotifiableResponse { get; set; }
}