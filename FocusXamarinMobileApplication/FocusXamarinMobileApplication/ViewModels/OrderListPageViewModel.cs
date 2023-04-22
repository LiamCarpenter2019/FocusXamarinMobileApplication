using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services;

namespace FocusXamarinMobileApplication.ViewModels;

public class OrderListPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public OrderListPageViewModel()
    {
        _assigmentService = new Assignments();

        _jobService = new Jobs();

        _userService = new Users();
    }

    public Assignments _assigmentService { get; set; }
    public Jobs _jobService { get; set; }
    public Users _userService { get; set; }

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


    public string _title { get; set; } = "Please detail the order";

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
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

    public bool _isLoading { get; set; }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public bool _showApprovalButton { get; set; }

    public bool ShowApprovalButton
    {
        get => _showApprovalButton;
        set
        {
            _showApprovalButton = value;
            OnPropertyChanged();
        }
    }

    public bool _showUploadButton { get; set; }

    public bool ShowUploadButton
    {
        get => _showUploadButton;
        set
        {
            _showUploadButton = value;
            OnPropertyChanged();
        }
    }

    public JobData4Tablet _selectedJob { get; set; }

    public JobData4Tablet SelectedJob
    {
        get => _selectedJob;
        set
        {
            _selectedJob = value;
            OnPropertyChanged();
        }
    }


    public Order _selectedOrder { get; set; }

    public Order SelectedOrder
    {
        get => _selectedOrder;
        set
        {
            _selectedOrder = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Order> _orderList { get; set; }

    public ObservableCollection<Order> OrderList
    {
        get => _orderList;
        set
        {
            _orderList = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        ShowSection1 = true;

        ShowSection2 = true;

        ShowSection3 = true;

        ShowSection4 = false;

        Title = "Orders Pending";

        ProjectName = "";

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy") ??
                      DateTime.Now.ToString("dd/MM/yyyy");

        IsLoading = false;

        OrderList = new ObservableCollection<Order>(_jobService.GetInvoicies());
    });

    public RelayCommand InvoicePhotoCommand => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMMERCIAL;

        //  _jobService.RemoveOrder(NavigationalParameters.Order);
        await NavigateTo(ViewModelLocator.OrderBookItemPage);
    });

    public RelayCommand NewOrderCommand => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMMERCIAL;

        NavigationalParameters.Order = new Order();

        await NavigateTo(ViewModelLocator.OrderBookItemPage);
    });

    public RelayCommand VoidCommand => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMMERCIAL;

        NavigationalParameters.Order.Approved = false;

        NavigationalParameters.Order.Void = true;

        _jobService.SaveOrder(NavigationalParameters.Order);

        await _jobService.UpdateOrderBookAsync(NavigationalParameters.Order);

        _jobService.DeleteOrder(NavigationalParameters.Order);

        OrderList = new ObservableCollection<Order>(_jobService.GetInvoicies());
        //await NavigateTo(ViewModelLocator.CommercialListPage);
    });

    public RelayCommand GoBackCommand => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMMERCIAL;

        await NavigateTo(ViewModelLocator.CommercialListPage);
    });
}