namespace FocusXamarinMobileApplication.Views;

public partial class ProjectListPage : ContentPage, IFormsPage
{
    public ProjectListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.SelectProjectPageViewModel;

        BindingContext = _vm;

        projectList.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public SelectProjectPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
        ((SelectProjectPageViewModel)BindingContext).Refresh.Execute(null);
    }

    private void OnProjectSelected(object sender, SelectedItemChangedEventArgs args)
    {
        ((ProjectsPageViewModel)BindingContext).ProjectSelected.Execute(args.SelectedItemIndex);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    public void SelectedProject(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedProject.Execute(e.SelectedItem);
    }
}