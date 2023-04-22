using System;
using System.Collections.Generic;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class StockFuelPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private readonly Jobs _jobService;

    private bool _isLoading;

    public StockFuelPageViewModel()
    {
        _jobService = new Jobs();

        Title = "Fuel";
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

    public StockFuelDTO _stockFuel { get; set; }

    public StockFuelDTO StockFuel
    {
        get => _stockFuel;
        set
        {
            _stockFuel = value;
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


        StartReading = 0;
        EndReading = 0;
        Qty = 0;
        FuelType = "";


        StockFuel = new StockFuelDTO
        {
            DateTimeOfDelivery = DateTime.Now,
            EndReading = 0,
            TotalLitres = 0,
            StartReading = 0,
            RemoteId = 0,
            RecievedBy = NavigationalParameters.LoggedInUser.FocusId,
            QNumber = NavigationalParameters.CurrentSelectedJob.QuoteNumber,
            Type = "",
            SignatureFileName = ""
        };
    });

    public RelayCommand FuelTypeCommand => new(async () => { StockFuel.Type = FuelType; });

    public RelayCommand Submit => new(async () =>
    {
        try
        {
            StockFuel.StartReading = StartReading;
            StockFuel.EndReading = EndReading;
            StockFuel.TotalLitres = Qty;
            StockFuel.Type = FuelType;

            await _jobService.SaveStockFuel(StockFuel);

            NavigateBack();
        }
        catch (Exception ex)
        {
            await Alert("please check numeric values are in decimal format!", "Ok");
        }
    });

    public RelayCommand GoBack => new(async () => { NavigateBack(); });
}