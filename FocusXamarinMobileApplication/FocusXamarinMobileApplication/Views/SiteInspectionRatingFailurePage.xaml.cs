namespace FocusXamarinMobileApplication.Views;

public partial class SiteInspectionRatingFailurePage : ContentPage, IFormsPage
{
    private readonly SiteInspectionRatingFailurePageViewModel _vm;

    public SiteInspectionRatingFailurePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SiteInspectionRatingFailurePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        //  throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }
}