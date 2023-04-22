namespace FocusXamarinMobileApplication.Views;

public partial class MyPlantScreenPage : ContentPage, IFormsPage
{
    public MyPlantScreenPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.MyPlantScreenPageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;
            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public MyPlantScreenPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    private async void ItemInfoCommand(object sender, EventArgs e)
    {
        OnAppearing();

        var button = sender as Button;

        await _vm.ItemInfoCommand(button);
    }

    private async void LogIssueCommand(object sender, EventArgs e)
    {
        OnAppearing();

        var button = sender as Button;

        await _vm.LogIssueCommand(button);
    }

    private async void PlantChecksCommand(object sender, EventArgs e)
    {
        OnAppearing();

        var button = sender as Button;
        NavigationalParameters.SelectedUser = null;
        await _vm.PlantChecksCommand(button);
    }

    private async void TransferOutCommand(object sender, EventArgs e)
    {
        OnAppearing();

        var button = sender as Button;

        await _vm.TransferOutCommand(button);
    }
}