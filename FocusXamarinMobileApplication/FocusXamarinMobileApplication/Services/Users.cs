#region

using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services;

public class Users : IUsers
{
    public List<Labour> LabourFiles { get; set; }
    public List<Labour> Labour { get; set; }


    [Time]
    public List<Person> GetAllUsersNow()
    {
        return App.Database.GetItems<Person>().ToList();
    }

    [Time]
    public Person GetLoggedInUser()
    {
        try
        {
            var p = App.Database.GetItems<Person>().FirstOrDefault(x => x.IsLoggedIn);

            if (p != null && p.FocusId > 0)
                try
                {
                    var x = GetAllOptions(p.FocusId);
                    p.MyOptions = GetAllOptions(p.FocusId);
                    p.MyLabour = GetAllLabour(p.FocusId);
                    p.Holidays = GetHolidays(p.FocusId);
                    p.MyGroups = GetMyUserGroup(p.FocusId);
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                }
            else
                return null;

            return p;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var p = NavigationalParameters.DataPassedToTablet.PersonData.FirstOrDefault(x => x.IsLoggedIn);


            if (p == null) return p;
            p.MyOptions = GetAllOptions(p.FocusId);
            p.MyLabour = GetAllLabour(p.FocusId);
            p.Holidays = GetHolidays(p.FocusId);
            p.MyGroups = GetMyUserGroup(p.FocusId);


            return p;
        }
    }

    [Time]
    public Person Check4ValidLoggedInPerson()
    {
        // First check that we have a valid DB Table
        try
        {
            App.Database.CreateTable<Person>();
        }
        catch (Exception)
        {
            return null; // Dont have a DB so return as a fail
        }

        // Ok we do have a DB so check if we have a logged in user
        var p = GetLoggedInUser();

        return p ?? NavigationalParameters.LoggedInUser;

        //if (p.SsiDdate.ToString("dd/MM/yyyy") != DateTime.Today.ToString("dd/MM/yyyy"))
        //{
        //    return null; // Not right so its a fail
        //}
    }


    [Time]
    public bool CreateDBifNotExists()
    {
        var returnValue = true;

        try
        {
            App.Database.CreateTable<Person>();
            App.Database.CreateTable<Options>();
            App.Database.CreateTable<Labour>();
            App.Database.CreateTable<Holiday>();
            App.Database.CreateTable<UserGroup>();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var exception = ex.ToString();
            return false;
        }


        return returnValue;
    }


    [Time]
    public Task ClearAllRows()
    {
        return Task.Factory.StartNew(async () =>
        {
            App.Database.ClearTable<Person>();

            App.Database.ClearTable<UserGroup>();

            //  foreach (var ug in App.Database.GetItems<UserGroup>()) App.Database.DeleteItem(ug);

            foreach (var holiday in App.Database.GetItems<Holiday>().Where(holiday => holiday.RemoteTableId != 0))
                App.Database.DeleteItem(holiday);

            App.Database.ClearTable<Options>();

            App.Database.ClearSavedRemoteData<Labour>();
        });
    }

    [Time]
    public async Task AddUsers(List<Person> passedUsers)
    {
        var usersTosave = new List<Person>();

        foreach (var user in passedUsers)
        {
            var user1 = App.Database.GetItems<Person>()?.FirstOrDefault(x => x.FocusId == user.FocusId);

            if (user1 == null) usersTosave.Add(user);
        }

        App.Database.SaveItems(usersTosave);
    }

    [Time]
    public Task AddUser(Person passedUser)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItem(passedUser); });
    }

    [Time]
    public Task AddOptions(Options passedOptions)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItem(passedOptions); });
    }

    [Time]
    public List<Person> GetAllUsers()
    {
        var users = new List<Person>();

        var allUsers = App.Database.GetItems<Person>();

        foreach (var user in allUsers)
        {
            user.BackgroundHighlighted = Color.Transparent;
            if (users.All(x => x.FocusId != user.FocusId)) users.Add(user);
        }

        return users;
    }

    [Time]
    public void UpdateSupervisorDailyLabour(UserDailyTime labour)
    {
        App.Database.SaveSupervisorDiaries(labour);
    }

    [Time]
    public int UpdateLabourFile(Labour labour)
    {
        return App.Database.UpdateLabourFile(labour);
        ;
    }

    [Time]
    public async Task AddLabour(List<Labour> passedLabour)
    {
        foreach (var labourFile in passedLabour)
        {
            var allItems = App.Database.GetItems<Labour>()?
                .Where(x => x.RemoteTableId == labourFile.RemoteTableId).ToList(); //452794

            if (allItems.Any())
            {
                if (allItems.All(x => x.ApprovedBySupervisor != DateTime.Parse("02/02/1900 00:00:00")))
                    App.Database.SaveItem(labourFile);
            }
            else
            {
                App.Database.SaveItem(labourFile);
            }
        }
    }

    [Time]
    public Options GetAllOptions(long loggedInUserId)
    {
        try
        {
            var options = App.Database.GetItems<Options>().FirstOrDefault(x => x.FkFocusId == loggedInUserId);
            return options;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.PersonData
                ?.FirstOrDefault(i => i.FocusId == loggedInUserId)?.MyOptions;
        }
    }

    [Time]
    public List<Labour> GetAllGangLabour(JobData4Tablet job)
    {
        try
        {
            LabourFiles = App.Database.GetItems<Labour>().Where(x => x.LabourGang == job.GangLeaderId.ToString()
                                                                     && x.LabourDate == job.JobDate
                                                                     && x.ContractReference == job.ContractReference)
                .ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }

        return LabourFiles;
    }

    [Time]
    public List<Labour> GetAllLabour(long loggedInUserId)
    {
        try
        {
            //return App.Database.Execute($"Select * from Labour where LabourOperative is {loggedInUserId.ToString()}");
            Labour = App.Database.GetItems<Labour>()?.Where(x => x.LabourOperative == loggedInUserId.ToString())
                .ToList();
            return Labour;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }

        return Labour;
    }

    [Time]
    public Person GetUserById(int userId)
    {
        try
        {
            var user = App.Database.GetItems<Person>()?.FirstOrDefault(x => x.FocusId == userId) ??
                       NavigationalParameters.DataPassedToTablet.PersonData?.FirstOrDefault(x => x.FocusId == userId);


            if (user == null) return null;
            user.MyOptions = GetAllOptions(user.FocusId);
            user.MyLabour = GetAllLabour(user.FocusId);
            user.Holidays = GetHolidays(user.FocusId);
            user.MyGroups = GetMyUserGroup(user.FocusId);
            return user;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var user = NavigationalParameters.DataPassedToTablet.PersonData
                .FirstOrDefault(x => x.FocusId == userId);


            if (user == null) return null;
            user.MyOptions = GetAllOptions(user.FocusId);
            user.MyLabour = GetAllLabour(user.FocusId);
            user.Holidays = GetHolidays(user.FocusId);
            user.MyGroups = GetMyUserGroup(user.FocusId);
            return user;
        }
    }

    private List<UserGroup> GetMyUserGroup(long userFocusId)
    {
        try
        {
            var userGroups = App.Database.GetItems<UserGroup>().Where(x => x.UserId == userFocusId).ToList();


            return userGroups;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var userGroups = NavigationalParameters.DataPassedToTablet.PersonData?
                .FirstOrDefault(x => x.FocusId == userFocusId)?.MyGroups;


            return userGroups;
        }
    }

    //Less info gathered for faster approval check in task list
    [Time]
    public Person GetUserById4Approval(long userId)
    {
        try
        {
            var user = App.Database.GetItems<Person>().FirstOrDefault(x => x.FocusId == userId);


            user.MyLabour = GetAllLabour(userId);

            return user;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var user = NavigationalParameters.DataPassedToTablet.PersonData?
                .FirstOrDefault(x => x.FocusId == userId);


            user.MyLabour = GetAllLabour(userId);

            return user;
        }
    }

    private List<Holiday> GetHolidays(long userId)
    {
        try
        {
            return App.Database.GetItems<Holiday>().Where(x => x.OperativeId == userId).ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.PersonData?
                .FirstOrDefault(x => x.FocusId == userId)?.Holidays;
        }
    }

    [Time]
    public Task AddHolidays(List<Holiday> holidays)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(holidays); });
    }

    [Time]
    public void UpdateInputDiariesFile(ClaimedNotesFile diaryInfo)
    {
        App.Database.UpdateInputDiariesFile(diaryInfo);
    }

    [Time]
    public UserDailyTime GetSupervisorLabour(long focusId)
    {
        var xx = App.Database.GetItems<UserDailyTime>()?.ToList();

        var supervisorLabour = xx.FirstOrDefault(x => (long)x.UserId == focusId
                                                      && x.Date.Value.Date == DateTime.Now.Date);

        if (supervisorLabour == null)
        {
            supervisorLabour = new UserDailyTime
            {
                Id = 0,
                IsApproved = false,
                CreatedOn = DateTime.Now,
                IsNightShift = false,
                LastModifiedBy = NavigationalParameters.LoggedInUser.FocusId,
                UserId = NavigationalParameters.LoggedInUser.FocusId,
                CreatedBy = NavigationalParameters.LoggedInUser.FocusId,
                LastModifiedOn = DateTime.Now,
                Date = DateTime.Now,
                InTime = DateTime.Now.TimeOfDay,
                OutTime = DateTime.Now.TimeOfDay
            };

            App.Database.SaveItem(supervisorLabour);
        }
        else
        {
            supervisorLabour.UserDailyTimeNotesList = App.Database.GetItems<UserDailyTimeNotes>()
                ?.Where(x => x.RemoteId == supervisorLabour?.RemoteId)?.ToList();

            supervisorLabour.UserDailyProjectTimesList = App.Database.GetItems<UserDailyProjectTimes>()?
                .Where(x => x.RemoteId == supervisorLabour?.RemoteId)?
                .ToList();

            if (supervisorLabour.UserDailyProjectTimesList?.Count > 0)
                foreach (var pt in supervisorLabour?.UserDailyProjectTimesList)
                {
                    var pn = App.Database.GetItems<DailyProjectNotes>()?
                        .Where(x => x.RemoteProjectTimeId == pt.RemoteProjectTimeId)?.ToList();

                    pt.DailyProjectNotesList.AddRange(pn);
                }
        }

        return supervisorLabour;
    }

    [Time]
    public Task AddMyGroups(List<UserGroup> itemMyGroups)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(itemMyGroups); });
    }

    [Time]
    public void UpdateSupervisorDailyProjectLabour(UserDailyTime supervisorDailyDiary)
    {
        App.Database.UpdateSupervisorDailyProjectLabour(supervisorDailyDiary);
    }

    [Time]
    public void DeleteLabourFile(Labour itemTodelete)
    {
        App.Database.DeleteItem(itemTodelete);
    }
}