using System;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class PlantTransferInPage : ContentPage, IFormsPage
{
    private readonly PlantTansferInPageViewModel _vm;

    public PlantTransferInPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.PlantTansferInPageViewModel;

        BindingContext = _vm;

        listView.ItemTapped += (sender, e) =>
        {
            // don't do anything if we just de-selected the row.
            if (e.Item == null) return;

            // Deselect the item.
            if (sender is ListView lv) lv.SelectedItem = null;
        };
    }

    public void RefreshPage()
    {
        // throw new System.NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (NavigationalParameters.AuthDetail.DetailsCorrect())
        {
            try
            {
                _vm.HideDisplay.Execute(null);

                _vm.SaveAfterAuth.Execute(null);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            NavigationalParameters.AuthDetail = new AuthorisationDetail();
        }
        else
        {
            _vm.RefreshAllCurrentChecks.Execute(null);
            _vm.ShowDisplay.Execute(null);
        }
    }

    private void GoYesNoNa(object sender, EventArgs args)
    {
        var button = sender as Button;

        _vm.GetCurrentAnswer(button);
    }
}