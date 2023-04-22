#region

using View = Xamarin.Forms.View;

#endregion

namespace FocusXamarinMobileApplication.Views;

public partial class CameraViewPage : ContentPage, IFormsPage
{
    private readonly PhotoPageViewModel _vm;

    public CameraViewPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.PhotoPageViewModel;
        BindingContext = _vm;
        PhotoList = new List<Pictures4Tablet>();
        //AddImageButton.WidthRequest = ButtonWidth;
        //AddImageButton.HeightRequest = ButtonHeight;
    }

    public int ImageWidth { get; set; } = 200;

    public int ImageHeight { get; set; } = 200;
    //public int ButtonHeight { get; set; } = 100;
    //public int ButtonWidth { get; set; } = 100;

    public List<Pictures4Tablet> PhotoList { get; set; }

    public void RefreshPage()
    {
        Refresh();
    }
    /*public void AddImage_Clicked(object sender, EventArgs args)
    {
        //Photo p = new Photo();
        Vm.AddImage.Execute(null);
        //await Navigation.PushAsync(new PhotoSelection(p, this));
    }*/

    public async void Refresh()
    {
        foreach (var p in PhotoList)
        {
            var b = ImageStack.Children[PhotoList.IndexOf(p)] as ImageButton;
            //b.Image = p.FilePath;

            var ls = new LocalStorage();
            var rootFolder = await ls.GetTabletDocsPath();
            if (await ls.DoesJobPictureExistOnTablet("JobPictures", p.PicturePath.Split('/').Last()))
            {
                var img = await ls.GetImageFromLocalstorage("JobPictures", p.PicturePath.Split('/').Last());
                //var pictureUrl = Path.Combine(rootFolder.Path, "JobPictures", p.FilePath.Split('/').Last());
                //var stream = new MemoryStream(img);
                b.Source = ImageSource.FromStream(() => new MemoryStream(img));
            }
            else
            {
                b.Source = SimpleStaticHelpers.GetImage("HarmonixIcon");
            }
        }
    }

    public void Image_Clicked(object sender, EventArgs args)
    {
        //Navigation.PushAsync(new PhotoSelection(PhotoList[ImageStack.Children.IndexOf((View)sender)], this));
        _vm.SelectImage.Execute(PhotoList[ImageStack.Children.IndexOf((View)sender)]);
    }

    public void RemoveImage(Pictures4Tablet photo)
    {
        ImageStack.Children.RemoveAt(PhotoList.IndexOf(photo));
        PhotoList.Remove(photo);
    }

    public async void AddImage(Pictures4Tablet photo)
    {
        PhotoList.Add(photo);
        var b = new ImageButton
        {
            WidthRequest = ImageWidth,
            HeightRequest = ImageHeight,
            BackgroundColor = Color.Transparent
        };
        b.Clicked += Image_Clicked;
        ImageStack.Children.Add(b);

        //var ls = new LocalStorage();
        //var r = new Random();

        //await ls.Save2AzureBlobByByteArray(photo.Image, $"{r.Next(1000,9999)}azuredamagetest.png", "focusspdata/jobpictures"); //TEMP TEST
    }
}