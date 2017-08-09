using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuspTime
{
    public interface IGeolocator
    {
        double[] GetCurrLatLon();
    }
}
