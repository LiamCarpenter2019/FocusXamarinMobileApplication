namespace FocusXamarinMobileApplication.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SettingsPageViewModel;

        BindingContext = _vm;
    }

    public SettingsPageViewModel _vm { get; set; }

    protected override void OnAppearing()
    {
        _vm.ScreenLoaded.Execute("");
    }
}