using System.ComponentModel;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.ViewModels;

public class OSMappingPageViewModel : FBaseViewModel, INotifyPropertyChanged
{
    public WebViewSource _webViewSource { get; internal set; }

    public WebViewSource WebViewSource
    {
        get => _webViewSource;
        set
        {
            _webViewSource = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand PageLoaded => new(async () =>
    {
        var htmlSource = new HtmlWebViewSource();

        htmlSource.Html = @"<html>
                                <head>
                                <link rel=""stylesheet"" href=""OSMapping.css"">
                                </head>
                                <body>
                                <h1>Xamarin.Forms</h1>
                                <p>The CSS and image are loaded from local files!</p>
                                <img src='XamarinLogo.png'/>
                                <p><a href=""OSMapping.html"">next page</a></p>
                                </body>
                                </html>";

        htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
        WebViewSource = htmlSource;
    });

    public interface IBaseUrl
    {
        string Get();
    }
}