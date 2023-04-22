using GalaSoft.MvvmLight.Command;

namespace FocusXamarinMobileApplication.Helpers;

public class MenuButtonInfo
{
    public string ImageName { get; set; } = "";
    public string LabelText { get; set; } = "";
    public bool Enabled { get; set; }
    public bool Status { get; set; }
    public RelayCommand NavCommand { get; set; }
}