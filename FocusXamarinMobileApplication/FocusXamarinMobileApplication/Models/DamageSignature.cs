#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class DamageSignature : BusinessEntityBase
{
    public string PublicUtilityDamageID { get; set; }
    public string SignatureType { get; set; }
    public string Filename { get; set; }
    public DateTime SignatureDate { get; set; }
    public int InvestigationId { get; set; }
    public long UserId { get; set; }
}