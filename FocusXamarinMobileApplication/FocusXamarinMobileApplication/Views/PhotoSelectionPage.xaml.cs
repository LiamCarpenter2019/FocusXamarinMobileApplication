namespace FocusXamarinMobileApplication.Views;

public partial class PhotoSelectionPage : ContentPage, IFormsPage
{
    private readonly PhotoSelectionPageViewModel _vm;

    public PhotoSelectionPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.PhotoSelectionPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        //throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }

    public void

        UpdateReason(object sender, EventArgs args)
    {
        var button = sender as Button;

        switch (button.Text)
        {
            case "Route":
                _vm.Route = Color.Green;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "Obstruction":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.Green;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "Condition":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.Green;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "Incident / Strike":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.Green;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "PIA":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.Green;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "H S E":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.Green;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "General":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.Green;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "Site Clear":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.Green;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;

            case "Trial Hole":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.Green;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "Trench Depth":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.Green;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
            case "Back Fill Depth":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.Green;
                _vm.Chamber = Color.LightGray;
                break;
            case "Chamber/Pole":
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.Green;
                break;
            default:
                _vm.Route = Color.LightGray;
                _vm.Obstruction = Color.LightGray;
                _vm.Condition = Color.LightGray;
                _vm.Incident = Color.LightGray;
                _vm.PIA = Color.LightGray;
                _vm.Hse = Color.LightGray;
                _vm.General = Color.LightGray;
                _vm.SiteClear = Color.LightGray;
                _vm.TrialHole = Color.LightGray;
                _vm.TrenchDepth = Color.LightGray;
                _vm.BackFillDepth = Color.LightGray;
                _vm.Chamber = Color.LightGray;
                break;
        }

        if (NavigationalParameters.SelectedPhoto != null && !string.IsNullOrEmpty(button.Text))
            NavigationalParameters.SelectedPhoto.PictureReason = _vm.Reason = button.Text;
    }
}