using Xamarin.Forms.Maps;

namespace FocusXamarinMobileApplication.Helpers;

public class CustomPin : Pin
{
    public string Url { get; set; }
    public string ImageName { get; set; } = "monkey.png";
}