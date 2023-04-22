﻿using System;using System.Collections.ObjectModel;using System.ComponentModel;using FocusXamarinMobileApplication.Helpers;using FocusXamarinMobileApplication.Models;using FocusXamarinMobileApplication.Services;using Xamarin.Forms;namespace FocusXamarinMobileApplication.ViewModels;public class WeatherEventPageViewModel : FBaseViewModel, INotifyPropertyChanged{    public Jobs _jobService;    public WeatherEventPageViewModel()    {        _jobService = new Jobs();        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;        ProjectDate = $"{NavigationalParameters.CurrentSelectedJob?.JobDate:dd/MM/yyyy}";    }    public string _projectDate { get; set; } =        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");    public string ProjectDate    {        get => _projectDate;        set        {            _projectDate = value;            OnPropertyChanged();        }    }    public string _projectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;    public string ProjectName    {        get => _projectName;        set        {            _projectName = value;            OnPropertyChanged();        }    }    private ObservableCollection<WeatherEvent> _weatherEventList { get; set; } = new();    public ObservableCollection<WeatherEvent> WeatherEventList    {        get => _weatherEventList;        set        {            _weatherEventList = value;            OnPropertyChanged();        }    }    private WeatherEvent _weatherEventItem { get; set; } = new();    public WeatherEvent WeatherEventItem    {        get => _weatherEventItem;        set        {            _weatherEventItem = value;            OnPropertyChanged();        }    }    private TimeSpan? _startTime { get; set; } = DateTime.Now.TimeOfDay;    public TimeSpan? StartTime    {        get => _startTime;        set        {            _startTime = value;            OnPropertyChanged();        }    }    private TimeSpan? _endTime { get; set; } = DateTime.Now.TimeOfDay;    public TimeSpan? EndTime    {        get => _endTime;        set        {            _endTime = value;            OnPropertyChanged();        }    }    private ImageSource _snowImage { get; set; } = SimpleStaticHelpers.GetImage("Snow");    public ImageSource SnowImage    {        get => _snowImage;        set        {            _snowImage = value;            OnPropertyChanged();        }    }    private ImageSource _stormImage { get; set; } = SimpleStaticHelpers.GetImage("Storm");    public ImageSource StormImage    {        get => _stormImage;        set        {            _stormImage = value;            OnPropertyChanged();        }    }    private ImageSource _lightRainImage { get; set; } = SimpleStaticHelpers.GetImage("LightRain");    public ImageSource LightRainImage    {        get => _lightRainImage;        set        {            _lightRainImage = value;            OnPropertyChanged();        }    }    private ImageSource _cloudyImage { get; set; } = SimpleStaticHelpers.GetImage("Cloudy");    public ImageSource CloudyImage    {        get => _cloudyImage;        set        {            _cloudyImage = value;            OnPropertyChanged();        }    }    private ImageSource _windImage { get; set; } = SimpleStaticHelpers.GetImage("Wind");    public ImageSource WindImage    {        get => _windImage;        set        {            _windImage = value;            OnPropertyChanged();        }    }    public ImageSource _sunnySpellsImage { get; set; } = SimpleStaticHelpers.GetImage("SunnySpells");    public ImageSource SunnySpellsImage    {        get => _sunnySpellsImage;        set        {            _sunnySpellsImage = value;            OnPropertyChanged();        }    }    private ImageSource _sunnyImage { get; set; } = SimpleStaticHelpers.GetImage("Sunshine");    public ImageSource SunnyImage    {        get => _sunnyImage;        set        {            _sunnyImage = value;            OnPropertyChanged();        }    }    public RelayCommand WeatherEventPageLoad => new(async () =>    {        SnowImage = SimpleStaticHelpers.GetImage("Snow");        LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        WindImage = SimpleStaticHelpers.GetImage("Wind");        SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        StormImage = SimpleStaticHelpers.GetImage("Storm");        WeatherEventList =            new ObservableCollection<WeatherEvent>(                _jobService.GetWeatherEvents(NavigationalParameters.CurrentSelectedJob));        WeatherEventItem.GangLeader = NavigationalParameters.LoggedInUser.FocusId;        WeatherEventItem.JobId = NavigationalParameters.CurrentSelectedJob.JobId;        WeatherEventItem.Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber;        WeatherEventItem.Date = NavigationalParameters.CurrentSelectedJob.JobDate;        StartTime = WeatherEventItem.StartTime?.TimeOfDay;        EndTime = WeatherEventItem.EndTime?.TimeOfDay;    });    public RelayCommand ReturnToDiary => new(async () => { NavigateBack(); });    public RelayCommand Submit => new(async () =>    {        if (string.IsNullOrEmpty(WeatherEventItem.Type))        {            await Alert("Please ensure all fields have been selected and try again", "Error!");        }        else        {            var weatherEventItemToSave = new WeatherEvent            {                GangLeader = NavigationalParameters.LoggedInUser.FocusId,                JobId = NavigationalParameters.CurrentSelectedJob.JobId,                Qnumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,                Date = NavigationalParameters.CurrentSelectedJob.JobDate,                Comments = WeatherEventItem.Comments,                StartTime = WeatherEventItem.Date?.Date + StartTime,                EndTime = WeatherEventItem.Date?.Date + EndTime,                DisplayType = WeatherEventItem.DisplayType,                Icon = WeatherEventItem.Icon,                Severity = WeatherEventItem.Severity,                Type = WeatherEventItem.Type            };            await _jobService.SaveWeatherEvent(weatherEventItemToSave);            StartTime = WeatherEventItem.StartTime == DateTime.Parse("01/01/1900 00:00:00")                ? DateTime.Now.TimeOfDay                : WeatherEventItem.StartTime.Value.TimeOfDay;            EndTime = WeatherEventItem.EndTime == DateTime.Parse("01/01/1900 00:00:00")                ? DateTime.Now.TimeOfDay                : WeatherEventItem.EndTime.Value.TimeOfDay;            WeatherEventList =                new ObservableCollection<WeatherEvent>(                    _jobService.GetWeatherEvents(NavigationalParameters.CurrentSelectedJob));            WeatherEventItem = new WeatherEvent();            WeatherEventItem.StartTime = WeatherEventItem.EndTime = DateTime.Now;            SnowImage = SimpleStaticHelpers.GetImage("Snow");            LightRainImage = SimpleStaticHelpers.GetImage("LightRain");            CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");            WindImage = SimpleStaticHelpers.GetImage("Wind");            SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");            SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");            StormImage = SimpleStaticHelpers.GetImage("Storm");        }    });}