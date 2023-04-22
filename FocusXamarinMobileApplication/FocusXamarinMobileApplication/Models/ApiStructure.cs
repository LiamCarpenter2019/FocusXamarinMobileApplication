#region

#endregion

using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class ApiStructure : BusinessEntityBase
{
    public string FriendlyName { get; set; }
    public string ApiUrl { get; set; }
    public string UpdatePinUrl { get; set; }
    public string CloudBlobContainer { get; set; }
    public string CloudStorageAccount { get; set; }

    public string ClientId { get; set; }
    public string Authority { get; set; }
    public string ReturnUri { get; set; }
    public string GraphResourceUri { get; set; }
    public string GetMeUrl { get; set; }
    public string FocusDataContainerInAzure { get; set; }

    public string FormsAppLoginUrl { get; set; }
    public string SubmitDamageUrl { get; set; }
    public string PartialSubmitUrl { get; set; }
    public string ApprovedUtilityDamageUrl { get; set; }
    public string FullSubmitUrl { get; set; }
    public string AzureSasUri { get; set; }

    public string TabletclientId { get; set; }
    public string TabletTenant { get; set; }
    public string TabletReturnUri { get; set; }
    public string TabletApiResourceId { get; set; }

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string defaultAADAppId { get; set; } = string.Empty;
    public string SPORootSiteUrl { get; set; } = string.Empty;
    public string ClientSideUrl { get; set; } = string.Empty;
}