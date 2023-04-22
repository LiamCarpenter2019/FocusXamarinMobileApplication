//[assembly: ExportRenderer(typeof(FocusMobileV3.Forms.Views.ExtMap), typeof(ExtMapRenderer))]

namespace FocusXamarinMobileApplication.Helpers;


/// <summary>
/// Renderer for the xamarin map.
/// Enable user to get a position by taping on the map.
/// </summary>
//public class ExtMapRendererA: MapRenderer, IOnMapReadyCallback
//{
//    // We use a native google map for Android
//    private GoogleMap _map;

//    public void OnMapReady(GoogleMap googleMap)
//    {
//        _map = googleMap;

//        if (_map != null)
//            _map.MapClick += googleMap_MapClick;
//    }

//    protected override void OnElementChanged(ElementChangedEventArgs<View> e)
//    {
//        if (_map != null)    //            _map.MapClick -= googleMap_MapClick;

//        base.OnElementChanged(e);

//        if (Control != null)
//            ((MKMapView)Control).GetMapAsync(this);
//    }

//    private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
//    {
//        ((ExtMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
//    }
//}