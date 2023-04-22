#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class UsersToolBoxTalks : BusinessEntityBase
{
    public int RemoteTableId { get; set; }
    public string ToolBoxTalkCode { get; set; }
    public long FocusId { get; set; }
    public string SignatureFileName { get; set; }
    public DateTime? DistributionDate { get; set; }
    public long SupervisorId { get; set; }
    public long GangLeaderId { get; set; }
    public DateTime Date { get; set; }
    public bool SaveToTraining { get; set; }
    public long TimeTaken { get; set; }
    public long QuoteId { get; set; }
}