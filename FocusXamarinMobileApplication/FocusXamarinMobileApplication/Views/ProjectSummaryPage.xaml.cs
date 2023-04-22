using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class ProjectSummaryPage : ContentPage, IFormsPage
{
    private readonly ProjectSummaryPageViewModel _vm;

    public ProjectSummaryPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ProjectSummaryPageViewModel;

        _vm.ScreenLoaded.Execute(null);

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }
}