namespace FocusXamarinMobileApplication.ViewModels;

public class RatingSurveyQuestionsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Assignments _assignmentService;
    public readonly Jobs _jobService;
    public readonly PhotoSelectionPageViewModel _psvm;
    public readonly Users _userService;
    private bool _isLoading;

    private bool _showSubmissionButton;

    private string _streetName;

    private List<SurveyAnswers> _surveyAnswers;
    private ImageSource Rating1;
    private ImageSource Rating10;
    private ImageSource Rating2;
    private ImageSource Rating3;
    private ImageSource Rating4;
    private ImageSource Rating5;
    private ImageSource Rating6;
    private ImageSource Rating7;
    private ImageSource Rating8;
    private ImageSource Rating9;
    private ImageSource RatingNA;

    public RatingSurveyQuestionsPageViewModel()
    {
        SelectedAnswer = null;

        _assignmentService = new Assignments();

        _userService = new Users();

        _jobService = new Jobs();

        _assignmentService = new Assignments();

        _psvm = new PhotoSelectionPageViewModel();
    }

    public string _projectDate { get; set; } =
        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    public string[] Options { get; set; }

    // public List<SurveyAnswers> Answers { get; set; }
    public List<IGrouping<decimal, SurveyAnswers>> tempAnswers { get; set; }

    public decimal TotalSectionTotal1 { get; set; }
    public decimal TotalSectionScore1 { get; set; }
    public decimal TotalSectionTotal2 { get; set; }
    public decimal TotalSectionScore2 { get; set; }
    public decimal TotalSectionTotal3 { get; set; }
    public decimal TotalSectionScore3 { get; set; }
    public decimal TotalSectionTotal4 { get; set; }
    public decimal TotalSectionScore4 { get; set; }
    public decimal TotalSectionScore5 { get; set; }
    public decimal TotalSectionTotal5 { get; set; }
    public decimal TotalSectionTotal6 { get; set; }
    public decimal TotalSectionScore6 { get; set; }
    public decimal TotalSectionTotal7 { get; set; }
    public decimal TotalSectionScore7 { get; set; }
    public decimal TotalSectionTotal8 { get; set; }
    public decimal TotalSectionScore8 { get; set; }
    public decimal TotalSectionTotal9 { get; set; }
    public decimal TotalSectionScore9 { get; set; }
    public decimal TotalSectionTotal10 { get; set; }
    public decimal TotalSectionScore10 { get; set; }

    public decimal TotalSection1Percentage { get; set; }
    public decimal TotalSection2Percentage { get; set; }
    public decimal TotalSection3Percentage { get; set; }
    public decimal TotalSection4Percentage { get; set; }
    public decimal TotalSection5Percentage { get; set; }
    public decimal TotalSection6Percentage { get; set; }
    public decimal TotalSection7Percentage { get; set; }
    public decimal TotalSection8Percentage { get; set; }
    public decimal TotalSection9Percentage { get; set; }

    public List<SurveyAnswers> SurveyAnswers
    {
        get => _surveyAnswers;
        set
        {
            _surveyAnswers = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<RatingQuestionViewModel> _ratingQuestionCollection { get; set; }

    public ObservableCollection<RatingQuestionViewModel> RatingQuestionCollection
    {
        get => _ratingQuestionCollection;
        set
        {
            _ratingQuestionCollection = value;
            OnPropertyChanged();
        }
    }

    private bool _showPage { get; set; }

    public bool ShowPage
    {
        get => _showPage;
        set
        {
            _showPage = value;
            OnPropertyChanged();
        }
    }

    private RatingQuestionViewModel _selectedQuestion { get; set; }

    public RatingQuestionViewModel SelectedQuestion
    {
        get => _selectedQuestion;
        set
        {
            _selectedQuestion = value;
            OnPropertyChanged();
        }
    }

    private SurveyAnswers _selectedAnswer { get; set; }

    public SurveyAnswers SelectedAnswer
    {
        get => _selectedAnswer;
        set
        {
            _selectedAnswer = value;
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

    public ImageSource _documents { get; set; } = SimpleStaticHelpers.GetImage("Documents");

    public ImageSource Documents
    {
        get => _documents;
        set
        {
            _documents = value;
            OnPropertyChanged("DocumentsImage");
        }
    }

    public ImageSource _gpsImage { get; set; } = SimpleStaticHelpers.GetImage("Gps_White");

    public ImageSource GpsImage
    {
        get => _gpsImage;
        set
        {
            _gpsImage = value;
            OnPropertyChanged();
        }
    }


    public bool _showIfNotAudit { get; set; }

    public bool ShowIfNotAudit
    {
        get => _showIfNotAudit;
        set
        {
            _showIfNotAudit = value;
            OnPropertyChanged();
        }
    }


    private Color _buttonAColour { get; set; } = Color.LightGray;
    private Color _buttonBColour { get; set; } = Color.LightGray;
    private Color _buttonCColour { get; set; } = Color.LightGray;
    private Color _buttonDColour { get; set; } = Color.LightGray;
    private Color _buttonEColour { get; set; } = Color.LightGray;
    private Color _buttonFColour { get; set; } = Color.LightGray;
    private Color _buttonGColour { get; set; } = Color.LightGray;
    private Color _buttonHColour { get; set; } = Color.LightGray;

    public string FilteredQuestionBy { get; set; }

    public Color ButtonAColour
    {
        get => _buttonAColour;
        set
        {
            _buttonAColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonBColour
    {
        get => _buttonBColour;
        set
        {
            _buttonBColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonCColour
    {
        get => _buttonCColour;
        set
        {
            _buttonCColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonDColour
    {
        get => _buttonDColour;
        set
        {
            _buttonDColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonEColour
    {
        get => _buttonEColour;
        set
        {
            _buttonEColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonFColour
    {
        get => _buttonFColour;
        set
        {
            _buttonFColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonGColour
    {
        get => _buttonGColour;
        set
        {
            _buttonGColour = value;
            OnPropertyChanged();
        }
    }

    public Color ButtonHColour
    {
        get => _buttonHColour;
        set
        {
            _buttonHColour = value;
            OnPropertyChanged();
        }
    }

    public string StreetName
    {
        get => _streetName;
        set
        {
            _streetName = value;
            OnPropertyChanged();
        }
    }

    public bool ShowSubmissionButton
    {
        get => _showSubmissionButton;
        set
        {
            _showSubmissionButton = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand GoComments => new(async () =>
    {
        try
        {
            NavigationalParameters.SelectedAnswer = SelectedAnswer = SurveyAnswers?.OrderByDescending(x => x.SubmittedDateTime).FirstOrDefault(x =>
                x.QuestionId == SelectedQuestion?.QuestionId
                && x.Category?.ToLower() == SelectedQuestion?.Category.ToLower()
                && x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            if (!string.IsNullOrEmpty(SelectedAnswer?.AnswerGiven))
                await NavigateTo(ViewModelLocator.FormsCommentPage);
            else
                await Alert("YPlease make a selection before proceeding", "No answer selected!");
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand GoQuestionGps => new(async () =>
    {
        try
        {
            NavigationalParameters.SelectedAnswer = SelectedAnswer = SurveyAnswers?.OrderByDescending(x => x.SubmittedDateTime).FirstOrDefault(x =>
                x.QuestionId == SelectedQuestion?.QuestionId
                && x.Category?.ToLower() == SelectedQuestion?.Category?.ToLower()
                && x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            if (!string.IsNullOrEmpty(SelectedAnswer?.AnswerGiven))
            {
                if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PRESITEPOLESURVEY)
                {
                    NavigationalParameters.MapType = "PoleQuestionGps";

                    await NavigateTo(ViewModelLocator.FormsMapPage);
                }
                else
                {
                    NavigationalParameters.MapType = "QuestionGps";
                    await NavigateTo(ViewModelLocator.FormsMapPage);
                }
            }
            else
            {
                await Alert("YPlease make a selection before proceeding", "No answer selected!");
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand GoBack => new(async () =>
    {
        NavigationalParameters.CurrentAudit.EndTime = DateTime.Now;

        await NavigateTo(ViewModelLocator.SiteInspectionResultPage);
    });

    public RelayCommand GoToAllocatedProjects => new(async () =>
    {
        NavigationalParameters.ReturnPage = "SurveyQuestionsViewModelKey";
        NavigationalParameters.ProjectListMode = NavigationalParameters.ProjectListModes.JOBLIST;
        await NavigateTo(ViewModelLocator.MainListPage);
    });

    public RelayCommand GoStartGps => new(async () =>
    {
        if (NavigationalParameters.PreviewMode)
        {
            await Alert("You cannot add a start location whilst in prieview mode", "Prieview Mode!");
        }
        else
        {
            NavigationalParameters.ReturnPage = "SurveyQuestionsViewModelKey";
            NavigationalParameters.MapType = "StartGps";

            await NavigateTo(ViewModelLocator.FormsMapPage);
        }
    });

    public RelayCommand GoToDesign => new(async () => { await NavigateTo(ViewModelLocator.DesignMapPage); });

    public RelayCommand GoEndGps => new(async () =>
    {
        if (NavigationalParameters.PreviewMode)
        {
            await Alert("You cannot add a end location whilst in prieview mode", "Prieview Mode!");
        }
        else
        {
            NavigationalParameters.MapType = "EndGps";
            await NavigateTo(ViewModelLocator.FormsMapPage);
        }
    });

    public RelayCommand GoToMapping => new(async () =>
    {
        NavigationalParameters.MapType = "PreSiteMapping";
        await NavigateTo(ViewModelLocator.DesignMapPage);
    });

    public RelayCommand GoToAddMeasure => new(async () =>
    {
        if (NavigationalParameters.PreviewMode)
            await Alert("You cannot add a measure whilst in prieview mode", "Prieview Mode!");
    });

    public RelayCommand GoPicture => new(async () =>
    {
        try
        {
            NavigationalParameters.SelectedAnswer = SelectedAnswer = SurveyAnswers.OrderByDescending(x => x.SubmittedDateTime).FirstOrDefault(x =>
                x.QuestionId == SelectedQuestion?.QuestionId
                && x.Category.ToLower() == SelectedQuestion?.Category.ToLower()
                && x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId);

            if (!string.IsNullOrEmpty(SelectedAnswer?.AnswerGiven))
            {
                NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.AUDITQUESTIONS;

                _psvm.TakePhoto.Execute(null);
            }
            else
            {
                await Alert("Please make a selection before proceeding", "No answer selected!");
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    });

    public RelayCommand GoToDfe => new(async () =>
    {
        if (NavigationalParameters.PreviewMode)
            await Alert("You cannot add a dfe whilst in prieview mode", "Prieview Mode!");
    });

    public RelayCommand SavePresiteCheck => new(async () =>
    {
        try
        {
            IsLoading = true;

            NavigationalParameters.AuthDetail ??= new AuthorisationDetail();

            NavigationalParameters.CurrentAssignment.CompletedOn = DateTime.Now;

            if (NavigationalParameters.SelectedEndPoint != null &&
                !string.IsNullOrEmpty(NavigationalParameters.SelectedEndPoint?.L4Number))
                NavigationalParameters.CurrentAssignment.LocationName = NavigationalParameters.SelectedEndPoint?.L4Number;

            await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

            NavigationalParameters.AppMode = NavigationalParameters.AppModes.SITEINSPECTION;
            await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
            NavigationalParameters.CurrentAssignment.Audit = NavigationalParameters.CurrentAudit;

            if (SurveyAnswers?.Count() >= RatingQuestionCollection?.Count())
            {
                decimal? totalScore = 0;

                try
                {
                    foreach (var answer in SurveyAnswers)
                    {
                        answer.AnswerGiven = string.IsNullOrEmpty(answer?.AnswerGiven) ? "0" : answer?.AnswerGiven?.Split("-").First();

                        if (answer.QuestionId.ToString().StartsWith("1.", StringComparison.Ordinal))
                        {
                            // decimal? totalSectionTotal1 = 0;
                            TotalSectionTotal1 += answer.Rating;
                            TotalSectionScore1 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection1Percentage = TotalSectionScore1 / TotalSectionTotal1 / 10 * 100;
                        }
                        else if (answer.QuestionId.ToString().StartsWith("2.", StringComparison.Ordinal))
                        {
                            // decimal? totalSectionTotal2 = 0;
                            TotalSectionTotal2 += answer.Rating;
                            TotalSectionScore2 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection2Percentage = TotalSectionScore2 / TotalSectionTotal2 / 10 * 100;
                            //answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("3.", StringComparison.Ordinal))
                        {
                            //decimal? totalSectionTotal3 = 0;
                            TotalSectionTotal3 += answer.Rating;
                            TotalSectionScore3 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection3Percentage = TotalSectionScore3 / TotalSectionTotal3 / 10 * 100;
                            // answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("4.", StringComparison.Ordinal))
                        {
                            ///decimal? totalSectionTotal4 = 0;
                            TotalSectionTotal4 += answer.Rating;
                            TotalSectionScore4 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection4Percentage = TotalSectionScore4 / TotalSectionTotal4 / 10 * 100;
                            //answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("5.", StringComparison.Ordinal))
                        {
                            //   decimal? totalSectionTotal5 = 0;
                            TotalSectionTotal5 += answer.Rating;
                            TotalSectionScore5 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection5Percentage = TotalSectionScore5 / TotalSectionTotal5 / 10 * 100;
                            //answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("6.", StringComparison.Ordinal))
                        {
                            //decimal? totalSectionTotal6 = 0;
                            TotalSectionTotal6 += answer.Rating;
                            TotalSectionScore6 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection6Percentage = TotalSectionScore6 / TotalSectionTotal6 / 10 * 100;
                            //answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("7.", StringComparison.Ordinal))
                        {
                            //decimal? totalSectionTotal7 = 0;
                            TotalSectionTotal7 += answer.Rating;
                            TotalSectionScore7 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection7Percentage = TotalSectionScore7 / TotalSectionTotal7 / 10 * 100;
                            //answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("8.", StringComparison.Ordinal))
                        {
                            //decimal? totalSectionTotal8 = 0;
                            TotalSectionTotal8 += answer.Rating;
                            TotalSectionScore8 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection8Percentage = TotalSectionScore8 / TotalSectionTotal8 / 10 * 100;
                            //answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                        else if (answer.QuestionId.ToString().StartsWith("9.", StringComparison.Ordinal))
                        {
                            //decimal? totalSectionTotal9 = 0;
                            TotalSectionTotal9 += answer.Rating;
                            TotalSectionScore9 +=
                                Convert.ToDecimal(answer?.AnswerGiven == "N/A" ? "10" : answer?.AnswerGiven) *
                                answer.Rating;
                            TotalSection9Percentage = TotalSectionScore9 / TotalSectionTotal9 / 10 * 100;
                            // answer.Rating / 10 * Convert.ToDecimal(answer.AnswerGiven);
                        }
                    }

                    if (TotalSection1Percentage > 0)
                        // var section1Weighting = Convert.ToDecimal(TotalSectionTotal1) / 100;
                        NavigationalParameters.CurrentAudit.Section1 =
                            string.Format("{0:0}", TotalSection1Percentage);

                    if (TotalSection2Percentage > 0)
                        //  var section2Weighting = Convert.ToDecimal(TotalSectionTotal2) / 100;
                        NavigationalParameters.CurrentAudit.Section2 =
                            string.Format("{0:0}", TotalSection2Percentage);

                    if (TotalSection3Percentage > 0)
                        //  var section3Weighting = Convert.ToDecimal(TotalSectionTotal3) / 100;
                        NavigationalParameters.CurrentAudit.Section3 =
                            string.Format("{0:0}", TotalSection3Percentage);

                    if (TotalSection4Percentage > 0)
                        // var section4Weighting = Convert.ToDecimal(TotalSectionTotal4) / 100;
                        NavigationalParameters.CurrentAudit.Section4 =
                            string.Format("{0:0}", TotalSection4Percentage);
                    if (TotalSection5Percentage > 0)
                        //  var section5Weighting = Convert.ToDecimal(TotalSectionTotal5) / 100;
                        NavigationalParameters.CurrentAudit.Section5 =
                            string.Format("{0:0}", TotalSection5Percentage);

                    if (TotalSection6Percentage > 0)
                        //  var section6Weighting = Convert.ToDecimal(TotalSectionTotal6) / 100;
                        NavigationalParameters.CurrentAudit.Section6 =
                            string.Format("{0:0}", TotalSection6Percentage);

                    if (TotalSection7Percentage > 0)
                        //  var section7Weighting = Convert.ToDecimal(TotalSectionTotal7) / 100;
                        NavigationalParameters.CurrentAudit.Section7 =
                            string.Format("{0:0}", TotalSection7Percentage);

                    if (TotalSection8Percentage > 0)
                        //  var section8Weighting = Convert.ToDecimal(TotalSectionTotal8) / 100;
                        NavigationalParameters.CurrentAudit.Section8 =
                            string.Format("{0:0}", TotalSection8Percentage);

                    if (TotalSection9Percentage > 0)
                        // var section9Weighting = Convert.ToDecimal(TotalSectionTotal9) / 100;
                        NavigationalParameters.CurrentAudit.Section9 =
                            string.Format("{0:0}", TotalSection9Percentage);

                    TotalSectionTotal10 = TotalSectionTotal9
                                          + TotalSectionTotal8 + TotalSectionTotal7
                                          + TotalSectionTotal6 + TotalSectionTotal5
                                          + TotalSectionTotal4 + TotalSectionTotal3
                                          + TotalSectionTotal2 + TotalSectionTotal1;

                    TotalSectionScore10 = TotalSectionScore9
                                          + TotalSectionScore8 + TotalSectionScore7
                                          + TotalSectionScore6 + TotalSectionScore5
                                          + TotalSectionScore4 + TotalSectionScore3
                                          + TotalSectionScore2 + TotalSectionScore1;

                    if (TotalSectionTotal10 > 0)
                    {
                        var TotalPercentage = TotalSectionScore10 / TotalSectionTotal10 / 10 * 100;

                        NavigationalParameters.CurrentAudit.Score =
                            string.Format("{0:0}", TotalPercentage);
                    }
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
              $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");


                    await Alert("Error", "Error whilst calculating section scores for audit please contact support");
                }

                try
                {
                    NavigationalParameters.CurrentAudit.EndTime = DateTime.Now;


                    NavigationalParameters.CurrentAudit.AuditTime =
                        $"Time Taken:{NavigationalParameters.CurrentAudit?.EndTime - NavigationalParameters.CurrentAudit?.StartTime}";

                    NavigationalParameters.CurrentAssignment.CompletedOn = DateTime.Now;

                    NavigationalParameters.CurrentAudit.Inadequacies = _assignmentService
                        .GetNcrList(NavigationalParameters.CurrentAudit.AuditId).Count(x => x.CorrectedOnSite == true);
                    NavigationalParameters.CurrentAudit.NonConformancies = _assignmentService
                        .GetNcrList(NavigationalParameters.CurrentAudit.AuditId).Count(x => x.CorrectedOnSite == false);

                    //NavigationalParameters.CurrentAudit.NcrList
                    //    .Count(x => x.CorrectedOnSite == true);

                    //NavigationalParameters.CurrentAudit.NonConformancies =
                    //    NavigationalParameters.CurrentAudit.NcrList
                    //    .Count(x => x.CorrectedOnSite == false);

                    await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);

                    NavigationalParameters.CurrentAssignment.Complete = "true";

                    await _assignmentService.SaveAudit(NavigationalParameters.CurrentAssignment, true);

                    if (NavigationalParameters.CurrentAssignment.Audit.AuditId ==
                        NavigationalParameters.CurrentAudit.AuditId)
                        NavigationalParameters.CurrentAssignment.Audit = NavigationalParameters.CurrentAudit;
                }
                catch (Exception ex)
                {
                    Analytics.TrackEvent(
              $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");


                    await Alert("Error!", "Error saving audit locally. Please contact support!");
                }

                await NavigateTo(ViewModelLocator.SiteInspectionResultPage);
            }
            else
            {

                await Alert("Incomplete!", "All Questions need to be complete before uploading to server");
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await Alert("Error!", "An issue has occured saving the survey. This has been saved");
            IsLoading = false;
        }
    });

    [Time]
    public async Task QuestionRatingCommand(ImageButton button)
    {
        var Score = button.ClassId;

        NavigationalParameters.SelectedQuestion = SelectedQuestion = button.CommandParameter as RatingQuestionViewModel;

        SelectedAnswer = SurveyAnswers.FirstOrDefault(x =>
            x.AssignmentId == NavigationalParameters.CurrentAssignment?.AssignmentId
            && x.QuestionId == SelectedQuestion?.QuestionId);

        if (SelectedAnswer == null)
            SelectedAnswer = new SurveyAnswers
            {
                RemoteTableId = 0,
                QuestionId = (decimal)SelectedQuestion?.QuestionId,
                QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                AnswerGiven = Score,
                Notifiable = SelectedQuestion?.NotifiableResponse,
                StreetName = SelectedQuestion.Category,
                Category = "Audit",
                SubmittedDateTime = DateTime.Now,
                CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                AssignmentId = (Guid)NavigationalParameters.CurrentAssignment?.AssignmentId,
                Rating = SelectedQuestion.DepthorRating
            };

        SelectedAnswer.AnswerGiven = Score;

        SelectedAnswer.Complete = "true";

        SurveyAnswers.Add(SelectedAnswer);
        //  NavigationalParameters.AnswerList.Remove(SelectedAnswer);
        //  NavigationalParameters.AnswerList.Add(SelectedAnswer);
        NavigationalParameters.SelectedAnswer = SelectedAnswer;

        await _assignmentService.AddAssignmentsResponse(SelectedAnswer);

        //if (NavigationalParameters.CurrentAudit != null)
        //{
        //    await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
        //}


        if (Score.ToLower() != "n/a" && Convert.ToInt32(Score) <= 6)
        {
            NavigationalParameters.CurrentNCR = new Ncr();
            NavigationalParameters.CurrentNCR.QuestionId = SelectedAnswer.QuestionId;
            await NavigateTo(ViewModelLocator.SiteInspectionRatingFailurePage);
        }

        UpdateQuestions(SelectedAnswer);
    }

    [Time]
    private void UpdateQuestions(SurveyAnswers SelectedAnswer)
    {
        // SelectedQuestion = ;

        var optionsTemp = RatingQuestionCollection
            ?.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId)?.QuestionOptions.Split(',').ToList();

        //  var keyAnswers = SelectedQuestion?.KeyAnswer.Split(',').ToList();

        // var questionToUpdate = RatingQuestionCollection.FirstOrDefault(x => x.QuestionId == SelectedQuestion.QuestionId);

        if (RatingQuestionCollection?.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId) != null)
        {
            // RatingQuestionCollection?.Remove(SelectedQuestion);

            // RatingQuestionCollection.FirstOrDefault()

            RatingQuestionCollection.FirstOrDefault(x =>
                    x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                    x.QuestionId == SelectedAnswer?.QuestionId).Rating10 =
                Rating10 = SimpleStaticHelpers.GetImage("10grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating9 = SimpleStaticHelpers.GetImage("9grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating8 = SimpleStaticHelpers.GetImage("8grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating7 = SimpleStaticHelpers.GetImage("7grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating6 = SimpleStaticHelpers.GetImage("6orbelowgrey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating5 = SimpleStaticHelpers.GetImage("5grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating4 = SimpleStaticHelpers.GetImage("4grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating3 = SimpleStaticHelpers.GetImage("3grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating2 = SimpleStaticHelpers.GetImage("2grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).Rating1 = SimpleStaticHelpers.GetImage("1grey");
            RatingQuestionCollection.FirstOrDefault(x =>
                x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                x.QuestionId == SelectedAnswer?.QuestionId).RatingNA = SimpleStaticHelpers.GetImage("NAgrey");

            switch (SelectedAnswer.AnswerGiven.ToLower())
            {
                case "1":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating1 = SimpleStaticHelpers.GetImage("1red");
                    //     Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                    break;
                case "2":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating2 = SimpleStaticHelpers.GetImage("2red");
                    //   Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                    break;
                case "3":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating3 = SimpleStaticHelpers.GetImage("3orange");
                    //   Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                    break;
                case "4":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating4 = SimpleStaticHelpers.GetImage("4orange");
                    //   Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                    break;
                case "5":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating5 = SimpleStaticHelpers.GetImage("5yellow");
                    //  Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
                    break;
                case "6":
                    RatingQuestionCollection.FirstOrDefault(x =>
                            x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                            x.QuestionId == SelectedAnswer?.QuestionId).Rating6 =
                        SimpleStaticHelpers.GetImage("6orbelowyellow");
                    break;
                case "7":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating7 = SimpleStaticHelpers.GetImage("7yellow");
                    break;
                case "8":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating8 = SimpleStaticHelpers.GetImage("8green");
                    break;
                case "9":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating9 = SimpleStaticHelpers.GetImage("9green");
                    break;
                case "10":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).Rating10 = SimpleStaticHelpers.GetImage("10green");
                    break;
                case "n/a":
                    RatingQuestionCollection.FirstOrDefault(x =>
                        x.Category.ToLower() == SelectedAnswer?.Category.ToLower() &&
                        x.QuestionId == SelectedAnswer?.QuestionId).RatingNA = SimpleStaticHelpers.GetImage("NAgreen");
                    break;
            }

            //var questionToAdd = new RatingQuestionViewModel
            //{
            //    Question = SelectedQuestion.Question,
            //    QuestionId = SelectedQuestion.QuestionId,
            //    KeyAnswer = SelectedQuestion.KeyAnswer,
            //    Category = SelectedQuestion.Category,
            //    DepthorRating = SelectedQuestion.DepthorRating,
            //    FollowUpAction = SelectedQuestion.FollowUpAction,
            //    FurtherQuestionId = SelectedQuestion.FurtherQuestionId,
            //    Grouping = SelectedQuestion.Grouping,
            //    InformationTo = SelectedQuestion.InformationTo,
            //    NotifiableResponse = SelectedQuestion.NotifiableResponse,
            //    QuestionOptions = SelectedQuestion.QuestionOptions,
            //    ResponseType = SelectedQuestion.ResponseType,
            //    Id = SelectedQuestion.Id,
            //    Stage = SelectedQuestion.Stage,
            //    IsEnabled = true,
            //    Rating10 = Rating10,
            //    Rating9 = Rating9,
            //    Rating8 = Rating8,
            //    Rating7 = Rating7,
            //    Rating6 = Rating6,
            //    Rating5 = Rating5,
            //    Rating4 = Rating4,
            //    Rating3 = Rating3,
            //    Rating2 = Rating2,
            //    Rating1 = Rating1,
            //    RatingNA = RatingNA
            //};

            //RatingQuestionCollection?.Add(questionToAdd);
            NavigationalParameters.RatingQuestions = RatingQuestionCollection;
        }
    }

    [Time]
    public void RefreshQuestionList()
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        IsLoading = false;
        StreetName = NavigationalParameters.SelectedStreet?.Key;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        SelectedAnswer = null;

        RatingQuestionCollection = NavigationalParameters.RatingQuestions;


        if (NavigationalParameters.CurrentAssignment != null)
            SurveyAnswers =
                _assignmentService.GetRelevantResponses(NavigationalParameters.CurrentAssignment.AssignmentId) ??
                new List<SurveyAnswers>();


        ShowIfNotAudit = true;
        ShowIfNotAudit = false;
        StreetName = NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER
            ? NavigationalParameters.LoggedInUser?.FullName
            : NavigationalParameters.SelectedUser?.FullName;
        RatingQuestionCollection.OrderBy(x => x.QuestionId);


        ShowSubmissionButton = RatingQuestionCollection.Count == SurveyAnswers.Count;
    }
}