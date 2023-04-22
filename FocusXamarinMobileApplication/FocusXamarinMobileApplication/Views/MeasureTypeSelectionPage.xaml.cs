namespace FocusXamarinMobileApplication.Views;

public partial class MeasureTypeSelectionPage : ContentPage, IFormsPage
{
    public MeasureTypeSelectionPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.MeasureTypePageViewModel;

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