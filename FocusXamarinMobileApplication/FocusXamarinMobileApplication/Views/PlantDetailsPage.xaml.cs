namespace FocusXamarinForms20082020V1.Views;

public partial class PlantDetailsPage : ContentPage, IFormsPage
{
    private readonly PlantDetailsPageViewModel _vm;

    public PlantDetailsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.PlantDetailsPageViewModel;

        BindingContext = _vm;
    }

    public string CurrentPath { get; set; } = "/";

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    private void OpenDocumentCommand(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedDocument = e.SelectedItem as DocumentData2Display;


        _vm.OpenDocumentCommand.Execute(null);
    }

    private void FuelCommand(object sender, EventArgs e)
    {
        _vm.FuelCommand.Execute(null);
    }
}