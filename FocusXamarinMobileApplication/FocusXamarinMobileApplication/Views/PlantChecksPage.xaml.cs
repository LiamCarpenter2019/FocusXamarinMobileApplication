namespace FocusXamarinMobileApplication.Views;

public partial class PlantChecksPage : ContentPage, IFormsPage
{
    public PlantChecksPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.PlantChecksPageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public PlantChecksPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        NavigationalParameters.PlantView = NavigationalParameters.PlantViews.PLANTCHECKS;
        _vm.PageLoaded.Execute(null);
    }

    private void GoYesNoNa(object sender, EventArgs args)
    {
        var button = sender as Button;
        _vm.GetCurrentAnswer(button);
    }

    public void SelectedUserCommand(object sender, EventArgs e)
    {
        if (e != null) _vm.UpdateSelectedUserCommand.Execute(null);
    }
}