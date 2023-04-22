namespace FocusXamarinMobileApplication.Views;

public partial class RatePage : ContentPage, IFormsPage
{
    public RatePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.DfePageViewModel;

        BindingContext = _vm;
    }

    public DfePageViewModel _vm { get; set; }


    public void RefreshPage()
    {
        ((DfePageViewModel)BindingContext).Refresh.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.RefreshRates();
    }

    private void SelectedRate(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedRate.Execute(e.SelectedItem);
    }
}