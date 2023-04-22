namespace FocusXamarinForms20082020V1.Views;

public partial class InvestigateDamagePage : ContentPage, IFormsPage
{
    private readonly InvestigateDamagePageViewModel _vm;

    public InvestigateDamagePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.InvestigateDamagePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    private void InjuredSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.InjuredPersonSelected.Execute(e.SelectedItemIndex);
    }

    private void GangSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.GangSelected.Execute(e.SelectedItemIndex);
    }
}