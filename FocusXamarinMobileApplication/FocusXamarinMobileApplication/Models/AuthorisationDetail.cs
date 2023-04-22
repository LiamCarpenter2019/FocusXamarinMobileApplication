#region

#endregion

using System;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Services;
using SQLite;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Models;

public class AuthorisationDetail : BusinessEntityBase
{
    //######### Used for permit 2 dig ( TODO remove )
    [Ignore] public Guid Identifier { get; set; }

    [Ignore] public string SignedBy { get; set; }

    [Ignore] public string OnBehalf { get; set; }

    /// <summary>
    ///     Gets or sets the name of the authorised user.
    /// </summary>
    /// <value>The name of the authorised.</value>
    public string AuthorisedName { get; set; }

    public string Email { get; set; }

    [Ignore] public ImageSource SignatureView { get; set; }

    public NavigationalParameters.AuthorisationTypes Type { get; set; }

    public long FocusId { get; set; } = 0;

    [Ignore] public byte[] SignatureImg { get; set; }

    public string SignatureFileName { get; set; }
    public string SignatureFolderName { get; set; } = "JobPictures";
    public bool AuthorisedSig { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = "Signature";

    public bool DetailsCorrect()
    {
        return SignedBy != null && SignatureImg != null ? true : false;
    }

    public async Task<bool> StoreSignature(byte[] img, string fileName)
    {
        var ls = new LocalStorage();

        //if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI) { SignatureFolderName = "CVIpics"; }

        await ls.StoreImagesLocallyAndUpdatePath(SignatureFolderName + "/", fileName, img);
        SignatureImg = await ls.GetImageFromLocalstorage(SignatureFolderName + "/", fileName);
        SignatureFileName = fileName;
        return true;
    }
}