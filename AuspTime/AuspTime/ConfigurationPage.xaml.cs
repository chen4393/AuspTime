using System;
using System.Collections.Generic;
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
    }
}