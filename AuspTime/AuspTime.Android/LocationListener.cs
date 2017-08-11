using Android.OS;
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