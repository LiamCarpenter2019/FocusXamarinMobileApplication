#region

using Map = Xamarin.Forms.Maps.Map;

#endregion

namespace FocusXamarinMobileApplication.Helpers;

public class CustomMap : Map
{
    public List<CustomPin> CustomPins { get; set; }
}