#region

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class FormsCommentPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public bool _canEditComments = true;
    private string _comments;
    public FocusMobileV3Database _db;

    public Assignments AssignmentService;

    //new Tuple<SurveyAnswers, Assignment, IGrouping<string, VMexpansionReleaseData>>(
    //             CurrentSurveyResponse,
    //             NavigationalParameters.CurrentAssignment, Vm.StreetInfo)
    public FormsCommentPageViewModel()
    {
        _db = new FocusMobileV3Database();
        AssignmentService = new Assignments();
    }

    public bool IsLoading { get; set; }
    public SurveyComments CommentToUpdate { get; set; }

    public List<SurveyComments> CurrentComments { get; set; }

    public string Comments
    {
        get => _comments;
        set
        {
            _comments = value;
            OnPropertyChanged();
        }
    }

    public bool CanEditComments
    {
        get => _canEditComments;
        set
        {
            _canEditComments = value;
            OnPropertyChanged();
        }
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


    public RelayCommand Refresh => new(() =>
    {
        Comments = "";
        CanEditComments = true;
        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE)
        {
            Comments = NavigationalParameters.CurrentInvestigationAnswer?.Comment;
            Comments = NavigationalParameters.CurrentInvestigationAnswer.Comment =
                Comments += $"\n {DateTime.Now} :";
        }
        else
        {
            CurrentComments =
                AssignmentService
                    .GetRelevantAssignmentComments(NavigationalParameters.CurrentAssignment.AssignmentId)
                    .Where(x => x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId).ToList();

            foreach (var comment in CurrentComments) Comments += $"{comment?.Text} \n";

            Comments += $"{DateTime.Now} : ";

            // CanEditComments = !NavigationalParameters.PreviewMode;
        }

        OnPropertyChanged("Notes");
    });

    public RelayCommand NavBack => new(async () => { NavigateBack(); });

    public RelayCommand SubmitComment => new(async () =>
    {
        IsLoading = true;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE)
        {
            NavigationalParameters.CurrentInvestigationAnswer.Comment = Comments;
        }
        else
        {
            CommentToUpdate =
                CurrentComments?.FirstOrDefault(x =>
                    x.QuestionId == NavigationalParameters.SelectedAnswer?.QuestionId) ??
                new SurveyComments
                {
                    CompletedById = NavigationalParameters.LoggedInUser?.FocusId.ToString(),
                    AssignmentId = NavigationalParameters.CurrentAssignment?.AssignmentId,
                    Category = NavigationalParameters.CurrentAssignment?.Category.ToUpper(),
                    Identifier = NavigationalParameters.SelectedAnswer?.Identifier,
                    QNumber = NavigationalParameters.CurrentAssignment.Qnumber,
                    QuestionId = NavigationalParameters.SelectedAnswer.QuestionId,
                    StreetName = NavigationalParameters.CurrentAssignment?.StreetName,
                    SubmittedDateTime = DateTime.Now
                };

            CommentToUpdate.Text = Comments;
        }

        var userChoice = await Alert("Submit Comment?", "Would you like to submit the Comment now?", "Yes", "No");
        if (userChoice)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE)
                        App.Database.SaveItem(NavigationalParameters.CurrentInvestigationAnswer);
                    else
                        await AssignmentService.AddSurveyComments(CommentToUpdate);
                    //commentToUpdate.Text = commentToUpdate.Text +
                    //                       $"{DateTime.Now} - {Comments}, \n ";

                    // await Alert("Successfully Stored", "Success");
                    NavigateBack();
                }
                catch (Exception ex)
                {
                    await Alert("An issue has occured submitting the Comment. This has not been saved", "Error!");
                    NavigateBack();
                }

                // submit presite whern complete                   
            }
            else
            {
                await Alert("Please connect to a network and try again. The Comments has not been saved. ",
                    "No Connectivity");
                NavigateBack();
            }
        }
        else
        {
            NavigateBack();
        }

        IsLoading = false;
    });
}