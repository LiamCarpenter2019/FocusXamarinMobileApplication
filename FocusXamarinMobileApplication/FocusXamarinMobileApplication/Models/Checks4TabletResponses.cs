#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class Checks4TabletResponses : BusinessEntityBase
{
    public int ServerId { get; set; }
    public string Qnumber { get; set; }
    public DateTime RelevantDate { get; set; }
    public string GangLeaderName { get; set; }
    public string SupervisorName { get; set; }
    public int PlantId { get; set; }
    public string QuestionResponse { get; set; }
    public DateTime DateTimeOfCheck { get; set; }
    public int QuestionId { get; set; }
    public string PlantCheckedByName { get; set; }
    public string PlantAssignedToName { get; set; }
    public string Comments { get; set; }
    public string PictureFileName { get; set; }
    public string PictureLatitude { get; set; }
    public string PictureLongitude { get; set; }
    public string SignatureFileName { get; set; }
    public string CheckSubmittedBy { get; set; }
    public string ChecksStatus { get; set; }
    public string ChecksType { get; set; }
    public int DifferentCheckIndicator { get; set; }
    public string Location { get; set; }
}