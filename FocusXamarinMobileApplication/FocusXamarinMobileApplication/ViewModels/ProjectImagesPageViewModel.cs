#region

using FileSystem = PCLStorage.FileSystem;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class ProjectImagesPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly WebApi wa;
    private Pictures4Tablet _currentPicture;
    private ObservableCollection<Pictures4Tablet> _pictures;


    public ProjectImagesPageViewModel()
    {
        _pictureService = new Pictures();
        wa = new WebApi();
        IsLoading = false;
    }

    public Pictures _pictureService { get; set; }
    public LocalStorage ls { get; set; }

    //   public FocusMobileV3Database sd { get; set; }

    public bool _isLoading { get; set; }
    public bool _showSubmit { get; set; }
    public bool _showDelete { get; set; }


    public bool areWeConnected { get; set; }

    public ImageSource _inputDiary { get; set; } = SimpleStaticHelpers.GetImage("InputDiary");

    public ImageSource InputDiary
    {
        get => _inputDiary;
        set
        {
            _inputDiary = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _cameraImage { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource CameraImage
    {
        get => _cameraImage;
        set
        {
            _cameraImage = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Pictures4Tablet> Pictures
    {
        get => _pictures;
        set
        {
            _pictures = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool ShowSubmit
    {
        get => _showSubmit;
        set
        {
            _showSubmit = value;
            OnPropertyChanged();
        }
    }

    public bool ShowDelete
    {
        get => _showDelete;
        set
        {
            _showDelete = value;
            OnPropertyChanged();
        }
    }

    public Pictures4Tablet CurrentPicture
    {
        get => _currentPicture;
        set
        {
            _currentPicture = value;

            if (CurrentPicture != null && CurrentPicture.SeverPictureId <= 0)
                IsLoading = false;
            else
                IsLoading = false;

            OnPropertyChanged();
        }
    }

    public RelayCommand Back => new(async () =>
    {
        if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.SURVEYQUESTION)
            await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
        if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.AUDITQUESTIONS)
        {
            await NavigateTo(ViewModelLocator.RatingSurveyQuestionsPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.CHAMBERAUDIT)
        {
            await NavigateTo(ViewModelLocator.AddChamber);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.POLESURVEY)
        {
            await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.UGCRPMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.POLEMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.CIVILSMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.CABLEMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.CHAMBERMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.DUCTMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.LATERALMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.LEADINMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.POLECABLEMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.LEADINMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.REINSTATEMENTMEASURE
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.SITESUPPORTMEASURE)
        {
            await NavigateTo(ViewModelLocator.InputMeasurePage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.STARTOFDAY
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.ENDOFDAY)
        {
            await NavigateTo(ViewModelLocator.DailyDiaryPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.RISKASSESMENT)
        {
            await NavigateTo(ViewModelLocator.DailyChecksIssuePage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PROJECTIMAGES)
        {
            await NavigateTo(ViewModelLocator.MenuSelectionPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTISSUE)
        {
            await NavigateTo(ViewModelLocator.PlantIssuePage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANT)
        {
            await NavigateTo(ViewModelLocator.DailyChecksIssuePage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTTRANSFERIN)
        {
            await NavigateTo(ViewModelLocator.PlantTransferInPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTTRANSFEROUT)
        {
            await NavigateTo(ViewModelLocator.PlantTransferPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.INVESTIGATION)
        {
            await NavigateTo(ViewModelLocator.MethodologyPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.CHAMBERAUDIT)
        {
            if (NavigationalParameters.SelectedAnswer != null)
                await NavigateTo(ViewModelLocator.AddChamber);
            else
                await NavigateTo(ViewModelLocator.AddChamber);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.BLOCKAGE)
        {
            await NavigateTo(ViewModelLocator.BlockagesInputPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.EVENTMANAGEMENT)
        {
            await NavigateTo(ViewModelLocator.RegisterNewEventPage);
        }
        else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.NCRAUDITDETAILS
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.NCRCORRECTIVEACTIONS
                 || NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.NCRINTERMEDIATEACTIONS)
        {
            await NavigateTo(ViewModelLocator.SiteInspectionRatingFailurePage);
        }
        else
        {
            await NavigateTo(ViewModelLocator.MenuSelectionPage);
        }
    });

    public RelayCommand Delete => new(async () =>
    {
        //  ShowSubmit = false;
        IsLoading = true;

        var userChoice = await Alert("Are you sure you want to delete the image.",
            "Caution!", "Yes", "No");
        if (userChoice)
            try
            {
                await _pictureService.DeleteJobPicture(CurrentPicture);

                await _pictureService.RemoveImageFromLocalStorage(CurrentPicture);
            }
            catch (Exception ex)
            {
                // await Alert("Error","Failed to delete image from pad","Ok");
            }

        IsLoading = false;

        await LoadPictures();
    });

    public RelayCommand Photo => new(async () =>
    {
        NavigationalParameters.SelectedPhoto = new Pictures4Tablet();
        await NavigateTo(ViewModelLocator.PhotoSelectionPage);
    });

    public RelayCommand Annotate => new(async () =>
    {
        if (CurrentPicture != null)
        {
            NavigationalParameters.ReturnPage = "Photo";

            NavigationalParameters.SelectedPhoto = CurrentPicture;

            await NavigateTo(ViewModelLocator.ImageEditorPage);
        }
        else
        {
            await Alert("No picture selected To annotate",
                "Ok!");
        }
    });

    public RelayCommand Upload => new(async () =>
    {
        if (CurrentPicture.SeverPictureId > 0)
        {
            await Alert("This image has been uploaded to server please swipe to refresh view",
                "Saved!");
        }
        else
        {
            areWeConnected = await wa.CanWeConnect2Api();
            // First off try & send to server
            // If there is no data then show message that can only save locally

            if (areWeConnected)
            {
                IsLoading = true;
                //  ShowSubmit = false;
                // 1. Send pic info over web service
                var serverPictureId = await wa.SavePicture2Server(CurrentPicture);
                if (serverPictureId == 0)
                {
                    await Alert(
                        "Failed to save data to the server. The picture will be saved locally please try to send later!",
                        "Error!");
                }
                else
                {
                    // it has been saved to the server so we have a db ID
                    CurrentPicture.SeverPictureId = serverPictureId;
                    //now try & upload to azure
                    var p = new Pictures();
                    CurrentPicture.Status =
                        await p.SyncPicture2Azure("JobPictures", "JobPictures", CurrentPicture.FileName);
                    if (CurrentPicture.Status != "Pic Transferred OK")
                        await Alert(
                            "There was an error in saving the picture to the remote server. The picture will be saved locally please try to send later!",
                            "Error!");
                    else
                        CurrentPicture.Status = "Saved on server";
                }

                await _pictureService.AddPicture(CurrentPicture);
                //    ShowSubmit = false;
                // ShowDelete = false;
                IsLoading = false;
                RefreshPictures.Execute(null);
            }
            else
            {
                // no connection so need to inform the user
                await Alert("NO Conectivity, Please find as better connection and try again",
                    "Connectivity Error!");
            }
        }
    });

    public RelayCommand UpdatePicCommnad => new(async () => { NavigationalParameters.SelectedPhoto = CurrentPicture; });


    public RelayCommand RefreshPictures => new(async () => { await LoadPictures(); });

    public RelayCommand EditImage => new(async () =>
    {
        NavigationalParameters.ReturnPage = "Photo";
        NavigationalParameters.SelectedPhoto.QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;
        NavigationalParameters.SelectedPhoto.ContractReference =
            NavigationalParameters.CurrentSelectedJob.ContractReference;
        NavigationalParameters.SelectedPhoto.JobId = NavigationalParameters.CurrentSelectedJob.JobId;
        NavigationalParameters.SelectedPhoto.OperativeId = NavigationalParameters.LoggedInUser.FocusId;
        NavigationalParameters.SelectedPhoto.OperativeRole = NavigationalParameters.LoggedInUser.Role;
        NavigationalParameters.SelectedPhoto.PicturePath = "JobPictures";
        NavigationalParameters.SelectedPhoto.GangLeaderId =
            NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString();
        NavigationalParameters.SelectedPhoto.PictureReason = "General";
        NavigationalParameters.SelectedPhoto.PicturePathOnTablet = "Uploaded From Library";

        var date = DateTime.Now.Date.ToShortDateString();

        var dateString = date.Replace("/", "");

        NavigationalParameters.SelectedPhoto.FileName =
            $"{dateString}_{Guid.NewGuid()}_{NavigationalParameters.LoggedInUser.FocusId}.jpg";

        NavigationalParameters.SelectedPhoto.DateTimeTaken =
            NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                : DateTime.Now;

        await ls.StoreImagesLocallyAndUpdatePath("JobPictures", NavigationalParameters.SelectedPhoto.FileName,
            NavigationalParameters.SelectedPhoto.Image);

        App.Database.SaveItem(NavigationalParameters.SelectedPhoto);

        CurrentPicture = NavigationalParameters.SelectedPhoto;

        await NavigateTo(ViewModelLocator.ImageEditorPage);
    });

    private Task LoadPictures()
    {
        var ls = new LocalStorage();
        var rootFolder = FileSystem.Current.LocalStorage;
        var tempPics = new List<Pictures4Tablet>();

        Pictures = new ObservableCollection<Pictures4Tablet>();
        ShowDelete = true;
        ShowSubmit = true;

        switch (NavigationalParameters.PhotoMode)
        {
            case NavigationalParameters.PhotoModes.CHAMBERAUDIT:
            case NavigationalParameters.PhotoModes.SITECLEAR:
            case NavigationalParameters.PhotoModes.PERMIT:
            case NavigationalParameters.PhotoModes.SURVEYQUESTION:
            case NavigationalParameters.PhotoModes.AUDITQUESTIONS:
            case NavigationalParameters.PhotoModes.POLESURVEY:
            {
                ShowDelete = false;

                ShowSubmit = false;

                if (NavigationalParameters.SelectedAnswer != null)
                    tempPics.AddRange(_pictureService.GetJobPictures(
                            NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                            NavigationalParameters.LoggedInUser.FocusId)
                        ?.Where(x => x.Identifier == NavigationalParameters.SelectedAnswer?.Identifier)?.ToList());
                else
                    tempPics.AddRange(_pictureService.GetJobPictures(
                            NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                            NavigationalParameters.LoggedInUser.FocusId)
                        ?.Where(x => x.Identifier.ToString() == NavigationalParameters.SelectedEndPoint?.L4Number)
                        ?.ToList());

                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);
                break;
            }
            case NavigationalParameters.PhotoModes.UGCRPMEASURE:
            case NavigationalParameters.PhotoModes.CABLEMEASURE:
            case NavigationalParameters.PhotoModes.CHAMBERMEASURE:
            case NavigationalParameters.PhotoModes.CIVILSMEASURE:
            case NavigationalParameters.PhotoModes.DUCTMEASURE:
            case NavigationalParameters.PhotoModes.LATERALMEASURE:
            case NavigationalParameters.PhotoModes.LEADINMEASURE:
            case NavigationalParameters.PhotoModes.POLECABLEMEASURE:
            case NavigationalParameters.PhotoModes.POLEMEASURE:
            case NavigationalParameters.PhotoModes.REINSTATEMENTMEASURE:
            {
                ShowDelete = false;

                ShowSubmit = false;


                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x =>
                        x.Identifier == NavigationalParameters.ClaimFile?.ClaimId &&
                        x.QuestionId == NavigationalParameters.ClaimFile.ClaimHeader)?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);
                break;
            }
            case NavigationalParameters.PhotoModes.PLANTISSUE:
            {
                ShowDelete = false;

                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.Category.ToLower() == NavigationalParameters.AppMode.ToString().ToLower()
                                 && x.PictureReason.ToLower() == "plantissue")?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);
                break;
            }
            case NavigationalParameters.PhotoModes.PLANTTRANSFERIN:
            {
                ShowDelete = false;

                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.Category.ToLower() == NavigationalParameters.AppMode.ToString().ToLower()
                                 && x.PictureReason.ToLower() == "planttransferin")?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);
                break;
            }
            case NavigationalParameters.PhotoModes.PLANTTRANSFEROUT:
            {
                ShowDelete = false;

                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.Category.ToLower() == NavigationalParameters.AppMode.ToString().ToLower()
                                 && x.PictureReason.ToLower() == "planttransferout")?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);
                break;
            }
            case NavigationalParameters.PhotoModes.PLANT:
            {
                ShowDelete = false;

                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.Category.ToLower() == NavigationalParameters.AppMode.ToString().ToLower()
                                 && x.PictureReason == NavigationalParameters.PhotoMode.ToString()
                                 && x.QuestionId == NavigationalParameters.CurrentCheckAnswer?.QuestionId.ToString())
                    ?.ToList());

                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);
                break;
            }
            case NavigationalParameters.PhotoModes.RISKASSESMENT:
            {
                ShowDelete = false;
                ShowSubmit = false;


                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x =>
                        x.Category == NavigationalParameters.PhotoModes.RISKASSESMENT.ToString() && x.QuestionId ==
                        NavigationalParameters.CurrentCheckAnswer?.QuestionId.ToString())?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);

                break;
            }
            case NavigationalParameters.PhotoModes.STARTOFDAY:
            {
                ShowDelete = false;
                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.PictureReason.ToLower() == "start of day"
                                 && x.DateTimeTaken.Date == NavigationalParameters.CurrentSelectedJob.JobDate.Date)
                    ?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);

                break;
            }
            case NavigationalParameters.PhotoModes.ENDOFDAY:
            {
                ShowDelete = false;
                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.PictureReason.ToLower() == "end of day"
                                 && x.DateTimeTaken.Date == NavigationalParameters.CurrentSelectedJob.JobDate.Date)
                    ?.ToList());


                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);

                break;
            }
            case NavigationalParameters.PhotoModes.BLOCKAGE:
            {
                ShowDelete = false;
                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.Category.ToLower() == "blockage"
                                 && x.DateTimeTaken.Date == NavigationalParameters.CurrentSelectedJob.JobDate.Date)
                    ?.ToList());

                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);

                break;
            }
            case NavigationalParameters.PhotoModes.EVENTMANAGEMENT:
            case NavigationalParameters.PhotoModes.NCRAUDITDETAILS:
            case NavigationalParameters.PhotoModes.NCRCORRECTIVEACTIONS:
            case NavigationalParameters.PhotoModes.NCRINTERMEDIATEACTIONS:
            {
                ShowDelete = false;
                ShowSubmit = false;

                tempPics.AddRange(_pictureService.GetJobPictures(
                        NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                        NavigationalParameters.LoggedInUser.FocusId)
                    ?.Where(x => x.PictureReason.ToLower() == NavigationalParameters.AppMode.ToString().ToLower()
                                 && x.Identifier.ToString().ToLower() == NavigationalParameters.EventManagement
                                     .Identifier.ToString().ToLower())?.ToList());

                Pictures = new ObservableCollection<Pictures4Tablet>(tempPics);

                break;
            }
            default:
            {
                Pictures = new ObservableCollection<Pictures4Tablet>(_pictureService.GetJobPictures(
                    NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                    NavigationalParameters.LoggedInUser.FocusId));

                ShowSubmit &= Pictures.Any(x => string.IsNullOrEmpty(x.Status));
                break;
            }
        }

        foreach (var pic in Pictures)
            pic.PicturePathOnTablet = Path.Combine(rootFolder.Path, "JobPictures", pic.FileName);

        return Task.CompletedTask;
    }
}