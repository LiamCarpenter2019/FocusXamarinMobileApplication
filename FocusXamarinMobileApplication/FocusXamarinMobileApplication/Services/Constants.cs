using System;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services.Interfaces;
using Microsoft.AppCenter.Analytics;
using SQLite;

namespace FocusXamarinMobileApplication.Services;

public class Constants : IConstants
{
    public const string DatabaseFilename = "FocusMobileV3Database.";

    public const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLiteOpenFlags.SharedCache;

    public string ApprovedUtilityDamageUrl = string.Empty;
    public string Authority = string.Empty;
    public string AzureSasUri = string.Empty;

    public string ClientId = string.Empty;
    public string ClientSideUrl = string.Empty;

    public string CloudBlobContainer = string.Empty;
    public string defaultAADAppId = string.Empty;

    public string FocusDataContainerInAzure = string.Empty;
    public string FocusMobileWebApiUrl = string.Empty;

    public string FriendlyName = string.Empty;
    public string FullSubmitUrl = string.Empty;
    public string GetMeUrl = string.Empty;
    public string GraphResourceUri = string.Empty;

    public string LoginUrl = string.Empty;
    public string PartialSubmitUrl = string.Empty;
    public string Password = string.Empty;
    public string ReturnUri = string.Empty;
    public string SPORootSiteUrl = string.Empty;
    public CloudStorageAccount StorageAccount;
    public string SubmitDamage = string.Empty;
    public string TabletApiResourceId = string.Empty;

    public string TabletclientId = string.Empty;
    public string TabletReturnUri;
    public string TabletTenant = string.Empty;
    public string UpdatePinUrl = string.Empty;

    public string Username = string.Empty;

    public Constants()
    {
        GetCurrentApiData();
    }

    public static string DatabasePath
    {
        get
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DatabaseFilename);
        }
    }

    public bool AreThereAnyConstants()
    {
        try
        {
            var anyConstants = App.Database.GetItems<ApiStructure>()?.ToList()?.FirstOrDefault();

            if (anyConstants != null)
            {
                AssignApiData(anyConstants);

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            return false;
        }

        return false;
    }

    public async Task<string> GetNewApiString(string aPIcode)
    {
        var wa = new WebApi();
        var rv = await wa.GetNewApIdata(aPIcode);
        if (rv != null && rv.FriendlyName != null)
        {
            if (rv.FriendlyName != "-^-FAIL-^-")
            {
                DropDbTable(); // Drop Db Table in case there are schema changes
                CreateDBifNotExists(); // Create Table again for ApiStructure
                await ClearAllRows();
                await AddApiResponse(rv);
                return rv.FriendlyName;
            }

            return "Fail";
        }

        return "Fail";
    }

    private bool DropDbTable()
    {
        var returnValue = true;
        try
        {
            App.Database.ClearTable<ApiStructure>(); //drop table
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return false;
        }

        return returnValue;
    }

    private bool CreateDBifNotExists()
    {
        var returnValue = true;
        try
        {
            App.Database.CreateTable<ApiStructure>(); //create table
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return false;
        }

        return returnValue;
    }

    private async Task ClearAllRows()
    {
        App.Database.ClearTable<ApiStructure>();
    }

    private async Task AddApiResponse(ApiStructure passedResponseData)
    {
        try
        {
            // NavigationalParameters.ApiStructyure = passedResponseData;
            App.Database.SaveItem(passedResponseData);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");


            var n = ex.Message;
        }
    }

    private void AssignApiData(ApiStructure data)
    {
        FriendlyName = data.FriendlyName;
        FocusMobileWebApiUrl = data.ApiUrl;
        UpdatePinUrl = data.UpdatePinUrl;

        CloudBlobContainer = data.CloudBlobContainer;
        StorageAccount = CloudStorageAccount.Parse(data.CloudStorageAccount);

        ClientId = data.ClientId;
        Authority = data.Authority;
        ReturnUri = data.ReturnUri;
        GraphResourceUri = data.GraphResourceUri;
        GetMeUrl = data.GetMeUrl;

        FocusDataContainerInAzure = data.FocusDataContainerInAzure;

        LoginUrl = data.FormsAppLoginUrl;
        SubmitDamage = data.SubmitDamageUrl;
        PartialSubmitUrl = data.PartialSubmitUrl;
        ApprovedUtilityDamageUrl = data.ApprovedUtilityDamageUrl;
        FullSubmitUrl = data.FullSubmitUrl;
        AzureSasUri = data.AzureSasUri;

        TabletclientId = data.TabletclientId;
        TabletTenant = data.TabletTenant;
        TabletReturnUri = data.TabletReturnUri;
        TabletApiResourceId = data.TabletApiResourceId;

        Username = data.Username;
        Password = data.Password;
        defaultAADAppId = data.defaultAADAppId;
        SPORootSiteUrl = data.SPORootSiteUrl;
        ClientSideUrl = data.ClientSideUrl;
    }

    private void GetCurrentApiData()
    {
        try
        {
            CreateDBifNotExists();
            var anyConstants = App.Database.GetItems<ApiStructure>();
            AssignApiData(anyConstants?.First());
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }
}