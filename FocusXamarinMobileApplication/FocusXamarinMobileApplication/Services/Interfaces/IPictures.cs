namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IPictures
{
    List<Pictures4Tablet> GetJobPictures(long jobQNumber, long loggedOnId);
    bool CreateDBifNotExists();
    Task ClearAllRows();
    Task AddPictures(IEnumerable<Pictures4Tablet> passedPics);
}