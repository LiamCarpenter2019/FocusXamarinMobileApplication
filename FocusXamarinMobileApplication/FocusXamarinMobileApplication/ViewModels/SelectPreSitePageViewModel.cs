#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

//using System.Security.Cryptography;

//using GalaSoft.MvvmLight.Helpers;

namespace FocusXamarinMobileApplication.ViewModels;

public class SelectPreSitePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;

    private bool _isLoading;

    private ObservableCollection<Assignment> _preSites;

    private string _preSiteStatus;

    private string _preSiteStatusColour;

    private bool _showSubmissionButton;

    //  public Assignments AssignmentService;
    private Person preditedBy;

    public SelectPreSitePageViewModel()
    {
        _jobService = new Jobs();
        _assignmentService = new Assignments();

        RefreshPreSites();
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

    public string PreSitedBy { get; set; }

    public ObservableCollection<Assignment> PreSites
    {
        get => _preSites;
        set
        {
            _preSites = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<PreSiteUserGroup> PreSiteUserGroups
    {
        get => _preSiteUserGroups;
        set
        {
            _preSiteUserGroups = value;
            OnPropertyChanged();
        }
    }

    public string PreSiteStatus
    {
        get => _preSiteStatus;
        set
        {
            _preSiteStatus = value;
            OnPropertyChanged();
        }
    }

    public string PreSiteStatusColour
    {
        get => _preSiteStatusColour;
        set
        {
            _preSiteStatusColour = value;
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

    public bool ShowSubmissionButton
    {
        get => _showSubmissionButton;
        set
        {
            _showSubmissionButton = value;
            OnPropertyChanged();
        }
    }

    public Assignments _assignmentService { get; set; }

    public DataPassed2Server DataPassedToserver { get; set; }

    public bool UserChoice { get; set; }

    public List<SurveyQuestion> Questions { get; set; }

    public List<SurveyQuestion> TempQuestions { get; set; }

    public RelayCommand<PreSiteUserGroup> SelectedPreSite => new(async i =>
    {
        if (i != null)
        {
            NavigationalParameters.CurrentAssignment =
                PreSites.FirstOrDefault(x => x.AssignmentId.ToString() == i.AssignmentId.ToString());
            NavigationalParameters.CurrentAssignment.Editable =
                i.FocusId == NavigationalParameters.LoggedInUser.FocusId ? true : false;
            //if (i.PreSiteById != NavigationalParameters.LoggedInUser.FocusId)
            //{
            //    UserChoice = await Alert("Preview Mode?",
            //           "You can only view this pre site in preview mode, Do you wish to continue", "No", "Yes");
            //    if (!UserChoice)
            //    {
            //        NavigationalParameters.PreviewMode = false;

            //        questions = _assignmentService.GetSurveyQuestions().Where(x =>
            //              x.Category == "PreSiteCivils" && x.Grouping == NavigationalParameters.SurveyType).ToList();

            //        tempQuestions = new List<SurveyQuestion>();

            //        foreach (var q in questions)
            //            if (!tempQuestions.Any(x => x.Category == q.Category && x.QuestionId == q.QuestionId))
            //                tempQuestions.Add(q);

            //        NavigationalParameters.Questions = tempQuestions;

            //          await NavigateTo(new SurveyQuestions());
            //    }
            //}
            //else {
            //    if (i.StatusText.Contains("Completed on"))
            //    {


            //============================================================

            UserChoice = await Alert("Start a new survey?",
                "Would you like to start a new survey or edit the previous?", "Start", "Continue");
            if (UserChoice)
                NavigationalParameters.CurrentAssignment = new Assignment
                {
                    Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                    ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                    StreetName = NavigationalParameters.SelectedStreet?.Key,
                    AssignmentId = Guid.NewGuid(),
                    Category = NavigationalParameters.SurveyType.ToString(),
                    Cnumber = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                    TermContract = NavigationalParameters.CurrentSelectedJob.BaseContractId,
                    ClientName = NavigationalParameters.CurrentSelectedJob.ClientName,
                    Complete = "false",
                    LocationName = NavigationalParameters.SelectedStreet?.FirstOrDefault().L4Number,
                    PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                    Worksnumber = NavigationalParameters.CurrentSelectedJob.WorksNumber,
                    RemoteTableId = 0
                };


            //============================================================


            if (NavigationalParameters.CurrentAssignment.RemoteTableId == 0)
            {
                NavigationalParameters.PreviewMode = false;

                NavigationalParameters.CurrentAssignment.Complete = "false";

                await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

                //NavigationalParameters.Questions = _assignmentService.GetSurveyQuestions("PreSiteCivils").Where(x =>
                //    x.Grouping == NavigationalParameters.SurveyType).ToList();

                //tempQuestions = new List<SurveyQuestion>();

                //foreach (var q in questions)
                //    if (!tempQuestions.Any(x => x.Category == q.Category && x.QuestionId == q.QuestionId))
                //        tempQuestions.Add(q);

                //NavigationalParameters.Questions = tempQuestions;

                IsLoading = false;

                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
            }
            //else
            //{
            //    UserChoice = await Alert("Preview Mode?",
            //  "You can only view this pre site in preview mode, Do you wish to continue", "No", "Yes");
            //    if (!UserChoice)
            //    {
            //        NavigationalParameters.PreviewMode = false;

            //        questions = _assignmentService.GetSurveyQuestions().Where(x =>
            //               x.Category == "PreSiteCivils" && x.Grouping == NavigationalParameters.SurveyType).ToList();

            //        tempQuestions = new List<SurveyQuestion>();

            //        foreach (var q in questions)
            //            if (!tempQuestions.Any(x => x.Category == q.Category && x.QuestionId == q.QuestionId))
            //                tempQuestions.Add(q);

            //        NavigationalParameters.Questions = tempQuestions;

            //          await NavigateTo(new SurveyQuestions());
            //    }
            //}
            //}
            //else
            //{
            //    NavigationalParameters.PreviewMode = false;

            //    questions = _assignmentService.GetSurveyQuestions().Where(x =>
            //        x.Category == "PreSiteCivils" && x.Grouping == NavigationalParameters.SurveyType).ToList();

            //    tempQuestions = new List<SurveyQuestion>();

            //    foreach (var q in questions)
            //        if (!tempQuestions.Any(x => x.Category == q.Category && x.QuestionId == q.QuestionId))
            //            tempQuestions.Add(q);

            //    NavigationalParameters.Questions = tempQuestions;

            //    IsLoading = false;

            //      await NavigateTo(new SurveyQuestions());
            //}
            //}
        }
    });

    public RelayCommand GoBack => new(async () =>
    {
        await NavigateTo(ViewModelLocator.SelectStreetPage);
        ;
    });

    public RelayCommand Refresh => new(() =>
    {
        //OnPropertyChanged("SavedDamageCount");
        OnPropertyChanged("ShowSubmissionButton");
    });

    public ObservableCollection<PreSiteUserGroup> _preSiteUserGroups { get; private set; }


    public void RefreshPreSites()
    {
        // PreSites = new List<Assignment>();
        var userService = new Users();
        PreSites = new ObservableCollection<Assignment>(_assignmentService
            .GetPreSites(NavigationalParameters.SelectedStreet).OrderBy(x => x.CompletedOn));

        PreSiteUserGroups = new ObservableCollection<PreSiteUserGroup>();

        if (PreSites.Any())
        {
            foreach (var item in PreSites)
            {
                //if (item.PreSiteById > 0)
                //    preditedBy = userService.GetUserById(Convert.ToInt32(item.PreSiteById));
                //else
                //    preditedBy = NavigationalParameters.LoggedInUser;

                //item.StatusColour = Color.Green;

                item.SurveyAnswers = _assignmentService.GetRelevantAssignmentsResponsesByStreetName(item.StreetName)
                    ?.ToList();

                if (item.SurveyAnswers.Any())
                {
                    var questionGroup = item.SurveyAnswers?.GroupBy(x => x.CompletedById)?.ToList();

                    foreach (var userSet in questionGroup)
                    {
                        var us = userSet.FirstOrDefault();
                        var userName = _jobService.GetUserName(us.CompletedById);
                        var presiteUserGroup = new PreSiteUserGroup();

                        if (us.CompletedById != NavigationalParameters.LoggedInUser.FocusId.ToString())
                        {
                            presiteUserGroup.StatusText = $"Created on {us.SubmittedDateTime.Date} by {userName}";
                            presiteUserGroup.StatusColour = Color.Green;
                            presiteUserGroup.AssignmentId = us.AssignmentId;
                            presiteUserGroup.StreetName = us.StreetName;
                            ShowSubmissionButton = true;
                        }
                        else if (us.RemoteTableId > 0)
                        {
                            presiteUserGroup.StatusText = $"Created on {us.SubmittedDateTime.Date} by {userName}";
                            presiteUserGroup.StatusColour = Color.Green;
                            presiteUserGroup.AssignmentId = us.AssignmentId;
                            presiteUserGroup.StreetName = us.StreetName;
                        }
                        else
                        {
                            presiteUserGroup.StatusText =
                                $"In Progress by {NavigationalParameters.LoggedInUser?.FullName}";
                            presiteUserGroup.StatusColour = Color.Red;
                            presiteUserGroup.AssignmentId = us.AssignmentId;
                            presiteUserGroup.StreetName = us.StreetName;
                        }

                        PreSiteUserGroups.Add(presiteUserGroup);
                    }
                }
                else
                {
                    var newPresiteUserGroup = new PreSiteUserGroup
                    {
                        StatusText = "Not Started",
                        StatusColour = Color.Red,
                        AssignmentId = item.AssignmentId,
                        StreetName = item.StreetName
                    };

                    PreSiteUserGroups.Add(newPresiteUserGroup);
                }
            }
        }
        else
        {
            var survey = new Assignment
            {
                CompletedOn = DateTime.Parse("01/01/1900 00:00:00"),
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                RemoteTableId = 0,
                AssignmentId = Guid.NewGuid(),
                Category = NavigationalParameters.SurveyType.ToString(),
                Cnumber = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                Complete = "false",
                PreSiteById = NavigationalParameters.LoggedInUser.FocusId,
                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                StatusText = "InProgress",
                StatusColour = Color.Red,
                StreetName = NavigationalParameters.SelectedStreet?.Key,
                TermContract = NavigationalParameters.CurrentSelectedJob?.BaseContractId,
                Type = ""
            };

            _assignmentService.AddACurrentAssignment(survey);
            PreSites.Add(survey);
            var newPresiteUserGroup = new PreSiteUserGroup
            {
                StatusText = "Not Started",
                StatusColour = Color.Red,
                AssignmentId = survey.AssignmentId,
                StreetName = survey.StreetName
            };

            PreSiteUserGroups.Add(newPresiteUserGroup);
        }
    }
}