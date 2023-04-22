namespace FocusXamarinMobileApplication.ViewModels;

public partial class ExampleViewModel
{
    private RelayCommand<string> _screenLoadedCommand4Example;
    // public List<string> ExampleList { get; set; }

    public string NavPassedInfo { get; set; }

    public RelayCommand<string> ScreenLoadedCommand4Example
    {
        get { return _screenLoadedCommand4Example ??= new RelayCommand<string>(e => { NavPassedInfo = e; }); }
    }
}