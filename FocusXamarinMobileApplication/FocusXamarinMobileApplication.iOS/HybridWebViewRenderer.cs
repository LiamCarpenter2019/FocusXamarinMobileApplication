#region

using System.IO;
using FocusXamarinMobileApplication.Helpers;
using FocusXamarinMobileApplication.iOS;
using Foundation;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

#endregion

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]

namespace FocusXamarinMobileApplication.iOS;

public class HybridWebViewRenderer : WkWebViewRenderer, IWKScriptMessageHandler
{
    private const string JavaScriptFunction =
        "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";

    private readonly WKUserContentController userController;

    public HybridWebViewRenderer() : this(new WKWebViewConfiguration())
    {
    }

    public HybridWebViewRenderer(WKWebViewConfiguration config) : base(config)
    {
        userController = config.UserContentController;
        var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
        userController.AddUserScript(script);
        userController.AddScriptMessageHandler(this, "invokeAction");
    }

    public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
    {
        ((HybridWebView)Element).InvokeAction(message.Body.ToString());
    }

    protected override void OnElementChanged(VisualElementChangedEventArgs e)
    {
        base.OnElementChanged(e);

        if (e.OldElement != null)
        {
            userController.RemoveAllUserScripts();
            userController.RemoveScriptMessageHandler("invokeAction");
            var hybridWebView = e.OldElement as HybridWebView;
            hybridWebView.Cleanup();
        }

        if (e.NewElement != null)
        {
            var filename = Path.Combine(NSBundle.MainBundle.BundlePath, $"Content/{((HybridWebView)Element).Uri}");
            LoadRequest(new NSUrlRequest(new NSUrl(filename, false)));
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) ((HybridWebView)Element).Cleanup();
        base.Dispose(disposing);
    }
}