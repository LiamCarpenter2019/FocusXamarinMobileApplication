#region

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class GangSelectPage : ContentPage
{
    private readonly GangSelectPageViewModel _vm;

    public GangSelectPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.GangSelectPageViewModel;

        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }


    public void gangListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            _vm.SelectedGangItem = e.SelectedItem as GangViewModel;

            _vm.NavigateToListItemView.Execute(null);
        }
    }
}