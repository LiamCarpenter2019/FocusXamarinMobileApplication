namespace FocusXamarinMobileApplication.Helpers;

public class NotesTemplateSelector : DataTemplateSelector
{
    public DataTemplate NotesQuestionTemplate { get; private set; }
    public DataTemplate NotesItemTemplate { get; private set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is NotesQuestionViewModel)
            return NotesQuestionTemplate;
        if (item is NotesItemViewModel)
            return NotesItemTemplate;

        return NotesQuestionTemplate;
    }
}

public class NotesQuestionViewModel : SurveyComments
{
    public bool IsEnabled { get; set; }
}

public class NotesItemViewModel : SurveyComments
{
    public bool IsEnabled { get; set; }
}