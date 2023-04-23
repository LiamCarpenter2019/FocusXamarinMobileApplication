#region

#endregion

using SignaturePad.Forms;

namespace FocusXamarinMobileApplication.Views;

public partial class VisitorLogOutPage : ContentPage, IFormsPage
{
    public VisitorLogOutPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.VisitorLogOutPageViewModel;

        BindingContext = _vm;
    }

    public VisitorLogOutPageViewModel _vm { get; set; }

    public void RefreshPage()
    {
        //  throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.ScreenLoaded.Execute(null);
    }

    private async void SaveBtn_Clicked(object sender, EventArgs e)
    {
        _vm.Image = await VisitorSignatureOutPad.GetImageStreamAsync(SignatureImageFormat.Jpg);

        _vm.Submit.Execute(null);
    }

    private void ClearBtn_Clicked(object sender, EventArgs e)
    {
        VisitorSignatureOutPad.Clear();
    }
}