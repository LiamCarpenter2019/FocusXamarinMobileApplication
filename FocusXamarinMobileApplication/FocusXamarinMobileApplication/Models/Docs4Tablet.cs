#region

#endregion

namespace FocusXamarinMobileApplication.Models;

//[Table("AllDocuments")]
public class Docs4Tablet : BusinessEntityBase
{
    //[AutoIncrement, PrimaryKey]
    //public int Id { get; set; }
    public string DocumentId { get; set; } = "";
    public string FolderPath { get; set; } = "";
    public string FileName { get; set; } = "";
    public string DocumentTitle { get; set; } = "";
    public string ContractNumber { get; set; } = "";
    public string QNumber { get; set; } = "";
    public DateTime UploadedDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public string DocType { get; set; } = "";
    public string OperativeId { get; set; } = "";
    public string PlantId { get; set; } = "";
    public string Section { get; set; } = "";
    public string DocLibraryPath { get; set; }
}