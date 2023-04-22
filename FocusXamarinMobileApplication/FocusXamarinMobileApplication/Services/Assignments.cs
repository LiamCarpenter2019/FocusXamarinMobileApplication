#region

//using Syncfusion.XlsIO.Parser.Biff_Records;
using Audit = FocusXamarinMobileApplication.Models.Audit;
using Location = FocusXamarinMobileApplication.Models.Location;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.Services;

public class Assignments : IAssignments
{
    private const string Abb = "Abbreviations";
    private readonly WebApi _api;
    private readonly Pictures _pictureService;

    private readonly Users _userService;

    //  private MainViewModel _mvm;
    [Time]
    public Assignments()
    {
        _pictureService = new Pictures();
        _api = new WebApi();
        _userService = new Users();
    }

    public ObservableCollection<string> MultiAnswers { get; set; } = new();

    public SurveyAnswers answerToUpdate { get; set; }

    public string[] Options { get; set; }

    [Time]
    public void CreateDBifNotExists()
    {
        try
        {
            App.Database.CreateTable<Assignment>(); //create table

            App.Database.CreateTable<AuthorisationDetail>(); //create table

            App.Database.CreateTable<SurveyAnswers>(); //create table

            App.Database.CreateTable<SurveyQuestion>(); //create table

            App.Database.CreateTable<VMl4CabDetail>(); //create table

            App.Database.CreateTable<Rates>(); //create table

            App.Database.CreateTable<ProjectWorks>(); //create table

            App.Database.CreateTable<Dfe>(); //create table

            App.Database.CreateTable<SurveyComments>(); //create table

            App.Database.CreateTable<Measure>(); //create table

            App.Database.CreateTable<Abbreviations>(); //create table

            App.Database.CreateTable<Blockage>(); //create table

            App.Database.CreateTable<ServicesCrossed4Tablet>(); //create table

            //  Connection.CreateTable<Visibility>(); //create table

            App.Database.CreateTable<ProjectSummary>(); //create table

            App.Database.CreateTable<DigPermit>(); //create table

            App.Database.CreateTable<VisitorLog>(); //create table

            App.Database.CreateTable<Cvi>(); //create table

            App.Database.CreateTable<Audit>(); //create table

            App.Database.CreateTable<Ncr>(); //create table

            App.Database.CreateTable<Location>(); //create table

            App.Database.CreateTable<Pictures4Tablet>(); //create table

            App.Database.CreateTable<VMexpansionReleaseData>(); //create table
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = ex.ToString();
            //return false;
        }

        //  return returnValue;
    }

    [Time]
    public void ClearAllRows(string whichTable)
    {
        //return Task.Factory.StartNew(() =>
        // {
        switch (whichTable)
        {
            case "Assignment":
                App.Database.ClearSavedRemoteData<Assignment>();
                break;
            case "SurveyAnswers":
                App.Database.ClearSavedRemoteData<SurveyAnswers>();
                break;
            case "ABMeasures":
                App.Database.ClearSavedRemoteData<Measure>();
                break;
            case "SurveyQuestions":
                App.Database.ClearTable<SurveyQuestion>(); //create table
                break;
            case "CabinetDetails":
                App.Database.ClearTable<VMl4CabDetail>(); //create table
                break;
            case "Rates":
                App.Database.ClearTable<Rates>(); //create table
                break;
            case "Abbreviations":
                App.Database.ClearTable<Abbreviations>(); //create table
                break;
            case "ProjectWorks":
                App.Database.ClearSavedRemoteData<ProjectWorks>();
                break;
            case "Dfe":
                App.Database.ClearSavedRemoteData<Dfe>();
                break;
            case "SurveyComments":
                App.Database.ClearSavedRemoteData<SurveyComments>();
                break;
            case "Cvi":
                App.Database.ClearSavedRemoteData<Cvi>();
                break;
            case "Audits":
                App.Database.ClearSavedRemoteData<Audit>();
                break;
            case "NCR":
                App.Database.ClearSavedRemoteData<Ncr>();
                break;
            case "Location":
                App.Database.ClearSavedRemoteData<Location>();
                break;
            case "Pictures4Tablet":
                App.Database.ClearSavedRemoteData<Pictures4Tablet>();
                break;
            case "DigPermit":
                App.Database.ClearSavedRemoteData<DigPermit>();
                break;
            case "All":
                App.Database.ClearSavedRemoteData<Measure>();
                App.Database.ClearTable<Abbreviations>();
                App.Database.ClearTable<VMl4CabDetail>(); //create table;
                App.Database.ClearTable<SurveyQuestion>(); //create table      
                App.Database.ClearSavedRemoteData<SurveyAnswers>();
                App.Database.ClearSavedRemoteData<SurveyComments>();
                App.Database.ClearSavedRemoteData<Location>();
                App.Database.ClearSavedRemoteData<Audit>();
                App.Database.ClearSavedRemoteData<Cvi>();
                App.Database.ClearSavedRemoteData<Ncr>();
                App.Database.ClearSavedRemoteData<Dfe>();
                App.Database.ClearSavedRemoteData<DigPermit>();
                App.Database.ClearSavedRemoteData<Assignment>();
                App.Database.ClearSavedRemoteData<ProjectWorks>();

                break;
        }

        //  });
    }

    [Time]
    public async Task AddAssignments(DataPassed2XamarinTablets passedChecksData)
    {
        NavigationalParameters.AssignmentList = App.Database.GetItems<Assignment>().ToList();
        var answerList = App.Database.GetItems<SurveyAnswers>();

        foreach (var check in passedChecksData.Assignments)
            if (NavigationalParameters.AssignmentList.All(x => x.AssignmentId != check.AssignmentId))
            {
                App.Database.SaveItem(check);

                foreach (var answer in check?.SurveyAnswers)
                {
                    App.Database.SaveItem(answer);
                    App.Database.SaveItems(answer?.ResponseComments);
                }

                App.Database.SaveItems(check?.DfeList);

                App.Database.SaveItems(check?.PermitToDigList);

                App.Database.SaveItems(check?.SurveyComments);

                App.Database.SaveItems(check?.Measures);

                App.Database.SaveItems(check?.Audits);

                App.Database.SaveItems(check?.LocationList);

                App.Database.SaveItems(check?.SurveyPictures);
            }

        NavigationalParameters.AssignmentList = App.Database.GetItems<Assignment>().ToList();
    }

    [Time]
    public async Task AddAssignmentsResponses(List<SurveyAnswers> passedChecksData)
    {
        foreach (var answer in passedChecksData)
        {
            var exsists = App.Database.GetItems<SurveyAnswers>()?.FirstOrDefault(x =>
                x.Identifier == answer.Identifier && x.QuestionId == answer.QuestionId);

            if (exsists != null) exsists.AnswerGiven = answer.AnswerGiven;

            App.Database.SaveItems(passedChecksData);
        }
    }

    [Time]
    public List<Assignment> GetRelevantAssignments(string category)
    {
        NavigationalParameters.AssignmentList = App.Database.GetItems<Assignment>()?.ToList();

        switch (category.ToLower())
        {
            case "presitecivils":
                return NavigationalParameters.AssignmentList
                    .Where(x => x.Category.ToLower() == "presitecivils").ToList();
                break;
            case "asbuilt":
                return NavigationalParameters.AssignmentList.Where(x => x.Category.ToLower() == "asbuilt")
                    .ToList();
                break;

            case "chamberasbuilt":
                return NavigationalParameters.AssignmentList.Where(x => x.Category.ToLower() == "chamberasbuilt")
                    .ToList();
                break;
            case "dpasbuilt":
                return NavigationalParameters.AssignmentList.Where(x => x.Category.ToLower() == "dpbuilt")
                    .ToList();
            case "djeasbuilt":
                return NavigationalParameters.AssignmentList.Where(x => x.Category.ToLower() == "djeasbuilt")
                    .ToList();
                break;
            case "fjeasbuilt":
                return NavigationalParameters.AssignmentList.Where(x => x.Category.ToLower() == "fjeasbuilt")
                    .ToList();
                break;
            case "bjeasbuilt":
                return NavigationalParameters.AssignmentList.Where(x => x.Category.ToLower() == "bjeasbuilt")
                    .ToList();
                break;
            case "presitepremises":
                return NavigationalParameters.AssignmentList
                    .Where(x => x.Category.ToLower() == "presitepremises").ToList();
                break;
            default:
                return NavigationalParameters.AssignmentList;
        }
    }

    [Time]
    public async Task AddCabinetDetails(List<VMl4CabDetail> cabinetDetails)
    {
        var areaList = new List<VMl4CabDetail>();


        foreach (var area in cabinetDetails)
        {
            if (areaList.Any(x => x.L4Number == area.L4Number)) continue;

            areaList.Add(area);
        }

        App.Database.SaveItems(areaList);
    }

    [Time]
    public void GetCurrentAnswer(Button button)
    {
        NavigationalParameters.SelectedQuestion = button.CommandParameter as YesNoNaQuestionViewModel;


        if (NavigationalParameters.AnswerList?.FirstOrDefault(x =>
                x.QuestionId == NavigationalParameters.SelectedQuestion.QuestionId
                && x.Category.ToLower() == NavigationalParameters.SelectedQuestion.Category.ToLower()
                && x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId) == null)
        {
            answerToUpdate = new SurveyAnswers
            {
                RemoteTableId = 0,
                CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                Notifiable = NavigationalParameters.SelectedQuestion?.NotifiableResponse,
                AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                Category = NavigationalParameters.SelectedQuestion?.Category.ToUpper(),
                QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                StreetName = NavigationalParameters.CurrentAssignment.StreetName,
                QuestionId = NavigationalParameters.SelectedQuestion.QuestionId,
                SubmittedDateTime = DateTime.Now,
                Complete = ""
            };

            answerToUpdate.AnswerGiven = button.Text;

            answerToUpdate.AppStatus = "IPad";

            App.Database.SaveItem(answerToUpdate);

            NavigationalParameters.AnswerList.Add(answerToUpdate);
        }

        NavigationalParameters.AnswerList.FirstOrDefault(x =>
            x.QuestionId == NavigationalParameters.SelectedQuestion.QuestionId
            && x.Category.ToLower() == NavigationalParameters.SelectedQuestion.Category.ToLower()
            && x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId).AnswerGiven = button.Text;
    }

    [Time]
    public List<SurveyComments> GetRelevantAssignmentComments(Guid assignmentId)
    {
        return App.Database.GetItems<SurveyComments>()?.Where(x => x.AssignmentId == assignmentId)?.ToList();
    }

    [Time]
    public ObservableCollection<Assignment> GetPreSites(IGrouping<string, VMexpansionReleaseData> selectedStreet)
    {
        var preSites = new ObservableCollection<Assignment>();
        var tempListAssignments = App.Database.GetItems<Assignment>()?
            .Where(x => x.StreetName.ToLower() == selectedStreet.Key.ToLower() &&
                        x.Category.ToLower().Contains("presite"))
            .ToList();

        foreach (var item in tempListAssignments.Where(item => preSites.All(x => x.AssignmentId != item.AssignmentId)))
            preSites.Add(item);

        return preSites;
    }

    [Time]
    public Assignment CheckAssignmentStatus(JobData4Tablet currenSelectedJob, string key, string category)
    {
        Assignment current;
        try
        {
            current = App.Database.GetItems<Assignment>().ToList()?.FirstOrDefault(x => x.Complete == "false"
                && x.StreetName.ToLower() == key.ToLower()
                && x.Category.ToLower() == category.ToLower());

            if (current != null)
                return current;

            current = App.Database.GetItems<Assignment>().ToList()?.OrderByDescending(x => x.CompletedOn)
                ?.FirstOrDefault(x =>
                    x.Qnumber == currenSelectedJob.QuoteNumber
                    && x.StreetName.ToLower() == key.ToLower()
                    && x.ProjectName == currenSelectedJob.ProjectName
                    && x.Category.ToLower() == category.ToLower());
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            current = NavigationalParameters.DataPassedToTablet.Assignments.Where(x => x.Complete == "false"
                    && x.StreetName.ToLower() ==
                    key.ToLower()
                    && x.Category.ToLower() == category.ToLower())
                ?.FirstOrDefault();

            if (current != null)
                return current;

            current = NavigationalParameters.DataPassedToTablet.Assignments.OrderByDescending(x => x.CompletedOn)
                .FirstOrDefault(x =>
                    x.Qnumber == currenSelectedJob?.QuoteNumber
                    && x.StreetName.ToLower() == key.ToLower()
                    && x.ProjectName == currenSelectedJob?.ProjectName
                    && x.Category.ToUpper() == category.ToUpper());
        }

        return current;
    }

    [Time]
    public void GetCurrentMultiAnswer(Button button)
    {
        NavigationalParameters.SelectedQuestion = button.CommandParameter as MultiQuestionViewModel;

        var answerToSave = "";

        var keyAnswers = NavigationalParameters.SelectedQuestion.KeyAnswer.Split(',').ToList();

        MultiAnswers.Clear();

        if (NavigationalParameters.AnswerList?.FirstOrDefault(x =>
                x.QuestionId == NavigationalParameters.SelectedQuestion?.QuestionId
                && x.Category.ToLower() == NavigationalParameters.SelectedQuestion?.Category.ToLower()
                && x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId) == null)
            NavigationalParameters.AnswerList.Add(new SurveyAnswers
            {
                RemoteTableId = 0,
                CompletedById = NavigationalParameters.LoggedInUser.FocusId.ToString(),
                Notifiable = NavigationalParameters.SelectedQuestion.NotifiableResponse,
                AssignmentId = NavigationalParameters.CurrentAssignment.AssignmentId,
                Category = NavigationalParameters.SelectedQuestion.Category,
                QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                StreetName = NavigationalParameters.CurrentAssignment.StreetName,
                QuestionId = NavigationalParameters.SelectedQuestion.QuestionId,
                Complete = "",
                AppStatus = "IPad",
                AnswerGiven = ""
            });

        var answers = NavigationalParameters.AnswerList?.FirstOrDefault(x =>
            x.QuestionId == NavigationalParameters.SelectedQuestion?.QuestionId
            && x.Category.ToLower() == NavigationalParameters.SelectedQuestion?.Category.ToLower()
            && x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId).AnswerGiven.Split(',').ToList();

        if (answers.All(x => x != button.Text))
        {
            MultiAnswers.Add(button.Text);
        }
        else
        {
            MultiAnswers.Remove(button.Text);

            if (NavigationalParameters.SelectedQuestion.FurtherQuestionId > 0 &&
                !NavigationalParameters.SelectedQuestion.KeyAnswer.Contains(button.Text))
                App.Database.DeleteAllItems((decimal)NavigationalParameters.SelectedQuestion.FurtherQuestionId,
                    NavigationalParameters.CurrentAssignment.AssignmentId);
        }

        foreach (var item in MultiAnswers) answerToSave += $"{item.Trim()},";

        NavigationalParameters.AnswerList.FirstOrDefault(x =>
            x.QuestionId == NavigationalParameters.SelectedQuestion?.QuestionId
            && x.Category.ToLower() == NavigationalParameters.SelectedQuestion?.Category.ToLower()
            && x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId).AnswerGiven = answerToSave;

        App.Database.SaveItem(NavigationalParameters.AnswerList.FirstOrDefault(x =>
            x.QuestionId == NavigationalParameters.SelectedQuestion?.QuestionId
            && x.Category.ToLower() == NavigationalParameters.SelectedQuestion?.Category.ToLower()
            && x.AssignmentId == NavigationalParameters.CurrentAssignment.AssignmentId));
    }

    [Time]
    public async Task SaveCurrentAnswer(SurveyAnswers selectedAnswer)
    {
        var answerExsists = App.Database.GetItems<SurveyAnswers>()?
            .FirstOrDefault(x => x.AssignmentId == selectedAnswer.AssignmentId
                                 && x.QuestionId == selectedAnswer.QuestionId);

        if (answerExsists != null) selectedAnswer.RemoteTableId = answerExsists.Id;

        App.Database.SaveItem(selectedAnswer);
    }

    [Time]
    public List<Assignment> GetAssignmentsToUpload()
    {
        var result = App.Database.GetItems<Assignment>()?.Where(x =>
            x.Category.ToLower().Contains(NavigationalParameters.AppMode.ToString().ToLower())
            && x.AssignmentId != Guid.Empty
            && x.Qnumber == NavigationalParameters.CurrentSelectedJob?.QuoteNumber
            && x.Complete == "false"
          
        ).ToList();

        return result;
    }

    [Time]
    public List<Assignment> GetAsBuiltAssignmentsToUpload()
    {
        return App.Database.GetItems<Assignment>()?.Where(x =>
            x.Category.ToLower().Contains(NavigationalParameters.SurveyType.ToString().ToLower())
            && x.AssignmentId != Guid.Empty
            && x.Qnumber == NavigationalParameters.CurrentSelectedJob?.QuoteNumber
            && x.Complete == "false"
        ).ToList();
    }

    [Time]
    public Audit GetLatestAudit(Guid assignmentId)
    {
        var auditList = App.Database.GetItems<Audit>().Where(x => x.AssignmentId == assignmentId).ToList();

        return auditList?.OrderBy(x => x.AuditDate)?.FirstOrDefault();
    }

    [Time]
    public async Task AddACurrentAssignment(Assignment currentAssignment)
    {
        App.Database.SaveItem(currentAssignment);
    }

    [Time]
    public async Task AddAssignmentsResponse(SurveyAnswers passedChecksData)
    {
        var answer = App.Database.GetItems<SurveyAnswers>()?.FirstOrDefault(x =>
            x.AssignmentId == passedChecksData.AssignmentId && x.QuestionId == passedChecksData.QuestionId);

        App.Database.SaveItem(passedChecksData);
    }

    [Time]
    public Task AddProjectWorkWorks(List<ProjectWorks> projectWorkRates)
    {
        return Task.Factory.StartNew(async () =>
        {
            if (projectWorkRates != null && projectWorkRates.Count > 0) App.Database.SaveItems(projectWorkRates);
        });
    }

    [Time]
    public async Task AddRate(ProjectWorks rate)
    {
        App.Database.SaveItem(rate);
    }

    [Time]
    public async Task AddSurveyDfe(Dfe newDfe)
    {
        App.Database.SaveItem(newDfe);
    }

    [Time]
    public async Task AddSurveyComments(SurveyComments surveyComments)
    {
        App.Database.SaveItem(surveyComments);
    }

    [Time]
    public async Task AddAudits(Audit audits)
    {
        App.Database.SaveItem(audits);
    }

    [Time]
    public async Task Addcabinets(List<VMl4CabDetail> cabinetDetails)
    {
        App.Database.SaveItems(cabinetDetails);
    }

    [Time]
    public async Task AddMeasure(Measure newMeasure)
    {
        App.Database.SaveItem(newMeasure);
    }

    [Time]
    public async Task AddVisitors(List<VisitorLog> visitors)
    {
        App.Database.SaveItems(visitors);
    }

    [Time]
    public async Task AddSignatures(List<AuthorisationDetail> signatures)
    {
        App.Database.SaveItems(signatures);
    }

    [Time]
    public async Task AddQuestions(List<SurveyQuestion> questionList)
    {
        App.Database.SaveItems(questionList);

        //NavigationalParameters.AnswerList = new List<SurveyAnswers>();
        //NavigationalParameters.SurveyQuestions = new List<SurveyQuestion>();

        //foreach (var question in questionList)
        //{


        //    App.Database.SaveItem(question);

        //    var optionsTemp = question.QuestionOptions.Split(',')?.ToList();

        //    switch (optionsTemp.Count())
        //    {
        //        case 1:
        //            Options = new[] { optionsTemp[0], "", "", "", "", "", "", "", "", "", "", "" };
        //            break;
        //        case 2:
        //            Options = new[] { optionsTemp[0], optionsTemp[1], "", "", "", "", "", "", "", "", "", "" };
        //            break;
        //        case 3:
        //            Options = new[] { optionsTemp[0], optionsTemp[1], optionsTemp[2], "", "", "", "", "", "", "", "", "" };
        //            break;
        //        case 4:
        //            Options = new[] { optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], "", "", "", "", "", "", "", "" };
        //            break;
        //        case 5:
        //            Options = new[] {optionsTemp[0], optionsTemp[1], optionsTemp[2],
        //                    optionsTemp[3], optionsTemp[4], "", "", "","", "", "", ""};
        //            break;
        //        case 6:
        //            Options = new[] {optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3],
        //                optionsTemp[4],optionsTemp[5], "", "", "", "", "", ""};
        //            break;
        //        case 7:
        //            Options = new[]{
        //                optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], optionsTemp[4],
        //                optionsTemp[5], optionsTemp[6], "", "", "", "", ""};
        //            break;
        //        case 8:
        //            Options = new[]{
        //                optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], optionsTemp[4],
        //                optionsTemp[5], optionsTemp[6], optionsTemp[7], "", "", "", ""};
        //            break;
        //        case 9:
        //            Options = new[] {
        //                optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], optionsTemp[4],
        //                optionsTemp[5], optionsTemp[6], optionsTemp[7], optionsTemp[8], "", "", ""};
        //            break;
        //        case 10:
        //            Options = new[]{
        //                optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], optionsTemp[4],
        //                optionsTemp[5], optionsTemp[6], optionsTemp[7], optionsTemp[8], optionsTemp[9], "", ""};
        //            break;
        //        case 11:
        //            Options = new[]{
        //                optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], optionsTemp[4],
        //                optionsTemp[5], optionsTemp[6], optionsTemp[7], optionsTemp[8], optionsTemp[9],
        //                optionsTemp[10], ""};
        //            break;
        //        default:
        //            Options = new[]{
        //                optionsTemp[0], optionsTemp[1], optionsTemp[2], optionsTemp[3], optionsTemp[4],
        //                optionsTemp[5], optionsTemp[6], optionsTemp[7], optionsTemp[8], optionsTemp[9],
        //                optionsTemp[10], optionsTemp[11]};
        //            break;
        //    }

        //    //var answerExsists = NavigationalParameters.AnswerList
        //    //    ?.Where(x => x.QuestionId == question?.QuestionId)?.ToList();

        //    switch (question.ResponseType)
        //    {
        //        case "Y/N Selection":
        //            var answer = new YesNoNaQuestionViewModel
        //            {
        //                Question = question.Question,
        //                QuestionId = question.QuestionId,
        //                KeyAnswer = question.KeyAnswer,
        //                Category = question.Category,
        //                DepthorRating = question.DepthorRating,
        //                FollowUpAction = question.FollowUpAction,
        //                FurtherQuestionId = question.FurtherQuestionId,
        //                Grouping = question.Grouping,
        //                InformationTo = question.InformationTo,
        //                NotifiableResponse = question.NotifiableResponse,
        //                QuestionOptions = question.QuestionOptions,
        //                ResponseType = question.ResponseType,
        //                Id = question.Id,
        //                Stage = question.Stage,
        //                BtnYesText = Options[0],
        //                BtnNoText = Options[1],
        //                BtnNaText = Options[2],
        //                BtnNaBgColour = Color.LightGray,
        //                IsEnabled = true
        //            };

        //            //if (answerExsists?.FirstOrDefault()?.AnswerGiven.ToLower() == Options[0].ToLower())
        //            //{
        //            //    switch (NavigationalParameters.AppMode)
        //            //    {
        //            //        case NavigationalParameters.AppModes.SITECLEAR:
        //            //            answer.BtnYesBgColour = question.KeyAnswer.ToLower().Contains(answerExsists?.FirstOrDefault()?.AnswerGiven.ToLower()) ? Color.Red : Color.Green;
        //            //            break;
        //            //        case NavigationalParameters.AppModes.PERMITTODIG:
        //            //            answer.BtnYesBgColour = question.KeyAnswer.ToLower().Contains(answerExsists?.FirstOrDefault()?.AnswerGiven.ToLower()) ? Color.Green : Color.Red;
        //            //            break;
        //            //        default:
        //            //            answer.BtnYesBgColour = Color.Green;
        //            //            break;
        //            //    }
        //            //}
        //            //else
        //            //{
        //                answer.BtnYesBgColour = Color.LightGray;
        //           // }

        //                //if (answerExsists?.FirstOrDefault()?.AnswerGiven.ToLower() == Options[1].ToLower())
        //                //{
        //                //    switch (NavigationalParameters.AppMode)
        //                //    {
        //                //        case NavigationalParameters.AppModes.SITECLEAR:
        //                //            answer.BtnNoBgColour = question.KeyAnswer.ToLower().Contains(answerExsists?.FirstOrDefault()?.AnswerGiven.ToLower()) ? Color.Red : Color.Green;
        //                //            break;
        //                //        case NavigationalParameters.AppModes.PERMITTODIG:
        //                //            answer.BtnNoBgColour = question.KeyAnswer.ToLower().Contains(answerExsists?.FirstOrDefault()?.AnswerGiven.ToLower()) ? Color.Green : Color.Red;
        //                //            break;
        //                //        default:
        //                //            answer.BtnNoBgColour = Color.Green;
        //                //            break;
        //                //    }
        //                //}
        //                //else
        //                //{
        //                answer.BtnNoBgColour = Color.LightGray;
        //          //  }

        //            NavigationalParameters.SurveyQuestions.Add(answer);
        //            break;
        //        case "multiSelector":
        //            var answer2 = new MultiQuestionViewModel
        //            {
        //                Question = question.Question,
        //                QuestionId = question.QuestionId,
        //                KeyAnswer = question.KeyAnswer,
        //                Category = question.Category,
        //                DepthorRating = question.DepthorRating,
        //                FollowUpAction = question.FollowUpAction,
        //                FurtherQuestionId = question.FurtherQuestionId,
        //                Grouping = question.Grouping,
        //                InformationTo = question.InformationTo,
        //                NotifiableResponse = question.NotifiableResponse,
        //                QuestionOptions = question.QuestionOptions,
        //                ResponseType = question.ResponseType,
        //                Id = question.Id,
        //                Stage = question.Stage,
        //                BtnAText = Options[0],
        //                BtnBText = Options[1],
        //                BtnCText = Options[2],
        //                BtnDText = Options[3],
        //                BtnEText = Options[4],
        //                BtnFText = Options[5],
        //                BtnGText = Options[6],
        //                BtnHText = Options[7],
        //                BtnIText = Options[8],
        //                BtnJText = Options[9],
        //                BtnKText = Options[10],
        //                BtnLText = Options[11],

        //                BtnAHidden = Options[0] != "",
        //                BtnBHidden = Options[1] != "",
        //                BtnCHidden = Options[2] != "",
        //                BtnDHidden = Options[3] != "",
        //                BtnEHidden = Options[4] != "",
        //                BtnFHidden = Options[5] != "",
        //                BtnGHidden = Options[6] != "",
        //                BtnHHidden = Options[7] != "",
        //                BtnIHidden = Options[8] != "",
        //                BtnJHidden = Options[9] != "",
        //                BtnKHidden = Options[10] != "",
        //                BtnLHidden = Options[11] != "",
        //                IsEnabled = true
        //            };

        //            answer2.BtnABgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //            //    ? NavigationalParameters.AnswerList.Any(x =>
        //            //        x.QuestionId == question.QuestionId &&
        //            //        x.AnswerGiven.ToLower().Contains(Options[0].ToLower()))
        //            //        ? Color.Green
        //            //        : Color.LightGray
        //                Color.LightGray;


        //            answer2.BtnBBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[1].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                 Color.LightGray;

        //            answer2.BtnCBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[2].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnDBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[3].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnEBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[4].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnFBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[5].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnGBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[6].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnHBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[7].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnIBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[8].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnJBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[9].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnKBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[10].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            answer2.BtnLBgColour =
        //                //answerExsists?.FirstOrDefault()?.AnswerGiven != null
        //                //? NavigationalParameters.AnswerList.Any(x =>
        //                //    x.QuestionId == question.QuestionId &&
        //                //    x.AnswerGiven.ToLower().Contains(Options[11].ToLower()))
        //                //    ? Color.Green
        //                //    : Color.LightGray
        //                //:
        //                Color.LightGray;

        //            NavigationalParameters.SurveyQuestions.Add(answer2);
        //            break;
        //        case "mark location on map, select house, comments, photos":
        //            NavigationalParameters.SurveyQuestions.Add(new LocationIdentityQuestionViewModel
        //            {
        //                Question = question.Question,
        //                QuestionId = question.QuestionId,
        //                KeyAnswer = question.KeyAnswer,
        //                Category = question.Category,
        //                DepthorRating = question.DepthorRating,
        //                FollowUpAction = question.FollowUpAction,
        //                FurtherQuestionId = question.FurtherQuestionId,
        //                Grouping = question.Grouping,
        //                InformationTo = question.InformationTo,
        //                NotifiableResponse = question.NotifiableResponse,
        //                QuestionOptions = question.QuestionOptions,
        //                ResponseType = question.ResponseType,
        //                Id = question.Id,
        //                Stage = question.Stage,
        //                IsEnabled = true
        //            });
        //            break;
        //        case "Rating":

        //            var rq = new RatingQuestionViewModel
        //            {
        //                Question = question.Question,
        //                QuestionId = question.QuestionId,
        //                KeyAnswer = question.KeyAnswer,
        //                Category = question.Category,
        //                DepthorRating = question.DepthorRating,
        //                FollowUpAction = question.FollowUpAction,
        //                FurtherQuestionId = question.FurtherQuestionId,
        //                Grouping = question.Grouping,
        //                InformationTo = question.InformationTo,
        //                NotifiableResponse = question.NotifiableResponse,
        //                QuestionOptions = question.QuestionOptions,
        //                ResponseType = question.ResponseType,
        //                Id = question.Id,
        //                Stage = question.Stage,
        //                IsEnabled = NavigationalParameters.AnswerList?.FirstOrDefault(x =>
        //                       x.QuestionId == question.QuestionId) != null,
        //                Rating10 = SimpleStaticHelpers.GetImage("10grey"),
        //                Rating9 = SimpleStaticHelpers.GetImage("9grey"),
        //                Rating8 = SimpleStaticHelpers.GetImage("8grey"),
        //                Rating7 = SimpleStaticHelpers.GetImage("7grey"),
        //                Rating6 = SimpleStaticHelpers.GetImage("6orbelowgrey"),
        //                Rating5 = SimpleStaticHelpers.GetImage("5grey"),
        //                Rating4 = SimpleStaticHelpers.GetImage("4grey"),
        //                Rating3 = SimpleStaticHelpers.GetImage("3grey"),
        //                Rating2 = SimpleStaticHelpers.GetImage("2grey"),
        //                Rating1 = SimpleStaticHelpers.GetImage("1grey"),
        //                RatingNA = SimpleStaticHelpers.GetImage("NAgrey"),
        //            };

        //            var ca = NavigationalParameters.AnswerList?.FirstOrDefault(x =>
        //                       x.QuestionId == question.QuestionId && x.Category.ToLower() == "audit");

        //            if (ca != null)
        //            {
        //                if (ca.AnswerGiven.ToLower() == "1")
        //                {
        //                    rq.Rating1 = SimpleStaticHelpers.GetImage("1red");
        //                    rq.Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "2")
        //                {
        //                    rq.Rating2 = SimpleStaticHelpers.GetImage("2red");
        //                    rq.Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "3")
        //                {
        //                    rq.Rating3 = SimpleStaticHelpers.GetImage("3orange");
        //                    rq.Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "4")
        //                {
        //                    rq.Rating4 = SimpleStaticHelpers.GetImage("4orange");
        //                    rq.Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "5")
        //                {
        //                    rq.Rating5 = SimpleStaticHelpers.GetImage("5yellow");
        //                    rq.Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "6")
        //                {
        //                    rq.Rating6 = SimpleStaticHelpers.GetImage("6orbelowyellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "7")
        //                {
        //                    rq.Rating7 = SimpleStaticHelpers.GetImage("7yellow");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "8")
        //                {
        //                    rq.Rating8 = SimpleStaticHelpers.GetImage("8green");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "9")
        //                {
        //                    rq.Rating9 = SimpleStaticHelpers.GetImage("9green");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "10")
        //                {
        //                    rq.Rating10 = SimpleStaticHelpers.GetImage("10green");
        //                }
        //                else if (ca.AnswerGiven.ToLower() == "n/a")
        //                {
        //                    rq.RatingNA = SimpleStaticHelpers.GetImage("NAgreen");
        //                }
        //            }
        //            NavigationalParameters.SurveyQuestions.Add(rq);
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    [Time]
    public async Task SaveCvi(Assignment assignment)
    {
        foreach (var cvi in assignment.Cvi) App.Database.SaveItem(cvi);

        App.Database.SaveItem(assignment);
    }

    [Time]
    public List<Cvi> GetCvi(Guid id)
    {
        var cvis = App.Database.GetItems<Cvi>()?.Where(x => x.CviId == id)?.ToList();
        return cvis;
    }

    [Time]
    public Task AddAbbreviations(List<Abbreviations> abbreviations)
    {
        return Task.Factory.StartNew(async () => { App.Database.SaveItems(abbreviations); });
    }

    [Time]
    public bool UpdatePin(Person person)
    {
        return _api.UpdatePin(person).Result;
    }

    [Time]
    public Assignment GetCurrentAssignment(long qNumber, string name, string category)
    {
     
        var check = App.Database.GetItems<Assignment>()?.OrderByDescending(x => x.CompletedOn)?.FirstOrDefault(x =>
            x.Qnumber == qNumber
            && x.StreetName == name
            && x.Category.ToLower() == category.ToLower()
            && x.RemoteTableId == 0
            && x.AssignmentId != Guid.Empty);

        if (check == null) return null;

        check.ProjectWorks = App.Database.GetItems<ProjectWorks>()
            ?.Where(x => x.QuoteId == qNumber && x.Status == "Quote").ToList();

        if (check.AssignmentId != Guid.Empty)
        {
            check.SurveyAnswers = App.Database.GetItems<SurveyAnswers>()?.Where(x =>
                x.AssignmentId == check.AssignmentId).ToList();

            check.SurveyComments = App.Database.GetItems<SurveyComments>()?.Where(x =>
                x.AssignmentId == check.AssignmentId).ToList();

            check.Measures = App.Database.GetItems<Measure>()?.Where(x => x.AssignmentId == check.AssignmentId)
                .ToList();

            check.Audit = App.Database.GetItems<Audit>()?.FirstOrDefault(x => x.AssignmentId == check.AssignmentId);
        }

        return check;
    }

    [Time]
    public List<VMexpansionReleaseData> GetStreets(Assignment currentAsssignment)
    {
        var endPoints = new List<VMexpansionReleaseData>();

        if (currentAsssignment != null)
            endPoints.AddRange(App.Database.GetItems<VMexpansionReleaseData>()?
                .Where(x => x.Qnumber == currentAsssignment.Qnumber).ToList());

        return endPoints;
    }

    [Time]
    public List<Assignment> GetRelevantAssignment(string category, long qNumber)
    {
        List<Assignment> assignments;
        try
        {
            assignments = App.Database.GetItems<Assignment>()
                ?.Where(x => x.Category.ToLower() == category.ToLower() && x.Qnumber == qNumber)?.ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            assignments = (List<Assignment>)NavigationalParameters.DataPassedToTablet.Assignments
                .Where(x => x.Category.ToLower() == category.ToLower() && x.Qnumber == qNumber);
        }


        foreach (var check in assignments)
        {
            if (check == null) return null;

            if (check.AssignmentId != Guid.Empty)
            {
                check.ProjectWorks = App.Database.GetItems<ProjectWorks>()?
                    .Where(x => x.AssignmentId == check.AssignmentId).ToList();

                try
                {
                    check.SurveyAnswers = App.Database.GetItems<SurveyAnswers>()?.Where(x =>
                        x.AssignmentId == check.AssignmentId).ToList();
                }
                catch (Exception ex)
                {
                    check.SurveyAnswers = NavigationalParameters.DataPassedToTablet.Assignments?.FirstOrDefault(x =>
                        x.AssignmentId == check.AssignmentId)?.SurveyAnswers;
                }

                try
                {
                    check.SurveyComments = App.Database.GetItems<SurveyComments>()?.Where(x =>
                        x.AssignmentId == check.AssignmentId)?.ToList();
                }
                catch (Exception ex)
                {
                    check.SurveyComments = App.Database.GetItems<SurveyComments>()?.Where(x =>
                        x.AssignmentId == check.AssignmentId)?.ToList();
                }

                check.Measures = App.Database.GetItems<Measure>()?.Where(x => x.AssignmentId == check.AssignmentId)
                    ?.ToList();

                check.PermitToDigList = App.Database.GetItems<DigPermit>()
                    ?.Where(x => x.AssignmentId == check.AssignmentId)?.ToList();

                check.Audit = App.Database.GetItems<Audit>()?.FirstOrDefault(x => x.AssignmentId == check.AssignmentId);

                check.Cvi = App.Database.GetItems<Cvi>()?.Where(x => x.CviId == check.AssignmentId)?.ToList();

                //foreach (var audit in check.Audits)
                check.Audit.NcrList =
                    App.Database.GetItems<Ncr>()?.Where(x => x.AuditId == check.Audit.AuditId)?.ToList();
            }
        }

        return assignments;
    }

    [Time]
    public Assignment GetRelevantNonConformanceAssignment(Guid assignmentId)
    {
        return App.Database.GetItems<Assignment>()?.FirstOrDefault(x => x.AssignmentId == assignmentId);
    }

    [Time]
    public List<Ncr> GetNcrList(Guid auditId)
    {
        return App.Database.GetItems<Ncr>()?.Where(x => x.AuditId == auditId).ToList();
    }

    [Time]
    public List<Measure> GetMeasures(Guid assignmentId)
    {
        return App.Database.GetItems<Measure>()?.Where(x => x.AssignmentId == assignmentId).ToList();
    }

    [Time]
    public Blockage GetBlockage(long remoteTableId)
    {
        return App.Database.GetItems<Blockage>()?
            .FirstOrDefault(x => x.RemoteTableId == remoteTableId && x.Cleared == false);
    }

    [Time]
    public async Task<Audit> GetAudit(Guid auditId)
    {
        // ReSharper disable once PossibleNullReferenceException
        var auditToReturn = App.Database.GetItems<Audit>()?.FirstOrDefault(x => x.AuditId == auditId) ??
                            NavigationalParameters.CurrentAudit;

        if (auditToReturn != null)
        {
            auditToReturn.NcrList =
                App.Database.GetItems<Ncr>()?.Where(x => x.AuditId == auditToReturn.AuditId)?.ToList();

            auditToReturn.Answers = App.Database.GetItems<SurveyAnswers>()?
                .Where(x => x.Identifier == auditToReturn.AuditId)?.ToList();

            auditToReturn.NonConformancies =
                auditToReturn.NcrList.Where(x => x.CorrectedOnSite == false).ToList().Count;

            auditToReturn.Inadequacies = auditToReturn.NcrList.Where(x => x.CorrectedOnSite == true).ToList().Count;
        }

        return auditToReturn;
    }

    [Time]
    public void RemoveImages(Dfe currentDfe)
    {
        var pics = App.Database.GetItems<Pictures4Tablet>()?.Where(x => x.Identifier == currentDfe.DfeId).ToList();

        foreach (var item in pics.ToList())
            if (item.Identifier == currentDfe.DfeId)
            {
                App.Database.DeleteItem(item);
            }
            else
            {
                var dfeList = App.Database.GetItems<Dfe>()
                    .ToList().TrueForAll(x => x.DfeId != item.Identifier);

                if (dfeList) App.Database.DeleteItem(item);
            }
    }

    [Time]
    public void RemoveDfe(Dfe currentDfe)
    {
        var dfes = App.Database.GetItems<Dfe>()?.Where(x => x.DfeId == currentDfe.DfeId).ToList();

        foreach (var item in dfes.ToList())
            if (item.DfeId == currentDfe.DfeId)
            {
                App.Database.DeleteItem(item);
            }
            else
            {
                var dfeList = (bool)App.Database.GetItems<Dfe>().ToList()?.TrueForAll(x => x.DfeId != item.DfeId);

                if (dfeList) App.Database.DeleteItem(item);
            }
    }

    [Time]
    public List<Abbreviations> GetAbbreviations()
    {
        try
        {
            if (App.Database.GetItems<Abbreviations>().Any())
            {
                var abbreviations = App.Database.GetItems<Abbreviations>()?.ToList();

                if (abbreviations == null || abbreviations?.Count() <= 0)
                    return NavigationalParameters.DataPassedToTablet?.Abbreviations;

                return abbreviations;
            }

            return new List<Abbreviations>();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return new List<Abbreviations>();
        }
    }

    [Time]
    public List<VMl4CabDetail> GetCabData(long quoteId)
    {
        return App.Database.GetItems<VMl4CabDetail>().Where(x => x.QuoteId == quoteId).ToList();
    }

    [Time]
    public List<ProjectWorks> GetRates(long qNumber, string status)
    {
        try
        {
            return App.Database.GetItems<ProjectWorks>()?.Where(x =>
                x.QuoteId == qNumber
                && x.Status == status).ToList();
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return
                NavigationalParameters.DataPassedToTablet.ProjectWorkRates?.Where(x => x.QuoteId == qNumber
                    && x.Status == status).ToList();
        }
    }

    [Time]
    public List<ProjectWorks> GetT6ProjectWorks(long qNumber, Guid assignmentId)
    {
        var result = App.Database.GetItems<ProjectWorks>()
            ?.Where(x => x.QuoteId == qNumber && x.AssignmentId == Guid.Empty)?.ToList();


        return result;
    }

    [Time]
    public List<ProjectWorks> GetProjectWorks(long qNumber, string status)
    {
        var result = App.Database.GetItems<ProjectWorks>()?.Where(x => x.QuoteId == qNumber
                                                                       && x.Status == status
                                                                       && x.AssignmentId == Guid.Empty)?.ToList();
        return result;
    }

    [Time]
    public List<ProjectWorks> GetNewDfeProjectWorks(long qNumber, Guid assignmentId)
    {
        var result = App.Database.GetItems<ProjectWorks>()?.Where(x => x.QuoteId == qNumber
                                                                       && x.Category.ToLower() == "dfe")?.ToList();
        return result;
    }

    [Time]
    public List<ProjectWorks> GetRatesCvi(long qNumber, string status)
    {
        var result = App.Database.GetItems<ProjectWorks>()
            ?.Where(x => x.QuoteId == qNumber && x.Category.ToLower() == "cvi")?.ToList();

        return result;
    }

    [Time]
    public List<SurveyQuestion> GetSurveyQuestions(string category)
    {
        try 
        {
            var questions = new List<SurveyQuestion>();

            if (NavigationalParameters.SurveyType.ToString().ToUpper().Contains("JEASBUILT")
                || NavigationalParameters.SurveyType.ToString().ToUpper().Contains("JEMEASURE"))
            {
                if (NavigationalParameters.SelectedAsset.StreetName.ToLower().Contains("dje"))
                {
                    questions = App.Database.GetItems<SurveyQuestion>()?
                   .Where(x => x.Category.ToUpper() == "DJEASBUILT")?.ToList();
                }
                else if (NavigationalParameters.SelectedAsset.StreetName.ToLower().Contains("fje"))
                {
                    questions = App.Database.GetItems<SurveyQuestion>()?
                    .Where(x => x.Category.ToUpper() == "FJEASBUILT")?.ToList();
                }
                else if (NavigationalParameters.SelectedAsset.StreetName.ToLower().Contains("bje"))
                {
                    questions = App.Database.GetItems<SurveyQuestion>()?
                    .Where(x => x.Category.ToUpper() == "BJEASBUILT")?.ToList();
                }
            }

            questions = App.Database.GetItems<SurveyQuestion>()?
            .Where(x => x.Category.ToLower() == category.ToLower())?.ToList();

            return questions;
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return null;
        }
    }

    [Time]
    public List<SurveyAnswers> GetRelevantAssignmentsResponses(Guid assignmentId)
    {
        return App.Database.GetItems<SurveyAnswers>()
            ?.Where(x => x.AssignmentId == assignmentId && !string.IsNullOrEmpty(x.AnswerGiven)
                                                        && x.Complete != "Superseeded")?.ToList();
    }

    [Time]
    public List<SurveyAnswers> GetRelevantAssignmentsResponsesByStreetName(string streetName)
    {
        return App.Database.GetItems<SurveyAnswers>()
            ?.Where(x => x.StreetName == streetName && !string.IsNullOrEmpty(x.AnswerGiven))?.ToList();
    }


    [Time]
    public List<SurveyAnswers> GetRelevantResponses(Guid assignmentId)
    {
        return App.Database.GetItems<SurveyAnswers>()?.Where(x => x.AssignmentId == assignmentId)?.ToList();
    }

    [Time]
    public List<SurveyComments> GetRelevantQuestionComments(Guid identifier)
    {
        var allComments = App.Database.GetItems<SurveyComments>();

        return allComments.Where(x => x.Identifier == identifier)?.ToList();
        ;
    }

    [Time]
    private List<Dfe> GetDfes(Guid assignmentId)
    {
        return App.Database.GetItems<Dfe>()?.Where(x => x.AssignmentId == assignmentId)?.ToList();
    }

    [Time]
    public ProjectSummary GetProjectSummary(long quoteNumber)
    {
        try
        {
            return App.Database.GetItems<ProjectSummary>().FirstOrDefault(x => x.QNumber == quoteNumber);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            return NavigationalParameters.DataPassedToTablet.AllProjectSummaries.FirstOrDefault(x =>
                x.QNumber == quoteNumber);
        }
    }

    //TODO
    [Time]
    public List<ClaimedFile> GetClaimedFiles()
    {
        //return App.Database.GetItems<ClaimedFile>().Where(x => x.RemoteTableId.ToString() == "0").ToList();
        return App.Database.GetItems<ClaimedFile>()?.ToList();
    }

    [Time]
    public async Task RemoveRates(Dfe newDfe)
    {
        foreach (var item in App.Database.GetItems<ProjectWorks>()?.Where(x => x.Identifier == newDfe.DfeId).ToList())
            App.Database.DeleteItem(item);
    }

    [Time]
    public async Task RemoveRates(Cvi cvi)
    {
        foreach (var item in App.Database.GetItems<ProjectWorks>()?.Where(x => x.Identifier == cvi.CviId).ToList())
            App.Database.DeleteItem(item);
    }


    [Time]
    public async Task RemoveCviRates(Guid id)
    {
        foreach (var item in App.Database.GetItems<ProjectWorks>()?.Where(x => x.Identifier == id).ToList())
            App.Database.DeleteItem(item);
    }

    [Time]
    public async Task RemoveSurveyAnswers(List<SurveyAnswers> surveyAnswers)
    {
        foreach (var item in surveyAnswers) App.Database.DeleteItem(item);
    }

    [Time]
    public void RemoveSurveyComments(SurveyAnswers response)
    {
        var commentListToDelete = new List<SurveyComments>();

        var questionToRemove = App.Database.GetItems<SurveyQuestion>()?
            .FirstOrDefault(x => x.QuestionId == response.QuestionId);

        var commentToDelete = App.Database.GetItems<SurveyComments>()?.Where(x =>
            x.Identifier == response.Identifier).ToList();
        commentListToDelete.AddRange(commentToDelete);

        if (!(questionToRemove.FurtherQuestionId > 0)) return;
        {
            var commentsToDeleteList = App.Database.GetItems<SurveyComments>()?.Where(x =>
                x.Identifier == response.Identifier).ToList();

            foreach (var comment in commentsToDeleteList)
            {
                commentListToDelete.Add(comment);

                var questionToRemove2 = App.Database.GetItems<SurveyQuestion>()?
                    .FirstOrDefault(x => x.QuestionId == questionToRemove.FurtherQuestionId);

                var answerToRemove2 = App.Database.GetItems<SurveyAnswers>()?.FirstOrDefault(x =>
                    x.QuestionId == questionToRemove2.QuestionId
                    && x.AssignmentId == response.AssignmentId
                    && x.CompletedById == response.CompletedById);

                var commentsToDelete2 = App.Database.GetItems<SurveyComments>()?.Where(x =>
                    x.Identifier == answerToRemove2.Identifier).ToList();

                commentListToDelete.AddRange(commentsToDelete2);

                if (questionToRemove2.FurtherQuestionId > 0)
                {
                    var questionToRemove3 = App.Database.GetItems<SurveyQuestion>()?
                        .FirstOrDefault(x => x.QuestionId == questionToRemove2.FurtherQuestionId);

                    var answerToRemove3
                        = App.Database.GetItems<SurveyAnswers>()?
                            .FirstOrDefault(x => x.QuestionId == questionToRemove3.QuestionId
                                                 && x.AssignmentId == response.AssignmentId
                                                 && x.CompletedById == response.CompletedById);

                    var commentsToDelete3 = App.Database.GetItems<SurveyComments>()?.Where(x =>
                        x.Identifier == answerToRemove3.Identifier)?.ToList();

                    commentListToDelete.AddRange(commentsToDelete3);
                }
            }
        }
    }

    [Time]
    public void DeleteCurrentAudit(Audit auditToBeRemoved)
    {
        var ncrList = App.Database.GetItems<Ncr>()?.Where(x => x.AuditId == auditToBeRemoved?.AuditId).ToList();

        foreach (var ncr in ncrList) DeleteCurrentNcr(ncr);

        var answerPicturesToDelete = App.Database.GetItems<Pictures4Tablet>()
            ?.Where(x => x.Identifier == auditToBeRemoved?.AuditId).ToList();

        foreach (var pic in answerPicturesToDelete) App.Database.DeleteItem(pic);

        var answerNotesToDelete = App.Database.GetItems<SurveyComments>()
            ?.Where(x => x.Identifier == auditToBeRemoved?.AuditId).ToList();

        foreach (var note in answerNotesToDelete) App.Database.DeleteItem(note);

        var answers = App.Database.GetItems<SurveyAnswers>()?.Where(x => x.Identifier == auditToBeRemoved?.AuditId)
            .ToList();

        foreach (var answer in answers) App.Database.DeleteItem(answer);

        //var audit = App.Database.GetItems<Audit>().FirstOrDefault(z => z.AuditId == auditToBeRemoved.AuditId && z.RemoteTableId == 0);

        if (auditToBeRemoved != null) App.Database.DeleteItem(auditToBeRemoved);

        var assignment = App.Database.GetItems<Assignment>()?
            .FirstOrDefault(y => y.AssignmentId == auditToBeRemoved?.AssignmentId);

        if (assignment != null) App.Database.DeleteItem(assignment);
    }

    [Time]
    public async Task DeleteCurrentNcr(Ncr ncr)
    {
        var ncrPicturesToDelete =
            App.Database.GetItems<Pictures4Tablet>()?.Where(x => x.Identifier == ncr.NcrId)?.ToList();

        foreach (var pic in ncrPicturesToDelete) App.Database.DeleteItem(pic);

        var nonConformance = App.Database.GetItems<Ncr>()?.FirstOrDefault(x => x.NcrId == ncr.NcrId);

        if (nonConformance != null) App.Database.DeleteItem(nonConformance);
    }

    [Time]
    public void RemoveSurveyAnswer(decimal questionId, Guid assignmentId)
    {
        try
        {
            App.Database.DeleteAllItems(questionId, assignmentId);
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            var error = "Nothing to delete";
        }
    }

    [Time]
    public async Task RemoveMeasure(Measure measure)
    {
        var measureToDelete = App.Database.GetItems<Measure>()?.FirstOrDefault(x =>
            x.MeasureId == measure.MeasureId);

        var ratesToRemove = App.Database.GetItems<ProjectWorks>()?.Where(x =>
            x.Identifier == measure.MeasureId).ToList();

        foreach (var rate in ratesToRemove) App.Database.DeleteItem(rate);

        App.Database.DeleteItem(measureToDelete);
    }

    [Time]
    public Assignment GenerateAssignments2SaveById(Assignment assignmentToCreate)
    {
        try
        {
            assignmentToCreate.Measures = App.Database.GetItems<Measure>()
                ?.Where(x => x.AssignmentId == assignmentToCreate.AssignmentId
                             && x.RemoteTableId == 0)
                .ToList();

            var dfeList = App.Database.GetItems<Dfe>()?.ToList();
            assignmentToCreate.DfeList = dfeList.Where(x => x.RemoteTableId == "0"
                                                            && x.AssignmentId == assignmentToCreate.AssignmentId)
                .ToList();

            assignmentToCreate.ProjectWorks = App.Database.GetItems<ProjectWorks>()
                ?.Where(x => x.RemoteTableId.ToString() == "0"
                             && x.AssignmentId == assignmentToCreate.AssignmentId).ToList();

            assignmentToCreate.SurveyAnswers =
                App.Database.GetItems<SurveyAnswers>()?.Where(x =>
                        x.AssignmentId == assignmentToCreate.AssignmentId && x.RemoteTableId == 0)
                    .ToList();

            foreach (var answer in assignmentToCreate.SurveyAnswers)
            {
                if (answer.StreetName == "Unavailable")
                {
                    answer.StreetName = NavigationalParameters.CurrentAssignment?.StreetName;
                }
            }

            var pics = App.Database.GetItems<Pictures4Tablet>().ToList();
            var pics2 = pics
                ?.Where(x =>
                    x.AssignmentId == assignmentToCreate.AssignmentId ||
                    x.Identifier == assignmentToCreate.AssignmentId).OrderByDescending(X => X.DateTimeTaken).ToList();

            assignmentToCreate.SurveyPictures.AddRange(pics2.Where(x =>
                    x.SeverPictureId.ToString() == "0"
                    && !string.IsNullOrEmpty(x.FileName))
                .ToList());

            assignmentToCreate.SurveyComments.AddRange(
                App.Database.GetItems<SurveyComments>()
                    ?.Where(x => x.RemoteTableId == 0
                                 && x.AssignmentId == assignmentToCreate.AssignmentId).ToList());

            assignmentToCreate.PermitToDigList = App.Database.GetItems<DigPermit>()
                ?.Where(x => x.RemoteTableId == 0
                             && x.AssignmentId == assignmentToCreate.AssignmentId).ToList();

            assignmentToCreate.Audit = App.Database.GetItems<Audit>()
                ?.FirstOrDefault(x => x.RemoteTableId == 0 && x.AssignmentId == assignmentToCreate.AssignmentId);

            if (assignmentToCreate.Audit != null)
                assignmentToCreate.Audit.NcrList = App.Database.GetItems<Ncr>()
                    ?.Where(x => x.AuditId == assignmentToCreate.Audit.AuditId && x.RemoteTableId == 0)
                    .ToList();

            assignmentToCreate.Audits = new List<Audit> { assignmentToCreate.Audit };

            assignmentToCreate.Cvi = App.Database.GetItems<Cvi>()?.Where(x =>
                    x.AssignmentId == assignmentToCreate.AssignmentId && x.RemoteTableId == "0")
                .ToList();

            assignmentToCreate.LocationList = App.Database.GetItems<Location>()
                ?.Where(x => x.RemoteTableId == 0
                             && x.Identifier == assignmentToCreate.AssignmentId).ToList();

            assignmentToCreate.PermitToDigList = App.Database.GetItems<DigPermit>()
                ?.Where(x => x.RemoteTableId == 0
                             && x.AssignmentId == assignmentToCreate.AssignmentId).ToList();

            assignmentToCreate.Complete = "true";

            return assignmentToCreate;
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
            return null;
        }
    }

    [Time]
    public ObservableCollection<Assignment> GenerateAssignments2Save(string category)
    {
        var assignmentToSave = App.Database.GetItems<Assignment>()?.Where(x =>
            x.PreSiteById == NavigationalParameters.LoggedInUser.FocusId
            && x.Qnumber == NavigationalParameters.CurrentSelectedJob.QuoteNumber
            && x.RemoteTableId <= 0
            && x.Category.ToLower() == category.ToLower()
            && x.AssignmentId != Guid.Empty).ToList();

        foreach (var assignment in assignmentToSave)
        {
            assignment.Measures = App.Database.GetItems<Measure>()
                ?.Where(x => x.AssignmentId == assignment.AssignmentId
                             && x.RemoteTableId == 0).ToList();

            var dfeList = App.Database.GetItems<Dfe>().ToList();

            assignment.DfeList = dfeList.Where(x => x.RemoteTableId == "0"
                                                    && x.AssignmentId == assignment.AssignmentId)
                .ToList();


            assignment.Cvi = App.Database.GetItems<Cvi>()
                ?.Where(x => x.AssignmentId == assignment.AssignmentId).ToList();

            assignment.ProjectWorks = App.Database.GetItems<ProjectWorks>()?
                .Where(x => x.RemoteTableId.ToString() == "0"
                            && x.AssignmentId == assignment.AssignmentId).ToList();

            assignment.SurveyAnswers =
                App.Database.GetItems<SurveyAnswers>()?.Where(x => x.AssignmentId == assignment.AssignmentId)
                    .ToList();

            var pics = App.Database.GetItems<Pictures4Tablet>().Where(x =>
                x.AssignmentId == assignment.AssignmentId || x.Identifier == assignment.AssignmentId).ToList();

            assignment.SurveyPictures.AddRange(pics.Where(x =>
                    x.SeverPictureId.ToString() == "0")
                .ToList());

            assignment.SurveyComments.AddRange(
                App.Database.GetItems<SurveyComments>()
                    ?.Where(x => x.RemoteTableId == 0
                                 && x.AssignmentId == assignment.AssignmentId).ToList());

            assignment.PermitToDigList = App.Database.GetItems<DigPermit>()?
                .Where(x => x.RemoteTableId == 0
                            && x.AssignmentId == assignment.AssignmentId).ToList();
            ;

            assignment.LocationList = App.Database.GetItems<Location>()?
                .Where(x => x.RemoteTableId == 0
                            && x.Identifier == assignment.AssignmentId).ToList();
            ;
        }

        return new ObservableCollection<Assignment>(assignmentToSave);
    }

    [Time]
    public async Task UpdateAnswer(SurveyAnswers selectedAnswer)
    {
        var answerToUpdate = App.Database.GetItems<SurveyAnswers>()?.FirstOrDefault(x =>
            x.AssignmentId == selectedAnswer.AssignmentId
            && x.Category.ToLower() == selectedAnswer.Category.ToLower()
            && x.QuestionId == selectedAnswer.QuestionId);

        answerToUpdate.AnswerGiven = selectedAnswer.AnswerGiven;
        App.Database.SaveItem(answerToUpdate);
    }

    [Time]
    public ObservableCollection<Assignment> GenerateAssignment2Save(long loggedInUserId)
    {
        var assignments = App.Database.GetItems<Assignment>()?.ToList();

        return new ObservableCollection<Assignment>(assignments.Where(x =>
            x.PreSiteById == loggedInUserId && x.AssignmentId != Guid.Empty));
    }

    [Time]
    public async Task RefreshAssignmentdata(ObservableCollection<Assignment> assignments)
    {
        foreach (var assignment in assignments)
        {
            foreach (var response in assignment.SurveyAnswers)
            {
                foreach (var rpic in response.ResponsePictures)
                {
                    var pic = App.Database.GetItems<Pictures4Tablet>()?
                        .FirstOrDefault(x => x.FileName == rpic.FileName);
                    if (pic != null) App.Database.DeleteItem(pic);

                    App.Database.SaveItem(rpic);
                }

                foreach (var rcom in response.ResponseComments)
                {
                    var com = App.Database.GetItems<SurveyComments>()?
                        .FirstOrDefault(x => x.WorksIdForDelete == rcom.WorksIdForDelete);
                    if (com != null) App.Database.DeleteItem(com);

                    App.Database.SaveItem(rcom);
                }
            }

            foreach (var dfe in assignment.DfeList)
            {
                foreach (var dfePic in dfe.DfePictures)
                {
                    var dfep = App.Database.GetItems<Pictures4Tablet>()?
                        .FirstOrDefault(x => x.FileName == dfePic.FileName);
                    if (dfep != null) App.Database.DeleteItem(dfep);

                    App.Database.SaveItem(dfePic);
                }

                foreach (var work in dfe.DfeWorks)
                {
                    var dfep = App.Database.GetItems<ProjectWorks>()?
                        .FirstOrDefault(x => x.WorksIdForDelete == work.WorksIdForDelete);
                    if (dfep != null) App.Database.DeleteItem(dfep);

                    App.Database.SaveItem(work);
                }
            }

            foreach (var comment in assignment.SurveyComments)
            {
                var dfep = App.Database.GetItems<SurveyComments>()?
                    .FirstOrDefault(x => x.WorksIdForDelete == comment.WorksIdForDelete);
                if (dfep != null) App.Database.DeleteItem(dfep);

                App.Database.SaveItem(comment);
            }

            foreach (var picture in assignment.SurveyPictures)
            {
                var dfep = App.Database.GetItems<Pictures4Tablet>()
                    ?.FirstOrDefault(x => x.FileName == picture.FileName);
                if (dfep != null) App.Database.DeleteItem(dfep);

                App.Database.SaveItem(picture);
            }

            foreach (var measure in assignment.Measures)
            {
                var mes = App.Database.GetItems<Measure>()?.FirstOrDefault(x => x.MeasureId == measure.MeasureId);
                if (mes != null) App.Database.DeleteItem(mes);

                App.Database.SaveItem(measure);
            }

            foreach (var mWorks in assignment.ProjectWorks)
            {
                var mes = App.Database.GetItems<ProjectWorks>()?
                    .FirstOrDefault(x => x.Identifier == mWorks.Identifier);
                if (mes != null) App.Database.DeleteItem(mes);

                App.Database.SaveItem(mWorks);
            }
        }
    }

    [Time]
    public async Task SaveAudit(Assignment auditToBeUpdated, bool completed)
    {
        auditToBeUpdated.Complete = completed.ToString().ToLower();
        auditToBeUpdated.CompletedOn = DateTime.Now;
        App.Database.SaveItem(auditToBeUpdated);
        App.Database.SaveItem(auditToBeUpdated.Audit);
    }

    [Time]
    public SurveyAnswers GetCurrentResponse(Guid auditId, decimal questionId)
    {
        return App.Database.GetItems<SurveyAnswers>().OrderByDescending(x => x.SubmittedDateTime)
            .FirstOrDefault(x => x.QuestionId == questionId && x.AssignmentId == auditId);
    }

    [Time]
    public async Task SaveNcr(Ncr ncrToSave)
    {
        App.Database.SaveItem(ncrToSave);
    }

    [Time]
    public void DeleteAssignment(Assignment assignmentToDelete)
    {
        App.Database.DeleteItem(assignmentToDelete);
    }

    [Time]
    public void DeleteCvi(Assignment assignmentToDelete)
    {
        var answerPicturesToDelete = App.Database.GetItems<Pictures4Tablet>()
            ?.Where(x => x.AssignmentId == assignmentToDelete.AssignmentId).ToList();

        foreach (var pic in answerPicturesToDelete) App.Database.DeleteItem(pic);

        foreach (var cvi in assignmentToDelete.Cvi) App.Database.DeleteItem(cvi);

        App.Database.DeleteItem(assignmentToDelete);
    }
}