using System.ComponentModel;
using FocusXamarinMobileApplication.Services;
using GalaSoft.MvvmLight.Views;

namespace FocusXamarinMobileApplication.ViewModels
{
    public class MainPageViewModel : FBaseViewModel, INotifyPropertyChanged
    {
        public AzureAuth azureAuth { get; set; }

        public MainPageViewModel()
        {
           
        }
    }
}
