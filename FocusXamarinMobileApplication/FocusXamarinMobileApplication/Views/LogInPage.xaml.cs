namespace FocusXamarinForms20082020V1.Views;

public partial class LogInPage : ContentPage, IFormsPage
{
    public LoginPageViewModel _vm;

    public LogInPage()
    {
        NavigationPage.SetHasNavigationBar(this, false);

        InitializeComponent();

        // BindingContext = 
        _vm = App.ViewModelLocator.LoginPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        //throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute("init");
    }

    private void LogIn(object sender, EventArgs e)
    {
        _vm.OnSignIn.Execute(null);
    }

    private void LogOut(object sender, EventArgs e)
    {
        _vm.OnSignOut.Execute(null);
    }
}