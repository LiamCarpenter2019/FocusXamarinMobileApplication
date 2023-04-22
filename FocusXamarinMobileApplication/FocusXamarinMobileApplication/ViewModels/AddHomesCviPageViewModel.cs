using MethodTimer;

namespace FocusXamarinMobileApplication.ViewModels;

public class AddHomesCviPageViewModel : FBaseViewModel
{
    private RelayCommand<string> _screenLoadedCommand4AddHomesCvi;

    [Time]
    public AddHomesCviPageViewModel()
    {
    }

    public string NavPassedInfo { get; set; }

    public RelayCommand<string> ScreenLoadedCommand4AddHomesCvi
    {
        get { return _screenLoadedCommand4AddHomesCvi ??= new RelayCommand<string>(e => { NavPassedInfo = e; }); }
    }
}