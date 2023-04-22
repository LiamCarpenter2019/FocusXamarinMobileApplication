#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services;

public class Plant : IPlant
{
    [Time]
    public Plant4Tablet GetPlantByServerId(int plantServerId)
    {
        var allPlantItems = App.Database.GetItems<Plant4Tablet>();

        return App.Database.GetItems<Plant4Tablet>()?
            .FirstOrDefault(i => i.RemotePlantId == plantServerId);
    }


    [Time]
    public async void AddPlantAsync(List<Plant4Tablet> passedPlantData)
    {
        try
        {
            if (passedPlantData != null && passedPlantData.Count > 0)
            {
                var c = new Checks();

                c.CreateDBifNotExists("Checks4TabletResponses");

                c.DeleteAllResponsesExcept4Today();

                var plantItems = App.Database.GetItems<Plant4Tablet>().ToList();

                foreach (var item in passedPlantData)
                {
                    if (plantItems.Any(x => x.RemotePlantId == item.RemotePlantId)) App.Database.DeleteItem(item);

                    if (item.Responses4ThisPlant2day != null && item.Responses4ThisPlant2day.Count > 0)
                        c.AddChecksResponses(item.Responses4ThisPlant2day);

                    if (plantItems.TrueForAll(x => x.RemotePlantId != item.RemotePlantId))
                    {
                        await AddPlantItem(item);
                    }
                    else
                    {
                        var duplicate = item;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var xx = ex.Message;
        }
    }

    // ------------------------------ Bit of Both --------------------------------------------------

    [Time]
    public bool
        CreateDBifNotExists(string whichTable) // Plant4Tablet | PlantIssue | PlantTransfers | PlantTransfer2Users
    {
        var returnValue = true;

        try
        {
            if (whichTable == "Plant4Tablet")
                App.Database.CreateTable<Plant4Tablet>(); //create table
            else if (whichTable == "PlantIssue")
                App.Database.CreateTable<PlantIssue>(); //create table
            else if (whichTable == "PlantTransfers")
                App.Database.CreateTable<PlantTransfers>(); //create table
            else if (whichTable == "TransferPlantToOperatives")
                App.Database.CreateTable<TransferPlantToOperatives>(); //create table
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
        switch (whichTable)
        {
            case "Plant4Tablet":
                App.Database.ClearTable<Plant4Tablet>();
                break;
            case "PlantIssue":
                App.Database.ClearTable<PlantIssue>();
                break;
            case "PlantTransfers":
                App.Database.ClearTable<PlantTransfers>();
                break;
            case "TransferPlantToOperatives":
                App.Database.ClearTable<TransferPlantToOperatives>();
                break;
            case "Docs4Tablet":
                App.Database.ClearPlantDocuments();
                break;
        }
    }

    // ------------------------------------- Plant Issues -------------------------------------------------

    [Time]
    public List<PlantIssue> GetAllPlantIssues()
    {
        CreateDBifNotExists("PlantIssue");
        var allPlantIssueItems = App.Database.GetItems<PlantIssue>();
        return allPlantIssueItems.ToList();
    }

    [Time]
    public async Task AddPlantIssues(List<PlantIssue> passedPlantIssueData)
    {
        try
        {
            App.Database.SaveItems(passedPlantIssueData.ToList());
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }
    }

    [Time]
    public async Task AddPlantIssue(PlantIssue passedPlantIssueData)
    {
        try
        {
            App.Database.SaveItem(passedPlantIssueData);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }
    }

    [Time]
    public List<Plant4Tablet> GetMyPlant(long plantUserId)
    {
        var plant2Return = App.Database.GetItems<Plant4Tablet>()
            ?.Where(x => x.IssuedToId == plantUserId || x.TransferFromId == plantUserId).ToList();

        foreach (var item in plant2Return)
            item.Responses4ThisPlant2day =
                new Checks().GetAllRelevantResponses4SelectedDate(item.RemotePlantId, DateTime.Now);

        return plant2Return;
    }

    [Time]
    public List<Plant4Tablet> GetPlant4AllTheseUsers(string pipedStringOfUserIDs)
    {
        var allPlantItems = GetAllPlant();
        var plant2Return = new List<Plant4Tablet>();

        if (string.IsNullOrEmpty(pipedStringOfUserIDs) ||
            !pipedStringOfUserIDs.Contains("|") ||
            allPlantItems == null || allPlantItems.Count == 0)
            return plant2Return;

        foreach (var plantFound in from operativeId in pipedStringOfUserIDs.Split('|')
                 where !string.IsNullOrEmpty(operativeId)
                 let plantFound = allPlantItems.Where(x =>
                         x.IssuedToId == Convert.ToInt64(operativeId) ||
                         x.TransferFromId == Convert.ToInt64(operativeId))
                     .ToList()
                 where plantFound != null && plantFound.ToList().Count > 0
                 select plantFound)
            plant2Return.AddRange(plantFound.ToList());

        return plant2Return.ToList();
    }

    [Time]
    public List<Plant4Tablet> GetAllPlant()
    {
        var foundPlantItems = App.Database.GetItems<Plant4Tablet>()?.ToList();

        var plantItems = new List<Plant4Tablet>();
        var c = new Checks();

        foreach (var item in foundPlantItems)
            if (plantItems.All(x => x.RemotePlantId != item.RemotePlantId))
            {
                item.Responses4ThisPlant2day =
                    c.GetAllRelevantResponses4SelectedDate(item.RemotePlantId, DateTime.Now);

                plantItems.Add(item);
            }

        try
        {
            return plantItems;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var x = ex.Message;
            return plantItems;
        }
    }

    // ---------------------------------- Plant Transfers --------------------------------------------

    [Time]
    public List<PlantTransfers> GetAllPlantTransfers()
    {
        CreateDBifNotExists("PlantTransfers");
        try
        {
            var allPlantTransferItems = App.Database.GetItems<PlantTransfers>().ToList();
            return allPlantTransferItems;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return null;
        }
    }

    [Time]
    public async Task AddPlantTransferItem(PlantTransfers passedPlantTransferData)
    {
        CreateDBifNotExists("PlantTransfers");
        try
        {
            App.Database.SaveItem(passedPlantTransferData);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public async Task AddTransferPlant2Users(List<TransferPlantToOperatives> passedPlantTransferData)
    {
        try
        {
            CreateDBifNotExists("PlantTransfers");

            foreach (var p in passedPlantTransferData) App.Database.SaveItem(p);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public List<TransferPlantToOperatives> GetAllPlantTransfer2Users()
    {
        CreateDBifNotExists("TransferPlantToOperatives");
        // var allPlantTransfer2Users = 
        return App.Database.GetItems<TransferPlantToOperatives>().ToList();
    }

    [Time]
    public async Task AddPlantItem(Plant4Tablet passedPlantData)
    {
        try
        {
            App.Database.SaveItem(passedPlantData);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }
    }

    [Time]
    public void AddPlant(List<Plant4Tablet> passedPlantData)
    {
        App.Database.SaveItems(passedPlantData);
    }

    [Time]
    public List<Pictures4Tablet> GetPlantImages(long assetNo)
    {
        try
        {
            return App.Database.GetItems<Pictures4Tablet>()?
                .Where(x => x.PictureComments.Contains($"--{assetNo}--") && x.SeverPictureId <= 0)?
                .ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Could not retrieve plant transfer images for asset No {assetNo} returning this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
            return null;
        }
    }

    [Time]
    public async Task<bool> GetPlant4AllTheseUsersFromServer(List<long> listOfUserIDs)
    {
        var wa = new WebApi();
        var check = new Checks();
        var areWeConnected = await wa.CanWeConnect2Api();
        if (areWeConnected)
        {
            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                if (listOfUserIDs.All(x => x != NavigationalParameters.LoggedInUser.FocusId))
                    listOfUserIDs.Add(NavigationalParameters.LoggedInUser.FocusId);


            var returnedPlantData = await wa.RefreshPlantData(listOfUserIDs);

            if (returnedPlantData != null)
                try
                {
                    // var wassup = await wa.LogonRequest(NavigationalParameters.LoggedInUser.LoginAlias);
                    // Now need to wipe databases & save then display
                    if (CreateDBifNotExists("Plant4Tablet"))
                    {
                        ClearAllRows("Plant4Tablet");
                        if (returnedPlantData.OperativesPlant.Count > 0
                           ) // Only save Plant if there are Plant to Save
                            AddPlantAsync(returnedPlantData?.OperativesPlant);
                    }

                    if (check.CreateDBifNotExists("Checks4Tablet"))
                    {
                        check.ClearAllRows("Checks4Tablet");
                        if (returnedPlantData.ChecksDetail?.Count > 0
                           ) // Only save ChecksDetail if there are ChecksDetail to Save
                            check.AddChecks(returnedPlantData?.ChecksDetail);
                    }


                    await ClearAllRows("Docs4Tablet");
                    if (returnedPlantData.DocumentsData?.Count > 0
                       ) // Only save ChecksDetail if there are ChecksDetail to Save
                        await AddDocs(returnedPlantData?.DocumentsData);
                }
                catch (Exception ex)
                {
                    var n = ex.Message;
                    throw;
                }
        }
        else
        {
            return false;
        }

        return true;
    }

    private async Task AddDocs(List<Docs4Tablet> documentsData)
    {
        try
        {
            App.Database.SaveItems(documentsData);

            new DocumentListingPageViewModel().UpdatePlantDocsCommand.Execute(
                "UpdatedDocs");
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }
    }

    [Time]
    public string GetEmailAddress(long issuedToId)
    {
        try
        {
            return App.Database.GetItems<Person>().FirstOrDefault(x => x.FocusId == issuedToId).Email;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;

            return NavigationalParameters.LoggedInUser.Email;
        }
    }
}