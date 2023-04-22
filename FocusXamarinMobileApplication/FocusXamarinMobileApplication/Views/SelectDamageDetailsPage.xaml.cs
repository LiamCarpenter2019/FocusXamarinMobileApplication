namespace FocusXamarinMobileApplication.Views;

public partial class SelectDamageDetailsPage : ContentPage, IFormsPage
{
    private readonly SelectDamageDetailsPageViewModel _vm;

    public SelectDamageDetailsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.SelectDamageDetailsPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    public void DamageItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (sender is ListView lv)
            if (lv.SelectedItem != null)
                _vm.NavToDamage.Execute(null);
    }
}