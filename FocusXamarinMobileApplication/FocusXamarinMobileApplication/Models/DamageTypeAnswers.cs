#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class DamageTypeAnswers : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public string DamageTypeID { get; set; } = "";
    public string DamageLocation { get; set; } = "";
    public string Answer1 { get; set; } = "";
    public string Answer2 { get; set; } = "";
    public string Answer3 { get; set; } = "";
    public string Answer4 { get; set; } = "";
    public int PublicUtilityDamageID { get; set; } = 0;
    public int InvestigationID { get; set; } = 0;
    public DateTime? InputOn { get; set; } = DateTime.Now;
    public string SurfaceMaterial { get; set; } = "";
    public bool? IsFinal { get; set; } = false;
    public bool SavedToServer { get; set; } = false;
}