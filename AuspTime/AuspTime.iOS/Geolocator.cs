using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using AuspTime.iOS;
using CoreLocation;
using UIKit;
using System.Diagnostics;

[assembly: Dependency(typeof(Geolocator))]
namespace AuspTime.iOS
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Geolocator : IGeolocator
    {
        CLLocationManager lm;

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
            lm = new CLLocationManager();
            lm.DesiredAccuracy = CLLocation.AccuracyBest;
            lm.DistanceFilter = CLLocationDistance.FilterNone;

            lm.LocationsUpdated += (sender, e) =>
            {
                var locations = e.Locations;
                var strLocation = locations[locations.Length - 1].Coordinate.Latitude.ToString();
                strLocation = strLocation + "," + locations[locations.Length - 1].Coordinate.Longitude.ToString();
                LocationEventArgs args = new LocationEventArgs();
                args.lat = locations[locations.Length - 1].Coordinate.Latitude;
                args.lng = locations[locations.Length - 1].Coordinate.Longitude;
                locationObtained(this, args);
            };
            lm.AuthorizationChanged += (sender, e) =>
            {
                //Debug.WriteLine("e.Status = " + e.Status + ", AuthorizedWhenInUse = " + CLAuthorizationStatus.AuthorizedWhenInUse);
                if (e.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
                {
                    lm.StartUpdatingLocation();
                }
            };
            lm.RequestWhenInUseAuthorization();
        }

        ~Geolocator()
        {
            lm.StopUpdatingLocation();
        }
    }
}
