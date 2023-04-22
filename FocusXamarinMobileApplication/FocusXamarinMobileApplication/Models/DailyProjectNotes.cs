#region

#endregion

using System;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class DailyProjectNotes : BusinessEntityBase
{
    public Guid? RemoteId { get; set; } = Guid.Empty;
    public long? ProjectTimeId { get; set; }
    public string Notes { get; set; }
    public Guid? RemoteProjectTimeId { get; set; } = Guid.Empty;


    [Ignore] public UserDailyProjectTimes UserDailyProjectTimes { get; set; }
}