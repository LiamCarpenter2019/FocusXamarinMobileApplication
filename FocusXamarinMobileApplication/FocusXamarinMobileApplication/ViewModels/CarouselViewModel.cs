using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using MethodTimer;

namespace FocusXamarinMobileApplication.ViewModels;

public class CarouselViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private Pictures4Tablet _currentPic;

    //  ObservableCollection<FileImageSource> imageSources = new ObservableCollection<FileImageSource>();
    private ObservableCollection<Pictures4Tablet> _pics4ThisJob;

    [Time]
    public CarouselViewModel()
    {
        pictureService = new Pictures();
        LoadPostsAsync();
    }

    // public List<Pictures4Tablet> Pics4ThisJob { get; set; }
    public Pictures pictureService { get; set; }

    public ObservableCollection<Pictures4Tablet> Pics4ThisJob
    {
        get => _pics4ThisJob;
        set
        {
            _pics4ThisJob = value;
            OnPropertyChanged();
        }
    }

    public Pictures4Tablet CurrentPic
    {
        get => _currentPic;
        set
        {
            _currentPic = value;
            OnPropertyChanged();
        }
    }

    [Time]
    private async Task LoadPostsAsync()
    {
        var ls = new LocalStorage();
        var rootFolder = await ls.GetTabletDocsPath();
        Pics4ThisJob = new ObservableCollection<Pictures4Tablet>(pictureService.GetJobPictures(
            NavigationalParameters.CurrentSelectedJob.QuoteNumber, NavigationalParameters.LoggedInUser.FocusId));

        foreach (var pic in Pics4ThisJob)
            pic.PicturePath = Path.Combine(rootFolder.Path, "JobPictures", pic.FileName);

        CurrentPic = Pics4ThisJob[0];
    }
}