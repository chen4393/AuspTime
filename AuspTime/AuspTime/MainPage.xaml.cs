using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AuspTime
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

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
    }
}
