using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class MeasureListPage : ContentPage, IFormsPage
{
    public MeasureListPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.MeasureListPageViewModel;

        BindingContext = _vm;
    }

    public MeasureListPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    public void ProjectWorkItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var si = e.SelectedItem as ProjectWorks;

            if (si != null && si.Header != "" && si.Header != "0")
            {
                _vm.SelectedProjectWork = si;
                _vm.AddMeasure.Execute("");
            }
        }
    }
}