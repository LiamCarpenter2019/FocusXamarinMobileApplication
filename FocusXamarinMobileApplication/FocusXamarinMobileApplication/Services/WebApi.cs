#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services.Interfaces;
using MethodTimer;
using Microsoft.AppCenter.Analytics;
using Event = FocusXamarinMobileApplication.Models.Event;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services;

public class WebApi : IWebApi
{
    private static MediaTypeWithQualityHeaderValue _json = new("application/json");
    private readonly Pictures _pictureService = new();

    private Uri uri { get; set; }

    public async Task<Tuple<Person, string, List<Assignment>>>
        LogonRequest(
            string loggedInUserAlias) //Task<DataPassed2XamarinTablets> LogonRequest(string LoggedInUserAlias)
    {
        var dataReturned = new DataPassed2XamarinTablets();

        //return new Tuple<Person, string, List<Assignment>>(null, "Good", new List<Assignment>()); //TODO COMMENT OUT - FOR TEST ONLY

        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "l"); // This is a Logon Request
            try
            {
                var json = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject("|-|-|" + loggedInUserAlias + "|-|-|"));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // var sw = Stopwatch.StartNew();
                var response = await client.PostAsync(uri, content);
                //Debug.WriteLine($"API Data Return: {sw.ElapsedMilliseconds / 1000 } seconds");

                //sw.Stop();
                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = await response.Content.ReadAsStringAsync();

                    try
                    {
                        dataReturned = await Task.Run(() =>
                            JsonConvert.DeserializeObject<DataPassed2XamarinTablets>(returnedDataFromServer));
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.ToString();
                    }

                    return await SortReturnedWebApiBaseData(dataReturned);
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return new Tuple<Person, string, List<Assignment>>(null, response.StatusCode.ToString(),
                        new List<Assignment>());
            }
            catch (Exception ex)
            {
                //  Debug.WriteLine(
                //   $"##########\nERROR:{ex.Message}-----{NavigationalParameters.LoggedInUser.FullName}---\n##########");
                Debug.WriteLine($"Error: {ex.Message} ");
            }
        }

        return new Tuple<Person, string, List<Assignment>>(null, "WebApi Fail", new List<Assignment>());
    }


    [Time]
    public async Task<long> SaveInvoiceToServer(Order order)
    {
        string json = null;
        //DataPassed2XamarinTablets dataReturned = null;
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "UpdateInvoice");

            foreach (var pic in order.Images)
            {
                var picExist = LocalStorage.DoesFileFolderExist($"{pic.PicturePath}/{pic.FileName}", "");

                if (picExist)
                    await new Pictures().SyncPicture2Azure(pic.PicturePath, "DeliveryNotes",
                        pic.FileName);

                var ls = new LocalStorage();
            }

            try
            {
                json = JsonConvert.SerializeObject(order);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode) return Convert.ToInt64(returnedDataFromServer);

            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest) return 0;
        }

        return 0;
    }

    [Time]
    public async Task<ApiStructure>
        GetNewApIdata(string aPIcode) //Task<DataPassed2XamarinTablets> LogonRequest(string LoggedInUserAlias)
    {
        var dataReturned = new ApiStructure();
        dataReturned.FriendlyName = "-^-FAIL-^-";

        using (var client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri("https://TabletSupportByHarmonix.azurewebsites.net/");

                var first = (aPIcode + "123456").Substring(0, 7);
                var second = DateTime.Now.ToString("ddMMyyyy");

                var content = $"api//Support?details=|-|-|{first}|-|-|{second}|-|-|";

                var response = await client.GetAsync(content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        dataReturned = await Task.Run(() =>
                            JsonConvert.DeserializeObject<ApiStructure>(returnedDataFromServer));
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.ToString();
                    }

                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest) return dataReturned;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser?.FullName}---");
            }
        }

        return dataReturned;
    }

    [Time]
    public async Task<Tuple<Person, string, List<Assignment>>>
        LogonRequestAzureAuth(
            AuthenticatedUser loggedInUserProfile) //Task<DataPassed2XamarinTablets> LogonRequest(string LoggedInUserAlias)
    {
        var dataReturned = new DataPassed2XamarinTablets();

        var alias = string.Empty;
        if (loggedInUserProfile.UserPrincipalName.Contains("@"))
            alias = loggedInUserProfile.UserPrincipalName.Split('@')[0];
        else
            return new Tuple<Person, string, List<Assignment>>(null, "400",
                new List<Assignment>()); // Fail - get out of here

        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            // var uri = new Uri("https://XamarinAuthTestApi.azurewebsites.net/api/StandardLibrary/l");// This is a Logon Request
            uri = new Uri(cv2.FocusMobileWebApiUrl + "l"); // This is a Logon Request

            try
            {
                var json = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject("|-|-|" + alias + "|-|-|"));

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loggedInUserProfile.AccessTokenFromAzure);

                var response =
                    await client.PostAsync(uri,
                        content);
                // *********** 6 seconds to return as a ganger *******************

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = await response.Content.ReadAsStringAsync();

                    try
                    {
                        dataReturned = await Task.Run(() =>
                            JsonConvert.DeserializeObject<DataPassed2XamarinTablets>(returnedDataFromServer));

                        NavigationalParameters.LoggedInUser =
                            dataReturned.PersonData?.FirstOrDefault(x => x.IsLoggedIn);
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.ToString();
                    }

                    return await SortReturnedWebApiBaseData(dataReturned);
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return new Tuple<Person, string, List<Assignment>>(null, response.StatusCode.ToString(),
                        new List<Assignment>());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return new Tuple<Person, string, List<Assignment>>(null, "WebApi Fail", new List<Assignment>());
    }

    [Time]
    public Task<bool> CanWeConnect2Api()
    {
        try
        {
            if (CrossConnectivity.Current.IsConnected) return Task.FromResult(true);

            return Task.FromResult(false);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return Task.FromResult(false);
        }
    }

    [Time]
    public async Task<long> SavePicture2Server(Pictures4Tablet pic2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SavePictures2server");
            try
            {
                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(pic2Save));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));
                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"Error Uploading Picture:  {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<long> SaveSurveyPicture2Server(Pictures4Tablet pic2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SavePictures2server");
            try
            {
                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(pic2Save));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));
                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<long> SavePlantIssue2Server(PlantIssue plantIssueData2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SavePlantIssue2server");
            try
            {
                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(plantIssueData2Save));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));
                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<long> SavePlantTransferOut2Server(PlantTransfers plantTransferOutData2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();

            uri = new Uri(cv2.FocusMobileWebApiUrl + "SavePlantTransferOut2server");

            try
            {
                var json = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject(plantTransferOutData2Save));

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;

                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));

                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}-----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<long> RejectPlantItem(PlantTransfers plantRejectionData2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "RejectPlantItem");
            try
            {
                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(plantRejectionData2Save));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));
                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest) return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<long> SaveChecksData2Server(List<Checks4TabletResponses> checksData2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();

            uri = new Uri(cv2.FocusMobileWebApiUrl + "SaveChecksData2server");
            try
            {
                if (!string.IsNullOrEmpty(checksData2Save?.FirstOrDefault()?.SignatureFileName))
                {
                    var signatures = checksData2Save?.FirstOrDefault()?.SignatureFileName.Split(',');

                    foreach (var sig in signatures)
                        if (!string.IsNullOrEmpty(sig))
                        {
                            var picStatus =
                                _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                                    checksData2Save?.FirstOrDefault()?.SignatureFileName);
                        }
                }

                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(checksData2Save));

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;

                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));

                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<long> SavePlantTransferIn2Tablet(PlantTransfers plantTransferOutData2Save)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SavePlantTransferIn2Tablet");
            try
            {
                var json = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject(plantTransferOutData2Save));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                    var dataReturned =
                        await Task.Run(() => JsonConvert.DeserializeObject<long>(returnedDataFromServer));
                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return 0;
    }

    [Time]
    public async Task<List<Assignment>> SaveAssignment2Server(Person loggedInUser,
        List<Assignment> surveyCheckData2Save)
    {
        List<Assignment> dataReturned = null;
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SaveAssignment2Server");

            string json = null;
            try
            {
                json = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject(
                        new Tuple<Person, List<Assignment>>(loggedInUser, surveyCheckData2Save)));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                try
                {
                    dataReturned = await Task.Run(() =>
                        JsonConvert.DeserializeObject<List<Assignment>>(returnedDataFromServer));
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                }
            }
            else if (response.StatusCode == HttpStatusCode.NotFound ||
                     response.StatusCode == HttpStatusCode.BadRequest)
            {
                return null;
            }
        }

        return dataReturned;
    }

    [Time]
    public async Task<bool> SaveAssignment2Server(Assignment uploadData)
    {
        //DataPassed2XamarinTablets dataReturned = null;
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SaveAssignment2Server");
            string json = null;
            try
            {
                json = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject(uploadData));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode) return true;
            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest)
                return false;
        }

        return true;
    }

    [Time]
    public async Task<bool> SaveDailyMeasures(DataPassed2Server uploadData)
    {
        //DataPassed2XamarinTablets dataReturned = null;
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SaveDailyMeasures2Server");
            string json = null;

            foreach (var pic in uploadData.JobPictureData)
                try
                {
                    var picExist = LocalStorage.DoesFileFolderExist($"{pic?.PicturePath}/{pic?.FileName}", "");

                    if (!picExist)
                        await new Pictures().SyncPicture2Azure(pic?.PicturePath, "JobPictures", pic?.FileName);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
                }

            try
            {
                json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(uploadData));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode) return true;

            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest)
                return false;
        }

        return true;
    }

    [Time]
    private async Task<Tuple<Person, string, List<Assignment>>> SortReturnedWebApiBaseData(
        DataPassed2XamarinTablets receivedWebApiData)
    {
        // long idOfSignedInUser = 0;
        var returnData = "Good Passed Data";
        Person loggedInPerson = null;

        var job = new Jobs();
        var assignmentService = new Assignments();
        var docs = new Documents();
        var user = new Users();
        var check = new Checks();
        var plantData = new Plant();

        try
        {
            if (receivedWebApiData != null)
            {
                NavigationalParameters.AllJobs = null;
                NavigationalParameters.DataPassedToTablet = receivedWebApiData;

                if (user.CreateDBifNotExists())
                {
                    await user.ClearAllRows();

                    if (receivedWebApiData.PersonData?.Count > 0) // Only save Users if there are Users to Save
                    {
                        await user.AddUsers(receivedWebApiData.PersonData);

                        Options options2Save = null;

                        foreach (var person in receivedWebApiData.PersonData)
                        {
                            if (person.IsLoggedIn)
                            {
                                options2Save = person?.MyOptions;
                                //idOfSignedInUser = person.FocusId;
                                options2Save.FkFocusId = person.FocusId;

                                loggedInPerson = person;
                            }

                            if (person.Holidays?.Count > 0) await user.AddHolidays(person?.Holidays);

                            if (person.MyGroups?.Count > 0) await user.AddMyGroups(person?.MyGroups);
                        }

                        await user.AddOptions(options2Save);
                    }
                } // time taken = seconds;

                if (job.CreateDBifNotExists())
                {
                    await job.ClearAllRows(); // ****  2 sces *****

                    if (receivedWebApiData.Locations?.Count > 0) // Only save Jobs if there are Jobs to Save
                        foreach (var item in receivedWebApiData.Locations)
                            await job.AddLocation(item);

                    if (receivedWebApiData.EndPoints?.Count > 0)
                        //   var xx = receivedWebApiData.EndPoints.Where(x => x.Qnumber == 407478 && x.BuildingStandard.ToLower() == "chamber")?.Count();
                        job.AddEndpoints(receivedWebApiData?.EndPoints); // ** 2.5 s 8 ***\

                    if (receivedWebApiData.TaskList?.Count > 0) job.AddTaskList(receivedWebApiData); // ** 2.5 s 8 ***

                    if (receivedWebApiData.AllProjectSummaries?.Count > 0) // Only save Jobs if there are Jobs to Save
                        job.AddSummaries(receivedWebApiData?.AllProjectSummaries);

                    if (receivedWebApiData.BlockageList?.Count > 0) // Only save Jobs if there are Jobs to Save
                        await job.AddBlockages(receivedWebApiData?.BlockageList);

                    if (receivedWebApiData.TransferPlantTo?.Count > 0) // Only save Jobs if there are Jobs to Save
                        await plantData.AddTransferPlant2Users(receivedWebApiData?.TransferPlantTo);

                    if (receivedWebApiData?.DamageTypeList?.Count > 0) // Only save Jobs if there are Jobs to Save
                        await job.AddUtilityDamageQuestion(receivedWebApiData.DamageTypeList);

                    if (receivedWebApiData?.UtilCompanyList?.Count > 0) // Only save Jobs if there are Jobs to Save
                        await job.AddUtilityCompany(receivedWebApiData.UtilCompanyList);

                    if (receivedWebApiData?.InvestigationQuestions?.Count >
                        0) // Only save Jobs if there are Jobs to Save
                        job.AddInvestigationQuestions(receivedWebApiData.InvestigationQuestions);

                    if (receivedWebApiData?.CableRuns?.Count > 0) // Only save Jobs if there are Jobs to Save
                        job.AddCableRuns(receivedWebApiData.CableRuns);

                    if (receivedWebApiData.CableStockAudits?.Count > 0) // Only save Jobs if there are Jobs to Save
                        foreach (var item in receivedWebApiData.CableStockAudits)
                        {
                            var cableStockUseMeasures = job.GetCableStockUseMeasuresValue(item);

                            if (cableStockUseMeasures > 0) item.InStock = item.InStock - cableStockUseMeasures;

                            await job.AddCableStockItem(item);
                        }

                    if (receivedWebApiData.EventsManagementTypes != null) // Only save Jobs if there are Jobs to Save
                        job.AddEventManagementTypeList(receivedWebApiData.EventsManagementTypes);

                    if (receivedWebApiData.EventList != null) // Only save Jobs if there are Jobs to Save
                        job.AddEventManagementList(receivedWebApiData.EventList);

                    if (receivedWebApiData.CableStockUse?.Count > 0) // Only save Jobs if there are Jobs to Save
                        foreach (var item in receivedWebApiData.CableStockUse)
                            await job.AddCableStockUse(item);

                    if ((bool)NavigationalParameters.LoggedInUser?.MyGroups?.Any(x =>
                            x.GroupName.ToLower().Contains("supervisor")))
                    {
                        if (receivedWebApiData.ClaimedFiles?.Count > 0)
                            await job.AddClaimedFiles(receivedWebApiData.ClaimedFiles);

                        if (receivedWebApiData.DailyDiary?.Count() > 0) // Only save Jobs if there are Jobs to Save
                            await job.AddClaimedNotes(receivedWebApiData.DailyDiary?.ToList());

                        if (receivedWebApiData.WeatherEvents?.Count() > 0) // Only save Jobs if there are Jobs to Save
                            await job.AddWeatherEvent(receivedWebApiData.WeatherEvents?.ToList());

                        if (receivedWebApiData.LabourFiles?.Count() > 0)
                            await user.AddLabour(receivedWebApiData.LabourFiles?.ToList());

                        if (receivedWebApiData?.RegisteredDamages?.Count > 0)
                            await job.AddInvestigateDamages(receivedWebApiData.RegisteredDamages);

                        if (receivedWebApiData.UserDailyProjectTimesList?.Count >
                            0) // Only save Jobs if there are Jobs to Save
                            foreach (var item in receivedWebApiData.UserDailyProjectTimesList)
                                job.AddUserDailyProjectTimes(item);

                        if (receivedWebApiData.UserDailyTimeList?.Count > 0) // Only save Jobs if there are Jobs to Save
                            foreach (var item in receivedWebApiData.UserDailyTimeList)
                                job.AddUserDailyTime(item);

                        if (receivedWebApiData.UserDailyTimeNotesList?.Count >
                            0) // Only save Jobs if there are Jobs to Save
                            foreach (var item in receivedWebApiData.UserDailyTimeNotesList)
                                job.AddUserDailyTimeNotes(item);

                        if (receivedWebApiData.DailyProjectNotesList?.Count >
                            0) // Only save Jobs if there are Jobs to Save
                            foreach (var item in receivedWebApiData.DailyProjectNotesList)
                                job.AddDailyProjectNotes(item);

                        if (receivedWebApiData.UserToolboxTalks?.Count > 0) // Only save Jobs if there are Jobs to Save
                            foreach (var item in receivedWebApiData.UserToolboxTalks)
                                job.AddUserToolBoxTalks(item);

                        if (receivedWebApiData.ToolBoxTalks?.Count > 0) // Only save Jobs if there are Jobs to Save
                            foreach (var item in receivedWebApiData.ToolBoxTalks)
                                job.AddToolBoxTalks(item);
                    }

                    job.AddJobs(receivedWebApiData.JobData);
                } //1 second to complete

                assignmentService.CreateDBifNotExists();

                assignmentService.ClearAllRows("All");

                await check.ClearAllRows("Checks4Tablet");

                check.CreateDBifNotExists("Checks4Tablet");

                await check.ClearAllRows("DeleteAllChecksBeforeToday");

                plantData.CreateDBifNotExists("Plant4Tablet");

                await plantData.ClearAllRows("Plant4Tablet");

                docs.CreateDBifNotExists();

                docs.ClearAllRows();

                check.AddChecks(receivedWebApiData?.ChecksDetail);

                plantData.AddPlant(receivedWebApiData?.OperativesPlant);

                await assignmentService.AddAbbreviations(receivedWebApiData?.Abbreviations);

                foreach (var cab in receivedWebApiData?.CabinetDetails)
                    if (cab.VmnbUnumber != null && cab.VmnbUnumber == "0")
                        await assignmentService.Addcabinets(receivedWebApiData?.CabinetDetails);
                    else
                        await assignmentService.AddCabinetDetails(receivedWebApiData?.CabinetDetails);

                if (receivedWebApiData.Questions?.Count > 0) // Only save ProjectWorks if there are Jobs to Save
                    await assignmentService.AddQuestions(receivedWebApiData?.Questions);

                if (receivedWebApiData.Assignments?.Count > 0)
                    await assignmentService.AddAssignments(receivedWebApiData);

                if (receivedWebApiData.ProjectWorkRates?.Count > 0) // Only save ProjectWorks if there are Jobs to Save
                    await assignmentService.AddProjectWorkWorks(receivedWebApiData?.ProjectWorkRates);

                if (receivedWebApiData.SchemeWorks?.Count > 0) // Only save ProjectWorks if there are Jobs to Save
                    await job.AddRates(receivedWebApiData?.SchemeWorks);

                NavigationalParameters.DataPassedToTablet.SavedToIpad = true;
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex.ToString()} for: {NavigationalParameters.LoggedInUser?.FullName}");

            Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
        }


        return new Tuple<Person, string, List<Assignment>>(loggedInPerson, returnData,
            receivedWebApiData.Assignments);
    }

    [Time]
    public async Task<bool> UploadEventManagementItem(Event eventManagement)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var jsonstring = string.Empty;
                //List<RegisterUtilityDamage> strikesToSend = null;

                var cv2 = new Constants();
                uri = new Uri($"{cv2.FocusMobileWebApiUrl}RegisterEventManagement");

                var jobService = new Jobs();

                // NavigationalParameters.EventManagement.UtilityDamages = jobService.GetRegisterUtilityDamages();


                if (NavigationalParameters.EventManagement.UtilityDamages.Any(x => x.InjuredPersonnel.Any()) &&
                    (NavigationalParameters.AppMode == NavigationalParameters.AppModes.ACCIDENT
                     || NavigationalParameters.AppMode == NavigationalParameters.AppModes.INCIDENT))
                    NavigationalParameters.EventManagement.Category =
                        NavigationalParameters.AppModes.ACCIDENT.ToString();

                foreach (var strike in NavigationalParameters.EventManagement.UtilityDamages)
                foreach (var pic in strike.Pictures)
                {
                    var picExist = LocalStorage.DoesFileFolderExist($"{pic.PicturePath}/{pic.FileName}", "");

                    if (picExist)
                        await new Pictures().SyncPicture2Azure(pic.PicturePath, "UtilityDamagePics",
                            pic.FileName);
                    var ls = new LocalStorage();
                }

                jsonstring = await Task.Factory.StartNew(() =>
                    JsonConvert.SerializeObject(NavigationalParameters.EventManagement));

                var content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    NavigationalParameters.EventManagement.UtilityDamages.ForEach(async s =>
                        await jobService.DeleteRegisterUtilityDamage(s));
                    return true;
                }

                return false;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
        }

        return false;
    }

    [Time]
    public async Task<bool> UpdatePin(Person member)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "ChangePin");
            try
            {
                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(member));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.InternalServerError:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }

            return false;
        }
    }

    [Time]
    public async Task<DataPassed2XamarinTablets> RefreshPlantData(List<long> operativesList)
    {
        var dataReturned = new DataPassed2XamarinTablets();

        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "RefreshMyPlant");
            try
            {
                var json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(operativesList));
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var returnedDataFromServer = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        dataReturned = await Task.Run(() =>
                            JsonConvert.DeserializeObject<DataPassed2XamarinTablets>(returnedDataFromServer));
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.ToString();
                    }

                    return dataReturned;
                }

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
            }
        }

        return null;
    }

    [Time]
    public async Task<bool> SaveAssignments2Server(List<Assignment> assignments)
    {
        //DataPassed2XamarinTablets dataReturned = null;
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SaveAssignments2Server");
            string json = null;
            try
            {
                json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(assignments));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                return true;
            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest)
                return false;
        }

        return true;
    }

    [Time]
    public async Task<bool> SaveInvestigations2Server(DamageToInvestigate investigationToSave, string mode)
    {
        //DataPassed2XamarinTablets dataReturned = null;
        using (var client = new HttpClient())
        {
            try
            {
                var cv2 = new Constants();

                if (mode == "Partial")
                    uri = new Uri(cv2.FocusMobileWebApiUrl + "PartialSubmitInvestigation");
                else
                    uri = new Uri(cv2.FocusMobileWebApiUrl + "FullSubmitInvestigation");

                string json = null;


                foreach (var pic in investigationToSave.PreviousImages)
                {
                    var picExist = LocalStorage.DoesFileFolderExist($"{pic.PicturePath}/{pic.FileName}", "");

                    if (picExist)
                        await new Pictures().SyncPicture2Azure(pic.PicturePath, "UtilityDamagePics",
                            pic.FileName);
                    // var ls = new LocalStorage();
                    //ls.GetImageFromLocalstorage("JobPictures/", "");
                    //await ls.Save2AzureBlobByByteArray(pic.Image, picList.Last().FileName, "focusspdata/UtilityDamagePics");
                }

                json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(investigationToSave));

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode) return true;

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message} -----{NavigationalParameters.LoggedInUser.FullName}---");
                return false;
            }

            return true;
        }
    }

    [Time]
    public async Task<bool> UploadSupervisorDailyDiaries(UserDailyTime supervisorDiariesToUpload)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "UploadSupervisorDailyDiaries");

            string json = null;
            try
            {
                json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(supervisorDiariesToUpload));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                return true;
            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest)
                return false;
        }

        return true;
    }

    [Time]
    public async Task<bool> SaveToolboxTalks2Server(List<UsersToolBoxTalks> currentUserToolboxTalks)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "SaveToolBoxTalks2Server");
            string json = null;
            try
            {
                json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(currentUserToolboxTalks));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode) return true;

            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest)
                return false;
        }

        return true;
    }

    [Time]
    public async Task<bool> UpdateCableRun(CableRuns cableRuns)
    {
        using (var client = new HttpClient())
        {
            var cv2 = new Constants();
            uri = new Uri(cv2.FocusMobileWebApiUrl + "UpdateCableRun");
            string json = null;

            try
            {
                json = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(cableRuns));
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                return true;
            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.BadRequest)
                return false;
        }

        return true;
    }

    internal void backupDatabase()
    {
        throw new NotImplementedException();
    }
}