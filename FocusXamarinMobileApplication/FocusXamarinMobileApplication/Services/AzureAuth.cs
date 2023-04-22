using System;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using Microsoft.Identity.Client;

namespace FocusXamarinMobileApplication.Services;

public class AzureAuth
{
    //public static string tabletclientId = "8deceab9-4135-4561-bd44-9dbbf9097d6f";
    //public static string tabletTenant = "scdconstruction.co.uk";
    //public static string tabletAuthority = String.Format("https://login.microsoftonline.com/{0}", tabletTenant);
    //public static Uri tabletReturnUri = new Uri("https://scdconstruction.co.uk/XamarinClientServiceAzureAuth");

    //const string tabletApiResourceId = "8deceab9-4135-4561-bd44-9dbbf9097d6f";

    [Time]
    public async Task<AuthenticatedUser> CheckAuthStatus()
    {
        var cv2 = new Constants();

        AuthenticationResult authResult = null;
        var results = new AuthenticatedUser();

        try
        {
            //var authContext =
            //    new AuthenticationContext($"https://login.microsoftonline.com/{cv2.TabletTenant}");

            //authResult = await authContext.AcquireTokenAsync(cv2.TabletApiResourceId, cv2.TabletclientId,
            //    new Uri(cv2.TabletReturnUri), parent);

            var au = new AuthenticatedUser
            {
                //GivenName = $"{authResult.UserInfo.GivenName} {authResult.UserInfo.FamilyName}",
                //Surname = authResult.UserInfo.FamilyName,
                //Forename = authResult.UserInfo.GivenName,

                //UserPrincipalName = authResult.UserInfo.DisplayableId,
                //TennantIdFromAzure = authResult.TenantId,
                //AccessTokenFromAzure = authResult.AccessToken,
                //AccessTokenTypeFromAzure = authResult.AccessTokenType,

                AccessTokenExpiryDate = Convert.ToDateTime(authResult.ExpiresOn),

                StatusResult = "Success"
            };

            // await SaveUpdateAccessToken(au);

            return au;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return new AuthenticatedUser { StatusResult = ex.Message };
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="userAuthModel"></param>
    /// <returns>bool</returns>
    [Time]
    public async Task<bool> SaveUpdateAccessToken(AuthenticatedUser userAuthModel)
    {
        try
        {
            var currentDateTime = DateTime.Now;

            var existingUserAuth = GetUserAuthInformation();

            if (existingUserAuth != null)
            {
                userAuthModel.UserUniqueId = existingUserAuth?.UserUniqueId;

                userAuthModel.LoginDateTime = string.IsNullOrEmpty(existingUserAuth.AccessTokenFromAzure)
                    ? currentDateTime
                    : existingUserAuth.LoginDateTime;
            }

            userAuthModel.ExpiresOn = currentDateTime.AddHours(10);

            if (userAuthModel.Id > 0)
            {
                App.Database.SaveItem(userAuthModel);
            }
            else
            {
                userAuthModel.LoginDateTime = currentDateTime;
                App.Database.SaveItem(userAuthModel);
            }

            return true;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return false;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="userUniqueId"></param>
    /// <returns></returns>
    [Time]
    public async Task<bool> SignoutUser()
    {
        var cv2 = new Constants();
        try
        {
            //  if (DeviceInfo.Platform == DevicePlatform.iOS)
            //     foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            //      NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);

            //var authContext =
            //    new AuthenticationContext($"https://login.microsoftonline.com/{cv2.TabletTenant}");


            //authContext.TokenCache.Clear();


            // Now wipe userdata from Database
            ClearAllRows();
            return true;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="userUniqueId"></param>
    /// <returns>UserAuth</returns>
    [Time]
    public AuthenticatedUser GetUserAuthInformation()
    {
        try
        {
            return App.Database.GetItems<AuthenticatedUser>().ToList().FirstOrDefault();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex.Message;
        }

        return new AuthenticatedUser();
    }

    [Time]
    public void ClearAllRows()
    {
        App.Database.ClearTable<AuthenticatedUser>();
    }

    [Time]
    public bool CreateDBifNotExists()
    {
        try
        {
            App.Database.CreateTable<AuthenticatedUser>(); //create table

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}