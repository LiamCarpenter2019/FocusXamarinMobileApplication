#region

using SignaturePad.Forms;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class VisitorLogPage : ContentPage, IFormsPage
{
    public VisitorLogPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.VisitorLogPageViewModel;

        BindingContext = _vm;
    }

    public VisitorLogPageViewModel _vm { get; set; }

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
        _vm.Image = await VisitorSignatureInPad.GetImageStreamAsync(SignatureImageFormat.Jpg);

        _vm.Submit.Execute(null);
    }

    private void ClearBtn_Clicked(object sender, EventArgs e)
    {
        VisitorSignatureInPad.Clear();
    }
}