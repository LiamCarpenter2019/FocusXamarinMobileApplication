using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Services;
using FocusXamarinMobileApplication.ViewModels;

namespace FocusXamarinMobileApplication.Models;

public class NotesViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;

    public NotesViewModel()
    {
        _assignmentService = new Assignments();
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


    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string NotesText
    {
        get => _notesText;
        set
        {
            _notesText = value;
            OnPropertyChanged();
        }
    }

    public Guid AssignmentId
    {
        get => _assignmentId;
        set
        {
            _assignmentId = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<SurveyComments> CommentsList
    {
        get => _commentsList;
        set
        {
            _commentsList = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () => { refreshComments(); });

    public RelayCommand Save => new(async () =>
    {
        var newComment = new SurveyComments
        {
            AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
            SubmittedDateTime = DateTime.Now,
            Category = NavigationalParameters.CurrentAssignment.Category,
            CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
            Identifier = NavigationalParameters.SelectedAnswer.Identifier,
            Text = NotesText,
            StreetName = NavigationalParameters.SelectedAnswer.StreetName,
            QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
            RemoteTableId = 0,
            QuestionId = NavigationalParameters.SelectedAnswer.QuestionId
        };

        _assignmentService.AddSurveyComments(newComment);

        refreshComments();
        //await Alert("Example button has been pressed!", "Pressed!");
        NavigateBack();
    });

    public RelayCommand Cancel => new(async () =>
    {
        //await Alert("Example button has been pressed!", "Pressed!");
        NavigateBack();
    });


    //public RelayCommand NavToPage => new  RelayCommand(async () =>
    //{
    //    //Navigate to a forms page
    //    await NavigateTo(new NotesPage());

    //    //Navigate to a native page
    //   await NavigateTo(ViewModelLocator.MenuSelectionPage);
    //});

    //public RelayCommand GoBack => new  RelayCommand(async () => { NavigateBack(); });

    public string _title { get; set; }
    public string _notesText { get; set; }
    public Guid _assignmentId { get; private set; }
    public ObservableCollection<SurveyComments> _commentsList { get; private set; }

    public void refreshComments()
    {
        var comments = _assignmentService
            .GetRelevantAssignmentComments(NavigationalParameters.CurrentAssignment.AssignmentId)
            .OrderByDescending(x => x.SubmittedDateTime);

        switch (NavigationalParameters.CommentType)
        {
            case NavigationalParameters.CommentTypes.PERMITQUESTION:
            case NavigationalParameters.CommentTypes.SITECLEARQUESTION:
                foreach (var comment in comments.Where(x =>
                             x.Identifier == NavigationalParameters.SelectedAnswer.Identifier))
                    CommentsList.Add(new NotesQuestionViewModel
                    {
                        AssignmentId = comment.AssignmentId,
                        SubmittedDateTime = comment.SubmittedDateTime,
                        Category = comment.Category,
                        CompletedById = comment.CompletedById,
                        Identifier = comment.Identifier,
                        Text = NotesText,
                        StreetName = comment.StreetName,
                        QNumber = comment.QNumber,
                        RemoteTableId = 0,
                        QuestionId = comment.QuestionId,
                        IsEnabled = false
                    });
                break;
            default:
                if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PRESITECIVILS
                    || NavigationalParameters.AppMode == NavigationalParameters.AppModes.PRESITEPREMISIS)
                    foreach (var comment in comments.Where(x =>
                                 x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId))
                        CommentsList.Add(new NotesItemViewModel
                        {
                            AssignmentId = comment.AssignmentId,
                            SubmittedDateTime = comment.SubmittedDateTime,
                            Category = comment.Category,
                            CompletedById = comment.CompletedById,
                            Identifier = comment.Identifier,
                            Text = NotesText,
                            StreetName = comment.StreetName,
                            QNumber = comment.QNumber,
                            RemoteTableId = 0,
                            QuestionId = comment.QuestionId,
                            IsEnabled = false
                        });
                else
                    foreach (var comment in comments.Where(x =>
                                 x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId))
                        CommentsList.Add(new NotesItemViewModel
                        {
                            AssignmentId = comment.AssignmentId,
                            SubmittedDateTime = comment.SubmittedDateTime,
                            Category = comment.Category,
                            CompletedById = comment.CompletedById,
                            Identifier = comment.Identifier,
                            Text = NotesText,
                            StreetName = comment.StreetName,
                            QNumber = comment.QNumber,
                            RemoteTableId = 0,
                            QuestionId = comment.QuestionId,
                            IsEnabled = false
                        });
                break;
        }
    }
}