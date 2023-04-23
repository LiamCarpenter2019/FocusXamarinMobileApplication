namespace FocusXamarinMobileApplication.Services;

public class SupervisorApprovalsPageViewModel : FBaseViewModel
{
    private readonly Jobs _jobService;

    private readonly Stopwatch _sw = new();

    private readonly Users _userService;

    private List<ClaimedNotesFile> _allClaimedNotes;

    public SupervisorApprovalsPageViewModel()
    {
        _jobService = new Jobs();
        _userService = new Users();
        // _allClaimedNotes = _jobService.GetAllClaimedNotes();
    }

    public static List<Labour> CurrentTimeSheets { get; set; }

    //public static JobData4Tablet CurrentJob { get; set; }

    public bool GetItemsForApproval(TaskItem taskItem)
    {
        var da = CheckDiaryApproval(taskItem);
        var cta = CheckTimesheetApproval(taskItem);
        var cwta = CheckWorkItemApproval(taskItem);
        var clt = CheckLateralApproval(taskItem);

        if (da || cta || cwta || clt)
            return true;

        return false;
    }

    public bool BypassCheck(JobData4Tablet job)
    {
        var claimedNotes = _jobService.GetAllClaimedNotes();

        var notes = claimedNotes.Any(x => x.ContractReference == job.ContractReference
                                          && x.NotesGang.ToString() == job.GangLeaderId.ToString()
                                          && x.NotesDate.Date == job.JobDate.Date);


        if (_jobService.GetAllClaimedNotes().Any(x =>
                x.RemoteTableId == 0 && x.ApprovedBySupervisor == DateTime.Parse("02/02/1900"))) return false;

        return true;
    }

    public bool CheckDiaryApproval(TaskItem taskItem)
    {
        var claimedNotes = _jobService.GetClaimedNotes(taskItem);

        NavigationalParameters.SelectedTaskItem.DiaryApproved = claimedNotes == null;
        return NavigationalParameters.SelectedTaskItem.DiaryApproved;
    }

    public bool ApprovalInProgress()
    {
        var ApprovalInProgress = false;

        if (NavigationalParameters.SelectedTaskItem != null)
        {
            ApprovalInProgress = GetItemsForApproval(NavigationalParameters.SelectedTaskItem);
            //  var claimedFiles = _jobService.GetJobClaimedFiles(job);
            //var diary = _jobService.GetClaimedNotes(job);

            if ((NavigationalParameters.SelectedTaskItem.ClaimesApproved
                 || NavigationalParameters.SelectedTaskItem.DiaryApproved
                 || NavigationalParameters.SelectedTaskItem.LabourApproved) &&
                (NavigationalParameters.SelectedTaskItem.ClaimesApproved == false
                 || NavigationalParameters.SelectedTaskItem.DiaryApproved == false
                 || NavigationalParameters.SelectedTaskItem.LabourApproved == false))
                ApprovalInProgress = true;
            else
                ApprovalInProgress = false;
        }

        //if (diary?.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"))
        //{
        //    labourApprovalInProgress |=
        //        labourFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("01/01/1900 00:00:00"));

        //    claimedFilesApprovalInProgress |=
        //        claimedFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("01/01/1900 00:00:00"));

        //    if (labourApprovalInProgress == false && claimedFilesApprovalInProgress == false)
        //        if (labourFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00")))
        //            claimedFilesApprovalInProgress = true;
        //}
        //else
        //{
        //    labourApprovalInProgress |=
        //        labourFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"));

        //    claimedFilesApprovalInProgress |=
        //        claimedFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"));
        //}

        //if (labourApprovalInProgress || claimedFilesApprovalInProgress)
        //    return true;
        return ApprovalInProgress;
    }

    public bool CheckTimesheetApproval(TaskItem taskItem)
    {
        CurrentTimeSheets = _jobService.GetLabourToApprove(taskItem);
        NavigationalParameters.SelectedTaskItem.LabourApproved = !CurrentTimeSheets.Any();
        return NavigationalParameters.SelectedTaskItem.LabourApproved;
    }

    public bool CheckWorkItemApproval(TaskItem taskItem)
    {
        var measures = _jobService.GetClaimedFiles(taskItem);

        if (measures != null && measures.Any())
            // NavigationalParameters.SelectedTaskItem.ClaimesApproved = !measures.Any();

            NavigationalParameters.SelectedTaskItem.ClaimesApproved = measures.All(item =>
                NavigationalParameters.LateralCodes.Any(x => x == item.ClaimHeader));
        else
            NavigationalParameters.SelectedTaskItem.ClaimesApproved = true;

        return NavigationalParameters.SelectedTaskItem.ClaimesApproved;
    }

    public bool CheckLateralApproval(TaskItem taskItem)
    {
        var measures = _jobService.GetClaimedFiles(taskItem);

        if (measures != null && measures.Any())
            // NavigationalParameters.SelectedTaskItem.LateralsApproved = false;
            NavigationalParameters.SelectedTaskItem.LateralsApproved = !measures.Any(item =>
                NavigationalParameters.LateralCodes.Contains(item.ClaimHeader));
        else
            NavigationalParameters.SelectedTaskItem.LateralsApproved = true;

        // NavigationalParameters.SelectedTaskItem.LateralsApproved = true;
        return NavigationalParameters.SelectedTaskItem.LateralsApproved;
    }

    public bool CheckReinstatementApproval(JobData4Tablet job)
    {
        var reinstatement = _jobService.GetReinstatementMeasure(job);

        switch (reinstatement)
        {
            case null:
                NavigationalParameters.ReinstatmentMeasureToApprove = false;
                return false;
            default:
                NavigationalParameters.ReinstatmentMeasureToApprove = true;
                return true;
        }
    }

    public async Task MarkAsAbsent(JobData4Tablet jobData)
    {
        await _jobService.MarAsAbsent(jobData);
    }
}