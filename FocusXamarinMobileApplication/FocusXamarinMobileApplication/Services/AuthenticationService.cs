using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using MethodTimer;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace FocusXamarinMobileApplication.Services;

public class AuthenticationService
{
    // The MSAL Public client app
    //      private static IPublicClientApplication PublicClientApp;

    private static readonly string MSGraphURL = "https://graph.microsoft.com/v1.0/";

    private static AuthenticationResult authResult;
    //Set the scope for API call to user.read
    // private string[] scopes = new string[] { "user.read" };

    //  private const string ClientId = "6fc0ff15-e87f-4f8b-a1bc-3e10323e769f";
    //private const string Tenant = "1074734c-beeb-4a27-aa02-b0cad30e52ae";
    // Microsoft Graph permissions used by app
    public readonly string[] Scopes = OAuthSettings.Scopes.Split(' ');


    [Time]
    public async Task<AuthenticatedUser> Authenticate()
    {
        try
        {
            // Sign-in user using MSAL and obtain an access token for MS Graph
            var graphClient = await SignInAndInitializeGraphServiceClient(Scopes);

            // Call the /me endpoint of Graph
            var graphUser = await graphClient.Me.Request().GetAsync();

            if (graphUser != null && graphUser.UserPrincipalName != null && authResult != null)
            {
                NavigationalParameters.AuthorisedUser = new AuthenticatedUser
                {
                    GivenName = graphUser.DisplayName,
                    Surname = graphUser.Surname,
                    Forename = graphUser.GivenName,
                    UserPrincipalName = graphUser.UserPrincipalName,
                    TennantIdFromAzure = authResult.TenantId,
                    AccessTokenFromAzure = authResult.AccessToken,
                    UserUniqueId = authResult.UniqueId,
                    AccessTokenTypeFromAzure = "Bearer",
                    LoginDateTime = DateTime.Now,
                    StatusResult = "Success"
                };

                return NavigationalParameters.AuthorisedUser;
            }

            return new AuthenticatedUser { StatusResult = "Authentication Failed. Please try again!" };
        }
        catch (Exception ex)
        {
            //return new AuthenticatedUser
            //{
            //    GivenName = "Team Leader1",
            //    Surname = "Leader1",
            //    Forename = "Team",
            //    UserPrincipalName = "tleader1@scd-ltd.com",
            //    TennantIdFromAzure = "1074734c-beeb-4a27-aa02-b0cad30e52ae",
            //    AccessTokenFromAzure = "eyJ0eXAiOiJKV1QiLCJub25jZSI6IkJiM2RTb1k1U2E4VlRZX2tQZ2MyOHdrbUphd2NhcjdnMW1feHFnWkZpYVkiLCJhbGciOiJSUzI1NiIsIng1dCI6IjVPZjlQNUY5Z0NDd0NtRjJCT0hIeEREUS1EayIsImtpZCI6IjVPZjlQNUY5Z0NDd0NtRjJCT0hIeEREUS1EayJ9.eyJhdWQiOiIwMDAwMDAwMy0wMDAwLTAwMDAtYzAwMC0wMDAwMDAwMDAwMDAiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8xMDc0NzM0Yy1iZWViLTRhMjctYWEwMi1iMGNhZDMwZTUyYWUvIiwiaWF0IjoxNjEwMzkwOTg5LCJuYmYiOjE2MTAzOTA5ODksImV4cCI6MTYxMDM5NDg4OSwiYWNjdCI6MCwiYWNyIjoiMSIsImFjcnMiOlsidXJuOnVzZXI6cmVnaXN0ZXJzZWN1cml0eWluZm8iLCJ1cm46bWljcm9zb2Z0OnJlcTEiLCJ1cm46bWljcm9zb2Z0OnJlcTIiLCJ1cm46bWljcm9zb2Z0OnJlcTMiLCJjMSIsImMyIiwiYzMiLCJjNCIsImM1IiwiYzYiLCJjNyIsImM4IiwiYzkiLCJjMTAiLCJjMTEiLCJjMTIiLCJjMTMiLCJjMTQiLCJjMTUiLCJjMTYiLCJjMTciLCJjMTgiLCJjMTkiLCJjMjAiLCJjMjEiLCJjMjIiLCJjMjMiLCJjMjQiLCJjMjUiXSwiYWlvIjoiRTJKZ1lMQnFhdDc0Y0ZMekUyNkdQL2ZDc3hidTF5NWQ3ZS91NlJtYy9FZ2h4SFpSMm1JQSIsImFtciI6WyJwd2QiXSwiYXBwX2Rpc3BsYXluYW1lIjoiRm9jdXNYYW1hcmluRm9ybXNfMDhfMjAyMF9WMSIsImFwcGlkIjoiODNlZWQzMzEtOWRiZC00MmE3LWExZWItZWMxNjljYThmN2U3IiwiYXBwaWRhY3IiOiIwIiwiZmFtaWx5X25hbWUiOiJMZWFkZXIxIiwiZ2l2ZW5fbmFtZSI6IlRlYW0iLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiI4Ni4yOS4xNTIuMjEyIiwibmFtZSI6IlRlYW0gTGVhZGVyMSIsIm9pZCI6ImU5YzQzNzVhLTliMDEtNGU0NC1iMzk3LThhODQ4MWNiNTVmZSIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS0yNTY5NjA5NTUtMzg5NDcxODM4MS0xNDExODQ3MDQ0LTE3MDI4IiwicGxhdGYiOiIyIiwicHVpZCI6IjEwMDMyMDAwNDU5NzRGRkIiLCJyaCI6IjAuQUFBQVRITjBFT3UtSjBxcUFyREswdzVTcmpIVDdvTzluYWRDb2V2c0ZweW85LWRjQUgwLiIsInNjcCI6IkNhbGVuZGFycy5SZWFkIG9wZW5pZCBwcm9maWxlIFVzZXIuUmVhZCBlbWFpbCIsInNpZ25pbl9zdGF0ZSI6WyJrbXNpIl0sInN1YiI6IjdGaFhDNWUwTWNycjU2Y2hmd0hXYmN5TlZLRkVzN3p0d3liNnFwOVkyYTgiLCJ0ZW5hbnRfcmVnaW9uX3Njb3BlIjoiRVUiLCJ0aWQiOiIxMDc0NzM0Yy1iZWViLTRhMjctYWEwMi1iMGNhZDMwZTUyYWUiLCJ1bmlxdWVfbmFtZSI6InRsZWFkZXIxQHNjZC1sdGQuY29tIiwidXBuIjoidGxlYWRlcjFAc2NkLWx0ZC5jb20iLCJ1dGkiOiJjbk5fbkhwaUdrNlFQYzVwLVN4SUFBIiwidmVyIjoiMS4wIiwid2lkcyI6WyJiNzlmYmY0ZC0zZWY5LTQ2ODktODE0My03NmIxOTRlODU1MDkiXSwieG1zX3N0Ijp7InN1YiI6IllTZUJxQzBEWkFmUW1IV3pLY0hweklzeTBITzVZUmhNd1Y0ZVgxMU5wQmsifSwieG1zX3RjZHQiOjE0NjY1ODA4NTR9.WCIfKxcWpYNMg9MM9eTwbHIkQCsDegPXmwzbFKZyL52gAUCXo2VPYuntwgPBwUZh7800C11v5n8DzyoG3eHPCXbyam5-mpC_rwyzP9BC9CAPadPIzaak_vGD53080J06IHwdbsBE0utsXtpApIkDsQwwyFAzCpwcRzEyZtuOR2Qz8uDTwxahIHYzAtlo8zE_9s1yld-mj6Mv9trEBUww9FMqQrbN4dDm4abh9sJRu_y8az9qcqP90fgT9RjVJRrGM3ZjGRD5aGkp5hDiD37i8KHcJELqrnSHuG9WT8LAxU8jJVdyuhvERimDvaNEFAvMPSlqml8bQ-JTS2Rz0WWpBw",
            //    UserUniqueId = "e9c4375a-9b01-4e44-b397-8a8481cb55fe",
            //    AccessTokenTypeFromAzure = "Bearer",
            //    LoginDateTime = DateTime.Now,
            //    StatusResult = "Success"
            //}; 
            return new AuthenticatedUser { StatusResult = ex.Message };
        }
    }

    /// <summary>
    ///     Signs in the user and obtains an Access token for MS Graph
    /// </summary>
    /// <param name="scopes"></param>
    /// <returns> Access Token</returns>
    [Time]
    public static async Task<string> SignInUserAndGetTokenUsingMSAL(string[] scopes)
    {
        // It's good practice to not do work on the UI thread, so use ConfigureAwait(false) whenever possible.
        var accounts = await App.PCA.GetAccountsAsync().ConfigureAwait(false);

        var firstAccount = accounts.FirstOrDefault();

        try
        {
            authResult = await App.PCA.AcquireTokenSilent(scopes, firstAccount)
                .ExecuteAsync();
        }
        catch (MsalUiRequiredException ex)
        {
            // A MsalUiRequiredException happened on AcquireTokenSilentAsync. This indicates you need to call AcquireTokenAsync to acquire a token
            Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

            authResult = await App.PCA.AcquireTokenInteractive(scopes)
                .WithParentActivityOrWindow(App.ParentWindow)
                .ExecuteAsync()
                .ConfigureAwait(false);
        }

        return authResult.AccessToken;
    }

    /// <summary>
    ///     Sign in user using MSAL and obtain a token for MS Graph
    /// </summary>
    /// <returns>GraphServiceClient</returns>
    [Time]
    public static async Task<GraphServiceClient> SignInAndInitializeGraphServiceClient(string[] scopes)
    {
        var graphClient = new GraphServiceClient(MSGraphURL,
            new DelegateAuthenticationProvider(async requestMessage =>
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("bearer", await SignInUserAndGetTokenUsingMSAL(scopes));
            }));

        return await Task.FromResult(graphClient);
    }


    /// <summary>
    /// </summary>
    /// <param name="userUniqueId"></param>
    /// <returns></returns>
    [Time]
    public async Task SignoutUser()
    {
        var accounts = await App.PCA.GetAccountsAsync().ConfigureAwait(false);

        var firstAccount = accounts.FirstOrDefault();

        try
        {
            await App.PCA.RemoveAsync(firstAccount).ConfigureAwait(false);

            //SuspensionManager.SessionState["auth"] = null;
        }
        catch (MsalException ex)
        {
            //ResultText.Text = $"Error signing-out user: {ex.Message}";
        }
    }
}