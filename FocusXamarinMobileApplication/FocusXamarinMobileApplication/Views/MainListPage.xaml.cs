using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class MainListPage : ContentPage
{
    private readonly MainListPageViewModel _vm;

    public MainListPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.MainListPageViewModel;

        BindingContext = _vm;

        //_vm.ScreenLoaded.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.ScreenLoaded.Execute(null);
    }

    private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (sender != null)
            switch (sender)
            {
                case ListView lv:
                    _vm.SelectedListItem = lv;
                    NavigationalParameters.CurrentSelectedJob = null;
                    _vm.NavigateToListItemView.Execute(null);
                    // //NavigationalParameters.ReturnPage = Locator.MainListPageViewModelKey;
                    break;
            }
    }
}