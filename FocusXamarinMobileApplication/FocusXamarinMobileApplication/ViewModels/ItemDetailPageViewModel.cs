using FocusXamarinMobileApplication.Models;

namespace FocusXamarinMobileApplication.ViewModels;

public class ItemDetailPageViewModel : BaseViewModel
{
    public ItemDetailPageViewModel(Item item = null)
    {
        Title = item?.Text;
        Item = item;
    }

    public Item Item { get; set; }
}