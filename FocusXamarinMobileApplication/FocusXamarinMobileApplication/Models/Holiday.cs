#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Holiday : BusinessEntityBase
{
    public string ForeName { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Month { get; set; } = "";
    public string Year { get; set; } = "";
    public string HolidayString { get; set; } = "";
    public string Comments { get; set; } = "";
    public string Category { get; set; } = "";
    public string Confirmed { get; set; } = "";
    public string Authorised { get; set; } = "";
    public DateTime? DateTimeHolidayRequested { get; set; } = DateTime.Parse("1/1/1900 00:00:00");
    public long? OperativeId { get; set; } = 0;
    public DateTime? StartDate { get; set; } = DateTime.Parse("1/1/1900 00:00:00");
    public DateTime? EndDate { get; set; } = DateTime.Parse("1/1/1900 00:00:00");
    public string LeaveType { get; set; } = "";
    public string LeaveFor { get; set; } = "";
    public long RemoteTableId { get; set; } = 0;
}