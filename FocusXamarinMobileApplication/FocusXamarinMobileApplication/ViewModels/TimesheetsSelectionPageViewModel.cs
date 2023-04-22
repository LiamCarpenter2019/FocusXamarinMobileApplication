namespace FocusXamarinMobileApplication.ViewModels;

public class TimesheetsSelectionPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private readonly Users _userService;

    private RelayCommand<List<int>> _screenLoaded;

    public TimesheetsSelectionPageViewModel()
    {
        _userService = new Users();

        _jobService = new Jobs();
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

    public string _date { get; set; }

    public string Date
    {
        get => _date;
        set
        {
            _date = value;
            OnPropertyChanged();
        }
    }

    public string _userName { get; set; }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
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


    public bool _showApprovalButton { get; set; }

    public bool ShowApprovalButton
    {
        get => _showApprovalButton;
        set
        {
            _showApprovalButton = value;
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

    public bool _showSupervisorControls { get; set; }

    public bool ShowSupervisorControls
    {
        get => _showSupervisorControls;
        set
        {
            _showSupervisorControls = value;
            OnPropertyChanged();
        }
    }

    public List<Labour> _labourFiles { get; set; }

    public List<Labour> LabourFiles
    {
        get => _labourFiles;
        set
        {
            _labourFiles = value;
            OnPropertyChanged();
        }
    }

    public Labour _selectedLabourFile { get; set; }

    public Labour SelectedLabourFile
    {
        get => _selectedLabourFile;
        set
        {
            _selectedLabourFile = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Labour> _gangLabourFiles { get; set; }

    public ObservableCollection<Labour> GangLabourFiles
    {
        get => _gangLabourFiles;
        set
        {
            _gangLabourFiles = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand<List<int>> ScreenLoaded
    {
        get
        {
            return _screenLoaded ??= new RelayCommand<List<int>>(e =>
            {
                LabourFiles = new List<Labour>();

                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                {
                    ProjectName = NavigationalParameters.SelectedTaskItem?.ProjectName;

                    Date = $"{NavigationalParameters.SelectedTaskItem?.Date:dd/MM/yyyy}";

                    LabourFiles = _jobService.GetJobLabour(NavigationalParameters.SelectedTaskItem).ToList();
                }
                else
                {
                    ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

                    Date = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";

                    NavigationalParameters.CurrentSelectedJob.JobGang = _jobService.GetCurrentSelectedGang();

                    foreach (var operative in NavigationalParameters.CurrentSelectedJob?.JobGang?.GangLabourFiles)
                    {
                        CurrentLabour = _jobService.GetJobLabour(NavigationalParameters.CurrentSelectedJob)?
                            .FirstOrDefault(x => x.LabourOperative == operative.FocusId.ToString());

                        if (CurrentLabour == null)
                        {
                            CurrentLabour = new Labour
                            {
                                FullName = operative.FullName,
                                Role = string.IsNullOrEmpty(operative.Role) ? "Operative" : operative.Role,
                                LabourDate = NavigationalParameters.CurrentSelectedJob.JobDate.Date,
                                ContractId = NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString(),
                                LabourSupervisor = NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString(),
                                QuoteId = NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(),
                                LabourGang = NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString(),
                                LabourAddress = NavigationalParameters.CurrentSelectedJob?.ProjectName,
                                LabourOperative = operative.FocusId.ToString(),
                                ContractReference = NavigationalParameters.CurrentSelectedJob?.ContractReference,
                                ClaimedorTracked = "T",
                                LabourType = "W",
                                ConPrefix = NavigationalParameters.CurrentSelectedJob?.ContractPrefix,
                                TimeOnSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                                TimeLeftSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                                ClaimedYardEnd = NavigationalParameters.CurrentSelectedJob.JobDate,
                                ClaimedYardStart = NavigationalParameters.CurrentSelectedJob.JobDate,
                                TravelFromSite = NavigationalParameters.CurrentSelectedJob.JobDate,
                                TravelToSite = NavigationalParameters.CurrentSelectedJob.JobDate
                            };

                            _userService.UpdateLabourFile(CurrentLabour);
                        }

                        ShowSupervisorControls = false;
                    }

                    LabourFiles = _jobService.GetJobLabour(NavigationalParameters.CurrentSelectedJob).ToList();
                }

                foreach (var labour in LabourFiles)
                {
                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                    {
                        ShowApprovalButton = labour.SupervisorMode = true;

                        ShowSupervisorControls = true;

                        if (labour?.SupervisorStart.Date == DateTime.Parse("01/01/1900 00:00:00"))
                        {
                            ShowSupervisorControls = false;
                            labour.SupervisorStart = labour.TravelToSite;
                        }

                        if (labour?.SupervisorOnSite.Date == DateTime.Parse("01/01/1900 00:00:00"))
                        {
                            ShowSupervisorControls = false;
                            labour.SupervisorOnSite = labour.TimeOnSite;
                        }

                        if (labour?.SupervisorYardStart.Date == DateTime.Parse("01/01/1900 00:00:00"))
                        {
                            ShowSupervisorControls = false;
                            labour.SupervisorYardStart = labour.ClaimedYardStart;
                        }

                        if (labour?.SupervisorYardEnd.Date == DateTime.Parse("01/01/1900 00:00:00"))
                        {
                            ShowSupervisorControls = false;
                            labour.SupervisorYardEnd = labour.ClaimedYardEnd;
                        }

                        if (labour?.SupervisorOffSite.Date == DateTime.Parse("01/01/1900 00:00:00"))
                        {
                            ShowSupervisorControls = false;
                            labour.SupervisorOffSite = labour.TimeLeftSite;
                        }

                        if (labour.SupervisorFinish.Date == DateTime.Parse("01/01/1900 00:00:00"))
                        {
                            ShowSupervisorControls = false;
                            labour.SupervisorFinish = labour.TravelFromSite;
                        }
                    }
                    else
                    {
                        ShowApprovalButton = labour.SupervisorMode = false;

                        labour.ContractId =
                            NavigationalParameters.CurrentSelectedJob?.ContractNumber.ToString();
                        labour.LabourSupervisor =
                            NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();
                        labour.QuoteId =
                            NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString();
                        labour.LabourGang =
                            NavigationalParameters.CurrentSelectedJob?.GangLeaderId.ToString();
                        labour.LabourAddress =
                            NavigationalParameters.CurrentSelectedJob?.ProjectName;

                        labour.ClaimedorTracked = "T";

                        labour.ConPrefix =
                            NavigationalParameters.CurrentSelectedJob?.ContractPrefix;
                    }

                    switch (labour?.LabourType?.ToLower())
                    {
                        case "c":
                            labour.bgColour = Color.Violet;
                            break;
                        case "s":
                            labour.bgColour = Color.Red;
                            break;
                        case "u":
                            labour.bgColour = Color.Orange;
                            break;
                        default:
                            labour.bgColour = Color.White;
                            break;
                    }

                    try
                    {
                        _userService.UpdateLabourFile(labour);
                    }
                    catch (Exception ex)
                    {
                        var error = ex.ToString();
                    }
                }

                GangLabourFiles = new ObservableCollection<Labour>(LabourFiles);
            });
        }
    }

    public RelayCommand OnListViewItemSelected => new(async () =>
    {
        NavigationalParameters.SelectedLabourFile = SelectedLabourFile;
        //await NavigateTo(new TimesheetPage());
        await NavigateTo(ViewModelLocator.TimesheetPage);
    });

    public RelayCommand ApproveTimesheet => new(async () =>
    {
        var confirmTimesheets = await Alert("Confirm Approve Timesheet", "Timesheet Approval", "OK", "Cancel");

        if (confirmTimesheets)
            try
            {
                foreach (var lf in GangLabourFiles)
                {
                    //var labourList = _userService.GetAllLabour(lf)
                    //    .Where(x => x.LabourDate == NavigationalParameters.CurrentSelectedJob.JobDate
                    //            && x.ContractReference == NavigationalParameters.CurrentSelectedJob.ContractReference)
                    //            .ToList();

                    lf.ApprovedBySupervisor = DateTime.Parse("2/2/1900 00:00:00");

                    lf.LabourSupervisor = NavigationalParameters.LoggedInUser.FocusId.ToString();

                    var id = _userService.UpdateLabourFile(lf);

                    //if (labourList.Any() && labourList.Count > 1)
                    //    {
                    //        if (labourList.Any(x => x.ApprovedBySupervisor == DateTime.Parse("2/2/1900 00:00:00")))
                    //        {
                    //            foreach (var itemTodelete in labourList)
                    //            {
                    //                if (itemTodelete.ApprovedBySupervisor != DateTime.Parse("2/2/1900 00:00:00"))
                    //                {
                    //                    _userService.DeleteLabourFile(itemTodelete);
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var lb = labourList.FirstOrDefault();
                    //            lb.ApprovedBySupervisor = DateTime.Parse("2/2/1900 00:00:00");

                    //            lb.LabourSupervisor = NavigationalParameters.LoggedInUser.FocusId.ToString();

                    //            var id = _userService.UpdateLabourFile(lb);
                    //        }
                    //    }
                    //    else
                    //    {                         
                    //            var lb = labourList?.FirstOrDefault();

                    //    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
            }
            finally
            {
                NavigationalParameters.PassedInfo = null;

                NavigationalParameters.SelectedTaskItem.LabourApproved = true;

                _jobService.AddApproval(NavigationalParameters.SelectedTaskItem);

                await NavigateTo(ViewModelLocator.TeamOptionsPage);
            }
    });

    public RelayCommand Back => new(async () =>
    {
        if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
            NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
            await NavigateTo(ViewModelLocator.MenuSelectionPage);
        else
            await NavigateTo(ViewModelLocator.TeamOptionsPage);
    });

    public Labour CurrentLabour { get; set; }
}