namespace FocusXamarinMobileApplication.Models
{
    public class CustomWebView : WebView_
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create("Uri",
            typeof(string),
            typeof(CustomWebView),
            default(string));

        public string Uri
        {
            get => (string) GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }
    }
}