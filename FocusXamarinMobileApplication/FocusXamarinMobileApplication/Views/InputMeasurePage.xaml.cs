using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class InputMeasurePage : ContentPage, IFormsPage
{
    private readonly InputMeasurePageViewModel _vm;

    public InputMeasurePage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        NavigationalParameters.ReturnPage = "";

        _vm = App.ViewModelLocator.InputMeasurePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        //throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    private void WorksPicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var item = sender as ProjectWorks;

        if (item != null) _vm.SelectedMeasure.Execute("");
    }


    private void DrumFilter_TextChanged(object sender, TextChangedEventArgs e)
    {
        _vm.FilterDrumList.Execute(null);
    }
}