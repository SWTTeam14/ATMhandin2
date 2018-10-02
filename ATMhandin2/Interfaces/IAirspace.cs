using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMhandin2.Interfaces
{
    public interface IAirspace
    {
        int South { get; set; }
        int West { get; set; }
        int North { get; set; }
        int East { get; set; }
        int LowerAltitude { get; set; }
        int UpperAltitude { get; set; }

        bool IsAircraftInside(ITransponderDataItem td);
    }
}