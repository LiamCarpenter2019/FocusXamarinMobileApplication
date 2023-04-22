#region

#endregion

using Syncfusion.SfImageEditor.XForms;

namespace FocusXamarinMobileApplication.Views;

public partial class ImageEditorPage : ContentPage, IFormsPage
{
    public readonly ImageEditorPageViewModel _vm;

    public ImageEditorPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.ImageEditorPageViewModel;

        BindingContext = _vm;
    }

    public void RefreshPage()
    {
        throw new NotImplementedException();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.PageLoaded.Execute(null);
    }

    public void ImageSaving(object sender, ImageSavingEventArgs e)
    {
        e.Cancel = true;

        NavigationalParameters.SelectedPhoto?.SetImage(e.Stream);

        _vm.SaveImage.Execute("Save");

        NavigationalParameters.SelectedPhoto = null;
    }
}