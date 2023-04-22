#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class UserDailyProjectTimes : BusinessEntityBase
{
    public Guid? RemoteId { get; set; }
    public Guid RemoteProjectTimeId { get; set; } = Guid.NewGuid();
    public long ProjectTimeId { get; set; }
    public long? DailyTimeId { get; set; }
    public long? UserId { get; set; }
    public long? QuoteId { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? Endtime { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public long? LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; } = DateTime.Parse("01/01/1900 00:00:00");

    public string ProjectName { get; set; }

    public string ClientName { get; set; }

    public string GangLeaderName { get; set; }

    public string OperativeNames { get; set; }

    public string _notes { get; set; }

    [Ignore] public List<DailyProjectNotes> DailyProjectNotesList { get; set; } = new();

    [Ignore] public UserDailyTime UserDailyTime { get; set; }

    public string Notes
    {
        get => _notes;
        set => _notes = DailyProjectNotesList?.FirstOrDefault()?.Notes ?? "";
    }
}