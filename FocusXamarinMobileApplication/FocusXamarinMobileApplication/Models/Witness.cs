#region

#endregion

using System;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class Witness : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;

    public string Name { get; set; } = "";

    public string Number { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Now;

    public string Statement { get; set; } = "";

    public DateTime InputOn { get; set; } = DateTime.Now;

    public string Address { get; set; } = "";

    public string Email { get; set; } = "";

    public string SignatureFileName { get; set; } = "";

    public bool IsSelected { get; set; } = false;

    [Ignore] public string DateToDisplay => Date.ToString();

    public string InvestigationId { get; set; }

    public string PublicUtilityDamageId { get; set; }

    public Guid PublicUtilityDamageGuid { get; set; }
}