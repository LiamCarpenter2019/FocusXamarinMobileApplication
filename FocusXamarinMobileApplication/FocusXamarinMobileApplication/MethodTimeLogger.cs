using System;
using System.Reflection;
using FocusXamarinMobileApplication.Helpers;
using Microsoft.AppCenter.Analytics;

namespace FocusXamarinMobileApplication;

public static class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, long milliseconds, string message)
    {
        try
        {
            Analytics.TrackEvent(
                $"The method {methodBase} has has taken {milliseconds} mls to process for: {NavigationalParameters.LoggedInUser?.FullName}");

            Console.WriteLine($"{milliseconds} mls---(method)--->{methodBase}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in MethodTimelogger");
        }
    }
}