using System;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class PlantTransferListPage : ContentPage, IFormsPage
{
    private readonly PlantTransferListPageViewModel _vm;

    public PlantTransferListPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.PlantTransferListPageViewModel;
        BindingContext = _vm;


        outView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };

        inlistView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.IsLoading = true;
        _vm.ShowSection1 = true;
        _vm.ShowSection2 = true;
        _vm.ShowSection3 = true;
        _vm.ShowSection4 = true;

        if (NavigationalParameters.AuthDetail.DetailsCorrect())
            try
            {
                _vm.ShowSection1 = false;
                _vm.ShowSection2 = false;
                _vm.ShowSection3 = false;
                _vm.ShowSection4 = false;
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            finally
            {
                _vm.IsLoading = false;
                _vm.ShowSection1 = true;
                _vm.ShowSection2 = true;
                _vm.ShowSection3 = true;
                _vm.ShowSection4 = true;
            }

        _vm.PageLoaded.Execute(null);
    }


    private void TransferOutCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        NavigationalParameters.SelectetedPlantItem = button.CommandParameter as PlantTransferViewModel;
        _vm.TransferOutCommand.Execute(null);
        // OnAppearing();
    }

    private void TransferInCommand(object sender, EventArgs e)
    {
        var button = sender as Button;
        NavigationalParameters.SelectetedPlantItem = button.CommandParameter as PlantTransferViewModel;
        _vm.TransferInCommand.Execute(null);
        //  OnAppearing();
    }
}