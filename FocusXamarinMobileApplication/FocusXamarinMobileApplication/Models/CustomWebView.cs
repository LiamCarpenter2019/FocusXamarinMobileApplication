﻿using Xamarin.Forms;
using WebView = FocusXamarinMobileApplication.Views.WebView;

namespace FocusXamarinMobileApplication.Models
{
    public class CustomWebView : WebView
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