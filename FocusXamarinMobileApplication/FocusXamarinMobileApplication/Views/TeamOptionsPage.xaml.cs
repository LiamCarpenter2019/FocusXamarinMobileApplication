using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class TeamOptionsPage : ContentPage, IFormsPage
{
    public TeamOptionsPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.TeamOptionsPageViewModel;

        BindingContext = _vm;
    }

    public TeamOptionsPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }
}