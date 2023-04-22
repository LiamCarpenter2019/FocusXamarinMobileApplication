#region

using System.Text.RegularExpressions;
using Event = FocusXamarinMobileApplication.Models.Event;
using Location = Xamarin.Essentials.Location;
using Person = FocusXamarinMobileApplication.Models.Person;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class SignaturePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public const int MaxPinLength = 4;

    private readonly Assignments _assignmentService;
    private readonly Checks _checkService;
    private readonly Jobs _jobService;
    private readonly Pictures _pictureService;
    private readonly Users _userService;
    private readonly WebApi _webApi;


    public SignaturePageViewModel()
    {
        _assignmentService = new Assignments();
        _jobService = new Jobs();
        _userService = new Users();
        _webApi = new WebApi();
        _pictureService = new Pictures();
        _checkService = new Checks();
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

    public Location location { get; set; }
    public PermissionStatus lStatus { get; set; }
    public string filename { get; private set; }


    public string _signatureMessage { get; set; }

    public string SignatureMessage
    {
        get => _signatureMessage;
        set
        {
            _signatureMessage = value;
            OnPropertyChanged("Title");
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

    public string _name { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public string _email { get; set; }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public string _pin { get; set; }

    public string Pin
    {
        get => _pin;
        set
        {
            _pin = value;


            OnPropertyChanged("Pin");
        }
    }

    public string _newPin { get; set; }

    public string NewPin
    {
        get => _newPin;
        set
        {
            _newPin = value;


            OnPropertyChanged("NewPin");
        }
    }

    public string _confirmedPin { get; set; }

    public string ConfirmedPin
    {
        get => _confirmedPin;
        set
        {
            _confirmedPin = value;


            OnPropertyChanged("ConfirmedPin");
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

    public bool _showPin { get; set; }

    public bool ShowPin
    {
        get => _showPin;
        set
        {
            _showPin = value;
            OnPropertyChanged("ShowPin");
        }
    }

    public bool _showNewPin { get; set; }

    public bool ShowNewPin
    {
        get => _showNewPin;
        set
        {
            _showNewPin = value;
            OnPropertyChanged("ShowNewPin");
        }
    }

    public bool _showConfirmPin { get; set; }

    public bool ShowConfirmPin
    {
        get => _showConfirmPin;
        set
        {
            _showConfirmPin = value;
            OnPropertyChanged("ShowConfirmPin");
        }
    }

    public bool _showName { get; set; }

    public bool ShowName
    {
        get => _showName;
        set
        {
            _showName = value;
            OnPropertyChanged();
        }
    }


    public bool _showEmail { get; set; }

    public bool ShowEmail
    {
        get => _showEmail;
        set
        {
            _showEmail = value;
            OnPropertyChanged();
        }
    }

    public bool _showPermit { get; set; }

    public bool ShowPermit
    {
        get => _showPermit;
        set
        {
            _showPermit = value;
            OnPropertyChanged();
        }
    }

    public Stream _Signature { get; set; }

    public Stream Signature
    {
        get => _Signature;
        set
        {
            _Signature = value;
            OnPropertyChanged();
        }
    }

    public Person _selectedUser { get; set; }

    public Person SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;

            OnPropertyChanged();
        }
    }

    public Person _supervisor { get; set; }

    public Person Supervisor
    {
        get => _supervisor;
        set
        {
            _supervisor = value;

            OnPropertyChanged();
        }
    }

    public Person _gangLeader { get; set; }

    public Person GangLeader
    {
        get => _gangLeader;
        set
        {
            _gangLeader = value;

            OnPropertyChanged();
        }
    }

    public ObservableCollection<Person> _userList { get; set; } = new();

    public ObservableCollection<Person> UserList
    {
        get => _userList;
        set
        {
            _userList = value;
            OnPropertyChanged();
        }
    }

    public string _permitStatus { get; set; }

    public string PermitStatus
    {
        get => _permitStatus;
        set
        {
            _permitStatus = value;
            OnPropertyChanged();
        }
    }

    public Color _permitColour { get; set; } = Color.Green;

    public Color PermitColour
    {
        get => _permitColour;
        set
        {
            _permitColour = value;
            OnPropertyChanged();
        }
    }

    public bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;

            OnPropertyChanged("IsLoading");
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        Pin = "";
        IsLoading = false;
       
        UserList = new ObservableCollection<Person>();

        if (NavigationalParameters.CurrentSelectedJob != null)
        {
            Supervisor =
                _userService.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob?.SupervisorId));
            Supervisor.BackgroundHighlighted = Color.Transparent;
            GangLeader =
                _userService.GetUserById(Convert.ToInt32(NavigationalParameters.CurrentSelectedJob?.GangLeaderId));
            GangLeader.BackgroundHighlighted = Color.Transparent;
        }

        if (NavigationalParameters.SelectedUser != null)
            NavigationalParameters.SelectedUser.BackgroundHighlighted = Color.Transparent;

        if (NavigationalParameters.MultSignatures == null) NavigationalParameters.MultSignatures = new List<Person>();

        if (NavigationalParameters.CurrentSelectedJob != null)
        {
            ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
            ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");
        }

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS)
        {
            Title = "Training Documents";
            SignatureMessage = "Please select a operative";
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN;
        }
        else
        {
            Title = "Authorisation";
            SignatureMessage = "";
        }

        NavigationalParameters.SelectedUser = SelectedUser ?? NavigationalParameters.LoggedInUser;
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = false;
        ShowSection4 = true;
        ShowNewPin = false;
        ShowConfirmPin = false;
        ShowPermit = false;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE)
        {
            SignatureMessage = "Please select the investigator";
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowSection3 = true;
            ShowEmail = false;
            ShowName = false;
            LoadPage_InvestigationSignature(NavigationalParameters.LoggedInUser, new AuthorisationDetail());
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PRESITE)
        {
            SignatureMessage = "Please select the surveyor";
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowSection3 = true;
            ShowEmail = false;
            ShowName = false;
            //LoadPage_ChangePin(person);
            LoadPage_Signature(NavigationalParameters.LoggedInUser, NavigationalParameters.CurrentSelectedJob,
                NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITECLEAR)
        {
            SignatureMessage = "Please select a operative";
            ShowPermit = true;
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowSection3 = true;
            ShowEmail = false;
            ShowName = false;
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
            NavigationalParameters.AuthDetail = new AuthorisationDetail
            {
                Type = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG
            };

            if (NavigationalParameters.CurrentPermit.Granted)
            {
                PermitColour = Color.Green;
                PermitStatus = "Site cleared";
            }
            else
            {
                PermitColour = Color.Red;
                PermitStatus = "Site not cleared";
            }

            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob);
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PERMITTODIG)
        {
            SignatureMessage = "Please ensure both gang member and authoriser sign";
            ShowPermit = true;

            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowSection3 = true;
            ShowEmail = false;
            ShowName = false;
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
            NavigationalParameters.AuthDetail = new AuthorisationDetail
            {
                Type = NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG
            };

            if (NavigationalParameters.CurrentPermit.Granted)
            {
                PermitColour = Color.Green;
                PermitStatus = "Permit granted";
            }
            else
            {
                PermitColour = Color.Red;
                PermitStatus = "Permit denied";
            }

            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob);
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION
                 || NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
        {
            SignatureMessage = NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION
                ? "Please ensure both gang member and Inspector sign"
                : "Please ensure both the gang member and client sign where applicable";
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowSection3 = true;
            ShowEmail = false;
            ShowName = false;

            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

            NavigationalParameters.AuthDetail = new AuthorisationDetail
            {
                Type = NavigationalParameters.AuthorisationType
            };

            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob);
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.CHANGE_PIN)
        {
            Title = "Change Pin";
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            //LoadPage_ChangePin(person);
            LoadPage_ChangePin(_userService.GetAllUsers());
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN
                 || NavigationalParameters.AuthorisationType ==
                 NavigationalParameters.AuthorisationTypes.SUPERVISOR_PIN)
        {
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = false;
            LoadPage_ConfirmPin(NavigationalParameters.CurrentSelectedJob, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG ||
                 NavigationalParameters.AuthorisationType ==
                 NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG)
        {
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = true;
            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob,
                NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType == NavigationalParameters.AuthorisationTypes.SIGNATURE_ONLY)
        {
            ShowPin = false;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = true;
            LoadPage_SignatureOnly(NavigationalParameters.SelectedUser, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType ==
                 NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL)
        {
            ShowPin = false;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = true;
            ShowName = true;
            ShowSection3 = true;
            LoadPage_SignatureEmailOnly(NavigationalParameters.SelectedUser, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.AuthorisationType ==
                 NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE)
        {
            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = true;
            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob,
                NavigationalParameters.AuthDetail);
        }
    });


    public RelayCommand UserSelected => new(async () =>
    {
        Pin = "";
        NewPin = "";
        ConfirmedPin = "";

        foreach (var user in UserList) user.BackgroundHighlighted = Color.Transparent;

        NavigationalParameters.SelectedUser = SelectedUser;
        NavigationalParameters.SelectedUser.BackgroundHighlighted = Color.Orange;

        UserList = new ObservableCollection<Person>(UserList);

        if (NavigationalParameters.SelectedUser.FullName == "Client Signature"
            && NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
        {
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.SIGNATURE_ONLY;

            ShowPin = false;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = true;

            LoadPage_SignatureEmailOnly(NavigationalParameters.SelectedUser, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.SelectedUser.FullName != "Client Signature"
                 && NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
        {
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = true;

            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob);
        }
        else if (NavigationalParameters.SelectedUser.FullName == "Auditor Signature"
                 && NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
        {
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL;

            ShowPin = false;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = true;
            ShowName = true;
            ShowSection3 = true;

            LoadPage_SignatureEmailOnly(NavigationalParameters.SelectedUser, NavigationalParameters.AuthDetail);
        }
        else if (NavigationalParameters.SelectedUser.FullName != "Auditor Signature" && NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
        {
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

            ShowPin = true;
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowEmail = false;
            ShowName = false;
            ShowSection3 = true;
            LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob);
        }
        //else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT)
        //{
        //    NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

        //    ShowPin = true;
        //    ShowNewPin = false;
        //    ShowConfirmPin = false;
        //    ShowEmail = false;
        //    ShowName = false;
        //    ShowSection3 = false;
        //    LoadPage_Signature(NavigationalParameters.SelectedUser, NavigationalParameters.CurrentSelectedJob);
        //}
        else
        {        
                ShowNewPin = false;
                ShowConfirmPin = false;
                ShowPin = true;  
        }
    });

    public RelayCommand Back => new(async () =>
    {
        var itemToRemove =
            NavigationalParameters.MultSignatures.FirstOrDefault(x =>
                x.FocusId == NavigationalParameters.SelectedUser.FocusId);

        NavigationalParameters.MultSignatures.Remove(itemToRemove);

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
            await NavigateTo(ViewModelLocator.SiteInspectionResultPage);
        else
            NavigateBack();
    });

    public RelayCommand Save => new(async () =>
    {
        var newPin = SimpleStaticHelpers.Reverse(NewPin);

        if ((!string.IsNullOrEmpty(NewPin) && NewPin.ToString() == newPin || NewPin == "1234" || NewPin == "4321"))
        {
            await Alert("Invalid Pin", "Please do not attempt to use any of the default passwords 1234 or 4321 as these will be rejected. !Please do not attempt to use any 4 identical charachters!");           
        }
        else
        {
            //if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT)
            //{
            //    NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN;
            //}

            if ((Pin == NavigationalParameters.SelectedUser?.MemberPin && Pin?.Length == MaxPinLength)
                || NavigationalParameters.SelectedUser?.FullName == "Client Signature"
                || NavigationalParameters.SelectedUser?.FullName == "Auditor Signature")
            {
                switch (NavigationalParameters.AuthorisationType)
                {
                    case NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN:
                    case NavigationalParameters.AuthorisationTypes.SUPERVISOR_PIN:
                        Submit(Pin);
                        break;
                    case NavigationalParameters.AuthorisationTypes.CHANGE_PIN:

                        ShowNewPin = true;

                        ShowPin = false;

                        if (!string.IsNullOrEmpty(NewPin) && NewPin.Count() == 4)
                        {
                            ShowNewPin = false;

                            ShowConfirmPin = true;

                            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_CONFIRM;
                        }
                        else
                        {
                            await Alert("Pin Required", "Please ensure that the pin is only 4 characters long as it will not be accepted if not!");
                        }
                        break;
                    case NavigationalParameters.AuthorisationTypes.PIN_CONFIRM:
                        await ChangePinAsync(NewPin, ConfirmedPin);
                        NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.CHANGE_PIN;
                        break;
                    case NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE:
                    case NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG:
                    case NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG:
                        if (Signature == null)
                            await Alert("Signature Required", "User Authentication");
                        else
                            using (var memoryStream = new MemoryStream())
                            {
                                Signature.CopyTo(memoryStream);
                                var signature = memoryStream.ToArray();
                                Submit(Pin, signature);
                            }

                        break;
                    case NavigationalParameters.AuthorisationTypes.SIGNATURE_ONLY:
                        using (var memoryStream = new MemoryStream())
                        {
                            Signature.CopyTo(memoryStream);
                            var signature = memoryStream.ToArray();
                            Submit(signature);
                        }

                        break;
                    case NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL:
                        if (NavigationalParameters.CurrentAudit != null)
                        {
                            NavigationalParameters.CurrentAudit.ConductedBy = Name;

                            NavigationalParameters.CurrentAudit.AuditorEmail = Email;
                            await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            Signature.CopyTo(memoryStream);
                            var signature = memoryStream.ToArray();
                            Submit(signature);
                        }

                        break;
                }
            }
            else if (!string.IsNullOrEmpty(Pin) && Pin != NavigationalParameters.SelectedUser.MemberPin &&
                     Pin?.Length == MaxPinLength)
            {
                ShowNewPin = false;
                ShowConfirmPin = false;
                ShowPin = true;

                await Alert("Incorrect Pin", "Authentication");
            }
            else
            {
                if (NavigationalParameters.MultSignatures.Any(x => x.Signature == ""))
                {
                    var ok = await Alert(
                        "Warning!! Not all members have signed to confim they are fully aware and understand this document! Only proceed if you are happy to procede and add noptes to your diary! Any issue please contact support before proceeding!",
                        "All gang members should sign!", "Confirm",
                        "Cancel");
                    if (ok)
                    {


                        switch (NavigationalParameters.AuthorisationType)
                        {
                            case NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN:
                            case NavigationalParameters.AuthorisationTypes.SUPERVISOR_PIN:
                                Submit(Pin);
                                break;
                            case NavigationalParameters.AuthorisationTypes.CHANGE_PIN:
                                Submit(Pin);
                                break;
                            case NavigationalParameters.AuthorisationTypes.PIN_CONFIRM:
                                await ChangePinAsync(NewPin, ConfirmedPin);
                                break;
                            case NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE:
                            case NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG:
                            case NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG:
                                if (Signature == null)
                                    await Alert("Signature Required", "User Authentication");
                                else
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        Signature.CopyTo(memoryStream);
                                        var signature = memoryStream.ToArray();
                                        Submit(Pin, signature);
                                    }

                                break;
                            case NavigationalParameters.AuthorisationTypes.SIGNATURE_ONLY:
                                using (var memoryStream = new MemoryStream())
                                {
                                    Signature.CopyTo(memoryStream);
                                    var signature = memoryStream.ToArray();
                                    Submit(signature);
                                }

                                break;
                            case NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL:
                                if (NavigationalParameters.CurrentAudit != null)
                                {
                                    NavigationalParameters.CurrentAudit.ConductedBy = Name;

                                    NavigationalParameters.CurrentAudit.AuditorEmail = Email;
                                    await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
                                }

                                using (var memoryStream = new MemoryStream())
                                {
                                    Signature.CopyTo(memoryStream);
                                    var signature = memoryStream.ToArray();
                                    Submit(signature);
                                }

                                break;
                        }

                        //   Pin = "";

                        if (NavigationalParameters.AuthorisationType ==
                            NavigationalParameters.AuthorisationTypes.CHANGE_PIN)
                        {
                            ShowNewPin = true;
                            ShowConfirmPin = true;
                            ShowPin = false;
                        }
                        else
                        {
                            ShowNewPin = false;
                            ShowConfirmPin = false;
                            ShowPin = true;
                        }
                    }
                }
                else
                {
                    await Alert("Incomplete", "Please complete all required fields", "Ok");
                }
            }
        }
            // Pin = "";
        
    });

    public Event Event { get; private set; }

    private void LoadPage_SignatureEmailOnly(Person person, AuthorisationDetail sigDetails)
    {
        var tempList = new ObservableCollection<Person>();

        if (string.IsNullOrEmpty(person.CompanyName)) person.FocusId = 0;

        if (NavigationalParameters.AppMode != NavigationalParameters.AppModes.SITEINSPECTION)
        {
            tempList = new ObservableCollection<Person> { person, Supervisor };
        }
        else
        {
            var list = new List<Person>();

            list.AddRange(_jobService.GetAuditors());

            list.AddRange(UserList);

            foreach (var user in list)
            {
                tempList.Add(user);

                if (person.FocusId == user.FocusId)
                {
                    tempList.Remove(person);

                    if (tempList.All(x => x.FocusId != person.FocusId)) tempList.Add(person);
                }
            }

            UserList = new ObservableCollection<Person>(tempList);
        }


        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL;

        NavigationalParameters.AuthDetail = sigDetails;
    }

    public void LoadPage_ChangePin(List<Person> gang)
    {
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.CHANGE_PIN;
        UserList = new ObservableCollection<Person>(gang);
    }

    //For authentication TODO Need to check for Supervisor or Gang
    public void LoadPage_ConfirmPin(JobData4Tablet jobData, AuthorisationDetail auth)
    {
        if (NavigationalParameters.AppMode != NavigationalParameters.AppModes.OPERATIVEDOCS
            && NavigationalParameters.AppMode != NavigationalParameters.AppModes.TASKLIST)
            NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_CONFIRM;

        if (NavigationalParameters.CurrentSelectedJob?.JobGang != null)
        {
            UserList = new ObservableCollection<Person>(NavigationalParameters.CurrentSelectedJob?.JobGang
                ?.GangLabourFiles);
        }
        else
        {
            var tempUsers = new ObservableCollection<Person>();

            if (NavigationalParameters.CurrentSelectedJob != null)
            {
                foreach (var item in NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped.Split('|'))
                    if (!string.IsNullOrEmpty(item))
                    {
                        var id = Convert.ToInt32(item);

                        var usr = _userService.GetUserById(id);

                        tempUsers.Add(usr);
                    }

                UserList = tempUsers;
            }
        }

        if (!UserList.Any()) UserList.Add(NavigationalParameters.LoggedInUser);

        if (NavigationalParameters.SelectedUser == null)
            NavigationalParameters.SelectedUser = NavigationalParameters.LoggedInUser;
    }

    //For authentication with signature
    public void LoadPage_InvestigationSignature(Person person, AuthorisationDetail sigDetails)
    {
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

        UserList = new ObservableCollection<Person> { person };
    }

    //For authentication with signature
    public void LoadPage_Signature(Person person, JobData4Tablet jobData, AuthorisationDetail sigDetails)
    {
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

        if (NavigationalParameters.AuthDetail?.Type == NavigationalParameters.AuthorisationTypes.OPERATIVE_PIN
            || NavigationalParameters.AuthDetail.Type == NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG)
        {
            UserList = new ObservableCollection<Person>(NavigationalParameters.CurrentSelectedJob?.JobGang == null
                ? _jobService.GetGang(NavigationalParameters.CurrentSelectedJob)?.GangLabourFiles
                : NavigationalParameters.CurrentSelectedJob.JobGang?.GangLabourFiles);

            foreach (var user in UserList)
                if (NavigationalParameters.MultSignatures != null &&
                    NavigationalParameters.MultSignatures.All(x => x.FocusId != user.FocusId))
                    NavigationalParameters.MultSignatures.Add(user);

            UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.TOOLBOXTALKS)
        {
            foreach (var item in NavigationalParameters.CurrentUserToolboxTalks)
            {
                var personToSave = _userService.GetUserById(Convert.ToInt32(item.FocusId));

                if (NavigationalParameters.MultSignatures != null &&
                    NavigationalParameters.MultSignatures.All(x => x.FocusId != personToSave.FocusId))
                    NavigationalParameters.MultSignatures.Add(personToSave);
            }

            UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);
        }
        else
        {
            //Look for supervisor in GangMembers add to person if found.
            if (NavigationalParameters.CurrentSelectedJob?.SupervisorId ==
                NavigationalParameters.CurrentSelectedJob?.JobGang?.SupervisorId)
            {
                Supervisor.FullName =
                    $"{NavigationalParameters.CurrentSelectedJob?.JobGang?.SupervisorFirstName} {NavigationalParameters.CurrentSelectedJob?.JobGang?.SupervisorSurname}";
                Supervisor.MemberPin = NavigationalParameters.CurrentSelectedJob?.JobGang?.SupervisorPin;
                Supervisor.FocusId = (int)NavigationalParameters.CurrentSelectedJob?.JobGang?.SupervisorId;
            }

            UserList = new ObservableCollection<Person> { Supervisor };
        }
    }

    public void LoadPage_Signature(Person person, JobData4Tablet jobData)
    {
        NavigationalParameters.AuthorisationType = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

        if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITECLEAR)
        {
            if (NavigationalParameters.AppType == NavigationalParameters.AppTypes.SUPERVISOR)
            {
                if (NavigationalParameters.MultSignatures != null &&
                    NavigationalParameters.MultSignatures.All(x => x.FocusId != Supervisor.FocusId))
                    NavigationalParameters.MultSignatures.Add(Supervisor);
            }
            else
            {
                if (NavigationalParameters.MultSignatures != null &&
                    NavigationalParameters.MultSignatures.All(x => x.FocusId != GangLeader.FocusId))
                    NavigationalParameters.MultSignatures.Add(GangLeader);

                if (NavigationalParameters.MultSignatures != null &&
                    NavigationalParameters.MultSignatures.All(x => x.FocusId != Supervisor.FocusId))
                    NavigationalParameters.MultSignatures.Add(Supervisor);
            }
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PERMITTODIG
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT)
        {
            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FocusId != GangLeader.FocusId))
                NavigationalParameters.MultSignatures.Add(GangLeader);

            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FocusId != Supervisor.FocusId))
                NavigationalParameters.MultSignatures.Add(Supervisor);
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.SITEINSPECTION)
        {
            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FocusId != GangLeader.FocusId))
                NavigationalParameters.MultSignatures.Add(GangLeader);

            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FocusId != Supervisor.FocusId))
                NavigationalParameters.MultSignatures.Add(Supervisor);

                var auditors = _jobService.GetAuditors();

                foreach (var a in auditors)
                {
                    if (NavigationalParameters.MultSignatures != null &&
                        NavigationalParameters.MultSignatures.All(x => x.FocusId != a.FocusId))
                        NavigationalParameters.MultSignatures.Add(a);
                }
                

            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FullName != "Auditor Signature"))
                NavigationalParameters.MultSignatures.Add(new Person
                {
                    FullName = "Auditor Signature"
                });
        }
        else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.CVI)
        {
            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FocusId != Supervisor.FocusId))
                NavigationalParameters.MultSignatures.Add(Supervisor);

            if (NavigationalParameters.MultSignatures != null &&
                NavigationalParameters.MultSignatures.All(x => x.FullName != "Client Signature"))
                NavigationalParameters.MultSignatures.Add(new Person
                {
                    FullName = "Client Signature"
                });
        }

        UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);
    }

    public void LoadPage_SignatureOnly(Person person, AuthorisationDetail sigDetails)
    {
        UserList = new ObservableCollection<Person>(new List<Person> { NavigationalParameters.SelectedUser });
        NavigationalParameters.AuthorisationType =
            NavigationalParameters.AuthorisationTypes.SIGNATURE_ONLY;
    }

    public async Task<bool> ChangePinAsync(string newPin, string confirmPin)
    {
        if (confirmPin?.Length == MaxPinLength && newPin?.Length == MaxPinLength && newPin == confirmPin)
        {
            NavigationalParameters.SelectedUser.MemberPin = newPin;
            await Alert("Pin Changed!", "Authentication");
            UpdateUserPin(NavigationalParameters.SelectedUser);
            NavigateBack();
            return true;
        }
        else
        {
            await Alert("Pin mismatch please ensure both pins match and try again!", "Authentication");
            confirmPin = "";
            return false;
        }
    }

    private async void UpdateUserPin(Person user)
    {
        await _userService.AddUser(user);

        await _webApi.UpdatePin(user);
    }

    private bool PersonSelected()
    {
        return true;
    }

    //checks the pin against the selected user.
    public async Task<bool> CheckPinAsync(string pin)
    {
        if (pin == NavigationalParameters.SelectedUser.MemberPin && pin.Length == MaxPinLength)
        {
            if (NavigationalParameters.AuthorisationType ==
                NavigationalParameters.AuthorisationTypes.CHANGE_PIN)
            {
                ShowNewPin = true;
                ShowConfirmPin = true;
                ShowPin = false;
            }
            else
            {
                ShowNewPin = false;
                ShowConfirmPin = false;
                ShowPin = true;
            }

            return true;
        }

        if (pin != NavigationalParameters.SelectedUser.MemberPin && pin.Length == MaxPinLength)
        {
            ShowNewPin = false;
            ShowConfirmPin = false;
            ShowPin = true;

            await Alert("Incorrect Pin", "Authentication");
        }

        return false;
    }

    //checks the pin against the other
    public async Task<bool> CheckPinAsync(string pin1, string pin2)
    {
        if (pin1.Length == MaxPinLength && pin2.Length == MaxPinLength && pin1 == pin2 ? true : false)
        {
            if (pin1 == "0000" || pin1 == "1234")
            {
                await Alert("Pin complexity error, cannot have 0000 or 1234", "Authentication");
                return false;
            }

            // CurrentViewState = NavigationalParameters.ViewStates.COMPLETE;
            //ViewChanged.Invoke(null, null);
            return true;
        }

        return false;
    }

    public bool ConfirmPin(string pin)
    {
        if (pin == NavigationalParameters.SelectedUser.MemberPin) return true;

        return false;
    }

    public async void Submit(string pin)
    {
        if (ConfirmPin(pin))
        {
            NavigationalParameters.AuthDetail.AuthorisedSig = true;
            NavigationalParameters.AuthDetail.AuthorisedName = NavigationalParameters.SelectedUser?.FullName;

            if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.OPERATIVEDOCS
                || NavigationalParameters.AppMode == NavigationalParameters.AppModes.TRAININGDOCS)
            {
                await NavigateTo(ViewModelLocator.DocumentListingPage);
            }
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.PLANT)
            {
                NavigationalParameters.AuthDetail.SignedBy = NavigationalParameters.SelectedUser?.FullName;
                NavigationalParameters.AuthDetail.FocusId = NavigationalParameters.SelectedUser.FocusId;
                Pin = "";
                NewPin = "";
                ConfirmedPin = "";
                await NavigateTo(ViewModelLocator.PlantTransferListPage);
            }
            else if (NavigationalParameters.AppMode == NavigationalParameters.AppModes.TASKLIST)
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    NavigationalParameters.AuthDetail.SignedBy = NavigationalParameters.SelectedUser?.FullName;
                    NavigationalParameters.AuthDetail.FocusId = NavigationalParameters.SelectedUser.FocusId;
                    NavigationalParameters.SelectedTaskItem.Complete = true;
                    _jobService.AddApproval(NavigationalParameters.SelectedTaskItem);

                    var DamageToInvestigate = NavigationalParameters.EventManagement.Investigations?.FirstOrDefault()
                        ?.DamageDetails?.FirstOrDefault();

                    var uploadedSucessfully =
                        await _jobService.UploadUtilityInvestigations(DamageToInvestigate, "Full");

                    if (uploadedSucessfully)
                    {
                        DamageToInvestigate.SavedToServer = true;

                        DamageToInvestigate.DamageInvestigated.IsUpdatedToServer = true;

                        await _jobService.UpdateInvestigationDetails(DamageToInvestigate);
                        NavigationalParameters.EventManagement.Investigations?.FirstOrDefault()?.DamageDetails
                            ?.Remove(NavigationalParameters.EventManagement.Investigations?.FirstOrDefault()
                                ?.DamageDetails?.FirstOrDefault());

                        NavigationalParameters.EventManagement.Investigations?.FirstOrDefault()?.DamageDetails
                            ?.Add(DamageToInvestigate);

                        IsLoading = false;
                    }

                    await NavigateTo(ViewModelLocator.TaskListPage);
                }
                else
                {
                    await Alert("No internet connectivity",
                        "Subbmission failed due to lack of connectivity, please find a better connection and try again",
                        "Ok");
                }
            }
            else
            {
                NavigationalParameters.AuthDetail.SignedBy = NavigationalParameters.SelectedUser?.FullName;
                NavigationalParameters.AuthDetail.FocusId = NavigationalParameters.SelectedUser.FocusId;
                Pin = "";
                NewPin = "";
                ConfirmedPin = "";
                NavigateBack();
            }
        }
        else
        {
            await Alert("Authentication Failed", "User Authentication");
        }
    }

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
            NavigationalParameters.AuthDetail.SignatureImg = signature;
            SaveSignature(NavigationalParameters.AuthDetail, NavigationalParameters.CurrentSelectedJob);
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
                Name ?? NavigationalParameters.SelectedUser?.FullName;
            NavigationalParameters.AuthDetail.Email = Email;
            SaveSignature(NavigationalParameters.AuthDetail, NavigationalParameters.CurrentSelectedJob);
        }
    }

    public async void SaveSignature(AuthorisationDetail sig, JobData4Tablet jobdata)
    {
        var guid = Guid.NewGuid();

        ShowSection1 = false;
        ShowSection2 = false;
        ShowSection3 = false;
        ShowSection4 = false;
        IsLoading = true;

        if (jobdata != null)
            filename =
                $"{DateTime.Now:hhmmss}_{DateTime.Now:ddMMyyyy}_{sig.Description}_{sig.FocusId}_{jobdata?.QuoteNumber}.jpg";


        if (NavigationalParameters.CurrentAudit != null)
        {
            filename =
                $"{DateTime.Now:hhmmss}_{DateTime.Now:ddMMyyyy}_{sig.Description}_{sig.FocusId}_{NavigationalParameters.CurrentAudit?.Qnumber}.jpg";

            if (sig.FocusId == jobdata.GangLeaderId)
                NavigationalParameters.CurrentAudit.AuditeeSignature = filename;
            else
                NavigationalParameters.CurrentAudit.AuditorSignature = filename;

            await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
        }

        if (NavigationalParameters.EventManagement != null)
        {
            Event = NavigationalParameters.EventManagement;

            if (Event != null && Event?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault() != null)
                filename =
                    $"{DateTime.Now:hhmmss}_{DateTime.Now:ddMMyyyy}_{sig.Description}_{sig.FocusId}_{Event.Investigations.FirstOrDefault().DamageDetails?.FirstOrDefault()?.QNumber}.jpg";
        }

        await sig.StoreSignature(sig.SignatureImg, filename);

        var current = Connectivity.NetworkAccess;

        if (current == NetworkAccess.Internet)
            // Connection to internet is available
            switch (NavigationalParameters.AppMode)
            {
                case NavigationalParameters.AppModes.INVESTIGATEUTILITYSTRIKE:
                {
                    var investigationId = Convert.ToInt32(Event.Investigations.FirstOrDefault().DamageDetails
                        .FirstOrDefault()?.InvestigationId);

                    Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault().SignatureInfo =
                        new DamageSignature
                        {
                            PublicUtilityDamageID = Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault()
                                ?.CurrentInvestigationStatus,
                            SignatureType = "Investigator",
                            Filename = filename,
                            SignatureDate = sig.Date,
                            InvestigationId = investigationId,
                            UserId = NavigationalParameters.LoggedInUser.FocusId
                        };

                    await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", filename);

                    try
                    {
                        Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault(x =>
                            x.Id == investigationId).PreviousImages = _pictureService.GetInvestigationImages(Event
                            .Investigations.FirstOrDefault().DamageDetails.FirstOrDefault(x =>
                                x.Id == investigationId).DamageGuid);

                        foreach (var item in Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault(x =>
                                     x.Id == investigationId).PreviousImages)
                            await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", item.FileName);

                        var uploadedSucessfully =
                            await _jobService.UploadUtilityInvestigations(
                                Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault(x =>
                                    x.Id == investigationId), "Submit");

                        if (uploadedSucessfully)
                        {
                            Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault().SavedToServer = true;

                            Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault().DamageInvestigated
                                .IsUpdatedToServer = true;

                            Event.Investigations.FirstOrDefault().DamageDetails.FirstOrDefault()
                                .CurrentInvestigationStatus = "Requires Sign Off";

                            await _jobService.SaveInvestigateDamage(Event.Investigations.FirstOrDefault().DamageDetails
                                .FirstOrDefault());

                            await _jobService.SaveCurrentInvestigation(Event.Investigations.FirstOrDefault()
                                .DamageDetails.FirstOrDefault().DamageInvestigated);

                            await _jobService.UpdateInvestigationDetails(Event.Investigations.FirstOrDefault()
                                .DamageDetails.FirstOrDefault());
                        }


                        NavigationalParameters.ReturnPage = "Signature";
                        //  NavigationalParameters.PassedInfo = new SelectInvestigationPage();
                        await NavigateTo(ViewModelLocator.SelectInvestigationPage);
                    }
                    catch (Exception ex)
                    {
                        var error = ex.ToString();

                        await Alert("An issue has occured saving the investigation. Please try again",
                            "Error!");
                    }
                }
                    break;
                case NavigationalParameters.AppModes.CVI:
                {
                    NavigationalParameters.CurrentCvi.LastUpdateTime = DateTime.Now;

                    if (NavigationalParameters.AuthorisationType ==
                        NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE)
                    {
                        NavigationalParameters.CurrentCvi.SupervisorName =
                            NavigationalParameters.AuthDetail?.AuthorisedName;
                        NavigationalParameters.CurrentCvi.SupervisorSignature = filename;
                        NavigationalParameters.CurrentCvi.SignatureDetails.Add(NavigationalParameters.AuthDetail);
                        NavigationalParameters.CurrentAssignment.Cvi.Clear();
                        NavigationalParameters.CurrentAssignment.Cvi.Add(NavigationalParameters.CurrentCvi);

                        NavigationalParameters.MultSignatures
                                .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId)
                                .Signature =
                            filename;

                        NavigationalParameters.MultSignatures
                                .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId)
                                .HasSigned =
                            true;
                        await _assignmentService.SaveCvi(NavigationalParameters.CurrentAssignment);

                        var addClient =
                            await Alert(
                                "Would you like to add the client signature or upload the CVI to the server? Please confirm",
                                "Confirmation", "Add client siganature", "Upload CVI");

                        if (addClient)
                        {
                            NavigationalParameters.AuthorisationType =
                                NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL;
                            await NavigateTo(ViewModelLocator.SignaturePage);
                        }
                        else
                        {
                            IsLoading = true;
                            ShowSection4 = false;
                            try
                            {
                                var cviToUpload =
                                    _assignmentService.GenerateAssignments2SaveById(NavigationalParameters
                                        .CurrentAssignment);
                                var success = await _jobService.UploadCviAsync(cviToUpload);

                                if (success)
                                {
                                    await Alert("The cvi has been submitted please check emails for confirmation",
                                        "Success", "Ok");

                                    NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGSELECTION;
                                    NavigationalParameters.ProjectListMode =
                                        NavigationalParameters.ProjectListModes.GANGLIST;
                                    await NavigateTo(ViewModelLocator.GangSelectPage);
                                    IsLoading = false;
                                    ShowSection4 = true;
                                }
                                else
                                {
                                    IsLoading = false;
                                    ShowSection4 = true;
                                    await Alert("An error has occured, please check connection and try again",
                                        "Upload Error!",
                                        "Ok");
                                }
                            }
                            catch
                            {
                                IsLoading = false;
                                ShowSection4 = true;
                                await Alert("An error has occured, please check connection and try again",
                                    "Upload Error!", "Ok");
                            }
                        }
                    }
                    else if (NavigationalParameters.AuthorisationType ==
                             NavigationalParameters.AuthorisationTypes.SIGNATURE_EMAIL)
                    {
                        var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
                        if (!Regex.IsMatch(NavigationalParameters.CurrentCvi.Email, emailPattern))
                        {
                            await Alert("Invalid email",
                                "The client email is not a valid email address pleasse check and try again", "Ok");
                        }
                        else
                        {
                            NavigationalParameters.CurrentCvi.OnBehalfOfClientId =
                                NavigationalParameters.CurrentSelectedJob?.ClientName;

                            NavigationalParameters.CurrentCvi.OnBehalfOfClientName =
                                NavigationalParameters.AuthDetail?.AuthorisedName;

                            NavigationalParameters.CurrentCvi.OnBehalfOfClientSignature =
                                NavigationalParameters.AuthDetail?.SignatureFileName;

                            //NavigationalParameters.CurrentCvi.Email = NavigationalParameters.AuthDetail?.Email;

                            NavigationalParameters.CurrentCvi.SignatureDetails.Add(NavigationalParameters.AuthDetail);

                            NavigationalParameters.MultSignatures.FirstOrDefault(x => x.FullName == "Client Signature")
                                .Signature = filename;

                            NavigationalParameters.MultSignatures.FirstOrDefault(x => x.FullName == "Client Signature")
                                .HasSigned = true;

                            NavigationalParameters.CurrentAssignment.Cvi.Clear();

                            NavigationalParameters.CurrentAssignment.Cvi.Add(NavigationalParameters.CurrentCvi);

                            await _assignmentService.SaveCvi(NavigationalParameters.CurrentAssignment);

                            if (string.IsNullOrEmpty(NavigationalParameters.CurrentCvi.SupervisorSignature))
                            {
                                await Alert("Please ensure both client and supervisor have signed before submitting.",
                                    "Supervisor signature required!", "Ok");

                                NavigationalParameters.AuthorisationType =
                                    NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

                                await NavigateTo(ViewModelLocator.SignaturePage);
                            }
                            else
                            {
                                try
                                {
                                    var cviToUpload =
                                        _assignmentService.GenerateAssignments2SaveById(NavigationalParameters
                                            .CurrentAssignment);


                                    var success = await _jobService.UploadCviAsync(cviToUpload);

                                    if (success)
                                    {
                                        await Alert("The cvi has been submitted please check emails for confirmation",
                                            "Success", "Ok");

                                        NavigationalParameters.AppMode = NavigationalParameters.AppModes.GANGSELECTION;
                                        NavigationalParameters.ProjectListMode =
                                            NavigationalParameters.ProjectListModes.GANGLIST;
                                        await NavigateTo(ViewModelLocator.GangSelectPage);
                                    }
                                    else
                                    {
                                        await Alert("An error has occured, please check connection and try again",
                                            "Upload Error!",
                                            "Ok");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    var error = ex.ToString();
                                    await Alert("An error has occured, please check connection and try again",
                                        "Upload Error!", "Ok");
                                }
                            }
                        }
                    }

                    await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", filename);
                }
                    break;
                case NavigationalParameters.AppModes.RISKASSESMENT:
                {
                    NavigationalParameters.CurrentSelectedJob.DailyChecksPosted = true;

                    NavigationalParameters.MultSignatures
                            .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId).Signature =
                        filename;

                    NavigationalParameters.MultSignatures
                        .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId).HasSigned = true;

                    await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", filename);

                    UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);

                    if (UserList.All(x => !string.IsNullOrEmpty(x.Signature)))
                    {
                        UserList.Clear();
                        await NavigateTo(ViewModelLocator.DailySiteInspectionPage);
                    }
                    else
                    {
                        ShowSection1 = true;
                        ShowSection2 = true;
                        ShowSection3 = true;
                        ShowSection4 = true;
                        IsLoading = false;
                        NavigationalParameters.AuthDetail.Type =
                            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                        NavigationalParameters.AuthorisationType =
                            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                        await NavigateTo(ViewModelLocator.SignaturePage);
                    }
                }
                    break;
                case NavigationalParameters.AppModes.PROJECTDOCS:
                {
                    NavigationalParameters.UserSignedDocument = new UsersToolBoxTalks
                    {
                        Date = DateTime.Now,
                        DistributionDate = jobdata.JobDate,
                        FocusId = sig.FocusId,
                        SaveToTraining = false,
                        SignatureFileName = filename,
                        SupervisorId = jobdata.SupervisorId,
                        GangLeaderId = jobdata.GangLeaderId,
                        ToolBoxTalkCode = NavigationalParameters.ToolBoxTalk.Code,
                        RemoteTableId = 0,
                        QuoteId = jobdata.QuoteNumber
                    };

                    NavigationalParameters.UserSignedDocument.SignatureFileName
                        = NavigationalParameters.MultSignatures
                                .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId).Signature
                            = filename;

                    NavigationalParameters.MultSignatures
                        .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId).HasSigned = true;

                    _jobService.AddUserToolBoxTalks(NavigationalParameters.UserSignedDocument);

                    _jobService.AddToolBoxTalks(NavigationalParameters.ToolBoxTalk);

                    await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                        NavigationalParameters.UserSignedDocument.SignatureFileName);

                    UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);

                    if (UserList.All(x => !string.IsNullOrEmpty(x.Signature)))
                    {
                        UserList.Clear();
                        NavigationalParameters.UserSignedDocument = null;
                        NavigationalParameters.ToolBoxTalk = null;

                        await NavigateTo(ViewModelLocator.DocumentListingPage);
                    }
                    else
                    {
                        ShowSection1 = true;
                        ShowSection2 = true;
                        ShowSection3 = true;
                        ShowSection4 = true;
                        IsLoading = false;

                        // NavigationalParameters.AuthDetail.Type = NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

                        //  NavigationalParameters.AutenticationType =
                        //     NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

                        await NavigateTo(ViewModelLocator.SignaturePage);
                    }
                }
                    break;
                case NavigationalParameters.AppModes.PLANT:
                    try
                    {
                        if (NavigationalParameters.PlantView == NavigationalParameters.PlantViews.TRANSFERIN)
                        {
                            if (NavigationalParameters.PlantTransfers != null)
                            {
                                NavigationalParameters.PlantTransfers.TransferSignatureStatus = "Transferred 2 Storage";

                                NavigationalParameters.AuthDetail.SignedBy = sig.SignedBy;
                                NavigationalParameters.AuthDetail.SignatureImg = sig.SignatureImg;
                            }

                            await _pictureService.SyncPicture2Azure("JobPictures", "PlantTransferInPics", filename);

                            NavigateBack();
                        }
                        else if (NavigationalParameters.PlantView == NavigationalParameters.PlantViews.TRANSFEROUT)
                        {
                            if (NavigationalParameters.PlantTransfers != null)
                            {
                                NavigationalParameters.PlantTransfers.TransferSignatureStatus = "Transferred 2 Storage";


                                NavigationalParameters.AuthDetail.SignedBy = sig.SignedBy;
                                NavigationalParameters.AuthDetail.SignatureImg = sig.SignatureImg;
                            }

                            await _pictureService.SyncPicture2Azure("JobPictures", "PlantTransferOutPics", filename);

                            NavigateBack();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(NavigationalParameters.AuthDetail.SignatureFileName) &&
                                NavigationalParameters.SelectedUser != null)
                            {
                                var wa = new WebApi();
                                var data2Send = new List<Checks4TabletResponses>();
                                var allRelevantPlantCheckResponses = new List<Checks4TabletResponses>();
                                var counter = 1;
                                var checksStatuses = _checkService.GetChecksStatus(
                                    NavigationalParameters.CurrentSelectedJob?.OperativeIdsPiped,
                                    NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString(), "Plant");

                                if (checksStatuses.Any())
                                {
                                    var areWeConnected = await wa.CanWeConnect2Api();

                                    if (areWeConnected)
                                    {
                                        var locationStatus =
                                            await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                                        if (locationStatus != PermissionStatus.Granted)
                                            lStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                                        else
                                            lStatus = PermissionStatus.Granted;

                                        if (lStatus == PermissionStatus.Granted)
                                            try
                                            {
                                                location =
                                                    await Geolocation.GetLocationAsync(new GeolocationRequest(
                                                        GeolocationAccuracy.Best,
                                                        new TimeSpan(0, 0, 5)));
                                            }
                                            catch (Exception ex)
                                            {
                                                var error = ex.ToString();
                                            }
                                        else
                                            await Alert("Location error",
                                                "Unable to retrieve location at this time please  check location settings or contact support!",
                                                "Ok");

                                        foreach (var item in checksStatuses)
                                            if (item.Split('|')[1] == "ChecksConfirmed")
                                            {
                                                allRelevantPlantCheckResponses.AddRange(
                                                    _checkService.GetAllRelevantResponses4SelectedDate(
                                                        Convert.ToInt64(item.Split('|')[0]), DateTime.Now));

                                                foreach (var checkData in allRelevantPlantCheckResponses)
                                                    if (checkData != null)
                                                    {
                                                        var modifiedCheck = new Checks4TabletResponses();

                                                        
                                                            modifiedCheck.Id = checkData.Id;
                                                            modifiedCheck.ServerId = 0;
                                                            modifiedCheck.Qnumber = "";
                                                            modifiedCheck.RelevantDate = checkData.RelevantDate;
                                                            modifiedCheck.GangLeaderName = checkData.GangLeaderName;
                                                            modifiedCheck.SupervisorName = checkData.SupervisorName;
                                                            modifiedCheck.PlantId = checkData.PlantId;
                                                            modifiedCheck.QuestionResponse = checkData.QuestionResponse;
                                                            modifiedCheck.DateTimeOfCheck = checkData.DateTimeOfCheck;
                                                            modifiedCheck.QuestionId = checkData.QuestionId;
                                                            modifiedCheck.PlantCheckedByName = checkData.PlantCheckedByName;
                                                            modifiedCheck.PlantAssignedToName = checkData.PlantAssignedToName;
                                                            modifiedCheck.Comments = checkData.Comments;
                                                            modifiedCheck.PictureFileName = checkData.PictureFileName;
                                                            modifiedCheck.PictureLatitude = "";
                                                            modifiedCheck.PictureLongitude = "";
                                                            modifiedCheck.SignatureFileName = NavigationalParameters.AuthDetail
                                                                ?.SignatureFileName;
                                                            modifiedCheck.CheckSubmittedBy = NavigationalParameters.SelectedUser
                                                                ?.FullName;
                                                            modifiedCheck.ChecksStatus = "ChecksPosted";

                                                            if (NavigationalParameters.SelectetedPlantItem == null)
                                                            {
                                                                modifiedCheck.ChecksType =
                                                                    NavigationalParameters.SelectetedPlantItem?.Group ==
                                                                    "Vehicles"
                                                                        ? "Vehicle"
                                                                        : "Plant";
                                                            }

                                                            modifiedCheck.DifferentCheckIndicator = counter++;
                                                            modifiedCheck.Location =
                                                                $"Lon:{location?.Longitude}; Lat:{location?.Latitude}";
                                                       

                                                        if (!data2Send.Any(x =>
                                                                x.PlantId == modifiedCheck.PlantId &&
                                                                x.DateTimeOfCheck == modifiedCheck.DateTimeOfCheck &&
                                                                x.SignatureFileName == modifiedCheck.SignatureFileName))
                                                            data2Send.Add(modifiedCheck);

                                                        if (checkData.PictureFileName != null &&
                                                            checkData.PictureFileName != string.Empty)
                                                            await _pictureService.SyncPicture2Azure("JobPictures",
                                                                "PicsFromIpad", checkData.PictureFileName);
                                                    }
                                            }

                                        await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad",
                                            NavigationalParameters.AuthDetail.SignatureFileName);

                                        //if (checkingFlag.Contains("Not Transferred 2 Storage"))
                                        //{
                                        //    await Alert(
                                        //        "Unable to save data to the Server at this time, please try to find better connectivity and try again!",
                                        //        "Error!");
                                        //}
                                        //else
                                        //{
                                        if (data2Send.Any())
                                        {
                                            var serverId = await wa.SaveChecksData2Server(data2Send);

                                            // If get back server Id then all is good so annotate Plant item & move it fromMy Plant tO Transferred Plant
                                            if (serverId > 0)
                                            {
                                                await Alert("Checks saved to the server!",
                                                    "Success!");

                                                foreach (var item in data2Send)
                                                {
                                                    item.ServerId = 1;

                                                    _checkService.AddSingleChecksResponse(item);
                                                }

                                                NavigateBack();
                                                // await NavigateTo(ViewModelLocator.MyPlantScreenPage);
                                            }
                                            else
                                            {
                                                await Alert(
                                                    "Unable to save data to the Server, please try to find better connectivity and try again!",
                                                    "Error!");

                                                NavigateBack();
                                            }
                                        }
                                        else
                                        {
                                            NavigateBack();
                                        }
                                    }
                                }
                                else
                                {
                                    await Alert("Nothing To Upload", "There are currently no checks to upload", "Ok");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var error = ex.ToString();
                        NavigateBack();
                    }

                    break;
                case NavigationalParameters.AppModes.PERMITTODIG:
                {
                    NavigationalParameters.MultSignatures
                            .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId).Signature =
                        filename;

                    NavigationalParameters.MultSignatures
                        .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId).HasSigned = true;

                    if (NavigationalParameters.SelectedUser.FocusId ==
                        NavigationalParameters.CurrentSelectedJob?.GangLeaderId)
                        NavigationalParameters.AuthDetail.Type =
                            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;
                    else
                        NavigationalParameters.AuthDetail.Type =
                            NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG;

                    await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", filename);

                    UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);

                    if (NavigationalParameters.AuthDetail.Type ==
                        NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG)
                    {
                        NavigationalParameters.CurrentPermit.OperativeSignature = sig.SignatureFileName;

                        NavigationalParameters.CurrentPermit.OperativeFocusId = sig.FocusId.ToString();
                    }
                    else if (NavigationalParameters.AuthDetail.Type ==
                             NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG)
                    {
                        if (sig.FocusId > 0)
                        {
                            NavigationalParameters.CurrentPermit.SupervisorFocusId = sig.FocusId.ToString();

                            NavigationalParameters.CurrentPermit.SupervisorSignature = sig.SignatureFileName;
                        }
                        else
                        {
                            NavigationalParameters.CurrentPermit.SupervisorFocusId =
                                NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();
                        }
                    }

                    await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

                    _jobService.AddPermit(NavigationalParameters.CurrentPermit);

                    if (UserList.All(x => !string.IsNullOrEmpty(x.Signature)))
                    {
                        ShowSection1 = false;
                        ShowSection2 = false;
                        ShowSection3 = false;
                        ShowSection4 = false;
                        ShowPermit = false;
                        IsLoading = true;

                        UserList.Clear();

                        var assignment =
                            _assignmentService.GenerateAssignments2SaveById(NavigationalParameters.CurrentAssignment);

                        var success = _jobService.UploadPermitsAsync(assignment);

                        await NavigateTo(ViewModelLocator.MenuSelectionPage);
                    }
                    else
                    {
                        NavigationalParameters.AuthDetail.Type =
                            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                        NavigationalParameters.AuthorisationType =
                            NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG;

                        await NavigateTo(ViewModelLocator.SignaturePage);
                    }
                }
                    break;
                case NavigationalParameters.AppModes.SITEINSPECTION:
                {
                    if (NavigationalParameters.CurrentAssignment != null && NavigationalParameters.CurrentAudit != null)
                    {
                        NavigationalParameters.MultSignatures
                                .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId)
                                .Signature =
                            filename;

                        NavigationalParameters.MultSignatures
                                .FirstOrDefault(x => x.FocusId == NavigationalParameters.SelectedUser.FocusId)
                                .HasSigned =
                            true;

                        await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", filename);

                        if (sig.FocusId == NavigationalParameters.CurrentSelectedJob?.GangLeaderId)
                        {
                            // NavigationalParameters.CurrentAudit.AuditeeSignature = sig.SignatureFileName;
                            NavigationalParameters.CurrentAudit.AuditeeSignature = sig.SignatureFileName;
                            NavigationalParameters.CurrentAudit.AuditeeFocusId = sig.FocusId;
                        }
                        else
                        {
                            NavigationalParameters.CurrentAudit.AuditorSignature = sig.SignatureFileName;
                            NavigationalParameters.CurrentAudit.AuditorsFocusId = sig.FocusId;
                            NavigationalParameters.CurrentAudit.ConductedBy = sig.AuthorisedName;
                        }

                        NavigationalParameters.CurrentAssignment.Audit = NavigationalParameters.CurrentAudit;
                        await _assignmentService.AddAudits(NavigationalParameters.CurrentAudit);
                        UserList = new ObservableCollection<Person>(NavigationalParameters.MultSignatures);

                        var signedUsers = UserList.Where(x => !string.IsNullOrEmpty(x.Signature))?.ToList()?.Count;

                        if (signedUsers >= 2)
                        {
                            UserList.Clear();

                            NavigationalParameters.CurrentAssignment.Complete = "true";

                            await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);

                            var assignment =
                                _assignmentService.GenerateAssignments2SaveById(
                                    NavigationalParameters.CurrentAssignment);

                            var success = await _jobService.UploadPermitsAsync(assignment);

                            if (success)
                            {
                                NavigationalParameters.CurrentAssignment.RemoteTableId = 1;

                                await _assignmentService.AddACurrentAssignment(NavigationalParameters
                                    .CurrentAssignment);

                                await NavigateTo(ViewModelLocator.MenuSelectionPage);

                                NavigationalParameters.CurrentAssignment = null;

                                NavigationalParameters.CurrentAudit = null;
                            }
                            else
                            {
                                await Alert("Error", "failure saving audit");
                            }
                        }
                        else
                        {
                            ShowSection1 = true;
                            ShowSection2 = true;
                            ShowSection3 = true;
                            ShowSection4 = true;
                            IsLoading = false;

                            NavigationalParameters.AuthDetail.Type =
                                NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

                            NavigationalParameters.AuthorisationType =
                                NavigationalParameters.AuthorisationTypes.PIN_AND_SIGNATURE;

                            await NavigateTo(ViewModelLocator.SignaturePage);
                        }
                    }
                    else
                    {
                        await Alert("Error submitting the audit",
                            "An error has occuered whilst saving the audit please contact support if this persists");
                        NavigateBack();
                    }
                }
                    break;
                case NavigationalParameters.AppModes.SITECLEAR:
                {
                    ShowPermit = false;

                    if (NavigationalParameters.AuthDetail.Type ==
                        NavigationalParameters.AuthorisationTypes.OPERATIVE_SIG)
                    {
                        NavigationalParameters.CurrentPermit.OperativeSignature = sig.SignatureFileName;

                        NavigationalParameters.CurrentPermit.OperativeFocusId = sig.FocusId.ToString();
                        NavigationalParameters.CurrentPermit.SupervisorFocusId =
                            NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();

                        await _assignmentService.AddACurrentAssignment(NavigationalParameters.CurrentAssignment);
                        _jobService.AddPermit(NavigationalParameters.CurrentPermit);

                        var assignment =
                            _assignmentService.GenerateAssignments2SaveById(NavigationalParameters.CurrentAssignment);

                        var success = _jobService.UploadPermitsAsync(assignment);

                        NavigationalParameters.ReturnPage = "SignatureKey";

                        await NavigateTo(ViewModelLocator.MenuSelectionPage);
                    }
                    else if (NavigationalParameters.AuthDetail.Type ==
                             NavigationalParameters.AuthorisationTypes.SUPERVISOR_SIG)
                    {
                        if (sig.FocusId > 0)
                        {
                            NavigationalParameters.CurrentPermit.SupervisorFocusId = sig.FocusId.ToString();

                            NavigationalParameters.CurrentPermit.SupervisorSignature = sig.SignatureFileName;
                        }
                        else
                        {
                            NavigationalParameters.CurrentPermit.SupervisorFocusId =
                                NavigationalParameters.CurrentSelectedJob?.SupervisorId.ToString();
                        }

                        _jobService.AddPermit(NavigationalParameters.CurrentPermit);

                        var assignment =
                            _assignmentService.GenerateAssignments2SaveById(NavigationalParameters.CurrentAssignment);

                        var success = _jobService.UploadPermitsAsync(assignment);

                        NavigationalParameters.ReturnPage = "SignatureKey";

                        await NavigateTo(ViewModelLocator.MenuSelectionPage);
                        // _navigationService.NavigateTo(Locator.MenuPageViewModelKey);
                    }

                    await _pictureService.SyncPicture2Azure("JobPictures", "PicsFromIpad", filename);
                }
                    break;
                default:
                    NavigateBack();
                    break;
            }
    }
}