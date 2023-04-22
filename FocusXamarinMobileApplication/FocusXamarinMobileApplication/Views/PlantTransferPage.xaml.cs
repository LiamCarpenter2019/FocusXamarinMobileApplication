namespace FocusXamarinForms20082020V1.Views;

public partial class PlantTransferPage : ContentPage, IFormsPage
{
    private readonly PlantTansferPageViewModel _vm;

    public PlantTransferPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.PlantTransferPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        // throw new System.NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (NavigationalParameters.AuthDetail.DetailsCorrect())
        {
            try
            {
                _vm.HideDisplay.Execute(null);

                _vm.SaveAfterAuth.Execute(null);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }
        else
        {
            _vm.ScreenLoaded.Execute(null);
            _vm.ShowDisplay.Execute(null);
        }
    }

    private void SelectedUserCommand(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedUserCommand.Execute(null);
    }
}