#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using FocusXamarinMobileApplication.Views;
using MethodTimer;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class AddMeasurePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public enum SupervisorMeasureAction
    {
        ADD_MEASURE,
        ADD_SUPER_CLAIM,
        MODIFY,
        MODIFY_LATERAL
    }


    private readonly Users _userService;
    public readonly Jobs JobService;
    private JobData4Tablet _jobData;
    private List<ProjectWorks> _measures;
    private string _pageHome;

    private RelayCommand<string> _returnToRateList;

    private RelayCommand<string> _screenLoadedCommand4AddMeasure;

    public Assignments AssignmentService;

    public List<string> Measures;

    [Time]
    public AddMeasurePageViewModel()
    {
        AssignmentService = new Assignments();
        JobService = new Jobs();
        _userService = new Users();
    }

    public string ClaimDesc { get; set; }
    public string BaseUnit { get; set; }
    public string ClaimHeader { get; set; }

    //public SupervisorMeasureAction SupervisorAction { get; set; }

    public ProjectWorks SelectedMeasure { get; set; }

    private RelayCommand<Tuple<string, long>> _updateMeasure { get; set; }

    public Tuple<Person, List<Assignment>, JobData4Tablet, List<ProjectWorks>> NavPassedData { get; set; }

    public Tuple<Person, List<Assignment>, JobData4Tablet, List<ProjectWorks>, ClaimedFile,
        ProjectWorks, string> NavPassedInfo { get; set; }

    public string _projectDate { get; set; }

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; }

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    public ClaimedFile ClaimedFileToAdd { get; set; }

    public RelayCommand<string> ScreenLoadedCommand4AddMeasure
    {
        get
        {
            return _screenLoadedCommand4AddMeasure ??= new RelayCommand<string>(e =>
            {
                NavigationalParameters.ReturnPage = e;
                // ClaimedFileToAdd = NavigationalParameters.ClaimFile;
                NavigationalParameters.ClaimFile.ClaimHeader =
                    ClaimHeader = NavigationalParameters.CurrentRate?.Header;
                NavigationalParameters.ClaimFile.ClaimDesc =
                    ClaimDesc = NavigationalParameters.CurrentRate?.Description;
                NavigationalParameters.ClaimFile.BaseUnit = BaseUnit = NavigationalParameters.CurrentRate?.BaseUnit;
                NavigationalParameters.ClaimFile.SynCode =
                    NavigationalParameters.CurrentRate?.RemoteTableId.ToString() ?? "0";
                //  var jobData = NavPassedInfo.Item3;
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
                ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";
            });
        }
    }

    public RelayCommand<string> ReturnToRateList
    {
        get
        {
            return _returnToRateList ??= new RelayCommand<string>(async e =>
            {
                NavigationalParameters.ReturnPage = e;
                // NavPassedData =
                //   new Tuple<Person, List<Models.Assignment>, JobData4Tablet,
                //     List<ProjectWorks>>(NavPassedInfo.Item1, NavPassedInfo.Item2,
                //   NavPassedInfo.Item3, NavPassedInfo.Item4);
                //  NavigationalParameters.NavigationParameter = NavPassedData;
                await NavigateTo(ViewModelLocator.InputMeasurePage);
            });
        }
    }

    public RelayCommand<Tuple<string, long>> UpdateMeasure
    {
        get
        {
            return _updateMeasure ??= new RelayCommand<Tuple<string, long>>(async e =>
            {
                try
                {
                    if (Convert.ToDecimal(e.Item1) + Convert.ToDecimal(NavigationalParameters.CurrentRate?.Qty) >=
                        0)
                    {
                        NavigationalParameters.ClaimFile.ClaimQty = Convert.ToDecimal(e.Item1);

                        NavigationalParameters.ClaimFile.SynCode = e.Item2.ToString() ?? "0";

                        if (!string.IsNullOrEmpty(NavigationalParameters.ClaimFile?.ClaimId.ToString()))
                        {
                            JobService.AddClaimedFile(NavigationalParameters.ClaimFile);

                            //  NavigationalParameters.NavigationParameter =
                            //    new Tuple<Person, List<Models.Assignment>, JobData4Tablet,
                            //      List<ProjectWorks>
                            //    , string>(NavPassedInfo.Item1, NavPassedInfo.Item2, NavPassedInfo.Item3,
                            //  NavPassedInfo.Item4, NavPassedInfo.Item7);
                            NavigationalParameters.AppMode = NavigationalParameters.AppModes.MENU;
                            //NavigationalParameters.ReturnPage = Locator.MenuPageViewModelKey;
                            NavigationalParameters.PassedInfo = new MenuSelectionPage();
                            await NavigateTo(ViewModelLocator.MenuSelectionPage);
                        }
                        else
                        {
                            await Alert(
                                "There has been an error whilst saving the measure please try again or contact support!",
                                "Error!");
                        }
                    }
                    else
                    {
                        await Alert(
                            "Cannot subtract a higher value than the total claim!", "Error!");
                    }
                }
                catch
                {
                    await Alert(
                        "Please Input a Decimal Value!", "Error!");
                }
            });
        }
    }

    [Time]
    public async Task GoBack()
    {
        NavigateBack();
    }


    #region SUPERVISOR

    public RelayCommand<string> ScreenLoadedSupervisor => new(e =>
    {
        //if (e is string str) _pageHome = str;
        // if (NavigationalParameters.PreviousPageKey != null)
        //{
        NavigationalParameters.ReturnPage = e;
        //  NavigationalParameters.PreviousPageKey = null;
        // }
        Measures = new List<string>();
        var projectWorksList = new List<ProjectWorks>();

        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = $"{NavigationalParameters.CurrentSelectedJob.JobDate:dd/MM/yyyy}";
        //NavigationalParameters.ClaimFile = null;
        // CurrentSelectedJob = NavigationalParameters.CurrenSelectedJob;


        // NavigationalParameters.SupervisorAction = SupervisorMeasureAction.MODIFY;

        //ClaimedFileToAdd = navParam2;

        if (NavigationalParameters.ClaimFile != null)
        {
            ClaimHeader = NavigationalParameters.ClaimFile?.ClaimHeader; // = navParam2.ClaimHeader;
            ClaimDesc = NavigationalParameters.ClaimFile?.ClaimDesc; // = navParam2.ClaimDesc;
            //NavigationalParameters.ClaimFile.SynCode = SelectedMeasure?.RemoteTableId.ToString() ?? "0";
            // NavigationalParameters.ClaimFile.SupervisorChanges = navParam2?.SupervisorChanges;
        }

        switch (NavigationalParameters.SupervisorAction)
        {
            case NavigationalParameters.SupervisorMeasureAction.MODIFY:
                BaseUnit = NavigationalParameters.ClaimFile?.BaseUnit;
                //  NavigationalParameters.ClaimFile.ClaimQty = navParam2.ClaimQty;


                ClaimHeader = NavigationalParameters.ClaimFile?.ClaimHeader;
                BaseUnit = NavigationalParameters.ClaimFile?.BaseUnit;
                ClaimDesc = NavigationalParameters.ClaimFile?.ClaimDesc;

                OnPropertyChanged("ClaimedFileToAdd");
                break;
            case NavigationalParameters.SupervisorMeasureAction.MODIFY_LATERAL:
                BaseUnit = NavigationalParameters.ClaimFile?.BaseUnit;
                //NavigationalParameters.ClaimFile.ClaimQty = ClaimedFileToAdd.width;
                //NavigationalParameters.ClaimFile.ClaimQty = ClaimedFileToAdd.ClaimQty;                  

                ClaimHeader = NavigationalParameters.ClaimFile?.ClaimHeader;
                BaseUnit = NavigationalParameters.ClaimFile?.BaseUnit;
                ClaimDesc = NavigationalParameters.ClaimFile?.ClaimDesc;

                OnPropertyChanged("ClaimedFileToAdd");
                break;
            case NavigationalParameters.SupervisorMeasureAction.ADD_MEASURE:
                _measures = AssignmentService
                    .GetRates(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract")
                    .Where(x => x.AssignmentId == Guid.Empty
                                && x.Category.ToLower() != "supervisor"
                                && x.Category.ToLower() != "commercial")
                    .Select(i => new ProjectWorks
                    {
                        RemoteTableId = i.RemoteTableId,
                        BaseUnit = i?.BaseUnit,
                        Description = i?.Description,
                        Header = i?.Header,
                        QuoteId = i.QuoteId,
                        Category = i?.Category.ToUpper()
                    }).ToList();

                if (_measures?.Count > 0)
                {
                    foreach (var item in _measures) Measures.Add(item?.Description);

                    SelectMeasurePicker.Execute(0);

                    ClaimHeader = SelectedMeasure?.Header;
                    BaseUnit = SelectedMeasure?.BaseUnit;
                    ClaimDesc = SelectedMeasure?.Description;
                }

                break;
            case NavigationalParameters.SupervisorMeasureAction.ADD_SUPER_CLAIM:
                _measures = AssignmentService
                    .GetRates(NavigationalParameters.CurrentSelectedJob.QuoteNumber, "Contract")
                    .Where(x => x.AssignmentId == Guid.Empty
                                && x.Category.ToLower() == "supervisor")
                    .Select(i => new ProjectWorks
                    {
                        RemoteTableId = i.RemoteTableId,
                        BaseUnit = i?.BaseUnit,
                        Description = i?.Description,
                        Header = i?.Header,
                        QuoteId = i.QuoteId,
                        Category = i?.Category.ToUpper(),
                        Qty = ""
                    }).ToList();

                if (_measures?.Count > 0)
                {
                    foreach (var item in _measures) Measures.Add(item?.Description);

                    SelectMeasurePicker.Execute(0);

                    ClaimHeader = SelectedMeasure?.Header;
                    BaseUnit = SelectedMeasure?.BaseUnit;
                    ClaimDesc = SelectedMeasure?.Description;
                }

                break;
        }

        OnPropertyChanged("ClaimHeader");
    });

    public JobData4Tablet CurrentSelectedJob { get; set; }

    public RelayCommand<int> SelectMeasurePicker => new(e =>
    {
        if (_measures?.Count > 0)
        {
            SelectedMeasure = _measures[e];
            ClaimHeader = SelectedMeasure?.Header;
            BaseUnit = SelectedMeasure?.BaseUnit;
            ClaimDesc = SelectedMeasure?.Description;
        }

        OnPropertyChanged("ClaimHeader");
    });

    public RelayCommand NavBack => new(async () =>
    {
        // NavigationalParameters.PassedInfo = null;
        NavigateBack();
    });

    public RelayCommand<string> UpdateSupervisorMeasure => new(async e =>
    {
        //modifyValue used to check when editing laterals, defaults to true unless lateral value does not parse
        //  var modifyValue = true;
        decimal d = 0;
        //if (NavigationalParameters.SupervisorAction == SupervisorMeasureAction.MODIFY_LATERAL)
        //    modifyValue = decimal.TryParse(BaseUnit, out d);

        if (decimal.TryParse(e, out var qty) && qty >= 0)
        {
            switch (NavigationalParameters.SupervisorAction)
            {
                case NavigationalParameters.SupervisorMeasureAction.ADD_MEASURE:
                    NavigationalParameters.ClaimFile = new ClaimedFile();
                    NavigationalParameters.ClaimFile.ApprovedBySupervisorName =
                        NavigationalParameters.LoggedInUser?.FullName;
                    NavigationalParameters.ClaimFile.SupervisorChanges =
                        $"This Measure Data has been input by Supervisor: {NavigationalParameters.ClaimFile?.ApprovedBySupervisorName} on {DateTime.Now}, Qty input by Supervisor is {e}";
                    NavigationalParameters.ClaimFile.ClaimQty = Convert.ToDecimal(e);
                    NavigationalParameters.ClaimFile.PostedByGanger = DateTime.Parse("02/02/1900 00:00:00");
                    NavigationalParameters.ClaimFile.PostedByGangerName =
                        $"{NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLeaderFirstName} {NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLeaderSurname}";
                    NavigationalParameters.ClaimFile.ApprovedBySupervisor = DateTime.Parse("01/01/1900 00:00:00");
                    OnPropertyChanged("ClaimedFileToAdd");
                    break;
                case NavigationalParameters.SupervisorMeasureAction.MODIFY:
                    if (string.IsNullOrEmpty(NavigationalParameters.ClaimFile.OriginalClaimQtyByGang))
                        NavigationalParameters.ClaimFile.OriginalClaimQtyByGang =
                            NavigationalParameters.ClaimFile?.ClaimQty.ToString();
                    NavigationalParameters.ClaimFile.ApprovedBySupervisorName =
                        NavigationalParameters.LoggedInUser?.FullName;
                    NavigationalParameters.ClaimFile.ClaimQty = qty;
                    NavigationalParameters.ClaimFile.SupervisorChanges =
                        $"Measure Data has been changed on {DateTime.Now} by Supervisor: {NavigationalParameters.ClaimFile?.ApprovedBySupervisorName} on his IPad, old qty was {NavigationalParameters.ClaimFile?.OriginalClaimQtyByGang}, New Qty is {NavigationalParameters.ClaimFile?.ClaimQty}";
                    break;
                case NavigationalParameters.SupervisorMeasureAction.MODIFY_LATERAL:
                    NavigationalParameters.ClaimFile.OriginalClaimQtyByGang +=
                        $",{NavigationalParameters.ClaimFile?.ClaimWidth}";
                    NavigationalParameters.ClaimFile.ClaimWidth = d;
                    NavigationalParameters.ClaimFile.ClaimQty = qty;
                    NavigationalParameters.ClaimFile.ApprovedBySupervisorName =
                        NavigationalParameters.LoggedInUser?.FullName;
                    NavigationalParameters.ClaimFile.SupervisorChanges =
                        $"Measure Data has been changed on {DateTime.Now} by Supervisor: {NavigationalParameters.ClaimFile?.ApprovedBySupervisorName} on his IPad, old qty was {NavigationalParameters.ClaimFile?.OriginalClaimQtyByGang}, New Qty is {NavigationalParameters.ClaimFile?.ClaimQty}";
                    break;
                case NavigationalParameters.SupervisorMeasureAction.ADD_SUPER_CLAIM:
                    NavigationalParameters.ClaimFile = new ClaimedFile();
                    // NavigationalParameters.ClaimFile.OriginalClaimQtyByGang += $",{ NavigationalParameters.ClaimFile?.ClaimWidth}";
                    // NavigationalParameters.ClaimFile.ClaimWidth = d;
                    NavigationalParameters.ClaimFile.ApprovedBySupervisorName =
                        NavigationalParameters.LoggedInUser?.FullName;
                    NavigationalParameters.ClaimFile.SupervisorChanges =
                        $"This Measure Data has been input by Supervisor {NavigationalParameters.ClaimFile?.ApprovedBySupervisorName} on {DateTime.Now}, Qty input by Supervisor is {e}";
                    NavigationalParameters.ClaimFile.ClaimQty = qty;
                    NavigationalParameters.ClaimFile.PostedByGanger = DateTime.Parse("02/02/1900 00:00:00");
                    NavigationalParameters.ClaimFile.PostedByGangerName =
                        $"{NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLeaderFirstName} {NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLeaderSurname}";
                    NavigationalParameters.ClaimFile.ClaimGang =
                        NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString();
                    NavigationalParameters.ClaimFile.ApprovedBySupervisor = DateTime.Now;
                    break;
            }

            NavigationalParameters.ClaimFile.ContractReference =
                NavigationalParameters.CurrentSelectedJob?.ContractReference;
            NavigationalParameters.ClaimFile.QuoteId =
                NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString();
            NavigationalParameters.ClaimFile.ContractId =
                NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString();
            NavigationalParameters.ClaimFile.ConPrefix = NavigationalParameters.CurrentSelectedJob?.ContractPrefix;
            NavigationalParameters.ClaimFile.ClaimSupervisor =
                NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();
            NavigationalParameters.ClaimFile.ClaimGang =
                NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
            NavigationalParameters.ClaimFile.ClaimDate = NavigationalParameters.CurrentSelectedJob.JobDate;
            NavigationalParameters.ClaimFile.ClaimHeader = ClaimHeader;
            NavigationalParameters.ClaimFile.ClaimDesc = ClaimDesc;
            NavigationalParameters.ClaimFile.BaseUnit = BaseUnit;
            NavigationalParameters.ClaimFile.SynCode = NavigationalParameters.ClaimFile.SynCode ??=
                SelectedMeasure?.RemoteTableId.ToString();


            if (NavigationalParameters.ClaimFile.ClaimId != Guid.Empty)
            {
                JobService.AddClaimedFile(NavigationalParameters.ClaimFile);

                NavBack.Execute(null);
            }
            else
            {
                await Alert(
                    "There has been an error whilst saving the measure please try again or contact support!",
                    "Error!");
            }
        }
        else
        {
            await Alert("Please Input a Decimal Value!", "Error!");
        }
    });

    #endregion
}