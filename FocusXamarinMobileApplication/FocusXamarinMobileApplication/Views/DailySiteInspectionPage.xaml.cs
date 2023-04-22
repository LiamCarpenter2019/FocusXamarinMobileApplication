using System;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class DailySiteInspectionPage : ContentPage
{
    private readonly DailySiteInspectionPageViewModel _vm;

    public DailySiteInspectionPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.DailySiteInspectionPageViewModel;

        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.ScreenLoaded.Execute(null);

        if (NavigationalParameters.MultSignatures != null
            && NavigationalParameters.MultSignatures.All(x => !string.IsNullOrEmpty(x.Signature))
            && NavigationalParameters.MultSignatures.Count() > 0)
        {
            try
            {
                _vm.IsLoading = true;
                _vm.ShowSection1 = false;
                _vm.ShowSection2 = false;
                _vm.ShowSection3 = false;

                _vm.UploadAllChecksCommand.Execute(null);
            }
            catch (Exception ex)
            {
                var exx = ex.ToString();
            }
        }
        else
        {
            _vm.IsLoading = false;
            _vm.ShowSection1 = true;
            _vm.ShowSection2 = true;
            _vm.ShowSection3 = true;
        }
    }

    private void GoYesNoNa(object sender, EventArgs args)
    {
        var button = sender as Button;

        _vm.GetCurrentAnswer(button);
    }
}