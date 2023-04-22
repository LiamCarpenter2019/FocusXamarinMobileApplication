#region

using System;
using System.Collections.Generic;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using MethodTimer;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class AuthorisationPageViewModel : FBaseViewModel
{
    public enum AuthType
    {
        CHANGE_PIN,
        PIN_CONFIRM,
        PIN_AND_SIGNATURE,
        SIGNATURE_ONLY,
        SIGNATURE_EMAIL
    }

    public enum ViewStates
    {
        NOT_SELECTED,
        SELECTED,
        CORRECT_PIN,
        COMPLETE
    }

    public const int MaxPinLength = 4;
    private readonly Assignments _assignmentService;
    private readonly Jobs _jobService;
    private readonly Users _userService;
    private readonly WebApi _webApi;

    // private AuthorisationDetail _authDetail;
    private RelayCommand<object> _screenLoadedCommand4Authorisation;

    private RelayCommand<Person> _tableCellSelected;

    public AuthorisationPageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        _webApi = new WebApi();
    }

    public string AuthorisationName { get; set; }
    public string AuthorisationEmail { get; set; }


    public object NavPassedInfo { get; set; }

    public List<Person> GangMembers { get; set; }

    public byte[] Signature { get; set; }

    public JobData4Tablet JobData { get; set; }
    public ViewStates CurrentViewState { get; set; }
    public AuthType AuthorisationType { get; set; }

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


    //Use Dictionary AuthDict to set the Authtype
    public RelayCommand<object> ScreenLoadedCommand4Authorisation
    {
        get
        {
            return _screenLoadedCommand4Authorisation ??= new RelayCommand<object>(e =>
            {
                NavPassedInfo = e;
                SetPageType(NavPassedInfo);
                ProjectDate = $"{DateTime.Now.Date:dd/MM/yyyy}";
            });
        }
    }

    public RelayCommand<Person> TableCellSelected
    {
        get
        {
            return _tableCellSelected ??= new RelayCommand<Person>(e =>
            {
                NavigationalParameters.SelectedUser = e;

                PersonSelected();
                ViewChanged.Invoke(e, null);
            });
        }
    }

    public event EventHandler ViewChanged;

    [Time]
    public async void SetPageType(object navParameters)
    {
        //var gang = new Jobs().GetCurrentSelectedGang();
        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE)
        {
            LoadPage_InvestigationSignature(NavigationalParameters.LoggedInUser, new AuthorisationDetail());
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.CHANGE_PIN)
        {
            //LoadPage_ChangePin(person);
            LoadPage_ChangePin(_userService.GetAllUsers());
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN
                 || NavigationalParameters.AuthorisationType ==
                 NavigationalParameters.AuthorisationTypes.SUPERVISOR_PIN)
        {
            LoadPage_ConfirmPin(NavigationalParameters.CurrentSelectedJob, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG
                 || NavigationalParameters.AuthorisationType ==
                 NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG)
        {
            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob,
                NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.SIGNATURE_ONLY)
        {
            LoadPage_SignatureOnly(NavigationalParameters.SelectedUser, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL)
        {
            LoadPage_SignatureEmailOnly(NavigationalParameters.SelectedUser, NavigationalParameters.AuthDetail);
        }
        //se if (NavigationalParameters.AutenticationType == NavigationalParameters.AuthorisationTypes.pin) { LoadPage_SignatureEmailOnly(NavigationalParameters.SelecterdUser, NavigationalParameters.AuthDetail); }
        else
        {
            await Alert("Incorrect Data Type Passed", "Nav Property Error");
            GoBack();
        }

        CurrentViewState = ViewStates.NOT_SELECTED;
    }

    private void LoadPage_SignatureEmailOnly(Person person, AuthorisationDetail sigDetails)
    {
        if (string.IsNullOrEmpty(person.CompanyName)) person.FocusId = 0;

        var supervisor =
            _userService.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob.SupervisorId));
        GangMembers = new List<Person> { person, supervisor };
        AuthorisationType = AuthType.SIGNATURE_EMAIL;
        NavigationalParameters.AuthDetail = sigDetails;
    }

    //For changing the pin
    public void LoadPage_ChangePin(Person person)
    {
        AuthorisationType = AuthType.CHANGE_PIN;
        GangMembers = new List<Person> { person };
    }

    //For changing the pin
    public void LoadPage_ChangePin(Gang gang)
    {
        AuthorisationType = AuthType.CHANGE_PIN;
        GangMembers = gang.GangLabourFiles;
    }

    //For changing the pin
    public void LoadPage_ChangePin(List<Person> gang)
    {
        AuthorisationType = AuthType.CHANGE_PIN;
        GangMembers = gang;
    }

    //For authentication TODO Need to check for Supervisor or Gang
    public void LoadPage_ConfirmPin(JobData4Tablet jobData, AuthorisationDetail auth)
    {
        AuthorisationType = AuthType.PIN_CONFIRM;
        GangMembers = NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles;
        JobData = NavigationalParameters.CurrentSelectedJob;
        // NavigationalParameters.AuthDetail = auth;
    }

    //For authentication with signature
    public void LoadPage_InvestigationSignature(Person person, AuthorisationDetail sigDetails)
    {
        AuthorisationType = AuthType.PIN_AND_SIGNATURE;

        var sup = new Person { FullName = "NOT_FOUND" };
        sup.FullName = $"{NavigationalParameters.SelectedUser.FirstName} {NavigationalParameters.SelectedUser.Surname}";
        sup.MemberPin = NavigationalParameters.SelectedUser.MemberPin;
        sup.FocusId = (int)NavigationalParameters.SelectedUser.FocusId;

        GangMembers = new List<Person> { sup };

        //NavigationalParameters.AuthDetail = sigDetails;
    }

    //For authentication with signature
    public void LoadPage_Signature(Person person, JobData4Tablet jobData, AuthorisationDetail sigDetails)
    {
        AuthorisationType = AuthType.PIN_AND_SIGNATURE;
        if (NavigationalParameters.AuthDetail.Type == NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG)
        {
            GangMembers = NavigationalParameters.CurrentSelectedJob.JobGang == null
                ? _jobService.GetGang(NavigationalParameters.CurrentSelectedJob).GangLabourFiles
                : NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles;
        }
        else
        {
            //Look for supervisor in GangLabourFiles 
            var sup = new Person { FullName = "NOT_FOUND" };

            foreach (var p in NavigationalParameters.CurrentSelectedJob.JobGang.GangLabourFiles)
                if (NavigationalParameters.CurrentSelectedJob.SupervisorId == p.FocusId)
                    sup = p;

            //Look for supervisor in GangMembers add to person if found.
            if (NavigationalParameters.CurrentSelectedJob.SupervisorId ==
                NavigationalParameters.CurrentSelectedJob.JobGang.SupervisorId)
            {
                sup.FullName =
                    $"{NavigationalParameters.CurrentSelectedJob.JobGang.SupervisorFirstName} {NavigationalParameters.CurrentSelectedJob.JobGang.SupervisorSurname}";
                sup.MemberPin = NavigationalParameters.CurrentSelectedJob.JobGang.SupervisorPin;
                sup.FocusId = (int)NavigationalParameters.CurrentSelectedJob.JobGang.SupervisorId;
            }

            GangMembers = new List<Person> { sup };
        }

        //  NavigationalParameters.AuthDetail = sigDetails;
        //JobData = new JobData4Tablet();
    }

    public void LoadPage_SignatureOnly(Person person, AuthorisationDetail sigDetails)
    {
        GangMembers = new List<Person> { NavigationalParameters.SelectedUser };
        AuthorisationType = AuthType.SIGNATURE_ONLY;
        //    NavigationalParameters.AuthDetail = sigDetails;
    }

    public async void GoBack()
    {
        NavigateBack();
    }

    public bool ChangePin(string newPin, string confirmPin)
    {
        if (confirmPin.Length == MaxPinLength && newPin.Length == MaxPinLength)
            if (newPin == confirmPin)
            {
                NavigationalParameters.SelectedUser.MemberPin = newPin;
                Alert("Pin Changed!", "Authentication");
                UpdateUserPin(NavigationalParameters.SelectedUser);
                return true;
            }

        return false;
    }

    private void UpdateUserPin(Person user)
    {
        _userService.AddUser(user);
        _webApi.UpdatePin(user);
    }

    private bool PersonSelected()
    {
        CurrentViewState = ViewStates.SELECTED;
        return true;
    }

    //checks the pin against the selected user.
    public bool CheckPin(string pin)
    {
        if (pin == NavigationalParameters.SelectedUser.MemberPin && pin.Length == MaxPinLength)
        {
            CurrentViewState = ViewStates.CORRECT_PIN;
            ViewChanged.Invoke(null, null);
            return true;
        }

        Alert("Incorrect Pin", "Authentication");
        return false;
    }

    //checks the pin against the other
    public bool CheckPin(string pin1, string pin2)
    {
        if (pin1.Length == MaxPinLength && pin2.Length == MaxPinLength && pin1 == pin2 ? true : false)
        {
            if (pin1 == "0000" || pin1 == "1234")
            {
                Alert("Pin complexity error, cannot have 0000 or 1234", "Authentication");
                return false;
            }

            CurrentViewState = ViewStates.COMPLETE;
            ViewChanged.Invoke(null, null);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Advanced check for pin, denys either repetition or predictable patterns.
    /// </summary>
    /// <returns><c>true</c>, if pin complexity was checked, <c>false</c> otherwise.</returns>
    /// <param name="pin">Pin.</param>
    public bool CheckPinComplexity(string pin)
    {
        //check for duplicate entries.
        var duplicates = 0;
        var intList = new List<int>();
        for (var i = 0; i < pin.Length; i++)
        {
            if (pin[0] == pin[i]) duplicates++;
            intList.Add(pin[i]);
        }

        if (duplicates == pin.Length) return false;

        //check for sequences.
        var predictUp = "";
        var predictDown = "";

        var start = int.Parse(pin[0].ToString());
        for (var i = start; i < start + pin.Length; i++) predictUp += i.ToString();
        for (var i = start; i > start - pin.Length; i--) predictDown += i.ToString();

        if (pin == predictUp || pin == predictDown) return false;

        return false;
    }

    //checks the pin against the selected user.                                                                                                                                                                                        
    public bool ConfirmPin(string pin)
    {
        if (pin == NavigationalParameters.SelectedUser.MemberPin)
        {
            ViewChanged.Invoke(null, null);
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Submit the specified pin.
    /// </summary>
    /// <param name="pin">Pin.</param>
    public async void Submit(string pin)
    {
        if (ConfirmPin(pin))
        {
            NavigationalParameters.AuthDetail.AuthorisedSig = true;
            NavigationalParameters.AuthDetail.AuthorisedName = NavigationalParameters.SelectedUser.FullName;
            GoBack();
        }
        else
        {
            await Alert("Authentication Failed", "User Authentication");
        }
    }

    /// <summary>
    ///     Submit the specified pin and signature.
    /// </summary>
    /// <param name="pin">string</param>
    /// <param name="signature">byte[]</param>
    public async void Submit(string pin, byte[] signature)
    {
        if (signature == null || signature.Length <= 0)
        {
            await Alert("Signature missing", "User Authentication");
        }
        else if (ConfirmPin(pin))
        {
            NavigationalParameters.AuthDetail.SignatureImg = signature;
            NavigationalParameters.AuthDetail.FocusId = NavigationalParameters.SelectedUser.FocusId;
            NavigationalParameters.AuthDetail.AuthorisedSig = true;
            NavigationalParameters.AuthDetail.Date = DateTime.Now;
            NavigationalParameters.AuthDetail.SignedBy = NavigationalParameters.SelectedUser?.FullName;
            NavigationalParameters.AuthDetail.AuthorisedName = NavigationalParameters.SelectedUser?.FullName;
            SaveSignature(NavigationalParameters.AuthDetail, JobData);
        }
        else
        {
            await Alert("Authentication Failed", "User Authentication");
        }
    }

    public async void Submit(byte[] signature)
    {
        if (signature == null || signature.Length <= 0)
        {
            await Alert("Signature missing", "User Authentication");
        }
        else
        {
            NavigationalParameters.AuthDetail.SignatureImg = signature;
            NavigationalParameters.AuthDetail.FocusId = NavigationalParameters.SelectedUser.FocusId;
            NavigationalParameters.AuthDetail.AuthorisedSig = true;
            NavigationalParameters.AuthDetail.Date = DateTime.Now;
            NavigationalParameters.AuthDetail.SignedBy = NavigationalParameters.SelectedUser?.FullName;
            NavigationalParameters.AuthDetail.AuthorisedName =
                AuthorisationName ?? NavigationalParameters.SelectedUser?.FullName;
            NavigationalParameters.AuthDetail.Email = AuthorisationEmail;

            // if (SimpleStaticHelpers.IsValidEmail(NavigationalParameters.AuthDetail.Email))
            // {
            SaveSignature(NavigationalParameters.AuthDetail, JobData);
            //  }
            //else
            //{
            //   await Alert("Email is invalid please check and try again", "Invalid Email");
            // }
        }
    }

    private async void SaveSignature(AuthorisationDetail sig, JobData4Tablet jobdata)
    {
        var guid = Guid.NewGuid();


        var filename =
            $"{DateTime.Now:hhmmss}_{DateTime.Now:ddMMyyyy}_{guid}_{sig.Description}_{sig.FocusId}_{jobdata?.QuoteNumber}.jpg";

        if (NavigationalParameters.CurrentAudit != null)
        {
            NavigationalParameters.CurrentAudit.AuditeeSignature = sig.Type.ToString() == "OPERATIVE_SIG"
                ? filename
                : NavigationalParameters.CurrentAudit?.AuditeeSignature;
            NavigationalParameters.CurrentAudit.AuditorSignature = sig.Type.ToString() == "SIGNATURE_EMAIL"
                ? filename
                : NavigationalParameters.CurrentAudit?.AuditorSignature;
            await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
        }

        await sig.StoreSignature(sig.SignatureImg, filename);

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE)
        {
            var investigationId = Convert.ToInt32(NavigationalParameters.InvestigationIdToFinalize);
            NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault(x =>
                    x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize).SignatureInfo =
                new DamageSignature
                {
                    PublicUtilityDamageID = NavigationalParameters.InvestigateDamage.DamageDetails
                        .FirstOrDefault(x => x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize)
                        ?.CurrentInvestigationStatus,

                    SignatureType = "Investigator",
                    Filename = filename,
                    SignatureDate = sig.Date,
                    InvestigationId = investigationId,
                    UserId = NavigationalParameters.LoggedInUser.FocusId
                };

            try
            {
                var uploadedSucessfully =
                    await _jobService.UploadUtilityInvestigations(
                        NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault(x =>
                            x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize), "Partial");

                if (uploadedSucessfully)
                {
                    NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault(x =>
                        x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize).SavedToServer = true;

                    NavigationalParameters.InvestigateDamage.DamageDetails
                        .FirstOrDefault(x => x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize)
                        .DamageInvestigated.IsUpdatedToServer = true;

                    NavigationalParameters.InvestigateDamage.DamageDetails
                        .FirstOrDefault(x => x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize)
                        .CurrentInvestigationStatus = "Requires Sign Off";

                    await _jobService.SaveInvestigateDamage(
                        NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault(x =>
                            x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize));

                    await _jobService.SaveCurrentInvestigation(NavigationalParameters.InvestigateDamage.DamageDetails
                        .FirstOrDefault(x => x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize)
                        .DamageInvestigated);

                    await _jobService.UpdateInvestigationDetails(
                        NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault(x =>
                            x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize));
                }

                //    NavigationalParameters.NavigationService = _navigationService;
                NavigationalParameters.AppMode = NavigationalParameters.AppModes.EVENTMANAGEMENT;
                NavigationalParameters.UtilityDamage =
                    NavigationalParameters.InvestigateDamage.DamageDetails.FirstOrDefault(x =>
                        x.InvestigationId == NavigationalParameters.InvestigationIdToFinalize);

                //await NavigateTo(ViewModelLocator.InvestigateDamagePage);
                await NavigateTo(ViewModelLocator.InvestigateDamagePage);
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                await Alert("An issue has occured saving the investigation. Please try again",
                    "Error!");
            }
        }

        GoBack();
    }
}