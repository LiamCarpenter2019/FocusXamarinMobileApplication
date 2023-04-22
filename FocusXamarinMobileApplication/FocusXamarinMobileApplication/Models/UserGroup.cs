#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class UserGroup : BusinessEntityBase
{
    public int UserId { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; } = "";
}