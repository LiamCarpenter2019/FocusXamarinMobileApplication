using System;
using Xamarin.Forms;

namespace FocusXamarinMobileApplication.Views
{
    public partial class WebView : ContentPage
    {
        public WebView()
        {
            InitializeComponent();

            webView.Source = "http://maps.apple.com/?q=Great+George+St+Leeds+LS1+3EX";                             
        }

        async void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}
