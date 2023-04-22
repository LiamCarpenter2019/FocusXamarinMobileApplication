#region

#endregion

using System;
using FocusXamarinMobileApplication.database;
using SQLite;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Models;

public class Labour : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;
    public string ContractReference { get; set; } = "";
    public string QuoteId { get; set; } = "";
    public string Role { get; set; } = "";
    public string FullName { get; set; } = "";
    public string ConPrefix { get; set; } = "";
    public string ContractId { get; set; } = "";
    public string LabourSupervisor { get; set; } = "";
    public string LabourGang { get; set; } = "";
    public string LabourOperative { get; set; } = "";
    public string LabourAddress { get; set; } = "";
    public DateTime LabourDate { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TravelToSite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TimeOnSite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TimeLeftSite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TravelFromSite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime LabourTravel { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime LabourWorked { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string LabourTravelRate { get; set; } = "0";
    public string LabourWorkRate { get; set; } = "0";
    public DateTime TrackerStart { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerOnsite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerOffsite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerFinish { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerTravel { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerWorked { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string LabourType { get; set; } = "W";
    public string LabourReason { get; set; } = "";
    public DateTime NormalHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TimeAndHalfHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime DoubleTimeHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public decimal? Wages { get; set; } = 0;
    public DateTime ClaimedYardStart { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime ClaimedYardEnd { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerYardStart { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime TrackerYardEnd { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime HoursWorked { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime HoursTravel { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string ModifyWages { get; set; } = "";
    public string ClaimedorTracked { get; set; } = "T";
    public DateTime ActualNormalHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime ActualTimeAndHalfHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime ActualDoubleHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime ActualTravelHours { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public decimal? NormalTrueCost { get; set; } = 0;
    public decimal? MidweekTrueCost { get; set; } = 0;
    public decimal? SaturdayTrueCost { get; set; } = 0;
    public decimal? DoubleTimeTrueCost { get; set; } = 0;
    public decimal? WagesTrueCost { get; set; } = 0;
    public DateTime PostedByGanger { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string PostedByGangerName { get; set; } = "";
    public DateTime PostedByAdmin { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public decimal? HoursDeductions { get; set; } = 0;
    public bool IsToUsePricework { get; set; } = false;
    public decimal? PriceworkValue { get; set; } = 0;
    public decimal? PlantDeduction { get; set; } = 0;
    public decimal? TeamMemberDeduction { get; set; } = 0;
    public decimal? PriceworkDeductions { get; set; } = 0;
    public decimal? PriceworkModifyWages { get; set; } = 0;
    public decimal? PriceworkWages { get; set; } = 0;
    public string HoursDeductionNotes { get; set; } = "";
    public string PriceworkDeductionNotes { get; set; } = "";
    public Guid LabourGuid { get; set; } = Guid.NewGuid();
    public DateTime SupervisorStart { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorYardStart { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorOnSite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorOffSite { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorFinish { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorYardEnd { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorWorked { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public DateTime SupervisorTravel { get; set; } = DateTime.Parse("01/01/1900 00:00:00");
    public string SupervisorLabourType { get; set; } = "W";
    public DateTime ApprovedBySupervisor { get; set; } = DateTime.Parse("01/01/1900 00:00:00");

    [Ignore] public Color bgColour { get; set; } = Color.White;

    [Ignore] public bool SupervisorMode { get; set; } = false;
}