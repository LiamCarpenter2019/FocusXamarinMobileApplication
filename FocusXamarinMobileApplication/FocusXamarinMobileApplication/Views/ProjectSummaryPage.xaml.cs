namespace FocusXamarinForms20082020V1.Views;

public partial class ProjectSummaryPage : ContentPage, IFormsPage
{
    private readonly ProjectSummaryPageViewModel _vm;

    public ProjectSummaryPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ProjectSummaryPageViewModel;

        _vm.ScreenLoaded.Execute(null);

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
    }
}