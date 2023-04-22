using System;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class AddWitnessPage : ContentPage, IFormsPage
{
    // private readonly AddWitnessViewModel _vm;


    public AddWitnessPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.AddWitnessPageViewModel;

        BindingContext = _vm;
        //_vm = new LoginPageViewModel(Navigation);
        // _vm = new AddWitnessViewModel();

        // BindingContext = _vm;
    }

    public AddWitnessPageViewModel _vm { get; }

    public void RefreshPage()
    {
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    private void btnCancel_Clicked(object sender, EventArgs args)
    {
        // _vm.GoBack.Execute(null);
    }

    private void AddWitnessCommand(object sender, EventArgs e)
    {
        // _vm.AddWitnessCommand.Execute(null);
    }
}