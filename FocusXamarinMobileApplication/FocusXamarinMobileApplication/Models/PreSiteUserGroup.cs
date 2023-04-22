#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class PreSiteUserGroup : BusinessEntityBase
{
    public long FocusId { get; set; }
    public string StatusText { get; internal set; }
    public Color StatusColour { get; internal set; }
    public object CompletedOn { get; internal set; }
    public Guid? AssignmentId { get; internal set; }
    public string StreetName { get; internal set; }
}