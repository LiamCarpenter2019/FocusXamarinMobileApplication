#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class EventManagementType : BusinessEntityBase
{
    public int RemoteTableId { get; set; }
    public string Category { get; set; }
    public string Title { get; set; }
}