using System.Linq;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class Street : BusinessEntityBase
{
    public string StreetName { get; set; }

    public IGrouping<string, VMexpansionReleaseData> Enpoints { get; set; }
    public string Status { get; set; } = "";
    public string StreetStatusColour { get; set; } = "";
}