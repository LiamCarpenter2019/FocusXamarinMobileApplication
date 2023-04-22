namespace FocusXamarinForms20082020V1.Views;

public partial class SiteInspectionResultPage : ContentPage, IFormsPage
{
    private readonly SiteInspectionResultPageViewModel _vm;

    public SiteInspectionResultPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SiteInspectionResultPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }
}