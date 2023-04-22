using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using MethodTimer;

namespace FocusXamarinMobileApplication.ViewModels;

public class ProvingPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public Jobs _jobService;

    public ProvingPageViewModel()
    {
        _jobService = new Jobs();

        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;

        ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";
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

    private bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    private string _cableRunPointA { get; set; }

    public string CableRunPointA
    {
        get => _cableRunPointA;
        set
        {
            _cableRunPointA = value;
            OnPropertyChanged();
        }
    }

    private string _cableRunPointB { get; set; }

    public string CableRunPointB
    {
        get => _cableRunPointB;
        set
        {
            _cableRunPointB = value;
            OnPropertyChanged();
        }
    }

    private string _approxDistance { get; set; }

    public string ApproxDistance
    {
        get => _approxDistance;
        set
        {
            _approxDistance = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<CableRuns> _cableRuns { get; set; } = new();

    public ObservableCollection<CableRuns> CableRuns
    {
        get => _cableRuns;
        set
        {
            _cableRuns = value;
            OnPropertyChanged();
        }
    }

    private CableRuns _cableRun { get; set; } = new();

    public CableRuns CableRun
    {
        get => _cableRun;
        set
        {
            _cableRun = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ProvingPageLoad => new(async () =>
    {
        var cableRuns = _jobService.GetCableRuns(NavigationalParameters.CurrentSelectedJob.QuoteNumber);

        foreach (var cr in cableRuns)
        {
            var blockages = _jobService.GetBlockages(cr?.CableRunIdentifier).Any(x => x.Cleared == false);

            if (blockages)
            {
                CableRun.Blocked = true;

                cr.BackgroungColour = Color.Red;
            }
            else
            {
                cr.BackgroungColour = Color.White;

                CableRun.Blocked = false;

                if (cr.Proved)
                    cr.BackgroungColour = Color.Green;
                else
                    cr.BackgroungColour = Color.White;
            }

            _jobService.UpdateCableRun(CableRun);
        }

        CableRuns = new ObservableCollection<CableRuns>(cableRuns);
    });

    public RelayCommand AddCableRun => new(async () =>
    {
        try
        {
            if (string.IsNullOrEmpty(CableRunPointA) || string.IsNullOrEmpty(CableRunPointB) ||
                string.IsNullOrEmpty(ApproxDistance))
            {
                await Alert("Incomplete",
                    "Both cable run start point and end point must be comp[lete in order to register a new cable run!",
                    "Ok");
            }
            else
            {
                var cableRuns = _jobService.GetCableRuns(NavigationalParameters.CurrentSelectedJob.QuoteNumber);


                var cableRunToAdd = new CableRuns
                {
                    CableRunIdentifier = $"{CableRunPointA} - {CableRunPointB}",
                    QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
                    ProjectName = NavigationalParameters.CurrentSelectedJob.ProjectName,
                    SavedToServer = false,
                    ApproxDistance = Convert.ToDecimal(ApproxDistance)
                };

                if (cableRuns.All(x => x.CableRunIdentifier != cableRunToAdd.CableRunIdentifier))
                {
                    _jobService.AddCableRun(cableRunToAdd);

                    var cableRuns2 = _jobService.GetCableRuns(NavigationalParameters.CurrentSelectedJob.QuoteNumber);

                    ProvingPageLoad.Execute(null);
                }
                else
                {
                    await Alert("Error", "This cable run currently exsists please rename and try again!", "Ok");
                }

                CableRunPointA = "";
                CableRunPointB = "";
                ApproxDistance = "";
            }
        }
        catch (Exception ex)
        {
            await Alert("Error", "It has been unable to register the cable run please try again or contact support!",
                "Ok");
        }
    });

    public RelayCommand GoBack => new(async () => { await NavigateTo(ViewModelLocator.MenuSelectionPage); });

    [Time]
    public async Task ClearEventAsync(Button button)
    {
        CableRun = button.CommandParameter as CableRuns;

        CableRun.Proved = true;

        CableRun.Blocked = false;

        CableRun.SavedToServer = false;

        if (_jobService.GetBlockages(CableRun.CableRunIdentifier).Any(x => x.Cleared == false))
        {
            await Alert("Blockages",
                "You must clear all reported blockages before you can mark this cable run as clear");

           // NavigationalParameters.SelectedCableRun = CableRun;

            await NavigateTo(ViewModelLocator.BlockageListPage);
        }
        else
        {
            var uploadComplete = await _jobService.UploadCableRun(CableRun);

            if (uploadComplete)
            {
                CableRuns.Remove(CableRun);
                CableRun.SavedToServer = true;
                CableRun.BackgroungColour = Color.Green;
                CableRuns.Add(CableRun);
            }
            else
            {
                await Alert("Error", "Error upldating cable run ");
            }

            _jobService.UpdateCableRun(CableRun);
            CableRuns = new ObservableCollection<CableRuns>(CableRuns.OrderBy(x => x.RemoteTableId));
        }
    }

    [Time]
    public async Task BlockedEventAsync(Button button)
    {
        CableRun = button.CommandParameter as CableRuns;

       // NavigationalParameters.SelectedCableRun = CableRun;

        //NavigationalParameters.SelectedCableRun.Proved = false;

        //NavigationalParameters.SelectedCableRun.Blocked = true;

        //NavigationalParameters.SelectedCableRun.SavedToServer = false;

        //CableRun = NavigationalParameters.SelectedCableRun;

        // CableRuns.Remove(CableRun);
        _jobService.UpdateCableRun(CableRun);
        //CableRuns.Add(CableRun);

        await NavigateTo(ViewModelLocator.BlockageListPage);
    }
}