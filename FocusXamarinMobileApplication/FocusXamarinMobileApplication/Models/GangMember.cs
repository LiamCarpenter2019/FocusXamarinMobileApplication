namespace FocusXamarinMobileApplication.Models;

public class GangMember : IDisposable
{
    private bool _disposed;

    #region Constructor

    public GangMember()
    {
        Id = 0;
        FirstName = string.Empty;
        Surname = string.Empty;
        HasChangedPin = false;
        IsUpdatedToServer = true;
        IsLoggedIn = false;
    }

    #endregion

    //[PrimaryKey, AutoIncrement, JsonIgnore]
    //public int MemberId { get; set; }

    //[ForeignKey(typeof(Gang)), JsonIgnore]
    //public int FKGangId { get; set; }

    //[ManyToOne, JsonIgnore]
    //public Gang Gang { get; set; }

    #region Private Variables

    #endregion

    #region Properties

    public long Id { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public string GangMemberPin { get; set; }

    public string MemberName { get; set; }
    public bool HasChangedPin { get; set; }
    public bool IsUpdatedToServer { get; set; }
    public bool IsLoggedIn { get; set; }

    public string FullName => Surname + ", " + FirstName;

    #endregion

    #region Disposable Stuff

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            // Free any other managed objects here.
            //
        }

        // Free any unmanaged objects here.
        //
        _disposed = true;
    }

    #endregion
}