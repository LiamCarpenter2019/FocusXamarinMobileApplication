using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Helpers;

public class QuestionTempleateSelector : DataTemplateSelector
{
    public DataTemplate YesNoNaQuestionTemplate { get; set; }
    public DataTemplate MultiSelectorQuestionTemplate { get; set; }
    public DataTemplate RatingQuestionsTemplate { get; set; }
    public DataTemplate LocationIdentityQuestionTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is YesNoNaQuestionViewModel)
            return YesNoNaQuestionTemplate;
        if (item is MultiQuestionViewModel)
            return MultiSelectorQuestionTemplate;
        if (item is LocationIdentityQuestionViewModel)
            return LocationIdentityQuestionTemplate;
        return RatingQuestionsTemplate;
    }
}