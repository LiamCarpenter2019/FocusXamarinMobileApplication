namespace FocusXamarinMobileApplication.Views;

public partial class CommercialListPage : ContentPage, IFormsPage
{
    private readonly CommercialListPageViewModel _vm;

    public CommercialListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.CommercialListPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }
}