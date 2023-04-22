#region

using FileSystem = PCLStorage.FileSystem;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class ImageEditorPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly LocalStorage db;
    private readonly IFolder rootFolder;
    private Pictures _picService;

    public ImageEditorPageViewModel()
    {
        rootFolder = FileSystem.Current.LocalStorage;

        db = new LocalStorage();

        NavigationalParameters.SelectedPhoto.PicturePathOnTablet = Path.Combine(rootFolder.Path, "JobPictures",
            NavigationalParameters.SelectedPhoto.FileName);

        var imgSource = NavigationalParameters.SelectedPhoto.PicturePathOnTablet;

        if (imgSource != null) Image = ImageSource.FromFile(imgSource);
    }

    private ImageSource _image { get; set; }

    public ImageSource Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        NavigationalParameters.SelectedPhoto.PicturePathOnTablet = Path.Combine(rootFolder.Path, "JobPictures",
            NavigationalParameters.SelectedPhoto.FileName);

        var imgSource = NavigationalParameters.SelectedPhoto?.PicturePathOnTablet;

        if (imgSource != null)
        {
            Image = ImageSource.FromFile(imgSource);

            NavigationalParameters.SelectedPhoto.Image =
                await db.GetImageFromLocalstorage("JobPictures", NavigationalParameters.SelectedPhoto.FileName);
        }
    });

    public RelayCommand SaveImage => new(async () =>
    {
        var db = new LocalStorage();

        await db.StoreImagesLocallyAndUpdatePath("JobPictures", NavigationalParameters.SelectedPhoto.FileName,
            NavigationalParameters.SelectedPhoto.Image);


        if (NavigationalParameters.AppMode != NavigationalParameters.AppModes.PROJECTIMAGES)
        {
            if (NavigationalParameters.MapType == "new asset" ||
                NavigationalParameters.SelectedAsset.Status == "new asset")
                await NavigateTo(ViewModelLocator.SelectEndPointPage);
            else
                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
        }
        else
        {
            await NavigateTo(ViewModelLocator.ProjectImagesPage);
        }
    });


    public RelayCommand Back => new(async () =>
    {
        if (!NavigationalParameters.AppMode.ToString().ToLower().Contains("presite"))
            await NavigateTo(ViewModelLocator.ProjectImagesPage);
        else
            NavigateBack();
        //await NavigateTo(ViewModelLocator.survey);
    });
}