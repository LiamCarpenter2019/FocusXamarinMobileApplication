namespace FocusXamarinMobileApplication.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        BindingContext = _vm;
    }

    public ProfilePageViewModel _vm { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }
}