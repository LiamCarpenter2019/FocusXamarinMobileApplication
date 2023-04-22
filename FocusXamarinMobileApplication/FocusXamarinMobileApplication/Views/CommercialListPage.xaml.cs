using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class CommercialListPage : ContentPage, IFormsPage
{
    private readonly CommercialListPageViewModel _vm;

    public CommercialListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.CommercialListPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }
}