using System.Threading.Tasks;

namespace FocusXamarinMobileApplication.Services.Interfaces;

public interface IVMsupport
{
    bool CreateDBifNotExists(string whichTable);

    Task ClearAllRows(string whichTable);

    //Task AddJob(JobData4Tablet passedJob);
    //Task AddJobs(List<JobData4Tablet> passedJobs);

    //Task<List<JobData4Tablet>> GetAllJobs();
}