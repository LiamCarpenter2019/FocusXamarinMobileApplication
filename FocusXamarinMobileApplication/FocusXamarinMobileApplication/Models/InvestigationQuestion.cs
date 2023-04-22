#region

#endregion

using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class InvestigationQuestion : BusinessEntityBase
{
    /*private ObservableCollection<string> yesNoEmpty;
    private ObservableCollection<InvestigationAnswer> multipleAnswers;

    public InvestigationQuestion()
    {
        AnswerYesNoEmpty = new ObservableCollection<string> { "", "Yes", "No" };
    }*/

    //public event PropertyChangedEventHandler PropertyChanged;

    public string QuestionId { get; set; }
    public string Question { get; set; }

    public bool QuestionType { get; set; }
    public string QuestionOrder { get; set; }
    public string Category { get; set; }

    [Ignore] public Color Btn1 { get; set; }
    [Ignore] public Color Btn2 { get; set; }
    [Ignore] public Color Btn3 { get; set; }
    [Ignore] public Color Btn4 { get; set; }

    public string Answer1 { get; set; } = "Yes";
    public string Answer2 { get; set; } = "No";
    public string Answer3 { get; set; } = "N/A";
    public string Answer4 { get; set; }

    public bool IsVisible { get; internal set; }
    /*
[JsonIgnore, Ignore]
public ObservableCollection<string> AnswerYesNoEmpty
{
get { return yesNoEmpty; }
set
{
yesNoEmpty = value;
}
}

[JsonIgnore, Ignore]
public ObservableCollection<InvestigationAnswer> MultipleAnswers
{
get
{
return multipleAnswers;
}
set
{
multipleAnswers = value;
}
}
*/
}