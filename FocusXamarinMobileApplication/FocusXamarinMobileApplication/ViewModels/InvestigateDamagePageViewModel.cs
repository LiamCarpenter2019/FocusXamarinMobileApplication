#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.database;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Event = FocusXamarinMobileApplication.Models.Event;

#endregion

namespace FocusXamarinMobileApplication.ViewModels;

public class InvestigateDamagePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public InvestigateDamagePageViewModel()
    {
        Event = NavigationalParameters.EventManagement;

        if (Event.Category.ToUpper().Contains("UTILITYDAMAGE"))
            ForwardButtonText = "Go to event details ==>";
        else
            ForwardButtonText = "Go to methodology ==>";

        BackButtonText = "<== Back to task list";
    }

    public FocusMobileV3Database _db { get; set; }
    public GangHistory GangHistoryItem { get; set; }
    public DamageToInvestigate DamageToInvestigate { get; set; }

    private ObservableCollection<Witness> _witnesses { get; set; }

    public ObservableCollection<Witness> Witnesses
    {
        get => _witnesses;
        set
        {
            _witnesses = value;
            OnPropertyChanged();
        }
    }

    private string _clientName { get; set; }

    public string ClientName
    {
        get => _clientName;
        set
        {
            _clientName = value;
            OnPropertyChanged();
        }
    }

    private bool _visible { get; set; }

    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
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

    private bool _showInjuredPeople { get; set; } = true;

    public bool ShowInjuredPeople
    {
        get => _showInjuredPeople;
        set
        {
            _showInjuredPeople = value;
            OnPropertyChanged();
        }
    }

    public string BackButtonText { get; private set; }

    private string _location { get; set; }

    public string Location
    {
        get => _location;
        set
        {
            _location = value;
            OnPropertyChanged();
        }
    }

    private string _forwardButtonText { get; set; }

    public string ForwardButtonText
    {
        get => _forwardButtonText;
        set
        {
            _forwardButtonText = value;

            OnPropertyChanged();
        }
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


    private string _damageId { get; set; }

    public string DamageId
    {
        get => _damageId;
        set
        {
            _damageId = value;
            OnPropertyChanged();
        }
    }

    private string _contractReference { get; set; }

    public string ContractReference
    {
        get => _contractReference;
        set
        {
            _contractReference = value;
            OnPropertyChanged();
        }
    }


    private Event _event { get; set; }

    public Event Event
    {
        get => _event;
        set
        {
            _event = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<GangResponsible> _gangList { get; set; } = new();

    public ObservableCollection<GangResponsible> GangList
    {
        get => _gangList;
        set
        {
            _gangList = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<InjuredPerson> _injuredPeople { get; set; } = new();

    public ObservableCollection<InjuredPerson> InjuredPeople
    {
        get => _injuredPeople;
        set
        {
            _injuredPeople = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<ExternalPersonnel> _thirdPartyPersonnel { get; set; } = new();

    public ObservableCollection<ExternalPersonnel> ThirdPartyPersonnel
    {
        get => _thirdPartyPersonnel;
        set
        {
            _thirdPartyPersonnel = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(() =>
    {
        Event = NavigationalParameters.EventManagement;

        ShowInjuredPeople = Event.Category.ToUpper().Contains("UTILITYDAMAGE") ||
                            Event.Category.ToUpper().Contains("ACCIDENT");

        BackButtonText = "<== Back to task list";

        if (Event.Category.ToUpper().Contains("UTILITYDAMAGE"))
            ForwardButtonText = "Go to event details ==>";
        else
            ForwardButtonText = "Go to methodology ==>";

        DamageToInvestigate = Event.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault();
        //======================================================
        if (DamageToInvestigate != null)
        {
            ContractReference = DamageToInvestigate?.ContractReference.Substring(0, 11);
            DamageId = DamageToInvestigate?.DamageId.ToString();
            ClientName = DamageToInvestigate?.ClientName;
            ProjectName = DamageToInvestigate?.ProjectName;
            Location = DamageToInvestigate?.Location;
            ProjectDate = DateTime.Now.ToString();

            if (DamageToInvestigate.CurrentInvestigationStatus != "Not Started")
                DamageToInvestigate.CurrentInvestigationStatus = "In Progress";

            DamageToInvestigate.SavedToServer = false;

            GangList = null;

            DamageToInvestigate.GangResponisble = new List<GangResponsible>();

            var gangs = App.Database.GetItems<GangResponsible>().ToList();

            var gang = gangs
                .Where(x => (x.PublicUtilityDamagesId == DamageToInvestigate?.DamageId && x.InvestigationID == 0) ||
                            (x.PublicUtilityDamagesId == DamageToInvestigate?.DamageId &&
                             x.InvestigationID.ToString() == DamageToInvestigate?.InvestigationId))?
                .OrderBy(x => x.InvestigationID)
                .ThenByDescending(x => x.InputOn)?
                .ToList();

            DamageToInvestigate?.GangResponisble?.Clear();

            var operativesDisplayNames = "";

            foreach (var item in gang)
                if (!operativesDisplayNames.Contains(item.OperativeName)
                    && !string.IsNullOrEmpty(item.OperativeName)
                    && item.OperativeName != item.SupervisorName)
                    operativesDisplayNames += $"{item.OperativeName}, ";

            var gangToAdd = gang.FirstOrDefault();

            gangToAdd.GangResponsibleNames = operativesDisplayNames;

            if (!DamageToInvestigate.GangResponisble.Contains(gangToAdd))
                DamageToInvestigate?.GangResponisble?.Add(gangToAdd);

            GangList = new ObservableCollection<GangResponsible>(DamageToInvestigate?.GangResponisble);

            OnPropertyChanged("GangList");

            //----------------------------------------------------------------------------
            InjuredPeople = null;

            var injuredPeople = App.Database.GetItems<InjuredPerson>()?
                .Where(x =>
                    x.PublicUtilityDamageId == DamageToInvestigate.DamageId
                    && (x.InvestigationId == "0"
                        || x.InvestigationId == DamageToInvestigate.InvestigationId))
                .OrderByDescending(x => x.InputOn)?
                .ToList();

            DamageToInvestigate?.InjuredPersonnel?.Clear();

            if (injuredPeople.Count() > 0)
            {
                foreach (var person in injuredPeople)
                    if (DamageToInvestigate.InjuredPersonnel.All(x =>
                            x.RemoteTableId != person.RemoteTableId && x.InjuredName != person.InjuredName &&
                            x.Position != person.Position && x.Injury != person.Injury))
                        DamageToInvestigate?.InjuredPersonnel.Add(person);

                InjuredPeople = new ObservableCollection<InjuredPerson>(DamageToInvestigate.InjuredPersonnel);

                OnPropertyChanged("InjuredPeople");
            }

            //----------------------------------------------------------------------------
            Witnesses = null;

            var witness = App.Database.GetItems<Witness>()?.Where(i =>
                    (i.PublicUtilityDamageId == DamageToInvestigate.DamageId.ToString()
                     && i.InvestigationId == "0") || i.InvestigationId == DamageToInvestigate.InvestigationId)
                .OrderByDescending(x => x.InputOn).ToList();

            if (witness?.Count() > 0)
            {
                //DamageToInvestigate?.Witnesses?.Clear();

                foreach (var wt in witness)
                    if (DamageToInvestigate.Witnesses.All(x =>
                            x.RemoteTableId != wt.RemoteTableId && x.Name != wt.Name && x.Statement != wt.Statement))
                        DamageToInvestigate.Witnesses.Add(wt);

                Witnesses = new ObservableCollection<Witness>(DamageToInvestigate?.Witnesses);

                OnPropertyChanged("Witnesses");
            }

            //----------------------------------------------------------------------------
            ThirdPartyPersonnel = null;
            var externalPersonnels = App.Database.GetItems<ExternalPersonnel>()?.Where(i =>
                    i.InvestigationId == 0 ||
                    i.InvestigationId.ToString() == DamageToInvestigate.InvestigationId)
                .OrderByDescending(x => x.DateOnSite).ToList();

            if (externalPersonnels?.Count > 0)
            {
                ThirdPartyPersonnel = new ObservableCollection<ExternalPersonnel>();

                foreach (var tp in externalPersonnels.Where(tp => ThirdPartyPersonnel.All(x => x.Id != tp.Id)))
                    ThirdPartyPersonnel.Add(tp);

                DamageToInvestigate.ThirdPartyPersonnel = ThirdPartyPersonnel?.ToList();

                OnPropertyChanged("ExternalPersonnel");
            }

            //----------------------------------------------------------------------------
            //GangList.Clear();
            DamageToInvestigate.DamageInvestigated = App.Database.GetItems<Investigation>()?.FirstOrDefault
                (x => x.RemoteTabledId.ToString() == DamageToInvestigate.InvestigationId);


            Event.Investigations?.FirstOrDefault()?.DamageDetails
                ?.Remove(Event?.Investigations?.FirstOrDefault()?.DamageDetails?.FirstOrDefault());
            Event.Investigations?.FirstOrDefault()?.DamageDetails?.Add(DamageToInvestigate);
            NavigationalParameters.EventManagement = Event;
        }
        // NavigationalParameters.InvestigateDamage.DamageDetails.Add(DamageToInvestigate);
    });

    //================Injury==============
    public RelayCommand AddInjuredCommand => new(async () =>
    {
        NavigationalParameters.EventManagement = Event;
        await NavigateTo(ViewModelLocator.EditInjuredPersonPage);
    });

    public RelayCommand<int> InjuredPersonSelected => new(async i =>
    {
        NavigationalParameters.EventManagement = Event;

        NavigationalParameters.InjuredPerson = InjuredPeople[i];

        await NavigateTo(ViewModelLocator.EditInjuredPersonPage);
    });

    //===================Witness=============
    public RelayCommand AddWitnessCommand => new(async () => { await NavigateTo(ViewModelLocator.AddWitnessPage); });

    public RelayCommand<int> WitnessSelected => new(async i =>
    {
        NavigationalParameters.Witness = Witnesses[i];

        await NavigateTo(ViewModelLocator.AddWitnessPage);
    });

    public RelayCommand DamageDetailsCommand => new(async () =>
    {
        NavigationalParameters.EventManagement = Event;

        if (Event.Category.ToUpper().Contains("UTILITYDAMAGE"))
            await NavigateTo(ViewModelLocator.InvestigationDetailPage);
        else
            await NavigateTo(ViewModelLocator.MethodologyPage);
    });

    public RelayCommand<int> GangSelected => new(async i =>
    {
        //await NavigateModalTo(new EditGang(GangListHistory[i]));
    });

    public RelayCommand GoBack => new(async () => { await NavigateTo(ViewModelLocator.TaskListPage); });
}