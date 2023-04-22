namespace FocusXamarinMobileApplication.ViewModels;

public class VisitorSignaturePageViewModel : FBaseViewModel
{
    private RelayCommand<string> _screenLoadedCommand4VisitorSignature;

    public string NavPassedInfo { get; set; }


    public RelayCommand<string> ScreenLoadedCommand4VisitorSignature
    {
        get { return _screenLoadedCommand4VisitorSignature ??= new RelayCommand<string>(e => { NavPassedInfo = e; }); }
    }
}