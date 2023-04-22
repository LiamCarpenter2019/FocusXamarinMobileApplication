using System;
using System.Collections.Generic;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class FuelConsumptionPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;

    private bool _isLoading;

    public FuelConsumptionPageViewModel()
    {
        _jobService = new Jobs();

        Title = "Fuel";
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


    public string Title { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public FuelConsumption _fuelConsumption { get; set; }

    public FuelConsumption FuelConsumption
    {
        get => _fuelConsumption;
        set
        {
            _fuelConsumption = value;
            OnPropertyChanged();
        }
    }

    public decimal _hoursOrMilage { get; set; }

    public decimal HoursOrMilage
    {
        get => _hoursOrMilage;
        set
        {
            _hoursOrMilage = value;

            OnPropertyChanged();
        }
    }

    public decimal _startReading { get; set; }

    public decimal StartReading
    {
        get => _startReading;
        set
        {
            _startReading = value;

            OnPropertyChanged();
        }
    }

    public decimal _endReading { get; set; }

    public decimal EndReading
    {
        get => _endReading;
        set
        {
            _endReading = value;
            Qty = EndReading - StartReading;
            OnPropertyChanged();
        }
    }

    public decimal _qty { get; set; }

    public decimal Qty
    {
        get => _qty;
        set
        {
            _qty = value;
            OnPropertyChanged();
        }
    }

    public string _fuelType { get; set; }

    public string FuelType
    {
        get => _fuelType;
        set
        {
            _fuelType = value;
            OnPropertyChanged();
        }
    }

    public List<string> Fueloptions { get; set; } = new() { "Red Diesel", "White Deisel", "Petrol" };

    public RelayCommand PageLoaded => new(async () =>
    {
        ProjectName = NavigationalParameters.CurrentSelectedJob?.ProjectName;
        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

        HoursOrMilage = 0;
        StartReading = 0;
        EndReading = 0;
        Qty = 0;
        FuelType = "";


        FuelConsumption = new FuelConsumption
        {
            DateOfEntry = DateTime.Now,
            RegAssetNumber = NavigationalParameters.SelectetedPlantItem.AssetNo,
            FullName = NavigationalParameters.LoggedInUser.FullName,
            EndReading = 0,
            HoursOrMilage = 0,
            Qty = 0,
            StartReading = 0,
            RemoteTableId = 0,
            Type = ""
        };
    });

    public RelayCommand FuelTypeCommand => new(async () => { FuelConsumption.Type = FuelType; });

    public RelayCommand Submit => new(async () =>
    {
        try
        {
            FuelConsumption.HoursOrMilage = HoursOrMilage;
            FuelConsumption.StartReading = StartReading;
            FuelConsumption.EndReading = EndReading;
            FuelConsumption.Qty = Qty;
            FuelConsumption.Type = FuelType;

            await _jobService.SaveFuelConsumption(FuelConsumption);

            NavigateBack();
        }
        catch (Exception ex)
        {
            await Alert("please check numeric values are in decimal format!", "Ok");
        }
    });

    public RelayCommand GoBack => new(async () => { NavigateBack(); });
}