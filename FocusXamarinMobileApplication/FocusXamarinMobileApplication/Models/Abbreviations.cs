//using StandardLibrary.Data;

#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Abbreviations : BusinessEntityBase
{
    public string RemoteTableId { get; set; } = "";
    public string Abb { get; set; } = "";
    public string FullName { get; set; } = "";
}