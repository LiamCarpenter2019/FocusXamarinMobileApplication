namespace FocusXamarinForms20082020V1.Views;

public partial class BlockageListPage : ContentPage, IFormsPage
{
    private readonly BlockagePageViewModel _vm;

    public BlockageListPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.BlockagePageViewModel;

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

    public void CloseBlockageCommand(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null) _vm.CloseBlockageCommand.Execute(null);
    }
}