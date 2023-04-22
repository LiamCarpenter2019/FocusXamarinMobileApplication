#region

using System;
using FocusXamarinMobileApplication.Helpers;
using Microsoft.AppCenter.Analytics;
using UIKit;

#endregion
namespace FocusXamarinMobileApplication.iOS;

public class Application
{
    [Obsolete("Obsolete")]
    private static void Main(string[] args)
    {
        try
        {
            UIApplication.Main(args, null, "AppDelegate");
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            Analytics.TrackEvent($"Error For: {NavigationalParameters.LoggedInUser?.FullName} with error {ex}");

            Console.WriteLine(error);
        }
    }
}