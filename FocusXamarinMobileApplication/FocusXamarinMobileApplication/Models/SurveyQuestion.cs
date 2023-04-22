#region

#endregion

using System.ComponentModel;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class SurveyQuestion : BusinessEntityBase, INotifyPropertyChanged
{
    public decimal QuestionId { get; set; } = 0;
    public string Grouping { get; set; } = "";
    public string Stage { get; set; } = "";
    public string Question { get; set; } = "";
    public string KeyAnswer { get; set; } = "";
    public decimal? FurtherQuestionId { get; set; } = 0;
    public string ResponseType { get; set; } = "";
    public string QuestionOptions { get; set; } = "";
    public string NotifiableResponse { get; set; } = "";
    public string InformationTo { get; set; } = "";
    public decimal DepthorRating { get; set; } = 0;
    public string Category { get; set; } = "";
    public string FollowUpAction { get; set; } = "";

    [Ignore] public string LatestAnswerGiven { get; set; } = "";

    [Ignore] public Color QuestionTextColour { get; set; } = Color.FromHex("#292F62");

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(string name)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(name));
    }
}