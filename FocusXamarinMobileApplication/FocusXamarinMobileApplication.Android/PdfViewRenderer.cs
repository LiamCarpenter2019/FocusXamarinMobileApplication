#region

using WebView = Android.Webkit.WebView;

#endregion

[assembly: ExportRenderer(typeof(PdfView), typeof(PdfViewRenderer))]

namespace FocusXamarinMobileApplication.Droid;

public class PdfViewRenderer : WebViewRenderer
{
    protected override void OnElementChanged(ElementChangedEventArgs<WebView_> e)
    {
        base.OnElementChanged(e);
        if (e.NewElement == null) return;
        var pdfView = Element as PdfView;
        if (pdfView == null) return;
        if (string.IsNullOrWhiteSpace(pdfView.Uri) == false)
            Control.SetWebChromeClient(new PdfWebChromeClient
            {
                Uri = pdfView.Uri,
                FileName = GetFileNameFromUri(pdfView.Uri)
            });
        Control.Settings.AllowFileAccess = true;
        Control.Settings.AllowUniversalAccessFromFileURLs = true;
        LoadFile(pdfView.Uri);
    }

    private static string GetFileNameFromUri(string uri)
    {
        var lastIndexOf = uri?.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase);
        return lastIndexOf > 0 ? uri.Substring(lastIndexOf.Value, uri.Length - lastIndexOf.Value) : string.Empty;
    }

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        base.OnElementPropertyChanged(sender, e);

        if (e.PropertyName != PdfView.UriProperty.PropertyName) return;
        var pdfView = Element as PdfView;
        if (pdfView == null) return;
        if (string.IsNullOrWhiteSpace(pdfView.Uri) == false)
            Control.SetWebChromeClient(new PdfWebChromeClient
            {
                Uri = pdfView.Uri,
                FileName = GetFileNameFromUri(pdfView.Uri)
            });
        LoadFile(pdfView.Uri);
    }

    private void LoadFile(string uri)
    {
        if (string.IsNullOrWhiteSpace(uri)) return;
        Control.LoadUrl($"file:///android_asset/pdfjs/web/viewer.html?file=file://{uri}");
    }

    internal class PdfWebChromeClient : WebChromeClient
    {
        public string Uri { private get; set; }
        public string FileName { private get; set; }

        public override bool OnJsAlert(WebView view, string url, string message, JsResult result)
        {
            if (message != "PdfViewer_app_scheme:print") return base.OnJsAlert(view, url, message, result);
            using (var printManager = Forms.Context.GetSystemService(Context.PrintService) as PrintManager)
            {
                printManager?.Print(FileName, new FilePrintDocumentAdapter(FileName, Uri), null);
            }

            return true;
        }
    }
}