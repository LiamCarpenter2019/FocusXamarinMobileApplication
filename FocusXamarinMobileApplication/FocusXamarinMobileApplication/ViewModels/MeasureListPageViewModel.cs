using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class MeasureListPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;


    public MeasureListPageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
        Title = "Measures";
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
    }

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

    public List<ProjectWorks> _currentWorksList { get; private set; }
    public RelayCommand<ProjectWorks> _screenLoadedCommand4InputMeasureEdit { get; private set; }
    public RelayCommand<string> _searchProjectWorks { get; private set; }
    public RelayCommand<string> _addLateralMeasure { get; private set; }
    public List<ClaimedFile> ClaimsToBeSubmitted { get; set; }


    public string _searchText { get; set; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;

            SearchProjectWorks();
            OnPropertyChanged();
        }
    }

    public string _title { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string _buttonText { get; set; }

    public string ButtonText
    {
        get => _buttonText;
        set
        {
            _buttonText = value;
            OnPropertyChanged();
        }
    }

    public string _submitButtonText { get; set; }

    public string SubmitButtonText
    {
        get => _submitButtonText;
        set
        {
            _submitButtonText = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection1 { get; set; }

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; }

    public bool ShowSection2
    {
        get => _showSection2;
        set
        {
            _showSection2 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection3 { get; set; }

    public bool ShowSection3
    {
        get => _showSection3;
        set
        {
            _showSection3 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection4 { get; set; }

    public bool ShowSection4
    {
        get => _showSection4;
        set
        {
            _showSection4 = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ProjectWorks> _projectWorksList { get; set; }

    public ObservableCollection<ProjectWorks> ProjectWorksList
    {
        get => _projectWorksList;
        set
        {
            _projectWorksList = value;
            OnPropertyChanged();
        }
    }

    public ProjectWorks _selectedProjectWork { get; set; }

    public ProjectWorks SelectedProjectWork
    {
        get => _selectedProjectWork;
        set
        {
            _selectedProjectWork = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;

        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");

        ProjectWorksList = new ObservableCollection<ProjectWorks>();

        if (NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
            ButtonText = "Back";
        else
            ButtonText = "Menu";

        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
            NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
        {
            ClaimsToBeSubmitted = _jobService.GetJobClaimedFiles()?.ToList();

            switch (NavigationalParameters.ClaimType.ToLower())
            {
                case "cabling":
                    {
                        foreach (var workItem in NavigationalParameters.CurrentWorksList)
                            if (!ProjectWorksList.Any(x =>
                                    x.Header == workItem.Header &&
                                    x.QuoteId == NavigationalParameters.CurrentSelectedJob.QuoteNumber))
                            {
                                workItem.Qty = ClaimsToBeSubmitted?.Where(x => x.ClaimHeader == workItem?.Header)
                                    .Sum(x => Convert.ToDecimal(x.ClaimQty)).ToString();
                                ProjectWorksList.Add(workItem);
                            }

                        break;
                    }
                case "civils":
                    {
                        foreach (var workItem in NavigationalParameters.CurrentWorksList)
                            switch (workItem.Header.ToLower())
                            {
                                //Excluded from search.
                                case "1005":
                                    break;
                                case "1011":
                                    break;
                                case "1017":
                                    break;
                                case "1065":
                                    break;
                                case "1069":
                                    break;
                                case "1073":
                                    break;
                                case "4004":
                                    break;
                                case "4008":
                                    break;
                                case "4012":
                                    break;
                                case "4016":
                                    break;
                                case "4020":
                                    break;
                                case "gr1":
                                    break;
                                case "d101":
                                    break;
                                default:
                                    if (!ProjectWorksList.Any(x =>
                                            x.Header == workItem.Header &&
                                            x.QuoteId == NavigationalParameters.CurrentSelectedJob.QuoteNumber))
                                    {
                                        workItem.Qty = ClaimsToBeSubmitted?.Where(x => x.ClaimHeader == workItem?.Header)
                                            .Sum(x => Convert.ToDecimal(x.ClaimQty)).ToString();
                                        ProjectWorksList.Add(workItem);
                                    }

                                    break;
                            }

                        break;
                    }
                case "reinstator":
                    {
                        foreach (var workItem in NavigationalParameters.CurrentWorksList)
                            if (!ProjectWorksList.Any(x =>
                                    x.Header == workItem.Header &&
                                    x.QuoteId == NavigationalParameters.CurrentSelectedJob?.QuoteNumber))
                            {
                                workItem.Qty = ClaimsToBeSubmitted?.Where(x => x.ClaimHeader == workItem?.Header)
                                    .Sum(x => Convert.ToDecimal(x.ClaimQty)).ToString();
                                ProjectWorksList.Add(workItem);
                            }

                        break;
                    }
                case "site support":
                    {
                        foreach (var workItem in NavigationalParameters.CurrentWorksList)
                            if (!ProjectWorksList.Any(x =>
                                    x.Header.ToLower() == "gr1" &&
                                    x.QuoteId == NavigationalParameters.CurrentSelectedJob?.QuoteNumber))
                            {
                                workItem.Qty = ClaimsToBeSubmitted?.Where(x => x.ClaimHeader == workItem?.Header)
                                    .Sum(x => Convert.ToDecimal(x.ClaimQty)).ToString();
                                ProjectWorksList.Add(workItem);
                            }

                        break;
                    }
                case "pole":
                case "duct":
                case "distribution point":
                case "chamber":
                case "ugcrp":
                case "overhead cable":
                case "joint enclosure":
                    foreach (var workItem in NavigationalParameters.CurrentWorksList)
                        if (!ProjectWorksList.Any(x =>
                                x.Header == workItem.Header &&
                                x.QuoteId == NavigationalParameters.CurrentSelectedJob.QuoteNumber))
                        {
                            workItem.Qty = ClaimsToBeSubmitted?.Where(x =>
                                    x.ClaimHeader == workItem?.Header &&
                                    x.ClaimId == NavigationalParameters.SelectedAsset.ClaimId)
                                .Sum(x => Convert.ToDecimal(x.ClaimQty)).ToString();

                            ProjectWorksList.Add(workItem);
                        }
                    break;
                default:
                    {
                        if (NavigationalParameters.ClaimType.ToLower() == "") NavigationalParameters.ClaimFile = null;

                        var works = NavigationalParameters.CurrentWorksList
                            .Where(x => x.QuoteId == NavigationalParameters.CurrentSelectedJob.QuoteNumber).ToList();

                        foreach (var workItem in works)
                            if (ProjectWorksList.All(x => x.Header != workItem.Header))
                            {
                                workItem.Qty = ClaimsToBeSubmitted?.Where(x => x.ClaimHeader == workItem?.Header)
                                    .Sum(x => Convert.ToDecimal(x.ClaimQty)).ToString();
                                ProjectWorksList.Add(workItem);
                            }

                        break;
                    }
            }
        }

        ProjectWorksList = new ObservableCollection<ProjectWorks>(ProjectWorksList?.OrderBy(x => x.Header));
    });

    public RelayCommand AddLateralMeasure => new(async () =>
    {
        var EndPointsAvailable = _jobService.CheckEndpoints(NavigationalParameters.CurrentSelectedJob);
        if (EndPointsAvailable)
        {
            //NavigationalParameters.ReturnPage = Locator.InputMeasurePageViewModelKey;

            if (NavigationalParameters.ProjectWorksList?.Count > 0)
            {
                NavigationalParameters.CurrentRate = SelectedProjectWork;

                NavigationalParameters.ClaimFile = new ClaimedFile
                {
                    ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference,
                    QuoteId = NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                    ContractId = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                    ConPrefix = NavigationalParameters.CurrentSelectedJob?.ContractPrefix,
                    ClaimSupervisor = NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString(),
                    ClaimGang = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString(),
                    ClaimDate = NavigationalParameters.CurrentSelectedJob.JobDate,
                    SynCode = NavigationalParameters.CurrentRate?.RemoteTableId.ToString(),
                    PostedByGanger = DateTime.Now,
                    PostedByGangerName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName,
                    ClaimHeader = NavigationalParameters.CurrentRate?.Header,
                    ClaimDesc = NavigationalParameters.CurrentRate.Description,
                    BaseUnit = NavigationalParameters.CurrentRate.BaseUnit,
                    ClaimType = NavigationalParameters.SelectedAsset.BuildingStandard
                };

                await NavigateTo(ViewModelLocator.InputMeasurePage);
                // await NavigateTo(ViewModelLocator.InputMeasurePage);
            }
            else
            {
                await Alert("Please Contact Support No Work Items Available!", "Error!");
            }
        }
        else
        {
            await Alert("Thereare currently no end points available",
                "please contact the support team!");
        }
    });

    public RelayCommand AddMeasure => new(async () =>
    {
        NavigationalParameters.CurrentRate = SelectedProjectWork;

        if (!string.IsNullOrEmpty(NavigationalParameters.CurrentRate.Header) && NavigationalParameters.CurrentRate.Header != "0")
        {
            NavigationalParameters.ClaimFile = App.Database.GetItems<ClaimedFile>().FirstOrDefault(x => x.QuoteId == NavigationalParameters.CurrentRate.QuoteId.ToString()
              && x.ClaimHeader == NavigationalParameters.CurrentRate?.Header
              && x.ClaimId == NavigationalParameters.SelectedAsset?.ClaimId)
                    ?? new ClaimedFile
                    {
                            ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference,
                            QuoteId = NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                            ContractId = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                            ConPrefix = NavigationalParameters.CurrentSelectedJob?.ContractPrefix,
                            ClaimSupervisor = NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString(),
                            ClaimGang = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString(),
                            ClaimDate = NavigationalParameters.CurrentSelectedJob.JobDate,
                            SynCode = NavigationalParameters.CurrentRate?.RemoteTableId.ToString(),
                            PostedByGanger = DateTime.Now,
                            PostedByGangerName = NavigationalParameters.CurrentSelectedJob?.GangLeaderName,
                            RemoteTableId = 0,
                            ClaimHeader = NavigationalParameters.CurrentRate.Header,
                            ClaimDesc = NavigationalParameters.CurrentRate.Description,
                            BaseUnit = NavigationalParameters.CurrentRate.BaseUnit,
                            ClaimType = NavigationalParameters.SelectedAsset.BuildingStandard
                    };

            if (NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
            {
                if (NavigationalParameters.SelectedAsset != null)
                    NavigationalParameters.ClaimFile.ClaimId = NavigationalParameters.SelectedAsset.ClaimId;
                NavigationalParameters.ClaimFile.ClaimQty = 1;

                await NavigateTo(ViewModelLocator.SurveyQuestionsPage);
            }
            else
            {
                if (NavigationalParameters.SelectedAsset != null && string.IsNullOrEmpty(NavigationalParameters.SelectedAsset?.ClaimId.ToString()))
                {
                    NavigationalParameters.SelectedAsset.ClaimId = Guid.NewGuid();

                    App.Database.SaveItem(NavigationalParameters.SelectedAsset);
                }
                else if (NavigationalParameters.SelectedAsset != null && !string.IsNullOrEmpty(NavigationalParameters.SelectedAsset?.ClaimId.ToString()))
                {
                    NavigationalParameters.ClaimFile.ClaimId = NavigationalParameters.SelectedAsset.ClaimId;

                    App.Database.SaveItem(NavigationalParameters.SelectedAsset);
                }

                App.Database.SaveItem(NavigationalParameters.ClaimFile);

                await NavigateTo(ViewModelLocator.InputMeasurePage);
            }
        }
    });

    public RelayCommand Cancel => new(async () =>
    {
        if (!NavigationalParameters.AppMode.ToString().ToLower().Contains("measure"))
            await NavigateTo(ViewModelLocator.SelectEndPointPage);
        else
            await NavigateTo(ViewModelLocator.MeasureTypeSelectionPage);
    });

    public void SearchProjectWorks()
    {
        ProjectWorksList = new ObservableCollection<ProjectWorks>(string.IsNullOrEmpty(SearchText.ToLower())
            ? NavigationalParameters.CurrentWorksList
            : NavigationalParameters.CurrentWorksList.Where(x =>
                x.Header.ToLower().Contains(SearchText.ToLower()) ||
                x.Description.ToLower().Contains(SearchText.ToLower())).ToList());
    }
}
