#region

#endregion

using FocusXamarinMobileApplication.database;
using SQLite;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Models;

public class CableRuns : BusinessEntityBase
{
    public int RemoteTableId { get; set; } = 0;
    public long QNumber { get; set; } = 0;
    public string ProjectName { get; set; } = "";
    public string CableRunIdentifier { get; set; } = "";
    public bool Blocked { get; set; } = false;
    public bool Proved { get; set; } = false;
    public decimal? ApproxDistance { get; set; } = 0;

    [Ignore] private Color _backgroungColour { get; set; } = Color.White;

    [Ignore]
    public Color BackgroungColour
    {
        get => _backgroungColour;
        set => _backgroungColour = value;
    }

    [Ignore] public bool SavedToServer { get; set; } = true;
}