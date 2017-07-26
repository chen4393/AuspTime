using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuspTime
{
    public static class IndianCalendar
    {
        private static string[] sunday_lastnight = { "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH" };
        private static string[] sunday_today = { "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG" };
        private static string[] sunday_tonight = { "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH" };
        private static string[] sunday_tomorrow = { "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT" };

        private static string[] monday_lastnight = { "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH" };
        private static string[] monday_today = { "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT" };
        private static string[] monday_tonight = { "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL" };
        private static string[] monday_tomorrow = { "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG" };

        private static string[] tuesday_lastnight = { "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL" };
        private static string[] tuesday_today = { "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG" };
        private static string[] tuesday_tonight = { "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL" };
        private static string[] tuesday_tomorrow = { "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH" };

        private static string[] wednesday_lastnight = { "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL" };
        private static string[] wednesday_today = { "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH" };
        private static string[] wednesday_tonight = { "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG" };
        private static string[] wednesday_tomorrow = { "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH" };

        private static string[] thursday_lastnight = { "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG" };
        private static string[] thursday_today = { "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH" };
        private static string[] thursday_tonight = { "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT" };
        private static string[] thursday_tomorrow = { "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL" };

        private static string[] friday_lastnight = { "AMRIT", "CHAL", "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT" };
        private static string[] friday_today = { "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL" };
        private static string[] friday_tonight = { "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG" };
        private static string[] friday_tomorrow = { "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL" };

        private static string[] saturday_lastnight = { "ROG", "KAAL", "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG" };
        private static string[] saturday_today = { "KAAL", "SHUBH", "ROG", "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL" };
        private static string[] saturday_tonight = { "LABH", "UDWEG", "SHUBH", "AMRIT", "CHAL", "ROG", "KAAL", "LABH" };
        private static string[] saturday_tomorrow = { "UDWEG", "CHAL", "LABH", "AMRIT", "KAAL", "SHUBH", "ROG", "UDWEG" };

    }
}
