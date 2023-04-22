namespace FocusXamarinMobileApplication.Droid;

[Activity(Label = "FocusXamarinForms20082020V1", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : FormsAppCompatActivity
{
    private const int RequestLocationsId = 0;

    private readonly string[] LocationPermissions =
    {
        Manifest.Permission.AccessCoarseLocation,
        Manifest.Permission.AccessFineLocation
    };

    protected override void OnStart()
    {
        base.OnStart();

        if ((int)Build.VERSION.SdkInt >= 23)
            if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                RequestPermissions(LocationPermissions, RequestLocationsId);
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
        TabLayoutResource = Android.Resource.Layout.Tabbar;
        ToolbarResource = Android.Resource.Layout.Toolbar;
        SyncfusionLicenseProvider.RegisterLicense(
            "Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5gSX9bd0BhXn5ZeHM=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1QdkRrWHpcd3VcRWg=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViUX5bcnNXR2Rb;MTU2NDQzNEAzMjMxMmUzMTJlMzMzNUdJK1owK1BEUVBvdlkrYy9VRWJyaVUwZmZuN0hpV3dVMElRUHYyZXFoOE09;MTU2NDQzNUAzMjMxMmUzMTJlMzMzNUwrZE1JQzVSR1gzZnk2dEgzTmEyRVliaEVJYm5OU3RvL2V3Ry9XcWY3dUU9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUZjWHdfdXZSRWFVVg==;MTU2NDQzN0AzMjMxMmUzMTJlMzMzNUxZOEhjNG9lbHltSTB3NWN2YTlQNllwbE5kU0N5dWhhSVpBazcrVHJyV1k9;MTU2NDQzOEAzMjMxMmUzMTJlMzMzNVl6UHc1UzEzNVVGUUFlamlRVElVUXpibFloMlYwdVVuQnE1R0dFbnM2cUk9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUViUX5bcnNRR2Vb;MTU2NDQ0MEAzMjMxMmUzMTJlMzMzNWVnSFN0VmM0SVowT3hHTUcxUkp1RllMWDk4dzR6cWdxdzQ4M2VUMzY3anc9;MTU2NDQ0MUAzMjMxMmUzMTJlMzMzNWVrYzlBY1JYaFh4MEJEQXhhSzRPYzRRSGxuOGZnb0lnQkl5SENmeWVTUk09;MTU2NDQ0MkAzMjMxMmUzMTJlMzMzNUxZOEhjNG9lbHltSTB3NWN2YTlQNllwbE5kU0N5dWhhSVpBazcrVHJyV1k9");
        base.OnCreate(savedInstanceState);

        Platform.Init(this, savedInstanceState);
        Forms.Init(this, savedInstanceState);
        FormsMaps.Init(this, savedInstanceState);
        LoadApplication(new App());

        AppCenter.Start("06866c2b-2713-4586-b055-e581cb1cf41c",
            typeof(Analytics), typeof(Crashes));

        App.ParentWindow = this;
    }

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
        [GeneratedEnum] Permission[] grantResults)
    {
        Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        if (requestCode == RequestLocationsId)
            if (grantResults.Length == 1 && grantResults[0] == (int)Permission.Granted)
            {
                //Permission granted - disply a message;                    
            }

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);
        AuthenticationContinuationHelper
            .SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
    }
}