namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IDocuments
{
    DocumentData2Display GetDocumentById(string path, string documentType, string jobQNumber, long operativeId,
        string documentId, long plantId = 0);

    List<DocumentData2Display> GetDocuments(string path, string documentType, string jobQNumber, long operativeId,
        long plantId = 0);

    bool CreateDBifNotExists();
    void ClearAllRows();
    Task AddDocument(Docs4Tablet passedDoc);

    Task AddDocuments(List<Docs4Tablet> passedDocs);

    //Task<List<Docs4Tablet>> GetAllJobDocuments();
    List<Docs4Tablet> GetAllJobDocuments();
}