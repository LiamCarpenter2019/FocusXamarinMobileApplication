namespace FocusXamarinMobileApplication.Views;

public partial class SelectInvestigationPage : ContentPage, IFormsPage
{
    private readonly SelectInvestigationPageViewModel _vm;

    public SelectInvestigationPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.SelectInvestigationPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
        //listSelectInvestigation.SelectedItem = null;
    }

    public void InvestigationItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (sender is ListView lv)
            if (lv.SelectedItem != null)
                _vm.NavToInvestigation.Execute(null);
    }
}