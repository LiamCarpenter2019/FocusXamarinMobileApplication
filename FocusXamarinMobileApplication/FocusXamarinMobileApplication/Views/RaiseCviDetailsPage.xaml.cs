namespace FocusXamarinForms20082020V1.Views;

public partial class RaiseCviDetailsPage : ContentPage, IFormsPage
{
    private readonly RaiseCviDetailsPageViewModel _vm;

    public RaiseCviDetailsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.RaiseCviDetailsPageViewModel;
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

    private void SwitchA_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        _vm.UpdateActions("SwitchA");
    }

    private void SwitchB_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        _vm.UpdateActions("SwitchB");
    }
}