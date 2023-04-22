namespace FocusXamarinForms20082020V1.Views;

public partial class FuelConsumptionPage : ContentPage, IFormsPage
{
    public FuelConsumptionPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.FuelConsumptionPageViewModel;
        BindingContext = _vm;
    }

    public FuelConsumptionPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }
}