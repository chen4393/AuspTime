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

[assembly: Dependency(typeof(Geolocator))]
namespace AuspTime.Droid
{
    public class Geolocator : IGeolocator
    {
        public Geolocator()
        {

        }

        public double[] GetCurrLatLon()
        {
            double[] data = new double[2];
            if (MainActivity.location != null)
            {
                data[0] = MainActivity.location.Latitude;
                data[1] = MainActivity.location.Longitude;
                return data;
            }
            return null;
        }
    }
}