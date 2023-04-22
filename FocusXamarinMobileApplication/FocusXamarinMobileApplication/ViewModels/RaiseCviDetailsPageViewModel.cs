#region

using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class RaiseCviDetailsPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public readonly Assignments _assignmentService;
    public readonly Jobs _jobService;
    public readonly Users _userService;

    private Cvi _currentCvi;

    private string _gangLeaderName = "Team Leader";


    public RaiseCviDetailsPageViewModel()
    {
        _assignmentService = new Assignments();
        _userService = new Users();
        _jobService = new Jobs();
    }


    public string _projectDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

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

    public bool _actionA { get; set; } = true;

    public bool ActionA
    {
        get => _actionA;
        set
        {
            _actionA = value;
            if (_actionA && CurrentCvi != null)
                CurrentCvi.Action =
                    "We are making necessary arrangements to carry out this work and in the absence of written instruction, this confirmation of your request will be deemed to have the same validity as your written instruction under the contract.";

            OnPropertyChanged();
        }
    }

    public bool _actionB { get; set; }

    public bool ActionB
    {
        get => _actionB;
        set
        {
            _actionB = value;
            if (_actionB && CurrentCvi != null)
                CurrentCvi.Action = "We will not proceed with this work until a formal written instruction is recieved";

            OnPropertyChanged();
        }
    }

    public bool _relatedToBool { get; set; }

    public bool RelatedToBool
    {
        get => _relatedToBool;
        set
        {
            _relatedToBool = value;


            OnPropertyChanged();
        }
    }

    public string GangLeaderName
    {
        get => _gangLeaderName;
        set
        {
            _gangLeaderName = value;
            OnPropertyChanged();
        }
    }

    public Cvi CurrentCvi
    {
        get => _currentCvi;
        set
        {
            _currentCvi = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = DateTime.Now.Date.ToString("dd/MM/yyyy");

        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;

        CurrentCvi = NavigationalParameters.CurrentCvi;

        if (NavigationalParameters.CurrentSelectedJob?.JobGang != null)
        {
            GangLeaderName = NavigationalParameters.CurrentSelectedJob?.JobGang.GangLabourFiles
                ?.FirstOrDefault(x => x.FocusId == NavigationalParameters.CurrentSelectedJob.GangLeaderId)?.FullName;
        }
        else
        {
            var tu = _userService.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.GangLeaderId));
            GangLeaderName = tu?.FullName;
        }
    });

    public RelayCommand Cancel => new(async () =>
    {
        var confirmCancel = await Alert("Do you want to delete the entire cvi record", "Warnining", "Yes", "No");

        if (confirmCancel)
        {
            _assignmentService.DeleteCvi(NavigationalParameters.CurrentAssignment);

            NavigationalParameters.CurrentAssignment = null;

            NavigationalParameters.CurrentCvi = null;

            NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGSELECTION;

            await NavigateTo(ViewModelLocator.GangSelectPage);
            ;
        }
    });

    public RelayCommand Submit => new(async () =>
    {
        var valid = await ValidateEntriesAsync();

        if (valid)
        {
            var confirmSubmit =
                await Alert(
                    "Please confirm all details are correct before procceding to the signature screen as as the information submitted cannot be updated after.",
                    "Confirmation", "Confirm", "Cancel");

            if (confirmSubmit)
                try
                {
                    if (RelatedToBool == false)
                        CurrentCvi.RelatedTo = "Scd Group Ltd";
                    else
                        CurrentCvi.RelatedTo = GangLeaderName;

                    NavigationalParameters.CurrentCvi = CurrentCvi;

                    NavigationalParameters.CurrentAssignment.Cvi.Clear();

                    NavigationalParameters.CurrentAssignment.Cvi.Add(NavigationalParameters.CurrentCvi);

                    await _assignmentService.SaveCvi(NavigationalParameters.CurrentAssignment);
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                }
                finally
                {
                    NavigationalParameters.AuthorisationType =
                        NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

                    await NavigateTo(ViewModelLocator.SignaturePage);
                }
        }
        else
        {
            await Alert("Please complete all fields before submitting", "Incomplete", "Ok");
        }
    });

    public RelayCommand Back => new(async () => { NavigateBack(); });

    public void UpdateActions(string action)
    {
        if (action == "ActionA")
        {
            if (ActionA)
            {
                ActionA = false;
                ActionB = true;
            }
        }
        else
        {
            if (ActionB)
            {
                ActionA = true;
                ActionB = false;
            }
        }
    }

    private async Task<bool> ValidateEntriesAsync()
    {
        if (CurrentCvi.ScheduleOfRates == false
            && CurrentCvi.DayWorkRates == false
            && CurrentCvi.SpecifiedPrice == false
            && CurrentCvi.PriceToBeAgreed == false
            && CurrentCvi.Prolongation == false)
        {
            await Alert("At least 1 Selection in the Charged at secdtion must be made", "Error", "Ok");

            return false;
        }

        if (ActionA && ActionB)
        {
            await Alert(
                "You can only select a single instruction from the second section, Please de-select an option to continue",
                "Error", "Ok");

            return false;
        }

        var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";

        if (string.IsNullOrEmpty(CurrentCvi?.Email) || !Regex.IsMatch(CurrentCvi?.Email, emailPattern))
        {
            await Alert("Invalid email", "The client email is not a valid email address pleasse check and try again",
                "Ok");

            return false;
        }

        return true;
    }
}