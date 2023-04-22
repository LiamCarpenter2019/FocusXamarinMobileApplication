namespace FocusXamarinMobileApplication.Models;

public enum MenuItemType
{
    LogIn,
    Settings
}

public class NavMenuItem
{
    public MenuItemType Id { get; set; }

    public string Title { get; set; }
}