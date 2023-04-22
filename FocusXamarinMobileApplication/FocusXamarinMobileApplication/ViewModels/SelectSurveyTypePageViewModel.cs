using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class SelectSurveyTypePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;

    private Jobs _jobService;

    public SelectSurveyTypePageViewModel()
    {
        _jobService = new Jobs();

        _assignmentService = new Assignments();
    }

    public ObservableCollection<SurveyQuestion> Questions { get; set; }

    private int _savedDamageCount { get; set; }

    public int SavedDamageCount
    {
        get => _savedDamageCount;
        set
        {
            _savedDamageCount = value;
            OnPropertyChanged();
        }
    }

    private string _projectName { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    private string _projectDate { get; set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }


    private bool _chamberSurveyVisibility { get; set; }

    public bool ChamberSurveyVisibility
    {
        get => _chamberSurveyVisibility;
        set
        {
            _chamberSurveyVisibility = value;
            OnPropertyChanged();
        }
    }

    private bool _preSitePremisesVisibility { get; set; }

    public bool PreSitePremisesVisibility
    {
        get => _preSitePremisesVisibility;
        set
        {
            _preSitePremisesVisibility = value;
            OnPropertyChanged("PreSitePremisesVisibility");
        }
    }
    private bool _preSiteChamberButtonsVisible { get; set; }

    public bool PreSiteChamberButtonsVisible
    {
        get => _preSiteChamberButtonsVisible;
        set
        {
            _preSiteChamberButtonsVisible = value;
            OnPropertyChanged("PreSiteChamberButtonsVisible");
        }
    }

    private bool _preSiteChamberPiaButtonsVisible { get; set; }

    public bool PreSitePiaChamberButtonsVisible
    {
        get => _preSiteChamberPiaButtonsVisible;
        set
        {
            _preSiteChamberPiaButtonsVisible = value;
            OnPropertyChanged("PreSitePiaChamberButtonsVisible");
        }
    }

    private bool _preSiteDuctButtonsVisible { get; set; }

    public bool PreSiteDuctButtonsVisible
    {
        get => _preSiteDuctButtonsVisible;
        set
        {
            _preSiteDuctButtonsVisible = value;
            OnPropertyChanged("PreSiteDuctButtonsVisible");
        }
    }

    private bool _preSitePiaDuctButtonsVisible { get; set; }

    public bool PreSitePiaDuctButtonsVisible
    {
        get => _preSitePiaDuctButtonsVisible;
        set
        {
            _preSitePiaDuctButtonsVisible = value;
            OnPropertyChanged("PreSitePiaDuctButtonsVisible");
        }
    }


    private bool _sedVisibility { get; set; }

    public bool SEDVisibility
    {
        get => _sedVisibility;
        set
        {
            _sedVisibility = value;
            OnPropertyChanged();
        }
    }


    private bool _preSitePoleButtonsVisible { get; set; }

    public bool PreSitePoleButtonsVisible
    {
        get => _preSitePoleButtonsVisible;
        set
        {
            _preSitePoleButtonsVisible = value;
            OnPropertyChanged("PreSitePoleButtonsVisible");
        }
    }

    private bool _preSitePiaPoleButtonsVisible { get; set; }

    public bool PreSitePiaPoleButtonsVisible
    {
        get => _preSitePiaPoleButtonsVisible;
        set
        {
            _preSitePiaPoleButtonsVisible = value;
            OnPropertyChanged("PreSitePiaPoleButtonsVisible");
        }
    }

    private bool _connexinAsBuiltButtonsVisible { get; set; }

    public bool ConnexinAsBuiltButtonsVisible
    {
        get => _connexinAsBuiltButtonsVisible;
        set
        {
            _connexinAsBuiltButtonsVisible = value;
            OnPropertyChanged();
        }
    }

    private bool _poleAsBuiltButtonVisible { get; set; }

    public bool PoleAsBuiltButtonVisible
    {
        get => _poleAsBuiltButtonVisible;
        set
        {
            _poleAsBuiltButtonVisible = value;
            OnPropertyChanged();
        }
    }

    private bool _internalVisibility { get; set; }

    public bool InternalVisibility
    {
        get => _internalVisibility;
        set
        {
            _internalVisibility = value;
            OnPropertyChanged();
        }
    }

    private bool _civilsVisibility { get; set; }

    public bool CivilsVisibility
    {
        get => _civilsVisibility;
        set
        {
            _civilsVisibility = value;
            OnPropertyChanged();
        }
    }

    public bool _permitToDigButtonsVisible { get; set; }

    public bool PermitToDigButtonsVisible
    {
        get => _permitToDigButtonsVisible;
        set
        {
            _permitToDigButtonsVisible = value;
            OnPropertyChanged();
        }
    }


    public bool _clearStreetButtonVisible { get; set; }

    public bool ClearStreetButtonVisible
    {
        get => _clearStreetButtonVisible;
        set
        {
            _clearStreetButtonVisible = value;
            OnPropertyChanged();
        }
    }


    public bool _chamberAsBuiltButtonVisible { get; set; }

    public bool ChamberAsBuiltButtonVisible
    {
        get => _chamberAsBuiltButtonVisible;
        set
        {
            _chamberAsBuiltButtonVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _dpAsBuiltButtonVisible { get; set; }

    public bool DpAsBuiltButtonVisible
    {
        get => _dpAsBuiltButtonVisible;
        set
        {
            _dpAsBuiltButtonVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _dJEAsBuiltButtonVisible { get; set; }

    public bool DJEAsBuiltButtonVisible
    {
        get => _dJEAsBuiltButtonVisible;
        set
        {
            _dJEAsBuiltButtonVisible = value;
            OnPropertyChanged();
        }
    }


    public bool _fJEAsBuiltButtonVisible { get; set; }

    public bool FJEAsBuiltButtonVisible
    {
        get => _fJEAsBuiltButtonVisible;
        set
        {
            _fJEAsBuiltButtonVisible = value;
            OnPropertyChanged();
        }
    }


    public bool _bJEAsBuiltButtonVisible { get; set; }

    public bool BJEAsBuiltButtonVisible
    {
        get => _bJEAsBuiltButtonVisible;
        set
        {
            _bJEAsBuiltButtonVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _remedialsButtonVisible { get; set; }

    public bool RemedialsButtonVisible
    {
        get => _remedialsButtonVisible;
        set
        {
            _remedialsButtonVisible = value;
            OnPropertyChanged();
        }
    }


    public bool _vertishorePermitToDigButtonsVisible { get; set; }

    public bool VertishorePermitToDigButtonsVisible
    {
        get => _vertishorePermitToDigButtonsVisible;
        set
        {
            _vertishorePermitToDigButtonsVisible = value;
            OnPropertyChanged();
        }
    }

    public bool _showSubmissionButton { get; set; }

    public bool ShowSubmissionButton
    {
        get => _showSubmissionButton;
        set
        {
            _showSubmissionButton = value;
            OnPropertyChanged();
        }
    }

    private bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged("ShowSubmissionButton");
        }
    }

    public RelayCommand PageLoad => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        ShowSubmissionButton = SavedDamageCount > 0 && IsLoading == false;
        PermitToDigButtonsVisible = false;
        VertishorePermitToDigButtonsVisible = false;
        SEDVisibility = false;
        ChamberSurveyVisibility = false;
        PreSitePremisesVisibility = false;
        CivilsVisibility = false;
        InternalVisibility = false;
        PreSitePoleButtonsVisible = false;
        PreSitePiaPoleButtonsVisible = false;
        PreSiteChamberButtonsVisible = false;
        PreSitePiaChamberButtonsVisible = false;
        PreSiteDuctButtonsVisible = false;
        PreSitePiaDuctButtonsVisible = false;
        ConnexinAsBuiltButtonsVisible = false;
        ClearStreetButtonVisible = false;
        ChamberAsBuiltButtonVisible = false;
        DpAsBuiltButtonVisible = false;
        DJEAsBuiltButtonVisible = false;
        FJEAsBuiltButtonVisible = false;
        RemedialsButtonVisible = false;
        BJEAsBuiltButtonVisible = false;
        PoleAsBuiltButtonVisible = false;

        switch (NavigationalParameters.AppMode)
        {
            case NavigationalParameters.AppModes.PERMITTODIG:
                PermitToDigButtonsVisible = true;
                VertishorePermitToDigButtonsVisible = true;
                break;
            case NavigationalParameters.AppModes.CHAMBERAUDIT:
                NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.CHAMBERAUDIT;

                NavigationalParameters.GenericQuestions =
                    new ObservableCollection<SurveyQuestion>(_assignmentService.GetSurveyQuestions("chamber"));

                await NavigateTo(ViewModelLocator.SelectStreetPage);
                break;
            case NavigationalParameters.AppModes.ASBUILT:
            case NavigationalParameters.AppModes.CHAMBERASBUILT:
            case NavigationalParameters.AppModes.DPASBUILT:
            case NavigationalParameters.AppModes.BJEASBUILT:
            case NavigationalParameters.AppModes.DJEASBUILT:
            case NavigationalParameters.AppModes.POLEASBUILT:
            case NavigationalParameters.AppModes.FJEASBUILT:
                PoleAsBuiltButtonVisible = true;
                ChamberAsBuiltButtonVisible = true;
                DpAsBuiltButtonVisible = true;
                DJEAsBuiltButtonVisible = true;
                FJEAsBuiltButtonVisible = true;
                BJEAsBuiltButtonVisible = true;
                break;
            case NavigationalParameters.AppModes.SITECLEAR:
                ClearStreetButtonVisible = true;
                break;
            default:
                SEDVisibility = _assignmentService.GetSurveyQuestions("SED").Any();
                PreSitePremisesVisibility = _assignmentService.GetSurveyQuestions("PreSitePremises").Any();
                CivilsVisibility = _assignmentService.GetSurveyQuestions("PreSiteCivils").Any();
                PreSitePoleButtonsVisible = _assignmentService.GetSurveyQuestions("PreSitePoleSurvey").Any();
                PreSitePiaPoleButtonsVisible = _assignmentService.GetSurveyQuestions("PreSitePiaPoleSurvey").Any();
                PreSiteChamberButtonsVisible = _assignmentService.GetSurveyQuestions("PreSiteChamberSurvey").Any();
                PreSitePiaChamberButtonsVisible = _assignmentService.GetSurveyQuestions("PreSitePiaChamber").Any();
                PreSiteDuctButtonsVisible = _assignmentService.GetSurveyQuestions("PreSiteDuctSurvey").Any();
                PreSitePiaDuctButtonsVisible = _assignmentService.GetSurveyQuestions("PreSitePiaDuctSurvey").Any();
                //await NavigateTo(ViewModelLocator.SelectStreetPage);
                break;
        }
    });

    public RelayCommand ChamberAuditCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "chamber"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.CHAMBERAUDIT;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.CHAMBERAUDIT;
            NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
                _assignmentService.GetSurveyQuestions("chamberaudit").Select(question => new YesNoNaQuestionViewModel
                {
                    BtnNaBgColour = Color.LightGray,
                    BtnYesBgColour = Color.LightGray,
                    BtnNoBgColour = Color.LightGray,
                    BtnNaText = question.QuestionOptions.Split(',')[2],
                    BtnNoText = question.QuestionOptions.Split(',')[1],
                    BtnYesText = question.QuestionOptions.Split(',')[0],
                    Question = question.Question,
                    FurtherQuestionId = question.FurtherQuestionId,
                    QuestionOptions = question.QuestionOptions,
                    Category = question.Category,
                    DepthorRating = question.DepthorRating,
                    FollowUpAction = question.FollowUpAction,
                    Grouping = question.Grouping,
                    InformationTo = question.InformationTo,
                    KeyAnswer = question.KeyAnswer,
                    NotifiableResponse = question.NotifiableResponse,
                    QuestionId = question.QuestionId,
                    Stage = question.Stage,
                    ResponseType = question.ResponseType,
                    Id = question.Id,
                    IsEnabled = true,
                    ShowButtonA = question.QuestionId == 0.10M ? false : true,
                    ShowButtonB = question.QuestionId == 0.10M ? false : true,
                    ShowButtonC = question.QuestionId == 0.10M ? false : true
                }));

            await NavigateTo(ViewModelLocator.SelectStreetPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    //public RelayCommand PolePiaCommand => new(async () =>
    //{
    //    if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
    //            x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
    //            x.BuildingStandard.ToLower() == "Pole"))
    //    {
    //        NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY;
    //        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPIAPOLESURVEY;
    //        NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
    //            _assignmentService.GetSurveyQuestions("PRESITEPIAPOLESURVEY").Select(question => new YesNoNaQuestionViewModel
    //            {
    //                BtnNaBgColour = Color.LightGray,
    //                BtnYesBgColour = Color.LightGray,
    //                BtnNoBgColour = Color.LightGray,
    //                BtnNaText = question.QuestionOptions.Split(',')[2],
    //                BtnNoText = question.QuestionOptions.Split(',')[1],
    //                BtnYesText = question.QuestionOptions.Split(',')[0],
    //                Question = question.Question,
    //                FurtherQuestionId = question.FurtherQuestionId,
    //                QuestionOptions = question.QuestionOptions,
    //                Category = question.Category,
    //                DepthorRating = question.DepthorRating,
    //                FollowUpAction = question.FollowUpAction,
    //                Grouping = question.Grouping,
    //                InformationTo = question.InformationTo,
    //                KeyAnswer = question.KeyAnswer,
    //                NotifiableResponse = question.NotifiableResponse,
    //                QuestionId = question.QuestionId,
    //                Stage = question.Stage,
    //                ResponseType = question.ResponseType,
    //                Id = question.Id,
    //                IsEnabled = true,
    //                ShowButtonA = question.QuestionId == 0.10M ? false : true,
    //                ShowButtonB = question.QuestionId == 0.10M ? false : true,
    //                ShowButtonC = question.QuestionId == 0.10M ? false : true
    //            }));

    //        await NavigateTo(ViewModelLocator.SelectStreetPage);
    //    }
    //    else
    //    {
    //        await Alert("There are no assets available for selection. please contact support for furthur assistance",
    //            "No assets available", "Ok");
    //    }
    //});

    public RelayCommand ConnexinAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "street"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.CHAMBERAUDIT;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.CONNEXINASBUILT;
            NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
                _assignmentService.GetSurveyQuestions("CONNEXINASBUILT").Select(question => new YesNoNaQuestionViewModel
                {
                    BtnNaBgColour = Color.LightGray,
                    BtnYesBgColour = Color.LightGray,
                    BtnNoBgColour = Color.LightGray,
                    BtnNaText = question.QuestionOptions.Split(',')[2],
                    BtnNoText = question.QuestionOptions.Split(',')[1],
                    BtnYesText = question.QuestionOptions.Split(',')[0],
                    Question = question.Question,
                    FurtherQuestionId = question.FurtherQuestionId,
                    QuestionOptions = question.QuestionOptions,
                    Category = question.Category,
                    DepthorRating = question.DepthorRating,
                    FollowUpAction = question.FollowUpAction,
                    Grouping = question.Grouping,
                    InformationTo = question.InformationTo,
                    KeyAnswer = question.KeyAnswer,
                    NotifiableResponse = question.NotifiableResponse,
                    QuestionId = question.QuestionId,
                    Stage = question.Stage,
                    ResponseType = question.ResponseType,
                    Id = question.Id,
                    IsEnabled = true,
                    ShowButtonA = question.QuestionId == 0.10M ? false : true,
                    ShowButtonB = question.QuestionId == 0.10M ? false : true,
                    ShowButtonC = question.QuestionId == 0.10M ? false : true
                }));

            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand PreSitePoleCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "pole"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITEPOLESURVEY;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY;
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.POLESURVEY;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand PreSitePremisesCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "premises"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITEPREMISIS;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPREMISES;

            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PRESITEPREMISES;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assests available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });
    //----------------------------- new surveys -----------------------------------
    public RelayCommand PreSitePiaPoleCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "pole"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITEPIAPOLESURVEY;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEPIAPOLESURVEY;

            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PRESITEPIAPOLESURVEY;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand PreSiteChamberCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "chamber"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITECHAMBERSURVEY;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITECHAMBERSURVEY;
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PRESITECHAMBERSURVEY;

            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand PreSiteDuctCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "duct"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITEDUCTSURVEY;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEDUCTSURVEY;
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PRESITEDUCTSURVEY;

            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand PreSitePiaDuctCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "duct"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITEDUCTPIASURVEY;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITEDUCTPIASURVEY;
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PRESITEDUCTPIASURVEY;

            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand PreSitePiaChamberCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "duct"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITECHAMBERPIASURVEY;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITECHAMBERPIASURVEY;
            NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.PRESITECHAMBERPIASURVEY;

            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    //----------------------------- new surveys End -----------------------------------
    public RelayCommand ClearStreetCommand => new(async () =>
    {
        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.SITECLEAR;

        //NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(_assignmentService.GetSurveyQuestions("siteclear").Select(question => new YesNoNaQuestionViewModel
        //{
        //    BtnNaBgColour = Color.LightGray,
        //    BtnYesBgColour = Color.LightGray,
        //    BtnNoBgColour = Color.LightGray,
        //    BtnNaText = question.QuestionOptions?.Split(',')[2],
        //    BtnNoText = question.QuestionOptions?.Split(',')[1],
        //    BtnYesText = question.QuestionOptions?.Split(',')[0],
        //    Question = question.Question,
        //    FurtherQuestionId = question.FurtherQuestionId,
        //    QuestionOptions = question.QuestionOptions,
        //    Category = question.Category,
        //    DepthorRating = question.DepthorRating,
        //    FollowUpAction = question.FollowUpAction,
        //    Grouping = question.Grouping,
        //    InformationTo = question.InformationTo,
        //    KeyAnswer = question.KeyAnswer,
        //    NotifiableResponse = question.NotifiableResponse,
        //    QuestionId = question.QuestionId,
        //    Stage = question.Stage,
        //    ResponseType = question.ResponseType,
        //    Id = question.Id,
        //    IsEnabled = true,
        //    ShowButtonA = question.QuestionId == 0.10M ? false : true,
        //    ShowButtonB = question.QuestionId == 0.10M ? false : true,
        //    ShowButtonC = question.QuestionId == 0.10M ? false : true
        //}));

        //await NavigateTo(ViewModelLocator.SelectStreetPage);
    });

    public RelayCommand PoleAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "pole"))
        {
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.POLEASBUILT;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.POLEASBUILT;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand ChamberAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "chamber"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.CHAMBERASBUILT;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.CHAMBERASBUILT;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand DJEAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "distributionjointenclosure"))
        {
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DJEASBUILT;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.DJEASBUILT;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand DPAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "distributionpoint"))
        {
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.DPASBUILT;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.DPASBUILT;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand BJEAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "backhauljointenclosure"))
        {
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.BJEASBUILT;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.BJEASBUILT;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand RemedialsCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "remedials"))
        {
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.REMEDIAL;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.REMEDIAL;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are remedial works available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand FJEAsBuiltCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "fibrejointenclosure"))
        {
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.FJEASBUILT;
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.FJEASBUILT;
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        }
        else
        {
            await Alert("There are no assets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand SED => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITE;
        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.SED;

        await NavigateTo(ViewModelLocator.SelectStreetPage);
    });

    public RelayCommand Internal => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITE;
        NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.INTERNAL;

        await NavigateTo(ViewModelLocator.SelectStreetPage);
    });

  

    public RelayCommand CivilEngineerimg => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "street"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PRESITECIVILS;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PRESITECIVILS;
            NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.PROJECTLIST;

            await NavigateTo(ViewModelLocator.SelectStreetPage);
        }
        else
        {
            await Alert("There are no streets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand DigPermit => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "street"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PERMITTODIG;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.PERMITTODIG;
            NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
                _assignmentService.GetSurveyQuestions("permittodig").Select(question => new YesNoNaQuestionViewModel
                {
                    BtnNaBgColour = Color.LightGray,
                    BtnYesBgColour = Color.LightGray,
                    BtnNoBgColour = Color.LightGray,
                    BtnNaText = question.QuestionOptions.Split(',')[2],
                    BtnNoText = question.QuestionOptions.Split(',')[1],
                    BtnYesText = question.QuestionOptions.Split(',')[0],
                    Question = question.Question,
                    FurtherQuestionId = question.FurtherQuestionId,
                    QuestionOptions = question.QuestionOptions,
                    Category = question.Category,
                    DepthorRating = question.DepthorRating,
                    FollowUpAction = question.FollowUpAction,
                    Grouping = question.Grouping,
                    InformationTo = question.InformationTo,
                    KeyAnswer = question.KeyAnswer,
                    NotifiableResponse = question.NotifiableResponse,
                    QuestionId = question.QuestionId,
                    Stage = question.Stage,
                    ResponseType = question.ResponseType,
                    Id = question.Id,
                    IsEnabled = true,
                    ShowButtonA = question.QuestionId == 0.10M ? false : true,
                    ShowButtonB = question.QuestionId == 0.10M ? false : true,
                    ShowButtonC = question.QuestionId == 0.10M ? false : true
                }));

            await NavigateTo(ViewModelLocator.SelectStreetPage);
        }
        else
        {
            await Alert("There are no streets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand VertishorePermitCommand => new(async () =>
    {
        if (App.Database.GetItems<VMexpansionReleaseData>().Any(x =>
                x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber &&
                x.BuildingStandard.ToLower() == "street"))
        {
            NavigationalParameters.AppMode = NavigationalParameters.AppModes.PERMITTODIG;
            NavigationalParameters.SurveyType = NavigationalParameters.SurveyTypes.VERTISHOREPERMIT;
            NavigationalParameters.YesNoCollection = new ObservableCollection<YesNoNaQuestionViewModel>(
                _assignmentService.GetSurveyQuestions("vertishorepermit").Select(question =>
                    new YesNoNaQuestionViewModel
                    {
                        BtnNaBgColour = Color.LightGray,
                        BtnYesBgColour = Color.LightGray,
                        BtnNoBgColour = Color.LightGray,
                        BtnNaText = question.QuestionOptions.Split(',')[2],
                        BtnNoText = question.QuestionOptions.Split(',')[1],
                        BtnYesText = question.QuestionOptions.Split(',')[0],
                        Question = question.Question,
                        FurtherQuestionId = question.FurtherQuestionId,
                        QuestionOptions = question.QuestionOptions,
                        Category = question.Category,
                        DepthorRating = question.DepthorRating,
                        FollowUpAction = question.FollowUpAction,
                        Grouping = question.Grouping,
                        InformationTo = question.InformationTo,
                        KeyAnswer = question.KeyAnswer,
                        NotifiableResponse = question.NotifiableResponse,
                        QuestionId = question.QuestionId,
                        Stage = question.Stage,
                        ResponseType = question.ResponseType,
                        Id = question.Id,
                        IsEnabled = true,
                        ShowButtonA = question.QuestionId == 0.10M ? false : true,
                        ShowButtonB = question.QuestionId == 0.10M ? false : true,
                        ShowButtonC = question.QuestionId == 0.10M ? false : true
                    }));

            await NavigateTo(ViewModelLocator.SelectStreetPage);
        }
        else
        {
            await Alert("There are no streets available for selection. please contact support for furthur assistance",
                "No assets available", "Ok");
        }
    });

    public RelayCommand Refresh => new(() =>
    {
        OnPropertyChanged("SavedDamageCount");
        OnPropertyChanged("ShowSubmissionButton");
    });

    public RelayCommand GoBack => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    public SurveyAnswers SelectedAnswer { get; set; }
}