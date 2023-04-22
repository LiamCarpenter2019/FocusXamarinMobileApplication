using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;
using Microsoft.AppCenter.Analytics;

namespace FocusXamarinMobileApplication.ViewModels;

public class DfePageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    private string _calculatedValue;

    private string _inputValue;

    private bool _isLoading;

    private bool _isVisible;


    private bool _labelVisibility;

    private string _rateCode;

    private string _rateDescription;


    private List<Rates> _rates;


    private string _rateUnit;

    public DfePageViewModel()
    {
        JobService = new Jobs();
        AssignmentService = new Assignments();
        NavigationalParameters.AllRates = Rates = JobService.GetAllRates()
            .Where(x => x.BaseContractId.ToString() == NavigationalParameters.CurrentAssignment.Cnumber)
            .OrderBy(x => x.WorkHeader).ToList();
    }

    public string ProjectName { get; set; } = NavigationalParameters.CurrentSelectedJob?.ProjectName;

    public string ProjectDate { get; set; } =
        NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy");

    public bool ShowSubmissionButton { get; set; } = false;
    public Jobs JobService { get; }
    public Assignments AssignmentService { get; set; }
    public Rates RateSelected { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged();
        }
    }

    public bool LabelVisibility
    {
        get => _labelVisibility;
        set
        {
            _labelVisibility = value;
            OnPropertyChanged();
        }
    }

    public List<Rates> Rates
    {
        get => _rates;
        set
        {
            _rates = value;
            OnPropertyChanged();
        }
    }

    public string RateUnit
    {
        get => _rateUnit;
        set
        {
            _rateUnit = value;
            OnPropertyChanged();
        }
    }

    public string RateCode
    {
        get => _rateCode;
        set
        {
            _rateCode = value;
            OnPropertyChanged();
        }
    }

    public string RateDescription
    {
        get => _rateDescription;
        set
        {
            _rateDescription = value;
            OnPropertyChanged();
        }
    }

    public string InputValue
    {
        get => _inputValue;
        set
        {
            _inputValue = value;
            OnPropertyChanged();
        }
    }

    public string CalculatedValue
    {
        get => _calculatedValue;
        set
        {
            _calculatedValue = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand Submit => new(async () =>
    {
        IsLoading = true;
        try
        {
            var numberValue = Convert.ToDecimal(InputValue);

            var userChoice = await Alert("Submit Work Item?", "Would you like to submit the Work Item now?", "Yes",
                "No");
            if (userChoice)
            {
                if (Convert.ToDecimal(CalculatedValue) + Convert.ToDecimal(InputValue) < 0)
                    await Alert(
                        "This value would result in a negative qty and is not permitted, This has not been saved",
                        "Error!");
                else
                    try
                    {
                        NavigationalParameters.CurrentRate.AssignmentId =
                            NavigationalParameters.CurrentAssignment.AssignmentId;
                        NavigationalParameters.CurrentRate.BaseUnit = RateUnit;
                        NavigationalParameters.CurrentRate.Category = "Dfe";
                        NavigationalParameters.CurrentRate.Description = RateDescription;
                        NavigationalParameters.CurrentRate.Header = RateCode;
                        NavigationalParameters.CurrentRate.Identifier = NavigationalParameters.CurrentDfe.DfeId;
                        NavigationalParameters.CurrentRate.Qty = InputValue;

                        NavigationalParameters.DfeRatesToAdd.Add(NavigationalParameters.CurrentRate);
                        //_assignmentService.AddRate(NavigationalParameters.CurrentRate);
                        //save item
                        await Alert("Work item has been successfully saved", "Success!");

                        NavigateBack();
                    }
                    catch (Exception ex)
                    {
                        await Alert("An issue has occured submitting the Work Item, This has not been saved",
                            "Error!");
                    }
            }
            else
            {
                NavigateBack();
            }
        }
        catch (Exception ex)
        {
            Analytics.TrackEvent(
                $"Data passed to the {ToString()} has thrown this error {ex} for: {NavigationalParameters.LoggedInUser?.FullName}");

            await Alert("Invalid input, Please Try again ensuring qty is in decimal format", "Invalid!");
        }


        IsLoading = false;
    });

    public RelayCommand Cancel => new(async () =>
    {
        IsLoading = true;


        var userChoice = await Alert("Cancel Work Item?", "Would you like to cancel the work item now?", "Yes",
            "No");
        if (userChoice) NavigateBack();

        IsLoading = false;
    });


    public RelayCommand<Rates> SelectedRate => new(async e =>
    {
        RateSelected = e;

        NavigationalParameters.T6 = AssignmentService
            .GetT6ProjectWorks(NavigationalParameters.CurrentAssignment.Qnumber,
                NavigationalParameters.CurrentAssignment.AssignmentId).ToList();

        NavigationalParameters.DfeWorkItems = AssignmentService.GetNewDfeProjectWorks(
            NavigationalParameters.CurrentAssignment.Qnumber,
            NavigationalParameters.CurrentAssignment.AssignmentId).ToList();

        NavigationalParameters.DfeWorkItems.AddRange(NavigationalParameters.DfeRatesToAdd);

        NavigationalParameters.DfeWorkItems.AddRange(NavigationalParameters.T6);

        var projectWorksGrouped = NavigationalParameters.DfeWorkItems?.GroupBy(x => x.Header)
            .Select(i => new ProjectWorks
            {
                BaseUnit = i.FirstOrDefault()?.BaseUnit,
                Description = i.FirstOrDefault()?.Description,
                Header = i.FirstOrDefault()?.Header,
                QuoteId = i.FirstOrDefault().QuoteId,
                Qty = i.Sum(x => Convert.ToDecimal(x.Qty)).ToString()
            }).ToList();

        RateUnit = RateSelected?.BaseUnit ?? NavigationalParameters.CurrentRate?.BaseUnit;
        RateCode = RateSelected?.WorkHeader ?? NavigationalParameters.CurrentRate?.Header;
        RateDescription = RateSelected?.WorkDescription ?? NavigationalParameters.CurrentRate?.Description;

        CalculatedValue = projectWorksGrouped?.FirstOrDefault(x =>
            x.Header == (RateSelected?.WorkHeader ?? NavigationalParameters.CurrentRate?.Header))?.Qty;

        CalculatedValue = CalculatedValue ?? "0";


        InputValue = "";
        OnPropertyChanged("RateUnit");
        OnPropertyChanged("RateCode");
        OnPropertyChanged("RateDescription");
        OnPropertyChanged("InputValue");
        OnPropertyChanged("CalculatedValue");
    });

    public RelayCommand Refresh => new(() =>
    {
        //OnPropertyChanged("SavedDamageCount");
        OnPropertyChanged("ShowSubmissionButton");
    });

    public void RefreshRates()
    {
        IsVisible = true;
        LabelVisibility = false;
        NavigationalParameters.T6 = AssignmentService
            .GetT6ProjectWorks(NavigationalParameters.CurrentAssignment.Qnumber,
                NavigationalParameters.CurrentAssignment.AssignmentId).ToList();

        NavigationalParameters.DfeWorkItems = AssignmentService.GetNewDfeProjectWorks(
            NavigationalParameters.CurrentAssignment.Qnumber,
            NavigationalParameters.CurrentAssignment.AssignmentId).ToList();

        NavigationalParameters.DfeWorkItems.AddRange(NavigationalParameters.T6);


        if (NavigationalParameters.ActionToPerform == "EditDfeWithAddresses")
        {
            IsVisible = false;
            LabelVisibility = true;
            SelectedRate.Execute(NavigationalParameters.CurrentRate);
        }
    }
}