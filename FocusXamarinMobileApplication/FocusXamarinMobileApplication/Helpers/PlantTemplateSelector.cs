namespace FocusXamarinMobileApplication.Helpers;

public class PlantTemplateSelector : DataTemplateSelector
{
    public DataTemplate Plantemplate { get; set; }
    public DataTemplate PlantTransferTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is PlantViewModel)
            return Plantemplate;
        if (item is PlantTransferViewModel)
            return PlantTransferTemplate;

        return Plantemplate;
    }
}

public class PlantViewModel : Plant4Tablet
{
    public bool IsEnabled { get; set; }
    public string NextServiceDateString { get; set; }
}

public class PlantTransferViewModel : Plant4Tablet
{
    public bool IsEnabled { get; set; }
    public bool TransferIn { get; set; }
    public string NextServiceDateString { get; set; }
}