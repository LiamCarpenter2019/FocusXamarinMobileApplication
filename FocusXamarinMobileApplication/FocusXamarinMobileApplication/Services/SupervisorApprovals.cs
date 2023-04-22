namespace FocusXamarinMobileApplication.Services
{
    public class SupervisorApprovals
    {
        private readonly Jobs _jobService;

        private readonly Stopwatch _sw = new Stopwatch();

        private readonly Services.Users _userService;

        private List<ClaimedNotesFile> _allClaimedNotes;

        [Time]
        public SupervisorApprovals()
        {
            _jobService = new Jobs();
            _userService = new Services.Users();
            // _allClaimedNotes = _jobService.GetAllClaimedNotes();
        }

        public static List<Labour> CurrentTimeSheets { get; set; }

        //[Time] public static JobData4Tablet CurrentJob { get; set; }

        [Time]
        public bool GetItemsForApproval(JobData4Tablet job)
        {
            var da = CheckDiaryApproval(job);
            var cta = CheckTimesheetApproval(job);
            var cwta = CheckWorkItemApproval(job);
            var clt = CheckLateralApproval(job);

            if (da || cta || cwta || clt)
                return true;

            return false;
        }

        [Time]
        public bool BypassCheck(JobData4Tablet job)
        {
            var claimedNotes = _jobService.GetAllClaimedNotes();

            var notes = claimedNotes.Any(x => x.ContractReference == job.ContractReference
                                              && x.NotesGang.ToString() == job.GangLeaderId.ToString()
                                              && x.NotesDate.Date == job.JobDate.Date
            );


            if (_jobService.GetAllClaimedNotes().Any(x =>
                x.RemoteTableId == 0 && x.ApprovedBySupervisor == DateTime.Parse("02/02/1900"))) return false;

            return true;
        }

        [Time]
        public bool CheckDiaryApproval(JobData4Tablet job)
        {
            //var claimedNotes = _jobService.GetTasks()
            //.Where(x => x.QuoteNumber == job.QuoteNumber
            //&& x.GangLeaderId == job.GangLeaderId
            //&& x.JobDate == job.JobDate
            //&& x.Complete == false);

            //foreach (var item in claimedNotes)
            //{
            //    item.
            //}

            //NavigationalParameters.DiariesToApprove =
            //    claimedNotes?.PostedByGanger != DateTime.Parse("01/01/1900 00:00:00") &&
            //    claimedNotes?.ApprovedBySupervisor ==
            //    NavigationalParameters.MinDateTime;



            return NavigationalParameters.DiariesToApprove;
        }

        [Time]
        public bool ApprovalInProgress(JobData4Tablet job)
        {
            var labourApprovalInProgress = false;
            var claimedFilesApprovalInProgress = false;

            var labourFiles = _jobService.GetJobLabour(job);
            var claimedFiles = _jobService.GetJobClaimedFiles(job);
            var diary = _jobService.GetClaimedNotes(job);

            if (diary?.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"))
            {
                labourApprovalInProgress |=
                    labourFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("01/01/1900 00:00:00"));

                claimedFilesApprovalInProgress |=
                    claimedFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("01/01/1900 00:00:00"));

                if (labourApprovalInProgress == false && claimedFilesApprovalInProgress == false)
                    if (labourFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00")))
                        claimedFilesApprovalInProgress = true;
            }
            else
            {
                labourApprovalInProgress |=
                    labourFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"));

                claimedFilesApprovalInProgress |=
                    claimedFiles.Any(x => x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"));
            }

            if (labourApprovalInProgress || claimedFilesApprovalInProgress)
                return true;
            return false;
        }

        [Time]
        public bool CheckTimesheetApproval(TaskItem taskItem)
        {
            CurrentTimeSheets = _jobService.GetLabourToApprove(taskItem);
            NavigationalParameters.TimeSheetsToApprove = CurrentTimeSheets.Any();
            return NavigationalParameters.TimeSheetsToApprove;
        }

        [Time]
        public bool CheckWorkItemApproval(TaskItem taskItem)
        {
            var measures = _jobService.GetClaimedFiles(taskItem);

            NavigationalParameters.MeasuresToApprove = false;

            foreach (var item in measures.Where(item =>
                !NavigationalParameters.LateralCodes.Contains(item.ClaimHeader)))
                NavigationalParameters.MeasuresToApprove = true;
            return NavigationalParameters.MeasuresToApprove;
        }

        [Time]
        public bool CheckLateralApproval(TaskItem taskItem)
        {
            var measures = _jobService.GetClaimedFiles(taskItem);

            NavigationalParameters.LateralsToApprove = false;

            foreach (var item in measures.Where(item => NavigationalParameters.LateralCodes.Contains(item.ClaimHeader)))
                NavigationalParameters.LateralsToApprove = true;
            return NavigationalParameters.LateralsToApprove;
        }

        [Time]
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
    }
}