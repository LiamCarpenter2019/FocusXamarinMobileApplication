using System;
using System.Collections.Generic;

namespace FocusXamarinMobileApplication.Models;

public class UtilityDamageData : IDisposable
{
    private bool _disposed;

    #region Variables

    #endregion

    #region Properties

    public List<InvestigationQuestion> Questions { get; set; }

    public List<Gang> GangList { get; set; }

    public List<UtilityCompany> UtilityCompany { get; set; }

    public List<PublicUtilityDamageQuestion> DamageType { get; set; }

    public List<InvestigateDamages> RegisteredDamages { get; set; }

    public List<OperativeDocument> Documents { get; set; }

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
        if (_disposed)
            return;

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