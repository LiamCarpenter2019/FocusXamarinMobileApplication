#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using FocusXamarinMobileApplication.Services.Interfaces;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using Image = Xamarin.Forms.Image;

#endregion

[assembly: Xamarin.Forms.Dependency(typeof(Documents))]

namespace FocusXamarinMobileApplication.Services;

public class Pictures : IPictures
{
    public readonly LocalStorage ls;

    public Pictures()
    {
        ls = new LocalStorage();
    }

    [Time]
    public List<Pictures4Tablet> GetJobPictures(long jobQNumber, long loggedOnId)
    {
        var allPics4Jobs = App.Database.GetItems<Pictures4Tablet>();

        var allP2 = allPics4Jobs?.Where(x =>
            x.QNumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber
            && x.OperativeId == NavigationalParameters.LoggedInUser.FocusId
            && x.JobId == NavigationalParameters.CurrentSelectedJob.JobId).ToList();

        return allPics4Jobs.ToList();
    }

    [Time]
    public Task AddPictures(IEnumerable<Pictures4Tablet> passedPics)
    {
        return Task.Factory.StartNew(async () =>
        {
            CreateDBifNotExists();

            App.Database.SaveItems(passedPics.ToList());
        });
    }

    [Time]
    public bool CreateDBifNotExists()
    {
        var returnValue = true;

        try
        {
            App.Database.CreateTable<Pictures4Tablet>(); //create table
        }
        catch (Exception)
        {
            return false;
        }

        return returnValue;
    }

    [Time]
    public Task ClearAllRows()
    {
        return Task.Factory.StartNew(() => { App.Database.ClearTable<Pictures4Tablet>(); });
    }

    [Time]
    public List<Pictures4Tablet> GetAllPictures()
    {
        return App.Database.GetItems<Pictures4Tablet>().ToList();
    }

    [Time]
    public async Task AddPicture(Pictures4Tablet passedPic)
    {
        //var passedPic2 = App.Database.GetItems<Pictures4Tablet>().ToList();
        try
        {
            if (App.Database.GetItems<Pictures4Tablet>().All(x => x.FileName != passedPic.FileName))
            {
                App.Database.SaveItem(passedPic);
            }
            else
            {
                var pic = App.Database.GetItems<Pictures4Tablet>()
                    .FirstOrDefault(x => x.FileName == passedPic.FileName);
                pic.SeverPictureId = passedPic.SeverPictureId;

                if (!string.IsNullOrEmpty(pic.FileName)) App.Database.SaveItem(pic);
            }


            var passedPic3 = App.Database.GetItems<Pictures4Tablet>();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    public async Task<Image> GetUserProfilePictureAsync(string userProfilePicFileName)
    {
        var ls = new LocalStorage();


        var picExists = await ls.DoesJobPictureExistOnTablet("JobPictures", userProfilePicFileName);

        if (picExists)
            return null;
        return null;
    }

    [Time]
    public async Task<int> SyncJobPictures2Tablet(long strQnumber, long loggedOnUserId)
    {
        var strReturnValue = -1;

        var myPics4ThisJob = GetJobPictures(strQnumber, loggedOnUserId);

        if (myPics4ThisJob != null && myPics4ThisJob.Count > 0)
        {
            strReturnValue = myPics4ThisJob.Count;

            var ls = new LocalStorage();
            //Make sure pics exist on Tablet & get them if not
            foreach (var item in myPics4ThisJob.Where(item => item != null))
                if (await ls.DoesJobPictureExistOnTablet("JobPictures", item.FileName))
                    await ls.UpdateFileFromAzure(item.FileName, "JobPictures", "JobPictures/");
        }

        return strReturnValue;
    }

    [Time]
    public async Task<bool> DeleteJobPicture(Pictures4Tablet picture)
    {
        App.Database.DeleteItem(picture);
        var ls = new LocalStorage();
        await ls.RemoveImageFromLocalStorage("JobPictures", picture.FileName);
        var picExists = await ls.DoesJobPictureExistOnTablet("JobPictures", picture.FileName);
        if (!picExists) return false;

        return true;
    }

    [Time]
    public async Task<bool> DeleteJobPicture(string filename)
    {
        var pic = App.Database.GetItems<Pictures4Tablet>()?.FirstOrDefault(x => x.FileName == filename);
        if (pic != null)
        {
            App.Database.DeleteItem(pic);

            var ls = new LocalStorage();
            var picExists = await ls.DoesJobPictureExistOnTablet("JobPictures", filename);
            if (!picExists) return false;
        }

        return true;
    }

    [Time]
    public async Task<string> SyncPicture2Azure(string storageLocation, string azurePath, string filename)
    {
        var returnCheck = "Pic Transferred OK";
        byte[] imageFromLocalStorage = null;

        var picExists = await ls.DoesJobPictureExistOnTablet(storageLocation, filename);
        if (!picExists) return "Picture doesnt exist on tablet!";

        // Pic exists so go get it into a byte array
        imageFromLocalStorage = await ls.GetImageFromLocalstorage(storageLocation, filename); //JobPictures
        if (imageFromLocalStorage.Length > 1000)
        {
            var cv2 = new Constants();
            await ls.Save2AzureBlobByByteArray(imageFromLocalStorage, $"{azurePath}/{filename}",
                cv2.FocusDataContainerInAzure);
        }
        else
        {
            return "Picture doesnt exist on tablet!";
        }

        return returnCheck;
    }

    [Time]
    public async Task<bool> RemoveImageFromLocalStorage(Pictures4Tablet picToDelete)
    {
        return await ls.RemoveImageFromLocalStorage(picToDelete.PicturePath, picToDelete.FileName);
    }

    [Time]
    public List<Pictures4Tablet> GetInvestigationImages(Guid damageGuid)
    {
        var images = App.Database.GetItems<Pictures4Tablet>()?.Where(x => x.Identifier == damageGuid)?.ToList();
        return images;
    }
}