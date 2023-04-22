namespace FocusXamarinMobileApplication.ViewModels;

public partial class ExampleViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public RelayCommand ExampleButtonPress => new(async () =>
    {
        await Alert("Example button has been pressed!", "Pressed!");
    });

    public RelayCommand NavToPage => new(async () =>
    {
        //Navigate to a forms page
        // await NavigateTo(Locator.Example);

        //Navigate to a native page
        await NavigateTo(ViewModelLocator.MenuSelectionPage);
    });

    public RelayCommand GoBack => new(async () => { NavigateBack(); });
}