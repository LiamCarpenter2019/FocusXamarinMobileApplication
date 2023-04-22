namespace FocusXamarinMobileApplication.Views;

public partial class PermitPage : ContentPage, IFormsPage
{
    private readonly PermitPageViewModel _vm;

    public PermitPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        BindingContext = Microsoft.SharePoint.Client.App.ViewModelLocator.PermitPageViewModel;
    }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }
}