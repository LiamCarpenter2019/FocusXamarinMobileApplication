namespace FocusXamarinMobileApplication.Views;

public partial class PdfViewPage : ContentPage, IFormsPage
{
    private readonly DocumentViewPageViewModel _vm;

    public PdfViewPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.DocumentViewPageViewModel;
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