namespace FocusXamarinMobileApplication.Views;

public partial class DesignMapPage : ContentPage
{
    private readonly DocumentPageViewModel _vm;

    public DesignMapPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.DesignMapPageViewModel;

        BindingContext = _vm;

        PdfView.Source = _vm.DocumentUrl;
    }

    public string DocumentUrl { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }
}