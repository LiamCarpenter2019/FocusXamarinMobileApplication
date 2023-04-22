namespace FocusXamarinMobileApplication.Views;

public partial class PlantIssuePage : ContentPage, IFormsPage
{
    private readonly PlantIssuesPageViewModel _vm;

    public PlantIssuePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.PlantIssuesPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        // throw new System.NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!string.IsNullOrEmpty(NavigationalParameters.AuthDetail.SignedBy))
        {
            try
            {
                _vm.HideDisplay.Execute(null);
                _vm.LocationText = NavigationalParameters.CurrentPlantIssue?.LocationText ?? "";

                _vm.SendToServer.Execute(null);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }
        else
        {
            _vm.PageLoaded.Execute(null);
            _vm.ShowDisplay.Execute(null);
        }
    }
}