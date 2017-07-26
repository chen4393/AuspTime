using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuspTime
{
    static class IndianCalendar
    {
        public static string[] sunday_lastnight = { "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH" };
        public static string[] sunday_today = { "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG" };
        public static string[] sunday_tonight = { "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH" };
        public static string[] sunday_tomorrow = { "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT" };

        public static string[] monday_lastnight = { "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH" };
        public static string[] monday_today = { "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT" };
        public static string[] monday_tonight = { "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL" };
        public static string[] monday_tomorrow = { "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG" };

        public static string[] tuesday_lastnight = { "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL" };
        public static string[] tuesday_today = { "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG" };
        public static string[] tuesday_tonight = { "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL" };
        public static string[] tuesday_tomorrow = { "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH" };

        public static string[] wednesday_lastnight = { "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL" };
        public static string[] wednesday_today = { "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH" };
        public static string[] wednesday_tonight = { "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG" };
        public static string[] wednesday_tomorrow = { "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH" };

        public static string[] thursday_lastnight = { "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG" };
        public static string[] thursday_today = { "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH" };
        public static string[] thursday_tonight = { "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT" };
        public static string[] thursday_tomorrow = { "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL" };

        public static string[] friday_lastnight = { "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT" };
        public static string[] friday_today = { "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL" };
        public static string[] friday_tonight = { "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG" };
        public static string[] friday_tomorrow = { "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL" };

        public static string[] saturday_lastnight = { "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG" };
        public static string[] saturday_today = { "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL" };
        public static string[] saturday_tonight = { "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH" };
        public static string[] saturday_tomorrow = { "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG" };

    }
}
