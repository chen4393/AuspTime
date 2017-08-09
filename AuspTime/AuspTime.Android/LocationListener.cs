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
using Android.Locations;

namespace AuspTime.Droid
{
    class LocationListener : Java.Lang.Object, ILocationListener
    {
        public void OnLocationChanged(Location location)
        {

        }

        public void OnStatusChanged(string s, Availability a, Bundle bundle)
        {

        }

        public void OnProviderEnabled(string s)
        {

        }

        public void OnProviderDisabled(string s)
        {

        }
    }
}