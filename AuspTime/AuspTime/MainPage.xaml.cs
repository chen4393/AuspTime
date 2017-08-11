using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace AuspTime
{
    public partial class MainPage : ContentPage
    {
        private double userLatitude = 44.83661;
        private double userLongitude = -93.30022;
        private double userOffset = -5.00;

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

        private void SetLocation()
        {
            IGeolocator locator = DependencyService.Get<IGeolocator>();
            locator.locationObtained += (sender, e) =>
            {
                userLatitude = e.lat;
                userLongitude = e.lng;
            };
            locator.ObtainMyLocation();
            
            userOffset = new DateTimeOffset(DateTime.Now).Offset.Hours;
            Debug.WriteLine("userLatitude = " + userLatitude);
            Debug.WriteLine("userLongitude = " + userLongitude);
            Debug.WriteLine("userOffset = " + userOffset);
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

            
            Debug.WriteLine("DateTime.Now = " + DateTime.Now);

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
            Debug.WriteLine("temp1 = " + temp1);
            
            for (int i = 0; i < 8; i++)
            {
                lastnightTime[i] = sunsetTimeYesterdayDefault + temp1 * i;
                todayTime[i] = sunriseTimeTodayDefault + temp2 * i;
                tonightTime[i] = sunsetTimeTodayDefault + temp3 * i;
                tomorrowTime[i] = sunriseTimeTomorrowDefault + temp4 * i;
            }
        }

        private void SetSequence()
        {
            int day = (int) DateTime.Now.DayOfWeek;
            switch (day)
            {
                case 0:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.sundayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.sundayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.sundayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.sundayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(0);
                    break;

                case 1:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.mondayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.mondayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.mondayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.mondayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(1);
                    break;

                case 2:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.tuesdayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.tuesdayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.tuesdayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.tuesdayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(2);
                    break;

                case 3:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.wednesdayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.wednesdayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.wednesdayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.wednesdayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(3);
                    break;

                case 4:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.thursdayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.thursdayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.thursdayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.thursdayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(4);
                    break;

                case 5:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.fridayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.fridayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.fridayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.fridayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(5);
                    break;

                case 6:
                    for (int i = 0; i < 8; i++)
                    {
                        labelGroup1[i + 1].Text = IndianCalendar.saturdayLastnight[i] + " " + SplitTime(lastnightTime[i]);
                        labelGroup2[i + 1].Text = IndianCalendar.saturdayToday[i] + " " + SplitTime(todayTime[i]);
                        labelGroup3[i + 1].Text = IndianCalendar.saturdayTonight[i] + " " + SplitTime(tonightTime[i]);
                        labelGroup4[i + 1].Text = IndianCalendar.saturdayTomorrow[i] + " " + SplitTime(tomorrowTime[i]);
                    }
                    PaintColor();
                    UpdateBackgroundColor(6);
                    break;

                default:
                    break;
            }
            
        }

        private string SplitTime(int time)
        {

            string results = null;

            if (time > 86400)
            {
                time = time - 86400;
            }
            int hours = (time / 3600);
            int remainder = (time - 3600 * hours);
            int mins = remainder / 60;

            if (hours > 12)
            {
                results = string.Format("{0:D2}:{1:D2}" + " PM", hours - 12, mins);
            }
            else if (hours == 12)
            {
                results = string.Format("12:{0:D2}" + " PM", mins);
            }
            else if (hours > 0 && hours < 12)
            {
                results = string.Format("{0:D2}:{1:D2}" + " AM", hours, mins);
            }
            else if (hours == 0)
            {
                results = string.Format("12:{0:D2}" + " AM", mins);
            }

            return results;
        }

        private void PaintColor()
        {

        }

        private void UpdateBackgroundColor(int day)
        {

        }
    }
}
