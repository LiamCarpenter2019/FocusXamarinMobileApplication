using System;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class AdditionalRisksPage : ContentPage, IFormsPage
{
    public AdditionalRisksPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.AdditionalRisksPageViewModel;

        BindingContext = _vm;
    }

    public AdditionalRisksPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    public void DeleteRecordCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        NavigationalParameters.CurrentCheckAnswer =
            button.CommandParameter as Checks4TabletResponses;


        _vm.DeleteRecordCommand.Execute(null);
    }

    public void InadequateControlCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        NavigationalParameters.CurrentCheckAnswer =
            button.CommandParameter as Checks4TabletResponses;


        _vm.InadequateControlCommand.Execute(null);
    }
}