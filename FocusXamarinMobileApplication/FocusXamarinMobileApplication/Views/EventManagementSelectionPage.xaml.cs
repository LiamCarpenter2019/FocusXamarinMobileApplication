namespace FocusXamarinMobileApplication.Views;

public partial class EventManagementSelectionPage : ContentPage, IFormsPage
{
    private readonly EventManagementSelectionPageViewModel _vm;

    public EventManagementSelectionPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.EventManagementSelectionPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }
}