using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class TimesheetPage : ContentPage, IFormsPage
{
    public TimesheetsPageViewModel _vm;

    public TimesheetPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.TimesheetsPageViewModel;

        BindingContext = _vm;

        //  _vm.ScreenLoaded.Execute(null);
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