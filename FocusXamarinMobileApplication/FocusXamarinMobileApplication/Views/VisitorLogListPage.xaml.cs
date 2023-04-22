namespace FocusXamarinMobileApplication.Views;

public partial class VisitorLogListPage : ContentPage, IFormsPage
{
    public VisitorLogListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.VisitorLogListPageViewModel;


        BindingContext = _vm;
    }

    public VisitorLogListPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PageLoaded.Execute(null);
    }

    public async void logOutCommand(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            NavigationalParameters.SelectedVisitor = e.SelectedItem as VisitorLog;

            _vm.LogOutCommand.Execute(null);

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}