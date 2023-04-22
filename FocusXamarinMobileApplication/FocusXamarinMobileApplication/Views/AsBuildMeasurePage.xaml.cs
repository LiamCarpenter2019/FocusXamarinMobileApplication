namespace FocusXamarinMobileApplication.Views;

public partial class AsBuildMeasurePage : ContentPage, IFormsPage
{
    private readonly AsBuildMeasurePageViewModel _vm;

    public AsBuildMeasurePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.AsBuildMeasurePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }

    public void question_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
    }
}