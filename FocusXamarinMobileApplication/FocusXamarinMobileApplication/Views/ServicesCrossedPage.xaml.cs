namespace FocusXamarinMobileApplication.Views;

public partial class ServicesCrossedPage : ContentPage
{
    private readonly ServicesCrossedPageViewModel _vm;

    public ServicesCrossedPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.ServicesCrossedPageViewModel;

        BindingContext = _vm;

        _vm.ScreenLoadedCommand4CrossedUtilities.Execute(null);
    }
}