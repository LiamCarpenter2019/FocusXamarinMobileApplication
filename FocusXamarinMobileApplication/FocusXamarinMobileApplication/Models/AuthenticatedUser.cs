#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class AuthenticatedUser : BusinessEntityBase
{
    public string UserUniqueId { get; set; }
    public string GivenName { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Forename { get; set; } = "";

    public string UserPrincipalName { get; set; } = "";
    public string TennantIdFromAzure { get; set; } = "";
    public string AccessTokenFromAzure { get; set; } = "";
    public string AccessTokenTypeFromAzure { get; set; } = "";

    public DateTime AccessTokenExpiryDate { get; set; }

    public DateTime LoginDateTime { get; set; } = DateTime.Now;
    public DateTime ExpiresOn { get; set; }

    public string StatusResult { get; set; }
}