#region

using System.Text.RegularExpressions;
using Location = Xamarin.Essentials.Location;

#endregion

namespace FocusXamarinMobileApplication.Helpers;

public class SimpleStaticHelpers
{
    public static string ShowItemBasedOnPath(string currentPath, string completePath, string docTitle,
        string filename)
    {
        string[] splitPath = { "" };
        try
        {
            if (completePath == null) completePath = "\\";

            var cleanedPath = string.Empty;
            if (completePath.Contains("\\"))
                cleanedPath = completePath.Replace("\\", "/");
            else
                cleanedPath = completePath;

            if (currentPath == cleanedPath) return docTitle == null ? "D" + filename : "D" + docTitle;

            var currentPathlevel = currentPath.Split('/').Length - 1;

            splitPath = cleanedPath.Split('/');
            var newPath = splitPath[currentPathlevel];
            return "F" + newPath;
        }
        catch (IndexOutOfRangeException ex)
        {
            return "F" + splitPath[splitPath.Length - 1];
        }
        catch (Exception ex)
        {
            var n = ex.Message;
            //return DocTitle == null ? "D" + Filename : "D" + DocTitle;
        }

        return "";
    }

    public static string BuildNewPathOrReturnFilename(string currentPath, string completePath, string docTitle,
        string filename)
    {
        var cleanedPath = completePath.Replace("\\", "/");
        var currentPathlevel = currentPath.Split('/').Length - 1; //GetCurrentPathLevel(CurrentPath);
        try
        {
            var splitPath = cleanedPath.Split('/');
            var newPath = string.Empty;
            for (var i = 1; i <= currentPathlevel; i++) newPath += splitPath[i] + "/";

            return "F/" + newPath;
        }
        catch (Exception ex)
        {
            var n = ex.Message;
            return "D" + filename;
        }
    }

    public static string SortPath(string path)
    {
        var returnValue = "";
        try
        {
            var cleanedPath = path.Replace("\\", "/");

            if (cleanedPath != null && cleanedPath.Contains("/") && cleanedPath.Length > 2)
            {
                var splitItUp = cleanedPath.Split('/');
                foreach (var item in splitItUp)
                    if (item != null && item.Length > 1)
                        returnValue = returnValue + item + "/";
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            var v = ex.Message;
        }

        return returnValue.Substring(0, returnValue.Length - 1);
    }

    public static byte[] ImgToByteArray(Stream stream)
    {
        stream.Position = 0;
        var buffer = new byte[stream.Length];
        for (var totalBytesCopied = 0; totalBytesCopied < stream.Length;)
            totalBytesCopied += stream.Read(buffer, totalBytesCopied,
                Convert.ToInt32(stream.Length) - totalBytesCopied);

        return buffer;
    }

    public static string SortPlantDocPath(string path, string plantId)
    {
        var returnValue = new StringBuilder();

        var splitItUpArray = path.Split('\\');
        foreach (var item in splitItUpArray)
            if (item != null && item.Length > 1)
                returnValue.Append($"[{plantId}]{item}/");

        return returnValue.ToString();
    }

    public static async Task<Location> GetCurrentPosition(int timeoutInSeconds)
    {
        try
        {
            Location location = null;
            location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best,
                new TimeSpan(0, 0, 5)));

            if (location != null) return location;
        }
        catch (FeatureNotSupportedException)
        {
            return new Location
            {
                Latitude = NavigationalParameters.NewPosition.Latitude,
                Longitude = NavigationalParameters.NewPosition.Longitude,
                Accuracy = 0,
                Altitude = 0,
                Speed = 0
            };
        }
        catch (PermissionException)
        {
            return new Location
            {
                Latitude = NavigationalParameters.NewPosition.Latitude,
                Longitude = NavigationalParameters.NewPosition.Longitude,
                Accuracy = 0,
                Altitude = 0,
                Speed = 0
            };
        }
        catch (Exception)
        {
            return new Location
            {
                Latitude = NavigationalParameters.NewPosition.Latitude,
                Longitude = NavigationalParameters.NewPosition.Longitude,
                Accuracy = 0,
                Altitude = 0,
                Speed = 0
            };
        }

        //Position position = null;
        return null;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;
        try
        {
            var result = Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            return result;
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static ImageSource GetImage(string Name)
    {
        return ImageSource.FromStream(() =>
            Assembly.GetExecutingAssembly()
                .GetManifestResourceStream($"FocusXamarinMobileApplication.Images.{Name}.png"));
    }

    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}

public class SortList
{
    public string Text { get; set; }
    public int Value { get; set; }
}

public static class ListSort
{
    public static void OrderByNumber(List<string> stringList)
    {
        var sorted = new List<SortList>();

        //Move list of strings into list to sort
        foreach (var s in stringList)
        {
            var spl = s.Split(' ');
            var txt = "";
            int.TryParse(spl[0], out var spInt);
            for (var i = 1; i < spl.Length; i++) txt += $"{spl[i]} ";
            sorted.Add(new SortList
            {
                Text = txt,
                Value = spInt
            });
        }

        var sortlist = sorted.OrderBy(x => x.Value);
        var sortbytextlist = sortlist.OrderBy(t => t.Text);
    }

    public static List<VMexpansionReleaseData> OrderByNumber(List<VMexpansionReleaseData> endpoint)
    {
        var sortlist = endpoint.OrderBy(x =>
            int.TryParse(x.BuildingNumber, out var result) ? result : endpoint.Count + 1);
        var sortbytextlist = sortlist.OrderBy(t => t.StreetName);
        endpoint = sortbytextlist.ToList();

        return endpoint;
    }

   
}