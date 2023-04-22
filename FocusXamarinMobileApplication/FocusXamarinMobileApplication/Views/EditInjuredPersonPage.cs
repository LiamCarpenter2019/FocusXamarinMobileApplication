//using StandardLibrary.Data;

#region

#endregion

namespace FocusXamarinForms20082020V1.Views;

public partial class EditInjuredPersonPage : ContentPage
{
    private readonly EditInjuredPersonPageViewModel _vm;
    private InjuredPerson injuredPerson;
    private RegisterUtilityDamage registerUtilityDamage;

    public EditInjuredPersonPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.InjuredPersonViewModel;
        BindingContext = _vm;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }
}