using System;
using Xamarin.Forms;

namespace FocusXamarinForms_V1_21_07_2020.ViewModels
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
