namespace FocusXamarinForms20082020V1.Views;

public partial class InvestigationDetailPage : ContentPage, IFormsPage
{
    private readonly InvestigationDetailPageViewModel _vm;

    public InvestigationDetailPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.InvestigationDetailPageViewModel;

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
}