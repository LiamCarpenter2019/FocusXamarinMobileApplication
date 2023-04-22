#region

using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using FocusXamarinMobileApplication.Services.Interfaces;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Constants = FocusXamarinMobileApplication.Services.Constants;

#endregion

namespace FocusXamarinMobileApplication.database;

public class FocusMobileV3Database
{
    
    public static string Root { get; set; }
    public int BeforePictures4TabletCount { get; set; }
    public int AfterPictures4TabletCount { get; set; }
    public int BeforeSurveyAnswersCount { get; set; }
    public List<VMexpansionReleaseData> BeforeVMexpansionReleaseDataCount { get; set; }
    public List<VMexpansionReleaseData> AfterVMexpansionReleaseDataCount { get; set; }
    public List<ProjectWorks> BeforeProjectWorksCount2 { get; set; }
    public List<ProjectWorks> BeforeProjectWorksCount { get; set; }
    public List<ProjectWorks> AftereProjectWorksCount { get; set; }
    public List<Blockage> BeforeBlockageCount { get; set; }
    public List<Blockage> AfterBlockageCount { get; set; }
    public List<VisitorLog> BeforeVisitorLogCount { get; set; }
    public List<VisitorLog> AfterVisitorLogCount { get; set; }
    public int BeforeLabourCount { get; set; }
    public int AfterLabourCount { get; set; }
    public string XContractReference { get; set; }
    public int BeforeCount { get; set; }
    public int AfterCount { get; set; }
    public List<ProjectWorks> AfterProjectWorksCount { get; set; }
    public List<ProjectWorks> AftereProjectWorksCount2 { get; set; }

    public static SQLiteConnection Connection { get; set; }

    public FocusMobileV3Database()
    {
        try
        {
            if (Connection == null)
            {
                Connection = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            }
            Connection.CreateTable<ApiStructure>();

            Connection.CreateTable<AuthenticatedUser>();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var y = ex.Message + " ...... " + ex.StackTrace;
        }
    }

    [Time]
    public async Task<bool> BackupDatabase(string path)
    {
        try
        {
            var rootFolder = PCLStorage.FileSystem.Current.LocalStorage;
            var ls = new LocalStorage();
            var cv2 = new Constants();
            var file = await rootFolder.GetFileAsync(path);

            using (var inStream = await file.OpenAsync(PCLStorage.FileAccess.Read))
            {
                var memStream = new MemoryStream();
                await inStream.CopyToAsync(memStream);

                if (inStream.Length > 1000)
                {
                    var dateTimex = DateTime.Now.ToString();
                    var dateTime = dateTimex.Replace('/', '_');
                    await ls.Save2AzureBlobByByteArray(memStream.ToArray(), $"TabletDbBackup/{Constants.DatabaseFilename}_{NavigationalParameters.LoggedInUser.FullName}_{dateTime}).",
                        cv2.FocusDataContainerInAzure);
                }
            }

            try
            {
                var picToDeletePath = path.Replace("Documents/FocusMobileV3Database.", "Library/JobPictures");
                DirectoryInfo picturesToDelete = new DirectoryInfo(picToDeletePath);

                var picList = picturesToDelete?.GetFiles();
                if (picList.Any())
                {
                    foreach (FileInfo picture in picList)
                    {
                        picture.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            try
            {
                var docsToDeletePath = path.Replace("Documents/FocusMobileV3Database.", "Library/JobPackFiles");
                DirectoryInfo documentsToDelete = new DirectoryInfo(docsToDeletePath);
                var docList = documentsToDelete?.GetFiles();
                if (docList.Any())
                {
                    foreach (FileInfo document in docList)
                    {
                        document.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }        

 
         
            return true;
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
            return false;
        }
       
    }

    public void ClearPlantDocuments()
    {
        //var beforeCount = _database.Table<Docs4Tablet>()?.ToList().Count();
        Connection.Query<Docs4Tablet>("delete from Docs4Tablet where Section = 'PlantDoc'");
        // var afterCount = _database.Table<Docs4Tablet>()?.ToList().Count();
    }

    public void ClearSavedInvestigations()
    {
        try
        {
            var investigations = GetItems<DamageToInvestigate>()?.Where(x => x.RemoteTableId > 0)?.ToList();

            if (investigations != null)
                foreach (var investigation in investigations)
                {
                    var id = Convert.ToInt64(investigation.InvestigationId);
                    var detailsToRemove = GetItems<Investigation>()
                        .Where(x => x.RemoteTabledId == id && x.IsUpdatedToServer)?.ToList();
                    // _database.Execute("delete from Investigation where RemoteTabledId = investigation.InvestigationId And IsUpdatedToServer = true");
                    if (detailsToRemove != null)
                        DeleteItems(detailsToRemove);

                    DeleteItem(investigation);
                }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
    {
        return Connection.Table<T>();
    }


    public T GetItem<T>(int id) where T : IBusinessEntity, new()
    {
        return Connection.Table<T>().FirstOrDefault(x => x.Id == id);
    }


    [Time]
    public void CreateTable<T>() where T : IBusinessEntity
    {
        try
        {
            Connection.CreateTable<T>();
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    public void ExecuteString(string sqlCommand)
    {
        Connection.Execute(sqlCommand);
    }

    public void SaveInvestigation(Investigation damageInvestigated)
    {
        Investigation investigationOnPad = null;

        var investigationId = Convert.ToInt32(damageInvestigated?.RemoteTabledId);

        if (investigationId > 0)
            //var investigations = da
            investigationOnPad = GetItems<Investigation>()
                .FirstOrDefault(x => x.RemoteTabledId.ToString() == investigationId.ToString());

        //if (damageInvestigated.RiddorDate.HasValue)
        //{
        //    var time = damageInvestigated.RiddorDate.Value.TimeOfDay;

        //    damageInvestigated.RiddorTime = time;
        //}

        if (investigationOnPad != null && investigationOnPad?.IsUpdatedToServer == true) DeleteItem(investigationOnPad);

        SaveItem(damageInvestigated);
    }

    /// <summary>
    ///     Save and update item of type T. If ID is set then will update the item, eelse creates new and returns the id.
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    /// <param name="item">Item to save or update</param>
    /// <returns>ID of item</returns>
    public int SaveItem<T>(T item) where T : IBusinessEntity
    {
        try
        {
            if (item.Id == 0) return Connection.Insert(item);

            Connection.Update(item);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            Console.WriteLine(ex.ToString());
        }

        return item.Id;
    }

    public void DeleteChecks4TabletResponses(int id)
    {
        var count = GetItems<Checks4TabletResponses>().Where(x => x.PlantId == id).Count();
        Connection.Query<Checks4TabletResponses>("delete from Checks4TabletResponses where PlantId = ?", id);
        var count2 = GetItems<Checks4TabletResponses>().Where(x => x.PlantId == id).Count();
    }

    public void SaveItems<T>(IEnumerable<T> items) where T : IBusinessEntity
    {
        foreach (var item in items) SaveItem(item);
    }

    public int DeleteItem<T>(T item) where T : IBusinessEntity, new()
    {
        return Connection.Delete(item);
    }

    public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
    {
        return Connection.Delete<T>(id);
    }

    public int DeleteItems<T>(List<T> items) where T : IBusinessEntity, new()
    {
        foreach (var item in items)
        {
            Connection.Delete<T>(item);
            return 1;
        }

        return 0;
    }

    public void SaveSupervisorDiaries(UserDailyTime labourFile)
    {
        try
        {
            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SUPERVISORINPUTDIARIES)
            {
                App.Database.SaveItem(labourFile);

                foreach (var item in labourFile.UserDailyTimeNotesList) App.Database.SaveItem(item);
            }
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PROJECTDIARIES)
            {
                foreach (var projectDiary in labourFile?.UserDailyProjectTimesList)
                    if (projectDiary != null)
                    {
                        App.Database.SaveItem(projectDiary);

                        foreach (var projectNotes in projectDiary.DailyProjectNotesList)
                            App.Database.SaveItem(projectNotes);
                    }
            }
        }
        catch (Exception exc)
        {
            var error = exc.ToString();
        }
    }

    public void UpdateInputDiariesFile(ClaimedNotesFile diaryInfo)
    {
        try
        {
            SaveItem(diaryInfo);
        }
        catch (Exception exc)
        {
            var error = exc.ToString();
        }
    }


    /// <summary>
    ///     Clear an entire table of specific type
    /// </summary>
    /// <typeparam name="T">Type to clear table</typeparam>
    public void ClearTable<T>() where T : IBusinessEntity, new()
    {
        Connection.Execute($"delete from {typeof(T).Name}");

        var itemsTodelete = Connection.Table<T>()?.ToList();

        //if (itemsTodelete != null && itemsTodelete?.Count() > 0)
        //    foreach (var item in itemsTodelete)
        //        DeleteItem(item);
        //var afterCount = Connection.Table<T>()?.ToList().Count();
    }

    public void ClearEndpoints()
    {
        Connection.Execute("delete from VMexpansionReleaseData where SavedToServer = true and RemoteTableId <> 0");
        var endpoints = Connection.Table<VMexpansionReleaseData>().Where(x => string.IsNullOrEmpty(x.BuildingStandard))
            .ToList();

        foreach (var endpoint in endpoints)
        {
            endpoint.BuildingStandard = "chamber";
            SaveItem(endpoint);
        }

        var eecount = Connection.Table<VMexpansionReleaseData>().ToList();
    }

    public void ClearSavedRemoteData<T>() where T : IBusinessEntity, new()
    {
        try
        {
            var beforexCount = Connection.Table<T>()?.ToList() ?? new List<T>();

            if (beforexCount?.Count() > 0)
            {
                switch (typeof(T).Name)
                {
                    case "Assignment":
                        try
                        {
                            Connection.Execute("delete from Assignment where AssignmentId = ? Or RemoteTableId > 0",
                                Guid.Empty);

                            //var assignmentsTodelete = GetItems<Assignment>().Where(x => x.AssignmentId == Guid.Empty ||
                            //    x.RemoteTableId > 0).ToList();

                            //foreach (var assignment in assignmentsTodelete) DeleteItem(assignment);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "Pictures4Tablet":
                        try
                        {
                            Connection.Execute("delete from Pictures4Tablet where SeverPictureId > 0");

                            //var picsTodelete = GetItems<Pictures4Tablet>().Where(x => x.SeverPictureId > 0).ToList();

                            //foreach (var pic in picsTodelete) DeleteItem(pic);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "injuredPerson":
                        try
                        {
                            Connection.Execute("delete from InjuredPerson where RemoteTableId > 0");

                            //var injuredPersons = GetItems<InjuredPerson>().Where(x => x.RemoteTableId != 0).ToList();

                            //foreach (var injuredPerson in injuredPersons) DeleteItem(injuredPerson);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "Witness":
                        try
                        {
                            Connection.Execute("delete from Witness where RemoteTableId > 0");

                            //var witnesses = GetItems<Witness>().Where(x => x.RemoteTableId != 0).ToList();

                            //foreach (var witness in witnesses) DeleteItem(witness);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "ExternalPersonnel":
                        try
                        {
                            Connection.Execute("delete from ExternalPersonnel where RemoteTableId > 0");

                            //var externalPersonnels = GetItems<ExternalPersonnel>().Where(x => x.RemoteTableId != 0).ToList();

                            //foreach (var externalPersonnel in externalPersonnels) DeleteItem(externalPersonnel);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "Location":
                        try
                        {
                            var x = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from Location where RemoteTableId > 0");

                            //var externalPersonnels = GetItems<ExternalPersonnel>().Where(x => x.RemoteTableId != 0).ToList();

                            //foreach (var externalPersonnel in externalPersonnels) DeleteItem(externalPersonnel);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "Blockage":
                        try
                        {
                            var xx = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from Blockage where RemoteTableId > 0");

                            //var externalPersonnels = GetItems<ExternalPersonnel>().Where(x => x.RemoteTableId != 0).ToList();

                            //foreach (var externalPersonnel in externalPersonnels) DeleteItem(externalPersonnel);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "PublicUtilityDamageQuestion":
                        try
                        {
                            var xxx = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from PublicUtilityDamageQuestion");

                            //var externalPersonnels = GetItems<ExternalPersonnel>().Where(x => x.RemoteTableId != 0).ToList();
                            var xxx2 = Connection.Table<T>()?.ToList();
                            //foreach (var externalPersonnel in externalPersonnels) DeleteItem(externalPersonnel);
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "Labour":
                        try
                        {
                            var xxx = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from Labour where RemoteTableId > 0 and ApprovedBySupervisor = ?",
                                NavigationalParameters.MinDateTime);

                            var yy = Connection.Table<T>()?.ToList();
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "TaskItem":
                        try
                        {
                            var xxx = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from TaskItem");

                            var yy = Connection.Table<T>()?.ToList();
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "StockFuelDTO":
                        try
                        {
                            var xxx = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from StockFuelDTO");

                            var yy = Connection.Table<T>()?.ToList();
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    case "NCR":
                        try
                        {
                            var xxx = Connection.Table<T>()?.ToList();

                            Connection.Execute("delete from NCR where  RemoteTableId > 0");

                            var yy = Connection.Table<T>()?.ToList();
                        }
                        catch (Exception ex)
                        {
                            var error = ex.ToString();
                        }
                        break;
                    default:
                        var xxxx = Connection.Table<T>()?.ToList();

                        Connection.Execute($"Delete from {typeof(T).Name} where RemoteTableId != 0");
                        break;
                }
            }

            var afterxCount = Connection.Table<T>()?.Count();
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            var xx = 1;
        }
      
    }

    public void SaveClaimedFileItem(ClaimedFile claimedFile)
    {
        var claim = Connection.Table<ClaimedFile>()
            .FirstOrDefault(x => x.QuoteId == claimedFile.QuoteId
                                 && x.ClaimGang == claimedFile.ClaimGang
                                 && x.ClaimDate == claimedFile.ClaimDate
                                 && x.ClaimId == claimedFile.ClaimId
                                 && x.ClaimHeader == claimedFile.ClaimHeader);

        try
        {
            if (claim != null)
            {
                claimedFile.Id = claim.Id;
                Connection.Update(claimedFile);
            }
            else
            {
                Connection.Insert(claimedFile);
            }

            var claim2 = Connection.Table<ClaimedFile>()
                .FirstOrDefault(x => x.QuoteId == claimedFile.QuoteId
                                     && x.ClaimGang == claimedFile.ClaimGang
                                     && x.ClaimDate == claimedFile.ClaimDate
                                     && x.ClaimId == claimedFile.ClaimId
                                     && x.ClaimHeader == claimedFile.ClaimHeader);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    public void UpdateSavedItems(DataPassed2Server dailyMeasures)
    {
        try
        {
            foreach (var item in dailyMeasures.Assignments)
            {
                Connection.Query<VMexpansionReleaseData>(
                    "Update VMexpansionReleaseData set SavedToServer = true, RemoteTableId = 1 where QNumber = ? and StreetName = ?",
                    $"{dailyMeasures.JobData.QuoteNumber}", $"{item.StreetName}");

                Connection.Query<SurveyAnswers>("update SurveyAnswers set RemoteTableId = 1 where AssignmentId = ?",
                    $"{item.AssignmentId}");

                Connection.Query<Pictures4Tablet>("update Pictures4Tablet set SeverPictureId = 1 where AssignmentId = ?",
                    $"{item.AssignmentId}");

                Connection.Query<ProjectWorks>("delete from ProjectWorks where AssignmentId = ?",
                    $"{item.AssignmentId}");

                Connection.Query<SurveyComments>("delete from  SurveyComments where AssignmentId = ?",
                    $"{item.AssignmentId}");

                Connection.Query<Dfe>("delete from Dfe where AssignmentId = ?", $"{item.AssignmentId}");

                Connection.Query<Measure>("delete from Measure where AssignmentId = ?", $"{item.AssignmentId}");

                Connection.Query<DigPermit>("delete from DigPermit where AssignmentId = ?", $"{item.AssignmentId}");

                Connection.Query<Cvi>("delete from Cvi where AssignmentId = ?", $"{item.AssignmentId}");

                if (item.Audit != null)
                {
                    Connection.Query<Ncr>("update Ncr set RemoteTableId = 1 where AuditId = ?", $"{item.AssignmentId}");
                    item.Audit.RemoteTableId = 1;
                    SaveItem(item.Audit);
                }

                Connection.Query<VMl4CabDetail>("delete From VMl4CabDetail Where L4Number is ?", $"{item.LocationName}");

                Connection.Query<Assignment>("update Assignment Set complete = true,RemoteTableId = 1 where AssignmentId = ?", $"{item.AssignmentId}");

                //App.Database.DeleteItem(item);
            }

            try
            {
                if (dailyMeasures.JobData != null)
                {
                    Connection.Query<ProjectWorks>("Update ProjectWorks set RemoteTableId = 1 where QuoteId = ?",
                        $"{dailyMeasures.JobData?.QuoteNumber}");

                    Connection.Query<VisitorLog>(
                        "Update VisitorLog set RemoteTableId = 1 where RemoteTableId = 0 and ContractReference is ?",
                        $"{dailyMeasures.JobData?.ContractReference}");

                    Connection.Query<CableStockUse>(
                        "Update CableStockUse set RemoteTableId = 1 where ContractName = ? And RemoteTableId = 0",
                        $"{dailyMeasures.JobData?.ProjectName}");

                    Connection.Query<Blockage>("Update Blockage set RemoteTableId = 1 where QNumber = ?",
                        $"{dailyMeasures.JobData?.QuoteNumber}");


                    Connection.Query<FuelConsumption>("Update FuelConsumption set RemoteTableId = 1");
                }

                foreach (var ep in dailyMeasures.EndPoints)
                {
                    Connection.Query<VMexpansionReleaseData>(
                        "Update VMexpansionReleaseData set SavedToServer = true where QNumber = ? And Streetname = ?",
                        $"{dailyMeasures.JobData?.QuoteNumber}", $"{ep.StreetName}");
                }

                foreach (var lf in dailyMeasures.LabourFiles) Connection.Delete(lf);

                foreach (var cf in dailyMeasures.ClaimedFiles) Connection.Delete(cf);

                if (dailyMeasures.DailyDiary != null)
                {
                    dailyMeasures.DailyDiary.RemoteTableId = 1;

                    dailyMeasures.DailyDiary.PostedByGanger = NavigationalParameters.ApprovedDate;

                    switch (NavigationalParameters.AppType)
                    {
                        case NavigationalParameters.AppTypes.SUPERVISOR:
                            Connection.Delete(dailyMeasures?.DailyDiary);
                            break;
                        default:
                            SaveItem(dailyMeasures.DailyDiary);
                            break;
                    }
                }

                if (dailyMeasures.BlockageList != null)
                    Connection.Query<Blockage>(
                        "Update Blockage set RemoteTableId = 1 where RemoteTableId = 0 and ImageId is ?",
                        $"{dailyMeasures?.BlockageList?.FirstOrDefault()?.ImageId}");
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }

    public int UpdateLabourFile(Labour labourFile)
    {
        var labour = GetItems<Labour>()?.FirstOrDefault(x => x.LabourOperative == labourFile?.LabourOperative
                                                             && x.ContractReference == labourFile.ContractReference
                                                             && x.LabourDate == labourFile.LabourDate);

        if (labour != null)
            SaveItem(labourFile);
        else
            Connection.Insert(labourFile);

        var labour2 = GetItems<Labour>()?.FirstOrDefault(x => x.LabourOperative == labourFile?.LabourOperative
                                                              && x.ContractReference == labourFile.ContractReference
                                                              && x.LabourDate == labourFile.LabourDate);

        return labourFile.Id;
    }

    public void UpdateSupervisorDailyProjectLabour(UserDailyTime supervisorDailyDiary)
    {
        try
        {
            var supervisorDailyLabour = GetItems<UserDailyTime>()
                ?.FirstOrDefault(x => x.RemoteId == supervisorDailyDiary?.RemoteId);

            if (supervisorDailyLabour?.UserDailyProjectTimesList == null) return;
            {
                foreach (var item in supervisorDailyDiary.UserDailyProjectTimesList)
                {
                    var itemToUpdate = GetItems<UserDailyProjectTimes>()
                        ?.FirstOrDefault(x => x.RemoteProjectTimeId == item?.RemoteProjectTimeId);

                    if (itemToUpdate != null)
                    {
                        //Connection.Update(item);
                    }
                    else
                    {
                        Connection.Insert(item);

                        if (item?.DailyProjectNotesList == null) continue;
                        foreach (var diary in item?.DailyProjectNotesList) Connection.Insert(diary);
                    }
                }
            }
        }
        catch (Exception exc)
        {
            var error = exc.ToString();
        }
    }

    public void DeleteAllItems(decimal questionId, Guid assignmentId)
    {
        var qnp1 = questionId.ToString().Split('.')?.First();
        var z = Convert.ToInt32(qnp1) + 1;
        //Connection.Execute(
        //          $"Delete From SurveyAnswers Where AssignmentId = {assignmentId} and QuestionId BETWEEN {questionId} and {z}");

        Connection.Query<SurveyAnswers>(
            "Delete From SurveyAnswers Where AssignmentId is ? AND QuestionId BETWEEN ? AND ?", $"{assignmentId}",
            $"{questionId}", $"{z}");
        //WHERE column_name BETWEEN value1 AND value2;

        //var answers = GetItems<SurveyAnswers>().Where(x => x.QuestionId > questionId && x.AssignmentId == assignmentId && x.QuestionId < z).ToList();

        //if (answers != null && answers.Count > 0)
        //{
        //    DeleteItems(answers);
        //}
    }


}