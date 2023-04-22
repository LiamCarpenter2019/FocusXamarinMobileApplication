using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class SelectPreSitePage : ContentPage, IFormsPage
{
    public SelectPreSitePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.SelectPreSitePageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    private SelectPreSitePageViewModel _vm { get; }


    public void RefreshPage()
    {
        ((SelectPreSitePageViewModel)BindingContext).Refresh.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Vm.RefreshPreSites();
    }

    private void SelectedPreSite(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedPreSite.Execute(e.SelectedItem);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}