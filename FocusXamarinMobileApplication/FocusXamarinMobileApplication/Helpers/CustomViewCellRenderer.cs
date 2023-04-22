using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Helpers;

public class CustomViewCellRenderer : ContentPage
{
    public CustomViewCellRenderer()
    {
        Content = new StackLayout
        {
            Children =
            {
                new Label { Text = "Hello ContentPage" }
            }
        };
    }
}