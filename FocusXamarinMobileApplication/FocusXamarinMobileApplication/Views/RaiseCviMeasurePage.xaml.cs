using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class RaiseCviMeasurePage : ContentPage, IFormsPage
{
    private readonly RaiseCviMeasurePageViewModel _vm;

    public RaiseCviMeasurePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.RaiseCviMeasurePageViewModel;
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


    public void UserSelectedCommand(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedRateCommand.Execute(null);
    }
}