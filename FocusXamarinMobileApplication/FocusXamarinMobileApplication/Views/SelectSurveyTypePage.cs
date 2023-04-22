namespace FocusXamarinMobileApplication.Views;

public partial class SelectSurveyTypePage : ContentPage, IFormsPage
{
    private readonly SelectSurveyTypePageViewModel _vm;

    public SelectSurveyTypePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.SelectSurveyTypePageViewModel;
        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        ((SelectSurveyTypePageViewModel)BindingContext).Refresh.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoad.Execute(null);
    }
}