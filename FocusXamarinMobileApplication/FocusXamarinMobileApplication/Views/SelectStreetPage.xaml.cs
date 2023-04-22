namespace FocusXamarinMobileApplication.Views;

public partial class SelectStreetPage : ContentPage, IFormsPage
{
    private readonly SelectStreetPageViewModel _vm;

    public SelectStreetPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.SelectStreetPageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public void RefreshPage()
    {
        //((SelectStreetPageViewModel)BindingContext).Refresh.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PageLoaded.Execute(null);
    }

    private void SelectedStreet(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            _vm.StreetSelected = e.SelectedItem as IGrouping<string, VMexpansionReleaseData>;
            _vm.SelectedStreet.Execute(null);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}