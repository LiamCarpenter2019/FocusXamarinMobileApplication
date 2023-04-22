namespace FocusXamarinMobileApplication.Views;

public partial class TeamOptionsPage : ContentPage, IFormsPage
{
    public TeamOptionsPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.TeamOptionsPageViewModel;

        BindingContext = _vm;
    }

    public TeamOptionsPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }
}