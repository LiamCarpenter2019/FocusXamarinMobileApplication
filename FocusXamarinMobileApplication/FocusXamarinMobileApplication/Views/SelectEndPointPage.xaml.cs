namespace FocusXamarinForms20082020V1.Views;

public partial class SelectEndPointPage : ContentPage, IFormsPage
{
    private readonly SelectEndPointPageViewModel _vm;

    public SelectEndPointPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SelectEndPointPageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            if (e.Item == null) return;

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

    private void SelectedAsset(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            _vm.AssetSelected = e.SelectedItem as VMexpansionReleaseData;

            _vm.SelectedAsset.Execute(null);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }


    public void RemoveAsset(object sender, EventArgs e)
    {
        var button = sender as Button;

        _vm.RemoveAsset(button);
    }

    void SearchCommand(System.Object sender, System.EventArgs e)
    {
        _vm.SearchCommand.Execute(null);
    }
}