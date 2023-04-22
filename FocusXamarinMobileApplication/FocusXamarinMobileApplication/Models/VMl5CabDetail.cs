#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class VMl5CabDetail : BusinessEntityBase
{
    public Guid Id { get; set; }
    public string AssociatedL4Number { get; set; }
    public string L5Number { get; set; }
    public string Location { get; set; }
    public int HomesServed { get; set; }
}