#region

#endregion

using System;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class UserDailyTimeNotes : BusinessEntityBase
{
    public Guid? RemoteId { get; set; } = Guid.Empty;
    public long? DailyTimeId { get; set; }
    public string Notes { get; set; }

    //[Ignore] public UserDailyTimeDto UserDailyTime { get; set; }
}