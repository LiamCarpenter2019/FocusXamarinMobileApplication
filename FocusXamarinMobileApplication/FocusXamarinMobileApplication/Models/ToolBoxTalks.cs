#region

#endregion

namespace FocusXamarinMobileApplication.Models;

public class ToolBoxTalks : BusinessEntityBase
{
    public int RemoteTableId { get; set; }
    public string Code { get; set; }
    public string FullName { get; set; }
    public string Frequency { get; set; }
    public string FileName { get; set; }
    public string Version { get; set; }
    public DateTime? DateOfUpload { get; set; }
    public string TrainingProvider { get; set; }
    public string TrainingType { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DateLastDistribution { get; set; }
}