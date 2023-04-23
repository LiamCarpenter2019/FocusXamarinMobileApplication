namespace FocusXamarinMobileApplication.Views;

public partial class SupervisorProjectPage : ContentPage
{
    private readonly SupervisorProjectPageViewModel _vm;

    public SupervisorProjectPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SupervisorProjectPageViewModel;

        BindingContext = _vm;

        //  _vm.ScreenLoaded.Execute(null);
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.ScreenLoaded.Execute(null);
    }

    public void SupervisorProjectListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedProjectItem =
            e.SelectedItem as ProjectViewModel;
        _vm.NavigateToListItemView.Execute(null);
    }
}