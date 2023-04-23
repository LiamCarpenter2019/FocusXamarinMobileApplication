namespace FocusXamarinMobileApplication.Views;

/// <summary>
/// The select survey type page.
/// </summary>
public partial class SelectSurveyTypePage : ContentPage, IFormsPage
{
    /// <summary>
    /// The vm.
    /// </summary>
    private readonly SelectSurveyTypePageViewModel _vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectSurveyTypePage"/> class.
    /// </summary>
    public SelectSurveyTypePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SelectSurveyTypePageViewModel;
        BindingContext = _vm;
    }

    /// <summary>
    /// Refreshes the page.
    /// </summary>
    public void RefreshPage()
    {
        ((SelectSurveyTypePageViewModel)BindingContext).Refresh.Execute(null);
    }

    /// <summary>
    /// On appearing.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoad.Execute(null);
    }
}