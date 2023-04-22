#region

#endregion

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class UserDailyTime : BusinessEntityBase
{
    public Guid RemoteId { get; set; } = Guid.NewGuid();
    public long DailyTimeId { get; set; }
    public long? UserId { get; set; }
    public DateTime? Date { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public bool IsNightShift { get; set; }
    public bool IsApproved { get; set; }
    public TimeSpan? InTime { get; set; }
    public TimeSpan? OutTime { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public long? LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public bool IsSubmited { get; set; }
    public byte[] Timestamp { get; set; }

    [Ignore] public List<UserDailyProjectTimes> UserDailyProjectTimesList { get; set; } = new();

    [Ignore] public List<UserDailyTimeNotes> UserDailyTimeNotesList { get; set; } = new();
}