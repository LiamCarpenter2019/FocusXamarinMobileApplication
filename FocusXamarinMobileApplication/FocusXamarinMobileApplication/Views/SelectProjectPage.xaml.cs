using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class SelectProjectPage : ContentPage, IFormsPage
{
    private readonly ProjectsPageViewModel _vm;

    public SelectProjectPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ProjectsPageViewModel;

        BindingContext = _vm;

        projectListView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PageLoaded.Execute(null);
    }

    private void OnProjectSelected(object sender, SelectedItemChangedEventArgs args)
    {
        ((ProjectsPageViewModel)BindingContext).ProjectSelected.Execute(args.SelectedItemIndex);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}