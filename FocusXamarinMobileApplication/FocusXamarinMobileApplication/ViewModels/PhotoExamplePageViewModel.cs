#region

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class PhotoExamplePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public PhotoExamplePageViewModel()
    {
        Title = "As Built Images";
        _ls = new LocalStorage();
        _jobService = new Jobs();
        _picService = new Pictures();
    }

    private Pictures _picService { get; }
    private LocalStorage _ls { get; }
    public Jobs _jobService { get; set; }
    private Pictures4Tablet _photo { get; set; }

    public Pictures4Tablet Photo
    {
        get => _photo;
        set
        {
            _photo = value;

            OnPropertyChanged();
        }
    }

    public bool _showSubmitButtons { get; set; }

    public bool ShowSubmitButtons
    {
        get => _showSubmitButtons;
        set
        {
            _showSubmitButtons = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; } = "";

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    public string _projectDate { get; set; } = "";

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public bool _showExampleFull { get; set; }

    public bool ShowExampleFull
    {
        get => _showExampleFull;
        set
        {
            _showExampleFull = value;
            OnPropertyChanged();
        }
    }

    public bool _showSplitPage { get; set; }

    public bool ShowSplitPage
    {
        get => _showSplitPage;
        set
        {
            _showSplitPage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource ButtonIcon { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource _sampleImage { get; set; }

    public ImageSource SampleImage
    {
        get => _sampleImage;
        set
        {
            _sampleImage = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _pictureToDisplay { get; set; }

    public ImageSource PictureToDisplay
    {
        get => _pictureToDisplay;
        set
        {
            _pictureToDisplay = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        try
        {
            Photo = NavigationalParameters.SelectedPhoto ?? new Pictures4Tablet();

            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

            if (NavigationalParameters.SelectedQuestion?.FollowUpAction != null)
            {
                SampleImage = SimpleStaticHelpers.GetImage(NavigationalParameters.SelectedQuestion?.FollowUpAction);

                if (Photo?.Image != null && SampleImage != null)
                {
                    PictureToDisplay = ImageSource.FromStream(() => new MemoryStream(Photo?.Image));

                    ShowExampleFull = false;
                    ShowSplitPage = true;
                    ShowSubmitButtons = true;
                }
                else if (Photo?.Image == null && SampleImage != null)
                {
                    ShowExampleFull = true;
                    ShowSplitPage = false;
                    ShowSubmitButtons = false;
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    });

    public RelayCommand Back => new(async () =>
    {
        try
        {
            var confirm = await Alert(
                "The current photo will be removed please confirm yo wish to continue, this cannot be undone?",
                "Warning?", "Continue",
                "Cancel");

            if (confirm) DeletePhoto.Execute(null);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand TakePhoto => new(async () =>
    {
        try
        {
            await CrossMedia.Current.Initialize();

            Photo = NavigationalParameters.SelectedPhoto = new Pictures4Tablet();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Alert("No Camera", ":( No camera available.");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 18, //Resize to 90% of original
                CompressionQuality = 92
            });


            if (file != null)
                try
                {
                    byte[] imageAsBytes = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.GetStream().CopyTo(memoryStream);
                        imageAsBytes = memoryStream.ToArray();
                        file.Dispose();
                    }

                    if (imageAsBytes.Count() > 1000)
                    {
                        if (Photo != null)
                        {
                            var time = DateTime.Now.ToString("HH:mm:ss").Replace(":", "");

                            if (NavigationalParameters.CurrentAssignment != null)
                            {
                                Photo.AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId;
                                Photo.Identifier = NavigationalParameters.CurrentAssignment.AssignmentId;
                                Photo.StreetName = NavigationalParameters.CurrentAssignment?.StreetName;
                                Photo.Category =
                                    !string.IsNullOrEmpty(NavigationalParameters.CurrentAssignment?.Category.ToUpper())
                                        ? NavigationalParameters.CurrentAssignment?.Category
                                        : NavigationalParameters.AppMode.ToString();
                                Photo.QNumber = NavigationalParameters.CurrentAssignment?.Qnumber;
                            }
                            else
                            {
                                Photo.PictureReason = NavigationalParameters.PhotoMode.ToString();
                                Photo.Category = NavigationalParameters.AppMode.ToString();
                            }

                            try
                            {
                                Photo.PicturePath = "JobPictures";
                                Photo.Image = imageAsBytes;
                                Photo.OperativeId = NavigationalParameters.LoggedInUser?.FocusId;
                                Photo.OperativeRole = NavigationalParameters.LoggedInUser?.Role;


                                Photo.QuestionId = NavigationalParameters.SelectedQuestion?.QuestionId.ToString() ??
                                                   "0";
                                                        

                                Photo.QNumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber;
                                Photo.ResponseId = NavigationalParameters.SelectedAnswer?.Id.ToString() ?? "0";
                                Photo.OperativeId = NavigationalParameters.LoggedInUser?.FocusId;
                                Photo.OperativeRole = NavigationalParameters.LoggedInUser?.Role;

                                Photo.FileName =
                                    $"{time}_{DateTime.Now:ddMMyyyy}_{NavigationalParameters.CurrentSelectedJob?.QuoteNumber}_{NavigationalParameters.LoggedInUser?.FocusId}.jpg";
                                Photo.GangLeaderId = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
                                Photo.SupervisorId = NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();
                                Photo.ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference;
                                Photo.DateTimeTaken =
                                    NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                                        ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                                        : DateTime.Now;
                                Photo.JobId = NavigationalParameters.CurrentSelectedJob.JobId;
                            }
                            catch (Exception ex)
                            {
                                var error = ex.ToString();
                            }

                            try
                            {
                                var loc = await SimpleStaticHelpers.GetCurrentPosition(5);

                                Photo.Latitude = loc?.Latitude.ToString() ??
                                                 NavigationalParameters.NewPosition.Latitude.ToString();

                                Photo.Longitude = loc?.Longitude.ToString() ??
                                                  NavigationalParameters.NewPosition.Longitude.ToString();
                            }
                            catch
                            {
                                Photo.Latitude = "N/A";

                                Photo.Longitude = "N/A";
                            }

                            if (NavigationalParameters.ClaimFile != null) {
                                Photo.PictureReason = NavigationalParameters.ClaimFile.ClaimHeader;
                            }

                            switch (NavigationalParameters.AppMode)
                            {
                                case NavigationalParameters.AppModes.CHAMBERASBUILT:
                                    Photo.PictureReason = "CHAMBERASBULT";
                                    break;
                                case NavigationalParameters.AppModes.BJEASBUILT:
                                    Photo.PictureReason = "BJEASBUILT";
                                    break;
                                case NavigationalParameters.AppModes.DJEASBUILT:
                                    Photo.PictureReason = "DJEASBUILT";
                                    break;
                                case NavigationalParameters.AppModes.FJEASBUILT:
                                    Photo.PictureReason = "FJEASBUILT";
                                    break;
                                case NavigationalParameters.AppModes.DPASBUILT:
                                    Photo.PictureReason = "DPASBuilt";
                                    break;
                                case NavigationalParameters.AppModes.POLEASBUILT:
                                    Photo.PictureReason = "POLEASBUILT";
                                    break;
                            }

                            if (NavigationalParameters.SelectedAnswer != null)
                            {
                                Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer.Identifier;

                                Photo.QuestionId =
                                    string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer.QuestionId.ToString())
                                        ? "0"
                                        : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();

                                Photo.StreetName = NavigationalParameters.SelectedAnswer?.StreetName;
                            }

                            if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.DFE)
                                Photo.Identifier = NavigationalParameters.CurrentDfe.DfeId;

                            await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);

                            await _picService.AddPicture(Photo);

                            PictureToDisplay = ImageSource.FromStream(() => new MemoryStream(Photo?.Image));

                            if (Photo?.Image != null && SampleImage != null)
                            {
                                ShowExampleFull = false;
                                ShowSplitPage = true;
                                ShowSubmitButtons = true;
                            }
                            else if (Photo?.Image == null && SampleImage != null)
                            {
                                ShowExampleFull = true;
                                ShowSplitPage = false;
                                ShowSubmitButtons = false;
                            }


                            NavigationalParameters.SelectedPhoto = Photo;
                        }
                    }
                    else
                    {
                        // No pic
                        await Alert("Warning", "No image has been taken");
                    }
                }
                catch (Exception ex)
                {
                    await Alert("Warning", $"{ex}");
                    var error = ex.ToString();
                }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent($"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand DeletePhoto => new(async () =>
    {
        try
        {
            SampleImage = null;

            PictureToDisplay = null;

            if (Photo?.Image != null && Photo?.FileName != "")
            {
                await _picService.DeleteJobPicture(Photo);

                await _ls.RemoveImageFromLocalStorage("JobPictures", Photo?.PicturePath.Split('/').Last());
            }

            Photo = NavigationalParameters.SelectedPhoto = null;

            ShowSubmitButtons = false;

            await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });

    public RelayCommand SavePhoto => new(async () =>
    {
        try
        {
            if (Photo.Image != null)
            {
                if (NavigationalParameters.CurrentSelectedJob != null)
                    Photo.JobId = NavigationalParameters.CurrentSelectedJob.JobId;

                if (Photo.PictureReason.ToLower() == "asbuilt")
                {
                    Photo.Category = Photo.PictureReason = NavigationalParameters.SurveyType.ToString();

                    App.Database.SaveItem(Photo);
                }
                await _picService.AddPicture(Photo);

                await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);

                Photo = NavigationalParameters.SelectedPhoto = null;

                PictureToDisplay = null;
                SampleImage = null;

                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
            }
            else
            {
                await Alert("Warning", "No image has been taken or reason selected");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    });
}