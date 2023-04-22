namespace FocusXamarinMobileApplication.Models;

public class MapViewModel
{
    private RelayCommand<string> _screenLoadedCommand4SurveyMap;

    public RelayCommand<string> ScreenLoadedCommand4SurveyMap
    {
        get { return _screenLoadedCommand4SurveyMap ??= new RelayCommand<string>(e => { }); }
    }
}