﻿namespace FocusXamarinMobileApplication.Views;public partial class WeatherEventPage : ContentPage, IFormsPage{    public WeatherEventPage()    {        InitializeComponent();        NavigationPage.SetHasNavigationBar(this, false);        _vm = Microsoft.SharePoint.Client.App.ViewModelLocator.WeatherEventPageViewModel;        BindingContext = _vm;        _vm.WeatherEventPageLoad.Execute(null);    }    public WeatherEventPageViewModel _vm { get; set; }    public void RefreshPage()    {    }    private void OnImage1ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.SNOW;        _vm.WeatherEventItem.Icon = "Snow";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("SnowGreen");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        _vm.WindImage = SimpleStaticHelpers.GetImage("Wind");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        _vm.StormImage = SimpleStaticHelpers.GetImage("Storm");    }    private void OnImage2ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.STORM;        _vm.WeatherEventItem.Icon = "Storm";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("Snow");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        _vm.WindImage = SimpleStaticHelpers.GetImage("Wind");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        _vm.StormImage = SimpleStaticHelpers.GetImage("StormGreen");    }    private void OnImage3ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.LIGHTRAIN;        _vm.WeatherEventItem.Icon = "LightRain";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("Snow");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRainGreen");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        _vm.WindImage = SimpleStaticHelpers.GetImage("Wind");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        _vm.StormImage = SimpleStaticHelpers.GetImage("Storm");    }    private void OnImage4ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.CLOUDY;        _vm.WeatherEventItem.Icon = "Cloudy";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("Snow");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("CloudyGreen");        _vm.WindImage = SimpleStaticHelpers.GetImage("Wind");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        _vm.StormImage = SimpleStaticHelpers.GetImage("Storm");    }    private void OnImage5ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.WIND;        _vm.WeatherEventItem.Icon = "Wind";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("Snow");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        _vm.WindImage = SimpleStaticHelpers.GetImage("WindGreen");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        _vm.StormImage = SimpleStaticHelpers.GetImage("Storm");    }    private void OnImage6ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.SUNNYSPELLS;        _vm.WeatherEventItem.Icon = "SunnySpells";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("Snow");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        _vm.WindImage = SimpleStaticHelpers.GetImage("Wind");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpellsGreen");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("Sunshine");        _vm.StormImage = SimpleStaticHelpers.GetImage("Storm");    }    private void OnImage7ButtonClicked(object sender, EventArgs e)    {        NavigationalParameters.WeatherCondition = NavigationalParameters.WeatherType.SUNSHINE;        _vm.WeatherEventItem.Icon = "Sunshine";        _vm.WeatherEventItem.Type = NavigationalParameters.WeatherCondition.ToString();        _vm.SnowImage = SimpleStaticHelpers.GetImage("Snow");        _vm.LightRainImage = SimpleStaticHelpers.GetImage("LightRain");        _vm.CloudyImage = SimpleStaticHelpers.GetImage("Cloudy");        _vm.WindImage = SimpleStaticHelpers.GetImage("Wind");        _vm.SunnySpellsImage = SimpleStaticHelpers.GetImage("SunnySpells");        _vm.SunnyImage = SimpleStaticHelpers.GetImage("SunshineGreen");        _vm.StormImage = SimpleStaticHelpers.GetImage("Storm");    }    public void OnStartTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)    {        // _vm.WeatherEventItem.StartTime = NavigationalParameters.CurrentSelectedJob.JobDate.Date + StartTimePicker.Time;    }    public void OnEndTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)    {        //_vm.WeatherEventItem.EndTime = NavigationalParameters.CurrentSelectedJob.JobDate.Date + EndTimePicker.Time;    }}