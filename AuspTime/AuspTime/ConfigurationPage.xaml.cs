using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AuspTime
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfigurationPage : ContentPage
	{
		public ConfigurationPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            // Load setting data
            latitudeEntry.Text = MainPage.userLatitude.ToString();
            longitudeEntry.Text = MainPage.userLongitude.ToString();
            offsetEntry.Text = MainPage.userOffset.ToString();
            CheckDate();
            CheckLocation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.Properties.ContainsKey("MyPreferences"))
            {
                DateLocation preferences = Application.Current.Properties["MyPreferences"] as DateLocation;
                latitudeEntry.Text = preferences.myLatitude.ToString();
                longitudeEntry.Text = preferences.myLongitude.ToString();
                offsetEntry.Text = preferences.myOffset.ToString();
                datePicker.Date = preferences.myDate;
                CheckDate();
                CheckLocation();
            }
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnAboutClicked(object sender, EventArgs e)
        {
            string message = "Ace Auspicious Time\nVersion 2.0\nCopyright \u00A9 2017\nwww.pyrahealth.com\n\n" +
                "Ace Auspicious Time calculates beneficial times for activities at your location. Using Indian Vedic astrology, the day and night's eight Choghadiya Muhurtas are calculated.\n" +
                "The Choghadiya time intervals have a nature of being good, neutral, or bad for starting an activity. Your location and time zone are used to calculate your sunrise and sunset." +
                "There are seven types of Choghadiyas:\n" +
                "\u2022 AMRIT: nectar. [Moon, good]\n" +
                "\u2022 CHAL: neutral, okay.[Venus, neutral]\n" +
                "\u2022 KAAL: to go after (with hostile intention), persecute [Saturn, bad]\n" +
                "\u2022 LABH: gain. [Mercury, good]\n" +
                "\u2022 ROG: disease [Mars, bad]\n" +
                "\u2022 SHUBH: good [Jupiter, good]\n" +
                "\u2022 UDWEG: regret, fear, distress (separation from a beloved object). [Sun, bad]";
            DisplayAlert("About Ace Auspicious Time", message, "done");
        }

        private void OnDoneClicked(object sender, EventArgs e)
        {
            DateTime dateTime =
                new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day, 
                             DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            DateLocation dl = new DateLocation
            {
                myDate = dateTime,
                myLatitude = Double.Parse(latitudeEntry.Text),
                myLongitude = Double.Parse(longitudeEntry.Text),
                myOffset = Double.Parse(offsetEntry.Text)
            };
            Application.Current.Properties["MyPreferences"] = dl;
            OnPreviousPageButtonClicked(sender, e);
        }

        private void OnCurrentLocationClicked(object sender, EventArgs e)
        {
            latitudeEntry.Text = MainPage.userLatitude.ToString();
            longitudeEntry.Text = MainPage.userLongitude.ToString();
            double userOffset = new DateTimeOffset(DateTime.Now).Offset.Hours;
            offsetEntry.Text = userOffset.ToString();
            currentLocationButton.BackgroundColor = Color.Aquamarine;
        }

        private void OnCurrentDateClicked(object sender, EventArgs e)
        {
            datePicker.Date = DateTime.Now;
            currentDateButton.BackgroundColor = Color.Aquamarine;
        }

        private void CheckDate()
        {
            if (datePicker.Date.DayOfYear != DateTime.Now.DayOfYear)
            {
                currentDateButton.BackgroundColor = Color.Red;
            }
        }

        private void CheckLocation()
        {
            double[] currLocation = GetCurrentLocation();
            double diffLatitude = Math.Abs(currLocation[0] - Double.Parse(latitudeEntry.Text));
            double diffLongitude = Math.Abs(currLocation[1] - Double.Parse(longitudeEntry.Text));
            if (diffLatitude > 0.01 || diffLongitude > 0.01)
            {
                currentLocationButton.BackgroundColor = Color.Red;
            }
            else
            {
                currentLocationButton.BackgroundColor = Color.Aquamarine;
            }
        }

        private double[] GetCurrentLocation()
        {
            double[] data = new double[2];
            int platform = 0;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    platform = 1;
                    break;
            }

            if (platform == 0) // Android
            {
                IGeolocator locator = DependencyService.Get<IGeolocator>();
                double currentLatitude = 0, currentLongitude = 0;
                locator.locationObtained += (sender, e) =>
                {
                    currentLatitude = e.lat;
                    currentLatitude = Math.Round(currentLatitude * 100000.00) / 100000.00;
                    currentLongitude = e.lng;
                    currentLongitude = Math.Round(currentLongitude * 100000.00) / 100000.00;
                };
                locator.ObtainMyLocation();
                data[0] = currentLatitude;
                data[1] = currentLongitude;
            }
            else // iOS
            {
                data[0] = MainPage.userLatitude;
                data[1] = MainPage.userLongitude;
            }

            return data;
        }
    }
}