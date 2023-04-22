#region

using System;
using System.IO;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.ViewModels;
using Syncfusion.SfImageEditor.XForms;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
using Image = Xamarin.Forms.Image;

#endregion

namespace FocusXamarinMobileApplication.Views;

public interface IImageEditorDependencyService
{
    void UploadFromCamera(ProjectImagesPage editor);

    void UploadFromGallery(ProjectImagesPage editor);

    byte[] GetBytes(string file);
}

public partial class ProjectImagesPage : ContentPage
{
    private bool isTakePhoto, isOpenGallery;

    public ProjectImagesPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _vm = App.ViewModelLocator.ProjectImagesPageViewModel;
        BindingContext = _vm;
    }

    public ProjectImagesPageViewModel _vm { get; set; }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        _vm.RefreshPictures.Execute("Refresh");
    }

    public void SwitchView(Stream file)
    {
        //NavigationalParameters.PictureToEdit = new Pictures4Tablet();
        NavigationalParameters.PictureToEdit.SetImage(file);
        _vm.EditImage.Execute(null);
    }

    public void SwitchView(string file)
    {
        Navigation.PopModalAsync();
        Navigation.PushModalAsync(new SfImageEditorPage(file));
    }

    private void TakePhoto_Clicked(object sender, EventArgs e)
    {
        DependencyService.Get<IImageEditorDependencyService>().UploadFromCamera(this);
    }

    private void BrowsePhoto_Clicked(object sender, EventArgs e)
    {
        DependencyService.Get<IImageEditorDependencyService>().UploadFromGallery(this);
    }


    private void TakeAPhotoTapped(object sender, EventArgs e)
    {
        if (!isTakePhoto)
            //  TakePhoto.Source = Vm.TakePicSelected;
            isTakePhoto = true;

        else
            //  TakePhoto.Source = Vm.TakePic;
            isTakePhoto = false;
        DependencyService.Get<IImageEditorDependencyService>().UploadFromCamera(this);
    }

    private void OpenGalleryTapped(object sender, EventArgs e)
    {
        if (!isOpenGallery)
            // OpenGallery.Source = Vm.ChooseImage;
            isOpenGallery = true;
        else
            // OpenGallery.Source = Vm.ChooseImageSelected;
            isOpenGallery = false;
        DependencyService.Get<IImageEditorDependencyService>().UploadFromGallery(this);
    }

    private void ImageTapped(object sender, EventArgs e)
    {
        LoadFromStream((sender as Image).Source);
    }

    private void LoadFromStream(ImageSource source)
    {
        if (Device.OS == TargetPlatform.iOS)
            Navigation.PushAsync(new SfImageEditorPage(source));
        else
            Navigation.PushModalAsync(new SfImageEditorPage(source));
    }

    public void PopUpCancelled()
    {
        // TakePhoto.Source = Vm.TakePic;
        // OpenGallery.Source = Vm.ChooseImageSelected;
        isOpenGallery = false;
        isTakePhoto = false;
    }
}

public class SfImageEditorPage : ContentPage
{
    public SfImageEditorPage(string file)
    {
        var mem = new MemoryStream(DependencyService.Get<IImageEditorDependencyService>().GetBytes(file));
        var editor = new SfImageEditor();
        editor.Source = ImageSource.FromFile(file);
        Content = editor;
    }

    public SfImageEditorPage(Stream file)
    {
        var editor = new SfImageEditor();
        editor.Source = ImageSource.FromStream(() => file);
        Content = editor;
    }

    public SfImageEditorPage(ImageSource imagesource)
    {
        var editor = new SfImageEditor();
        editor.Source = imagesource;
        Content = editor;
    }
}