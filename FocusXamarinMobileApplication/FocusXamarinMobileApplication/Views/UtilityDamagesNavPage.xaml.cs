using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class UtilityDamagesNavPage : ContentPage, IFormsPage
{
    public UtilityDamagesNavPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.UtilityDamagesNavPageViewModel;
        BindingContext = _vm;
    }

    public UtilityDamagesNavPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
        ((UtilityDamagesNavPageViewModel)BindingContext).Refresh.Execute(null);
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PageLoaded.Execute(null);
    }
}