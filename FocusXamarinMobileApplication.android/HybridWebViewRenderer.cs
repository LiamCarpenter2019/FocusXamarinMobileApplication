﻿using FocusXamarinMobileApplication.Droid;
using FocusXamarinMobileApplication.Helpers;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]

namespace FocusXamarinMobileApplication.Droid;

public class HybridWebViewRenderer : WebViewRenderer
{
    private const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
    private Context _context;

    public HybridWebViewRenderer(Context context) : base(context)
    {
        _context = context;
    }

    protected override void OnElementChanged(ElementChangedEventArgs<WebView_> e)
    {
        base.OnElementChanged(e);

        if (e.OldElement != null)
        {
            Control.RemoveJavascriptInterface("jsBridge");
            ((HybridWebView)Element).Cleanup();
        }

        if (e.NewElement != null)
        {
            Control.SetWebViewClient(new JavascriptWebViewClient(this, $"javascript: {JavascriptFunction}"));
            Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
            Control.LoadUrl($"file:///android_asset/Content/{((HybridWebView)Element).Uri}");
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) ((HybridWebView)Element).Cleanup();
        base.Dispose(disposing);
    }
}