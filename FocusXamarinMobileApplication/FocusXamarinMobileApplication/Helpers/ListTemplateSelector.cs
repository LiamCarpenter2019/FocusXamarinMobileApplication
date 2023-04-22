namespace FocusXamarinMobileApplication.Helpers;

public class ListTemplateSelector : DataTemplateSelector
{
    public DataTemplate TaskTemplate { get; set; }
    public DataTemplate ProjectTemplate { get; set; }
    public DataTemplate GangTemplate { get; set; }
    public DataTemplate JobTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is TaskViewModel)
            return TaskTemplate;
        if (item is ProjectViewModel)
            return ProjectTemplate;
        if (item is GangViewModel)
            return GangTemplate;
        if (item is JobViewModel)
            return JobTemplate;
        return GangTemplate;
    }
}

public class GangViewModel : JobData4Tablet
{
    public string ModelType = "Gang";
    public string Date { get; set; }
    public string SupervisorName { get; set; }
    public string GangMemberNames { get; set; }
    public Color DateColour { get; set; } = Color.White;
    public Color BackgroundHighlighted { get; set; } = Color.Transparent;
}

public class ProjectViewModel : JobData4Tablet
{
    public string ModelType = "Project";
    public string Date { get; set; }
    public string GangCount { get; set; }
    public Color DateColour { get; set; } = Color.White;
    public Color BackgroundHighlighted { get; set; } = Color.Transparent;
}

public class JobViewModel : JobData4Tablet
{
    public string ModelType = "Job";
    public string Date { get; set; }
    public string GangCount { get; set; }
    public Color DateColour { get; set; } = Color.White;
    public Color BackgroundHighlighted { get; set; } = Color.Transparent;
}

public class TaskViewModel : JobData4Tablet
{
    public string ModelType = "Task";
    public string Date { get; set; }
    public string Category { get; set; }
    public Color DateColour { get; set; } = Color.White;
    public string RefTitle { get; set; }
    public string ReferenceNo { get; set; }
    public Color CategoryColour { get; set; } = Color.White;
    public bool ShowCode { get; internal set; }
    public Color BackgroundHighlighted { get; set; } = Color.Transparent;
}