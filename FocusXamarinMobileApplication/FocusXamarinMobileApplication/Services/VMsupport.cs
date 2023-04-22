namespace FocusXamarinMobileApplication.Services;

public class VMsupport : IVMsupport
{
    [Time]
    public bool CreateDBifNotExists(string whichTable)
    {
        var returnValue = true;

        try
        {
            switch (whichTable)
            {
                case "PlanningInfo":
                    App.Database.CreateTable<PlanningInfo>(); //create table
                    break;
                case "VMtotalProjectInfo":
                    App.Database.CreateTable<VMtotalProjectInfo>(); //create table
                    break;
                case "VMexpansionReleaseData":
                    App.Database.CreateTable<VMexpansionReleaseData>(); //create table
                    break;
                case "VMl3CabDetail":
                    App.Database.CreateTable<VMl3CabDetail>(); //create table
                    break;
                case "VMl4CabDetail":
                    App.Database.CreateTable<VMl4CabDetail>(); //create table
                    break;
            }
        }
        catch (Exception)
        {
            return false;
        }

        return returnValue;
    }

    [Time]
    public Task ClearAllRows(string whichTable)
    {
        return Task.Factory.StartNew(async () =>
        {
            switch (whichTable)
            {
                case "PlanningInfo":
                    App.Database.ClearTable<PlanningInfo>(); //create table
                    break;
                case "VMtotalProjectInfo":
                    App.Database.ClearTable<VMtotalProjectInfo>(); //create table
                    break;
                case "VMexpansionReleaseData":
                    App.Database.ClearTable<VMexpansionReleaseData>(); //create table
                    break;
                case "VMl3CabDetail":
                    App.Database.ClearTable<VMl3CabDetail>(); //create table
                    break;
                case "VMl4CabDetail":
                    App.Database.ClearTable<VMl4CabDetail>(); //create table
                    App.Database.ClearTable<VMl5CabDetail>(); //create table
                    break;
            }
        });
    }

    //[Time] public Task AddJob(JobData4Tablet passedJob)
    //{
    //    return Task.Factory.StartNew(() =>
    //    {
    //        App.Database.SaveItem<JobData4Tablet>(passedJob);
    //    });
    //}

    [Time]
    public Task AddVMexpansionReleaseData(List<VMexpansionReleaseData> passedData)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(passedData); });
    }

    [Time]
    public Task AddVMplanningData(List<PlanningInfo> passedData)
    {
        CreateDBifNotExists("lanningInfo");

        return Task.Factory.StartNew(async () => { App.Database.SaveItems(passedData); });
    }

    [Time]
    public Task AddVMl3CabDetail(List<VMl3CabDetail> passedData)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(passedData); });
    }

    [Time]
    public Task AddVMl4CabDetail(List<VMl4CabDetail> passedData)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(passedData); });
    }

    [Time]
    public Task AddVMl5CabDetail(List<VMl5CabDetail> passedData)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(passedData); });
    }

    [Time]
    public List<VMexpansionReleaseData> GetVMexpansionReleaseData()
    {
        return App.Database.GetItems<VMexpansionReleaseData>().ToList();
    }

    [Time]
    public List<PlanningInfo> GetPlanningInfo()
    {
        return App.Database.GetItems<PlanningInfo>().ToList();
    }

    [Time]
    public List<VMl3CabDetail> GetVMl3CabDetail()
    {
        return App.Database.GetItems<VMl3CabDetail>().ToList();
    }

    [Time]
    public List<VMl4CabDetail> GetVMl4CabDetail()
    {
        return App.Database.GetItems<VMl4CabDetail>().ToList();
    }

    [Time]
    public List<VMl5CabDetail> GetVMl5CabDetail()
    {
        return App.Database.GetItems<VMl5CabDetail>().ToList();
    }

    //[Time] public JobData4Tablet GetCurrentSelectedJob()
    //{
    //    return (from i in App.Database.Table<JobData4Tablet>()
    //            where i.IsSelected == true
    //            //where i.QuoteNumber == 406278
    //            select i).FirstOrDefault();
    //}

    //[Time] public Gang GetCurrentSelectedGang()
    //{
    //    var currentSelectedJob = GetCurrentSelectedJob();
    //    Gang gangInfo2return = new Gang();
    //    try
    //    {
    //        var user = new Users();
    //        Person p = user.GetUserById(Convert.ToInt32(currentSelectedJob.ProjectManagerId));
    //        if (p != null) {
    //            gangInfo2return.ProjectManagerID = currentSelectedJob.ProjectManagerId;
    //            gangInfo2return.ProjectManagerFirstName = p.FirstName;
    //            gangInfo2return.ProjectManagerSurname = p.Surname;
    //            gangInfo2return.ProjectManagerPIN = p.MemberPIN;
    //        }

    //        p = user.GetUserById(Convert.ToInt32(currentSelectedJob.SupervisorId));
    //        if (p != null)
    //        {
    //            gangInfo2return.SupervisorID = currentSelectedJob.SupervisorId;
    //            gangInfo2return.SupervisorFirstName = p.FirstName;
    //            gangInfo2return.SupervisorSurname = p.Surname;
    //            gangInfo2return.SupervisorPIN = p.MemberPIN;
    //        }

    //        p = user.GetUserById(Convert.ToInt32(currentSelectedJob.GangLeaderId));
    //        if (p != null)
    //        {
    //            gangInfo2return.GangLeaderID = currentSelectedJob.GangLeaderId;
    //            gangInfo2return.GangLeaderFirstName = p.FirstName;
    //            gangInfo2return.GangLeaderSurname = p.Surname;
    //            gangInfo2return.GangLeaderPIN = p.MemberPIN;
    //        }

    //        if (currentSelectedJob.OperativeIdsPiped.Contains("|"))
    //        {
    //            List<GangMember> gm = new List<GangMember>();

    //            string[] operativeIDs = currentSelectedJob.OperativeIdsPiped.Split('|');
    //            foreach (var item in operativeIDs)
    //            {
    //                if (item != null && item.Length > 3)
    //                {
    //                    p = user.GetUserById(Convert.ToInt32(item));
    //                    if (p != null)
    //                    {
    //                        gm.Add(new GangMember()
    //                        {
    //                            ID = Convert.ToInt64(item),
    //                            FirstName = p.FirstName,
    //                            Surname = p.Surname,
    //                            GangMemberPin = p.MemberPIN
    //                        });
    //                    }
    //                }
    //            }

    //            gangInfo2return.GangMembers = gm;

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var n = ex.Message;
    //    }

    //    return gangInfo2return;
    //}
}