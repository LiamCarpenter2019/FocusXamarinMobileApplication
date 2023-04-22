namespace FocusXamarinMobileApplication.Views;

public partial class AddChamberPage : ContentPage
{
    private readonly AddChamberPageViewModel _vm;

    public AddChamberPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.AddChamberPageViewModel;

        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoad.Execute(null);
    }
}