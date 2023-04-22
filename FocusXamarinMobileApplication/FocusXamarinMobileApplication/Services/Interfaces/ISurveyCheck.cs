using FocusXamarinMobileApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IAssignments
{
    List<Assignment> GetRelevantAssignments(string category);

    //Task<List<SurveyAnswers>> GetAllRelevantAssignmentResponses(long passedId, string qnumber);
    Task AddAssignments(DataPassed2XamarinTablets passedChecksData);
    Task AddAssignmentsResponses(List<SurveyAnswers> passedChecksData);
    void CreateDBifNotExists();
    void ClearAllRows(string whichTable);
    Task AddCabinetDetails(List<VMl4CabDetail> cabinetDetails);
    void GetCurrentAnswer(Button button);
}