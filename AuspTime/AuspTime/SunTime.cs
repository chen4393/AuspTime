using System;

namespace AuspTime
{
    class SunTime
    {
        public const double PI = 3.141592653589793;
        public double longitude { get; set; }
        public double latitude { get; set; }
        private double utcOffset;

        public int sunriseTime { get; set; }
        public int sunsetTime { get; set; }
        public int flagrise { get; set; }
        public int flagset { get; set; }

        private DateTime calendar;

        public SunTime()
        {
            latitude = 44.838331;
            longitude = -93.298806;
            utcOffset = -5.0;
        }

        // Create SunTime object for current date
        public SunTime(double latitude, double longitude, double offset, DateTime calendar)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            this.calendar = calendar;
            utcOffset = offset;
            Update();
        }

        private void Update()
        {
            sunriseTime = CalculateTime(1);
            sunsetTime = CalculateTime(2);
        }

        private int CalculateTime(int flag)
        {
            // Calculate day of year
            int dayOfYear = calendar.DayOfYear;
            
            // Convert the longitude to hour value and calculate an approximate time
            double lngHour = longitude / 15.0;
            double t;
            if (flag == 1)
            {
                t = dayOfYear + ((6.0 - lngHour) / 24.0);     // if rising
            }
            else
            {
                t = dayOfYear + ((18.0 - lngHour) / 24.0);    // if setting
            }

            // Calculate the Sun's mean anomaly
            double M = (0.9856 * t) - 3.289;

            // Calculate the Sun's true longitude, and adjust it to the range of (0, 360)
            double L = M + (1.916 * Math.Sin(Deg2Rad(M))) + (0.020 * Math.Sin(Deg2Rad(2 * M))) + 282.634;
            L = FixValue(L, 0, 360);

            // Calculate the Sun's right ascension, and adjust it to the range of (0, 360)
            double RA = Rad2Deg(Math.Atan(0.91764 * Math.Tan(Deg2Rad(L))));

            // Right ascension value needs to be in the same quadrant as L and need to be converted into hours
            double Lquadrant = (Math.Floor(L / 90.0)) * 90.0;
            double RAquadrant = (Math.Floor(RA / 90.0)) * 90.0;
            RA = RA + (Lquadrant - RAquadrant);
            RA = RA / 15.0;

            // Calculate the Sun's declination
            double sinDec = 0.39782 * Math.Sin(Deg2Rad(L));
            double cosDec = Math.Cos(Math.Asin(sinDec));

            // Calculate the Sun's local hour angle
            double cosH = (-0.01454 - (sinDec * Math.Sin(Deg2Rad(latitude)))) / (cosDec * Math.Cos(Deg2Rad(latitude)));
            if (cosH > 1)
            {
                flagrise = 100;
            }
            else if (cosH < -1)
            {
                flagset = 100;
            }

            // Finish calculating H and convert into hours
            double H;
            if (flag == 1)
            {
                H = 360.0 - Rad2Deg(Math.Acos(cosH));
            }
            else
            {
                H = Rad2Deg(Math.Acos(cosH));
            }
            H = H / 15.0;

            // Calculate local mean time of rising/setting
            double T = H + RA - (0.06571 * t) - 6.622;

            // Adjust back to UTC
            double UT = T - lngHour;

            UT = UT + utcOffset;
            UT = FixValue(UT, 0, 24);

            return (int) Math.Round(UT * 3600.0);
        }

        private static double Deg2Rad(double angle)
        {
            return PI * angle / 180.0;
        }

        private static double Rad2Deg(double angle)
        {
            return 180.0 * angle / PI;
        }

        private static double FixValue(double value, double min, double max)
        {
            while (value < min)
            {
                value += (max - min);
            }
            while (value >= max)
            {
                value -= (max - min);
            }
            return value;
        }
    }
}
