#region

#endregion

using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class TransferPlantToOperatives : BusinessEntityBase
{
    public int FocusId { get; set; } = 0;
    public string FirstName { get; set; } = "";
    public string Surname { get; set; } = "";
    public string LoginAlias { get; set; } = "";
    public string Role { get; set; } = "";
    [Ignore] public Color BackgroundHighlighted { get; set; } = Color.Transparent;
}