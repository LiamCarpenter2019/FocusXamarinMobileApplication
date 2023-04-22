namespace FocusXamarinMobileApplication.Views;

public partial class DailyChecksIssuePage : ContentPage, IFormsPage
{
    private readonly DailyChecksIssuePageViewModel _vm;

    public DailyChecksIssuePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.DailyChecksIssuePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }

    private void GoPhoto(object sender, EventArgs e)
    {
        try
        {
            var imageButton = sender as ImageButton;

            NavigationalParameters.SelectedQuestion = imageButton.CommandParameter as SurveyQuestion;

            _vm.Photo.Execute(null);

            OnAppearing();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }
}