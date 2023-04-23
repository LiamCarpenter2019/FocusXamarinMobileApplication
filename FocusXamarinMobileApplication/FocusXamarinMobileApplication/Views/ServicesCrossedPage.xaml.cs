namespace FocusXamarinMobileApplication.Views;

public partial class ServicesCrossedPage : ContentPage
{
    private readonly ServicesCrossedPageViewModel _vm;

    public ServicesCrossedPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ServicesCrossedPageViewModel;

        BindingContext = _vm;

        _vm.ScreenLoadedCommand4CrossedUtilities.Execute(null);
    }
}