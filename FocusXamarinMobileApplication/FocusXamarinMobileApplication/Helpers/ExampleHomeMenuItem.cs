namespace FocusXamarinMobileApplication.Helpers;

public enum MenuItemType
{
    Browse,
    About
}

public class ExampleHomeMenuItem
{
    public MenuItemType Id { get; set; }

    public string Title { get; set; }
}