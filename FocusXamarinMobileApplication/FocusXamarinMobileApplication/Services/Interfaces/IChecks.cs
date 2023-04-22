#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IChecks
{
    List<Checks4Tablet> GetRelevantChecks(string passedType); //
    List<Checks4TabletResponses> GetAllRelevantResponses(long passedPlantId, string qnumber);

    List<Checks4TabletResponses> GetAllRelevantResponses4Dalies(string qnumber, DateTime forWhatDate,
        string passedGangLeaderName);

    List<Checks4TabletResponses> GetAllRelevantResponses4SelectedDate(long passedPlantId, string qnumber,
        DateTime date4Selection);

    List<Checks4TabletResponses> GetAllRelevantResponses4SelectedDate(long passedPlantId, DateTime date4Selection);
    List<Checks4TabletResponses> GetAllRelevantResponses4_2day();
    string GetRelevantChecksResponse(int passedListIndex); //
    void AddChecks(List<Checks4Tablet> passedChecksData); //
    void AddChecksResponses(List<Checks4TabletResponses> passedChecksData); //
    void DeleteAllResponsesExcept4Today();
    bool CreateDBifNotExists(string whichTable); //
    Task ClearAllRows(string whichTable); //
    List<Checks4TabletResponses> GetAllPlantResponses(); //
    void AddSingleChecksResponse(Checks4TabletResponses passedChecksData); //

    Checks4TabletResponses GetSingleSampleOfAnyChecks(long plantId,
        List<Checks4TabletResponses> allCurrentResponses); //

    string CheckChecksOnCurrentItem(Plant4Tablet currentItemSelected, List<Checks4Tablet> allChecksRequired,
        List<Checks4TabletResponses> allCurrentResponses); //

    string CheckChecksOnAllItems(List<Plant4Tablet> allPlantItems, List<Checks4TabletResponses> allResponses); //

    void DeleteAllInstancesOfThisQuestionFromDb(int questionNo, int plantId,
        string qnumber); //

    List<Checks4TabletResponses> DeleteAllResponsesForThisPlantIdFromDb4Today(int plantId,
        List<Checks4TabletResponses> allCurrentResponses, string qnumber);

    void DeleteAllInstancesOfThisPlantIdFromDb(int plantId); //

    Task ChangeCurrentStatus4PlantId(Plant4Tablet currentItemSelected,
        List<Checks4TabletResponses> allCurrentResponses, string newStatus, Person selectedUser);

    Task ChangeCurrentStatus4Dailies(JobData4Tablet currentSelectedJob,
        List<Checks4TabletResponses> allCurrentResponses, string newStatus);
}