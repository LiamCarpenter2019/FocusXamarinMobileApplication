namespace FocusXamarinMobileApplication.ViewModels;

public class VisitorLogPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public VisitorLogPageViewModel()
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");
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

    public List<string> YesNo { get; set; } = new() { "Yes", "No" };
    public string _title { get; set; }
    public Stream Image { get; set; }

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

    public bool _showSection5 { get; set; }

    public bool ShowSection5
    {
        get => _showSection5;
        set
        {
            _showSection5 = value;
            OnPropertyChanged();
        }
    }

    public string _foreName { get; set; }

    public string ForeName
    {
        get => _foreName;
        set
        {
            _foreName = value;
            OnPropertyChanged();
        }
    }

    public string _surName { get; set; }

    public string SurName
    {
        get => _surName;
        set
        {
            _surName = value;
            OnPropertyChanged();
        }
    }

    public string _organisation { get; set; }

    public string Organisation
    {
        get => _organisation;
        set
        {
            _organisation = value;
            OnPropertyChanged();
        }
    }

    public string _registration { get; set; }

    public string Registration
    {
        get => _registration;
        set
        {
            _registration = value;
            OnPropertyChanged();
        }
    }

    public string _reasonForVisit { get; set; }

    public string ReasonForVisit
    {
        get => _reasonForVisit;
        set
        {
            _reasonForVisit = value;
            OnPropertyChanged();
        }
    }

    public string _answerA { get; set; }

    public string AnswerA
    {
        get => _answerA;
        set
        {
            _answerA = value;
            OnPropertyChanged();
        }
    }

    public string _answerB { get; set; }

    public string AnswerB
    {
        get => _answerB;
        set
        {
            _answerB = value;
            OnPropertyChanged();
        }
    }

    public string _answerC { get; set; }

    public string AnswerC
    {
        get => _answerC;
        set
        {
            _answerC = value;
            OnPropertyChanged();
        }
    }

    public string _answerD { get; set; }

    public string AnswerD
    {
        get => _answerD;
        set
        {
            _answerD = value;
            OnPropertyChanged();
        }
    }

    public string _answerE { get; set; }

    public string AnswerE
    {
        get => _answerE;
        set
        {
            _answerE = value;
            OnPropertyChanged();
        }
    }

    public string _answerF { get; set; }

    public string AnswerF
    {
        get => _answerF;
        set
        {
            _answerF = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        Title = "New Visitor";
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        ForeName = NavigationalParameters.NewVisitor?.Forename ?? "";
        SurName = NavigationalParameters.NewVisitor?.Surname ?? "";
        Organisation = NavigationalParameters.NewVisitor?.Organisation ?? "";
        Registration = NavigationalParameters.NewVisitor?.VehicleReg ?? "";
        ReasonForVisit = NavigationalParameters.NewVisitor?.Reason4Visit ?? "";
        AnswerA = (bool)NavigationalParameters.NewVisitor.Question1 ? "yes" : "no";
        AnswerB = (bool)NavigationalParameters.NewVisitor.Question2 ? "yes" : "no";
        AnswerC = (bool)NavigationalParameters.NewVisitor.Question3 ? "yes" : "no";
        AnswerD = (bool)NavigationalParameters.NewVisitor.Question3 ? "yes" : "no";
        AnswerE = (bool)NavigationalParameters.NewVisitor.Question3 ? "yes" : "no";
        AnswerF = (bool)NavigationalParameters.NewVisitor.Question3 ? "yes" : "no";
    });

    public RelayCommand Submit => new(async () =>
    {
        try
        {
            //newVisitor.Forename = ForeName;
            //   newVisitor.Surname = SurName;
            //  newVisitor.Organisation = Organisation;
            ////   newVisitor.Reason4Visit = ReasonForVisit;
            //   newVisitor.VehicleReg = Registration;
            NavigationalParameters.NewVisitor.Question1 = AnswerA.ToLower() == "yes";
            NavigationalParameters.NewVisitor.Question2 = AnswerB.ToLower() == "yes";
            NavigationalParameters.NewVisitor.Question3 = AnswerC.ToLower() == "yes";
            NavigationalParameters.NewVisitor.Question4 = AnswerD.ToLower() == "yes";
            NavigationalParameters.NewVisitor.Question5 = AnswerE.ToLower() == "yes";
            NavigationalParameters.NewVisitor.Question6 = AnswerF.ToLower() == "yes";
            NavigationalParameters.NewVisitor.ContractReference =
                NavigationalParameters.CurrentSelectedJob.ContractReference;
            NavigationalParameters.NewVisitor.DateLogged = DateTime.Now;
            NavigationalParameters.NewVisitor.GangLeader =
                $"{NavigationalParameters.CurrentSelectedJob?.JobGang.GangLeaderFirstName} {NavigationalParameters.CurrentSelectedJob?.JobGang.GangLeaderSurname}";
            NavigationalParameters.NewVisitor.GangLeaderId = NavigationalParameters.CurrentSelectedJob.GangLeaderId;
            //newVisitor.SignatureIn = NavigationalParameters.AuthDetail.SignatureFileName;
            NavigationalParameters.NewVisitor.OnSiteTime = DateTime.Now;

            using (var memoryStream = new MemoryStream())
            {
                Image.CopyTo(memoryStream);
                NavigationalParameters.NewVisitor.SignatureInImg = memoryStream.ToArray();
            }


            // var data = NavPassedInfo.Item2;
            if (NavigationalParameters.NewVisitor.LogIn(ForeName, SurName, Organisation, Registration, ReasonForVisit,
                    NavigationalParameters.NewVisitor.SignatureInImg, NavigationalParameters.NewVisitor.Question1,
                    NavigationalParameters.NewVisitor.Question2, NavigationalParameters.NewVisitor.Question3,
                    NavigationalParameters.NewVisitor.Question4, NavigationalParameters.NewVisitor.Question5,
                    NavigationalParameters.NewVisitor.Question6))
            {
                NavigationalParameters.NewVisitor.SignatureIn =
                    $"{DateTime.Now:hhmmss}_{DateTime.Now:ddMMyyyy}_VisitorInSignature_{NavigationalParameters.CurrentSelectedJob.GangLeaderId.ToString()}_{NavigationalParameters.CurrentSelectedJob?.QuoteNumber.ToString()}.jpg";

                NavigationalParameters.NewVisitor.StoreImage(NavigationalParameters.NewVisitor.SignatureInImg,
                    NavigationalParameters.NewVisitor.SignatureIn);

                App.Database.SaveItem(NavigationalParameters.NewVisitor);

                await NavigateTo(ViewModelLocator.VisitorLogListPage);
            }
            else
            {
                if (Registration.Length > 10)
                    await Alert(
                        "a maximum of 10 charachters can be entered into the registration field please check and try again.",
                        "Please check the registration details");
                else
                    await Alert("Please complete all entries", "Form incomplete");
            }
            //  Vm.Save(txtLogInForename.Text, txtLogInSurname.Text, txtLogInOrganisation.Text,
            //               txtLogInVehicleReg.Text, txtViewLogInReason.Text, (int) segQuestion1.SelectedSegment,
            //             (int) segQuestion2.SelectedSegment,
            //           (int) segQuestion3.SelectedSegment, (int) segQuestion4.SelectedSegment,
            //        );
        }
        catch (Exception ex)
        {
            await Alert("Error whilst saving Please ensure all entries are completete", "Form incomplete");
        }
    });

    public RelayCommand Cancel => new(async () => { NavigateBack(); });
}