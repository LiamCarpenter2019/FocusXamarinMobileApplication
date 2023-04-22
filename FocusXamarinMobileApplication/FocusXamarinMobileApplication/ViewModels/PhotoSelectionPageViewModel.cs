#region

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class PhotoSelectionPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private Pictures _picService { get; }
    private LocalStorage _ls { get; }

    private Pictures4Tablet _photo { get; set; }
    public Jobs _jobService { get; set; }

    public ImageSource _source { get; set; }

    public ImageSource Source
    {
        get => _source;
        set
        {
            _source = value;
            OnPropertyChanged();
        }
    }

    public ImageSource _pictureToDisplay { get; set; } = SimpleStaticHelpers.GetImage("If_camera_W");

    public ImageSource PictureToDisplay
    {
        get => _pictureToDisplay;
        set
        {
            _pictureToDisplay = value;
            OnPropertyChanged("ButtonAImage");
        }
    }

    private bool _showButton { get; set; } = true;
    private bool _showSubmitButtons { get; set; } = true;
    public bool _visible { get; set; } = true;
    public bool _showPlaceHolder { get; set; } = true;
    public bool _showImage { get; set; }

    public bool _showPhotoButton { get; set; }

    private string _deleteButtonText { get; set; } = "Delete";
    public string _comments { get; set; } = "";

    private Color _route { get; set; } = Color.LightGray;
    private Color _siteClear { get; set; } = Color.LightGray;
    private Color _general { get; set; } = Color.LightGray;
    private Color _hse { get; set; } = Color.LightGray;
    private Color _pia { get; set; } = Color.LightGray;
    private Color _incident { get; set; } = Color.LightGray;
    private Color _obstruction { get; set; } = Color.LightGray;
    private Color _condition { get; set; } = Color.LightGray;
    private Color _trenchDepth { get; set; } = Color.LightGray;
    private Color _backFillDepth { get; set; } = Color.LightGray;
    private Color _trialHole { get; set; } = Color.LightGray;
    private Color _chamber { get; set; } = Color.LightGray;

    private Photostate _photoState { get; set; }

    public Color TrenchDepth
    {
        get => _trenchDepth;
        set
        {
            _trenchDepth = value;
            OnPropertyChanged();
        }
    }

    public Color BackFillDepth
    {
        get => _backFillDepth;
        set
        {
            _backFillDepth = value;
            OnPropertyChanged();
        }
    }

    public Color TrialHole
    {
        get => _trialHole;
        set
        {
            _trialHole = value;
            OnPropertyChanged();
        }
    }

    public Color Chamber
    {
        get => _chamber;
        set
        {
            _chamber = value;
            OnPropertyChanged();
        }
    }

    public Color Route
    {
        get => _route;
        set
        {
            _route = value;
            OnPropertyChanged();
        }
    }

    public Color Condition
    {
        get => _condition;
        set
        {
            _condition = value;
            OnPropertyChanged();
        }
    }

    public Color Obstruction
    {
        get => _obstruction;
        set
        {
            _obstruction = value;
            OnPropertyChanged();
        }
    }

    public Color Incident
    {
        get => _incident;
        set
        {
            _incident = value;
            OnPropertyChanged();
        }
    }

    public Color PIA
    {
        get => _pia;
        set
        {
            _pia = value;
            OnPropertyChanged();
        }
    }

    public Color Hse
    {
        get => _hse;
        set
        {
            _hse = value;
            OnPropertyChanged();
        }
    }

    public Color General
    {
        get => _general;
        set
        {
            _general = value;
            OnPropertyChanged();
        }
    }

    public Color SiteClear
    {
        get => _siteClear;
        set
        {
            _siteClear = value;
            OnPropertyChanged();
        }
    }

    //----------------
    public bool ShowButton
    {
        get => _showButton;
        set
        {
            _showButton = value;
            OnPropertyChanged();
        }
    }

    public bool ShowSubmitButtons
    {
        get => _showSubmitButtons;
        set
        {
            _showSubmitButtons = value;
            OnPropertyChanged();
        }
    }

    public string DeleteButtonText
    {
        get => _deleteButtonText;
        set
        {
            _deleteButtonText = value;
            OnPropertyChanged();
        }
    }

    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
            OnPropertyChanged();
        }
    }

    public bool ShowPlaceHolder
    {
        get => _showPlaceHolder;
        set
        {
            _showPlaceHolder = value;
            OnPropertyChanged();
        }
    }

    public bool ShowImage
    {
        get => _showImage;
        set
        {
            _showImage = value;
            OnPropertyChanged();
        }
    }


    public bool ShowPhotoButton
    {
        get => _showPhotoButton;
        set
        {
            _showPhotoButton = value;
            OnPropertyChanged();
        }
    }

    public string Comments
    {
        get => _comments;
        set
        {
            _comments = value;
            OnPropertyChanged();
        }
    }

    public string Reason
    {
        get => _reason;
        set
        {
            _reason = value;
            OnPropertyChanged();
        }
    }

    public Pictures4Tablet Photo
    {
        get => _photo;
        set
        {
            _photo = value;

            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        Photo = NavigationalParameters.SelectedPhoto ?? new Pictures4Tablet();

        ShowPhotoButton = false;
        ShowImage = false;

        switch (NavigationalParameters.PhotoMode)
        {
            case NavigationalParameters.PhotoModes.STARTOFDAY:
            case NavigationalParameters.PhotoModes.INVESTIGATION:
            case NavigationalParameters.PhotoModes.ENDOFDAY:
            case NavigationalParameters.PhotoModes.AUDITQUESTIONS:
            case NavigationalParameters.PhotoModes.RISKASSESMENT:
            case NavigationalParameters.PhotoModes.PLANT:
            case NavigationalParameters.PhotoModes.PLANTISSUE:
            case NavigationalParameters.PhotoModes.CVI:
            case NavigationalParameters.PhotoModes.BLOCKAGE:
            case NavigationalParameters.PhotoModes.INVOICE:
            case NavigationalParameters.PhotoModes.EVENTMANAGEMENT:
            case NavigationalParameters.PhotoModes.PLANTTRANSFEROUT:
            case NavigationalParameters.PhotoModes.PLANTTRANSFERIN:
            case NavigationalParameters.PhotoModes.CHAMBERAUDIT:
            case NavigationalParameters.PhotoModes.NCRAUDITDETAILS:
            case NavigationalParameters.PhotoModes.NCRCORRECTIVEACTIONS:
            case NavigationalParameters.PhotoModes.NCRINTERMEDIATEACTIONS:
            case NavigationalParameters.PhotoModes.POLEMEASURE:
            case NavigationalParameters.PhotoModes.CHAMBERMEASURE:
            case NavigationalParameters.PhotoModes.DUCTMEASURE:
            case NavigationalParameters.PhotoModes.CABLEMEASURE:
            case NavigationalParameters.PhotoModes.REINSTATEMENTMEASURE:
            case NavigationalParameters.PhotoModes.CIVILSMEASURE:
            case NavigationalParameters.PhotoModes.LATERALMEASURE:
            case NavigationalParameters.PhotoModes.POLECABLEMEASURE:
            case NavigationalParameters.PhotoModes.LEADINMEASURE:
            case NavigationalParameters.PhotoModes.UGCRPMEASURE:
            case NavigationalParameters.PhotoModes.SITESUPPORTMEASURE:
                ShowButton = false;
                break;
            case NavigationalParameters.PhotoModes.ASBUILT:
                ShowButton = false;
                ShowImage = true;
                break;
        }

        ShowPlaceHolder = true;


        if (string.IsNullOrEmpty(Photo.PictureComments)) Comments = "";

        if (Photo?.Image != null)
        {
            Visible = false;
            _photoState = Photostate.EDIT;
            Source = ImageSource.FromStream(() => new MemoryStream(Photo?.Image));
        }

        NavigationalParameters.SelectedPhoto = Photo;

        ShowPhotoButton = true;
    });

    public RelayCommand TakePhoto => new(async () =>
    {
        await CrossMedia.Current.Initialize();

        Photo = NavigationalParameters.SelectedPhoto = new Pictures4Tablet();

        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        {
            await Alert("No Camera", ":( No camera available.");
            return;
        }

        // var file = await CrossMedia.Current.TakePhotoA
        // sync(new StoreCameraMediaOptions());

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
                            Photo.QNumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                            NavigationalParameters.CurrentAssignment?.Qnumber;
                        }

                        try
                        {
                            Photo.PicturePath = "JobPictures";
                            Photo.Image = imageAsBytes;
                            Photo.OperativeId = NavigationalParameters.LoggedInUser?.FocusId;
                            Photo.OperativeRole = NavigationalParameters.LoggedInUser?.Role;

                            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
                                Photo.QuestionId =
                                    NavigationalParameters.SelectedRatingQuestion?.QuestionId.ToString() ?? "0";
                            else
                                Photo.QuestionId = NavigationalParameters.SelectedQuestion?.QuestionId.ToString() ??
                                                   "0";

                            //Reason = Photo.PictureReason = NavigationalParameters.PhotoMode.ToString();

                            Photo.QNumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                            NavigationalParameters.CurrentAssignment?.Qnumber;
                            Photo.ResponseId = NavigationalParameters.SelectedAnswer?.Id.ToString() ?? "0";
                            Photo.OperativeId = NavigationalParameters.LoggedInUser?.FocusId;
                            Photo.OperativeRole = NavigationalParameters.LoggedInUser?.Role;
                            Photo.Category = NavigationalParameters.AppMode.ToString();
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

                        if (NavigationalParameters.AddingNewAsset)
                        {
                            NavigationalParameters.AddingNewAsset = false;

                            ShowButton = true;
                            Photo.Category = NavigationalParameters.SurveyType.ToString();
                            Photo.Identifier = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId;
                            Reason = Photo.PictureReason = "New Asset";
                            Photo.QNumber =
                                Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                NavigationalParameters.CurrentAssignment?.Qnumber);

                            await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                Photo?.Image);
                            await _picService.AddPicture(Photo);
                        }
                        else
                        {
                            switch (NavigationalParameters.AppMode)
                            {
                                case NavigationalParameters.AppModes.PLANT:
                                case NavigationalParameters.AppModes.SUPERVISORGANGPLANT:
                                    {
                                        Photo.FileName =
                                            $"{DateTime.Now.ToString("hhmmss")}_{DateTime.Now.ToString("ddMMyyyy")}_CheckIssuePic_{NavigationalParameters.CurrentCheckAnswer?.QuestionId}.jpg";
                                        Photo.DateTimeTaken =
                                            NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                                                ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                                                : DateTime.Now;
                                        Reason = Photo.PictureReason = NavigationalParameters.PhotoMode.ToString();
                                        Photo.ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference;
                                        Photo.GangLeaderId = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
                                        Photo.SupervisorId = NavigationalParameters.LoggedInUser?.FocusId.ToString();
                                        Photo.QNumber =
                                            Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                            NavigationalParameters.CurrentAssignment?.Qnumber);
                                        Photo.Category = $"{NavigationalParameters.AppMode.ToString()}";

                                        if (NavigationalParameters.CurrentCheckAnswer?.QuestionId != null)
                                            Photo.QuestionId =
                                                NavigationalParameters.CurrentCheckAnswer?.QuestionId.ToString() ?? "0";

                                        if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTISSUE)
                                        {
                                            Photo.QuestionId = NavigationalParameters.CurrentCheckAnswer?.QuestionId.ToString();

                                            Photo.PictureComments =
                                                $"The {NavigationalParameters.SelectetedPlantItem.Type} with asset number --{NavigationalParameters.SelectetedPlantItem.AssetNo}-- has an issue";

                                            if (string.IsNullOrEmpty(NavigationalParameters.CurrentPlantIssue
                                                    ?.Picture1Filename))
                                                NavigationalParameters.CurrentPlantIssue.Picture1Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.CurrentPlantIssue
                                                         ?.Picture2Filename))
                                                NavigationalParameters.CurrentPlantIssue.Picture2Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.CurrentPlantIssue
                                                         ?.Picture3Filename))
                                                NavigationalParameters.CurrentPlantIssue.Picture3Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.CurrentPlantIssue
                                                         ?.Picture4Filename))
                                                NavigationalParameters.CurrentPlantIssue.Picture4Filename = Photo?.FileName;
                                        }
                                        else if (NavigationalParameters.PhotoMode ==
                                                 NavigationalParameters.PhotoModes.PLANTTRANSFERIN)
                                        {
                                            // Photo.PicturePath = "PlantTransferPics";
                                            ShowButton = false;
                                            Photo.QuestionId = NavigationalParameters.CurrentCheckAnswer?.QuestionId.ToString();

                                            Reason = Photo.PictureReason = "PlantTransferIn";
                                            Photo.PictureComments =
                                                $"This image has been taken whilst transfering a {NavigationalParameters.PlantTransfers.PlantType} with asset number --{NavigationalParameters.PlantTransfers.PlantAssetNo}--";
                                            if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers?.Picture1Filename))
                                                NavigationalParameters.PlantTransfers.Picture1Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers
                                                         ?.Picture2Filename))
                                                NavigationalParameters.PlantTransfers.Picture2Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers
                                                         ?.Picture3Filename))
                                                NavigationalParameters.PlantTransfers.Picture3Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers
                                                         ?.Picture4Filename))
                                                NavigationalParameters.PlantTransfers.Picture4Filename = Photo?.FileName;
                                        }
                                        else if (NavigationalParameters.PhotoMode ==
                                                 NavigationalParameters.PhotoModes.PLANTTRANSFEROUT)
                                        {
                                            // Photo.PicturePath = "PlantTransferPics";
                                            ShowButton = false;
                                            Reason = Photo.PictureReason = "PlantTransferOut";
                                            Photo.PictureComments =
                                                $"This image has been taken whilst transfering a {NavigationalParameters.PlantTransfers.PlantType} with asset number --{NavigationalParameters.PlantTransfers.PlantAssetNo}--";
                                            if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers?.Picture1Filename))
                                                NavigationalParameters.PlantTransfers.Picture1Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers
                                                         ?.Picture2Filename))
                                                NavigationalParameters.PlantTransfers.Picture2Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers
                                                         ?.Picture3Filename))
                                                NavigationalParameters.PlantTransfers.Picture3Filename = Photo?.FileName;
                                            else if (string.IsNullOrEmpty(NavigationalParameters.PlantTransfers
                                                         ?.Picture4Filename))
                                                NavigationalParameters.PlantTransfers.Picture4Filename = Photo?.FileName;
                                        }
                                    }

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);

                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.RISKASSESMENT:
                                    {
                                        Photo.FileName =
                                            $"{DateTime.Now.ToString("hhmmss")}_{DateTime.Now.ToString("ddMMyyyy")}_CheckIssuePic_{NavigationalParameters.CurrentCheckAnswer?.QuestionId}.jpg";
                                        Photo.DateTimeTaken =
                                            NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                                                ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                                                : DateTime.Now;
                                        Reason = Photo.PictureReason = "RiskAssesment";
                                        Photo.ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference;
                                        Photo.GangLeaderId = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
                                        Photo.SupervisorId = NavigationalParameters.LoggedInUser?.FocusId.ToString();
                                        Photo.QNumber =
                                            Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                            NavigationalParameters.CurrentAssignment?.Qnumber);
                                        Photo.Category = $"{NavigationalParameters.AppMode.ToString()}";
                                        Photo.QuestionId = $"{NavigationalParameters.CurrentCheckAnswer?.QuestionId}";
                                        NavigationalParameters.CurrentCheckAnswer.PictureFileName = Photo?.FileName;
                                        NavigationalParameters.CurrentCheckAnswer.PictureLatitude = Photo?.Latitude;
                                        NavigationalParameters.CurrentCheckAnswer.PictureLongitude = Photo?.Longitude;
                                        App.Database.SaveItem(NavigationalParameters.CurrentCheckAnswer);

                                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);

                                        await _picService.AddPicture(Photo);
                                        //  NavigateBack();
                                    }
                                    break;
                                case NavigationalParameters.AppModes.EVENTMANAGEMENT:
                                case NavigationalParameters.AppModes.INCIDENT:
                                case NavigationalParameters.AppModes.NEARMISS:
                                case NavigationalParameters.AppModes.ACCIDENT:
                                case NavigationalParameters.AppModes.UTILITYDAMAGE:
                                    ShowButton = false;
                                    Reason = Photo.PictureReason =
                                        NavigationalParameters.EventManagement?.Category.ToUpper();
                                    Photo.Category = Reason;
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = NavigationalParameters.EventManagement.Identifier; //106

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.ORDERBOOK:
                                    {
                                        ShowButton = false;
                                        Reason = Photo.PictureReason = "Invoice";
                                        Photo.FileName =
                                            $"{DateTime.Now.ToString("hhmmss")}_{DateTime.Now.ToString("ddMMyyyy")}_Invoice_{NavigationalParameters.CurrentCheckAnswer?.QuestionId}.jpg";
                                        Photo.DateTimeTaken =
                                            NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                                                ? NavigationalParameters.CurrentSelectedJob.JobDate + DateTime.Now.TimeOfDay
                                                : DateTime.Now;
                                        Reason = Photo.PictureReason = "Invoice";
                                        Photo.ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference;
                                        Photo.GangLeaderId = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
                                        Photo.SupervisorId = NavigationalParameters.LoggedInUser?.FocusId.ToString();
                                        Photo.QNumber =
                                            Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                            NavigationalParameters.CurrentAssignment?.Qnumber);
                                        Photo.Category = NavigationalParameters.AppModes.ORDERBOOK.ToString();
                                        break;
                                    }
                                case NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE:
                                    {
                                        Photo.FileName =
                                            $"{time}_{DateTime.Now:ddMMyyyy}_{NavigationalParameters.InvestigateDamage?.DamageDetails?.FirstOrDefault()?.QNumber}_{NavigationalParameters.LoggedInUser?.FocusId}.jpg";
                                        Photo.DateTimeTaken =
                                            NavigationalParameters.CurrentSelectedJob?.JobDate.Date < DateTime.Now.Date
                                                ? NavigationalParameters.CurrentSelectedJob.JobDate
                                                : DateTime.Now;
                                        Reason = Photo.PictureReason = "Incident / Strike";
                                        Photo.ContractReference = NavigationalParameters.InvestigateDamage?.DamageDetails
                                            ?.FirstOrDefault()?.ContractReference;
                                        Photo.GangLeaderId = NavigationalParameters.InvestigateDamage?.DamageDetails
                                            ?.FirstOrDefault()?.GangLeaderId.ToString();
                                        Photo.SupervisorId = NavigationalParameters.LoggedInUser?.FocusId.ToString();
                                        Photo.QNumber = Convert.ToInt64(NavigationalParameters.InvestigateDamage
                                            ?.DamageDetails?.FirstOrDefault()?.QNumber);
                                        Photo.Identifier = NavigationalParameters.InvestigateDamage.DamageDetails
                                            .FirstOrDefault().DamageGuid;
                                        Photo.Category =
                                            $"{NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE.ToString()}-{NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault().InvestigationId}";
                                    }
                                    break;
                                case NavigationalParameters.AppModes.LATERALS:
                                    {
                                        ShowButton = false;
                                        Photo.Identifier = NavigationalParameters.ClaimFile.ClaimId;
                                        //Photo.PictureComments = NavigationalParameters.PhotoMode.ToString();

                                        if (NavigationalParameters.PhotoMode ==
                                            NavigationalParameters.PhotoModes.LATERALMEASURE)
                                            Reason = Photo.PictureReason = "Lateral-Measure";
                                        else if (NavigationalParameters.PhotoMode ==
                                                 NavigationalParameters.PhotoModes.LATERALTOBY)
                                            Reason = Photo.PictureReason = "Lateral-Toby";
                                        else if (NavigationalParameters.PhotoMode ==
                                                 NavigationalParameters.PhotoModes.LATERALTRACK)
                                            Reason = Photo.PictureReason = "Lateral-Track";
                                    }
                                    break;
                                case NavigationalParameters.AppModes.PRESITE:
                                    ShowButton = true;
                                    Photo.Category = NavigationalParameters.SurveyType.ToString();
                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId = NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    Reason = Photo.PictureReason = "PreSite";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    // NavigateBack();
                                    break;
                                case NavigationalParameters.AppModes.PRESITECIVILS:
                                    ShowButton = true;
                                    Photo.Category = NavigationalParameters.SurveyType.ToString();
                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId = NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    Reason = Photo.PictureReason = "PreSiteCivilsSurvey";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    //  NavigateBack();
                                    break;
                                case NavigationalParameters.AppModes.PRESITEPREMISIS:
                                    ShowButton = true;
                                    Photo.Category = NavigationalParameters.SurveyType.ToString();
                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Reason = Photo.PictureReason = "PreSitePremisesSurvey";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    // NavigateBack();
                                    break;
                                case NavigationalParameters.AppModes.PERMITTODIG:
                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId = NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    Photo.Category = NavigationalParameters.SurveyType.ToString();
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Reason = Photo.PictureReason = "Permit to Dig";

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    // NavigateBack();
                                    break;
                                case NavigationalParameters.AppModes.CVI:
                                    ShowButton = false;
                                    Photo.Identifier = NavigationalParameters.CurrentCvi.CviId;
                                    Photo.Category = "CVI";
                                    Reason = Photo.PictureReason = "CVI";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.DFE:
                                    ShowButton = true;
                                    Photo.Identifier = NavigationalParameters.CurrentDfe.AssignmentId;
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.SITECLEAR:
                                    ShowButton = false;
                                    Reason = Photo.PictureReason = "Site Clear";
                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId = NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    // NavigateBack();
                                    break;
                                case NavigationalParameters.AppModes.BLOCKAGE:
                                    ShowButton = false;
                                    Reason = Photo.PictureReason = "Blockage";
                                    Photo.Identifier = NavigationalParameters.BlockageItem.ImageId;
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.SITEINSPECTION:
                                    {
                                        ShowButton = false;
                                        Photo.Category = NavigationalParameters.AppModes.SITEINSPECTION.ToString();
                                        Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                        Photo.QuestionId = NavigationalParameters.SelectedRatingQuestion?.QuestionId.ToString();
                                        Photo.QNumber =
                                            Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                            NavigationalParameters.CurrentAssignment?.Qnumber);
                                        Reason = Photo.PictureReason = "";

                                        if (NavigationalParameters.PhotoMode ==
                                            NavigationalParameters.PhotoModes.NCRINTERMEDIATEACTIONS)
                                        {
                                            Reason = Photo.PictureReason = "Immediate";
                                            await _ls.StoreImagesLocallyAndUpdatePath("NonConformancePics/", Photo?.FileName,
                                                Photo?.Image);
                                        }
                                        else if (NavigationalParameters.PhotoMode ==
                                                 NavigationalParameters.PhotoModes.NCRAUDITDETAILS)
                                        {
                                            Reason = Photo.PictureReason = "Detail";
                                            await _ls.StoreImagesLocallyAndUpdatePath("NonConformancePics/", Photo?.FileName,
                                                Photo?.Image);
                                        }
                                        else if (NavigationalParameters.PhotoMode ==
                                                 NavigationalParameters.PhotoModes.NCRCORRECTIVEACTIONS)
                                        {
                                            Reason = Photo.PictureReason = "Corrective";
                                            await _ls.StoreImagesLocallyAndUpdatePath("NonConformancePics/", Photo?.FileName,
                                                Photo?.Image);
                                        }
                                        else
                                        {
                                            Reason = Photo.PictureReason = "Audit";

                                            await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                                Photo?.Image);
                                        }

                                        await _picService.AddPicture(Photo);
                                        // NavigateBack();
                                    }
                                    break;
                                case NavigationalParameters.AppModes.PROJECTIMAGES:
                                    ShowButton = true;
                                    break;
                                case NavigationalParameters.AppModes.DIARIES:
                                    ShowButton = false;
                                    if (NavigationalParameters.PhotoMode ==
                                        NavigationalParameters.PhotoModes.STARTOFDAY)
                                        Reason = Photo.PictureReason = "Start of Day";
                                    else
                                        Reason = Photo.PictureReason = "End of Day";

                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.ToAddress = NavigationalParameters.ClaimedNotes?.StartAddress;
                                    Photo.FromAddress = NavigationalParameters.ClaimedNotes?.EndAddress;
                                    Photo.Identifier = (Guid)NavigationalParameters.ClaimedNotes?.ImageId;
                                    await _picService.AddPicture(Photo);
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    break;
                                case NavigationalParameters.AppModes.CHAMBERAUDIT:
                                    Reason = Photo.PictureReason = "CHAMBERAUDIT";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    break;
                                case NavigationalParameters.AppModes.POLEASBUILT:
                                    Reason = Photo.PictureReason = "POLEASBUILT";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.CHAMBERASBUILT:
                                    Reason = Photo.PictureReason = "CHAMBERASBULT";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.BJEASBUILT:
                                    Reason = Photo.PictureReason = "BJEASBUILT";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.DJEASBUILT:
                                    Reason = Photo.PictureReason = "DJEASBUILT";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.FJEASBUILT:
                                    Reason = Photo.PictureReason = "FJEASBUILT";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.DPASBUILT:
                                    Reason = Photo.PictureReason = "DPAsBuilt";
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);
                                    break;
                                case NavigationalParameters.AppModes.PRESITEPOLESURVEY:
                                case NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY:
                                case NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY:
                                case NavigationalParameters.AppModes.PRESITECHAMBERSURVEY:
                                case NavigationalParameters.AppModes.PRESITEDUCTSURVEY:
                                case NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY:
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Reason = NavigationalParameters.SelectedQuestion?.QuestionId == 0.10M ||
                                             NavigationalParameters.SelectedAnswer?.QuestionId == 0.10M
                                        ? Photo.PictureReason = "AssetLocationImage"
                                        : Photo.PictureReason = NavigationalParameters.AppMode.ToString();

                                    Photo.Identifier = (Guid)NavigationalParameters.SelectedAnswer?.Identifier;
                                    Photo.QuestionId =
                                        string.IsNullOrEmpty(NavigationalParameters.SelectedAnswer?.QuestionId.ToString())
                                            ? "0"
                                            : NavigationalParameters.SelectedAnswer?.QuestionId.ToString();
                                    ShowButton = false;
                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName,
                                        Photo?.Image);
                                    await _picService.AddPicture(Photo);

                                    if (NavigationalParameters.SelectedQuestion?.QuestionId == 0.10M ||
                                        NavigationalParameters.SelectedAnswer?.QuestionId == 0.10M)
                                        await NavigateTo(ViewModelLocator.ImageEditorPage);
                                    break;
                                case NavigationalParameters.AppModes.POLEMEASURE:
                                case NavigationalParameters.AppModes.DISTRIBUTIONPOINTMEASURE:
                                case NavigationalParameters.AppModes.CHAMBERMEASURE:
                                case NavigationalParameters.AppModes.DUCTMEASURE:
                                case NavigationalParameters.AppModes.CABLEMEASURE:
                                case NavigationalParameters.AppModes.REINSTATEMENTMEASURE:
                                case NavigationalParameters.AppModes.CIVILSMEASURE:
                                case NavigationalParameters.AppModes.LATERALMEASURE:
                                case NavigationalParameters.AppModes.POLECABLEMEASURE:
                                case NavigationalParameters.AppModes.LEADINMEASURE:
                                case NavigationalParameters.AppModes.UGCRPMEASURE:
                                case NavigationalParameters.AppModes.SITESUPPORTMEASURE:
                                    Reason = Photo.PictureReason = NavigationalParameters.ClaimFile?.ClaimHeader;
                                    Photo.QNumber =
                                        Convert.ToInt64(NavigationalParameters.CurrentSelectedJob?.QuoteNumber ??
                                                        NavigationalParameters.CurrentAssignment?.Qnumber);

                                    Photo.Category = NavigationalParameters.SurveyType.ToString().ToUpper();

                                    Photo.Identifier = NavigationalParameters.ClaimFile.ClaimId;

                                    if (NavigationalParameters.SelectedQuestion != null)
                                    {
                                        Photo.QuestionId = NavigationalParameters.SelectedQuestion?.QuestionId.ToString();
                                        ShowButton = false;
                                        NavigationalParameters.SelectedQuestion.QuestionTextColour = Color.DarkGreen;
                                        NavigationalParameters.PreSiteQuestions.FirstOrDefault(x =>
                                                x.QuestionId == NavigationalParameters.SelectedQuestion?.QuestionId)
                                            .QuestionTextColour = Color.DarkGreen;
                                    }

                                    await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);

                                    await _picService.AddPicture(Photo);

                                    await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                                    break;
                                default:
                                    ShowButton = true;
                                    Reason = Photo.PictureReason = "End of Day";
                                    break;
                            }
                        }
                        if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.DFE)
                            Photo.Identifier = NavigationalParameters.CurrentDfe.DfeId;

                        if (NavigationalParameters.SelectedAnswer != null)
                            Photo.StreetName = NavigationalParameters.SelectedAnswer?.StreetName;

                        ShowPlaceHolder = false;
                        ShowImage = true;
                        ShowSubmitButtons = true;
                        Source = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                        Photo.Image = imageAsBytes;
                        NavigationalParameters.SelectedPhoto = Photo;


                        if (NavigationalParameters.SelectedQuestion != null &&
                            NavigationalParameters.SelectedQuestion.QuestionId == 0.10M &&
                            NavigationalParameters.AppMode.ToString().ToLower().Contains("asbuilt"))
                            await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                }
                else
                {
                    // No pic
                    await Alert("Warning", "No image has been taken");
                }

                ShowPhotoButton = false;
            }
            catch (Exception ex)
            {
                await Alert("Warning", $"{ex}");
                var error = ex.ToString();
            }
    });

    public RelayCommand DeletePhoto => new(async () =>
    {
        try
        {
            if (Photo?.Image != null || Photo?.SeverPictureId == 0)
            {
                await _picService.DeleteJobPicture(Photo);

                await _ls.RemoveImageFromLocalStorage("JobPictures", Photo?.PicturePath.Split('/').Last());

                if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.PLANTISSUE)
                {
                    if (NavigationalParameters.CurrentPlantIssue?.Picture1Filename == Photo.FileName)
                        NavigationalParameters.CurrentPlantIssue.Picture1Filename = null;
                    else if (NavigationalParameters.CurrentPlantIssue?.Picture2Filename == Photo.FileName)
                        NavigationalParameters.CurrentPlantIssue.Picture2Filename = null;
                    else if (NavigationalParameters.CurrentPlantIssue?.Picture3Filename == Photo.FileName)
                        NavigationalParameters.CurrentPlantIssue.Picture3Filename = null;
                    else if (NavigationalParameters.CurrentPlantIssue?.Picture4Filename == Photo.FileName)
                        NavigationalParameters.CurrentPlantIssue.Picture4Filename = null;
                }

                Route = Color.LightGray;
                Obstruction = Color.LightGray;
                Condition = Color.LightGray;
                Incident = Color.LightGray;
                PIA = Color.LightGray;
                Hse = Color.LightGray;
                General = Color.LightGray;
                SiteClear = Color.LightGray;
                TrialHole = Color.LightGray;
                TrenchDepth = Color.LightGray;
                BackFillDepth = Color.LightGray;
                Chamber = Color.LightGray;

                GoBack.Execute(null);
            }
            else
            {
                GoBack.Execute(null);
            }
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
            ShowPhotoButton = false;

            if (Photo.Image != null && !string.IsNullOrEmpty(Reason))
            {
                Photo.PictureReason = Reason;

                Photo.PictureComments += Comments;

                if (NavigationalParameters.CurrentSelectedJob != null)
                {
                    Photo.JobId = NavigationalParameters.CurrentSelectedJob.JobId;
                }
                else
                {
                    if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.INVESTIGATION)
                    {
                        var damage = NavigationalParameters.InvestigateDamage?.DamageDetails
                            ?.FirstOrDefault();

                        if (damage != null)
                        {
                            NavigationalParameters.CurrentSelectedJob = _jobService
                                .GetAllJobs()?
                                .FirstOrDefault(x => x.QuoteNumber == Convert.ToInt64(damage.QNumber)
                                                     && x.GangLeaderId == damage?.GangLeaderId
                                                     && x.JobDate.Date == damage?.IncidentDateTime.Date);

                            if (NavigationalParameters.CurrentSelectedJob != null)
                                Photo.JobId = NavigationalParameters.CurrentSelectedJob.JobId;
                        }
                    }
                }

                if (Reason.ToLower() == "PoleLocationImage")
                {
                    var PhotosToDelete = _picService.GetAllPictures().ToList();

                    var p2 = PhotosToDelete?.Where(x => x.QNumber == Photo?.QNumber
                                                        && x.PictureReason == "PoleLocationImage"
                                                        && x.StreetName == NavigationalParameters.CurrentAssignment
                                                            ?.StreetName).ToList();

                    foreach (var item in p2) await _picService.DeleteJobPicture(item);
                }

                await _picService.AddPicture(Photo);

                switch (NavigationalParameters.PhotoMode)
                {
                    case NavigationalParameters.PhotoModes.RISKASSESMENT:
                    case NavigationalParameters.PhotoModes.PLANT:
                        Photo.PictureReason = "Checks";
                        NavigationalParameters.CurrentCheckAnswer.PictureFileName = Photo?.FileName;
                        App.Database.SaveItem(NavigationalParameters.CurrentCheckAnswer);
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);                     
                        // ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.NCRCORRECTIVEACTIONS:
                        Photo.PictureReason = "NcrCorrectiveActions";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        // ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.NCRAUDITDETAILS:
                        Photo.PictureReason = "NCRAuditDetails";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        //  ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.NCRINTERMEDIATEACTIONS:
                        Photo.PictureReason = "NCRIntermediateActions";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        // ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.EVENTMANAGEMENT:
                        //Photo.PictureReason = "EventManagement";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        //  ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.CVI:
                        Photo.PictureReason = "CVI";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        // ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.PLANTISSUE:
                        Photo.PictureReason = "PlantIssue";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.PLANTTRANSFERIN:
                        Photo.PictureReason = "PlantTransferIn";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.PLANTTRANSFEROUT:
                        Photo.PictureReason = "PlantTransferOut";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        //    ShowPhotoButton = true;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.CHAMBERAUDIT:
                        Photo.PictureReason = "ChamberAudit";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        //    ShowPhotoButton = false;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.POLESURVEY:
                        Photo.PictureReason = "PreSitePoleSurvey";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        //     ShowPhotoButton = false;
                        GoBack.Execute(null);
                        break;
                    case NavigationalParameters.PhotoModes.INVOICE:
                        Photo.PictureReason = "Invoice";
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        NavigationalParameters.Order.ImageFileName = Photo?.FileName;
                        _jobService.SaveOrder(NavigationalParameters.Order);
                        var OrderNumber = await _jobService.UpdateOrderBookAsync(NavigationalParameters.Order);
                        if (OrderNumber > 0)
                        {
                            Photo = null;

                            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER
                                || NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                                await NavigateTo(ViewModelLocator.MainListPage);
                            else
                                await NavigateTo(ViewModelLocator.SupervisorProjectPage);
                        }

                        break;
                    case NavigationalParameters.PhotoModes.ASBUILT:
                    case NavigationalParameters.PhotoModes.CABLEMEASURE:
                    case NavigationalParameters.PhotoModes.CHAMBERMEASURE:
                    case NavigationalParameters.PhotoModes.DPASBUILT:
                    case NavigationalParameters.PhotoModes.DUCTMEASURE:
                    case NavigationalParameters.PhotoModes.POLECABLEMEASURE:
                    case NavigationalParameters.PhotoModes.POLEMEASURE:
                    case NavigationalParameters.PhotoModes.UGCRPMEASURE:
                    {
                        Photo.PictureReason = NavigationalParameters.PhotoMode.ToString().ToUpper();
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);
                        await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
                    }
                        break;
                    default:
                        await _ls.StoreImagesLocallyAndUpdatePath("JobPictures/", Photo?.FileName, Photo?.Image);

                        if (NavigationalParameters.PhotoMode != NavigationalParameters.PhotoModes.ASBUILT)
                            GoBack.Execute(null);

                        break;
                }

                Route = Color.LightGray;
                Obstruction = Color.LightGray;
                Condition = Color.LightGray;
                Incident = Color.LightGray;
                PIA = Color.LightGray;
                Hse = Color.LightGray;
                General = Color.LightGray;
                SiteClear = Color.LightGray;
                TrialHole = Color.LightGray;
                TrenchDepth = Color.LightGray;
                BackFillDepth = Color.LightGray;
                Chamber = Color.LightGray;


                Comments = "";
                Photo = NavigationalParameters.SelectedPhoto = null;

                //ShowPhotoButton = true;
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

    public RelayCommand<string> GoBack => new(async e =>
    {
        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
        {
            NavigateBack();
        } else {
            if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.INVESTIGATION)
                await NavigateTo(ViewModelLocator.MethodologyPage);
            else if (NavigationalParameters.PhotoMode == NavigationalParameters.PhotoModes.ASBUILT)
                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
            else
                await NavigateTo(ViewModelLocator.ProjectImagesPage);
        }
    });

    public string _reason { get; private set; }

    //private readonly Photostate _photoState = Photostate.NEW;
    public PhotoSelectionPageViewModel()
    {
        _picService = new Pictures();
        _ls = new LocalStorage();
        _jobService = new Jobs();
    }

    private enum Photostate
    {
        NEW,
        EDIT
    }
}