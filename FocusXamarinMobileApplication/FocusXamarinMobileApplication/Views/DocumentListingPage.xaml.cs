#region

using Device = Xamarin.Forms.Device;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class DocumentListingPage : ContentPage, IFormsPage
{
    private readonly DocumentListingPageViewModel _vm;

    public DocumentListingPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.DocumentListingPageViewModel;
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
        //throw new NotImplementedException();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
        //  _vm.UpdateDocsCommand.Execute(null);
    }

    private void DocumentSelectedCommand(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            _vm.SelectedDocument = e.SelectedItem as DocumentData2Display;

            NavigationalParameters.SelectedDocument = _vm.SelectedDocument;

            if (Device.RuntimePlatform == Device.iOS)
                _vm.Platform = "IOS";
            else if (Device.RuntimePlatform == Device.Android) _vm.Platform = "ANDROID";

            _vm.DocumentSelected.Execute(null);
        }
    }

    private void SaveBtn_Clicked(object sender, EventArgs e)
    {
        _vm.UpdateDocsCommand.Execute(null);
    }
}