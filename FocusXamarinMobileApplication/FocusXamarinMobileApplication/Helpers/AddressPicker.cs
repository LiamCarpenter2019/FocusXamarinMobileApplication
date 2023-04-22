namespace FocusXamarinMobileApplication.Helpers;

public class AddressPicker : INotifyPropertyChanged
{
    public List<Abbreviations> _abbreviations;
    private string _fromAbbvr = "";

    private string _fromAddress = "";

    private string _fromNumber = "";
    private List<string> _fromNumberList;
    private int _selectedToAddress, _selectedFromAddress = -1;
    private string _toAbbvr = "";
    private string _toAddress = "";
    private string _toNumber = "";
    private List<string> _toNumberList;
    protected List<VMexpansionReleaseData> EndpointList;

    public int SelectedRelated = -1;
    public int SelectedRelatedGang;
    protected int Test = 5;

    public AddressPicker(List<Abbreviations> abbreviations, List<VMexpansionReleaseData> endpoints)
    {
        //Get All Addresses
        _abbreviations = abbreviations; //_assignmentService.GetAbbreviations();
        Abbreviations.Clear();
        Abbreviations.Add("--");
        Abbreviations.AddRange(abbreviations.Select(abr => abr.Abb).ToList());


        EndpointList = ListSort.OrderByNumber(endpoints).ToList();


        FromAddressList = new List<string>();
        FromAddressList.Add("-- Select --");
        foreach (var en in EndpointList)
            if (!FromAddressList.Contains(en.StreetName))
                FromAddressList.Add(en.StreetName);

        ToAddressList = new List<string>();
        ToAddressList.Add("-- Select --");
        foreach (var en in EndpointList)
            if (!ToAddressList.Contains(en.StreetName))
                ToAddressList.Add(en.StreetName);

        FromNumberList = new List<string>();
        //FromNumberList.Add("--Select--");
        ToNumberList = new List<string>();
        //ToNumberList.Add("--Select--");

        Debug.WriteLine($"Endpoints: {EndpointList.Count}");

        //Reset the picker items.
        if (EndpointList.Count > 0) ResetSelections();
    }

    public List<string> Abbreviations { get; set; } = new();
    public List<string> FromAddressList { get; set; } = new();
    public List<string> ToAddressList { get; set; } = new();


    public List<string> FromNumberList
    {
        get => _fromNumberList;
        set
        {
            _fromNumberList = value;
            OnPropertyChanged("FromNumberList");
        }
    }

    public List<string> ToNumberList
    {
        get => _toNumberList;
        set
        {
            _toNumberList = value;
            OnPropertyChanged("ToNumberList");
        }
    }

    public string FromAddress
    {
        get => _fromAddress;
        set
        {
            _fromAddress = value;
            OnPropertyChanged("FromAddress");
        }
    }

    public string ToAddress
    {
        get => _fromAddress;
        set
        {
            _toAddress = value;
            OnPropertyChanged("ToAddress");
        }
    }

    public Abbreviations ToAbbreviation { get; private set; }

    public Abbreviations FromAbbreviation { get; private set; }

    public VMexpansionReleaseData StartEndpoint { get; private set; }

    public VMexpansionReleaseData EndEndpoint { get; private set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public void SelectFromAbbvr(int i)
    {
        _fromAbbvr = Abbreviations[i];

        if (i > 0)
        {
            FromAbbreviation = _abbreviations[i - 1];
            OnPropertyChanged("FromAbbreviation");
        }
    }

    public void SelectToAbbvr(int i)
    {
        _toAbbvr = Abbreviations[i];

        if (i > 0)
        {
            ToAbbreviation = _abbreviations[i - 1];
            OnPropertyChanged("ToAbbreviation");
        }
    }

    public void SelectFromNumber(int i)
    {
        _fromNumber = FromNumberList[i];
        //_startEndpoint =
        //_endpointList.FirstOrDefault(x => x.StreetName == _fromAddress && x.BuildingNumber == fromNumber);
        Debug.WriteLine($"Endpoint: {_fromAbbvr} {_fromNumber} {_fromAddress}");
        OnPropertyChanged("StartEndpoint");
    }

    public void SelectToNumber(int i)
    {
        _toNumber = ToNumberList[i];
        //_endEndpoint =
        //_endpointList.FirstOrDefault(x => x.StreetName == _toAddress && x.BuildingNumber == toNumber);
        Debug.WriteLine($"EndPoint {_toAbbvr} {_toNumber} {_toAddress}");
        OnPropertyChanged("EndEndpoint");
    }

    public void SelectFromAddress(int i)
    {
        Debug.WriteLine($"From Address: {(FromAddressList.Count > 0 ? FromAddressList[i] : "")}");
        _fromAddress = FromAddressList.Count > 0 ? FromAddressList[i] : "";
        _selectedFromAddress = i;
        FromNumberList.Clear();
        FromNumberList.Add("-- Select -->");
        FromNumberList.AddRange(EndpointList.Where(end => end.StreetName == FromAddressList[i])
            .Select(num => num.BuildingNumber).ToList());

        //SelectFromNumber(0);
        OnPropertyChanged("StartEndpoint");
    }

    public void SelectToAddress(int i)
    {
        Debug.WriteLine($"To Address: {(ToAddressList.Count > 0 ? ToAddressList[i] : "")}");
        _toAddress = ToAddressList.Count > 0 ? ToAddressList[i] : "";
        _selectedToAddress = i;
        ToNumberList.Clear();
        ToNumberList.Add("-- Select -->");
        ToNumberList.AddRange(EndpointList.Where(end => end.StreetName == ToAddressList[i])
            .Select(num => num.BuildingNumber).ToList());

        //SelectToNumber(0);
        OnPropertyChanged("EndEndpoint");
    }

    public void ResetSelections()
    {
        SelectToAbbvr(0);
        SelectToAddress(0);
        SelectToNumber(0);
        SelectFromAbbvr(0);
        SelectFromAddress(0);
        SelectFromNumber(0);
    }

    // Create the OnPropertyChanged method to raise the event
    protected void OnPropertyChanged(string name)
    {
        StartEndpoint =
            EndpointList.FirstOrDefault(x => x.StreetName == _fromAddress && x.BuildingNumber == _fromNumber);
        EndEndpoint = EndpointList.FirstOrDefault(x => x.StreetName == _toAddress && x.BuildingNumber == _toNumber);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public void SetStartNumber(VMexpansionReleaseData ep)
    {
        _fromNumber = ep.BuildingNumber;
        FromAddress = ep.StreetName;
        StartEndpoint =
            ep; // _endpointList.FirstOrDefault(x => x.StreetName == _fromAddress && x.BuildingNumber == fromNumber);
    }

    public void SetEndNumber(VMexpansionReleaseData ep)
    {
        _toNumber = ep.BuildingNumber;
        ToAddress = ep.StreetName;
        EndEndpoint =
            ep; // _endpointList.FirstOrDefault(x => x.StreetName == _toAddress && x.BuildingNumber == toNumber);
    }
}