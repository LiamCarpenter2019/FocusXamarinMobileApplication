namespace FocusXamarinMobileApplication.Views;

public partial class SupervisorDiaryPage : ContentPage, IFormsPage
{
    private readonly SupervisorDiaryPageViewModel _vm;

    public SupervisorDiaryPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.SupervisorDiaryPageViewModel;

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
}