using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class InvestigationDetail2Page : ContentPage, IFormsPage
{
    private readonly InvestigationDetail2PageViewModel _vm;

    public InvestigationDetail2Page()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);  

        _vm = App.ViewModelLocator.InvestigationDetail2PageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }
}