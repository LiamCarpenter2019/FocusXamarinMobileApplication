#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class PublicUtilityDamageQuestion : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;

    //public string DamageId { get; set; } = "";
    public string DamageGroup { get; set; } = "";
    public string DamageColour { get; set; } = "";
    public string QuestionOne { get; set; } = "N/A";
    public string QuestionTwo { get; set; } = "N/A";
    public string QuestionThree { get; set; } = "N/A";
    public string QuestionFour { get; set; } = "N/A";
    public string DamageType { get; set; } = "";
    public string SurfaceType { get; set; } = "";
    public long InvestigationId { get; set; } = 0;

    [Ignore] public string _category { get; set; } = "";

    public string Category
    {
        get => _category;
        set
        {
            _category = value;
            if (DamageGroup == "UnKnown")
                DisplayDamageType = $"{DamageGroup}";
            else
                DisplayDamageType = $"{DamageGroup} - {DamageType}";
        }
    }

    [Ignore] public string DisplayDamageType { get; set; } = "";
}