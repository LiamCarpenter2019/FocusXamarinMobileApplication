using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Helpers;

public class ExampleTemplateSelector : DataTemplateSelector
{
    public DataTemplate Example1Template { get; set; }
    public DataTemplate Example2Template { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is Example1Model)
            return Example1Template;
        if (item is Example2Model)
            return Example2Template;

        return Example1Template;
    }
}

public class Example1Model : object
{
    public bool IsEnabled { get; set; }
}

public class Example2Model : object
{
    public bool IsEnabled { get; set; }
}