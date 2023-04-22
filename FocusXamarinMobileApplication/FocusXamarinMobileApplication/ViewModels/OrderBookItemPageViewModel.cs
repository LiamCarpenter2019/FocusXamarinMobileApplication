namespace FocusXamarinMobileApplication.ViewModels;

public class OrderBookItemPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public OrderBookItemPageViewModel()
    {
        _assigmentService = new Assignments();

        _jobService = new Jobs();

        _userService = new Users();

        ProjectName = "";

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy") ??
                      DateTime.Now.ToString("dd/MM/yyyy");
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

    public bool _showInvoiceButton { get; set; }

    public bool ShowInvoiceButton
    {
        get => _showInvoiceButton;
        set
        {
            _showInvoiceButton = value;
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

    public long _orderNumber { get; set; }

    public long OrderNumber
    {
        get => _orderNumber;
        set
        {
            _orderNumber = value;
            OnPropertyChanged();
        }
    }

    public string _userName { get; set; }

    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged();
        }
    }


    public string _supplier { get; set; }

    public string Supplier
    {
        get => _supplier;
        set
        {
            _supplier = value;
            OnPropertyChanged();
        }
    }


    public int _qty { get; set; }

    public int Qty
    {
        get => _qty;
        set
        {
            _qty = value;
            OnPropertyChanged();
        }
    }

    public decimal _pPU { get; set; }

    public decimal PPU
    {
        get => _pPU;
        set
        {
            _pPU = value;
            OnPropertyChanged();
        }
    }

    public decimal _pPUEV { get; set; }

    public decimal PPUEV
    {
        get => _pPUEV;
        set
        {
            _pPUEV = value;
            OnPropertyChanged();
        }
    }

    public string _description { get; set; }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public string _comments { get; set; }

    public string Comments
    {
        get => _comments;
        set
        {
            _comments = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<JobData4Tablet> _availableJobs { get; set; }

    public ObservableCollection<JobData4Tablet> AvailableJobs
    {
        get => _availableJobs;
        set
        {
            _availableJobs = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<OrderBookItem> _itemList { get; set; }

    public ObservableCollection<OrderBookItem> ItemList
    {
        get => _itemList;
        set
        {
            _itemList = value;
            OnPropertyChanged();
        }
    }

    public OrderBookItem _selectedOrderItem { get; set; }

    public OrderBookItem SelectedOrderItem
    {
        get => _selectedOrderItem;
        set
        {
            _selectedOrderItem = value;
            OnPropertyChanged();
        }
    }

    public Order _order { get; set; }

    public Order Order
    {
        get => _order;
        set
        {
            _order = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand ScreenLoaded => new(async () =>
    {
        var jobs = new List<JobData4Tablet>();

        Order = NavigationalParameters.Order;

        ShowSection1 = true;

        ShowSection2 = true;

        ShowSection3 = true;

        ShowSection4 = true;

        Supplier = "";

        Description = "";

        Comments = "";

        Title = "Order Details";

        IsLoading = false;

        foreach (var job in NavigationalParameters.AllJobs)
            if (jobs.All(x => x.ProjectName != job.ProjectName))
                jobs.Add(job);

        AvailableJobs = new ObservableCollection<JobData4Tablet>(jobs);

        Supplier = Order.Supplier;

        Comments = Order.Comments;

        ProjectDate = NavigationalParameters.CurrentSelectedJob?.JobDate.ToString("dd/MM/yyyy") ??
                      DateTime.Now.ToString("dd/MM/yyyy");

        SelectedJob = AvailableJobs.FirstOrDefault(x => x.QuoteNumber == Order.QuoteNumber);

        NavigationalParameters.Order = Order;

        ItemList = new ObservableCollection<OrderBookItem>(_jobService.GetOrderItems(NavigationalParameters.Order));

        if (NavigationalParameters.Order?.OrderNumber > 0)
        {
            ShowInvoiceButton = true;
            ShowApprovalButton = false;
        }
        else
        {
            ShowInvoiceButton = false;
            ShowApprovalButton = true;
        }
    });

    public RelayCommand AddOrderItem => new(async () =>
    {
        if (PPU <= 0 || Qty <= 0 || string.IsNullOrEmpty(Description))
        {
            await Alert("Incomplete",
                "Plese ensure the decriptioon, Qty and price per unit are all complete before adding the item to the list! please note zero values are not permitted!",
                "Ok");
        }
        else
        {
            SelectedOrderItem = new OrderBookItem
            {
                OrderGuid = NavigationalParameters.Order.OrderGuid,

                Description = Description,

                PricePerUnit = PPU,

                Qty = Qty
            };

            _jobService.SaveOrderItem(SelectedOrderItem);

            Description = "";

            Qty = 0;

            PPU = 0;

            ItemList = new ObservableCollection<OrderBookItem>(_jobService.GetOrderItems(Order));
        }
    });

    public RelayCommand ApprovalCommand => new(async () =>
    {
        try
        {
            IsLoading = true;

            ShowInvoiceButton = false;

            ShowApprovalButton = false;

            ShowSection4 = true;

            Order.OrderBookItem = _jobService.GetOrderItems(Order).ToList();

            _jobService.SaveOrder(Order);

            OrderNumber = await _jobService.UpdateOrderBookAsync(Order);


            if (OrderNumber > 0)
            {
                Order.OrderNumber = OrderNumber;

                foreach (var item in Order.OrderBookItem)
                {
                    item.OrderNumber = OrderNumber;

                    if (item?.PricePerUnit != null) Order.PriceExcludingVAT += item.Qty * item.PricePerUnit;

                    _jobService.SaveOrderItem(SelectedOrderItem);
                }

                Order.OrderedById = NavigationalParameters.LoggedInUser.FocusId;
                Order.OrderByName = NavigationalParameters.LoggedInUser.FullName;
                Order.QuoteNumber = SelectedJob.QuoteNumber;

                _jobService.SaveOrder(Order);

                IsLoading = false;

                ShowUploadButton = true;

                ShowSection4 = false;

                Order = null;

                NavigationalParameters.Order = null;

                ItemList = null;

                await NavigateTo(ViewModelLocator.OrderListPage);
            }
            else
            {
                await Alert(
                    "The order has failed to process please try again or contact cupport should the issue persist!",
                    "Ok");
            }
        }
        catch (Exception ex)
        {
            await Alert(
                "There is an error whilst submitting for approval of this persists please contact system support!",
                "Ok");

            IsLoading = false;

            ShowApprovalButton = true;
        }
    });

    public RelayCommand InvoicePhotoCommand => new(async () =>
    {
        NavigationalParameters.Order = Order;

        if (Order.OrderNumber <= 0)
            await Alert("the order must be submitted for approval befor an order number can be issued!", "Ok");
        else
            try
            {
                NavigationalParameters.PhotoMode = NavigationalParameters.PhotoModes.INVOICE;

                await NavigateTo(ViewModelLocator.PhotoSelectionPage);
            }
            catch (Exception ex)
            {
                ShowSection4 = false;

                await Alert(
                    "There is an error whilst uploading the invoice if this persists please contact system support!",
                    "Ok");
            }

        Order = NavigationalParameters.Order;
    });

    public RelayCommand GoBackCommand => new(async () =>
    {
        NavigationalParameters.AppMode = NavigationalParameters.AppModes.COMMERCIAL;

        Order.Approved = false;

        _jobService.SaveOrder(Order);

        NavigationalParameters.Order = Order;


        await NavigateTo(ViewModelLocator.OrderListPage);
    });
}