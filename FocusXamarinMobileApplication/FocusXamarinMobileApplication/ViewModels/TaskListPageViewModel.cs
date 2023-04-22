using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class TaskListPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;

    private readonly Users _userService;
    private RelayCommand _backCommand;

    public TaskListPageViewModel()
    {
        _jobService = new Jobs();

        _userService = new Users();
    }

    public string _projectDate { get; set; } =
        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

    public string ProjectDate
    {
        get => _projectDate;
        set
        {
            _projectDate = value;
            OnPropertyChanged();
        }
    }

    public string _projectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;

    public string ProjectName
    {
        get => _projectName;
        set
        {
            _projectName = value;
            OnPropertyChanged();
        }
    }

    //  public List<TaskViewModel> tempList { get; set; } 

    public string UserName { get; set; }

    public ObservableCollection<TaskItem> _jobTaskList { get; set; }

    public ObservableCollection<TaskItem> JobTaskList
    {
        get => _jobTaskList;
        set
        {
            _jobTaskList = value;
            OnPropertyChanged();
        }
    }

    private string _date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

    public string Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    public bool _showTick { get; set; }

    public bool ShowTick
    {
        get => _showTick;
        set
        {
            _showTick = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection1 { get; set; } = true;

    public bool ShowSection1
    {
        get => _showSection1;
        set
        {
            _showSection1 = value;
            OnPropertyChanged();
        }
    }

    public bool _showSection2 { get; set; } = true;

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

    public bool _loading { get; set; }

    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            OnPropertyChanged();
        }
    }

    public bool _showRefresh { get; set; } = true;

    public bool ShowRefresh
    {
        get => _showRefresh;
        set
        {
            _showRefresh = value;
            OnPropertyChanged();
        }
    }

    public string _title { get; set; } = "Task List";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string ErrorText { get; private set; }
    public string ErrorTitle { get; private set; }

    public TaskItem _selectedTaskItem { get; set; }

    public TaskItem SelectedTaskItem
    {
        get => _selectedTaskItem;
        set
        {
            _selectedTaskItem = value;
            OnPropertyChanged();
        }
    }

    public ListView _selectedListItem { get; set; }

    public ListView SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            _selectedListItem = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectName = "";

        Title = "Task List";

        ProjectDate = $"{DateTime.Now.Date:dd/MM/yyyy}";

        ShowRefresh = false;

        NavigationalParameters.LoggedInUser = _userService.Check4ValidLoggedInPerson();

        UserName = NavigationalParameters.LoggedInUser?.FullName;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.TOOLBOXTALKS
            || NavigationalParameters.AppMode == NavigationalParameters.AppModes.GANGTOOLBOXTALKS)
        {
            Title = "Toolbox talks";
            ErrorTitle = "No toolbox talks available";
            ErrorText = "toolbox talks";
        }
        else
        {
            Title = "Task List";
            ErrorText = "Tasks";
            ErrorTitle = "There are currently no tasks to complete!";
        }

        var Tasks = _jobService.GetTasks().Where(x => x.ContractReference != "legacy").ToList();

        if (Tasks.Any())
            JobTaskList = new ObservableCollection<TaskItem>(Tasks);
        else
            await Alert("There are no tasks to complete",
                "There are no jobs asllocated on the resource planner and no tasks to be completed. Please ensure all work has been allocated on the resource planner by contacting the operations team! Alternatly if there is still issues then please contact the support team",
                "Ok");
    });

    public RelayCommand NavigateToListItemView => new(async () =>
    {
        NavigationalParameters.SelectedTaskItem = SelectedTaskItem;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.TOOLBOXTALKS)
        {
            NavigationalParameters.SelectedTaskItem = SelectedTaskItem;

            NavigationalParameters.CurrentUserToolboxTalks = _jobService
                .GetItemsForToolboxTalks(SelectedTaskItem.SupervisorId)
                .Where(x => x.GangLeaderId == NavigationalParameters.SelectedTaskItem.GangLeaderId
                            && x.ToolBoxTalkCode == NavigationalParameters.SelectedTaskItem.Description).ToList();

            await NavigateTo(ViewModelLocator.ToolboxTalkDocumentViewPage);
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.GANGAPPROVALS)
        {
            await NavigateTo(ViewModelLocator.GangSelectPage);
        }
        else
        {
            if (SelectedTaskItem.Category.ToLower() == "toolboxtalks")
            {
                NavigationalParameters.SelectedTaskItem = SelectedTaskItem;

                NavigationalParameters.CurrentUserToolboxTalks = _jobService
                    .GetItemsForToolboxTalks(SelectedTaskItem.SupervisorId)
                    .Where(x => x.GangLeaderId == NavigationalParameters.SelectedTaskItem.GangLeaderId
                                && x.ToolBoxTalkCode == NavigationalParameters.SelectedTaskItem.Description).ToList();

                await NavigateTo(ViewModelLocator.ToolboxTalkDocumentViewPage);
            }
            else if (SelectedTaskItem.Category.ToLower().Contains("investigation"))
            {
                NavigationalParameters.SelectedTaskItem = SelectedTaskItem;

                if (!string.IsNullOrEmpty(SelectedTaskItem?.ContractReference))
                {
                    var PublicUtilityDamageGuid = Guid.Parse(SelectedTaskItem?.ContractReference);

                    NavigationalParameters.EventManagement = _jobService.GetEventManagement(PublicUtilityDamageGuid);
                }

                await NavigateTo(ViewModelLocator.InvestigateDamagePage);
            }
            else
            {
                await NavigateTo(ViewModelLocator.TeamOptionsPage);
            }
        }
    });

    public RelayCommand BackCommand => _backCommand ??= new RelayCommand(async () =>
    {
        await NavigateTo(ViewModelLocator.SupervisorProjectPage);
    });
}