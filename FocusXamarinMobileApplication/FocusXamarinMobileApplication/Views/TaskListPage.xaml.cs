namespace FocusXamarinMobileApplication.Views;

public partial class TaskListPage : ContentPage
{
    private readonly TaskListPageViewModel _vm;

    public TaskListPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.TaskListPageViewModel;

        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);
    }

    public void taskListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.SelectedTaskItem = e.SelectedItem as TaskItem;

        _vm.NavigateToListItemView.Execute(null);
    }
}