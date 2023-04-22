#region

using Foundation;
using Microsoft.Identity.Client;
using Syncfusion.Licensing;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfImageEditor.XForms.iOS;
using Syncfusion.SfPdfViewer.XForms.iOS;
using Syncfusion.SfRangeSlider.XForms.iOS;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

#endregion

namespace FocusXamarinMobileApplication.iOS;

// The UIApplicationDelegate for the application. This class is responsible for launching the 
// User Interface of the application, as well as listening (and optionally responding) to 
// application events from iOS.
[Register("AppDelegate")]
public class AppDelegate : FormsApplicationDelegate
{
    //
    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
#if ENABLE_TEST_CLOUD
        // Xamarin.Calabash.Start();
#endif
        Forms.Init();
        FormsMaps.Init();
        SyncfusionLicenseProvider.RegisterLicense(
            "Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5gSX9bd0BhXn5ZeHM=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1QdkRrWHpcd3VcRWg=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViUX5bcnNXR2Rb;MTU2NDQzNEAzMjMxMmUzMTJlMzMzNUdJK1owK1BEUVBvdlkrYy9VRWJyaVUwZmZuN0hpV3dVMElRUHYyZXFoOE09;MTU2NDQzNUAzMjMxMmUzMTJlMzMzNUwrZE1JQzVSR1gzZnk2dEgzTmEyRVliaEVJYm5OU3RvL2V3Ry9XcWY3dUU9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUZjWHdfdXZSRWFVVg==;MTU2NDQzN0AzMjMxMmUzMTJlMzMzNUxZOEhjNG9lbHltSTB3NWN2YTlQNllwbE5kU0N5dWhhSVpBazcrVHJyV1k9;MTU2NDQzOEAzMjMxMmUzMTJlMzMzNVl6UHc1UzEzNVVGUUFlamlRVElVUXpibFloMlYwdVVuQnE1R0dFbnM2cUk9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViUX5bcnNRR2Vb;MTU2NDQ0MEAzMjMxMmUzMTJlMzMzNWVnSFN0VmM0SVowT3hHTUcxUkp1RllMWDk4dzR6cWdxdzQ4M2VUMzY3anc9;MTU2NDQ0MUAzMjMxMmUzMTJlMzMzNWVrYzlBY1JYaFh4MEJEQXhhSzRPYzRRSGxuOGZnb0lnQkl5SENmeWVTUk09;MTU2NDQ0MkAzMjMxMmUzMTJlMzMzNUxZOEhjNG9lbHltSTB3NWN2YTlQNllwbE5kU0N5dWhhSVpBazcrVHJyV1k9");
        SfImageEditorRenderer.Init();
        // Syncfusion.SfMaps.XForms.iOS.SfMapsRenderer.Init();
        SfListViewRenderer.Init();
        //SfEffectsViewRenderer.Init();  //Initialize only when effects view is added to Listview.
        SfPdfDocumentViewRenderer.Init();
        SfRangeSliderRenderer.Init();
        App.iOSKeychainSecurityGroup = NSBundle.MainBundle.BundleIdentifier;
        LoadApplication(new App());
        App.ParentWindow = null; // no UI parent on iOS
        ObjCRuntime.Class.ThrowOnInitFailure = false;
        return base.FinishedLaunching(app, options);
    }

    // Handling redirect URL
    // See: https://github.com/azuread/microsoft-authentication-library-for-dotnet/wiki/Xamarin-iOS-specifics
    public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
    {
        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
        return true;
    }
}