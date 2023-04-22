#region

#endregion

using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class UtilityCompany : BusinessEntityBase
{
    public string CompanyName { get; set; }
    public string CompanyId { get; set; }
    public string Telephone { get; set; }
    public string Type { get; set; }
    public string Email { get; set; }
}