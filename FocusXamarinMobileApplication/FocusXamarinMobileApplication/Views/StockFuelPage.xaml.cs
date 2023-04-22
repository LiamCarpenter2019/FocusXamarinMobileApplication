using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class StockFuelPage : ContentPage, IFormsPage
{
    public StockFuelPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.StockFuelPageViewModel;
        BindingContext = _vm;
    }

    public StockFuelPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }
}