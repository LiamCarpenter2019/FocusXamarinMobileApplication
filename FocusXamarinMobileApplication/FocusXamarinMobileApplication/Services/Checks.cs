#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

[assembly: Xamarin.Forms.Dependency(typeof(Documents))]

namespace FocusXamarinMobileApplication.Services;

public class Checks : IChecks
{
    [Time]
    public List<Checks4Tablet> GetRelevantChecks(string passedType)
    {
        //CreateDBifNotExists("Checks4Tablet");       
        var checks2Return = new List<Checks4Tablet>();
        try
        {
            foreach (var item in App.Database.GetItems<Checks4Tablet>()
                         ?.Where(x => x.Type.ToLower().Trim() == passedType.ToLower().Trim()).ToList()
                         ?.Where(item => checks2Return.All(x => x.ListIndex != item.ListIndex)))
                checks2Return.Add(new Checks4Tablet
                {
                    Id = item.Id,
                    Type = item.Type,
                    CheckText = item.CheckText,
                    ButtonType = item.ButtonType,
                    ListIndex = item.ListIndex,
                    NotifiableResponse = item.NotifiableResponse
                });
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            foreach (var item in NavigationalParameters.DataPassedToTablet.ChecksDetail
                         .Where(x => x.Type.ToLower().Trim() == passedType.ToLower().Trim()).ToList())
                if (checks2Return.All(x => x.ListIndex != item.ListIndex))
                    checks2Return.Add(new Checks4Tablet
                    {
                        Id = item.Id,
                        Type = item.Type,
                        CheckText = item.CheckText,
                        ButtonType = item.ButtonType,
                        ListIndex = item.ListIndex,
                        NotifiableResponse = item.NotifiableResponse
                    });
        }

        return checks2Return.ToList();
        //var checks2Return = from x in allChecks
        //    where x.Type.ToLower().Trim() == passedType.ToLower().Trim()
        //    select ;
    }

    [Time]
    public List<Checks4TabletResponses> GetAllRelevantResponses(long passedPlantId, string qnumber)
    {
        CreateDBifNotExists("Checks4TabletResponses");

        var checksResponsesToReturn = new List<Checks4TabletResponses>();

        try
        {
            return App.Database.GetItems<Checks4TabletResponses>()?
                .Where(x => x.PlantId == passedPlantId && x.Qnumber == qnumber).ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return checksResponsesToReturn;
        }
    }

    [Time]
    public List<Checks4TabletResponses> GetAllRelevantResponses4Dalies(string qnumber, DateTime forWhatDate,
        string passedGangLeaderName)
    {
        var checkList = new List<Checks4TabletResponses>();
        try
        {
            var checks = App.Database.GetItems<Checks4TabletResponses>()
                .Where(x => x.Qnumber == qnumber
                            && x.RelevantDate.Date == forWhatDate.Date
                            && x.GangLeaderName == passedGangLeaderName)?
                .OrderByDescending(x => x.DateTimeOfCheck)?
                .ToList();


            foreach (var check in checks)
                if (check.QuestionId != 0)
                {
                    if (checkList.All(x => x.QuestionId != check.QuestionId)) checkList.Add(check);
                }
                else
                {
                    checkList.Add(check);
                }

            return checkList;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return new List<Checks4TabletResponses>();
        }
    }

    [Time]
    public List<Checks4TabletResponses> GetAllRelevantResponses4SelectedDate(long passedPlantId, string qnumber,
        DateTime date4Selection)
    {
        CreateDBifNotExists("Checks4TabletResponses");

        try
        {
            return App.Database.GetItems<Checks4TabletResponses>()?.Where(x =>
                x.PlantId == passedPlantId && x.Qnumber == qnumber &&
                x.DateTimeOfCheck.Date == date4Selection.Date).ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return new List<Checks4TabletResponses>();
        }
    }

    [Time]
    public List<Checks4TabletResponses> GetAllRelevantResponses4SelectedDate(long passedPlantId,
        DateTime date4Selection)
    {
        try
        {
            var xx = App.Database.GetItems<Checks4TabletResponses>().Where(x =>
                    x.PlantId == passedPlantId && x.RelevantDate.Date == date4Selection.Date)
                .OrderByDescending(x => x.DateTimeOfCheck).ToList();

            return xx;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ex} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return new List<Checks4TabletResponses>();
        }
    }

    [Time]
    public List<Checks4TabletResponses> GetAllRelevantResponses4_2day()
    {
        try
        {
            return App.Database.GetItems<Checks4TabletResponses>()?
                .Where(x => x.DateTimeOfCheck.Date == DateTime.Now.Date).ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return new List<Checks4TabletResponses>();
        }
    }

    [Time]
    public string GetRelevantChecksResponse(int passedListIndex)
    {
        try
        {
            var allChecksResponses = App.Database.GetItems<Checks4TabletResponses>();
            if (allChecksResponses == null) return "";

            var checksResponse2Return = "";
            foreach (var item in allChecksResponses.Where(item => item.QuestionId == passedListIndex))
                return item.QuestionResponse.ToLower();

            return checksResponse2Return;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return "0";
        }
    }

    [Time]
    public void AddChecks(List<Checks4Tablet> passedChecksData)
    {
        App.Database.SaveItems(passedChecksData);
    }

    [Time]
    public void AddChecksResponses(List<Checks4TabletResponses> passedChecksData)
    {
        try
        {
            App.Database.SaveItems(passedChecksData);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }
    }

    [Time]
    public void DeleteAllResponsesExcept4Today()
    {
        var resultList = new List<Checks4TabletResponses>();
        var all2daysResponses = GetAllRelevantResponses4_2day();
        _ = ClearAllRows("Checks4TabletResponses");
        // Now save any responses for today back
        foreach (var item in all2daysResponses)
        {
            item.Id = 0;

            if (resultList.All(x => x.ServerId != item.ServerId))
            {
                resultList.Add(item);
                AddSingleChecksResponse(item);
            }

            //else {
            //    var error = "Duplicate";
            //}
        }

        //  var responses = GetAllPlantResponses();
    }

    [Time]
    public bool CreateDBifNotExists(string whichTable)
    {
        var returnValue = true;
        try
        {
            if (whichTable == "Checks4Tablet")
                App.Database.CreateTable<Checks4Tablet>(); //create table
            else
                App.Database.CreateTable<Checks4TabletResponses>(); //create table
        }
        catch (Exception)
        {
            return false;
        }

        return returnValue;
    }

    [Time]
    public async Task ClearAllRows(string whichTable)
    {
        if (whichTable == "Checks4Tablet")
            App.Database.ClearTable<Checks4Tablet>();
        else
            try
            {
                var checkresponses = App.Database.GetItems<Checks4TabletResponses>()?
                    .Where(x => x.RelevantDate.Date < DateTime.Now.Date).ToList();

                if (checkresponses != null && checkresponses.Any())
                    foreach (var item in checkresponses)
                        App.Database.DeleteItem(item);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
    }

    [Time]
    public List<Checks4TabletResponses> GetAllPlantResponses()
    {
        try
        {
            return App.Database.GetItems<Checks4TabletResponses>()?.Where(x => x.PlantId > 0).ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex;
            return null;
        }
    }

    [Time]
    public void AddSingleChecksResponse(Checks4TabletResponses passedChecksData)
    {
        try
        {
            App.Database.SaveItem(passedChecksData);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }
    }

    [Time]
    public Checks4TabletResponses GetSingleSampleOfAnyChecks(long plantId,
        List<Checks4TabletResponses> allCurrentResponses)
    {
        var returnInfo = new Checks4TabletResponses();
        if (allCurrentResponses == null || allCurrentResponses.Count < 1) return returnInfo;

        return (from s in allCurrentResponses where s.PlantId == plantId select s).First();
    }

    [Time]
    public string CheckChecksOnCurrentItem(Plant4Tablet currentItemSelected, List<Checks4Tablet> allChecksRequired,
        List<Checks4TabletResponses> allCurrentResponses)
    {
        try
        {
            if (allCurrentResponses == null || allCurrentResponses.Count < 1) return "ChecksIncomplete";

            var sampleOneItem = allCurrentResponses.Where(s => s.PlantId == currentItemSelected.RemotePlantId)
                .First();

            if (sampleOneItem.ServerId > 0) return "ChecksSent";

            if (sampleOneItem.ChecksStatus != "ChecksIncomplete") return sampleOneItem.ChecksStatus;

            // OK now need to cycle through AllChecksRequired & compare with AllCurrentResponses to see if all checks have been completed
            var allTheChecksForThisItemType =
                allChecksRequired.Where(a => a.Type == currentItemSelected.Type).ToList();
            foreach (var _ in
                     //  from a in allChecksRequired where a.Type == currentItemSelected.Type select a;
                     from checkItem in allTheChecksForThisItemType //var acr = (from a in allCurrentResponses
                     //  where 
                     //select a).ToList();
                     where !allCurrentResponses.Any(a => a.QuestionId == checkItem.ListIndex &&
                                                         a.PlantId == currentItemSelected.RemotePlantId)
                     select new { })
                return "ChecksIncomplete";

            AddChecksResponses(allCurrentResponses.Where(a => a.PlantId == currentItemSelected.RemotePlantId)
                .ToList());

            return "AllChecksCompleted";
        }
        catch
        {
            var n = 1;
        }

        return "";
    }

    [Time]
    public string CheckChecksOnAllItems(List<Plant4Tablet> allPlantItems, List<Checks4TabletResponses> allResponses)
    {
        var countOfItems = 0;
        try
        {
            if (allPlantItems == null || allPlantItems.Count <= 0 || allResponses == null ||
                allResponses.Count <= 0) return "";

            foreach (var _ in from plantItem in allPlantItems
                     from checkItem in allResponses
                     where checkItem.PlantId == plantItem.RemotePlantId
                     where checkItem.ChecksStatus == "ChecksConfirmed"
                     select new { })
            {
                countOfItems++;
                break;
            }

            if (countOfItems > 0) return "ChecksConfirmed";
        }
        catch
        {
            var x = 1;
        }

        return "";
    }

    [Time]
    public void DeleteAllInstancesOfThisQuestionFromDb(int questionNo, int plantId, string qnumber)
    {
        try
        {
            var itemsTodelete = App.Database.GetItems<Checks4TabletResponses>()?.Where(a => a.QuestionId == questionNo
                    && a.PlantId == plantId
                    && a.DateTimeOfCheck.Date ==
                    DateTime.Now.Date
                    && a.Qnumber == qnumber && a.ChecksStatus != "ChecksPosted")
                .ToList();


            if (itemsTodelete != null && itemsTodelete.Any()) App.Database.DeleteItems(itemsTodelete);
        }
        catch (Exception)
        {
            var n = 1;
        }
    }

    [Time]
    public List<Checks4TabletResponses> DeleteAllResponsesForThisPlantIdFromDb4Today(int plantId,
        List<Checks4TabletResponses> allCurrentResponses, string qnumber)
    {
        try
        {
            var itemsTodelete = allCurrentResponses
                .Where(a => a.PlantId == plantId && a.DateTimeOfCheck.Date < DateTime.Now.Date).ToList();

            App.Database.DeleteItems(itemsTodelete);
        }
        catch (Exception)
        {
            var n = 1;
        }

        return GetAllRelevantResponses(plantId, plantId > 0 ? "" : qnumber);
    }

    [Time]
    public void DeleteAllInstancesOfThisPlantIdFromDb(int plantId)
    {
        try
        {
            App.Database.DeleteChecks4TabletResponses(plantId);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    [Time]
    public async Task ChangeCurrentStatus4PlantId(Plant4Tablet currentItemSelected,
        List<Checks4TabletResponses> allCurrentResponses, string newStatus, Person selectedUser)
    {
        try
        {
            foreach (var item in allCurrentResponses.Where(a => a.PlantId == currentItemSelected.RemotePlantId)
                         .ToList())
            {
                item.ChecksStatus = newStatus;
                item.PlantCheckedByName = selectedUser.FullName;

                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR
                    && NavigationalParameters.CurrentSelectedJob == null)
                    item.GangLeaderName = selectedUser.FullName;
            }

            AddChecksResponses(allCurrentResponses);
        }
        catch (Exception)
        {
            var n = 1;
        }
    }

    [Time]
    public async Task ChangeCurrentStatus4Dailies(JobData4Tablet currentSelectedJob,
        List<Checks4TabletResponses> allCurrentResponses, string newStatus)
    {
        try
        {
            foreach (var item in allCurrentResponses
                         .Where(a => a.Qnumber == currentSelectedJob.QuoteNumber.ToString())
                         .ToList()) item.ChecksStatus = newStatus;

            AddChecksResponses(allCurrentResponses);
        }
        catch (Exception)
        {
            var n = 1;
        }
    }

    [Time]
    public List<Checks4TabletResponses> GetAdditionalResponses()
    {
        return App.Database.GetItems<Checks4TabletResponses>()
            ?.Where(x => x.Qnumber == NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString()
                         && x.QuestionId == 0.10M
                         && x.RelevantDate == NavigationalParameters.CurrentSelectedJob.JobDate
                         && x.ServerId == 0)
            ?.ToList();
    }

    [Time]
    public void DeleteSingleResponse(Checks4TabletResponses currentDailyCheckAnswer)
    {
        App.Database.DeleteItem(currentDailyCheckAnswer);
    }

    [Time]
    public void DeleteAllInstancesOfThisQuestionFromDb(Checks4TabletResponses passedData)
    {
        var itemsToDeletes = App.Database.GetItems<Checks4TabletResponses>()?.Where(x =>
            x.Qnumber == passedData.Qnumber &&
            x.RelevantDate.Date == DateTime.Now.Date &&
            x.GangLeaderName == passedData.GangLeaderName
            && x.QuestionId == passedData.QuestionId).ToList();

        foreach (var item in itemsToDeletes) App.Database.DeleteItem(item);
    }

    [Time]
    public bool CheckDailyChecksComplete()
    {
        var checks = App.Database.GetItems<Checks4Tablet>()
            ?.Where(x => x.Type == "DailyCheck" && x.ButtonType.ToLower() != "button").ToList();

        var responses = GetAllRelevantResponses4Dalies(
            NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
            NavigationalParameters.CurrentSelectedJob.JobDate,
            NavigationalParameters.CurrentSelectedJob.GangLeaderName);

        foreach (var check in checks)
            if (responses.Count > 0)
            {
                if (responses.All(x => x.QuestionId != check.ListIndex)) return false;

                return true;
            }
            else
            {
                return false;
            }

        return true;
    }

    internal List<SurveyQuestion> GetMyDailyChecks()
    {
        try
        {
            return App.Database.GetItems<SurveyQuestion>()?
                .Where(x => x.Category == "DailyCheck" && x.QuestionOptions != "Button")
                .ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.Questions
                .Where(x => x.Category == "DailyCheck" && x.QuestionOptions != "Button")
                .ToList();
        }
    }

    internal List<Checks4Tablet> GetAllChecks()
    {
        CreateDBifNotExists("Checks4Tablet");

        return App.Database.GetItems<Checks4Tablet>().ToList();
        ;
    }

    internal List<string> GetChecksStatus(string pipedListOfOperativeIds, string qnumber2Check4Dailies,
        string PlantOrDailies)
    {
        var returnInfo = new List<string>();

        switch (PlantOrDailies)
        {
            case "Plant":
            {
                var currentPlant = new Plant();
                var myPlant = currentPlant.GetPlant4AllTheseUsers(pipedListOfOperativeIds); // GetMyPlant(userId);

                returnInfo.AddRange(from plantItem in myPlant
                    let n = GetAllRelevantResponses4SelectedDate(plantItem.RemotePlantId, DateTime.Now)
                    select plantItem.RemotePlantId + "|" +
                           GetSingleSampleOfAnyChecks(plantItem.RemotePlantId,
                                   GetAllRelevantResponses4SelectedDate(plantItem.RemotePlantId, DateTime.Now))
                               .ChecksStatus);
                break;
            }

            default:
            {
                // Now sort out dailies
                var c4T = GetMyDailyChecks();
                if (c4T != null && c4T.Count > 0)
                {
                    var singleId = c4T[0].Id;
                    returnInfo.Add("D||" + GetSingleSampleOfAnyChecks(0,
                            GetAllRelevantResponses4SelectedDate(0, qnumber2Check4Dailies,
                                DateTime.Now))
                        .ChecksStatus);
                }

                break;
            }
        }

        return returnInfo;
    }


    #region Commented Out Code

    //public List<Pictures4Tablet> GetAllPictures()
    //{
    //    return App.Database.GetItems<Pictures4Tablet>().ToList();
    //}

    //public async Task<int> SyncJobPictures2Tablet(long strQnumber, int LoggedOnUserId)
    //{
    //    int strReturnValue = -1;

    //    var _myPics4thisJob = GetJobPictures(strQnumber.ToString(), LoggedOnUserId.ToString());

    //    if (_myPics4thisJob != null && _myPics4thisJob.Count > 0)
    //    {
    //        strReturnValue = _myPics4thisJob.Count;

    //        LocalStorage ls = new LocalStorage();
    //        //Make sure pics exist on Tablet & get them if not
    //        foreach (var item in _myPics4thisJob)
    //        {
    //            if (item != null)
    //            {
    //                if (await ls.DoesJobPictureExistOnTablet(item.FileName))
    //                {
    //                    await ls.UpdateFileFromAzure(item.FileName, "JobPictures", "JobPictures/");
    //                }
    //            }
    //        }
    //    }
    //    return strReturnValue;
    //}

    //public async Task<string> SyncJobPicture2Azure(string Filename)
    //{
    //    string returnCheck = "Pic Transferred OK";
    //    byte[] imageFromLocalStorage = null;
    //    LocalStorage ls = new LocalStorage();
    //    var picExists = await ls.DoesJobPictureExistOnTablet(Filename);
    //    if (picExists)
    //    {
    //        // Doesnt Exist so need to return with error
    //        return "Picture doesnt exist on tablet!";
    //    } else
    //    {
    //        // Pic exists so go get it into a byte array
    //        imageFromLocalStorage = await ls.GetImageFromLocalstorage("JobPictures", Filename);
    //        if (imageFromLocalStorage.Length > 1000)
    //        {
    //            await ls.Save2AzureBlobByByteArray(imageFromLocalStorage, $"JobPictures/{Filename}", Constants.FocusDataContainerInAzure.ToString());

    //        } else
    //        {
    //            return "Picture doesnt exist on tablet!";
    //        }

    //    }
    //    return returnCheck;
    //}

    #endregion
}