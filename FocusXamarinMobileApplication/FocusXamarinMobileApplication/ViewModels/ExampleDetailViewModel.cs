namespace FocusXamarinMobileApplication.ViewModels;

public class ExampleDetailViewModel : BaseViewModel
{
    public ExampleDetailViewModel(ExampleItem item = null)
    {
        Title = item?.Text;
        Item = item;
    }

    public ExampleItem Item { get; set; }
}