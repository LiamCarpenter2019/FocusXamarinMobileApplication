namespace FocusXamarinForms20082020V1.Views;

public partial class ProvingPage : ContentPage, IFormsPage
{
    private readonly ProvingPageViewModel _vm;

    public ProvingPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ProvingPageViewModel;
        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ProvingPageLoad.Execute(null);
    }

    private void BlockedEvent(object sender, EventArgs e)
    {
        var button = sender as Button;

        _vm.BlockedEventAsync(button);
    }

    private void ClearEvent(object sender, EventArgs e)
    {
        var button = sender as Button;

        _vm.ClearEventAsync(button);
    }
}