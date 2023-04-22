#region

#endregion

using System;
using System.Text.Json.Serialization;
using FocusXamarinMobileApplication.database;

namespace FocusXamarinMobileApplication.Models;

public class ClaimedNotesFile : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public long NotesId { get; set; } = 0;
    public string ContractReference { get; set; } = "";
    public string QuoteId { get; set; } = "";
    public string ContractId { get; set; } = "";
    public string ConPrefix { get; set; } = "";
    public string NotesGang { get; set; } = "";
    public string NotesText { get; set; } = "";
    public DateTime NotesDate { get; set; } = DateTime.Now;
    public DateTime PostedByGanger { get; set; } = DateTime.Now;
    public DateTime ApprovedBySupervisor { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string SupervisorText { get; set; } = "";
    public string PostedByGangerName { get; set; } = "";
    public string ApprovedBySupervisorName { get; set; } = "";
    public DateTime PostedByAdmin { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string DailyChecksTodaysTask { get; set; } = "";
    public string DailyChecksComments { get; set; } = "";
    public string AnyDelays { get; set; } = "";
    public string AnyAddnlWorkReq { get; set; } = "";
    public string AdminText { get; set; } = "";
    public int VMhomesPassed { get; set; } = 0;
    public Guid? ImageId { get; set; } = Guid.NewGuid();
    public string SupervisorDelays { get; set; } = "";
    public string SupervisorAddnlWork { get; set; } = "";
    public string PayrollComment { get; set; } = "";
    public string StartAddress { get; set; } = "";
    public string EndAddress { get; set; } = "";

    [JsonIgnore] public string StartAbbreviationId { get; set; } = "0";

    [JsonIgnore] public string EndAbbreviationId { get; set; } = "0";

    [JsonIgnore] public int StartEndpointId { get; set; } = 0;

    [JsonIgnore] public int EndEndpointId { get; set; } = 0;
}