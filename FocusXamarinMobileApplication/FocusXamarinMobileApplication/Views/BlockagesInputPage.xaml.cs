namespace FocusXamarinMobileApplication.Views;

public partial class BlockagesInputPage : ContentPage, IFormsPage
{
    private readonly BlockageInputPageViewModel _vm;

    public BlockagesInputPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.BlockageInputPageViewModel;

        //  NavigationalParameters.ReturnPage = "";

        BindingContext = _vm;
    }


    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ShowSection4 = true;

        _vm.RefreshCommand.Execute(null);
    }
}