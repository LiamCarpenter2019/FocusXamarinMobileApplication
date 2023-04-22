namespace FocusXamarinMobileApplication.Services.Interfaces;

internal interface ILocalStorage
{
    //Task DownloadAndSaveFile(string FileName);

    //Task<bool> CheckFileExists(string path);

    //Task<string> GetFile(string fileName);

    //Task<bool> DeleteImage(string FilePath);
    Task<int> CheckIfDocExistsLocallyAndIfNotThenDownload(Docs4Tablet document, ClientContext clientContext);
}