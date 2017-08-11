using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android;
using Android.Content;
using Android.Support.V4.App;

namespace AuspTime.Droid
{
    [Activity(Label = "AuspTime", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Location location;

        protected override void OnCreate(Bundle bundle)
        {
            LocationManager locationManager = (LocationManager)GetSystemService(Context.LocationService);
            LocationListener locationListener = new LocationListener();

            const string permission = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission) != (int)Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { permission }, 1);
            }
            else
            {
                locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, locationListener);
                location = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
            }

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

