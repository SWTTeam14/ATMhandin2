using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMhandin2.Interfaces;

namespace ATMhandin2.Classes
{
    public class Airspace : IAirspace
    {
        public Airspace(int South, int West, int North, int East, int LowerAltitude, int UpperAltitude)
        {
            this.South = South;
            this.West = West;
            this.North = North;
            this.East = East;
            this.LowerAltitude = LowerAltitude;
            this.UpperAltitude = UpperAltitude;
            
        }
        public int South { get; set; }
        public int West { get; set; }
        public int North { get; set; }
        public int East { get; set; }
        public int LowerAltitude { get; set; }
        public int UpperAltitude { get; set; }

        public bool IsAircraftInside(ITransponderDataItem td)
        {
            return td.XCoordinate <= East && td.XCoordinate >= West && td.YCoordinate <= North && td.YCoordinate >= South &&
                   td.Altitude >= LowerAltitude && td.Altitude <= UpperAltitude;
        }
    }
}
