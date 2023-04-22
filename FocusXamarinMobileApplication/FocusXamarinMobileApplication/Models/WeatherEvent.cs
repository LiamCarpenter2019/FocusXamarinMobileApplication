#region

#endregion

using System;
using FocusXamarinMobileApplication.database;
using SQLite;

namespace FocusXamarinMobileApplication.Models;

public class WeatherEvent : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public long Qnumber { get; set; } = 0;
    public DateTime? StartTime { get; set; } = DateTime.Now;
    public DateTime? EndTime { get; set; } = DateTime.Now;
    public string Type { get; set; } = "";
    public string Severity { get; set; } = "";
    public DateTime? Date { get; set; } = DateTime.Now;
    public long GangLeader { get; set; } = 0;
    public long JobId { get; set; } = 0;
    public string Comments { get; set; } = "";
    public string Icon { get; set; } = "";

    [Ignore] public string DisplayType { get; set; } = "";
}