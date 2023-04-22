namespace FocusXamarinForms20082020V1.Views;

public partial class HybridWebViewPage : ContentPage
{
    private readonly HybridWebViewPageViewModel _vm;
    private readonly HybridWebView hybridWebView;
    private readonly HybridWebView hybridWebView1;
    private readonly Label labelLoading;

    public HybridWebViewPage()
    {
        InitializeComponent();

        NavigationPage.SetHasNavigationBar(this, false);

        _vm = App.ViewModelLocator.HybridWebViewPageViewModel;

        BindingContext = _vm;

        _vm.SetCurrentPosition.Execute(null);

        _vm.PageLoaded.Execute(null);

        //Loading label should not render by default.
        labelLoading = new Label { Text = "Loading...", IsVisible = false };
        hybridWebView1 = new HybridWebView { HeightRequest = 1000, WidthRequest = 1000 };
        hybridWebView = new HybridWebView { HeightRequest = 1000, WidthRequest = 1000 };
        hybridWebView.Uri = _vm.Url;
        hybridWebView1.Navigated += webviewNavigated;
        hybridWebView1.Navigating += webviewNavigating;

        hybridWebView1SL.Children.Add(hybridWebView1);
        hybridWebView1SL.Children.Add(labelLoading);

        hybridWebView.Navigated += webviewNavigated;
        hybridWebView.Navigating += webviewNavigating;

        hybridWebViewSL.Children.Add(hybridWebView);

        hybridWebView.RegisterAction(data => _vm.FindLocationInfo(data));
    }

    public Placemark CurrentPlacemark { get; set; }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            _vm.PageLoaded.Execute(null);

            if (_vm.IsDocMode)
            {
                hybridWebView1.Source = _vm.PdfSource;
            }
            else
            {
                hybridWebView.Uri = _vm.Url;
            }
        }
        catch (Exception ex)
        {
            var error = ex.ToString();
        }
    }


    /// <summary>
    ///     Called when the webview starts navigating. Displays the loading label.
    /// </summary>
    public void webviewNavigating(object sender, WebNavigatingEventArgs e)
    {
        labelLoading.IsVisible = true;
    }

    public async void OnCallJavaScriptButtonClicked(object sender, EventArgs e)
    {
        var result = await hybridWebView1.EvaluateJavaScriptAsync($"setPin({1})");

        var xx = result;
    }


    /// <summary>
    ///     Called when the webview finished navigating. Hides the loading label.
    /// </summary>
    private void webviewNavigated(object sender, WebNavigatedEventArgs e)
    {
        labelLoading.IsVisible = false;

        //hybridWebView.Uri = _vm.Url;
    }

    public async void StoreLocation(object sender, EventArgs e)
    {
        _vm.StoreLocation.Execute(null);
    }
}