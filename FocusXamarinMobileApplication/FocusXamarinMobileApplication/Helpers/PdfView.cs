namespace FocusXamarinMobileApplication.Helpers;

public class PdfView : Xamarin.Forms.WebView
{
    public static readonly BindableProperty UriProperty =
        BindableProperty.Create(nameof(Uri), typeof(string), typeof(PdfView));

    public string Uri
    {
        get => (string)GetValue(UriProperty);
        set => SetValue(UriProperty, value);
    }
}