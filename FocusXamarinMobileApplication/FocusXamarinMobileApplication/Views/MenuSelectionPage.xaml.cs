namespace FocusXamarinMobileApplication.Views;

public partial class MenuSelectionPage : ContentPage, IFormsPage
{
    private readonly MenuSelectionPageViewModel _vm;

    public MenuSelectionPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.MenuSelectionPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoad.Execute(null);
    }
}