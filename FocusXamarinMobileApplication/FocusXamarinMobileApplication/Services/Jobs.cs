#region

using Audit = FocusXamarinMobileApplication.Models.Audit;
using Event = FocusXamarinMobileApplication.Models.Event;
using Location = FocusXamarinMobileApplication.Models.Location;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

[assembly: Xamarin.Forms.Dependency(typeof(Documents))]

namespace FocusXamarinMobileApplication.Services;

public class Jobs : IJobs
{
    private readonly WebApi _api;
    private readonly Assignments _assignmentService;
    private readonly Pictures _pictureService;
    private readonly Users _userService;

    [Time]
    public Jobs()
    {
        _api = new WebApi();
        _pictureService = new Pictures();
        _assignmentService = new Assignments();
        _userService = new Users();
    }


    public bool Result { get; set; }
    public List<InvestigateDamages> RegisteredDamages { get; set; }
    public long QNumber { get; private set; }
    public DamageToInvestigate DamageToInvestigate { get; set; }

    public Person GangMemberToAdd { get; set; }
    public List<VisitorLog> visitorLogs { get; private set; }
    public List<VisitorLog> visitorListTemp { get; private set; }

    [Time]
    public bool CreateDBifNotExists()
    {
        var returnValue = true;

        try
        {
            App.Database.CreateTable<StockFuelDTO>(); //create table
            App.Database.CreateTable<FuelConsumption>(); //create table
            App.Database.CreateTable<JobData4Tablet>(); //create table
            App.Database.CreateTable<ClaimedNotesFile>(); //create table
            App.Database.CreateTable<ClaimedFile>();
            App.Database.CreateTable<Abbreviations>();
            App.Database.CreateTable<DamageTypeAnswers>();
            App.Database.CreateTable<VMexpansionReleaseData>();
            App.Database.CreateTable<VisitorLog>();
            App.Database.CreateTable<Rates>();
            App.Database.CreateTable<ServicesCrossed4Tablet>();
            App.Database.CreateTable<ProjectSummary>();
            App.Database.CreateTable<Blockage>();
            App.Database.CreateTable<Labour>();
            App.Database.CreateTable<TransferPlantToOperatives>();
            App.Database.CreateTable<ReinstatementMeasure>();
            App.Database.CreateTable<Labour>();
            App.Database.CreateTable<TaskItem>();
            App.Database.CreateTable<Audit>();
            App.Database.CreateTable<VMl4CabDetail>();
            App.Database.CreateTable<UserDailyProjectTimes>();
            App.Database.CreateTable<UserDailyTime>();
            App.Database.CreateTable<UserDailyTimeNotes>();
            App.Database.CreateTable<DailyProjectNotes>();
            App.Database.CreateTable<Location>();
            App.Database.CreateTable<Pictures4Tablet>();
            App.Database.CreateTable<WeatherEvent>();
            App.Database.CreateTable<Checks4Tablet>(); //create table
            App.Database.CreateTable<Checks4TabletResponses>(); //create table
            App.Database.CreateTable<UtilityCompany>();
            App.Database.CreateTable<DamageToInvestigate>();
            App.Database.CreateTable<RegisterUtilityDamage>();
            App.Database.CreateTable<InvestigateDamages>();
            App.Database.CreateTable<Investigation>();
            App.Database.CreateTable<PublicUtilityDamageQuestion>();
            App.Database.CreateTable<InvestigationQuestion>();
            App.Database.CreateTable<InvestigationAnswer>();
            App.Database.CreateTable<PrintTypesProvided>();
            App.Database.CreateTable<GangResponsible>();
            App.Database.CreateTable<ExternalPersonnel>();
            App.Database.CreateTable<Witness>();
            App.Database.CreateTable<InjuredPerson>();
            App.Database.CreateTable<CableStockAudit>();
            App.Database.CreateTable<CableStockUse>();
            App.Database.CreateTable<UsersToolBoxTalks>();
            App.Database.CreateTable<ToolBoxTalks>();
            App.Database.CreateTable<CableRuns>();
            App.Database.CreateTable<Order>();
            App.Database.CreateTable<OrderBookItem>();
            App.Database.CreateTable<EventManagementType>();
            App.Database.CreateTable<Event>();
            App.Database.CreateTable<FibreTestResults>();
            App.Database.CreateTable<ClaimedPole>();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            return false;
        }

        return returnValue;
    }

    [Time]
    public Task ClearAllRows()
    {
        return Task.Factory.StartNew(async () =>
        {
            try
            {
                App.Database.ClearTable<Abbreviations>();
                App.Database.ClearTable<JobData4Tablet>();
                App.Database.ClearTable<Rates>();
                App.Database.ClearTable<TaskItem>();
                App.Database.ClearSavedRemoteData<ProjectSummary>();
                App.Database.ClearSavedRemoteData<ServicesCrossed4Tablet>();
                App.Database.ClearEndpoints();
                App.Database.ClearTable<TransferPlantToOperatives>();
                App.Database.ClearSavedRemoteData<VisitorLog>();
                App.Database.ClearSavedRemoteData<FuelConsumption>();
                App.Database.ClearSavedRemoteData<StockFuelDTO>();
                App.Database.ClearSavedRemoteData<Location>();
                App.Database.ClearSavedRemoteData<Blockage>();
                App.Database.ClearSavedRemoteData<ReinstatementMeasure>();
                App.Database.ClearTable<RegisterUtilityDamage>();
                App.Database.ClearTable<UtilityCompany>();
                App.Database.ClearTable<ToolBoxTalks>();
                App.Database.ClearSavedRemoteData<UsersToolBoxTalks>();
                App.Database.ClearTable<InvestigateDamages>();
                App.Database.ClearSavedRemoteData<DamageTypeAnswers>();
                App.Database.ClearSavedInvestigations();
                App.Database.ClearTable<InvestigationQuestion>();
                App.Database.ClearTable<ExternalPersonnel>();
                App.Database.ClearSavedRemoteData<Witness>();
                App.Database.ClearSavedRemoteData<InjuredPerson>();
                App.Database.ClearSavedRemoteData<PublicUtilityDamageQuestion>();
                App.Database.ClearSavedRemoteData<InvestigationAnswer>();
                App.Database.ClearTable<GangResponsible>();
                App.Database.ClearSavedRemoteData<PrintTypesProvided>();
                App.Database.ClearSavedRemoteData<WeatherEvent>();
                App.Database.ClearSavedRemoteData<CableStockAudit>();
                App.Database.ClearSavedRemoteData<CableStockUse>();
                App.Database.ClearSavedRemoteData<CableRuns>();
                App.Database.ClearTable<OrderBookItem>();
                App.Database.ClearTable<EventManagementType>();
                App.Database.ClearTable<Event>();
                App.Database.ClearSavedRemoteData<FibreTestResults>();
                App.Database.ClearSavedRemoteData<ClaimedPole>();

                // App.Database.ExecuteString($"delete from ClaimedNotesFile where ApprovedBySupervisor <> '02/02/1900 00:00:00' and RemoteTableId <> 0");


                foreach (var cnf in App.Database.GetItems<ClaimedNotesFile>()?.Where(cnf =>
                             cnf.ApprovedBySupervisor != DateTime.Parse("02/02/1900 00:00:00") &&
                             cnf.RemoteTableId != 0))
                    App.Database.DeleteItem(cnf);

                // App.Database.ExecuteString($"delete from ClaimedFile where ApprovedBySupervisor <> '02/02/1900 00:00:00' and RemoteTableId <> 0 and ApprovedBySupervisorName <> ''");


                foreach (var cf in App.Database.GetItems<ClaimedFile>()?.Where(cf =>
                             cf.ApprovedBySupervisor != DateTime.Parse("02/02/1900 00:00:00") &&
                             cf.RemoteTableId != 0 &&
                             cf.ApprovedBySupervisorName != ""))
                    App.Database.DeleteItem(cf);

                //  App.Database.ExecuteString($"delete from ClaimedFile where ApprovedBySupervisor <> '02/02/1900 00:00:00' and RemoteTableId <> 0");

                foreach (var lf in App.Database.GetItems<Labour>()?.Where(lf =>
                             lf.ApprovedBySupervisor != DateTime.Parse("02/02/1900 00:00:00") && lf.RemoteTableId != 0))
                    App.Database.DeleteItem(lf);

                //var labourfiles2 = App.Database.GetItems<Labour>();

                App.Database.ExecuteString("delete from ReinstatementMeasure where RemoteTableId <> 0");

                //foreach (var reinstatment in App.Database.GetItems<ReinstatementMeasure>()?.Where(reinstatment => reinstatment.ReinstatementMeasure != 0))
                //    App.Database.DeleteItem(reinstatment);
                App.Database.ExecuteString("delete from Pictures4Tablet where SeverPictureId <> 0");

                // foreach (var pic in App.Database.GetItems<Pictures4Tablet>()?.Where(pic => pic.SeverPictureId != 0))
                //   App.Database.DeleteItem(pic);

                App.Database.ExecuteString("delete from UserDailyProjectTimes where ProjectTimeId <> 0");

                //  foreach (var udpt in App.Database.GetItems<UserDailyProjectTimes>()?.Where(udpt => udpt.ProjectTimeId != 0))
                // App.Database.DeleteItem(udpt);

                App.Database.ExecuteString("delete from UserDailyTime where DailyTimeId <> 0");

                // foreach (var udt in App.Database.GetItems<UserDailyTime>()?.Where(udt => udt.DailyTimeId != 0))
                // App.Database.DeleteItem(udt);

                App.Database.ExecuteString("delete from UserDailyTimeNotes where DailyTimeId <> 0");

                //  foreach (var udtn in App.Database.GetItems<UserDailyTimeNotes>()?.Where(udtn => udtn.DailyTimeId != 0))
                // App.Database.DeleteItem(udtn);
                App.Database.ExecuteString("delete from DailyProjectNotes where ProjectTimeId <> 0");

                // foreach (var dpn in App.Database.GetItems<DailyProjectNotes>()?.Where(dpn => dpn.ProjectTimeId != 0))
                //  App.Database.DeleteItem(dpn);
                App.Database.ExecuteString("delete from UsersToolBoxTalks where RemoteTableId <> 0");


                App.Database.ExecuteString("delete from VMexpansionReleaseData where SavedToServer = true");

                // App.Database.ExecuteString($"delete from Order");

                App.Database.ClearSavedRemoteData<PrintTypesProvided>();
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        });
    }

    [Time]
    public Task AddJob(JobData4Tablet passedJob)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItem(passedJob); });
    }

    [Time]
    public async void AddJobs(List<JobData4Tablet> passedJobs)
    {
        foreach (var job in passedJobs)
        {
            if (job.DailyChecksCompleted) job.DailyChecksPosted = true;

            var jobToDelete = App.Database.GetItems<JobData4Tablet>()?
                .Where(x => x.JobId == job.JobId
                            && x.JobDate.Date == job.JobDate.Date).ToList();

            foreach (var j in jobToDelete) App.Database.DeleteItem(j);

            App.Database.SaveItem(job);
        }

        var jobList = App.Database.GetItems<JobData4Tablet>()?.Count();
    }


    [Time]
    public List<JobData4Tablet> GetAllJobs()
    {
        try
        {
            //    List<JobData4Tablet> list = new List<JobData4Tablet>();
            var jobList = App.Database.GetItems<JobData4Tablet>()?.ToList();

            foreach (var job in jobList)
            {
                job.GangLeaderName = _userService.GetUserById(Convert.ToInt32(job?.GangLeaderId))?.FullName;

                job.JobGang = GetGang(job);

                var operativeIDs = job.OperativeIdsPiped.Split('|');

                job.OperativeNames = "";

                foreach (var item in operativeIDs)
                    if (!string.IsNullOrEmpty(item) && Convert.ToInt32(item) != Convert.ToInt32(job.GangLeaderId))
                        job.OperativeNames += $"{_userService.GetUserById(Convert.ToInt32(item))?.FullName}, ";

                if (!string.IsNullOrEmpty(job?.OperativeNames))
                    job.OperativeNames =
                        job?.OperativeNames?.Remove(job.OperativeNames.LastIndexOf(",", StringComparison.Ordinal),
                            1);
            }

            return jobList;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
            return null;
        }
    }

    public DataPassed2Server GetNewCabinetsAndEndPointsForPoleSurvey(DataPassed2Server dataPassedToserver)
    {
        dataPassedToserver.CabinetDetails = new List<VMl4CabDetail>();

        dataPassedToserver.EndPoints = new List<VMexpansionReleaseData>();

        foreach (var assignment in dataPassedToserver.Assignments)
        {
            dataPassedToserver.CabinetDetails.AddRange(App.Database.GetItems<VMl4CabDetail>()?
                .Where(x => x.L4Number == assignment.LocationName && !string.IsNullOrEmpty(assignment.LocationName)));

            dataPassedToserver.EndPoints.AddRange(App.Database.GetItems<VMexpansionReleaseData>()?.Where(x =>
                x.L4Number == assignment.LocationName
                && !string.IsNullOrEmpty(assignment.LocationName))?.ToList());
        }

        return dataPassedToserver;
    }

    [Time]
    public List<WeatherEvent> GetWeatherEvents(JobData4Tablet currentSelectedJob)
    {
        var wel = App.Database.GetItems<WeatherEvent>()?
            .Where(x => x.Qnumber == currentSelectedJob.QuoteNumber
                        && x.Date == currentSelectedJob.JobDate
                        && x.JobId == currentSelectedJob.JobId
                        && x.GangLeader == currentSelectedJob.GangLeaderId)?.ToList();

        foreach (var item in wel)
            if (item.Type == NavigationalParameters.WeatherType.CLOUDY.ToString())
                item.DisplayType = $"CLOUDY, {item.StartTime} - {item.EndTime}";
            else if (item.Type == NavigationalParameters.WeatherType.LIGHTRAIN.ToString())
                item.DisplayType = $"LIGHT RAIN, {item.StartTime} - {item.EndTime}";
            else if (item.Type == NavigationalParameters.WeatherType.STORM.ToString())
                item.DisplayType = $"STORM, {item.StartTime} - {item.EndTime}";
            else if (item.Type == NavigationalParameters.WeatherType.SNOW.ToString())
                item.DisplayType = $"SNOW, {item.StartTime} - {item.EndTime}";
            else if (item.Type == NavigationalParameters.WeatherType.SUNNYSPELLS.ToString())
                item.DisplayType = $"SUNNY SPELLS, {item.StartTime} - {item.EndTime}";
            else if (item.Type == NavigationalParameters.WeatherType.SUNSHINE.ToString())
                item.DisplayType = $"SUNSHINE, {item.StartTime} - {item.EndTime}";
            else if (item.Type == NavigationalParameters.WeatherType.WIND.ToString())
                item.DisplayType = $"WIND, {item.StartTime} - {item.EndTime}";
        return wel;
    }

    [Time]
    public ObservableCollection<Event> GetEvent()
    {
        var events = App.Database.GetItems<Event>();

        foreach (var ev in events) ev.Investigations = GetInvestigatedDamages(ev.Identifier);

        return new ObservableCollection<Event>(events);
    }

    [Time]
    public void UpdateCableRun(CableRuns cableRun)
    {
        App.Database.SaveItem(cableRun);
    }

    [Time]
    public List<FibreTestResults> GetFibreTests(JobData4Tablet currentSelectedJob)
    {
        return App.Database.GetItems<FibreTestResults>()?.Where(x => x.QuoteId == currentSelectedJob.QuoteNumber)
            .ToList();
    }

    [Time]
    public async Task SaveStockFuel(StockFuelDTO stockFuel)
    {
        App.Database.SaveItem(stockFuel);
    }

    public ClaimedNotesFile GetClaimedNotes(JobData4Tablet currentSelectedJob)
    {
        try
        {
            var claimedNotes = App.Database.GetItems<ClaimedNotesFile>()?.FirstOrDefault(x =>
                x.QuoteId == currentSelectedJob.QuoteNumber.ToString()
                && x.NotesDate == currentSelectedJob.JobDate.Date
                && x.NotesGang.ToString() == currentSelectedJob.GangLeaderId.ToString()
                && x.ApprovedBySupervisor == NavigationalParameters.MinDateTime
                && x.PostedByGanger != NavigationalParameters.MinDateTime);
            return claimedNotes;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.DailyDiary?.FirstOrDefault(x =>
                x.QuoteId == currentSelectedJob.QuoteNumber.ToString()
                && x.NotesDate == currentSelectedJob.JobDate.Date
                && x.NotesGang.ToString() == currentSelectedJob.GangLeaderId.ToString()
                && x.ApprovedBySupervisor == NavigationalParameters.MinDateTime
                && x.PostedByGanger != NavigationalParameters.MinDateTime);
        }
    }

    public InvestigateDamages GetInvestigatedDamage(string damageid)
    {
        return App.Database.GetItems<InvestigateDamages>().FirstOrDefault(x => x.DamageId == damageid);
    }

    public List<TaskItem> GetTasks()
    {
        var tasks = App.Database.GetItems<TaskItem>()?.ToList();


        return tasks?
            .Where(x => x.Complete == false)?
            .OrderBy(x => x.JobDate)
            .ThenBy(x => x.Category)
            .ToList(); ;
    }

    public Event GetEventManagement(Guid damageGuid)
    {
        var eventMangaement = App.Database.GetItems<Event>()?.FirstOrDefault(x => x.Identifier == damageGuid);

        if (eventMangaement != null)
            eventMangaement.Investigations = GetInvestigatedDamages(eventMangaement.Identifier);

        return eventMangaement;
    }

    public void AddCableRun(CableRuns cableRunToAdd)
    {
        App.Database.SaveItem(cableRunToAdd);
    }

    [Time]
    public void RemoveOrder(Order order)
    {
        //  throw new NotImplementedException();
    }

    [Time]
    public List<EventManagementType> GetEventManagementTypes(string category)
    {
        return App.Database.GetItems<EventManagementType>()?.Where(x => x.Category == category)?.ToList();
    }

    [Time]
    public List<Order> GetInvoicies()
    {
        return App.Database.GetItems<Order>()
            ?.Where(x => x.OrderedById == NavigationalParameters.LoggedInUser.FocusId && x.Void == false)
            ?.ToList();
    }

    [Time]
    public void SaveFibreTest(FibreTestResults selectedCalibrationTest)
    {
        App.Database.SaveItem(selectedCalibrationTest);
    }

    [Time]
    public void SaveOrder(Order order)
    {
        App.Database.SaveItem(order);

        foreach (var item in order.OrderBookItem) App.Database.SaveItem(item);
    }

    [Time]
    public List<Blockage> GetBlockages(string ?AssetId)
    {
        return App.Database.GetItems<Blockage>().Where(x =>
                x.CableRunId == AssetId &&
                x.QNumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString())
            .ToList();
    }

    [Time]
    public IEnumerable<OrderBookItem> GetOrderItems(Order order)
    {
        return App.Database.GetItems<OrderBookItem>().Where(x => x.OrderGuid == order.OrderGuid);
    }

    [Time]
    public void SaveOrderItem(OrderBookItem selectedOrderItem)
    {
        App.Database.SaveItem(selectedOrderItem);
    }

    [Time]
    public Plant4Tablet GetPlantItem(string e)
    {
        return App.Database.GetItems<Plant4Tablet>()?.FirstOrDefault(x => x.RemotePlantId == Convert.ToInt64(e));
    }

    [Time]
    public VMl4CabDetail GetCabinets(string l4number)
    {
        return App.Database.GetItems<VMl4CabDetail>()?.FirstOrDefault(x => x.L4Number == l4number);
    }

    [Time]
    public async Task SaveWeatherEvent(WeatherEvent weatherEventItem)
    {
        App.Database.SaveItem(weatherEventItem);
    }

    [Time]
    public void AddApproval(TaskItem selectedTaskItem)
    {
        App.Database.SaveItem(selectedTaskItem);
    }

    [Time]
    public List<CableStockUse> GetCableStockAuditMeasures()
    {
        if (NavigationalParameters.CurrentSelectedJob.ClientName.ToLower().Contains("nynet"))
            return App.Database.GetItems<CableStockUse>().Where(x =>
                x.DrumNo.ToLower().Contains("nynet") &&
                x.ContractName == NavigationalParameters.CurrentSelectedJob.ProjectName)?.ToList();
        return App.Database.GetItems<CableStockUse>().Where(x =>
            x.DrumNo.ToLower().Contains("nynet") == false &&
            x.ContractName == NavigationalParameters.CurrentSelectedJob.ProjectName)?.ToList();
    }

    [Time]
    public List<CableStockAudit> GetCableAudit()
    {
        if (NavigationalParameters.CurrentSelectedJob.ClientName.ToLower().Contains("nynet"))
        {
            var listToReturn = new List<CableStockAudit>();
            var drums = App.Database.GetItems<CableStockAudit>()?
                .Where(x => x.DrumNo.ToLower().Contains("nynet"))?
                .ToList();

            foreach (var item in drums)
            {
                item.Name = $"{item.DrumNo}-- {item.InStock} -- {item.CableType}";
                listToReturn.Add(item);
            }

            return listToReturn?.OrderByDescending(x => x.CableType)?
                .ThenByDescending(y => y.InStock)?
                .ToList();
        }
        else
        {
            var drums = App.Database.GetItems<CableStockAudit>()?
                .Where(x => !x.DrumNo.ToLower().Contains("nynet"))?.ToList();


            return drums?.OrderByDescending(x => x.CableType)?
                .ThenByDescending(y => y.InStock)?
                .ToList();
        }
    }

    [Time]
    public string GetUserName(string completedById)
    {
        return App.Database.GetItems<Person>()?.FirstOrDefault(x => x.FocusId.ToString() == completedById).FullName;
    }

    [Time]
    public List<CableRuns> GetCableRuns(long quoteNumber)
    {
        return App.Database.GetItems<CableRuns>().Where(x => x.QNumber == quoteNumber).ToList();
    }

    [Time]
    public List<Person> GetAuditors()
    {
        return App.Database.GetItems<Person>()?.Where(x => x.Role == "Auditor").ToList();
    }

    [Time]
    public async Task DeleteWitness(Witness newWitness)
    {
        App.Database.DeleteItem(newWitness);
    }

    [Time]
    public async Task SaveInvestigateDamage(DamageToInvestigate investigateDamage)
    {
        try
        {
            App.Database.SaveItem(investigateDamage);

            if (investigateDamage?.DamageInvestigated != null)
            {
                investigateDamage.DamageInvestigated.ContractReference = investigateDamage.ContractReference;

                App.Database.SaveItem(investigateDamage?.DamageInvestigated);
            }

            if (investigateDamage?.PrintTypesProvided != null)
                App.Database.SaveItem(investigateDamage?.PrintTypesProvided);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public Task AddWitness(Witness newWitness)
    {
        return Task.Factory.StartNew(async () =>
        {
            if (newWitness.RemoteTableId > 0)
            {
                var witness = App.Database.GetItems<Witness>()
                    ?.FirstOrDefault(x => x.RemoteTableId == newWitness.RemoteTableId);
                if (witness == null) App.Database.SaveItem(newWitness);
            }
            else
            {
                App.Database.SaveItem(newWitness);
            }
        });
    }

    [Time]
    public async Task<List<CustomPin>> GetPinsForJobAsync()
    {
        var pins = new List<CustomPin>();

        QNumber = NavigationalParameters.CurrentSelectedJob?.QuoteNumber > 0
            ? NavigationalParameters.CurrentSelectedJob.QuoteNumber
            : NavigationalParameters.CurrentAssignment.Qnumber;

        var pics = App.Database.GetItems<Pictures4Tablet>()?.Where(x => x.QNumber == QNumber).ToList();
        var locations = App.Database.GetItems<Location>()?.Where(x => x.QuoteId == QNumber).ToList();

        foreach (var pic in pics.Where(pic =>
                     NavigationalParameters.NewPosition.Latitude > 0 &&
                     NavigationalParameters.NewPosition.Longitude > 0))
        {
            var placemark = await Geocoding.GetPlacemarksAsync(NavigationalParameters.NewPosition.Latitude,
                NavigationalParameters.NewPosition.Longitude);

            if (placemark != null)
                pins.Add(new CustomPin
                {
                    MarkerId = $"{pic?.PictureReason}",
                    Address =
                        $"{placemark.FirstOrDefault()?.FeatureName}, {placemark.FirstOrDefault()?.Thoroughfare}, {placemark.FirstOrDefault()?.AdminArea}, {placemark.FirstOrDefault()?.PostalCode}",
                    Label = $"{pic?.Category}",
                    Position = new Position(NavigationalParameters.NewPosition.Latitude,
                        NavigationalParameters.NewPosition.Longitude),
                    ImageName = pic?.FileName,
                    Type = PinType.Place
                });
        }

        var questions = NavigationalParameters.GenericQuestions
            .Where(x => x.Category == "PreSiteCivils" || x.Category == "PreSitePremises").ToList();

        foreach (var loc in locations)
        {
            var stage = questions.FirstOrDefault(x => x.QuestionId == loc.QuestionId)?.Stage;

            var pin = new CustomPin
            {
                MarkerId = $"{loc?.Name}",
                Address = $"{loc?.FeatureName}, {loc?.ThroughFare}, {loc?.AdminArea}, {loc?.PostalCode}",
                Label = $"{loc?.Name}",
                Position = new Position(Convert.ToDouble(loc?.Latitude), Convert.ToDouble(loc?.Longitude)),
                Type = PinType.Place
            };

            switch (stage)
            {
                case "Civils":
                    pin.ImageName = "Civils.png";
                    break;
                case "HSE":
                    pin.ImageName = "HSE.png";
                    break;
                case "Wayleaves / Easement":
                    pin.ImageName = "Wayleaves.png";
                    break;
                case "Reinstatement":
                    pin.ImageName = "Reinstatement.png";
                    break;
                case "TM Requirements":
                    pin.ImageName = "TM.png";
                    break;
                case "Noticing / Coordination":
                    pin.ImageName = "Notice.png";
                    break;
                default:
                    pin.ImageName = "Default";
                    break;
            }

            pins.Add(pin);
        }

        return pins;
    }

    [Time]
    public async Task SaveEventManagementItem(Event eventManagement)
    {
        App.Database.SaveItem(eventManagement);
    }

    [Time]
    public void SaveCableAudit(CableStockAudit selectedCableStockAudit)
    {
        App.Database.SaveItem(selectedCableStockAudit);
    }

    [Time]
    public bool CheckEndpoints(JobData4Tablet currentSelectedJob)
    {
        var endpoints = (bool)App.Database.GetItems<VMexpansionReleaseData>().ToList()
            ?.TrueForAll(x => x.Postcode != "NY Net");

        return endpoints;
    }

    [Time]
    public async Task DeleteReinstatementMeasure(ReinstatementMeasure measuretodelete)
    {
        App.Database.DeleteItem(measuretodelete);
    }

    [Time]
    public List<InvestigateDamages> GetInvestigatedDamages(Guid identifier)
    {
        var damagesToRemove = new List<InvestigateDamages>();

        var dl = App.Database.GetItems<InvestigateDamages>()?
            .Where(x => x.PublicUtilityDamageGuid == identifier.ToString())?
            .ToList();

        RegisteredDamages = dl?.Where(x => x.InvestigationLevel == "Utility Damage Investigate")?.ToList();

        foreach (var damage in RegisteredDamages)
        {
            damage.FullName = damage.Surname + ", " + damage.Forename;

            damage.GangResponisble = App.Database.GetItems<GangResponsible>()?
                .Where(x => x.PublicUtilityDamagesId.ToString() == damage.DamageId)?
                .ToList();

            damage.DamageDetails = GetDamageDetails(damage)?
                .OrderByDescending(x => x.InvestigationDate)?
                .ToList();

            if (damage.DamageDetails.Any(x => x.CurrentInvestigationStatus == "Submit")) damagesToRemove.Add(damage);
        }

        foreach (var damage in damagesToRemove) RegisteredDamages.Remove(damage);

        return RegisteredDamages;
    }

    [Time]
    public List<DamageToInvestigate> GetDamageDetails(InvestigateDamages investigateDamage)
    {
        var damageToInvestigate = App.Database.GetItems<DamageToInvestigate>()?
            .Where(x => x.DamageId.ToString() == investigateDamage.DamageId)?
            .ToList();

        foreach (var item in damageToInvestigate)
        {
            var ipList = App.Database.GetItems<InjuredPerson>()?.Where(x => x.PublicUtilityDamageId == item.DamageId)?
                .ToList();

            foreach (var ip in ipList.Where(ip => item.InjuredPersonnel.All(x => x.RemoteTableId != ip.RemoteTableId)))
                item.InjuredPersonnel.Add(ip);

            var epl = App.Database.GetItems<ExternalPersonnel>()?.Where(x => x.PublicUtilityDamageId == item.DamageId)?
                .ToList();

            foreach (var ex in epl.Where(ex => item.ThirdPartyPersonnel.All(x => x.RemoteTableId != ex.RemoteTableId)))
                item.ThirdPartyPersonnel.Add(ex);

            var damageId = item.DamageId.ToString();

            var wList = App.Database.GetItems<Witness>()?.Where(x => x.PublicUtilityDamageId == damageId)?.ToList();

            foreach (var ip in wList.Where(ip => item.Witnesses.All(x => x.RemoteTableId != ip.RemoteTableId)))
                item.Witnesses.Add(ip);

            item.DamageInvestigated =
                App.Database.GetItems<Investigation>()?.FirstOrDefault(x => x.Id.ToString() == item.InvestigationId);

            try
            {
                if (item.DamageInvestigated != null)
                {
                    var damageType = App.Database.GetItems<PublicUtilityDamageQuestion>()?
                        .FirstOrDefault(x => x.RemoteTableId == item.DamageType.Id);

                    item.DamageType = damageType;
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
        }

        return damageToInvestigate;
    }

    public void AddTaskList(DataPassed2XamarinTablets approvals)
    {
        foreach (var approval in approvals.TaskList)
        {
            var approvalExsists = App.Database.GetItems<TaskItem>()
                .Any(x => x.GangLeaderId == approval.GangLeaderId
                          && x.QuoteNumber == approval.QuoteNumber
                          && x.JobDate == approval.JobDate
                          && x.Description == approval.Description);

            if (!approvalExsists)
            {
                var labourExsists = approvals.LabourFiles.Any(x =>
                    x.LabourGang == approval.GangLeaderId.ToString() && x.QuoteId == approval.QuoteNumber.ToString() &&
                    x.LabourDate == approval.JobDate);

                if (approval.Category.ToLower() == "approvals")
                {
                    if (labourExsists) App.Database.SaveItem(approval);
                }
                else
                {
                    App.Database.SaveItem(approval);
                }
            }
        }
    }

    [Time]
    public async Task<bool> UploadEventManagementItem(Event eventManagement)
    {
        var result = await _api.UploadEventManagementItem(eventManagement);
        return result;
    }

    [Time]
    public List<GangResponsible> GetUtilityDamagesGangResponsible(Guid damageId)
    {
        var gangResponsible = App.Database.GetItems<GangResponsible>()
            ?.Where(x => x.PublicUtilityDamageGuid == damageId).ToList();

        return gangResponsible;
    }

    [Time]
    public List<InvestigateDamages> GetInvestigateDamages(InvestigateDamages investigateDamage)
    {
        var investigateDamageList = App.Database.GetItems<InvestigateDamages>()
            ?.Where(x => x.InvestigationDamagesId == investigateDamage.InvestigationDamagesId).ToList();

        foreach (var dmg in investigateDamageList) dmg.DamageDetails = GetDamageDetails(dmg);

        return investigateDamageList;
    }

    [Time]
    public ProjectSummary GetProjectSummary(JobData4Tablet currenSelectedJob)
    {
        var projectSummary = App.Database.GetItems<ProjectSummary>()?
            .FirstOrDefault(x => x.QNumber == currenSelectedJob.QuoteNumber);

        return projectSummary;
    }


    [Time]
    public async Task AddLocation(Placemark currentplacemark, string locationInputText)
    {
        var location = new Location
        {
            Name = locationInputText,
            AdminArea = currentplacemark.AdminArea,
            CountryCode = currentplacemark.CountryCode,
            CountryName = currentplacemark.CountryName,
            CreationDate = DateTime.Now,
            FeatureName = currentplacemark.FeatureName,
            Identifier = NavigationalParameters.SelectedAnswer?.Identifier ??
                         NavigationalParameters.CurrentAssignment?.AssignmentId ??
                         NavigationalParameters.BlockageItem?.ImageId,
            Id = 0,
            Latitude = currentplacemark.Location.Latitude.ToString(),
            Locality = currentplacemark.Locality,
            Longitude = currentplacemark.Location.Longitude.ToString(),
            PostalCode = currentplacemark.PostalCode,
            QuoteId = (long)NavigationalParameters.CurrentSelectedJob?.QuoteNumber,
            QuestionId = NavigationalParameters.SelectedAnswer?.QuestionId ?? 0,
            SubAdminArea = currentplacemark.SubAdminArea,
            SubLocality = currentplacemark.SubLocality,
            SubThroughFare = currentplacemark.SubThoroughfare,
            ThroughFare = currentplacemark.Thoroughfare
        };

        App.Database.SaveItem(location);
    }

    [Time]
    public async Task AddLocation(Location location)
    {
        //CreateDBifNotExists();

        App.Database.SaveItem(location);
    }

    [Time]
    public async Task SaveFuelConsumption(FuelConsumption fc)
    {
        App.Database.SaveItem(fc);
    }

    [Time]
    public async Task AddArea(VMl4CabDetail area)
    {
        //CreateDBifNotExists();
        var cabexsists = App.Database.GetItems<VMl4CabDetail>().FirstOrDefault(x => x.L4Number == area.L4Number);
        try
        {
            if (cabexsists != null) area.Id = cabexsists.Id;

            App.Database.SaveItem(area);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public void AddEventManagementList(List<Event> eventManagments)
    {
        App.Database.SaveItems(eventManagments);

        var xx = App.Database.GetItems<Event>();
    }

    [Time]
    public void AddEventManagementTypeList(List<EventManagementType> eventManagementTypeList)
    {
        foreach (var item in eventManagementTypeList)
        {
            var itemExsists = App.Database.GetItems<EventManagementType>()
                .Any(x => x.RemoteTableId == item.RemoteTableId);

            if (!itemExsists) App.Database.SaveItems(eventManagementTypeList);
        }
    }

    [Time]
    public async Task MarAsAbsent(JobData4Tablet jobData)
    {
        NavigationalParameters.CurrentSelectedJob = jobData;

        var diary = App.Database.GetItems<ClaimedNotesFile>()?.FirstOrDefault(x =>
            x.ContractReference == NavigationalParameters.CurrentSelectedJob.ContractReference
            && x.NotesDate == NavigationalParameters.CurrentSelectedJob.JobDate
            && x.NotesGang == NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString());

        if (diary == null)
        {
            diary = new ClaimedNotesFile
            {
                RemoteTableId = 0,
                NotesDate = NavigationalParameters.CurrentSelectedJob.JobDate.Date,
                ConPrefix = NavigationalParameters.CurrentSelectedJob?.ContractPrefix,
                ContractId = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                NotesGang = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString(),
                QuoteId = NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                PostedByGangerName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName,
                ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference,
                ApprovedBySupervisor = DateTime.Parse("02/02/1900 00:00:00"),
                NotesText = "Added by supervisor to set gang as absent"
            };
        }
        else
        {
            diary.NotesText = "Added by supervisor to set gang as absent";
            diary.ApprovedBySupervisor = DateTime.Parse("02/02/1900 00:00:00");
        }

        var crossedUtilities = App.Database.GetItems<ServicesCrossed4Tablet>()?.FirstOrDefault(x =>
            x.ContractReference == NavigationalParameters.CurrentSelectedJob.ContractReference
            && x.PostedDate == NavigationalParameters.CurrentSelectedJob.JobDate
            && x.GangLeaderId == NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString());

        if (crossedUtilities == null)
            crossedUtilities = new ServicesCrossed4Tablet
            {
                ContractReference = NavigationalParameters.CurrentSelectedJob.ContractReference,
                ContractId = NavigationalParameters.CurrentSelectedJob.ContractNumber.ToString(),
                GangLeaderId = NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString(),
                GangLeaderName = NavigationalParameters.CurrentSelectedJob.GangLeaderName,
                PostedDate = NavigationalParameters.CurrentSelectedJob.JobDate,
                QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                RemoteTableId = 0,
                ServicesCrossedData1 = "No Utilities Crossed"
            };
        else
            crossedUtilities.ServicesCrossedData1 = "No Utilities Crossed";

        var currentGang = NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles;

        foreach (var person in currentGang)
        {
            var currentLabour = App.Database.GetItems<Labour>()?.FirstOrDefault(x =>
                x.ContractReference == NavigationalParameters.CurrentSelectedJob.ContractReference
                && x.LabourGang == NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString()
                && x.LabourDate == NavigationalParameters.CurrentSelectedJob.JobDate
                && x.LabourOperative == person.FocusId.ToString());

            if (currentLabour == null)
            {
                currentLabour = new Labour
                {
                    LabourDate = NavigationalParameters.CurrentSelectedJob.JobDate.Date,
                    ContractId = NavigationalParameters.CurrentSelectedJob.ContractNumber
                        .ToString(),
                    LabourSupervisor = NavigationalParameters.CurrentSelectedJob.SupervisorId
                        .ToString(),
                    QuoteId = NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString(),
                    LabourGang = NavigationalParameters.CurrentSelectedJob.GangLeaderId
                        .ToString(),
                    LabourAddress = NavigationalParameters.CurrentSelectedJob.ProjectName,
                    LabourOperative = person.FocusId.ToString(),
                    ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference,
                    ClaimedorTracked = "T",
                    LabourType = "A",
                    ConPrefix = NavigationalParameters.CurrentSelectedJob.ContractPrefix,
                    TravelFromSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                    TravelToSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                    TimeLeftSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                    TimeOnSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                    ClaimedYardEnd = NavigationalParameters.CurrentSelectedJob.JobDate,
                    ClaimedYardStart = NavigationalParameters.CurrentSelectedJob.JobDate
                };
            }
            else
            {
                currentLabour.ApprovedBySupervisor = DateTime.Parse("02/02/1900 00:00:00");
                currentLabour.LabourType = "A";
                currentLabour.ConPrefix = NavigationalParameters.CurrentSelectedJob.ContractPrefix;
                currentLabour.TravelFromSite = NavigationalParameters.CurrentSelectedJob.JobDate;
                currentLabour.TravelToSite = NavigationalParameters.CurrentSelectedJob.JobDate;
                currentLabour.TimeLeftSite = NavigationalParameters.CurrentSelectedJob.JobDate;
                currentLabour.TimeOnSite = NavigationalParameters.CurrentSelectedJob.JobDate;
                currentLabour.ClaimedYardEnd = NavigationalParameters.CurrentSelectedJob.JobDate;
                currentLabour.ClaimedYardStart = NavigationalParameters.CurrentSelectedJob.JobDate;
            }

            App.Database.SaveItem(currentLabour);
        }

        App.Database.SaveItem(diary);
        App.Database.SaveItem(crossedUtilities);
    }

    public void AddPoleClaimed(VMexpansionReleaseData sp)
    {
        var poleclaimed = App.Database.GetItems<ClaimedPole>()
            ?.FirstOrDefault(x => x.ClaimId == NavigationalParameters.ClaimFile.ClaimId
                                  && x.WorksHeader == NavigationalParameters.ClaimFile.ClaimHeader &&
                                  x.PoleIdentifier == sp.ClaimId);

        if (poleclaimed != null)
        {
            poleclaimed.ClaimId = NavigationalParameters.ClaimFile.ClaimId;
            poleclaimed.DateOfEntry = DateTime.Now;
            poleclaimed.PoleIdentifier = sp.ClaimId;
            poleclaimed.WorksHeader = NavigationalParameters.ClaimFile.ClaimHeader;
            poleclaimed.QuoteId = NavigationalParameters.ClaimFile.QuoteId;
        }
        else
        {
            poleclaimed = new ClaimedPole
            {
                RemoteTableId = 0,
                ClaimId = NavigationalParameters.ClaimFile.ClaimId,
                DateOfEntry = DateTime.Now,
                PoleIdentifier = sp.ClaimId,
                WorksHeader = NavigationalParameters.ClaimFile.ClaimHeader,
                QuoteId = NavigationalParameters.ClaimFile.QuoteId
            };
        }

        App.Database.SaveItem(poleclaimed);
    }

    public void RemovePoleClaimed(VMexpansionReleaseData sp)
    {
        var poleclaimed = App.Database.GetItems<ClaimedPole>()
            ?.FirstOrDefault(x =>
                x.ClaimId == NavigationalParameters.ClaimFile.ClaimId &&
                x.WorksHeader == NavigationalParameters.ClaimFile.ClaimHeader && x.PoleIdentifier == sp.ClaimId);

        App.Database.DeleteItem(poleclaimed);
    }

    //[Time] public void AddEventManagementType(EventManagementInfo eventManagementInfo)
    //{
    //    App.Database.SaveItems(eventManagementInfo.EventManagementTypeList);

    //}

    [Time]
    public void AddCableRuns(List<CableRuns> cableRuns)
    {
        App.Database.SaveItems(cableRuns);
    }

    [Time]
    public void AddToolBoxTalks(ToolBoxTalks item)
    {
        var currentTT = App.Database.GetItems<ToolBoxTalks>().FirstOrDefault(x => x.FullName == item.FullName);

        if (currentTT == null) App.Database.SaveItem(item);
    }

    [Time]
    public void AddUserToolBoxTalks(UsersToolBoxTalks item)
    {
        var currentUTT = App.Database.GetItems<UsersToolBoxTalks>().FirstOrDefault(x =>
            x.ToolBoxTalkCode == item.ToolBoxTalkCode && x.FocusId == item.FocusId && x.QuoteId == item.QuoteId);

        if (currentUTT != null) item.Id = currentUTT.Id;

        App.Database.SaveItem(item);
    }

    [Time]
    public long GetCableStockUseMeasuresValue(CableStockAudit item)
    {
        var x = App.Database.GetItems<CableStockUse>()
            .Where(x => x.DrumNo == item.DrumNo && x.RemoteTableId == "0")
            .Sum(x => x.HowMuchUsed);

        return x;
    }

    [Time]
    public async Task AddCableStockUse(CableStockUse item)
    {
        try
        {
            App.Database.SaveItem(item);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async Task AddCableStockItem(CableStockAudit item)
    {
        try
        {
            App.Database.SaveItem(item);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async Task AddWeatherEvent(List<WeatherEvent> weatherEvents)
    {
        foreach (var weather in weatherEvents.Select(we => App.Database.GetItems<WeatherEvent>()?.FirstOrDefault(x =>
                     x.RemoteTableId == we.RemoteTableId)).Where(weather => weather == null))
            App.Database.SaveItem(weather);
    }

    [Time]
    public bool CheckAllPlantHasBeenExcepted()
    {
        var gangMembetIds = NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped?.Split('|');

        List<Plant4Tablet> plantItems;

        try
        {
            plantItems = App.Database.GetItems<Plant4Tablet>().ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            plantItems = NavigationalParameters.DataPassedToTablet.OperativesPlant;
        }

        if (plantItems.Any(x => x.Ontransfer))
            foreach (var id in gangMembetIds)
                if (plantItems.Any(x => x.TransferToId.ToString() == id && x.CurrentStatus.Contains("Assigned To")))
                    return false;

        return true;
    }

    [Time]
    public List<Labour> GetJobLabour(JobData4Tablet job)
    {
        return App.Database.GetItems<Labour>()?.Where(x => x.QuoteId == job.QuoteNumber.ToString()
                                                           && x.LabourGang == job.GangLeaderId.ToString()
                                                           && x.LabourDate == job.JobDate).ToList();
    }

    [Time]
    public List<Labour> GetJobLabour(TaskItem job)
    {
        return App.Database.GetItems<Labour>()?.Where(x => x.QuoteId == job.QuoteNumber.ToString()
                                                           && x.LabourGang == job.GangLeaderId.ToString()
                                                           && x.LabourDate == job.JobDate).ToList();
    }

    [Time]
    public List<Labour> GetLabourToApprove(TaskItem task)
    {
        // NavigationalParameters.CurrentSelectedJob = job;
        var labourFiles = App.Database.GetItems<Labour>().ToList();


        var lfr = labourFiles.Where(x => x.QuoteId == task.QuoteNumber.ToString() &&
                                         x.LabourGang == task.GangLeaderId.ToString()
                                         && x.LabourDate == task.JobDate
                                         && x.RemoteTableId > 0
                                         && x.ApprovedBySupervisor == NavigationalParameters.MinDateTime
                                         && x.PostedByGanger != NavigationalParameters.MinDateTime)
            .ToList();


        return lfr;
    }

    [Time]
    public async Task SaveCurrentInvestigation(Investigation currentInvestigation)
    {
        App.Database.SaveItem(currentInvestigation);
    }

    [Time]
    public List<Labour> GetLabourApproved(JobData4Tablet job)
    {
        NavigationalParameters.CurrentSelectedJob = job;

        var labourFiles = App.Database.GetItems<Labour>()?.Where(x => x.ContractReference == job.ContractReference
                                                                      && x.LabourGang == job.GangLeaderId.ToString()
                                                                      && x.LabourDate == job.JobDate
                                                                      && x.ApprovedBySupervisor !=
                                                                      DateTime.Parse("01/01/1900 00:00:00"))?.ToList();

        return labourFiles;
    }

    [Time]
    public async Task AddVisitor(VisitorLog visitor)
    {
        App.Database.SaveItem(visitor);
    }

    [Time]
    public List<UsersToolBoxTalks> GetItemsForToolboxTalks(long supervisorId)
    {
        return App.Database.GetItems<UsersToolBoxTalks>()
            .Where(x => x.SupervisorId == supervisorId)
            .ToList();
    }

    [Time]
    public async Task AddVisitors(List<VisitorLog> visitors)
    {
        App.Database.SaveItems(visitors);
    }

    [Time]
    public async Task AddReinstatmentMeasures(List<ReinstatementMeasure> reinstatementMeasures)
    {
        App.Database.SaveItems(reinstatementMeasures);
    }

    [Time]
    public async Task AddReinstatmentMeasure(ReinstatementMeasure reinstatementMeasure)
    {
        App.Database.SaveItem(reinstatementMeasure);
    }

    [Time]
    public async Task AddClaimedNote(ClaimedNotesFile passedNote)
    {
        App.Database.SaveItem(passedNote);
    }

    [Time]
    public async Task AddClaimedNotes(List<ClaimedNotesFile> passedNotes)
    {
        Debug.WriteLine($"Passed notes count {passedNotes?.Count}");

        foreach (var notesFile in passedNotes)
        {
            var noteToSave = App.Database.GetItems<ClaimedNotesFile>()?.FirstOrDefault(x =>
                x.RemoteTableId == notesFile.RemoteTableId
                && x.ApprovedBySupervisor == DateTime.Parse("02/02/1900 00:00:00"));

            if (noteToSave == null) App.Database.SaveItem(notesFile);
        }
    }

    [Time]
    public void AddSummaries(List<ProjectSummary> summaries)
    {
        App.Database.SaveItems(summaries);
    }

    [Time]
    public void AddUserDailyProjectTimes(UserDailyProjectTimes passedUserDailyProjectTimes)
    {
        App.Database.SaveItem(passedUserDailyProjectTimes);
    }

    [Time]
    public void AddUserDailyTime(UserDailyTime passedUserDailyTime)
    {
        App.Database.SaveItem(passedUserDailyTime);
    }

    [Time]
    public void AddUserDailyTimeNotes(UserDailyTimeNotes passedUserDailyTimeNotes)
    {
        App.Database.SaveItem(passedUserDailyTimeNotes);
    }

    [Time]
    public void AddDailyProjectNotes(DailyProjectNotes passedDailyProjectNotes)
    {
        App.Database.SaveItem(passedDailyProjectNotes);
    }

    [Time]
    public async Task AddNcr(Ncr passedncr)
    {
        App.Database.SaveItem(passedncr);
    }

    [Time]
    public async void AddMeasure(ClaimedFile passedClaim)
    {
        App.Database.SaveItem(passedClaim);
        //App.Database.SaveClaimedFileItem(passedClaim);
    }

    [Time]
    public void AddClaimedFile(ClaimedFile passedClaim)
    {
        App.Database.SaveClaimedFileItem(passedClaim);
    }

    [Time]
    public async void AddEndpoints(List<VMexpansionReleaseData> passedHomes)
    {
        try
        {
            foreach (var item in passedHomes)
            {
                var localEndpoints = App.Database.GetItems<VMexpansionReleaseData>()
                    ?.Where(x => x.StreetName == item.StreetName && x.Qnumber == item.Qnumber)?.ToList();

                if (!localEndpoints.Any()) App.Database.SaveItem(item);
            }


            //foreach (var home in passedHomes)
            //{
            //    if (string.IsNullOrEmpty(home?.BuildingNumber))
            //    {
            //        var itemTodelete = App.Database.GetItems<VMexpansionReleaseData>()
            //           .Where(x => x.StreetName == home?.StreetName).ToList();

            //        foreach (var items in itemTodelete)
            //        {
            //            App.Database.DeleteItem(items);
            //        }

            //        App.Database.SaveItem(home);
            //    }
            //    else if (localEndpoints.All(x => x.RemoteTableId != home.RemoteTableId))
            //    {
            //        App.Database.SaveItem(home);
            //    }
            //}
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async Task AddEndPoint(VMexpansionReleaseData passedHome)
    {
        try
        {
            App.Database.SaveItem(passedHome);
        }
        catch (Exception ex)
        {
            var error = ex.ToString();

            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }
    }

    [Time]
    public async Task AddClaimedFiles(List<ClaimedFile> passedClaims)
    {
        int calimTocheck1 = App.Database.GetItems<ClaimedFile>().Count();

        foreach (var claimFile in passedClaims)
        {
            var exsistingClaim = App.Database.GetItems<ClaimedFile>().Any(x => x.ClaimHeader == claimFile.ClaimHeader
                                                                            && x.ClaimId == claimFile.ClaimId
                                                                            && x.QuoteId == claimFile.QuoteId);
            if (!exsistingClaim)
            {
                App.Database.SaveItem(claimFile);
            }
        }

        int calimTocheck2 = App.Database.GetItems<ClaimedFile>().Count();
    }

    [Time]
    public async Task AddBlockages(List<Blockage> blockageList)
    {
        foreach (var item in blockageList) await AddBlockage(item);
    }

    [Time]
    public async Task AddBlockage(Blockage blockage)
    {
        var btu = App.Database.GetItems<Blockage>().FirstOrDefault(x => x.ImageId == blockage.ImageId);

        if (btu != null) blockage.Id = btu.Id;

        App.Database.SaveItem(blockage);
    }

    [Time]
    public void AddPermit(DigPermit permit)
    {
        App.Database.SaveItem(permit);
    }

    [Time]
    public void AddPermits(List<DigPermit> permits)
    {
        App.Database.SaveItems(permits);
    }

    [Time]
    public void AddAuditList(List<Audit> audits)
    {
        App.Database.SaveItems(audits);
    }

    [Time]
    public async Task AddRates(List<Rates> rates)
    {
        App.Database.SaveItems(rates);
    }

    [Time]
    public async Task AddCrossedUtilities(ServicesCrossed4Tablet crossedUtilities)
    {
        App.Database.SaveItem(crossedUtilities);
    }

    [Time]
    public async Task AddCrossedUtilitiesList(List<ServicesCrossed4Tablet> crossedUtilitiesList)
    {
        App.Database.SaveItems(crossedUtilitiesList);
    }

    [Time]
    public ToolBoxTalks GetToolboxTalk(string toolBoxTalkCode)
    {
        return App.Database.GetItems<ToolBoxTalks>()?.FirstOrDefault(x => x.FullName == toolBoxTalkCode);
    }

    [Time]
    public ClaimedNotesFile GetClaimedNotes(TaskItem taskItem)
    {
        try
        {
            var claimedNotes = App.Database.GetItems<ClaimedNotesFile>().Where(x =>
                x.QuoteId == taskItem.QuoteNumber.ToString()
                && x.NotesDate == taskItem.JobDate.Date
                && x.NotesGang.ToString() == taskItem.GangLeaderId.ToString()
                && x.ApprovedBySupervisor == NavigationalParameters.MinDateTime
                && x.PostedByGanger != NavigationalParameters.MinDateTime).ToList();

            var diaryCount = claimedNotes?.Count();


            foreach (var diary in claimedNotes)
                if (diaryCount > 1)
                {
                    diaryCount--;
                    App.Database.DeleteItem(diary);
                }

            var chck = claimedNotes?.FirstOrDefault(x =>
                x.QuoteId == taskItem.QuoteNumber.ToString()
                && x.NotesDate == taskItem.JobDate.Date
                && x.NotesGang.ToString() == taskItem.GangLeaderId.ToString()
                && x.ApprovedBySupervisor == NavigationalParameters.MinDateTime
                && x.PostedByGanger != NavigationalParameters.MinDateTime);

            return chck;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return null;
        }
    }

    [Time]
    public List<ClaimedNotesFile> GetAllClaimedNotes()
    {
        try
        {
            return App.Database.GetItems<ClaimedNotesFile>().ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.DailyDiary;
        }
    }


    [Time]
    public List<VMexpansionReleaseData> GetEndpoints(JobData4Tablet currentJob)
    {
        List<VMexpansionReleaseData> endpoints = null;
        // NavigationalParameters.CurrenSelectedJob = currentJob;
        try
        {
            //var ed = App.Database.GetItems<VMexpansionReleaseData>().ToList();
            // var x = ed.FirstOrDefault(z => z.RemoteTableId == 50950);
            var ep = App.Database.GetItems<VMexpansionReleaseData>()
                .Where(x => x.Qnumber == currentJob.QuoteNumber)
                .ToList(); //ToConnect


            return ep;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            //var ed = App.Database.GetItems<VMexpansionReleaseData>().ToList();
            return NavigationalParameters.DataPassedToTablet.EndPoints
                .Where(x => x.Qnumber == NavigationalParameters.CurrentSelectedJob?.QuoteNumber)
                .ToList(); //ToConnect
        }
    }

    [Time]
    public List<VMexpansionReleaseData> GetEndpoints(TaskItem currentTask)
    {
        List<VMexpansionReleaseData> endpoints = null;
        // NavigationalParameters.CurrenSelectedJob = currentJob;
        try
        {
            //var ed = App.Database.GetItems<VMexpansionReleaseData>().ToList();
            // var x = ed.FirstOrDefault(z => z.RemoteTableId == 50950);
            return App.Database.GetItems<VMexpansionReleaseData>()
                .Where(x => x.Qnumber == currentTask.QuoteNumber)
                .ToList(); //ToConnect
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            //var ed = App.Database.GetItems<VMexpansionReleaseData>().ToList();
            return NavigationalParameters.DataPassedToTablet.EndPoints
                .Where(x => x.Qnumber == NavigationalParameters.CurrentSelectedJob?.QuoteNumber)
                .ToList(); //ToConnect
        }
    }

    [Time]
    public List<VMexpansionReleaseData> GetEndpoints(Assignment assignment)
    {
        List<VMexpansionReleaseData> endpoints = null;
        try
        {
            endpoints = App.Database.GetItems<VMexpansionReleaseData>()?.Where(x => x.Qnumber == assignment.Qnumber)
                .ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }

        return endpoints;
    }

    [Time]
    public List<ClaimedFile> GetJobClaimedFiles(JobData4Tablet currentJob)
    {
        var claimedFiles = App.Database.GetItems<ClaimedFile>().ToList();

        claimedFiles = claimedFiles?.Where(x =>
            x.QuoteId == currentJob.QuoteNumber.ToString()
            && x.ClaimDate.Date == currentJob.JobDate.Date
            && x.ClaimGang == currentJob?.GangLeaderId.ToString()
            && x.ClaimSupervisor == NavigationalParameters.LoggedInUser.FocusId.ToString()
            && x.ApprovedBySupervisor == NavigationalParameters.MinDateTime
            && x.PostedByGanger != NavigationalParameters.MinDateTime)?.ToList();

        return claimedFiles;
    }

    [Time]
    public List<ClaimedFile> GetJobClaimedFiles()
    {
        var claimedFiles = App.Database.GetItems<ClaimedFile>()?.ToList();

        var claimedFiles2 = claimedFiles?.Where(x =>
            x.QuoteId == NavigationalParameters.CurrentSelectedJob.QuoteNumber.ToString()
            //&& x.ClaimDate.Date == NavigationalParameters.CurrentSelectedJob.JobDate.Date
            && x.ClaimGang == NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString()
            && x.ClaimSupervisor == NavigationalParameters.CurrentSelectedJob.SupervisorId.ToString()
            && x.RemoteTableId == 0)?.ToList();

        return claimedFiles2;
    }

    [Time]
    public List<ClaimedFile> GetClaimedFiles(TaskItem taskItem)
    {
        var claimedFiles = new List<ClaimedFile>();

        var claimedFiles2 = App.Database.GetItems<ClaimedFile>()?.Where(x =>
         x.QuoteId == taskItem.QuoteNumber.ToString()
         && x.ClaimDate.Date == taskItem.JobDate.Date
         && x.ClaimGang == taskItem?.GangLeaderId.ToString()
         && x.ClaimSupervisor == NavigationalParameters.LoggedInUser.FocusId.ToString()
         && string.IsNullOrEmpty(x.ApprovedBySupervisorName)
         && x.PostedByGanger != NavigationalParameters.MinDateTime)?.ToList();

        foreach (var cf in claimedFiles2)
        {
            if (!claimedFiles.Any(x => x.ClaimHeader == cf.ClaimHeader && x.QuoteId == cf.QuoteId && x.ClaimId == cf.ClaimId)) { claimedFiles.Add(cf); }
        }

      

        return claimedFiles;
    }

    [Time]
    public List<ClaimedFile> GetApprovedClaimedFiles(JobData4Tablet currentJob)
    {
        NavigationalParameters.CurrentSelectedJob = currentJob;
        var claimedFiles = App.Database.GetItems<ClaimedFile>()?
            .Where(x => x.ContractReference == currentJob.ContractReference
                        && x.ClaimDate == currentJob.JobDate.Date
                        && x.ClaimGang == currentJob.GangLeaderId.ToString()
                        && x.ApprovedBySupervisor != DateTime.Parse("01/01/1900 00:00:00")).ToList();

        return claimedFiles;
    }

    [Time]
    public List<ClaimedFile> GetApprovedClaimedFiles(TaskItem ti)
    {
        //NavigationalParameters.CurrentSelectedJob = ti;
        var claimedFiles = App.Database.GetItems<ClaimedFile>()?
            .Where(x => x.QuoteId == ti.QuoteNumber.ToString()
                        && x.ClaimDate == ti.JobDate.Date
                        && x.ClaimGang == ti.GangLeaderId.ToString()
                        && x.ApprovedBySupervisor != DateTime.Parse("01/01/1900 00:00:00")).ToList();

        return claimedFiles;
    }

    [Time]
    public List<VisitorLog> GetVisitors(JobData4Tablet currentJob)
    {
        var visitorList = new List<VisitorLog>();

        try
        {
            var visitorListTemp1 = App.Database.GetItems<VisitorLog>();
            var cr = NavigationalParameters.CurrentSelectedJob.ContractReference;
            var jd = NavigationalParameters.CurrentSelectedJob.JobDate.Date;
            var gl = NavigationalParameters.CurrentSelectedJob.GangLeaderName;


            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                visitorListTemp = visitorListTemp1?.Where(x =>
                    x.ContractReference == cr
                    && x.OnSiteTime.Date == jd
                    && x.GangLeader == gl).ToList();
            else
                visitorListTemp = visitorListTemp1?.Where(x =>
                    x.ContractReference == cr
                    && x.OnSiteTime.Date == jd).ToList();


            foreach (var visitor in visitorListTemp.Where(visitor =>
                         visitorList.All(x => x.SignatureIn != visitor.SignatureIn)))
            {
                visitor.FullName = $"{visitor.Forename} {visitor.Surname}";
                // visitor.OffSiteTime = null;


                if (visitor.OffSiteTime != null && visitor.OffSiteTime > DateTime.Parse("1/1/1900 00:00:00"))
                {
                    visitor.OffSite = true;
                    visitor.OnSite = false;
                }
                else
                {
                    visitor.OffSite = false;
                    visitor.OnSite = true;
                }

                //visitor.OffSite = visitor.OffSiteTime > DateTime.Parse("1/1/1900 00:00:00") && visitor.OffSiteTime != null;

                //    visitor.OnSite = (visitor.OnSiteTime > DateTime.Parse("1/1/1900 00:00:00") )&& visitor.OnSiteTime != null && (visitor.OffSiteTime == DateTime.Parse("1/1/1900 00:00:00") || visitor.OffSiteTime == null);
                visitorList.Add(visitor);
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");
        }

        return visitorList;
    }

    [Time]
    public ReinstatementMeasure GetReinstatementMeasure(JobData4Tablet currentJob)
    {
        NavigationalParameters.CurrentSelectedJob = currentJob;
        // var list = App.Database.GetItems<ReinstatementMeasure>().ToList();
        var reinstatement = App.Database.GetItems<ReinstatementMeasure>()
            .FirstOrDefault(x =>
                x.ContractReference == NavigationalParameters.CurrentSelectedJob.ContractReference
                && x.GangLeaderId == NavigationalParameters.CurrentSelectedJob.GangLeaderId
                && x.QuoteNumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber
                && x.JobDate.Date == NavigationalParameters.CurrentSelectedJob.JobDate.Date);

        NavigationalParameters.ReinstatmentMeasureToApprove = reinstatement != null;

        return reinstatement;
    }

    [Time]
    public List<ReinstatementMeasure> GetReinstatementMeasures(TaskItem SelectedTaskItem)
    {
        // NavigationalParameters.CurrentSelectedJob = currentJob;
        // var list = App.Database.GetItems<ReinstatementMeasure>().ToList();
        return App.Database.GetItems<ReinstatementMeasure>()?.Where(x =>
            x.RemoteTableId == 0
            && x.GangLeaderId == SelectedTaskItem.GangLeaderId
            && x.QuoteNumber == SelectedTaskItem.QuoteNumber
            && x.JobDate.Date == SelectedTaskItem.JobDate.Date)?.ToList();
        // NavigationalParameters.ReinstatmentMeasureToApprove = reinstatement != null;
        //return reinstatement;
    }

    [Time]
    public List<ReinstatementMeasure> GetReinstatementMeasures(JobData4Tablet selectedJob)
    {
        // NavigationalParameters.CurrentSelectedJob = currentJob;
        // var list = App.Database.GetItems<ReinstatementMeasure>().ToList();
        return App.Database.GetItems<ReinstatementMeasure>()?.Where(x =>
            x.RemoteTableId == 0
            && x.GangLeaderId == selectedJob.GangLeaderId
            && x.QuoteNumber == selectedJob.QuoteNumber
            && x.JobDate.Date == selectedJob.JobDate.Date)?.ToList();
        // NavigationalParameters.ReinstatmentMeasureToApprove = reinstatement != null;
        //return reinstatement;
    }

    [Time]
    public List<Rates> GetAllRates()
    {
        var rates = NavigationalParameters.DataPassedToTablet.SchemeWorks; // App.Database.GetItems<Rates>();
        return rates.ToList();
    }

    [Time]
    public async Task<JobData4Tablet> GetCurrentSelectedJob()
    {
        return NavigationalParameters.CurrentSelectedJob != null
            ? NavigationalParameters.CurrentSelectedJob
            : App.Database.GetItems<JobData4Tablet>()?.FirstOrDefault(x => x.IsSelected);
    }

    [Time]
    public JobData4Tablet GetJob(string reference)
    {
        return App.Database.GetItems<JobData4Tablet>()?.FirstOrDefault(x => x.ContractReference == reference);
    }

    [Time]
    public ServicesCrossed4Tablet GetCrossedUtilities(JobData4Tablet job)
    {
        try
        {
            return App.Database.GetItems<ServicesCrossed4Tablet>()?.FirstOrDefault(x =>
                x.ContractReference == NavigationalParameters.CurrentSelectedJob.ContractReference
                && x.GangLeaderId == NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString()
                && x.PostedDate.Date == NavigationalParameters.CurrentSelectedJob.JobDate.Date);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.ServicesCrossed.FirstOrDefault(x =>
                x.ContractReference == NavigationalParameters.CurrentSelectedJob?.ContractReference
                && x.GangLeaderId == NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString()
                && x.PostedDate.Date == NavigationalParameters.CurrentSelectedJob?.JobDate.Date);
        }
    }

    [Time]
    public Gang GetCurrentSelectedGang()
    {
        var user = new Users();
        var gangInfo2Return = new Gang();
        var gm = new List<GangMember>();
        var gang = new List<Person>();
        var TempGm = new List<GangMember>();
        var TempGang = new List<Person>();

        try
        {
            try
            {
                GangMemberToAdd =
                    user.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob?.ProjectManagerId));
            }
            catch (Exception ex)
            {
                GangMemberToAdd = NavigationalParameters.DataPassedToTablet.PersonData?.FirstOrDefault(x =>
                    x.FocusId
                    == NavigationalParameters.CurrentSelectedJob?.ProjectManagerId);
            }

            if (GangMemberToAdd != null)
            {
                gangInfo2Return.ProjectManagerId = GangMemberToAdd.FocusId;
                gangInfo2Return.ProjectManagerFirstName = GangMemberToAdd.FirstName;
                gangInfo2Return.ProjectManagerSurname = GangMemberToAdd.Surname;
                gangInfo2Return.ProjectManagerPin = GangMemberToAdd.MemberPin;
            }

            try
            {
                GangMemberToAdd =
                    user.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob?.SupervisorId));
            }
            catch (Exception ex)
            {
                GangMemberToAdd = NavigationalParameters.DataPassedToTablet.PersonData?.FirstOrDefault(x =>
                    x.FocusId
                    == NavigationalParameters.CurrentSelectedJob?.SupervisorId);
            }

            if (GangMemberToAdd != null)
            {
                gangInfo2Return.SupervisorId = GangMemberToAdd.FocusId;
                gangInfo2Return.SupervisorFirstName = GangMemberToAdd.FirstName;
                gangInfo2Return.SupervisorSurname = GangMemberToAdd.Surname;
                gangInfo2Return.SupervisorPin = GangMemberToAdd.MemberPin;
            }

            try
            {
                GangMemberToAdd =
                    user.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob?.GangLeaderId));

                NavigationalParameters.CurrentSelectedJob.GangLeaderName = GangMemberToAdd.FullName;
            }
            catch (Exception ex)
            {
                GangMemberToAdd = NavigationalParameters.DataPassedToTablet.PersonData?.FirstOrDefault(x =>
                    x.FocusId
                    == NavigationalParameters.CurrentSelectedJob?.GangLeaderId);
            }

            if (GangMemberToAdd != null)
            {
                gangInfo2Return.GangLeaderId = GangMemberToAdd.FocusId;
                gangInfo2Return.GangLeaderFirstName = GangMemberToAdd?.FirstName;
                gangInfo2Return.GangLeaderSurname = GangMemberToAdd?.Surname;
                gangInfo2Return.GangLeaderPin = GangMemberToAdd?.MemberPin;

                if (gang.All(x => x.FocusId != GangMemberToAdd.FocusId)) gang.Add(GangMemberToAdd);
            }

            if ((bool)NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped.Contains("|"))
            {
                var operativeIDs = NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped.Split('|').ToList();


                foreach (var item in operativeIDs)
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (item.Length > 3)
                        {
                            GangMemberToAdd = user.GetUserById(Convert.ToInt32(item));

                            if (GangMemberToAdd != null)
                            {
                                if (GangMemberToAdd.MyGroups.Any(x => x.GroupName.Contains("Leader")) &&
                                    GangMemberToAdd.FocusId == NavigationalParameters.CurrentSelectedJob.GangLeaderId)
                                {
                                    if (gm.All(x => x.Id != GangMemberToAdd.FocusId))
                                    {
                                        if (gang.All(x => x.FocusId != GangMemberToAdd.FocusId))
                                            gang.Add(GangMemberToAdd);

                                        gm.Add(new GangMember
                                        {
                                            Id = Convert.ToInt64(item),
                                            FirstName = GangMemberToAdd.FirstName,
                                            Surname = GangMemberToAdd.Surname,
                                            GangMemberPin = GangMemberToAdd.MemberPin
                                        });
                                    }
                                }
                                else
                                {
                                    if (TempGang.All(x => x.FocusId != GangMemberToAdd.FocusId))
                                    {
                                        TempGang.Add(GangMemberToAdd);

                                        TempGm.Add(new GangMember
                                        {
                                            Id = Convert.ToInt64(item),
                                            FirstName = GangMemberToAdd.FirstName,
                                            Surname = GangMemberToAdd.Surname,
                                            GangMemberPin = GangMemberToAdd.MemberPin
                                        });
                                    }
                                }
                            }
                        }

                        NavigationalParameters.CurrentSelectedJob.OperativeNames += $"{GangMemberToAdd.FullName}, ";
                    }

                gang.AddRange(TempGang);

                gm.AddRange(TempGm);

                NavigationalParameters.CurrentSelectedJob.OperativeNames.TrimEnd(',');

                gangInfo2Return.GangMembers = gm;

                gangInfo2Return.GangLabourFiles = gang;

                NavigationalParameters.CurrentSelectedJob.JobGang = gangInfo2Return;
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }

        return gangInfo2Return;
    }

    [Time]
    public bool CheckStartOfDayPictureExsits(string picType)
    {
        try
        {
            return App.Database.GetItems<Pictures4Tablet>().Any(x =>
                x.ContractReference == NavigationalParameters.CurrentSelectedJob?.ContractReference
                && x.JobId == NavigationalParameters.CurrentSelectedJob?.JobId
                && x.OperativeId == NavigationalParameters.CurrentSelectedJob?.GangLeaderId
                && x.PictureReason == picType);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return false;
        }
    }

    [Time]
    public DataPassed2Server GetNewNewCabinetsAndEndPoints(DataPassed2Server dataPassed)
    {
        dataPassed.EndPoints = App.Database.GetItems<VMexpansionReleaseData>()?.Where(x => x.Qnumber ==
            NavigationalParameters
                .CurrentSelectedJob
                .QuoteNumber && x.SavedToServer == false).ToList();

        dataPassed.CableRuns = App.Database.GetItems<CableRuns>()
            .Where(x => x.QNumber == dataPassed.JobData.QuoteNumber && x.RemoteTableId == 0).ToList();


        return dataPassed;
    }

    [Time]
    public async Task<bool> UploadBlockageAsync(Blockage blockage)
    {
        var success = true;

        var pics = _pictureService.GetAllPictures().Where(x => x.Identifier == blockage.ImageId).ToList();

        var data2Upload = new DataPassed2Server
        {
            Category = "Blockage",
            JobData = NavigationalParameters.CurrentSelectedJob,
            BlockageList = new List<Blockage> { NavigationalParameters.BlockageItem },
            JobPictureData = pics,
            Locations = GetLoactaions(NavigationalParameters.BlockageItem)
        };

        var x = await _api.CanWeConnect2Api();

        success = x ? await SaveDailyInputAsync(data2Upload) : false;

        return success;
    }


    [Time]
    public async Task<bool> UploadCableRun(CableRuns cableRuns)
    {
        try
        {
            var x = await _api.CanWeConnect2Api();

            if (x)
            {
                var result = await _api.UpdateCableRun(cableRuns);
                return result;
            }

            return false;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return false;
        }
    }


    [Time]
    public List<Location> GetLoactaions(Blockage blockageItem)
    {
        return App.Database.GetItems<Location>()?.Where(x =>
            x.Identifier == blockageItem.ImageId).ToList();
    }

    [Time]
    public async Task<bool> UploadPermitsAsync(Assignment assignment)
    {
        // var cviToLoad = _assignmentService.GenerateAssignments2SaveById(assignment);
        var success = true;

        var data2Upload = new DataPassed2Server
        {
            Category = assignment.Category,
            JobData = NavigationalParameters.CurrentSelectedJob,
            //JobPictureData = cviToLoad.SurveyPictures,
            Assignments = new List<Assignment> { assignment }
        };

        //data2Upload.Assignments.FirstOrDefault().SurveyPictures.Clear();


        var x = await _api.CanWeConnect2Api();

        if (x)
            success = await SaveDailyInputAsync(data2Upload);
        else
            success = false;

        // if (success) App.Database.UpdateSavedItems(data2Upload);

        //Debug.WriteLine($"CVI UPLOAD {success}");
        if (success)
        {
        }

        //  UpdateDailyMeasures(data2Upload);
        return success;
    }

    [Time]
    public async Task<bool> UploadReInstatementMeasuresAsync()
    {
        // var cviToLoad = _assignmentService.GenerateAssignments2SaveById(assignment);
        var success = true;

        var data2Upload = new DataPassed2Server
        {
            Category = "ReInstatement",
            JobData = NavigationalParameters.CurrentSelectedJob,
            //JobPictureData = cviToLoad.SurveyPictures,
            ReinstatementMeasures = GetReinstatementMeasures(NavigationalParameters.CurrentSelectedJob)
        };

        //data2Upload.Assignments.FirstOrDefault().SurveyPictures.Clear();


        var connected = await _api.CanWeConnect2Api();

        if (connected)
            success = await SaveDailyInputAsync(data2Upload);
        else
            success = false;

        // if (success) App.Database.UpdateSavedItems(data2Upload);

        //Debug.WriteLine($"CVI UPLOAD {success}");

        return success;
    }

    [Time]
    public async Task<bool> UploadCviAsync(Assignment assignment)
    {
        // var cviToLoad = _assignmentService.GenerateAssignments2SaveById(assignment);
        var success = true;

        var data2Upload = new DataPassed2Server
        {
            Category = "CVI",
            JobData = NavigationalParameters.CurrentSelectedJob,
            //JobPictureData = cviToLoad.SurveyPictures,
            Assignments = new List<Assignment> { assignment }
        };

        //data2Upload.Assignments.FirstOrDefault().SurveyPictures.Clear();


        var connected = await _api.CanWeConnect2Api();

        if (connected)
            success = await SaveDailyInputAsync(data2Upload);
        else
            success = false;

        // if (success) App.Database.UpdateSavedItems(data2Upload);

        //Debug.WriteLine($"CVI UPLOAD {success}");

        return success;
    }

    [Time]
    public bool CheckUploadVisibility(JobData4Tablet currentSelectedJob)
    {
        var notesApproved = false;
        var claimsApproved = false;
        var labourApproved = false;

        if (GetClaimedNotes(NavigationalParameters
                .CurrentSelectedJob) != null && GetClaimedNotes(NavigationalParameters
                .CurrentSelectedJob).ApprovedBySupervisor != DateTime.Parse("01/01/1900 00:00:00"))
            notesApproved = true;
        else if (GetApprovedClaimedFiles(NavigationalParameters
                     .CurrentSelectedJob) != null && GetApprovedClaimedFiles(NavigationalParameters
                     .CurrentSelectedJob).Any())
            claimsApproved = true;
        else if (GetLabourApproved(NavigationalParameters
                     .CurrentSelectedJob) != null && GetLabourApproved(NavigationalParameters.CurrentSelectedJob).Any())
            labourApproved = true;

        return notesApproved && claimsApproved && labourApproved;
    }

    [Time]
    public Gang GetGang(JobData4Tablet job)
    {
        // NavigationalParameters.CurrenSelectedJob = job;
        var gangInfo2Return = new Gang();

        var gang = new List<Person>();

        try
        {
            var user = new Users();

            Person gangMember;

            if (Convert.ToInt32(job?.ProjectManagerId) > 0)
            {
                gangMember = user.GetUserById(Convert.ToInt32(job?.ProjectManagerId));

                if (gangMember != null)
                {
                    gangInfo2Return.ProjectManagerId = job.ProjectManagerId;
                    gangInfo2Return.ProjectManagerFirstName = gangMember.FirstName;
                    gangInfo2Return.ProjectManagerSurname = gangMember.Surname;
                    gangInfo2Return.ProjectManagerPin = gangMember.MemberPin;
                }
            }

            gangMember = user.GetUserById(Convert.ToInt32(job.SupervisorId));
            if (gangMember != null)
            {
                gangInfo2Return.SupervisorId = job.SupervisorId;
                gangInfo2Return.SupervisorFirstName = gangMember.FirstName;
                gangInfo2Return.SupervisorSurname = gangMember.Surname;
                gangInfo2Return.SupervisorPin = gangMember.MemberPin;
            }


            var gm = new List<GangMember>();
            var operativeIDs = new List<string>();

            gangMember = user.GetUserById(Convert.ToInt32(job.GangLeaderId));
            if (gangMember != null)
            {
                gangInfo2Return.GangLeaderId = job.GangLeaderId;
                gangInfo2Return.GangLeaderFirstName = gangMember.FirstName;
                gangInfo2Return.GangLeaderSurname = gangMember.Surname;
                gangInfo2Return.GangLeaderPin = gangMember.MemberPin;

                gm.Add(new GangMember
                {
                    Id = Convert.ToInt64(gangMember.FocusId),
                    FirstName = gangMember.FirstName,
                    Surname = gangMember.Surname,
                    GangMemberPin = gangMember.MemberPin
                });
            }

            if (job.OperativeIdsPiped.Contains("|"))
                //  var notesList = GetClaimedNotes(currentSelectedJob);
                operativeIDs = job.OperativeIdsPiped.Split('|').ToList();
            else
                operativeIDs.Add(job.GangLeaderId.ToString());

            foreach (var item in operativeIDs.Where(item => item != null && item.Length > 3))
            {
                gangMember = user.GetUserById(Convert.ToInt32(item));

                if (gangMember != null && gm.All(x => x.Id != gangMember.FocusId))
                {
                    gang.Add(gangMember);

                    gm.Add(new GangMember
                    {
                        Id = Convert.ToInt64(item),
                        FirstName = gangMember.FirstName,
                        Surname = gangMember.Surname,
                        GangMemberPin = gangMember.MemberPin
                    });
                }
            }

            gangInfo2Return.GangMembers = gm;

            gangInfo2Return.GangLabourFiles = gang;

            if (NavigationalParameters.CurrentSelectedJob?.JobGang != null)
                NavigationalParameters.CurrentSelectedJob.JobGang.GangMembers = gm;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var n = ex.Message;
        }

        return gangInfo2Return;
    }

    //Retrieves only information required for approval
    [Time]
    public Gang GetGang4Approval(JobData4Tablet job)
    {
        var gangInfo2Return = new Gang();
        var gang = new List<Person>();
        NavigationalParameters.CurrentSelectedJob = job;
        var user = new Users();

        Person p;

        p = user.GetUserById((int)NavigationalParameters.CurrentSelectedJob.SupervisorId);
        gangInfo2Return.SupervisorId = NavigationalParameters.CurrentSelectedJob.SupervisorId;
        gangInfo2Return.SupervisorFirstName = p.FirstName;
        gangInfo2Return.SupervisorSurname = p.Surname;
        gangInfo2Return.SupervisorPin = p.MemberPin;

        p = user.GetUserById4Approval((int)NavigationalParameters.CurrentSelectedJob?.GangLeaderId);
        gangInfo2Return.GangLeaderId = NavigationalParameters.CurrentSelectedJob.GangLeaderId;
        gangInfo2Return.GangLeaderFirstName = p.FirstName;
        gangInfo2Return.GangLeaderSurname = p.Surname;

        if (!string.IsNullOrEmpty(NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped) &&
            NavigationalParameters.CurrentSelectedJob.OperativeIdsPiped.Contains("|"))
        {
            var gm = new List<GangMember>();

            foreach (var item in NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped.Split('|'))
                if (item != null && item.Length > 3)
                {
                    p = user.GetUserById4Approval(int.Parse(item));

                    if (p != null && gm.All(x => x.Id != p.FocusId))
                    {
                        gang.Add(p);

                        gm.Add(new GangMember
                        {
                            Id = long.Parse(item),
                            FirstName = p.FirstName,
                            Surname = p.Surname,
                            GangMemberPin = p.MemberPin
                        });
                    }
                }

            gangInfo2Return.GangMembers = gm;
            gangInfo2Return.GangLabourFiles = gang;
        }

        return gangInfo2Return;
    }

    [Time]
    public List<UserDailyProjectTimes> GetUserDailyProjectTimes(long id)
    {
        return App.Database.GetItems<UserDailyProjectTimes>().ToList();
    }

    [Time]
    public List<UserDailyTime> GetUserDailyTime(long id)
    {
        return App.Database.GetItems<UserDailyTime>().ToList();
    }

    [Time]
    public List<UserDailyTimeNotes> GetUserDailyTimeNotes(long id)
    {
        return App.Database.GetItems<UserDailyTimeNotes>().ToList();
    }

    [Time]
    public List<DailyProjectNotes> GetDailyProjectNotes(long id)
    {
        return App.Database.GetItems<DailyProjectNotes>().ToList();
    }

    [Time]
    public void SaveClaim(ClaimedFile newClaim)
    {
        App.Database.SaveClaimedFileItem(newClaim);
    }

    [Time]
    public async Task SaveEndPoint(VMexpansionReleaseData claimEndpoints)
    {
        claimEndpoints.SavedToServer = false;
        App.Database.SaveItem(claimEndpoints);
    }

    [Time]
    public async Task UpdateEndPoints(Tuple<List<VMexpansionReleaseData>, Guid> claimEndpoints)
    {
        foreach (var item in claimEndpoints.Item1)
        {
            item.ClaimId = claimEndpoints.Item2;
            item.SavedToServer = false;
            App.Database.SaveItem(item);
        }
    }

    [Time]
    public async Task UpdateEndPoints(VMexpansionReleaseData claimEndpoint)
    {
        claimEndpoint.SavedToServer = false;
        App.Database.SaveItem(claimEndpoint);
    }

    [Time]
    public async Task<bool> SaveDailyInputAsync(DataPassed2Server dailyMeasures)
    {
        if (NavigationalParameters.CurrentSelectedJob != null)
        {
            var currentSelectedJob = GetJob(NavigationalParameters.CurrentSelectedJob?.ContractReference);

            if (currentSelectedJob != null)
            {
                dailyMeasures.JobData.ContractReference = currentSelectedJob?.ContractReference;
                dailyMeasures.JobData.SubContractorCompanyId = currentSelectedJob.SubContractorCompanyId;
                dailyMeasures.JobData.SubcontractorLabourTeamId = currentSelectedJob.SubcontractorLabourTeamId;
            }
        }

        if (dailyMeasures.VisitorLogs.Any())
            foreach (var visitor in dailyMeasures.VisitorLogs)
            {
                if (!string.IsNullOrEmpty(visitor.SignatureIn))
                {
                    var picStatus =
                        _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", visitor.SignatureIn);
                }

                if (!string.IsNullOrEmpty(visitor.SignatureOut))
                {
                    var picStatus =
                        _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", visitor.SignatureOut);
                }
            }

        foreach (var pic in dailyMeasures.JobPictureData)
            try
            {
                var path = "";

                switch (pic.PictureReason)
                {
                    case "Immediate":
                        path = "NonConformancePics";
                        break;
                    case "Detail":
                        path = "NonConformancePics";
                        break;
                    case "Corrective":
                        path = "NonConformancePics";
                        break;
                    case "Audit":
                        path = "JobPictures";
                        break;
                    default:
                        path = "JobPictures";
                        break;
                }

                var picStatus = await _pictureService.SyncPicture2Azure("JobPictures", path,
                    pic.FileName);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }           

        if (dailyMeasures.Assignments.Any())
            foreach (var assignment in dailyMeasures.Assignments)
            {
                foreach (var permit in assignment.PermitToDigList)
                {
                    if (!string.IsNullOrEmpty(permit.SupervisorSignature))
                    {
                        var picStatus = _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                            permit.SupervisorSignature);
                    }

                    if (!string.IsNullOrEmpty(permit.OperativeSignature))
                    {
                        var picStatus = _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                            permit.OperativeSignature);
                    }
                }

                foreach (var cvi in assignment.Cvi)
                {
                    if (!string.IsNullOrEmpty(cvi.SupervisorSignature))
                    {
                        var picStatus =
                            _pictureService.SyncPicture2Azure("JobPictures", "CVIpics",
                                cvi.SupervisorSignature);
                    }

                    if (!string.IsNullOrEmpty(cvi.OnBehalfOfClientSignature))
                    {
                        var picStatus =
                            _pictureService.SyncPicture2Azure("JobPictures", "CVIpics",
                                cvi.OnBehalfOfClientSignature);
                    }
                }

                // foreach (var audit in assignment.Audit)
                if (assignment.Audit != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(assignment.Audit?.AuditorSignature))
                        {
                            var picStatus = _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                                assignment.Audit.AuditorSignature);
                        }

                        if (!string.IsNullOrEmpty(assignment.Audit?.AuditeeSignature))
                        {
                            var picStatus = _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                                assignment.Audit.AuditeeSignature);
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.ToString();
                    }
                }
                    
                foreach (var pic in assignment.SurveyPictures)
                {
                    try
                    {
                        if (assignment.Category.ToLower() == "cvi")
                        {
                            var picStatus =
                                await _pictureService.SyncPicture2Azure("JobPictures", "CVIpics", pic.FileName);
                        }
                        else
                        {
                            var picStatus =
                                await _pictureService.SyncPicture2Azure("JobPictures", "JobPictures", pic.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.ToString();
                    }                   
                }

                assignment.Complete = "true";

                App.Database.SaveItem(assignment);
            }

        if (NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.AUDIT
            || NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.SITECLEAR
            || NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.VERTISHOREPERMIT
            || NavigationalParameters.SurveyType == NavigationalParameters.SurveyTypes.PERMITTODIG)
        {
            var result = await _api.SaveAssignments2Server(dailyMeasures.Assignments);

            if (result) return UpdateDailyMeasures(dailyMeasures);
        }
        else
        {
            var result = await _api.SaveDailyMeasures(dailyMeasures);

            if (result)
            {
                if (dailyMeasures.Category.ToUpper().Contains("GANGDAILYMEASURES")
                    || (dailyMeasures.Category.ToLower() != "siteclear"
                    && dailyMeasures.Category.ToLower() != "permittodig"
                    && !dailyMeasures.Category.ToLower().Contains("blockages")
                    && dailyMeasures.Category.ToLower() != "chamberaudit"
                    && dailyMeasures.Category.ToLower() != "presitepolesurvey"
                    && dailyMeasures.Category.ToLower() != "remedial"
                    && !dailyMeasures.Category.ToLower().Contains("measure")
                    && !dailyMeasures.Category.ToLower().Contains("asbuilt")))
                {
                    if ((dailyMeasures.JobData != null
                         && NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER)
                         || NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                        UpdateJobToPosted(dailyMeasures?.JobData);
                }

                return UpdateDailyMeasures(dailyMeasures);
            }
        }

        return false;
    }

    [Time]
    public void UpdateJobToSelected(JobData4Tablet jobData)
    {
        NavigationalParameters.JobTaskList = new List<JobData4Tablet>();

        var allJobs = App.Database.GetItems<JobData4Tablet>();

        foreach (var job in allJobs)
        {
            job.IsSelected = false;

            if (job.JobId == jobData.JobId && job.JobDate == jobData.JobDate &&
                job.GangLeaderId == jobData.GangLeaderId)
            {
                job.IsSelected = true;

                NavigationalParameters.CurrentSelectedJob = job;
            }

            App.Database.SaveItem(job);

            NavigationalParameters.JobTaskList.Add(job);
        }
    }

    private void UpdateJobToPosted(JobData4Tablet jobData)
    {
        jobData.SavedToServer = true;

        App.Database.SaveItem(jobData);
    }

    [Time]
    public bool UpdateDailyMeasures(DataPassed2Server dailyMeasures)
    {
        try
        {
            App.Database.UpdateSavedItems(dailyMeasures);
            return true;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            return false;
        }
    }

    [Time]
    public DataPassed2Server GetApprovalsToUpload(TaskItem selectedTaskItem)
    {
        var dataPassed2Server = new DataPassed2Server();

        var loggedinUser = NavigationalParameters.LoggedInUser;


        dataPassed2Server.Category = "SUPERVISORAPPROVALS";

        var diary = App.Database.GetItems<ClaimedNotesFile>()?.FirstOrDefault(x =>
            x.QuoteId ==
            selectedTaskItem.QuoteNumber.ToString()
            && x.NotesDate == selectedTaskItem.JobDate
            && x.NotesGang == selectedTaskItem.GangLeaderId.ToString()
            && x.PostedByGanger != DateTime.Parse("01/01/1900 00:00:00"));

        if (diary != null) dataPassed2Server.DailyDiary = diary;

        var measures = App.Database.GetItems<ClaimedFile>()
            .Where(x =>
                x.QuoteId == selectedTaskItem.QuoteNumber.ToString()
                && x.ClaimGang == selectedTaskItem.GangLeaderId.ToString()
                && x.ClaimDate == selectedTaskItem.JobDate).ToList();

        if (measures.Any()) dataPassed2Server.ClaimedFiles.AddRange(measures);

        var labourFiles = App.Database.GetItems<Labour>()?.Where(x =>
            x.QuoteId == selectedTaskItem.QuoteNumber.ToString()
            && x.LabourDate == selectedTaskItem.JobDate
            && x.LabourGang == selectedTaskItem.GangLeaderId.ToString()
            && x.ApprovedBySupervisor == NavigationalParameters.ApprovedDate).ToList();

        foreach (var labourFileToSave in labourFiles)
        {
            labourFileToSave.PostedByGangerName = labourFileToSave.PostedByGangerName;
            dataPassed2Server.LabourFiles.Add(labourFileToSave);
        }

        dataPassed2Server.ReinstatementMeasures = App.Database.GetItems<ReinstatementMeasure>()?.Where(x =>
            x.RemoteTableId == 0
            && x.QuoteNumber == selectedTaskItem.QuoteNumber
            && x.JobDate == selectedTaskItem.JobDate
            && x.GangLeaderId == selectedTaskItem.GangLeaderId).ToList();

        return dataPassed2Server;
    }

    [Time]
    public DataPassed2Server GetDailyUploadData(Tuple<Person, List<Assignment>, JobData4Tablet> navPassedData)
    {
        var dataPassed2Server = new DataPassed2Server();

        var loggedinUser = NavigationalParameters.LoggedInUser;
        var currentSelectedJob = NavigationalParameters.CurrentSelectedJob;

        dataPassed2Server.JobData = currentSelectedJob;

        dataPassed2Server.EndPoints =
            GetEndpoints(currentSelectedJob).Where(x => x.SavedToServer == false).ToList();

        dataPassed2Server.CableRuns =
            GetCableRuns(currentSelectedJob.QuoteNumber).Where(x => x.SavedToServer == false).ToList();

        dataPassed2Server.Locations =
            App.Database.GetItems<Location>()?.Where(x => x.RemoteTableId == 0).ToList();

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
        {
            dataPassed2Server.Category = "SUPERVISORAPPROVALS";

            var diary = App.Database.GetItems<ClaimedNotesFile>()?.FirstOrDefault(x =>
                x.ContractReference ==
                currentSelectedJob.ContractReference
                && x.NotesDate == currentSelectedJob.JobDate
                && x.NotesGang == currentSelectedJob.GangLeaderId.ToString()
                && x.PostedByGanger != DateTime.Parse("01/01/1900 00:00:00"));

            if (diary != null) dataPassed2Server.DailyDiary = diary;

            var measures = App.Database.GetItems<ClaimedFile>()
                .Where(x =>
                    x.ContractReference == currentSelectedJob.ContractReference
                    && x.ClaimGang == currentSelectedJob.GangLeaderId.ToString()
                    && x.ClaimDate == currentSelectedJob.JobDate).ToList();

            if (measures.Any()) dataPassed2Server.ClaimedFiles.AddRange(measures);

            dataPassed2Server.JobPictureData = App.Database.GetItems<Pictures4Tablet>()
                ?.Where(x => x.QNumber == currentSelectedJob.QuoteNumber).ToList();
            if (dataPassed2Server.JobPictureData.Any())
                foreach (var pic in dataPassed2Server.JobPictureData)
                    if (pic.SeverPictureId <= 0)
                    {
                        var pictureStatus =
                            _pictureService.SyncPicture2Azure(pic.PicturePath, "JobPictures", pic.FileName);
                    }

            foreach (var labourFileToSave in currentSelectedJob.JobGang.GangLabourFiles.Select(labourFile => App
                             .Database.GetItems<Labour>()?.FirstOrDefault(x =>
                                 x.ContractReference == currentSelectedJob.ContractReference
                                 && x.LabourDate == currentSelectedJob.JobDate
                                 && x.LabourOperative == labourFile.FocusId.ToString()
                                 && x.ApprovedBySupervisor == NavigationalParameters.ApprovedDate))
                         .Where(labourFileToSave => labourFileToSave != null))
            {
                labourFileToSave.PostedByGangerName = currentSelectedJob.GangLeaderName;
                dataPassed2Server.LabourFiles.Add(labourFileToSave);
            }

            var vls = App.Database.GetItems<VisitorLog>();

            visitorLogs = vls.Where(x => x.ContractReference == currentSelectedJob?.ContractReference
                                         && x.RemoteTableId == 0).ToList();

            dataPassed2Server.ReinstatementMeasures = App.Database.GetItems<ReinstatementMeasure>()?.Where(x =>
                x.RemoteTableId == 0
                && x.QuoteNumber == currentSelectedJob.QuoteNumber
                && x.JobDate == currentSelectedJob.JobDate
                && x.GangLeaderId == currentSelectedJob.GangLeaderId).ToList();
        }
        else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                 NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
        {
            dataPassed2Server.Category = "GANGDAILYMEASURES";

            var diary = App.Database.GetItems<ClaimedNotesFile>()?.FirstOrDefault(x =>
                x.RemoteTableId == 0
                && x.ContractReference ==
                currentSelectedJob.ContractReference
                && x.NotesDate == currentSelectedJob.JobDate
                && x.NotesGang == currentSelectedJob.GangLeaderId.ToString());

            if (diary != null) dataPassed2Server.DailyDiary = diary;

            var we = App.Database.GetItems<WeatherEvent>()?.Where(x =>
                x.RemoteTableId == 0
                && x.Qnumber ==
                currentSelectedJob.QuoteNumber
                && x.Date == currentSelectedJob.JobDate
                && x.GangLeader == currentSelectedJob.GangLeaderId
                && x.JobId == currentSelectedJob.JobId).ToList();

            if (we != null) dataPassed2Server.WeatherEvents = we;

            foreach (var blockage in dataPassed2Server.EndPoints.Select(item => App.Database.GetItems<Blockage>()
                         .FirstOrDefault(x => x.EndPointId == item.RemoteTableId)).Where(blockage => blockage != null))
                dataPassed2Server.BlockageList.Add(blockage);

            foreach (var item in App.Database.GetItems<Blockage>().Where(x =>
                             x.RemoteTableId == 0 && x.QNumber == dataPassed2Server.JobData.QuoteNumber.ToString())
                         .ToList())
                if (dataPassed2Server.BlockageList.All(x => x.ImageId != item.ImageId))
                    dataPassed2Server.BlockageList.Add(item);

            visitorLogs = App.Database.GetItems<VisitorLog>()
                .Where(x => x.ContractReference == currentSelectedJob.ContractReference
                            && x.RemoteTableId == 0
                            && x.OffSiteTime != DateTime.Parse("1/1/1900 00:00:00")
                            && x.GangLeader == currentSelectedJob.GangLeaderName).ToList();

            dataPassed2Server.FuelConsumption =
                App.Database.GetItems<FuelConsumption>().Where(x => x.RemoteTableId == 0).ToList();

            dataPassed2Server.StockFuel = App.Database.GetItems<StockFuelDTO>().Where(x => x.RemoteId == 0).ToList();

            var pictures = App.Database.GetItems<Pictures4Tablet>();
            dataPassed2Server.JobPictureData = pictures?.Where(x =>
                x.SeverPictureId == 0
                && x.ContractReference ==
                currentSelectedJob.ContractReference
                && x.OperativeId ==
                currentSelectedJob.GangLeaderId &&
                x.DateTimeTaken.Date >=
                currentSelectedJob.JobDate).ToList();

            if (dataPassed2Server.JobPictureData.Any())
                foreach (var pic in dataPassed2Server.JobPictureData)
                    if (pic.SeverPictureId <= 0)
                    {
                        var pictureStatus =
                            _pictureService.SyncPicture2Azure(pic.PicturePath, "JobPictures", pic.FileName);
                    }

            var crossedUtilities = App.Database.GetItems<ServicesCrossed4Tablet>()?.FirstOrDefault(x =>
                x.ContractReference ==
                currentSelectedJob.ContractReference
                && x.PostedDate.Date == currentSelectedJob.JobDate.Date
                && x.GangLeaderId == currentSelectedJob.GangLeaderId.ToString()
                && !string.IsNullOrEmpty(x.ServicesCrossedData1));

            if (crossedUtilities != null) dataPassed2Server.ServicesCrossed = crossedUtilities;

            var measures = App.Database.GetItems<ClaimedFile>()
                .Where(x => x.RemoteTableId == 0
                            && x.ContractReference == currentSelectedJob.ContractReference
                            && x.ClaimGang == currentSelectedJob.GangLeaderId.ToString()
                            && x.ClaimDate == currentSelectedJob.JobDate).ToList();

            dataPassed2Server.ClaimedFiles = new List<ClaimedFile>();
            dataPassed2Server.CableAuditMeasures = new List<CableStockUse>();

            var claimedPoles = new List<ClaimedPole>();

            foreach (var item in measures)
            {
                dataPassed2Server.ClaimedFiles.Add(item);

                dataPassed2Server.CableAuditMeasures.AddRange(App.Database.GetItems<CableStockUse>()
                    .Where(x => x.ClaimId == item.ClaimId && x.RemoteTableId == "0").ToList());

                claimedPoles.AddRange(App.Database.GetItems<ClaimedPole>().Where(x => x.ClaimId == item.ClaimId));
            }

            dataPassed2Server.PolesClaimed = claimedPoles;
            foreach (var labourFile in currentSelectedJob.JobGang.GangLabourFiles)
            {
                var labourFileToSave = App.Database.GetItems<Labour>()?.FirstOrDefault(x =>
                    x.ContractReference == currentSelectedJob.ContractReference
                    && x.LabourDate == currentSelectedJob.JobDate
                    && x.LabourOperative == labourFile.FocusId.ToString());

                if (labourFileToSave != null)
                {
                    labourFileToSave.PostedByGangerName = currentSelectedJob.GangLeaderName;

                    if (labourFileToSave.TravelFromSite < labourFileToSave.TravelToSite)
                        labourFileToSave.TravelFromSite = labourFileToSave.TravelFromSite.AddDays(1);
                    if (labourFileToSave.TimeLeftSite < labourFileToSave.TimeOnSite)
                        labourFileToSave.TimeLeftSite = labourFileToSave.TimeLeftSite.AddDays(1);
                    if (labourFileToSave.ClaimedYardEnd < labourFileToSave.ClaimedYardStart)
                        labourFileToSave.ClaimedYardEnd = labourFileToSave.ClaimedYardEnd.AddDays(1);

                    if (labourFileToSave.SupervisorFinish < labourFileToSave.SupervisorStart)
                        labourFileToSave.SupervisorFinish = labourFileToSave.SupervisorFinish.AddDays(1);
                    if (labourFileToSave.SupervisorOffSite < labourFileToSave.SupervisorOnSite)
                        labourFileToSave.SupervisorOffSite = labourFileToSave.SupervisorOffSite.AddDays(1);
                    if (labourFileToSave.SupervisorYardEnd < labourFileToSave.SupervisorYardStart)
                        labourFileToSave.SupervisorYardEnd = labourFileToSave.SupervisorYardEnd.AddDays(1);

                    dataPassed2Server.LabourFiles.Add(labourFileToSave);
                }

                var userPlant = App.Database.GetItems<Plant4Tablet>()?.Where(x => x.IssuedToId == labourFile.FocusId)
                    .ToList();

                if (userPlant.Any()) dataPassed2Server.OperativesPlant.AddRange(userPlant);
            }
        }

        if (visitorLogs.Any())
        {
            var list = new List<VisitorLog>();
            foreach (var visitor in visitorLogs.Where(visitor =>
                         list.All(x => x.VisitorLogGuid != visitor.VisitorLogGuid)))
                list.Add(visitor);

            dataPassed2Server.VisitorLogs.AddRange(list);
        }

        dataPassed2Server.ToolBoxTalkList =
            App.Database.GetItems<ToolBoxTalks>()?.Where(x => x.RemoteTableId == 0)?.ToList();

        dataPassed2Server.UserToolboxtalkList = App.Database.GetItems<UsersToolBoxTalks>()?.Where(x =>
            x.RemoteTableId == 0 && x.QuoteId == dataPassed2Server.JobData?.QuoteNumber &&
            x.GangLeaderId == dataPassed2Server.JobData?.GangLeaderId)?.ToList();

        var tempAssignments = _assignmentService.GenerateAssignment2Save(loggedinUser.FocusId).Where(x =>
            x.Qnumber == currentSelectedJob.QuoteNumber
            && x.CompletedOn == currentSelectedJob.JobDate).ToList();

        foreach (var item in tempAssignments)
            dataPassed2Server.Assignments.Add(_assignmentService.GenerateAssignments2SaveById(item));

        return dataPassed2Server;
    }

    [Time]
    public async Task UpdateInvestigationDetails(DamageToInvestigate damageToInvestigate)
    {
        foreach (var person in damageToInvestigate?.InjuredPersonnel)
        {
            person.RemoteTableId = 1;
            App.Database.SaveItem(person);
        }

        foreach (var thirdParty in damageToInvestigate?.ThirdPartyPersonnel)
        {
            thirdParty.RemoteTableId = 1;
            App.Database.SaveItem(thirdParty);
        }

        foreach (var witness in damageToInvestigate?.Witnesses)
        {
            witness.RemoteTableId = 1;
            App.Database.SaveItem(witness);
        }

        foreach (var answer in damageToInvestigate.DamageInvestigated.InvestigationAnswers)
        {
            answer.RemoteTableId = 1;
            answer.Ticked = true;
            App.Database.SaveItem(answer);
        }

        foreach (var gang in damageToInvestigate.GangResponisble)
        {
            gang.RemoteTableId = 1;
            App.Database.SaveItem(gang);
        }

        foreach (var pic in damageToInvestigate.PreviousImages)
        {
            pic.SeverPictureId = 1;
            App.Database.SaveItem(pic);
        }

        damageToInvestigate.DamageInvestigated.RemoteTabledId = 1;
        App.Database.SaveItem(damageToInvestigate.DamageInvestigated);
    }

    [Time]
    public List<Assignment> GetAudits(JobData4Tablet jobData)
    {
        try
        {
            var result = new List<Assignment>();

            var assignments = App.Database.GetItems<Assignment>()
                .Where(x => x.Category == "Audit" && x.Qnumber == jobData.QuoteNumber).ToList();

            //var assignments = assignmentsx.Where(x => x.Category == "Audit" && x.Qnumber == jobData.QuoteNumber)?
            //    .ToList();

            foreach (var assignment in assignments)
            {
                var audits = App.Database.GetItems<Audit>().ToList();

                if (audits.Any(x => x.AssignmentId == assignment?.AssignmentId))
                {
                    assignment.Audit = audits?.Where(y => y.AssignmentId == assignment?.AssignmentId)
                        ?.OrderByDescending(x => x.StartTime)?.FirstOrDefault();

                    if (assignment.Audit != null)
                    {
                        // foreach (var audit in assignment.Audits)
                        // {
                        assignment.Audit.Answers =
                            _assignmentService.GetRelevantResponses(assignment.Audit.AssignmentId);
                        result.Add(assignment);
                        //}
                    }
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
            return null;
        }
    }

    [Time]
    public List<Ncr> GetNcRs(Audit audit)
    {
        var ncrList = App.Database.GetItems<Ncr>();
        return ncrList.Where(x => x.AuditId == audit.AuditId).ToList();
    }

    [Time]
    public async Task UpdateSupervisorDailyDiaries(UserDailyTime supervisorDiariesToUpload)
    {
        foreach (var dpt in supervisorDiariesToUpload.UserDailyProjectTimesList)
        {
            dpt.ProjectTimeId = 1;
            App.Database.SaveItem(dpt);

            foreach (var dptn in dpt.DailyProjectNotesList)
            {
                dptn.ProjectTimeId = 1;
                App.Database.SaveItem(dptn);
            }
        }

        foreach (var dn in supervisorDiariesToUpload.UserDailyTimeNotesList)
        {
            dn.DailyTimeId = 1;
            App.Database.SaveItem(dn);
        }

        supervisorDiariesToUpload.DailyTimeId = 1;
    }

    [Time]
    public async Task UpdateJob(JobData4Tablet currenSelectedJob)
    {
        App.Database.SaveItem(currenSelectedJob);
    }

    [Time]
    public async Task<long> UpdateOrderBookAsync(Order orderBookItem)
    {
        var orderNumber = await _api.SaveInvoiceToServer(orderBookItem);
        return orderNumber;
    }

    [Time]
    public void DeleteOrder(Order order)
    {
        App.Database.DeleteItem(order);
    }

    #region UTILITY DAMAGES

    [Time]
    public async Task AddUtilityCompany(UtilityCompany company)
    {
        App.Database.SaveItem(company);
    }

    [Time]
    public async Task AddUtilityCompany(List<UtilityCompany> company)
    {
        App.Database.SaveItems(company);
    }

    [Time]
    public List<UtilityCompany> GetUtilityCompany()
    {
        return App.Database.GetItems<UtilityCompany>()?.Where(arg => !string.IsNullOrEmpty(arg.Type)).ToList();
    }

    [Time]
    public List<UtilityCompany> GetCompanies()
    {
        return App.Database.GetItems<UtilityCompany>()?.Where(arg => string.IsNullOrEmpty(arg.Type)).ToList();
    }

    [Time]
    public async Task AddUtilityDamageQuestion(PublicUtilityDamageQuestion damageQuestion)
    {
        App.Database.SaveItem(damageQuestion);
    }

    [Time]
    public async Task AddUtilityDamageQuestion(List<PublicUtilityDamageQuestion> damageQuestion)
    {
        //  var xx = App.Database.GetItems<PublicUtilityDamageQuestion>();
        foreach (var damage in damageQuestion)
        {
            var itemExsists = App.Database.GetItems<PublicUtilityDamageQuestion>()
                .Any(x => x.RemoteTableId == damage.RemoteTableId);
            if (!itemExsists) App.Database.SaveItems(damageQuestion);
        }

        var xxx = App.Database.GetItems<PublicUtilityDamageQuestion>();
    }

    [Time]
    public List<PublicUtilityDamageQuestion> GetUtilityDamageQuestions(string category)
    {
        var rl = new List<PublicUtilityDamageQuestion>();

        var dq = App.Database.GetItems<PublicUtilityDamageQuestion>()
            ?.Where(x => x.Category.ToLower().Contains(category.ToLower()))?.ToList();

        foreach (var q in dq)
            if (rl.All(x => x.RemoteTableId != q.RemoteTableId))
                rl.Add(q);

        return rl;
    }

    [Time]
    public async Task AddDamageToInvestigate(DamageToInvestigate damageToInvestigate)
    {
        App.Database.SaveItem(damageToInvestigate);
    }

    [Time]
    public List<DamageToInvestigate> GetDamageToInvestigate()
    {
        return App.Database.GetItems<DamageToInvestigate>().ToList();
    }

    [Time]
    public async Task AddInvestigateDamage(InvestigateDamages investigateDamage)
    {
        App.Database.SaveItem(investigateDamage);
    }

    [Time]
    public async Task AddInvestigateDamages(List<InvestigateDamages> investigateDamages)
    {
        try
        {
            foreach (var investigationOpen in investigateDamages)
            {
                var xx = App.Database.GetItems<InvestigateDamages>()
                    .Where(x => x.DamageId == investigationOpen.DamageId)
                    .ToList();

                if (!xx.Any()) App.Database.SaveItem(investigationOpen);

                foreach (var item in investigationOpen?.DamageDetails)
                {
                    if (string.IsNullOrEmpty(item.InvestigatorId))
                    {
                        item.InvestigatorId = NavigationalParameters.LoggedInUser?.FocusId.ToString();
                        item.InvestigatorName =
                            NavigationalParameters.LoggedInUser?.FullName ?? "Please Select To Begin";
                    }

                    if (investigationOpen.GangResponisble != null && investigationOpen.GangResponisble.Any())
                        foreach (var gr in (investigationOpen?.GangResponisble).Where(gr =>
                                     App.Database.GetItems<GangResponsible>().ToList()
                                         .TrueForAll(x => x.RemoteTableId != gr.RemoteTableId)))
                            App.Database.SaveItem(gr);

                    if (item.GangMembers != null && item.GangMembers.Any())
                        foreach (var gm in (item?.GangMembers).Where(gm =>
                                     App.Database.GetItems<Person>().ToList().TrueForAll(x => x.FocusId != gm.FocusId)))
                            App.Database.SaveItem(gm);

                    if (item.InjuredPersonnel != null && item.InjuredPersonnel.Any())
                        foreach (var ip in (item?.InjuredPersonnel).Where(ip =>
                                     App.Database.GetItems<InjuredPerson>().ToList()
                                         .TrueForAll(x => x.RemoteTableId != ip.RemoteTableId)))
                            App.Database.SaveItem(ip);

                    if (item.ThirdPartyPersonnel != null && item.ThirdPartyPersonnel.Any())
                        foreach (var ep in (item?.ThirdPartyPersonnel).Where(ep =>
                                     App.Database.GetItems<ExternalPersonnel>().ToList()
                                         .TrueForAll(x => x.RemoteTableId != ep.RemoteTableId)))
                            App.Database.SaveItem(ep);

                    if (item.Witnesses != null && item.Witnesses.Any())
                        foreach (var wt in item.Witnesses.Where(wt =>
                                     App.Database.GetItems<Witness>().ToList()
                                         .TrueForAll(x => x.RemoteTableId != wt.RemoteTableId)))
                            App.Database.SaveItem(wt);

                    if (item.DamageTypeAnswer != null)
                        if (App.Database.GetItems<DamageTypeAnswers>()
                            .ToList().TrueForAll(x => x.RemoteTableId != item.DamageTypeAnswer.RemoteTableId))
                            App.Database.SaveItem(item.DamageTypeAnswer);

                    if (item.DamageType != null)
                        if (App.Database.GetItems<PublicUtilityDamageQuestion>()
                            .All(x =>
                                x.RemoteTableId != item.DamageType.RemoteTableId))
                            App.Database.SaveItem(item.DamageType);

                    if (item.PrintTypesProvided != null)
                    {
                        var itemsToDelete = App.Database.GetItems<PrintTypesProvided>()
                            .Where(x => x.PublicUtilityDamageId == item.DamageId
                                        && x.InvestigationId.ToString() == item.InvestigationId).ToList();

                        if (itemsToDelete.Any())
                            foreach (var x in itemsToDelete)
                                App.Database.DeleteItem(x);

                        App.Database.SaveItem(item.PrintTypesProvided);
                    }

                    if (item.PreviousImages != null && item.PreviousImages.Any())
                        foreach (var pi in item.PreviousImages.Where(pi => App.Database.GetItems<Pictures4Tablet>()
                                     .ToList().TrueForAll(x => x.SeverPictureId != pi.SeverPictureId)))
                            App.Database.SaveItem(pi);

                    if (item.SignatureImages != null && item.SignatureImages.Any())
                        foreach (var si in item.SignatureImages.Where(si => App.Database.GetItems<Pictures4Tablet>()
                                     .ToList().TrueForAll(x => x.SeverPictureId != si.SeverPictureId)))
                            App.Database.SaveItem(si);

                    if (item.GangResponisble != null && item.GangResponisble.Any())
                        foreach (var gr in item.GangResponisble)
                        {
                            var exsists = App.Database.GetItems<GangResponsible>()
                                .Any(x => x.OperativeID == gr.OperativeID
                                          && x.InvestigationID == gr.InvestigationID
                                          && x.PublicUtilityDamagesId == gr.PublicUtilityDamagesId);

                            if (!exsists)
                            {
                                gr.InvestigationID = Convert.ToInt32(item.InvestigationId);
                                App.Database.SaveItem(gr);
                            }
                        }

                    if (item.DamageInvestigated != null)
                    {
                        App.Database.SaveInvestigation(item.DamageInvestigated);

                        if (item.DamageInvestigated.InvestigationAnswers != null &&
                            item.DamageInvestigated.InvestigationAnswers.Any())
                        {
                            var exsistingAnswers = App.Database.GetItems<InvestigationAnswer>().ToList();

                            foreach (var answer in item.DamageInvestigated.InvestigationAnswers.Where(answer =>
                                         exsistingAnswers.All(x => x.RemoteTableId != answer.RemoteTableId)))
                            {
                                exsistingAnswers.Add(answer);
                                App.Database.SaveItem(answer);
                            }
                        }

                        if (item.DamageInvestigated.InvestigationImage != null &&
                            item.DamageInvestigated.InvestigationImage.Any())
                            foreach (var li in item.DamageInvestigated.InvestigationImage.Where(li => App.Database
                                         .GetItems<Pictures4Tablet>()
                                         .ToList().TrueForAll(x => x.SeverPictureId != li.SeverPictureId)))
                                App.Database.SaveItem(li);
                    }

                    var zz = App.Database.GetItems<DamageToInvestigate>()?.Where(x => x.DamageId == item.DamageId)
                        .ToList();

                    if (zz.All(x => x.RemoteTableId != item.RemoteTableId || x.InvestigatorId != item.InvestigatorId))
                        App.Database.SaveItem(item);

                    investigationOpen.DamageType = App.Database.GetItems<PublicUtilityDamageQuestion>()
                        ?.FirstOrDefault(x => x.Id.ToString() == investigationOpen.DamageTypeId)?.DamageType;
                }
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
        }
    }

    [Time]
    public List<InvestigateDamages> GetInvestigateDamages()
    {
        return App.Database.GetItems<InvestigateDamages>().ToList();
    }

    [Time]
    public void AddInvestigationQuestion(InvestigationQuestion investigationQuestion)
    {
        App.Database.SaveItem(investigationQuestion);
    }

    [Time]
    public void AddInvestigationQuestions(List<InvestigationQuestion> investigationQuestions)
    {
        App.Database.SaveItems(investigationQuestions);
    }

    [Time]
    public List<InvestigationQuestion> GetInvestigationQuestions()
    {
        return App.Database.GetItems<InvestigationQuestion>().ToList();
    }

    [Time]
    public void AddInjuredPerson(InjuredPerson injuredPerson)
    {
        //   var items = GetInjuredPeople();
        App.Database.SaveItem(injuredPerson);
        var items2 = GetInjuredPeople();
    }

    [Time]
    public void DeleteInjuredPerson(InjuredPerson injuredPerson)
    {
        App.Database.DeleteItem(injuredPerson);
    }

    [Time]
    public List<InjuredPerson> GetInjuredPeople()
    {
        var injured = App.Database.GetItems<InjuredPerson>().ToList();

        return injured;
    }

    [Time]
    public async Task<bool> SaveRegisterUtilityDamage(RegisterUtilityDamage registerUtilityDamage)
    {
        if (!App.Database.GetItems<RegisterUtilityDamage>().Any(x =>
                x.ContractReference == registerUtilityDamage.ContractReference &&
                x.DamageTypeId == registerUtilityDamage.DamageTypeId &&
                x.IncidentDateTime == registerUtilityDamage.IncidentDateTime &&
                x.GangResponsible?.FirstOrDefault()?.GangLeaderID ==
                registerUtilityDamage.GangResponsible.FirstOrDefault()?.GangLeaderID))
        {
            App.Database.SaveItem(registerUtilityDamage);
            if (registerUtilityDamage?.GangResponsible?.Count > 0)
                App.Database.SaveItems(registerUtilityDamage.GangResponsible);
            if (registerUtilityDamage?.InjuredPersonnel?.Count > 0)
                App.Database.SaveItems(registerUtilityDamage.InjuredPersonnel);
            if (registerUtilityDamage?.Pictures?.Count > 0)
                App.Database.SaveItems(registerUtilityDamage.Pictures);
        }

        var items = App.Database.GetItems<RegisterUtilityDamage>();
        if (items != null) return await Task.FromResult(true);
        return await Task.FromResult(false);
    }

    [Time]
    public List<RegisterUtilityDamage> GetRegisterUtilityDamages()
    {
        var damageList = App.Database.GetItems<RegisterUtilityDamage>()?.Where(x =>
            x.FirstName == NavigationalParameters.LoggedInUser.FirstName &&
            x.Surname == NavigationalParameters.LoggedInUser.Surname &&
            x.PublicUtilityDamageGuid == NavigationalParameters.EventManagement.Identifier).ToList();

        foreach (var d in damageList)
        {
            d.GangResponsible = App.Database.GetItems<GangResponsible>()
                ?
                .Where(g => g.PublicUtilityDamageGuid == d.PublicUtilityDamageGuid).ToList();

            d.InjuredPersonnel = App.Database.GetItems<InjuredPerson>()?
                .Where(p => p.PublicUtilityDamageGuid == d.PublicUtilityDamageGuid).ToList();
            d.Pictures = App.Database.GetItems<Pictures4Tablet>()?
                .Where(pic => pic.Identifier == d.PublicUtilityDamageGuid).ToList();
        }

        return damageList;
    }

    [Time]
    public Task<bool> DeleteRegisterUtilityDamage(RegisterUtilityDamage damageToDelete)
    {
        damageToDelete.InjuredPersonnel.ForEach(p => App.Database.DeleteItem(p));
        damageToDelete.GangResponsible.ForEach(g => App.Database.DeleteItem(g));
        damageToDelete.Pictures.ForEach(pic => App.Database.DeleteItem(pic));
        App.Database.DeleteItem(damageToDelete);
        return Task.FromResult(true);
    }

    [Time]
    public async Task<bool> UploadUtilityInvestigations(DamageToInvestigate investigationsToSave, string mode)
    {
        return await _api.SaveInvestigations2Server(investigationsToSave, mode);
    }

    [Time]
    public async Task<bool> UploadToolBoxTalks(List<UsersToolBoxTalks> currentUserToolboxTalks)
    {
        var success = await _api.SaveToolboxTalks2Server(currentUserToolboxTalks);

        if (success)
        {
            NavigationalParameters.SelectedTaskItem.Complete = true;
            App.Database.SaveItem(NavigationalParameters.SelectedTaskItem);

            return true;
        }

        return false;
    }

    #endregion
}