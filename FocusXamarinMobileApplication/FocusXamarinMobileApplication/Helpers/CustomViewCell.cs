namespace FocusXamarinMobileApplication.Helpers;

public class CustomViewCell : ContentPage
{
    public CustomViewCell()
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