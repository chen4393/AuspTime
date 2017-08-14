using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using AuspTime.Droid;
using Java.Lang;
using Android.Locations;

[assembly: Dependency(typeof(Geolocator))]
namespace AuspTime.Droid
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Geolocator : Java.Lang.Object, IGeolocator, ILocationListener
    {
        LocationManager lm;

        public event EventHandler<ILocationEventArgs> locationObtained;

        event EventHandler<ILocationEventArgs> IGeolocator.locationObtained
        {
            add
            {
                locationObtained += value;
            }
            remove
            {
                locationObtained -= value;
            }
        }

        public void ObtainMyLocation()
        {
            lm = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            lm.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
            Location location;
            if (lm.IsProviderEnabled(LocationManager.GpsProvider))
            {
                location = lm.GetLastKnownLocation(LocationManager.GpsProvider);
            }
            else
            {
                location = lm.GetLastKnownLocation(LocationManager.NetworkProvider);
            }
            OnLocationChanged(location);
        }

        ~Geolocator()
        {
            lm.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            if (location != null)
            {
                LocationEventArgs args = new LocationEventArgs();
                args.lat = location.Latitude;
                args.lng = location.Longitude;
                locationObtained(this, args);
            }
        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }
    }
}