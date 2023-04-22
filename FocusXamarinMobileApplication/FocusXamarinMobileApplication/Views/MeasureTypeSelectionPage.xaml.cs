using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class MeasureTypeSelectionPage : ContentPage, IFormsPage
{
    public MeasureTypeSelectionPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.MeasureTypePageViewModel;

        BindingContext = _vm;
    }

    public MeasureTypePageViewModel _vm { get; set; }

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