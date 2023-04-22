namespace FocusXamarinMobileApplication.Views;

public partial class OrderBookItemPage : ContentPage, IFormsPage
{
    private readonly OrderBookItemPageViewModel _vm;

    public OrderBookItemPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.OrderBookItemPageViewModel;

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