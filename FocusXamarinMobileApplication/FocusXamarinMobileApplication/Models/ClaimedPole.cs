#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class ClaimedPole : BusinessEntityBase
{
    public int RemoteTableId { get; set; }
    public Guid? ClaimId { get; set; }
    public Guid? PoleIdentifier { get; set; }
    public DateTime? DateOfEntry { get; set; }
    public string WorksHeader { get; set; }
    public string QuoteId { get; set; }
}