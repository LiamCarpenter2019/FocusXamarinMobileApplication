namespace FocusXamarinMobileApplication.Models;

public interface IFormsPage
{
    /// <summary>
    ///     Allows the base Activity/Controller to call the refresh on resume/willAppear.
    ///     Due to forms OnAppearing not triggering with Android.
    /// </summary>
    void RefreshPage();
}