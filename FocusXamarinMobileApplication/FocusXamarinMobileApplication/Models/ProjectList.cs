namespace FocusXamarinMobileApplication.Models;

public class ProjectList : ContentPage
{
    public ProjectList()
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