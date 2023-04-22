//using StandardLibrary.Data;

#region

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class ProjectsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public ProjectsPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
    }

    public Jobs _jobService { get; }
    public Users _userService { get; }
    public ObservableCollection<JobData4Tablet> ProjectsList { get; set; }

    public RelayCommand<int> PageLoaded => new(async i =>
    {
        ProjectsList = new ObservableCollection<JobData4Tablet>();
        var jobList = _jobService.GetAllJobs()?.ToList();

        foreach (var job in jobList)
            if (!ProjectsList.Any(y =>
                    y.ProjectName == job.ProjectName && y.OperativeIdsPiped == job.OperativeIdsPiped))
            {
                var operatives = job.OperativeIdsPiped?.Split('|');

                foreach (var opId in operatives)
                    if (int.TryParse(opId, out var opName))
                        job.OperativeNames += $"{_userService.GetUserById(opName)?.FullName}, ";

                if (!string.IsNullOrEmpty(job.OperativeNames))
                    job.OperativeNames =
                        job.OperativeNames.Remove(job.OperativeNames.LastIndexOf(",", StringComparison.Ordinal), 1);

                ProjectsList.Add(job);
            }

        if (NavigationalParameters.CurrentSelectedJob != null)
            ProjectsList = new ObservableCollection<JobData4Tablet>(ProjectsList?
                .Where(x => x.ProjectName == NavigationalParameters.CurrentSelectedJob?.ProjectName)?.ToList());
    });

    public RelayCommand<int> ProjectSelected => new(async i =>
    {
        NavigationalParameters.CurrentSelectedJob = ProjectsList[i];
        NavigateBack();
    });
}