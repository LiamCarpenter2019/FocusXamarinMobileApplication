#region

using FileSystem = PCLStorage.FileSystem;
using Image = Xamarin.Forms.Image;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class ProfilePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public ProfilePageViewModel()
    {
        _assigmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        _pictureService = new Pictures();
        _ls = new LocalStorage();
    }

    public Assignments _assigmentService { get; set; }

    public Jobs _jobService { get; set; }

    public Users _userService { get; set; }

    public Pictures _pictureService { get; set; }

    public LocalStorage _ls { get; set; }

    public string _title { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public List<UserGroup> _roles { get; private set; }

    public List<UserGroup> Roles
    {
        get => _roles;
        set
        {
            _roles = value;
            OnPropertyChanged();
        }
    }


    public string _projectDate { get; set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }


    public bool _showSection1 { get; set; }

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; }

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection3 { get; set; }

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection4 { get; set; }

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged();
        }
    }

    public Person _userProfile { get; set; }

    public Person UserProfile
    {
        get => _userProfile;
        set
        {
            _userProfile = value;
            OnPropertyChanged();
        }
    }

    public Image _userProfileImage { get; set; }

    public Image UserProfileImage
    {
        get => _userProfileImage;
        set
        {
            _userProfileImage = value;
            OnPropertyChanged();
        }
    }

    public Pictures4Tablet CurrentPicture
    {
        get => _currentPicture;
        set
        {
            _currentPicture = value;

            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        var rootFolder = FileSystem.Current.LocalStorage;

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

        UserProfile = NavigationalParameters.SelectedUser;

        Title = $"{UserProfile.FullName} Profile";

        Roles = UserProfile.MyGroups;

        // CurrentPicture = _pictureService.GetUserProfilePictureAsync();

        CurrentPicture.PicturePathOnTablet = Path.Combine(rootFolder.Path, "JobPictures", CurrentPicture.FileName);
    });

    public RelayCommand TrainingListCommand => new(async () =>
    {
        //NavigationalParameters.AppMode = NavigationalParameters.AppModes.TRAININGDOCS;

        await NavigateTo(ViewModelLocator.DocumentListingPage);
    });

    public RelayCommand BackCommand => new(async () => { await NavigateTo(ViewModelLocator.SignaturePage); });

    public Pictures4Tablet _currentPicture { get; private set; }
}


#region Commented Out Code

//public List<Pictures4Tablet> GetAllPictures()
//{
//    return App.Database.GetItems<Pictures4Tablet>().ToList();
//}

//public async Task<int> SyncJobPictures2Tablet(long strQnumber, int LoggedOnUserId)
//{
//    int strReturnValue = -1;

//    var _myPics4thisJob = GetJobPictures(strQnumber.ToString(), LoggedOnUserId.ToString());

//    if (_myPics4thisJob != null && _myPics4thisJob.Count > 0)
//    {
//        strReturnValue = _myPics4thisJob.Count;

//        LocalStorage ls = new LocalStorage();
//        //Make sure pics exist on Tablet & get them if not
//        foreach (var item in _myPics4thisJob)
//        {
//            if (item != null)
//            {
//                if (await ls.DoesJobPictureExistOnTablet(item.FileName))
//                {
//                    await ls.UpdateFileFromAzure(item.FileName, "JobPictures", "JobPictures/");
//                }
//            }
//        }
//    }
//    return strReturnValue;
//}

//public async Task<string> SyncJobPicture2Azure(string Filename)
//{
//    string returnCheck = "Pic Transferred OK";
//    byte[] imageFromLocalStorage = null;
//    LocalStorage ls = new LocalStorage();
//    var picExists = await ls.DoesJobPictureExistOnTablet(Filename);
//    if (picExists)
//    {
//        // Doesnt Exist so need to return with error
//        return "Picture doesnt exist on tablet!";
//    } else
//    {
//        // Pic exists so go get it into a byte array
//        imageFromLocalStorage = await ls.GetImageFromLocalstorage("JobPictures", Filename);
//        if (imageFromLocalStorage.Length > 1000)
//        {
//            await ls.Save2AzureBlobByByteArray(imageFromLocalStorage, $"JobPictures/{Filename}", Constants.FocusDataContainerInAzure.ToString());

//        } else
//        {
//            return "Picture doesnt exist on tablet!";
//        }

//    }
//    return returnCheck;
//}

#endregion