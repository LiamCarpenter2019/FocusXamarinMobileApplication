namespace FocusXamarinForms20082020V1.Views;

public partial class CalibrationPage : ContentPage, IFormsPage
{
    private readonly CalibrationPageViewModel _vm;

    public CalibrationPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.CalibrationPageViewModel;

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

    public void SearchCommand(object sender, TextChangedEventArgs e)
    {
    }
}