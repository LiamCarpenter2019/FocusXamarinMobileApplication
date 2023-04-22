#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class PlantIssue : BusinessEntityBase
{
    public long RemoteId { get; set; }
    public long PlantId { get; set; }
    public DateTime DateTimeReported { get; set; }
    public string FirstQuestion { get; set; } = "";
    public string SecondQuestion { get; set; } = "";
    public string Comments { get; set; } = "";
    public string LocationLatitude { get; set; } = "";
    public string LocationLongitude { get; set; } = "";
    public string LocationText { get; set; } = "";
    public string ReportedByName { get; set; } = "";
    public string InUseByName { get; set; } = "";
    public string Picture1Filename { get; set; } = "";
    public string Picture1Status { get; set; } = "Not Transferred 2 Storage";
    public string Picture2Filename { get; set; } = "";
    public string Picture2Status { get; set; } = "Not Transferred 2 Storage";
    public string Picture3Filename { get; set; } = "";
    public string Picture3Status { get; set; } = "Not Transferred 2 Storage";
    public string Picture4Filename { get; set; } = "";
    public string Picture4Status { get; set; } = "Not Transferred 2 Storage";
    public string Status { get; set; } = "";
    public DateTime LastUpdateDateTime { get; set; }
    public string RelevantJobCnumber { get; set; }
    public string RelevantJobWorksNumber { get; set; }
    public string RelevantJobQnumber { get; set; }
    public string GangLeaderEmailAddress { get; set; }
    public string SupervisorEmailaddress { get; set; }
}