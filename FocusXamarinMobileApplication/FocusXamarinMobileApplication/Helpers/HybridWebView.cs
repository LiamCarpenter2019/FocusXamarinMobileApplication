namespace FocusXamarinMobileApplication.Helpers;

public class HybridWebView : WebView_
{
    public static readonly BindableProperty UriProperty = BindableProperty.Create(
        "Uri",
        typeof(string),
        typeof(HybridWebView));


    private Action<string> action;


    public string Uri
    {
        get => (string)GetValue(UriProperty);
        set => SetValue(UriProperty, value);
    }

    public void RegisterAction(Action<string> callback)
    {
        action = callback;
    }

    public void Cleanup()
    {
        action = null;
    }

    public string InvokeAction(string data)
    {
        if (action == null || data == null) return "";
        action.Invoke(data);

        return "";
    }
}