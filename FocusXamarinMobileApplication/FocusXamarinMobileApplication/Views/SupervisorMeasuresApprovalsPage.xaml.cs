using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class SupervisorMeasuresApprovalsPage : ContentPage, IFormsPage
{
    private readonly SupervisorMeasuresApprovalsPageViewModel _vm;

    public SupervisorMeasuresApprovalsPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.SupervisorMeasuresApprovalsPageViewModel;

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

    private void measureLisView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (sender != null)
        {
            _vm.SelectedWorkItems = e.SelectedItem as ClaimedFile;

            NavigationalParameters.SupervisorAction = NavigationalParameters.SupervisorMeasureAction.MODIFY;

            NavigationalParameters.ClaimFile = _vm.SelectedWorkItems;

            _vm.WorkItemSelected.Execute(null);
        }
    }

    private void supervisorListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (sender != null)
        {
            _vm.SelectedWorkItems = e.SelectedItem as ClaimedFile;

            NavigationalParameters.SupervisorAction =
                NavigationalParameters.SupervisorMeasureAction.MODIFY;

            NavigationalParameters.ClaimFile = _vm.SelectedSupervisorItems;

            _vm.SupervisorItemSelected.Execute(null);
        }
    }
}