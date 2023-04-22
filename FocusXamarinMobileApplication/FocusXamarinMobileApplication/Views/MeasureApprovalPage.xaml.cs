namespace FocusXamarinMobileApplication.Views;

public partial class MeasureApprovalPage : ContentPage, IFormsPage
{
    public MeasureApprovalPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = Microsoft.SharePoint.Client.App.ViewModelLocator.MeasureApprovalPageViewModel;
    }

    public void RefreshPage()
    {
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}