using Plugin.Geolocator;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AuspTime
{
    public partial class MainPage : ContentPage
    {
        private double userLatitude;
        private double userLongitude;
        private double userOffset;

        private Label[] labelGroup1 = new Label[9];
        private Label[] labelGroup2 = new Label[9];
        private Label[] labelGroup3 = new Label[9];
        private Label[] labelGroup4 = new Label[9];

        private int[] lastnightTime = new int[8];
        private int[] todayTime = new int[8];
        private int[] tonightTime = new int[8];
        private int[] tomorrowTime = new int[8];

        public MainPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            SetPadding();
            SetLocation();
            InitPanel();
            CalculateTimeSequence();
            SetSequence();
        }

        private void SetPadding()
        {
            double padding = 0.0;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    padding = 20;
                    break;
                case Device.Android:
                    padding = 0;
                    break;
            }
            this.Padding = new Thickness(0, padding, 0, 0);
        }

        private async void SetLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, false);
                if (position != null)
                {
                    userLatitude = position.Latitude;
                    userLatitude = Math.Round(userLatitude * 100000.00) / 100000.00;
                    userLongitude = position.Longitude;
                    userLongitude = Math.Round(userLongitude * 100000.00) / 100000.00;
                }
                userOffset = new DateTimeOffset(DateTime.Now).Offset.Hours;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception Caught: " + e.ToString());
            }
        }

        private void InitPanel()
        {
            // Display the current date and time
            dateTimeTitle.Text = DateTime.Now.ToString("yyyy-MMM-dd h:mm tt");
            // Display using data description
            useCurrentOrConfigured.Text = "Using current date and location";

            labelGroup1[0] = labelLastnight1;
            labelGroup1[1] = labelLastnight2;
            labelGroup1[2] = labelLastnight3;
            labelGroup1[3] = labelLastnight4;
            labelGroup1[4] = labelLastnight5;
            labelGroup1[5] = labelLastnight6;
            labelGroup1[6] = labelLastnight7;
            labelGroup1[7] = labelLastnight8;
            labelGroup1[8] = labelLastnight9;

            labelGroup2[0] = labelToday1;
            labelGroup2[1] = labelToday2;
            labelGroup2[2] = labelToday3;
            labelGroup2[3] = labelToday4;
            labelGroup2[4] = labelToday5;
            labelGroup2[5] = labelToday6;
            labelGroup2[6] = labelToday7;
            labelGroup2[7] = labelToday8;
            labelGroup2[8] = labelToday9;

            labelGroup3[0] = labelTonight1;
            labelGroup3[1] = labelTonight2;
            labelGroup3[2] = labelTonight3;
            labelGroup3[3] = labelTonight4;
            labelGroup3[4] = labelTonight5;
            labelGroup3[5] = labelTonight6;
            labelGroup3[6] = labelTonight7;
            labelGroup3[7] = labelTonight8;
            labelGroup3[8] = labelTonight9;

            labelGroup4[0] = labelTomorrow1;
            labelGroup4[1] = labelTomorrow2;
            labelGroup4[2] = labelTomorrow3;
            labelGroup4[3] = labelTomorrow4;
            labelGroup4[4] = labelTomorrow5;
            labelGroup4[5] = labelTomorrow6;
            labelGroup4[6] = labelTomorrow7;
            labelGroup4[7] = labelTomorrow8;
            labelGroup4[8] = labelTomorrow9;

            labelGroup1[0].Text = "Last night";
            labelGroup2[0].Text = "Today";
            labelGroup3[0].Text = "Tonight";
            labelGroup4[0].Text = "Tomorrow";
        }

        private void CalculateTimeSequence()
        {
            SunTime sunTime = new SunTime(userLatitude, userLongitude, userOffset, DateTime.Now);
            int sunriseTimeTodayDefault = sunTime.sunriseTime, sunsetTimeTodayDefault = sunTime.sunsetTime;
            int flagrise = sunTime.flagrise, flagset = sunTime.flagset;

            DateTime yesterday = DateTime.Now.AddDays(-1);
            sunTime = new SunTime(userLatitude, userLongitude, userOffset, yesterday);
            int sunriseTimeYesterdayDefault = sunTime.sunriseTime, sunsetTimeYesterdayDefault = sunTime.sunsetTime;

            DateTime tomorrow = DateTime.Now.AddDays(1);
            sunTime = new SunTime(userLatitude, userLongitude, userOffset, tomorrow);
            int sunriseTimeTomorrowDefault = sunTime.sunriseTime, sunsetTimeTomorrowDefault = sunTime.sunsetTime;

            int temp1 = (86400 - sunsetTimeYesterdayDefault + sunriseTimeTodayDefault) / 8; // last night
            int temp2 = (sunsetTimeTodayDefault - sunriseTimeTodayDefault) / 8; // today
            int temp3 = (86400 - sunsetTimeTodayDefault + sunriseTimeTomorrowDefault) / 8; // tonight
            int temp4 = (sunsetTimeTomorrowDefault - sunriseTimeTomorrowDefault) / 8; // tomorrow

            for (int i = 0; i < 8; i++)
            {
                lastnightTime[i] = sunsetTimeYesterdayDefault + temp1 * i;
                todayTime[i] = sunriseTimeTodayDefault + temp2 * i;
                tonightTime[i] = sunsetTimeYesterdayDefault + temp3 * i;
                tomorrowTime[i] = sunriseTimeTomorrowDefault + temp4 * i;
            }
        }

        private void SetSequence()
        {
            int dayOfWeek = (int) DateTime.Now.DayOfWeek;
        }
    }
}
