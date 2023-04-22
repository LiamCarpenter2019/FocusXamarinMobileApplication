using System.Collections.Generic;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Models;

namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IPlant
{
    Plant4Tablet GetPlantByServerId(int plantServerId); //
    void AddPlantAsync(List<Plant4Tablet> passedPlantData); //
    bool CreateDBifNotExists(string whichTable); //
    Task ClearAllRows(string whichTable); //
    List<PlantIssue> GetAllPlantIssues(); //
    Task AddPlantIssues(List<PlantIssue> passedPlantIssueData); //
    Task AddPlantIssue(PlantIssue passedPlantIssueData); //
    List<Plant4Tablet> GetMyPlant(long plantUserId); //
    List<Plant4Tablet> GetPlant4AllTheseUsers(string pipedStringOfUserIDs); //
    List<Plant4Tablet> GetAllPlant(); //
    List<PlantTransfers> GetAllPlantTransfers(); //
    Task AddPlantTransferItem(PlantTransfers passedPlantTransferData); //
    Task AddTransferPlant2Users(List<TransferPlantToOperatives> passedPlantTransferData); //
    List<TransferPlantToOperatives> GetAllPlantTransfer2Users(); //
    Task AddPlantItem(Plant4Tablet passedPlantData);
}