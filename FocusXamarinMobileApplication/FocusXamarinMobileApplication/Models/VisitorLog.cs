#region

using System;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Services;
using SQLite;
using Constants = FocusXamarinMobileApplication.Services.Constants;

#endregion

namespace FocusXamarinMobileApplication.Models;

public class VisitorLog : BusinessEntityBase
{
    public long RemoteTableId { get; set; } = 0;

    public string GangLeader { get; set; } = "";
    public string ContractReference { get; set; } = "";
    public DateTime DateLogged { get; set; } = DateTime.Parse("1/1/1900 00:00:00");
    public string Forename { get; set; } = "";
    public string Surname { get; set; } = "";
    public string Organisation { get; set; } = "";
    public string VehicleReg { get; set; } = "";
    public string Reason4Visit { get; set; } = "";
    public DateTime OnSiteTime { get; set; } = DateTime.Parse("1/1/1900 00:00:00");
    public DateTime? OffSiteTime { get; set; }
    public bool SwitchIn { get; set; } = false;
    public bool SwitchOut { get; set; }
    public string Comments { get; set; } = "";
    public long? GangLeaderId { get; set; }
    public bool? Question1 { get; set; } = false;
    public bool? Question2 { get; set; } = false;
    public bool? Question3 { get; set; } = false;
    public bool? Question4 { get; set; } = false;
    public bool? Question5 { get; set; } = false;
    public Guid? VisitorLogGuid { get; set; } = Guid.NewGuid();
    public string SignatureIn { get; set; } = "";
    public string SignatureOut { get; set; } = "";

    [Ignore] public byte[] SignatureInImg { get; set; }

    [Ignore] public byte[] SignatureOutImg { get; set; }

    [Ignore] private string _fullName { get; set; }

    [Ignore]
    public string FullName
    {
        get => _fullName;
        set => _fullName = $"{Forename} {Surname} ";
    }

    [Ignore] public bool OnSite { get; set; }

    [Ignore] public bool OffSite { get; set; }

    public bool Question6 { get; internal set; }

    //[Ignore]  -  prevents the prop from being serialized
    public bool LogIn(string forename, string surname, string org, string reg, string reason, byte[] signature,
        bool? q1, bool? q2, bool? q3, bool? q4, bool? q5, bool? q6)
    {
        try
        {
            Forename = forename;
            Surname = surname;
            Organisation = org;
            VehicleReg = reg;
            Reason4Visit = reason;
            OnSiteTime = DateTime.Now;
            SignatureInImg = signature;

            return q1 != null && q2 != null && q3 != null && q4 != null && q5 != null && q6 != null &&
                   reg.Length <= 10 && CheckLogInDetails();
        }
        catch (Exception ex)
        {
            return false;
        }

        return false;
    }

    public bool LogOut(string comments, bool switchOut, byte[] signature)
    {
        SwitchOut = switchOut;
        Comments = comments;
        SignatureOutImg = signature;
        OffSiteTime = DateTime.Now;
        OnSite = false;
        OffSite = true;
        return CheckLogOutDetails();
    }

    //used to log out user if not present, no checks are made.
    public bool LogOut(string comments, DateTime logOutTime)
    {
        Comments = comments;
        OffSiteTime = logOutTime;
        OnSite = false;
        OffSite = true;

        return true;
    }

    public bool CheckLogInDetails()
    {
        string[] stringProps = { Forename, Surname, Organisation, Reason4Visit };
        foreach (var s in stringProps)
            if (s == "")
                return false;


        return SignatureInImg != null;
    }

    public bool CheckLogOutDetails()
    {
        return SignatureOutImg != null;
    }

    public bool ToBool(int num)
    {
        return num == 0 ? true : false;
    }

    /// <summary>
    ///     Stores the signature to the local storage in the JobPictures folder.
    /// </summary>
    /// <param name="img">Image.</param>
    public async void StoreImage(byte[] img, string fileName)
    {
        try
        {
            var ls = new LocalStorage();
            await ls.StoreImagesLocallyAndUpdatePath("JobPictures/", fileName, img);

            var imageFromLocalStorage = await ls.GetImageFromLocalstorage("JobPictures/", fileName); //JobPictures

            if (imageFromLocalStorage.Length > 1000)
            {
                var cv2 = new Constants();
                await ls.Save2AzureBlobByByteArray(imageFromLocalStorage, $"PicsFromIpad/{fileName}",
                    cv2.FocusDataContainerInAzure);
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }
}