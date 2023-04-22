#region

using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class TimesheetSelectionPage : ContentPage, IFormsPage
{
    private readonly TimesheetsSelectionPageViewModel _vm;

    public TimesheetSelectionPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.TimesheetsSelectionPageViewModel;

        BindingContext = _vm;

        //listView.ItemTapped += (sender, e) =>
        //{
        //    // don't do anything if we just de-selected the row.
        //    if (e.Item == null) return;

        //    // Deselect the item.
        //    if (sender is ListView lv) lv.SelectedItem = null;
        //};

        //_vm.ScreenLoaded.Execute(null);
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }


    private void OnListViewItemSelected(object sender, ItemTappedEventArgs e)
    {
        _vm.SelectedLabourFile = e.ItemData as Labour;

        _vm.OnListViewItemSelected.Execute(null);
    }
}