using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.ViewModels;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class FormsCommentPage : ContentPage, IFormsPage
{
    private readonly FormsCommentPageViewModel _vm;

    public FormsCommentPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.FormsCommentPageViewModel;

        BindingContext = _vm;
    }


    public void RefreshPage()
    {
        ((FormsCommentPageViewModel)BindingContext).Refresh.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.Refresh.Execute(null);
    }
}