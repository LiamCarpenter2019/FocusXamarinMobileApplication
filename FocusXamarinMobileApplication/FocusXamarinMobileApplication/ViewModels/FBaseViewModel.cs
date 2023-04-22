#region

using Application = Xamarin.Forms.Application;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class FBaseViewModel : BaseViewModel
{
    public async Task<bool> NavigateTo(string page)
    {
        // NavigationalParameters.PassedInfo = page;
        App.ViewModelLocator.NavigationService.NavigateTo(page);
        //  NavigationalParameters.NavigationService.NavigateTo(page);
        return true;
    }

    [Time]
    public bool NavigateBack()
    {
        // NavigationalParameters.PassedInfo = page;
        //await NavigationalParameters.Navigation.PopModalAsync();
        App.ViewModelLocator.NavigationService.GoBack();
        return true;
    }


    //public async Task<bool> NavigateBack()
    //{
    //    NavigationalParameters.NavigationService.GoBack();
    //    return true;
    //}

    public async Task<bool> Alert(string message, string title, string cancel = "OK")
    {
        await Application.Current?.MainPage?.DisplayAlert(message, title, cancel);

        return true;
    }

    public async Task<bool> Alert(string message, string title, string accept, string cancel)
    {
        return await Application.Current?.MainPage?.DisplayAlert(title, message, accept, cancel);
    }
}