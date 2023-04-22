using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class PermitPage : ContentPage, IFormsPage
{
    private readonly PermitPageViewModel _vm;

    public PermitPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        BindingContext = App.ViewModelLocator.PermitPageViewModel;
    }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }
}