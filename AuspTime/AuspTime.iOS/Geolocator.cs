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
    public class Geolocator : IGeolocator
    {
        double[] data;

        CLLocationManager locationManager;

        public Geolocator()
        {
            locationManager = new CLLocationManager();
            locationManager.DesiredAccuracy = 100;

            data = new double[2];
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                locationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    CLLocation location = e.Locations[e.Locations.Length - 1];
                    if (location != null)
                    {
                        data[0] = location.Coordinate.Latitude;
                        data[1] = location.Coordinate.Longitude;
                    }
                };
            }

            locationManager.AuthorizationChanged += (sender, e) =>
            {
                Debug.WriteLine("e.Status = " + e.Status + ", AuthorizedWhenInUse = " + CLAuthorizationStatus.AuthorizedWhenInUse);
                if (e.Status == CLAuthorizationStatus.AuthorizedWhenInUse)
                {
                    locationManager.StartUpdatingLocation();
                }
            };

            locationManager.RequestWhenInUseAuthorization();
        }

        public double[] GetCurrLatLon()
        {
            
            if (data[0] == 0)
            {
                return null;
            }
            return data;
        }
    }
}
