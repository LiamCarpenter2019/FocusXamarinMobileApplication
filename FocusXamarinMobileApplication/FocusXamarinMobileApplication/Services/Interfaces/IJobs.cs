using System.Collections.Generic;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Models;

namespace FocusXamarinMobileApplication.Services.Interfaces;

internal interface IJobs
{
    bool CreateDBifNotExists();
    Task ClearAllRows();
    Task AddJob(JobData4Tablet passedJob);
    void AddJobs(List<JobData4Tablet> passedJobs);
    List<JobData4Tablet> GetAllJobs();
    DataPassed2Server GetNewCabinetsAndEndPointsForPoleSurvey(DataPassed2Server dataPassedToserver);
}