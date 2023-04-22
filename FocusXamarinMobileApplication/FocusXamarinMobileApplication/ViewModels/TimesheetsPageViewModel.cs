#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class TimesheetsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;
    private readonly Users _userService;
    public RelayCommand<string> _changeTime;
    private RelayCommand<string> _copyToAll;
    private RelayCommand<string> _markCommpassionate;
    private RelayCommand<string> _markHoliday;
    private RelayCommand<string> _markSick;
    private RelayCommand<string> _markUnauthorised;

    private RelayCommand<string> _screenLoaded;

    //======================================================================
    public TimesheetsPageViewModel()
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


    public bool _isEditable { get; set; }

    public bool IsEditable
    {
        get => _isEditable;
        set
        {
            _isEditable = value;
            OnPropertyChanged();
        }
    }

    public bool _isEditable1 { get; set; }

    public bool IsEditable1
    {
        get => _isEditable1;
        set
        {
            _isEditable1 = value;
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

    public Person _selectedOperative { get; private set; }

    public Person SelectedOperative
    {
        get => _selectedOperative;
        set
        {
            _selectedOperative = value;
            OnPropertyChanged();
        }
    }

    public Color _bgColour { get; set; }

    public Color BgColour
    {
        get => _bgColour;
        set
        {
            _bgColour = value;
            OnPropertyChanged();
        }
    }

    //======================================================================
    public TimeSpan _travelToSite { get; set; }

    public TimeSpan TravelToSite
    {
        get => _travelToSite;
        set
        {
            _travelToSite = value;


            SelectedLabourFile.TravelToSite
                = SelectedLabourFile.TravelToSite.Date + _travelToSite;

            if (SelectedLabourFile.LabourType != "W" && _travelToSite > DateTime.Now.Date.TimeOfDay)
                UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);

            OnPropertyChanged();
        }
    }

    public TimeSpan _claimedYardStart { get; set; }

    public TimeSpan ClaimedYardStart
    {
        get => _claimedYardStart;
        set
        {
            _claimedYardStart = value;

            SelectedLabourFile.ClaimedYardStart
                = SelectedLabourFile.ClaimedYardStart.Date + _claimedYardStart;

            if (SelectedLabourFile.LabourType != "W" && _claimedYardStart > DateTime.Now.Date.TimeOfDay
               ) UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);

            OnPropertyChanged();
        }
    }

    public TimeSpan _timeOnSite { get; set; }

    public TimeSpan TimeOnSite
    {
        get => _timeOnSite;
        set
        {
            _timeOnSite = value;

            SelectedLabourFile.TimeOnSite
                = SelectedLabourFile.TimeOnSite.Date + _timeOnSite;

            if (SelectedLabourFile.LabourType != "W" && _timeOnSite > DateTime.Now.Date.TimeOfDay)
                UpdateAbsence("W");
            _userService?.UpdateLabourFile(SelectedLabourFile);

            OnPropertyChanged();
        }
    }

    public TimeSpan _timeLeftSite { get; set; }

    public TimeSpan TimeLeftSite
    {
        get => _timeLeftSite;
        set
        {
            _timeLeftSite = value;

            SelectedLabourFile.TimeLeftSite
                = SelectedLabourFile.TimeLeftSite.Date + _timeLeftSite;

            if (SelectedLabourFile.LabourType != "W" && _timeLeftSite > DateTime.Now.Date.TimeOfDay)
                UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _claimedYardEnd { get; set; }

    public TimeSpan ClaimedYardEnd
    {
        get => _claimedYardEnd;
        set
        {
            _claimedYardEnd = value;

            SelectedLabourFile.ClaimedYardEnd
                = SelectedLabourFile.ClaimedYardEnd.Date + _claimedYardEnd;

            if (SelectedLabourFile.LabourType != "W" && _claimedYardEnd > DateTime.Now.Date.TimeOfDay)
                UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _travelFromSite { get; set; }

    public TimeSpan TravelFromSite
    {
        get => _travelFromSite;
        set
        {
            _travelFromSite = value;

            SelectedLabourFile.TravelFromSite
                = SelectedLabourFile.TravelFromSite.Date + _travelFromSite;

            if (SelectedLabourFile.LabourType != "W" && _travelFromSite > DateTime.Now.Date.TimeOfDay)
                UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    //======================================================================
    public TimeSpan _supervisorStart { get; set; }

    public TimeSpan SupervisorStart
    {
        get => _supervisorStart;
        set
        {
            _supervisorStart = value;

            SelectedLabourFile.SupervisorStart
                = SelectedLabourFile.SupervisorStart.Date + _supervisorStart;

            if (SelectedLabourFile.LabourType != "W" && _supervisorStart > DateTime.Now.Date.TimeOfDay)
                UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _supervisorOnSite { get; set; }

    public TimeSpan SupervisorOnSite
    {
        get => _supervisorOnSite;
        set
        {
            _supervisorOnSite = value;

            SelectedLabourFile.SupervisorOnSite
                = SelectedLabourFile.SupervisorOnSite.Date + _supervisorOnSite;

            if (SelectedLabourFile.LabourType != "W" && _supervisorOnSite > DateTime.Now.Date.TimeOfDay
               ) UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _supervisorYardStart { get; set; }

    public TimeSpan SupervisorYardStart
    {
        get => _supervisorYardStart;
        set
        {
            _supervisorYardStart = value;

            SelectedLabourFile.SupervisorYardStart
                = SelectedLabourFile.SupervisorYardStart.Date + _supervisorYardStart;

            if (SelectedLabourFile.LabourType != "W" &&
                _supervisorYardStart > DateTime.Now.Date.TimeOfDay) UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _supervisorYardEnd { get; set; }

    public TimeSpan SupervisorYardEnd
    {
        get => _supervisorYardEnd;
        set
        {
            _supervisorYardEnd = value;

            SelectedLabourFile.SupervisorYardEnd
                = SelectedLabourFile.SupervisorYardEnd.Date + _supervisorYardEnd;

            if (SelectedLabourFile.LabourType != "W" &&
                _supervisorYardEnd > DateTime.Now.Date.TimeOfDay) UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _supervisorOffSite { get; set; }

    public TimeSpan SupervisorOffSite
    {
        get => _supervisorOffSite;
        set
        {
            _supervisorOffSite = value;

            SelectedLabourFile.SupervisorOffSite
                = SelectedLabourFile.SupervisorOffSite.Date + _supervisorOffSite;

            if (SelectedLabourFile.LabourType != "W" &&
                _supervisorOffSite > DateTime.Now.Date.TimeOfDay) UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public TimeSpan _supervisorFinish { get; set; }

    public TimeSpan SupervisorFinish
    {
        get => _supervisorFinish;
        set
        {
            _supervisorFinish = value;


            SelectedLabourFile.SupervisorFinish
                = SelectedLabourFile.SupervisorFinish.Date + _supervisorFinish;

            if (SelectedLabourFile.LabourType != "W" && _supervisorFinish > DateTime.Now.Date.TimeOfDay
               ) UpdateAbsence("W");

            _userService?.UpdateLabourFile(SelectedLabourFile);
            OnPropertyChanged();
        }
    }

    public RelayCommand<string> ScreenLoaded
    {
        get
        {
            return _screenLoaded ??= new RelayCommand<string>(e =>
            {
                ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
                Date = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";

                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                    NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                {
                    IsEditable = true;
                    IsEditable1 = false;

                    ShowSupervisorControls = false;
                }
                else
                {
                    IsEditable = false;
                    IsEditable1 = false;

                    ShowSupervisorControls = true;
                }

                if (NavigationalParameters.SelectedLabourFile != null)
                    SelectedLabourFile = NavigationalParameters.SelectedLabourFile;

                if (SelectedLabourFile != null)
                {
                    TravelToSite = SelectedLabourFile.TravelToSite.TimeOfDay;
                    ClaimedYardStart = SelectedLabourFile.ClaimedYardStart.TimeOfDay;
                    TimeOnSite = SelectedLabourFile.TimeOnSite.TimeOfDay;
                    TimeLeftSite = SelectedLabourFile.TimeLeftSite.TimeOfDay;
                    ClaimedYardEnd = SelectedLabourFile.ClaimedYardEnd.TimeOfDay;
                    TravelFromSite = SelectedLabourFile.TravelFromSite.TimeOfDay;

                    UpdateAbsence(SelectedLabourFile.LabourType);

                    if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.GANGER ||
                        NavigationalParameters.AppType == NavigationalParameters.AppTypes.YARDMAN)
                    {
                        //ShowSupervisorControls = false;
                        NavigationalParameters.SelectedOperative = SelectedOperative = App.Database.GetItems<Person>()
                            .FirstOrDefault(x => x.FocusId.ToString() == SelectedLabourFile.LabourOperative);
                        BgColour = SelectedLabourFile.bgColour;
                    }
                    else if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                    {
                        //ShowSupervisorControls = true;
                        SupervisorStart =
                            SelectedLabourFile.SupervisorStart == DateTime.Parse("01/01/1900 00:00:00")
                                ? TravelToSite
                                : SelectedLabourFile.SupervisorStart.TimeOfDay;

                        SupervisorOnSite =
                            SelectedLabourFile.SupervisorOnSite ==
                            DateTime.Parse("01/01/1900 00:00:00")
                                ? TimeOnSite
                                : SelectedLabourFile.SupervisorOnSite.TimeOfDay;

                        SupervisorYardStart =
                            SelectedLabourFile.SupervisorYardStart ==
                            DateTime.Parse("01/01/1900 00:00:00")
                                ? ClaimedYardStart
                                : SelectedLabourFile.SupervisorYardStart.TimeOfDay;

                        SupervisorYardEnd =
                            SelectedLabourFile.SupervisorYardEnd ==
                            DateTime.Parse("01/01/1900 00:00:00")
                                ? ClaimedYardEnd
                                : SelectedLabourFile.SupervisorYardEnd.TimeOfDay;

                        SupervisorOffSite =
                            SelectedLabourFile.SupervisorOffSite ==
                            DateTime.Parse("01/01/1900 00:00:00")
                                ? TimeLeftSite
                                : SelectedLabourFile.SupervisorOffSite.TimeOfDay;

                        SupervisorFinish =
                            SelectedLabourFile.SupervisorFinish ==
                            DateTime.Parse("01/01/1900 00:00:00")
                                ? TravelFromSite
                                : SelectedLabourFile.SupervisorFinish.TimeOfDay;
                        ;
                    }
                }
            });
        }
    }

    public RelayCommand<string> MarkUnauthorised
    {
        get { return _markUnauthorised ??= new RelayCommand<string>(e => { UpdateAbsence("U"); }); }
    }

    public RelayCommand<string> MarkSick
    {
        get { return _markSick ??= new RelayCommand<string>(e => { UpdateAbsence("S"); }); }
    }

    public RelayCommand<string> MarkCommpassionate
    {
        get { return _markCommpassionate ??= new RelayCommand<string>(e => { UpdateAbsence("C"); }); }
    }

    public RelayCommand<string> MarkHoliday
    {
        get { return _markHoliday ??= new RelayCommand<string>(e => { UpdateAbsence("H"); }); }
    }

    public RelayCommand<string> CopyToAll
    {
        get
        {
            return _copyToAll ??= new RelayCommand<string>(async e =>
            {
                if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
                {
                    LabourFiles = _jobService.GetJobLabour(NavigationalParameters.SelectedTaskItem).ToList();

                    foreach (var lf in LabourFiles)
                    {
                        lf.SupervisorStart =
                            Convert.ToDateTime(SelectedLabourFile?.SupervisorStart);

                        lf.SupervisorOnSite =
                            Convert.ToDateTime(SelectedLabourFile?.SupervisorOnSite);

                        lf.SupervisorYardStart =
                            Convert.ToDateTime(SelectedLabourFile?.SupervisorYardStart);

                        lf.SupervisorYardEnd =
                            Convert.ToDateTime(SelectedLabourFile?.SupervisorYardEnd);

                        lf.SupervisorOffSite =
                            Convert.ToDateTime(SelectedLabourFile?.SupervisorOffSite);

                        lf.SupervisorFinish =
                            Convert.ToDateTime(SelectedLabourFile?.SupervisorFinish);

                        lf.bgColour = SelectedLabourFile.bgColour;

                        lf.LabourType = SelectedLabourFile?.LabourType;

                        _userService.UpdateLabourFile(lf);
                    }
                }
                else
                {
                    LabourFiles = _jobService.GetJobLabour(NavigationalParameters.CurrentSelectedJob).ToList();

                    foreach (var lf in LabourFiles)
                    {
                        lf.SupervisorFinish = lf.TravelFromSite =
                            Convert.ToDateTime(SelectedLabourFile?.TravelFromSite);

                        lf.SupervisorStart = lf.TravelToSite =
                            Convert.ToDateTime(SelectedLabourFile?.TravelToSite);

                        lf.SupervisorOffSite = lf.TimeLeftSite =
                            Convert.ToDateTime(SelectedLabourFile?.TimeLeftSite);

                        lf.SupervisorOnSite = lf.TimeOnSite =
                            Convert.ToDateTime(SelectedLabourFile.TimeOnSite);

                        lf.SupervisorYardEnd = lf.ClaimedYardEnd =
                            Convert.ToDateTime(SelectedLabourFile?.ClaimedYardEnd);

                        lf.SupervisorYardStart = lf.ClaimedYardStart =
                            Convert.ToDateTime(SelectedLabourFile?.ClaimedYardStart);

                        lf.bgColour = SelectedLabourFile.bgColour;

                        lf.LabourType = SelectedLabourFile?.LabourType;

                        _userService.UpdateLabourFile(lf);
                    }
                }

                //NavigationalParameters.ReturnPage = Locator.TimesheetsViewModelKey;
                await NavigateTo(ViewModelLocator.TimesheetSelectionPage);
            });
        }
    }

    public RelayCommand Back => new(async () =>
    {
        _userService.UpdateLabourFile(SelectedLabourFile);
        //NavigationalParameters.ReturnPage = Locator.TimesheetsViewModelKey;            
        await NavigateTo(ViewModelLocator.TimesheetSelectionPage);
    });

    public RelayCommand<string> SaveChanges
    {
        get
        {
            return _changeTime ??= new RelayCommand<string>(e =>
            {
                _userService.UpdateLabourFile(SelectedLabourFile);
            });
        }
    }


    public void UpdateAbsence(string absenceType)
    {
        SelectedLabourFile.LabourType = absenceType;
        //NavigationalParameters.SelectedLabourFile.LabourType = absenceType;

        switch (SelectedLabourFile?.LabourType?.ToLower())
        {
            case "c":
                SelectedLabourFile.bgColour = Color.Violet;
                UpdateTimes();
                break;
            case "s":
                SelectedLabourFile.bgColour = Color.Red;
                UpdateTimes();
                break;
            case "u":
                SelectedLabourFile.bgColour = Color.Orange;
                UpdateTimes();
                break;
            default:
                SelectedLabourFile.bgColour = Color.White;
                break;
        }

        BgColour = SelectedLabourFile.bgColour;
        _userService.UpdateLabourFile(SelectedLabourFile);
    }


    public void UpdateTimes()
    {
        switch (NavigationalParameters.AppType)
        {
            case NavigationalParameters.AppTypes.YARDMAN:
            case NavigationalParameters.AppTypes.GANGER:
                SelectedLabourFile.SupervisorFinish = SelectedLabourFile.TravelFromSite =
                    Convert.ToDateTime(NavigationalParameters.CurrentSelectedJob.JobDate.ToString());
                TravelFromSite = SelectedLabourFile.TravelFromSite.TimeOfDay;

                SelectedLabourFile.SupervisorStart = SelectedLabourFile.TravelToSite =
                    Convert.ToDateTime(NavigationalParameters.CurrentSelectedJob.JobDate.ToString());
                TravelToSite = SelectedLabourFile.TravelToSite.TimeOfDay;

                SelectedLabourFile.SupervisorOffSite = SelectedLabourFile.TimeLeftSite =
                    Convert.ToDateTime(NavigationalParameters.CurrentSelectedJob.JobDate.ToString());
                TimeLeftSite = SelectedLabourFile.TimeLeftSite.TimeOfDay;

                SelectedLabourFile.SupervisorOnSite = SelectedLabourFile.TimeOnSite =
                    Convert.ToDateTime(NavigationalParameters.CurrentSelectedJob.JobDate.ToString());
                TimeOnSite = SelectedLabourFile.TimeOnSite.TimeOfDay;

                SelectedLabourFile.SupervisorYardEnd = SelectedLabourFile.ClaimedYardEnd =
                    Convert.ToDateTime(NavigationalParameters.CurrentSelectedJob.JobDate.ToString());
                ClaimedYardEnd = SelectedLabourFile.ClaimedYardEnd.TimeOfDay;

                SelectedLabourFile.SupervisorYardStart = SelectedLabourFile.ClaimedYardStart =
                    Convert.ToDateTime(NavigationalParameters.CurrentSelectedJob.JobDate.ToString());
                ClaimedYardStart = SelectedLabourFile.ClaimedYardStart.TimeOfDay;
                break;
            case NavigationalParameters.AppTypes.SUPERVISOR:
                SelectedLabourFile.SupervisorStart =
                    Convert.ToDateTime(NavigationalParameters.SelectedTaskItem.JobDate.ToString());
                SupervisorStart = SelectedLabourFile.SupervisorStart.TimeOfDay;

                SelectedLabourFile.SupervisorOnSite =
                    Convert.ToDateTime(NavigationalParameters.SelectedTaskItem.JobDate.ToString());
                SupervisorOnSite = SelectedLabourFile.SupervisorOnSite.TimeOfDay;

                SelectedLabourFile.SupervisorYardStart =
                    Convert.ToDateTime(NavigationalParameters.SelectedTaskItem.JobDate.ToString());
                SupervisorYardStart = SelectedLabourFile.SupervisorYardStart.TimeOfDay;

                SelectedLabourFile.SupervisorYardEnd =
                    Convert.ToDateTime(NavigationalParameters.SelectedTaskItem.JobDate.ToString());
                SupervisorYardEnd = SelectedLabourFile.SupervisorYardEnd.TimeOfDay;

                SelectedLabourFile.SupervisorOffSite =
                    Convert.ToDateTime(NavigationalParameters.SelectedTaskItem.JobDate.ToString());
                SupervisorOffSite = SelectedLabourFile.SupervisorOffSite.TimeOfDay;

                SelectedLabourFile.SupervisorFinish =
                    Convert.ToDateTime(NavigationalParameters.SelectedTaskItem.JobDate.ToString());
                SupervisorFinish = SelectedLabourFile.SupervisorFinish.TimeOfDay;
                break;
        }
    }
}