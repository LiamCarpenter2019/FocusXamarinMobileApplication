namespace FocusXamarinMobileApplication.ViewModels;

public class VisitorLogOutPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public VisitorLogOutPageViewModel()
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob.JobDate.ToString("dd/MM/yyyy");
    }

    public List<string> YesNo { get; set; } = new() { "Yes", "No" };
    public Stream Image { get; set; }


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

    public string _safteyInstructions { get; set; }

    public string SafteyInstructions
    {
        get => _safteyInstructions;
        set
        {
            _safteyInstructions = value;
            OnPropertyChanged();
        }
    }


    public string _comments { get; set; } = "";

    public string Comments
    {
        get => _comments;
        set
        {
            _comments = value;
            OnPropertyChanged();
        }
    }

    public bool _isVisible { get; set; }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan _logOutTime { get; set; } = DateTime.Now.TimeOfDay;

    public TimeSpan LogOutTime
    {
        get => _logOutTime;
        set
        {
            _logOutTime = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(() =>
    {
        ShowSection1 = true;
        ShowSection2 = true;
        ShowSection3 = true;
        ShowSection4 = true;
        ShowSection5 = true;
        Comments = "";
        SafteyInstructions = "";
    });

    public RelayCommand LogoutNotPresent => new(async () =>
    {
        IsVisible = true;

        if (Comments == "") NavigationalParameters.SelectedVisitor.Comments = Comments = "Visitor not present";

        var ok = await Alert(
            "Warning!! Please complete the approximate time the visitor left site before selecting the visitor not present button and logging out",
            "Log out", "Log out", "Ok");

        if (ok)
        {
            NavigationalParameters.SelectedVisitor.LogOut(Comments, DateTime.Now.Date + LogOutTime);

            App.Database.SaveItem(NavigationalParameters.SelectedVisitor);

            IsVisible = false;

            await NavigateTo(ViewModelLocator.VisitorLogListPage);
        }
    });

    public RelayCommand Submit => new(async () =>
    {
        if (Image != null)
            using (var memoryStream = new MemoryStream())
            {
                Image.CopyTo(memoryStream);
                NavigationalParameters.SelectedVisitor.SignatureOutImg = memoryStream.ToArray();
            }

        var si = false;
        if (SafteyInstructions.ToLower() == "yes")
            si = true;
        else
            si = false;

        if (Comments == "") NavigationalParameters.SelectedVisitor.Comments = Comments = "Visitor not present";

        if (NavigationalParameters.SelectedVisitor.LogOut(Comments, si,
                NavigationalParameters.SelectedVisitor.SignatureOutImg))
        {
            NavigationalParameters.SelectedVisitor.SignatureOut =
                $"{DateTime.Now:hhmmss}_{DateTime.Now:ddMMyyyy}_VisitorOutSignature_{NavigationalParameters.SelectedVisitor.GangLeaderId}_{NavigationalParameters.CurrentSelectedJob.QuoteNumber}.jpg";
            NavigationalParameters.SelectedVisitor.StoreImage(NavigationalParameters.SelectedVisitor.SignatureOutImg,
                NavigationalParameters.SelectedVisitor.SignatureOut);

            App.Database.SaveItem(NavigationalParameters.SelectedVisitor);

            await NavigateTo(ViewModelLocator.VisitorLogListPage);
        }
        else
        {
            await Alert("Please sign before logging out", "Not signed");
        }
    });

    public RelayCommand Back => new(async () => { NavigateBack(); });
}