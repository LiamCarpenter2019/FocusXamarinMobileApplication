#region

using System;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using SignaturePad.Forms;
using Xamarin.Forms;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class SignaturePage : ContentPage, IFormsPage
{
    public readonly SignaturePageViewModel _vm;

    public SignaturePage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.SignaturePageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        // throw new NotImplementedException();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.PageLoaded.Execute(null);
        _vm.IsLoading = false;
        // listView.ItemsSource = _vm.FilteredQuestionCollection;
    }

    private async void UserSelectedCommand(object sender, SelectedItemChangedEventArgs e)
    {
        _vm.Signature = await AuthSignatureInPad.GetImageStreamAsync(SignatureImageFormat.Jpeg);

        _vm.UserSelected.Execute(null);
    }

    private async void SaveCommand(object sender, EventArgs e)
    {
        _vm.Signature = await AuthSignatureInPad.GetImageStreamAsync(SignatureImageFormat.Jpeg);

        _vm.Save.Execute(null);
    }
}