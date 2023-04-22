namespace FocusXamarinMobileApplication.Services;

//[Serializable]
public class S3Image : IDisposable
{
    #region Private Variables

    private bool _disposed;

    #endregion

    #region Constructor

    public S3Image()
    {
        FileSize = 0;
        FileName = string.Empty;
        File = null;
        MimeType = string.Empty;
        Extension = string.Empty;
    }

    #endregion

    #region Destructor

    ~S3Image()
    {
        Dispose(false);
    }

    #endregion

    #region Properties

    public long FileSize { get; set; }

    public string FileName { get; set; }

    public MemoryStream File { get; set; }

    public string MimeType { get; set; }

    public string Extension { get; set; }

    #endregion

    #region Disposable Stuff

    public void Dispose()
    {
        // Set the dispose logic here
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
            {
                // Dispose of your managed objects/code here
            }

        _disposed = true;
    }

    #endregion
}