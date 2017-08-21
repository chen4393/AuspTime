using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace AuspTime
{
    public partial class MainPage : ContentPage
    {
        public static double userLatitude = 30;
        public static double userLongitude = -94;
        public static double currrentLatitude;
        public static double currentLongitude;

        public double UserLatitude
        {
            get
            {
                return userLatitude;
            }

            set
            {
                if (Math.Abs(userLatitude - value) > 0.01)
                {
                    SendPropertyChanging("UserLatitude");
                    userLatitude = value;
                    userLatitude = Math.Round(userLatitude * 100000.00) / 100000.00;
                    currrentLatitude = userLatitude;
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Init();
                    }
                    else
                    {
                        if (isConfigured)
                        {
                            ReInit();
                        }
                        else
                        {
                            Init();
                        }
                    }
                    

                    if (Application.Current.Properties.ContainsKey("Default"))
                    {
                        DateLocation defaultLoc = Application.Current.Properties["Default"] as DateLocation;
                        defaultLoc.myLatitude = value;
                        Application.Current.Properties["Default"] = defaultLoc;
                    }
                }
            }
        }

        private void SendPropertyChanging(string property)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(property));
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public static void UserLocationPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            Debug.WriteLine(e.PropertyName + " has been changing");
        }

        public double UserLongitude
        {
            get
            {
                return userLongitude;
            }

            set
            {
                if (Math.Abs(userLongitude - value) > 0.01)
                {
                    SendPropertyChanging("UserLongitude");
                    userLongitude = value;
                    userLongitude = Math.Round(userLongitude * 100000.00) / 100000.00;
                    currentLongitude = userLongitude;
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        Init();
                    }
                    else
                    {
                        if (isConfigured)
                        {
                            ReInit();
                        }
                        else
                        {
                            Init();
                        }
                    }

                    if (Application.Current.Properties.ContainsKey("Default"))
                    {
                        DateLocation defaultLoc = Application.Current.Properties["Default"] as DateLocation;
                        defaultLoc.myLatitude = value;
                        Application.Current.Properties["Default"] = defaultLoc;
                    }
                }
            }
        }

        public static double userOffset = -5.00;
        public DateTime userDate = DateTime.Now;

        private Label[] labelGroup1 = new Label[9];
        private Label[] labelGroup2 = new Label[9];
        private Label[] labelGroup3 = new Label[9];
        private Label[] labelGroup4 = new Label[9];

        private int[] lastnightTime = new int[8];
        private int[] todayTime = new int[8];
        private int[] tonightTime = new int[8];
        private int[] tomorrowTime = new int[8];

        private bool isToday = true;
        private bool isLocationChanged = false;
        private bool isConfigured = false;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            this.PropertyChanging += new PropertyChangingEventHandler(UserLocationPropertyChanging);

            Init();

            aboutButton.Clicked += delegate
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
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            isConfigured = true;
            ReInit();
        }

        private void Init()
        {
            
            SetPadding();
            SetLocation();
            InitPanel();
            CalculateTimeSequence();
            SetSequence();
        }

        private void ReInit()
        {
            if (!Application.Current.Properties.ContainsKey("MyPreferences"))
            {
                return;
            }
            DateLocation dl = Application.Current.Properties["MyPreferences"] as DateLocation;
            dateTimeTitle.Text = dl.myDate.ToString("yyyy-MMM-dd h:mm tt");
            if (dl.myDate.DayOfYear != DateTime.Now.DayOfYear)
            {
                isToday = false;
                useCurrentOrConfigured.Text = "Using configured date and location";
            }
            else
            {
                isToday = true;
            }
            userDate = dl.myDate;
            ResetLocation();
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
                UserLatitude = e.lat;
                UserLatitude = Math.Round(UserLatitude * 100000.00) / 100000.00;
                userLatitude = UserLatitude;
                UserLongitude = e.lng;
                UserLongitude = Math.Round(userLongitude * 100000.00) / 100000.00;
                userLongitude = UserLongitude;
            };
            locator.ObtainMyLocation();

            Debug.WriteLine("userLatitude: " + userLatitude + ", userLongitude: " + userLongitude);

            userOffset = new DateTimeOffset(DateTime.Now).Offset.Hours;

            DateLocation defaultSettings = new DateLocation
            {
                myDate = DateTime.Now,
                myLatitude = userLatitude,
                myLongitude = userLongitude,
                myOffset = userOffset
            };
            Application.Current.Properties["Default"] = defaultSettings;
        }

        private void ResetLocation()
        {
            DateLocation dl = Application.Current.Properties["MyPreferences"] as DateLocation;
            isLocationChanged = CheckLocation(dl.myLatitude, dl.myLongitude, dl.myOffset);
            if (isLocationChanged)
            {
                useCurrentOrConfigured.Text = "Using configured date and location";
                userLatitude = dl.myLatitude;
                userLongitude = dl.myLongitude;
                userOffset = dl.myOffset;
            }
            else if (isToday)
            {
                useCurrentOrConfigured.Text = "Using current date and location";
            }
        }

        private bool CheckLocation(double lat, double lng, double offset)
        {
            bool locationChanged = true;
            double diffLatitude = Math.Abs(currrentLatitude - lat);
            double diffLongitude = Math.Abs(currentLongitude - lng);
            double diffOffset = Math.Abs(userOffset - offset);
            if (diffLatitude <= 0.01 && diffLongitude <= 0.01 && diffOffset <= 0.01)
            {
                locationChanged = false;
            }
            return locationChanged;
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
            SunTime sunTime = new SunTime(userLatitude, userLongitude, userOffset, userDate);
            int sunriseTimeTodayDefault = sunTime.sunriseTime, sunsetTimeTodayDefault = sunTime.sunsetTime;
            int flagrise = sunTime.flagrise, flagset = sunTime.flagset;

            DateTime yesterday = userDate.AddDays(-1);
            sunTime = new SunTime(userLatitude, userLongitude, userOffset, yesterday);
            int sunriseTimeYesterdayDefault = sunTime.sunriseTime, sunsetTimeYesterdayDefault = sunTime.sunsetTime;

            DateTime tomorrow = userDate.AddDays(1);
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
                tonightTime[i] = sunsetTimeTodayDefault + temp3 * i;
                tomorrowTime[i] = sunriseTimeTomorrowDefault + temp4 * i;
            }
        }

        private void SetSequence()
        {
            int day = (int) userDate.DayOfWeek;
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
                    break;

                default:
                    break;
            }
            UpdateClockBackgroundColor();
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
            for (int i = 1; i < 9; i++)
            {
                if (new Regex("AMRIT.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.FromRgba(175, 230, 255, 255); }
                else if (new Regex("LABH.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.FromRgba(120, 200, 200, 255); }
                else if (new Regex("SHUBH.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.FromRgba(0, 165, 190, 255); }
                else if (new Regex("CHAL.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.White; }
                else if (new Regex("KAAL.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.FromRgba(255, 190, 190, 255); }
                else if (new Regex("ROG.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.FromRgba(255, 128, 128, 255); }
                else if (new Regex("UDWEG.*").IsMatch(labelGroup1[i].Text.ToString())) { labelGroup1[i].BackgroundColor = Color.FromRgba(255, 0, 0, 255); }
                else { labelGroup1[i].BackgroundColor = Color.Black; }
            }

            for (int i = 1; i < 9; i++)
            {
                if (new Regex("AMRIT.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.FromRgba(175, 230, 255, 255); }
                else if (new Regex("LABH.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.FromRgba(120, 200, 200, 255); }
                else if (new Regex("SHUBH.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.FromRgba(0, 165, 190, 255); }
                else if (new Regex("CHAL.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.White; }
                else if (new Regex("KAAL.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.FromRgba(255, 190, 190, 255); }
                else if (new Regex("ROG.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.FromRgba(255, 128, 128, 255); }
                else if (new Regex("UDWEG.*").IsMatch(labelGroup2[i].Text.ToString())) { labelGroup2[i].BackgroundColor = Color.FromRgba(255, 0, 0, 255); }
                else { labelGroup2[i].BackgroundColor = Color.Black; }
            }

            for (int i = 1; i < 9; i++)
            {
                if (new Regex("AMRIT.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.FromRgba(175, 230, 255, 255); }
                else if (new Regex("LABH.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.FromRgba(120, 200, 200, 255); }
                else if (new Regex("SHUBH.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.FromRgba(0, 165, 190, 255); }
                else if (new Regex("CHAL.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.White; }
                else if (new Regex("KAAL.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.FromRgba(255, 190, 190, 255); }
                else if (new Regex("ROG.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.FromRgba(255, 128, 128, 255); }
                else if (new Regex("UDWEG.*").IsMatch(labelGroup3[i].Text.ToString())) { labelGroup3[i].BackgroundColor = Color.FromRgba(255, 0, 0, 255); }
                else { labelGroup3[i].BackgroundColor = Color.Black; }
            }

            for (int i = 1; i < 9; i++)
            {
                if (new Regex("AMRIT.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.FromRgba(175, 230, 255, 255); }
                else if (new Regex("LABH.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.FromRgba(120, 200, 200, 255); }
                else if (new Regex("SHUBH.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.FromRgba(0, 165, 190, 255); }
                else if (new Regex("CHAL.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.White; }
                else if (new Regex("KAAL.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.FromRgba(255, 190, 190, 255); }
                else if (new Regex("ROG.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.FromRgba(255, 128, 128, 255); }
                else if (new Regex("UDWEG.*").IsMatch(labelGroup4[i].Text.ToString())) { labelGroup4[i].BackgroundColor = Color.FromRgba(255, 0, 0, 255); }
                else { labelGroup4[i].BackgroundColor = Color.Black; }
            }
        }

        private void UpdateClockBackgroundColor()
        {
            DateTime calendar = DateTime.Now;
            int hour = calendar.Hour;
            int minute = calendar.Minute;
            int second = calendar.Second;
            int temp = hour * 3600 + minute * 60 + second;

            if (temp < todayTime[0])
            {
                temp += 86400;
                FillTitleBackgroundColor(temp, lastnightTime, labelGroup1);
            }
            else if (temp >= todayTime[0] && temp < tonightTime[0])
            {
                FillTitleBackgroundColor(temp, todayTime, labelGroup2);
            }
            else
            {
                FillTitleBackgroundColor(temp, tonightTime, labelGroup3);
            }
        }

        private void FillTitleBackgroundColor(int temp, int[] time, Label[] group)
        {
            int index = 0;
            if (temp >= time[0] - 20 && temp < time[1] - 20)
            {
                dateTimeTitle.BackgroundColor = group[1].BackgroundColor;
                index = 1;
            }
            if (temp >= time[1] - 20 && temp < time[2] - 20)
            {
                dateTimeTitle.BackgroundColor = group[2].BackgroundColor;
                index = 2;
            }
            if (temp >= time[2] - 20 && temp < time[3] - 20)
            {
                dateTimeTitle.BackgroundColor = group[3].BackgroundColor;
                index = 3;
            }
            if (temp >= time[3] - 20 && temp < time[4] - 20)
            {
                dateTimeTitle.BackgroundColor = group[4].BackgroundColor;
                index = 4;
            }
            if (temp >= time[4] - 20 && temp < time[5] - 20)
            {
                dateTimeTitle.BackgroundColor = group[5].BackgroundColor;
                index = 5;
            }
            if (temp >= time[5] - 20 && temp < time[6] - 20)
            {
                dateTimeTitle.BackgroundColor = group[6].BackgroundColor;
                index = 6;
            }
            if (temp >= time[6] - 20 && temp < time[7] - 20)
            {
                dateTimeTitle.BackgroundColor = group[7].BackgroundColor;
                index = 7;
            }
            if (temp >= time[7] - 20 && temp < time[8] - 20)
            {
                dateTimeTitle.BackgroundColor = group[8].BackgroundColor;
                index = 8;
            }
            if (isToday)
            {
                group[index].Text = ("\u261E " + group[index].Text);
            }
        }

        async void OnConfigure(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurationPage());
        }
    }
}
