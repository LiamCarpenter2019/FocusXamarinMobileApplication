#region

using Java.Interop;
using Object = Java.Lang.Object;

#endregion

namespace FocusXamarinMobileApplication.Droid;

public class JSBridge : Object
{
    private readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

    public JSBridge(HybridWebViewRenderer hybridRenderer)
    {
        hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
    }

    [JavascriptInterface]
    [Export("invokeAction")]
    public void InvokeAction(string data)
    {
        HybridWebViewRenderer hybridRenderer;

        if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            ((HybridWebView)hybridRenderer.Element).InvokeAction(data);
    }
}