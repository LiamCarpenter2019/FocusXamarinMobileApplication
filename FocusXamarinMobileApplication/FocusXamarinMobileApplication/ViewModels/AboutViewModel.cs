#region

using System.Windows.Input;
using MethodTimer;
using Xamarin.Essentials;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class AboutViewModel : BaseViewModel
{
    [Time]
    public AboutViewModel()
    {
        Title = "About";
        OpenWebCommand = new RelayCommand(async () => await Browser.OpenAsync("https://xamarin.com"));
    }

    public ICommand OpenWebCommand { get; }
}