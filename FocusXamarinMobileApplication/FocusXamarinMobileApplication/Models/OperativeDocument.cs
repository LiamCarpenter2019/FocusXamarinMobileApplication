namespace FocusXamarinMobileApplication.Models;

[Table("Document")]
public class OperativeDocument
{
    [AutoIncrement] [PrimaryKey] public int Id { get; set; }

    public string DocumentId { get; set; }
    public string FolderPath { get; set; }
    public string FileName { get; set; }
    public string VisibleFileName { get; set; }
    public string ContractNumber { get; set; }
    public string QNumber { get; set; }
}